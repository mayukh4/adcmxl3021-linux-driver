// Decompiled with JetBrains decompiler
// Type: adisInterface.DataCaptureClass
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

#nullable disable
namespace adisInterface;

public class DataCaptureClass
{
  private const double StreamPacketTime = 0.3;
  private TextWriter writer;
  private TimeSpan duration;
  private Stopwatch watch;
  private bool workerReportsProgress;
  private bool workerSupportsCancellation;
  private IDutInterface _dut;
  private bool _cancelled;
  private BackgroundWorker _bgWorker;
  private uint _samples;
  private double _sampleRate;
  private IList<RegClass> _regList;
  private bool _scaleData;
  private string _FileName;
  private bool _fileHeader;
  private string delim;
  private bool _UseEvalLabel;

  public DataCaptureClass()
  {
    this.watch = new Stopwatch();
    this.workerReportsProgress = false;
    this.workerSupportsCancellation = false;
    this._cancelled = false;
    this._bgWorker = (BackgroundWorker) null;
    this._samples = 0U;
    this._regList = (IList<RegClass>) null;
    this._scaleData = false;
    this._FileName = "";
    this._fileHeader = true;
    this.delim = ",";
  }

  public IDutInterface Dut
  {
    get => this._dut;
    set => this._dut = value;
  }

  /// <summary>Returns true if the previous capture operation was cancelled.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public bool Cancelled
  {
    get => this._cancelled;
    private set => this._cancelled = value;
  }

  /// <summary>
  /// If set to a non-null value, Data capture will report progress check for cancellation.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public BackgroundWorker bgWorker
  {
    get => this._bgWorker;
    set
    {
      this._bgWorker = value;
      if (this._bgWorker != null)
      {
        this.workerReportsProgress = this._bgWorker.WorkerReportsProgress;
        this.workerSupportsCancellation = this._bgWorker.WorkerSupportsCancellation;
      }
      else
      {
        this.workerReportsProgress = false;
        this.workerSupportsCancellation = false;
      }
    }
  }

  /// <summary>Number of samples for capture operations.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint Samples
  {
    get => this._samples;
    set => this._samples = value;
  }

  /// <summary>Sample rate (data ready rate) in Hz, for data capture.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks>Used to calculate streaming packet size.</remarks>
  public double SampleRate
  {
    get => this._sampleRate;
    set => this._sampleRate = value;
  }

  /// <summary>List of registers to be read</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public IList<RegClass> RegList
  {
    get => this._regList;
    set => this._regList = value;
  }

  /// <summary>Scaled Data will be stored if True.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public bool ScaleData
  {
    get => this._scaleData;
    set => this._scaleData = value;
  }

  /// <summary>File Name that data file should be stored in</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public string FileName
  {
    get => this._FileName;
    set => this._FileName = value;
  }

  /// <summary>Set to true if file header is to be included</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public bool FileHeader
  {
    get => this._fileHeader;
    set => this._fileHeader = value;
  }

  /// <summary>Field Delimiter for file writes.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks>Defaults to comma for .csv files.</remarks>
  public string delimiter
  {
    get => this.delim;
    set => this.delim = value;
  }

  /// <summary>Set to true if we want to use EvalLabels in file header instead of Labels.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public bool UseEvalLabel
  {
    get => this._UseEvalLabel;
    set => this._UseEvalLabel = value;
  }

  /// <summary>Capture using most appropriate capture type.</summary>
  /// <remarks></remarks>
  public void Capture()
  {
    if (this.SampleRate < 4.0)
      this.CaptureByLine();
    else
      this.CaptureStream();
  }

  /// <summary>Captures by reading one line of data at a time.</summary>
  /// <remarks>Appropriate when sampling slowly enough to allow USB round trip delay.</remarks>
  public void CaptureByLine()
  {
    double[] numArray = new double[checked ((int) this.Samples * this.RegList.Count - 1 + 1)];
    if (this.SampleRate > 5.0)
      throw new Exception("Data rate must be <5 Hz to use Line By Line Data Capture.");
    this.Cancelled = false;
    if (!this.OpenFile())
      return;
    if (this.FileHeader)
      this.WriteHeader();
    this.watch.Reset();
    this.watch.Start();
    uint NumRows = 0;
    while (NumRows < this.Samples & !this.Cancelled)
    {
      uint[] datList = this.Dut.ReadUnsigned((IEnumerable<RegClass>) this.RegList);
      double[] sourceArray;
      if (this.ScaleData)
      {
        sourceArray = this.Dut.ScaleRegData((IEnumerable<RegClass>) this.RegList, (IEnumerable<uint>) datList);
      }
      else
      {
        uint[] source = datList;
        Func<uint, double> selector;
        // ISSUE: reference to a compiler-generated field
        if (DataCaptureClass._Closure\u0024__.\u0024I52\u002D0 != null)
        {
          // ISSUE: reference to a compiler-generated field
          selector = DataCaptureClass._Closure\u0024__.\u0024I52\u002D0;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          DataCaptureClass._Closure\u0024__.\u0024I52\u002D0 = selector = (Func<uint, double>) ([SpecialName] (d) => (double) d);
        }
        sourceArray = ((IEnumerable<uint>) source).Select<uint, double>(selector).ToArray<double>();
      }
      Array.Copy((Array) sourceArray, 0L, (Array) numArray, checked ((long) NumRows * (long) this.RegList.Count), (long) this.RegList.Count);
      checked { ++NumRows; }
      if (this.workerReportsProgress)
        this.bgWorker.ReportProgress(checked ((int) Math.Round(unchecked ((double) NumRows / (double) this.Samples * 100.0))));
      if (this.workerSupportsCancellation && this.bgWorker.CancellationPending)
        this.Cancelled = true;
    }
    this.duration = this.watch.Elapsed;
    this.WriteDataLines((IList<double>) numArray, NumRows);
    if (this.FileHeader)
      this.WriteFooter();
    this.writer.Close();
  }

