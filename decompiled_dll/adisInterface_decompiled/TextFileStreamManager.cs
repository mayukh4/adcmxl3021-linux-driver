// Decompiled with JetBrains decompiler
// Type: adisInterface.TextFileStreamManager
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace adisInterface;

/// <summary>This class manages the streaming process from a device to a text file.</summary>
/// <remarks></remarks>
public class TextFileStreamManager
{
  private RunAsyncCompletedEventArgs RunAsyncReturnArgs;
  private Stopwatch captureStopwatch;
  private int lineCount;
  private int fileCount;
  private bool m_CancellationPending;
  private uint m_Buffers;
  private uint m_BuffersPerWrite;
  private int m_BufferTimeout;
  private uint m_Captures;
  private string m_DataFormatString;
  private string m_DataSeperator;
  private IBufferedStreamProducer m_DutInterface;
  private string m_FileBaseName;
  private string m_FileExtension;
  private uint m_FileMaxDataRows;
  private string m_FilePath;
  private Func<string, string> m_FooterGeneratorFunc;
  private Func<string, string> m_HeaderGeneratorFunc;
  private bool m_IncludeSampleNumberColumn;
  private bool m_IsBusy;
  private IEnumerable<RegClass> m_RegList;
  private bool m_ScaleData;

  private virtual BackgroundWorker EnqueueWorker
  {
    get => this._EnqueueWorker;
    [MethodImpl(MethodImplOptions.Synchronized)] set
    {
      DoWorkEventHandler workEventHandler = new DoWorkEventHandler(this.EnqueueWorker_DoWork);
      ProgressChangedEventHandler changedEventHandler = new ProgressChangedEventHandler(this.EnqueueWorker_ProgressChanged);
      RunWorkerCompletedEventHandler completedEventHandler = new RunWorkerCompletedEventHandler(this.EnqueueWorker_RunWorkCompleted);
      BackgroundWorker enqueueWorker1 = this._EnqueueWorker;
      if (enqueueWorker1 != null)
      {
        enqueueWorker1.DoWork -= workEventHandler;
        enqueueWorker1.ProgressChanged -= changedEventHandler;
        enqueueWorker1.RunWorkerCompleted -= completedEventHandler;
      }
      this._EnqueueWorker = value;
      BackgroundWorker enqueueWorker2 = this._EnqueueWorker;
      if (enqueueWorker2 == null)
        return;
      enqueueWorker2.DoWork += workEventHandler;
      enqueueWorker2.ProgressChanged += changedEventHandler;
      enqueueWorker2.RunWorkerCompleted += completedEventHandler;
    }
  }

  private virtual BackgroundWorker DequeueWorker
  {
    get => this._DequeueWorker;
    [MethodImpl(MethodImplOptions.Synchronized)] set
    {
      DoWorkEventHandler workEventHandler = new DoWorkEventHandler(this.DequeueWorker_DoWork);
      RunWorkerCompletedEventHandler completedEventHandler = new RunWorkerCompletedEventHandler(this.DequeueWorker_RunWorkCompleted);
      BackgroundWorker dequeueWorker1 = this._DequeueWorker;
      if (dequeueWorker1 != null)
      {
        dequeueWorker1.DoWork -= workEventHandler;
        dequeueWorker1.RunWorkerCompleted -= completedEventHandler;
      }
      this._DequeueWorker = value;
      BackgroundWorker dequeueWorker2 = this._DequeueWorker;
      if (dequeueWorker2 == null)
        return;
      dequeueWorker2.DoWork += workEventHandler;
      dequeueWorker2.RunWorkerCompleted += completedEventHandler;
    }
  }

  /// <summary>
  /// Event will be raised to report progress percentage each time a packet is added to the queue.
  /// </summary>
  /// <remarks></remarks>
  public event ProgressChangedEventHandler ProgressChanged;

  /// <summary>
  /// Event will be raised when the streaming operation is completed, including canceeled operations and operation with errors.
  /// </summary>
  /// <remarks></remarks>
  public event RunAsyncCompletedEventHandler RunAsyncCompleted;

  /// <summary>
  /// Indicates that stream cancellation has been requested for thie StreamManager.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public bool CancellationPending
  {
    get => this.m_CancellationPending;
    set => this.m_CancellationPending = value;
  }

  /// <summary>The number of stream buffers to be transferred in the streaming operation.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint Buffers
  {
    get => this.m_Buffers;
    set => this.m_Buffers = value;
  }

  /// <summary>The number of stream buffers to be written to the file in one operation.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint BuffersPerWrite
  {
    get => this.m_BuffersPerWrite;
    set => this.m_BuffersPerWrite = value;
  }

  /// <summary>The maximum time system will wait for each streaming data packet.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public int BufferTimeout
  {
    get => this.m_BufferTimeout;
    set => this.m_BufferTimeout = value;
  }

