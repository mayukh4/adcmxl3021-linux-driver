// Decompiled with JetBrains decompiler
// Type: StreamDataLogger.Logger
// Assembly: StreamDataLogger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 328A96D1-45A7-47F9-A7ED-7DBD0C49147E
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.xml

using adisInterface;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Timers;

#nullable disable
namespace StreamDataLogger;

public class Logger : IDisposable
{
  private bool m_StreamRunning;
  private IBufferedStreamProducer m_StreamProducer;
  private ConcurrentQueue<ushort[]> m_DataQueue;
  private int m_BufferCount;
  private int m_BuffersSinceWrite;
  private Thread m_WriteThread;
  private BlockingCollection<FileCommand> m_CommandQueue;
  private System.Timers.Timer m_EventTimer;
  private int m_Progress;
  private bool m_StreamFinalized;
  private bool m_DutStreamRunning;
  private bool m_lowerFirst;
  private uint m_Buffers;
  private uint m_Captures;
  private uint m_BuffersPerWrite;
  private uint m_LinesPerFile;
  private uint m_BufferTimeoutSeconds;
  private string m_FileBaseName;
  private string m_FilePath;
  private string m_FileExtension;
  private IEnumerable<RegClass> m_RegList;
  private string m_DataSeparator;
  private bool m_TimeoutOccurred;

  private virtual IStreamEventProducer m_StreamEvents
  {
    get => this._m_StreamEvents;
    [MethodImpl(MethodImplOptions.Synchronized)] set
    {
      IStreamEventProducer.NewBufferAvailableEventHandler availableEventHandler = new IStreamEventProducer.NewBufferAvailableEventHandler(this.NewDataHandler);
      IStreamEventProducer.StreamFinishedEventHandler finishedEventHandler = new IStreamEventProducer.StreamFinishedEventHandler(this.StreamFinished);
      IStreamEventProducer mStreamEvents1 = this._m_StreamEvents;
      if (mStreamEvents1 != null)
      {
        mStreamEvents1.NewBufferAvailable -= availableEventHandler;
        mStreamEvents1.StreamFinished -= finishedEventHandler;
      }
      this._m_StreamEvents = value;
      IStreamEventProducer mStreamEvents2 = this._m_StreamEvents;
      if (mStreamEvents2 == null)
        return;
      mStreamEvents2.NewBufferAvailable += availableEventHandler;
      mStreamEvents2.StreamFinished += finishedEventHandler;
    }
  }