  public void CaptureStream()
  {
    double[] numArray = new double[checked ((int) this.Samples * this.RegList.Count - 1 + 1)];
    this.Cancelled = false;
    uint numCaptures = this.SampleRate >= 4.0 ? checked ((uint) Math.Round(unchecked (0.3 * this.SampleRate))) : throw new Exception("Data rate must be >4 Hz to use Streaming Data Capture.");
    int num1;
    if (numCaptures > this.Samples)
    {
      numCaptures = this.Samples;
      num1 = 1;
    }
    else
    {
      int num2 = checked ((int) Math.Round(Math.Ceiling(unchecked ((double) this.Samples / (double) numCaptures))));
      if (checked ((long) num2 * (long) numCaptures) < (long) this.Samples)
        num1 = checked (num2 + 1);
    }
    if (!this.OpenFile())
      return;
    if (this.FileHeader)
      this.WriteHeader();
    this.watch.Reset();
    this.watch.Start();
    this.Dut.StartStream((IEnumerable<RegClass>) this.RegList, numCaptures);
    uint NumRows = 0;
    int destinationIndex = 0;
    while (NumRows < this.Samples & !this.Cancelled)
    {
      uint[] u32 = this.Dut.ConvertReadDataToU32((IEnumerable<RegClass>) this.RegList, (IEnumerable<ushort>) Utils.CopyArray((Array) this.Dut.GetStreamDataPacketU16(), (Array) new ushort[checked ((int) ((long) numCaptures * (long) this.RegList.SpiTransferCount() - 1L) + 1)]));
      double[] sourceArray;
      if (this.ScaleData)
      {
        sourceArray = this.Dut.ScaleRegData((IEnumerable<RegClass>) this.RegList, (IEnumerable<uint>) u32);
      }
      else
      {
        uint[] source = u32;
        Func<uint, double> selector;
        // ISSUE: reference to a compiler-generated field
        if (DataCaptureClass._Closure\u0024__.\u0024I53\u002D0 != null)
        {
          // ISSUE: reference to a compiler-generated field
          selector = DataCaptureClass._Closure\u0024__.\u0024I53\u002D0;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          DataCaptureClass._Closure\u0024__.\u0024I53\u002D0 = selector = (Func<uint, double>) ([SpecialName] (d) => (double) d);
        }
        sourceArray = ((IEnumerable<uint>) source).Select<uint, double>(selector).ToArray<double>();
      }
      Array.Copy((Array) sourceArray, 0, (Array) numArray, destinationIndex, sourceArray.Length);
      checked { NumRows += numCaptures; }
      checked { destinationIndex += sourceArray.Length; }
      if (checked (NumRows + numCaptures) > this.Samples)
        numCaptures = checked (this.Samples - NumRows);
      if (this.workerReportsProgress)
        this.bgWorker.ReportProgress(checked ((int) Math.Round(unchecked ((double) NumRows / (double) this.Samples * 100.0))));
      if (this.workerSupportsCancellation && this.bgWorker.CancellationPending)
        this.Cancelled = true;
    }
    this.duration = this.watch.Elapsed;
    this.Dut.StopStream();
    this.WriteDataLines((IList<double>) numArray, NumRows);
    if (this.FileHeader)
      this.WriteFooter();
    this.writer.Close();
  }

  private void WriteHeader()
  {
    StringBuilder stringBuilder = new StringBuilder();
    this.writer.WriteLine($"File Name{this.delim}{this.FileName}");
    this.writer.WriteLine($"Start Date{this.delim}{DateAndTime.Today.ToString("dd MMM yyyy")}");
    this.writer.WriteLine($"Start Time{this.delim}{DateAndTime.TimeOfDay.ToString("HH:mm:ss")}");
    this.writer.WriteLine();
    stringBuilder.Append("Sample #");
    try
    {
      foreach (RegClass reg in (IEnumerable<RegClass>) this.RegList)
      {
        stringBuilder.Append(this.delim);
        if (this.UseEvalLabel)
          stringBuilder.Append(reg.EvalLabel);
        else
          stringBuilder.Append(reg.Label);
      }
    }
    finally
    {
      IEnumerator<RegClass> enumerator;
      enumerator?.Dispose();
    }
    this.writer.WriteLine(stringBuilder.ToString());
  }

  private void WriteDataLines(IList<double> Data, uint NumRows)
  {
    int num1 = checked ((int) NumRows - 1);
    int num2 = 0;
    while (num2 <= num1)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(checked (num2 + 1).ToString());
      int num3 = checked (this.RegList.Count - 1);
      int num4 = 0;
      while (num4 <= num3)
      {
        stringBuilder.AppendFormat("{0}{1:0.0000}", (object) this.delim, (object) Data[checked (num2 * this.RegList.Count + num4)]);
        checked { ++num4; }
      }
      this.writer.WriteLine(stringBuilder.ToString());
      checked { ++num2; }
    }
  }

  private void WriteFooter()
  {
    this.writer.WriteLine();
    this.writer.WriteLine($"Stop Date{this.delim}{DateAndTime.Today.ToString("dd MMM yyyy")}");
    this.writer.WriteLine($"Stop Time{this.delim}{DateAndTime.TimeOfDay.ToString("HH:mm:ss")}");
    this.writer.Write($"Duration{this.delim}{this.duration.TotalSeconds.ToString("F3")}");
    this.writer.WriteLine(this.delim + "seconds (including USB transfer overhead)");
  }

  private bool OpenFile()
  {
    bool flag = true;
    this.writer = (TextWriter) new StreamWriter(this.FileName);
    return flag;
  }
}