  /// <summary>The number of captures of the reg list to be inclded in each stream buffer.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint Captures
  {
    get => this.m_Captures;
    set => this.m_Captures = value;
  }

  /// <summary>
  /// The data formatting stream to be used for the generated data files. (Defaults to "".)
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public string DataFormatString
  {
    get => this.m_DataFormatString;
    set => this.m_DataFormatString = value;
  }

  /// <summary>
  /// The field delimiter for the generated data files. (Defaults to comma deleimted.)
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public string DataSeperator
  {
    get => this.m_DataSeperator;
    set => this.m_DataSeperator = value;
  }

  /// <summary>The instance of DutInterface associated with the device to be used.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public IBufferedStreamProducer DutInterface
  {
    get => this.m_DutInterface;
    set => this.m_DutInterface = value;
  }

  /// <summary>
  /// Returns the amount of time that elapsed during a capture operation.  May be called during a capture or after a capture is complete.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public TimeSpan ElapsedCaptureTime => this.captureStopwatch.Elapsed;

  /// <summary>The base name (not includeng path) of the data files that will be written.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public string FileBaseName
  {
    get => this.m_FileBaseName;
    set => this.m_FileBaseName = value;
  }

  /// <summary>
  /// The file extension to be used for the generated data files (Defaults to "csv")
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public string FileExtension
  {
    get => this.m_FileExtension;
    set
    {
      if (value == null)
        this.m_FileExtension = (string) null;
      else
        this.m_FileExtension = value.TrimStart('.');
    }
  }

  /// <summary>The maximum number of data rows to be included in each file.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint FileMaxDataRows
  {
    get => this.m_FileMaxDataRows;
    set => this.m_FileMaxDataRows = value;
  }

  /// <summary>The directory path (not including file name) for the generated data files</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public string FilePath
  {
    get => this.m_FilePath;
    set => this.m_FilePath = value;
  }

  /// <summary>Externally supplied function to produce file footers.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks>No footer will be used if this property is null.</remarks>
  public Func<string, string> FooterGeneratorFunc
  {
    get => this.m_FooterGeneratorFunc;
    set => this.m_FooterGeneratorFunc = value;
  }

  /// <summary>Externally supplied function to produce non-default file headers.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks>Default header format will be used if this property is null.</remarks>
  public Func<string, string> HeaderGeneratorFunc
  {
    get => this.m_HeaderGeneratorFunc;
    set => this.m_HeaderGeneratorFunc = value;
  }

  /// <summary>
  /// Indicates wether text files will contain a sample number column on the left.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public bool IncludeSampleNumberColumn
  {
    get => this.m_IncludeSampleNumberColumn;
    set => this.m_IncludeSampleNumberColumn = value;
  }

  /// <summary>Indicates whether a streaming  operation is currently in progress.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public bool IsBusy
  {
    get => this.m_IsBusy;
    private set => this.m_IsBusy = value;
  }

  /// <summary>The registers that are to be read for each capture.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public IEnumerable<RegClass> RegList
  {
    get => this.m_RegList;
    set => this.m_RegList = value;
  }

  /// <summary>Indicates whether data should be scaled.</summary>
  /// <remarks></remarks>
  public bool ScaleData
  {
    get => this.m_ScaleData;
    set => this.m_ScaleData = value;
  }

  /// <summary>Creates a new instance of the TextFileStreamManager object.</summary>
  /// <remarks></remarks>
  public TextFileStreamManager()
  {
    this.EnqueueWorker = new BackgroundWorker()
    {
      WorkerSupportsCancellation = true,
      WorkerReportsProgress = true
    };
    this.DequeueWorker = new BackgroundWorker()
    {
      WorkerSupportsCancellation = true,
      WorkerReportsProgress = true
    };
    this.captureStopwatch = new Stopwatch();
    this.IsBusy = false;
    this.CancellationPending = false;
    this.Buffers = 1U;
    this.BuffersPerWrite = 20U;
    this.BufferTimeout = 5;
    this.DataFormatString = "";
    this.DataSeperator = ",";
    this.FileMaxDataRows = 1000000U;
    this.FileExtension = "csv";
    this.IncludeSampleNumberColumn = false;
    this.ScaleData = true;
  }

  /// <summary>Requests cancellation of current streaming operation.</summary>
  /// <remarks></remarks>
  public void CancelAsync()
  {
    if (!(this.IsBusy & !this.CancellationPending))
      return;
    this.CancellationPending = true;
    this.EnqueueWorker.CancelAsync();
  }