  [field: AccessedThroughProperty("m_BackgroundWorker")]
  private virtual BackgroundWorker m_BackgroundWorker { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

  /// <summary>
  /// Property to get/set if the lower word is returned first for a 32-bit register
  /// </summary>
  /// <returns></returns>
  public bool LowerWordFirst
  {
    get => this.m_lowerFirst;
    set => this.m_lowerFirst = value;
  }

  /// <summary>
  /// The total number of buffers to read in the stream. Each buffer is the reglist read numcaptures times
  /// </summary>
  /// <returns></returns>
  public uint Buffers
  {
    get => this.m_Buffers;
    set
    {
      this.m_Buffers = value >= 1U ? value : throw new ArgumentException("ERROR: Buffers must be at least 1");
    }
  }

  /// <summary>Number of captures of the reglist to perform in a single buffer.</summary>
  /// <returns></returns>
  public uint Captures
  {
    get => this.m_Captures;
    set => this.m_Captures = value;
  }

  /// <summary>The number of buffers to accumulate before a file write operation.</summary>
  /// <returns></returns>
  public uint BuffersPerWrite
  {
    get => this.m_BuffersPerWrite;
    set
    {
      this.m_BuffersPerWrite = value >= 1U ? value : throw new ArgumentException("ERROR: Buffers per write must be at least 1");
    }
  }

  /// <summary>Number of data rows in a single file.</summary>
  /// <returns></returns>
  public uint FileMaxDataRows
  {
    get => this.m_LinesPerFile;
    set
    {
      this.m_LinesPerFile = value >= 1U ? value : throw new ArgumentException("ERROR: Must have at least one buffer per file");
    }
  }

  /// <summary>Timeout period before stream cancellation if a buffer is not recieved.</summary>
  /// <returns></returns>
  public uint BufferTimeoutSeconds
  {
    get => this.m_BufferTimeoutSeconds;
    set
    {
      this.m_BufferTimeoutSeconds = value >= 1U ? value : throw new ArgumentException("ERROR: Timeout must be at least one second");
      this.m_EventTimer.Interval = (double) checked ((long) value * 1000L);
    }
  }

  /// <summary>File base name. File numbers will be appended after this.</summary>
  /// <returns></returns>
  public string FileBaseName
  {
    get => this.m_FileBaseName;
    set => this.m_FileBaseName = value;
  }

  /// <summary>Base path for the output files</summary>
  /// <returns></returns>
  public string FilePath
  {
    get => this.m_FilePath;
    set
    {
      this.m_FilePath = value;
      if (Operators.CompareString(Conversions.ToString(this.m_FilePath.Last<char>()), "\\", false) == 0)
        return;
      this.m_FilePath += "\\";
    }
  }

  /// <summary>File extension. Defaults to "csv"</summary>
  /// <returns></returns>
  public string FileExtension
  {
    get => this.m_FileExtension;
    set
    {
      this.m_FileExtension = value;
      this.m_FileExtension = this.m_FileExtension.Replace(".", "");
    }
  }

  /// <summary>Register list to stream from. Can be on multiple pages</summary>
  /// <returns></returns>
  public IEnumerable<RegClass> RegList
  {
    get => this.m_RegList;
    set => this.m_RegList = value;
  }

  /// <summary>String to seperate data elements. Defaults to ','</summary>
  /// <returns></returns>
  public string DataSeparator
  {
    get => this.m_DataSeparator;
    set => this.m_DataSeparator = value;
  }

  /// <summary>Readonly property to check if a stream is currently running.</summary>
  /// <returns></returns>
  public bool Busy => this.m_StreamRunning | this.m_DutStreamRunning;

  /// <summary>Readonly property to check if a timeout has occurred.</summary>
  /// <returns></returns>
  public bool TimeoutOccurred => this.m_TimeoutOccurred;

  /// <summary>
  /// This event is raised when a new progress tick has been made during a stream. Values will be in range (0 - 100)
  /// </summary>
  /// <param name="e"></param>
  public event Logger.ProgressChangedEventHandler ProgressChanged;

  /// <summary>This event is raised when all data is done being written to the disk.</summary>
  public event Logger.RunAsyncCompletedEventHandler RunAsyncCompleted;

  /// <summary>Constructor, takes stream producer and event producer</summary>
  /// <param name="EventProducer"></param>
  /// <param name="DataProducer"></param>
  public Logger(ref IStreamEventProducer EventProducer, ref IBufferedStreamProducer DataProducer)
  {
    this.m_StreamProducer = DataProducer;
    this.m_StreamEvents = EventProducer;
    this.LoadDefaultValues();
  }

  /// <summary>Starts an asynchronous stream</summary>
  /// <returns>If the stream start was successful. A stream cannot be started until previous stream has been terminated</returns>
  public bool RunAsync()
  {
    if (this.m_StreamRunning)
      return false;
    this.ResetStateVariables();
    // ISSUE: reference to a compiler-generated field
    Logger.ProgressChangedEventHandler progressChangedEvent = this.ProgressChangedEvent;
    if (progressChangedEvent != null)
      progressChangedEvent(new ProgressChangedEventArgs(0, (object) null));
    this.m_StreamRunning = true;
    this.m_WriteThread = new Thread(new ThreadStart(this.WriteThreadWorker));
    this.m_WriteThread.IsBackground = true;
    this.m_WriteThread.Start();
    this.m_CommandQueue.Add(FileCommand.CreateFile);
    this.m_StreamProducer.StartBufferedStream(this.m_RegList, this.m_Captures, this.m_Buffers, checked ((int) this.m_BufferTimeoutSeconds), this.m_BackgroundWorker);
    this.m_DutStreamRunning = true;
    return true;
  }

  /// <summary>Cancels a currently running stream.</summary>
  public void CancelAsync()
  {
    if (!this.m_StreamRunning)
      return;
    this.m_StreamEvents.CancelStreamAsync();
    this.NewDataHandler(0);
    this.m_StreamFinalized = true;
    this.m_CommandQueue.Add(FileCommand.FinalizeFile);
  }

  public void Dispose()
  {
    if (!Information.IsNothing((object) this.m_CommandQueue))
      this.m_CommandQueue.Dispose();
    if (Information.IsNothing((object) this.m_EventTimer))
      return;
    this.m_EventTimer.Dispose();
  }

  /// <summary>Cancels if the timeout occurred</summary>
  private void TimeoutCallback()
  {
    this.m_TimeoutOccurred = true;
    this.CancelAsync();
  }

  private void NewDataHandler(int BufferCount)
  {
    if (this.m_StreamFinalized)
      return;
    this.m_EventTimer.Enabled = false;
    ushort[] streamDataPacket = this.m_StreamProducer.GetBufferedStreamDataPacket();
    while (!Information.IsNothing((object) streamDataPacket) & (long) this.m_BufferCount < (long) this.m_Buffers)
    {
      this.m_DataQueue.Enqueue(streamDataPacket);
      Interlocked.Increment(ref this.m_BufferCount);
      Interlocked.Increment(ref this.m_BuffersSinceWrite);
      if ((long) this.m_BuffersSinceWrite >= (long) this.m_BuffersPerWrite)
      {
        this.m_CommandQueue.Add(FileCommand.WriteData);
        this.m_BuffersSinceWrite = 0;
      }
      streamDataPacket = this.m_StreamProducer.GetBufferedStreamDataPacket();
      long progressPercentage = checked ((long) Math.Round(unchecked ((double) checked ((long) this.m_BufferCount * 100L) / (double) this.m_Buffers)));
      if (progressPercentage != (long) this.m_Progress)
      {
        // ISSUE: reference to a compiler-generated field
        Logger.ProgressChangedEventHandler progressChangedEvent = this.ProgressChangedEvent;
        if (progressChangedEvent != null)
          progressChangedEvent(new ProgressChangedEventArgs(checked ((int) progressPercentage), (object) null));
        this.m_Progress = checked ((int) progressPercentage);
      }
    }
    if ((long) this.m_BufferCount >= (long) this.m_Buffers)
    {
      this.m_CommandQueue.Add(FileCommand.FinalizeFile);
      this.m_StreamFinalized = true;
    }
    else
      this.m_EventTimer.Enabled = true;
  }

  private void StreamFinished()
  {
    if (!this.m_StreamRunning)
    {
      // ISSUE: reference to a compiler-generated field
      Logger.RunAsyncCompletedEventHandler asyncCompletedEvent = this.RunAsyncCompletedEvent;
      if (asyncCompletedEvent != null)
        asyncCompletedEvent();
    }
    this.m_DutStreamRunning = false;
  }

  private void WriteThreadWorker()
  {
    StreamWriter streamWriter = (StreamWriter) null;
    int FileNumber = 0;
    int num1 = 0;
    bool flag = true;
    ushort[] result = (ushort[]) null;
    int num2 = 0;
    while (flag)
    {
      switch (this.m_CommandQueue.Take())
      {
        case FileCommand.CreateFile:
          streamWriter = this.CreateFile(FileNumber);
          break;
        case FileCommand.WriteData:
          int buffersPerWrite = checked ((int) this.m_BuffersPerWrite);
          int num3 = 1;
          while (num3 <= buffersPerWrite && this.m_DataQueue.TryDequeue(out result))
          {
            streamWriter.Write(this.BuildLines(result));
            checked { ++num2; }
            num1 = checked ((int) ((long) num1 + (long) this.m_Captures));
            if ((long) num1 >= (long) this.m_LinesPerFile)
            {
              streamWriter.Close();
              checked { ++FileNumber; }
              num1 = 0;
              if ((long) num2 < (long) this.m_Buffers)
                streamWriter = this.CreateFile(FileNumber);
            }
            checked { ++num3; }
          }
          break;
        case FileCommand.FinalizeFile:
          while (this.m_DataQueue.Count != 0 & (long) num2 < (long) this.m_Buffers && this.m_DataQueue.TryDequeue(out result))
          {
            streamWriter.Write(this.BuildLines(result));
            checked { ++num2; }
            num1 = checked ((int) ((long) num1 + (long) this.m_Captures));
            if ((long) num1 >= (long) this.m_LinesPerFile)
            {
              streamWriter.Close();
              checked { ++FileNumber; }
              num1 = 0;
              if ((long) num2 < (long) this.m_Buffers)
                streamWriter = this.CreateFile(FileNumber);
            }
          }
          flag = false;
          streamWriter.Close();
          break;
        case FileCommand.WriteCompleteFile:
          streamWriter = this.CreateFile(FileNumber);
          for (num1 = 0; this.m_DataQueue.Count != 0 & (long) num1 < (long) this.m_LinesPerFile && this.m_DataQueue.TryDequeue(out result); num1 = checked ((int) ((long) num1 + (long) this.m_Captures)))
          {
            streamWriter.Write(this.BuildLines(result));
            checked { ++num2; }
          }
          streamWriter.Close();
          checked { ++FileNumber; }
          break;
      }
    }
    this.m_StreamRunning = false;
    if (this.m_DutStreamRunning)
      return;
    // ISSUE: reference to a compiler-generated field
    Logger.RunAsyncCompletedEventHandler asyncCompletedEvent = this.RunAsyncCompletedEvent;
    if (asyncCompletedEvent != null)
      asyncCompletedEvent();
  }

  private StreamWriter CreateFile(int FileNumber)
  {
    StreamWriter file = new StreamWriter(this.GetFileName(FileNumber), true);
    file.WriteLine(this.BuildHeader());
    return file;
  }

  private string BuildHeader()
  {
    string str = "";
    try
    {
      foreach (RegClass reg in this.m_RegList)
        str = str + reg.Label + this.m_DataSeparator;
    }
    finally
    {
      IEnumerator<RegClass> enumerator;
      enumerator?.Dispose();
    }
    return str.Substring(0, checked (str.Length - this.m_DataSeparator.Length));
  }

  private string BuildLines(ushort[] Buffer)
  {
    StringBuilder stringBuilder = new StringBuilder(checked (((IEnumerable<ushort>) Buffer).Count<ushort>() * 12));
    int index = 0;
    int num1 = checked ((int) ((long) this.m_Captures - 1L));
    int num2 = 0;
    while (num2 <= num1)
    {
      try
      {
        foreach (RegClass reg in this.m_RegList)
        {
          switch (reg.NumBytes)
          {
            case 1:
              if (reg.IsTwosComp)
                stringBuilder.Append(SignedConverter.ToSignedByte(Buffer[index], (ulong) reg.Address % 2UL > 0UL).ToString());
              else
                stringBuilder.Append(SignedConverter.ToSignedByte(Buffer[index], (ulong) reg.Address % 2UL > 0UL).ToString());
              stringBuilder.Append(this.m_DataSeparator);
              checked { ++index; }
              break;
            case 2:
              if (reg.IsTwosComp)
                stringBuilder.Append(SignedConverter.ToSignedShort(Buffer[index]).ToString());
              else
                stringBuilder.Append(Buffer[index].ToString());
              stringBuilder.Append(this.m_DataSeparator);
              checked { ++index; }
              break;
            case 4:
              ushort lower;
              ushort upper;
              if (this.LowerWordFirst)
              {
                lower = Buffer[index];
                upper = Buffer[checked (index + 1)];
              }
              else
              {
                upper = Buffer[index];
                lower = Buffer[checked (index + 1)];
              }
              if (reg.IsTwosComp)
                stringBuilder.Append(SignedConverter.ToSignedInt(upper, lower).ToString());
              else
                stringBuilder.Append(SignedConverter.ToUnsignedInt(upper, lower).ToString());
              stringBuilder.Append(this.m_DataSeparator);
              checked { index += 2; }
              break;
            default:
              throw new Exception("ERROR: Register should only be 1, 2, or 4 bytes");
          }
        }
      }
      finally
      {
        IEnumerator<RegClass> enumerator;
        enumerator?.Dispose();
      }
      stringBuilder.Remove(checked (stringBuilder.Length - this.m_DataSeparator.Length), this.m_DataSeparator.Length);
      stringBuilder.AppendLine();
      checked { ++num2; }
    }
    return stringBuilder.ToString();
  }

  private string GetFileName(int FileNumber)
  {
    return this.m_FilePath + $"{this.m_FileBaseName}_{FileNumber.ToString("D4")}.{this.m_FileExtension}";
  }

  private void LoadDefaultValues()
  {
    this.ResetStateVariables();
    this.m_DataSeparator = ",";
    this.m_BufferTimeoutSeconds = 10U;
    this.m_FileExtension = "csv";
    this.m_Captures = 1U;
    this.m_lowerFirst = true;
    this.m_EventTimer = new System.Timers.Timer((double) checked ((long) this.m_BufferTimeoutSeconds * 1000L));
    this.m_EventTimer.Enabled = false;
    this.m_EventTimer.Elapsed += (ElapsedEventHandler) ([DebuggerHidden, SpecialName] (a0, a1) => this.TimeoutCallback());
  }

  private void ResetStateVariables()
  {
    this.m_BufferCount = 0;
    this.m_BuffersSinceWrite = 0;
    this.m_DataQueue = new ConcurrentQueue<ushort[]>();
    this.m_CommandQueue = new BlockingCollection<FileCommand>();
    this.m_TimeoutOccurred = false;
    this.m_Progress = 0;
    this.m_StreamFinalized = false;
    this.m_DutStreamRunning = false;
  }

  public delegate void ProgressChangedEventHandler(ProgressChangedEventArgs e);

  public delegate void RunAsyncCompletedEventHandler();
}