  /// <summary>Creates a new data file, begins a streaming operation.</summary>
  /// <remarks></remarks>
  public void RunAsync()
  {
    this.IsBusy = !this.IsBusy ? true : throw new InvalidOperationException("StreamManager is already busy.");
    try
    {
      this.ValidateProperties();
      this.CreateNewFile(0);
      this.InitilizeForRun();
    }
    catch (Exception ex)
    {
      ProjectData.SetProjectError(ex);
      this.IsBusy = false;
      throw;
    }
    this.EnqueueWorker.RunWorkerAsync();
    this.DequeueWorker.RunWorkerAsync();
  }

  private void ValidateProperties()
  {
    if (this.DutInterface == null)
      throw new NullReferenceException("DutInterface must not be null.");
    if (this.Buffers == 0U)
      throw new ArgumentException("Buffers must be non-zero.");
    if (this.BuffersPerWrite == 0U)
      throw new ArgumentException("BuffersPerWrite must be non-zero.");
    if (this.Captures == 0U)
      throw new ArgumentException("Captures must be non-zero.");
    if (this.FileMaxDataRows == 0U)
      throw new ArgumentException("FileMaxDataRows must be non-zero.");
    if (this.RegList == null)
      throw new NullReferenceException("RegList must not be null.");
    if (this.RegList.Count<RegClass>() == 0)
      throw new ArgumentException("Reglist must contain at least one element.");
    if (this.FileBaseName == null)
      throw new NullReferenceException("FileBaseName must not be null.");
    if (this.DataSeperator == null)
      throw new NullReferenceException("FileDelimiter must not be null.");
    if (this.FileExtension == null)
      throw new NullReferenceException("FileExtension must not be null.");
    if (this.FilePath == null)
      throw new NullReferenceException("FilePath must not be null.");
    if (this.DataSeperator.Count<char>() != 1)
      throw new ArgumentException("DataSeperator must be a single character.");
  }

  /// <summary>Creates a new file, with header.</summary>
  /// <param name="n"></param>
  /// <remarks></remarks>
  private void CreateNewFile(int n)
  {
    string fileName = this.GetFileName(n);
    StreamWriter writer = new StreamWriter(fileName, false);
    this.WriteHeaderText((TextWriter) writer, fileName);
    writer.Close();
  }

  /// <summary>Initialize variables to run.</summary>
  /// <remarks></remarks>
  private void InitilizeForRun()
  {
    this.fileCount = 0;
    this.lineCount = 0;
    this.captureStopwatch.Reset();
    this.RunAsyncReturnArgs = new RunAsyncCompletedEventArgs();
  }

  /// <summary>Writes custom or default header text to TextWriter.</summary>
  /// <param name="writer"></param>
  /// <param name="fileName"></param>
  /// <remarks></remarks>
  private void WriteHeaderText(TextWriter writer, string fileName)
  {
    string str;
    if (this.HeaderGeneratorFunc != null)
    {
      str = this.HeaderGeneratorFunc(fileName);
    }
    else
    {
      List<string> stringList1 = new List<string>();
      if (this.IncludeSampleNumberColumn)
        stringList1.Add("Sample #");
      List<string> stringList2 = stringList1;
      IEnumerable<RegClass> regList = this.RegList;
      Func<RegClass, string> selector;
      // ISSUE: reference to a compiler-generated field
      if (TextFileStreamManager._Closure\u0024__.\u0024I100\u002D0 != null)
      {
        // ISSUE: reference to a compiler-generated field
        selector = TextFileStreamManager._Closure\u0024__.\u0024I100\u002D0;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        TextFileStreamManager._Closure\u0024__.\u0024I100\u002D0 = selector = (Func<RegClass, string>) ([SpecialName] (r) => r.EvalLabel);
      }
      IEnumerable<string> collection = regList.Select<RegClass, string>(selector);
      stringList2.AddRange(collection);
      str = string.Join(this.DataSeperator, stringList1.ToArray());
    }
    if (string.IsNullOrEmpty(str))
      return;
    writer.WriteLine(str);
  }

  /// <summary>Writes custom footer text to TextWriter.</summary>
  /// <param name="writer"></param>
  /// <param name="fileName"></param>
  /// <remarks></remarks>
  private void WriteFooterText(TextWriter writer, string fileName)
  {
    if (this.FooterGeneratorFunc == null)
      return;
    string str = this.FooterGeneratorFunc(fileName);
    if (!string.IsNullOrEmpty(str))
      writer.WriteLine(str);
  }

  private string GetFileName(int n)
  {
    return Path.Combine(this.FilePath, $"{this.FileBaseName}_{n:D4}.{this.FileExtension}");
  }

  /// <summary>Writes data to a data file.</summary>
  /// <param name="dat">Data to write.</param>
  /// <param name="finalize">True if we should write footer to this file.</param>
  /// <remarks></remarks>
  private void WriteDequeuedData(IEnumerable<uint> dat, bool finalize)
  {
    string fileName = this.GetFileName(this.fileCount);
    StreamWriter writer = new StreamWriter(fileName, true);
    if (dat.Count<uint>() > 0)
    {
      List<string> stringList = new List<string>();
      double[] numArray;
      if (this.ScaleData)
      {
        numArray = this.DutInterface.ScaleRegData(this.RegList, dat);
      }
      else
      {
        IEnumerable<uint> source = dat;
        Func<uint, double> selector;
        // ISSUE: reference to a compiler-generated field
        if (TextFileStreamManager._Closure\u0024__.\u0024I103\u002D0 != null)
        {
          // ISSUE: reference to a compiler-generated field
          selector = TextFileStreamManager._Closure\u0024__.\u0024I103\u002D0;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          TextFileStreamManager._Closure\u0024__.\u0024I103\u002D0 = selector = (Func<uint, double>) ([SpecialName] (d) => (double) d);
        }
        numArray = source.Select<uint, double>(selector).ToArray<double>();
      }
      int num1 = checked (unchecked (dat.Count<uint>() / this.RegList.Count<RegClass>()) - 1);
      int num2 = 0;
      while (num2 <= num1)
      {
        if ((long) this.lineCount % (long) this.FileMaxDataRows == 0L && this.lineCount > 0)
        {
          this.WriteFooterText((TextWriter) writer, fileName);
          writer.Close();
          // ISSUE: variable of a reference type
          int& local;
          // ISSUE: explicit reference operation
          int num3 = checked (^(local = ref this.fileCount) + 1);
          local = num3;
          this.CreateNewFile(this.fileCount);
          fileName = this.GetFileName(this.fileCount);
          writer = new StreamWriter(fileName, true);
        }
        stringList.Clear();
        if (this.IncludeSampleNumberColumn)
          stringList.Add(this.lineCount.ToString());
        int num4 = checked (this.RegList.Count<RegClass>() - 1);
        int num5 = 0;
        while (num5 <= num4)
        {
          stringList.Add(numArray[checked (num2 * this.RegList.Count<RegClass>() + num5)].ToString(this.DataFormatString));
          checked { ++num5; }
        }
        writer.WriteLine(string.Join(this.DataSeperator, stringList.ToArray()));
        // ISSUE: variable of a reference type
        int& local1;
        // ISSUE: explicit reference operation
        int num6 = checked (^(local1 = ref this.lineCount) + 1);
        local1 = num6;
        checked { ++num2; }
      }
    }
    if (finalize)
      this.WriteFooterText((TextWriter) writer, fileName);
    writer.Close();
  }

  private void EnqueueWorker_DoWork(object sender, DoWorkEventArgs e)
  {
    this.captureStopwatch.Start();
    this.DutInterface.StartBufferedStream(this.RegList, this.Captures, this.Buffers, this.BufferTimeout, this.EnqueueWorker);
    this.captureStopwatch.Stop();
    e.Cancel = this.EnqueueWorker.CancellationPending;
  }

  private void EnqueueWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
  {
    // ISSUE: reference to a compiler-generated field
    ProgressChangedEventHandler progressChangedEvent = this.ProgressChangedEvent;
    if (progressChangedEvent == null)
      return;
    progressChangedEvent((object) this, e);
  }

  private void EnqueueWorker_RunWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    this.RunAsyncReturnArgs.EnqueueError = e.Error;
    this.DequeueWorker.CancelAsync();
  }

  private void DequeueWorker_DoWork(object sender, DoWorkEventArgs e)
  {
    List<uint> dat = new List<uint>();
    bool flag = false;
    do
    {
      bool cancellationPending = this.DequeueWorker.CancellationPending;
      ushort[] streamDataPacket = this.DutInterface.GetBufferedStreamDataPacket();
      if (streamDataPacket == null)
      {
        if (cancellationPending)
          flag = true;
      }
      else
      {
        dat.AddRange((IEnumerable<uint>) this.DutInterface.ConvertReadDataToU32(this.RegList, (IEnumerable<ushort>) streamDataPacket));
        int num;
        checked { ++num; }
        if ((long) num == (long) this.BuffersPerWrite)
        {
          this.WriteDequeuedData((IEnumerable<uint>) dat, false);
          num = 0;
          dat.Clear();
        }
      }
    }
    while (!flag);
    this.WriteDequeuedData((IEnumerable<uint>) dat, true);
  }

  private void DequeueWorker_RunWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
  {
    this.RunAsyncReturnArgs.DequeueError = e.Error;
    this.RunAsyncReturnArgs.Cancelled = this.CancellationPending;
    this.CancellationPending = false;
    this.IsBusy = false;
    // ISSUE: reference to a compiler-generated field
    RunAsyncCompletedEventHandler asyncCompletedEvent = this.RunAsyncCompletedEvent;
    if (asyncCompletedEvent == null)
      return;
    asyncCompletedEvent((object) this, this.RunAsyncReturnArgs);
  }
}
