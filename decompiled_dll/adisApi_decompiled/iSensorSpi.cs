// Decompiled with JetBrains decompiler
// Type: AdisApi.iSensorSpi
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using sdpApi1;
using sdpApi1.Generic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace AdisApi;

/// <summary>
/// Basic SPI configuration, reads, and writes using ADiS spi code on Blackfin.
/// </summary>
public class iSensorSpi : IRegInterface, ISpi32Interface
{
  private int sclkFrequency;
  private int stallCycles;
  private volatile bool Cancel = false;
  private uint spiStreamInit;
  private uint spiStreamStart;
  private uint spiTranfer;
  private static uint _sampleRate;
  private iSensorSpi.frameProtocolType _frameProtocol = iSensorSpi.frameProtocolType.adis16bit;
  /// <summary>
  /// Queue for data synchronization.  Stores packets as arrays of ushort data.
  /// </summary>
  private Queue<ushort[]> packetDataSyncQueue = new Queue<ushort[]>();
  /// <summary>Object used for locking the queue.</summary>
  /// <remarks>
  /// Need an instance variable availabe to multiple threads.
  /// </remarks>
  private static object packetDataSyncLock = new object();

  /// <summary>
  /// Set properties to values appropriate for an isensor SPI interface.
  /// </summary>
  private void setDefaultProperties()
  {
    this.LoopMode = false;
    this.WordSize = 16 /*0x10*/;
    this.StallCycles = 1;
    this.Cpha = true;
    this.Cpol = true;
    this.sclkFrequency = 1000000;
    this.BurstMode = (ushort) 0;
    this.StreamTimeoutSeconds = 10;
    this.Read0Write1Flip = false;
    this.CancelStream = false;
    this.TriggerDrRegCommand = (ushort) 0;
    this.sampleRate = 0U;
  }

  /// <summary>Aborts a streaming operation.</summary>
  /// <remarks>
  /// Should NOT require a bool parameter.  See additional note at CancelStream property.
  /// </remarks>
  /// <param name="cancel"></param>
  [Obsolete("This overload is obsolete, use AbortStream().", false)]
  public void AbortStream(bool cancel) => this.CancelStream = cancel;

  /// <summary>Aborts a streaming operation.</summary>
  public void AbortStream() => this.CancelStream = true;

  /// <summary>
  /// Creates a new instance of iSenesorSpi, sets default values.
  /// </summary>
  public iSensorSpi()
  {
    this.AdisBase = new AdisBase();
    this.InitializeInstance();
  }

  /// <summary>
  /// Creates a new instance of iSenesorSpi using specified AdisBase, sets default values.
  /// </summary>
  public iSensorSpi(AdisBase AdisBase)
  {
    this.AdisBase = AdisBase;
    this.InitializeInstance();
  }

  /// <summary>
  /// Constructor implementation, common to all constructor overloads.
  /// </summary>
  private void InitializeInstance()
  {
    this.setDefaultProperties();
    this.PinFunctions = new PinFcns(this.AdisBase);
  }

  /// <summary>
  /// Gets the AdisBase object associated with thie Spi interface.
  /// </summary>
  public AdisBase AdisBase { get; private set; }

  /// <summary>
  /// If true, SPI pins will be tri-stated after each read/erite operation, except for variable length stream operations.
  /// </summary>
  public bool AutoPinDisable { get; set; }

  /// <summary>
  /// Switches burstMode on and off. Set burstMode to the number of burst read registers.
  /// </summary>
  public ushort BurstMode { get; set; }

  /// <summary>obsolete</summary>
  /// <remarks>
  /// This was left over from an earlier experiment.  Marked obsolete with error,
  /// </remarks>
  [Obsolete("This property is obsolete.", true)]
  public ushort[] readStreamDataReturn { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the Clock Phase is 0 or 1.
  /// </summary>
  public bool Cpha { get; set; }

  /// <summary>
  /// Gets or sets a value indicating whether the Clock Polarity is 0 or 1.
  /// </summary>
  public bool Cpol { get; set; }

  /// <summary>Gets or sets the data ready active bit</summary>
  public bool DrActive { get; set; }

  /// <summary>Gets or sets the data ready polarity.</summary>
  public bool DrPolarity { get; set; }

  /// <summary>
  /// If non-null, this function will be called to disable the pins instead of the default pin disable function.
  /// </summary>
  public Action PinDisableDelegate { get; set; }

  /// <summary>
  /// Returns the pinFunctions instance used by this instance of iSensorSpi.
  /// </summary>
  public PinFcns PinFunctions { get; private set; }

  /// <summary>
  /// Gets or sets the SPI Clock Frequency (in Hz) to use for this SPI interface.
  /// </summary>
  public int SclkFrequency
  {
    get => this.sclkFrequency;
    set
    {
      this.sclkFrequency = value >= 1 ? value : throw new ArgumentOutOfRangeException("SPI Sclk Frequency must be positive.");
    }
  }

  /// <summary>
  /// If False, Read is Active Low and  Write is Active High. If true, Read is Active High, Write is Active Low
  /// </summary>
  public bool Read0Write1Flip { get; set; }

  /// <summary>If set to true, will cause stream to be cancelled.</summary>
  /// <remarks>
  /// Consider a "CancelRequested" pattern similar to backGroundWorker class.
  /// Should be a "write only" method (see AbortStream above) to set this.
  /// Stream routines should set back to false when cancel is executed. (using private set.)
  /// </remarks>
  public bool CancelStream
  {
    get => this.Cancel;
    set => this.Cancel = value;
  }

  /// <summary>
  /// Gets or sets the number of stall cycles to use for this SPI interface.
  /// </summary>
  public int StallCycles
  {
    get => this.stallCycles;
    set
    {
      this.stallCycles = value >= 0 ? value : throw new ArgumentOutOfRangeException("Spi Stall Cycles must be >= 0.");
    }
  }

  /// <summary>
  /// Gets or sets burst timeout value: the max number of seconds to between burst packets.
  /// </summary>
  public int StreamTimeoutSeconds { get; set; }

  /// <summary>
  /// Gets the word size (number of SCLKS per read/write) for this SPI interface.
  /// </summary>
  public int WordSize { get; set; }

  /// <summary>
  /// Register Command to Trigger Data Ready. This was needed for ST LSM330DLC
  /// Data Ready Signal Stays High Once Data is Ready
  /// To Pull Data Ready Signal Low, one needs to read either Out_X_H_G, OUT_Y_H_G, and OUT_Z_H_G
  /// Hence, to do this, one sets TriggerDrRegCommand as 0xad (read command for Out_Z_H_G).
  /// Firmware will take the value 0xad and execute the read of Out_Z_H_G.
  /// </summary>
  public ushort TriggerDrRegCommand { get; set; }

  private bool LoopMode { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public PinObject SclkPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public PinObject MisoPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public PinObject MosiPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public PinObject CsPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public PinObject ReadyPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public PinObject ResetPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="map"></param>
  public void initializePins(PinMapObject map)
  {
    this.SclkPin = map.SclkPin;
    this.MisoPin = map.MisoPin;
    this.MosiPin = map.MosiPin;
    this.CsPin = map.CsPin;
    this.ReadyPin = map.ReadyPin;
    this.ResetPin = map.ResetPin;
  }

  /// <summary>
  /// Used to controll data rate when DR is Not active. iSensorActive must be set to false.
  /// </summary>
  public uint sampleRate
  {
    get => iSensorSpi._sampleRate;
    set
    {
      iSensorSpi._sampleRate = value;
      if (value == 0U)
      {
        this.spiStreamInit = 4160749572U /*0xF8000004*/;
        this.spiStreamStart = 4160749571U /*0xF8000003*/;
        this.spiTranfer = 4160749573U /*0xF8000005*/;
      }
      else
      {
        this.DrActive = false;
        this.spiStreamInit = 4160749593U;
        this.spiStreamStart = 4160749600U /*0xF8000020*/;
        this.spiTranfer = 4160749592U;
      }
    }
  }

  /// <summary>Sets the WordSize and Cpha per selected protocol.</summary>
  public iSensorSpi.frameProtocolType frameProtocol
  {
    get => this._frameProtocol;
    set
    {
      this._frameProtocol = value;
      if (value == iSensorSpi.frameProtocolType.xc32bit)
      {
        this.WordSize = 32 /*0x20*/;
        this.Cpha = true;
        this.Cpol = true;
      }
      if (value != iSensorSpi.frameProtocolType.adis16bit)
        return;
      this.WordSize = 16 /*0x10*/;
      this.Cpha = true;
      this.Cpol = true;
    }
  }

  public IPinObject DrPin
  {
    get => throw new NotImplementedException();
    set => throw new NotImplementedException();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numBursts"></param>
  /// <returns></returns>
  public int Loopback(IEnumerable<uint> addr, uint numBursts)
  {
    ushort[] commandWordArray = this.getCommandWordArray(addr);
    ushort[] readData;
    try
    {
      this.LoopMode = true;
      readData = this.ReadRegArray(addr, numBursts);
    }
    finally
    {
      this.LoopMode = false;
    }
    return iSensorSpi.countLoopbackErrors((IEnumerable<ushort>) commandWordArray, (IEnumerable<ushort>) readData);
  }

  /// <summary>
  /// Returns number of errors encountered in a loop back test.
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  public int FixedStreamLoopback(IEnumerable<uint> addr, uint numCaptures, uint numBuffers)
  {
    ushort[] commandWordArray = this.getCommandWordArray(addr);
    ushort[] readData;
    try
    {
      this.LoopMode = true;
      readData = this.ReadRegArrayStream(addr, numCaptures, numBuffers);
    }
    finally
    {
      this.LoopMode = false;
    }
    return iSensorSpi.countLoopbackErrors((IEnumerable<ushort>) commandWordArray, (IEnumerable<ushort>) readData);
  }

  /// <summary>
  /// Returns number of errors encountered in a loop back test.
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numBursts"></param>
  /// <param name="numPackets"></param>
  /// <returns></returns>
  public int VariableStreamLoopback(IEnumerable<uint> addr, uint numBursts, uint numPackets)
  {
    int num = addr.Count<uint>() * (int) numBursts;
    ushort[] numArray1 = new ushort[iSensorSpi.getStreamPacketSize(num)];
    ushort[] numArray2 = new ushort[(long) num * (long) numPackets];
    ushort[] commandWordArray = this.getCommandWordArray(addr);
    this.LoopMode = true;
    try
    {
      this.StartStream(addr, numBursts);
      for (int index = 0; (long) index < (long) numPackets; ++index)
        Array.Copy((Array) this.GetStreamDataPacketU16(), 0, (Array) numArray2, index * num, num);
      this.StopStream();
    }
    finally
    {
      this.LoopMode = false;
    }
    return iSensorSpi.countLoopbackErrors((IEnumerable<ushort>) commandWordArray, (IEnumerable<ushort>) numArray2);
  }

  /// <summary>
  /// Returns number of mismatches between a written and read arrays.
  /// </summary>
  /// <param name="writeData"></param>
  /// <param name="readData"></param>
  /// <returns></returns>
  /// <remarks>
  /// Note that writeData will be recycled if readData is longer than writeData.
  /// </remarks>
  private static int countLoopbackErrors(
    IEnumerable<ushort> writeData,
    IEnumerable<ushort> readData)
  {
    List<ushort> list1 = readData.ToList<ushort>();
    List<ushort> list2 = writeData.ToList<ushort>();
    int num = 0;
    for (int index = 0; index < readData.Count<ushort>(); ++index)
    {
      if ((int) list2[index % list2.Count<ushort>()] != (int) list1[index])
        ++num;
    }
    return num;
  }

  /// <summary>Dequeues and returns data from streaming read queue.</summary>
  /// <returns>Returns data if it is there, returns null if queue is null or empty.</returns>
  /// <remarks>
  /// </remarks>
  public ushort[] GetBufferedStreamDataPacket()
  {
    lock (iSensorSpi.packetDataSyncLock)
      return this.packetDataSyncQueue.Count > 0 ? this.packetDataSyncQueue.Dequeue() : (ushort[]) null;
  }

  /// <summary>Dequeues and returns data from streaming read queue.</summary>
  /// <returns>Returns data if it is there, returns null if queue is empty.</returns>
  /// <remarks>
  /// Consider renaming to GetStreamQueueData or something similar.
  /// Consider adding manual locking and using a generic Queueto prevent cast.
  /// </remarks>
  [Obsolete("This may be Obsoleted. Please use GetBufferedStreamData instead.")]
  public ushort[] ReadStreamQueue() => this.GetBufferedStreamDataPacket();

  /// <summary>Starts a buffered stream read, no timeout.</summary>
  /// <param name="addrData">Adresses to read/write.</param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  public void StartBufferedStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers)
  {
    this.StartBufferedStream(this.getCommandWordArray(addrData), numCaptures, numBuffers, new int?(), (BackgroundWorker) null);
  }

  /// <summary>Starts a buffered stream read, no timeout.</summary>
  /// <param name="addrData">Adresses to read/write.</param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <param name="worker">BackgroundWorker that routine can report progress to (may be null).</param>
  /// <remarks>
  /// This overload allows callers to call without timeoutSeconds.
  /// </remarks>
  public void StartBufferedStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers,
    BackgroundWorker worker)
  {
    this.StartBufferedStream(this.getCommandWordArray(addrData), numCaptures, numBuffers, new int?(), worker);
  }

  /// <summary>Starts a buffered stream read, no background worker.</summary>
  /// <param name="addrData">Adresses to read/write.</param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <param name="timeoutSeconds"></param>
  /// <remarks>
  /// This overload allows callers to call without backgroundWorker.
  /// </remarks>
  public void StartBufferedStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds)
  {
    this.StartBufferedStream(this.getCommandWordArray(addrData), numCaptures, numBuffers, new int?(timeoutSeconds), (BackgroundWorker) null);
  }

  /// <summary>
  /// Starts a buffered stream read, uses the background worker handle to update progress.
  /// </summary>
  /// <param name="addrData">Adresses to read/write.</param>
  /// <param name="numCaptures">Number of captures in each buffer for SDP transfer.</param>
  /// <param name="numBuffers">Number of buffers to Be captured.</param>
  /// <param name="timeoutSeconds">Timeout for transmission of one stream packet.</param>
  /// <param name="worker">BackgroundWorker that routine can report progress to (may be null).</param>
  /// <remarks>
  /// </remarks>
  public void StartBufferedStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    this.StartBufferedStream(this.getCommandWordArray(addrData), numCaptures, numBuffers, new int?(timeoutSeconds), worker);
  }

  /// <summary>
  /// Starts a buffered stream read, no background worker, no Timeout.
  /// </summary>
  /// <param name="addr">Adresses to read.</param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <remarks>
  /// This overload allows callers to call without timeoutSeconds or background worker.
  /// </remarks>
  public void StartBufferedStream(IEnumerable<uint> addr, uint numCaptures, uint numBuffers)
  {
    this.StartBufferedStream(this.getCommandWordArray(addr), numCaptures, numBuffers, new int?(), (BackgroundWorker) null);
  }

  /// <summary>Starts a buffered stream read, no timeout.</summary>
  /// <param name="addr">Adresses to read.</param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <param name="worker">BackgroundWorker that routine can report progress to (may be null).</param>
  /// <remarks>
  /// This overload allows callers to call without timeoutSeconds.
  /// </remarks>
  public void StartBufferedStream(
    IEnumerable<uint> addr,
    uint numCaptures,
    uint numBuffers,
    BackgroundWorker worker)
  {
    this.StartBufferedStream(this.getCommandWordArray(addr), numCaptures, numBuffers, new int?(), worker);
  }

  /// <summary>Starts a buffered stream read, no background worker.</summary>
  /// <param name="addr">Adresses to read.</param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <param name="timeoutSeconds"></param>
  /// <remarks>
  /// This overload allows callers to call without backgroundWorker.
  /// </remarks>
  public void StartBufferedStream(
    IEnumerable<uint> addr,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds)
  {
    this.StartBufferedStream(this.getCommandWordArray(addr), numCaptures, numBuffers, new int?(timeoutSeconds), (BackgroundWorker) null);
  }

  /// <summary>
  /// Starts a buffered stream read, uses the background worker handle to update progress.
  /// </summary>
  /// <param name="addr">Adresses to read.</param>
  /// <param name="numCaptures">Number of captures in each buffer for SDP transfer.</param>
  /// <param name="numBuffers">Number of buffers to Be captured.</param>
  /// <param name="timeoutSeconds">Timeout for transmission of one stream packet.</param>
  /// <param name="worker">BackgroundWorker that routine can report progress to (may be null).</param>
  /// <remarks>
  /// </remarks>
  public void StartBufferedStream(
    IEnumerable<uint> addr,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    this.StartBufferedStream(this.getCommandWordArray(addr), numCaptures, numBuffers, new int?(timeoutSeconds), worker);
  }

  /// <summary>
  /// Starts a buffered stream read, uses the background worker handle to update progress.
  /// </summary>
  /// <param name="writeData">Data to be shifted.</param>
  /// <param name="numCaptures">Number of captures in each buffer for SDP transfer.</param>
  /// <param name="numBuffers">Number of buffers to Be captured.</param>
  /// <param name="timeoutSeconds">Timeout for transmission of one stream packet.</param>
  /// <param name="worker">BackgroundWorker that routine can report progress to (may be null).</param>
  /// <remarks>
  /// </remarks>
  private void StartBufferedStream(
    ushort[] writeData,
    uint numCaptures,
    uint numBuffers,
    int? timeoutSeconds,
    BackgroundWorker worker)
  {
    bool flag1 = worker != null && worker.WorkerReportsProgress;
    bool flag2 = worker != null && worker.WorkerSupportsCancellation;
    int num1 = 0;
    if (timeoutSeconds.HasValue)
      this.StreamTimeoutSeconds = timeoutSeconds.Value;
    int numWords = writeData.Length * (int) numCaptures;
    if (this.BurstMode > (ushort) 0)
      numWords = (int) this.BurstMode * (int) numCaptures;
    int streamPacketSize = iSensorSpi.getStreamPacketSize(numWords);
    lock (iSensorSpi.packetDataSyncLock)
      this.packetDataSyncQueue.Clear();
    this.initializeStream(writeData, numCaptures, numBuffers, iSensorSpi.TransferType.streamFixLength);
    this.StreamFrom(streamPacketSize);
    uint num2 = 0;
    while (num2 < numBuffers && !this.Cancel && (!flag2 || !worker.CancellationPending))
    {
      try
      {
        ushort[] streamDataPacket = this.getStreamDataPacket(numWords);
        lock (iSensorSpi.packetDataSyncLock)
          this.packetDataSyncQueue.Enqueue(streamDataPacket);
        ++num2;
        if (flag1)
        {
          int percentProgress = (int) ((double) num2 / (double) numBuffers * 100.0);
          if (percentProgress > num1)
          {
            num1 = percentProgress;
            worker.ReportProgress(percentProgress);
          }
        }
      }
      catch (SdpDriverInterfaceException ex)
      {
        this.StopStream();
        this.CancelStream = false;
        this.DrActive = false;
        if (ex.number == 3001)
          throw new TimeoutException((Exception) ex);
        throw;
      }
    }
    this.StopStream();
    this.CancelStream = false;
    this.DrActive = false;
  }

  private ushort[] getStreamDataPacket(int numWords)
  {
    ushort[] array1;
    switch (this.WordSize)
    {
      case 8:
        byte[] array2;
        this.AdisBase.Base.getStreamDataU8(ref array2);
        Array.Resize<byte>(ref array2, numWords);
        array1 = array2.Cast<ushort>().ToArray<ushort>();
        break;
      case 16 /*0x10*/:
        this.AdisBase.Base.getStreamDataU16(ref array1);
        Array.Resize<ushort>(ref array1, numWords);
        break;
      case 32 /*0x20*/:
        uint[] source;
        this.AdisBase.Base.getStreamDataU32(ref source);
        array1 = new ushort[((IEnumerable<uint>) source).Count<uint>()];
        for (int index = 0; index < ((IEnumerable<uint>) source).Count<uint>(); ++index)
          array1[index] = (ushort) (source[index] >> 16 /*0x10*/);
        break;
      default:
        throw new Exception("Invalid Word Size");
    }
    return array1;
  }

  /// <summary>
  /// Starts a stream read, uses the background worker handle to update progress.
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <param name="streamTimeoutInSec"></param>
  /// <param name="worker"></param>
  /// <param name="e"></param>
  /// <remarks>
  /// This should be obsoleted.  Use the reworked StartBufferedStream above.
  /// </remarks>
  [Obsolete("This method obsolete. Use StartBufferedStream instead.", true)]
  public void ReadRegArrayStream(
    IEnumerable<uint> addr,
    uint numCaptures,
    uint numBuffers,
    int streamTimeoutInSec,
    BackgroundWorker worker,
    DoWorkEventArgs e)
  {
  }

  /// <summary>
  /// Starts, reads and returns data from a fixed number of buffers using SDP stream.
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  public ushort[] ReadRegArrayStream(IEnumerable<uint> addr, uint numCaptures, uint numBuffers)
  {
    return this.ReadRegArrayStream(this.getCommandWordArray(addr), numCaptures, numBuffers);
  }

  /// <summary>
  /// Starts, reads and returns data from a fixed number of buffers from SDP stream.
  /// </summary>
  /// <param name="addrData"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  public ushort[] ReadRegArrayStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers)
  {
    return this.ReadRegArrayStream(this.getCommandWordArray(addrData), numCaptures, numBuffers);
  }

  private ushort[] ReadRegArrayStream(ushort[] writeData, uint numCaptures, uint numBuffers)
  {
    int num = (int) numCaptures * writeData.Length;
    if (this.BurstMode > (ushort) 0)
    {
      if (writeData.Length != 1)
        throw new ArgumentException("writedata length must be 1 in burst mode.");
      num = (int) numCaptures * (int) this.BurstMode;
    }
    int streamPacketSize = iSensorSpi.getStreamPacketSize(num);
    ushort[] destinationArray = new ushort[(long) num * (long) numBuffers];
    try
    {
      this.initializeStream(writeData, numCaptures, numBuffers, iSensorSpi.TransferType.streamFixLength);
      this.StreamFrom(streamPacketSize);
      for (int index1 = 0; (long) index1 < (long) numBuffers & !this.Cancel; ++index1)
      {
        ushort[] sourceArray = new ushort[1];
        switch (this.WordSize)
        {
          case 8:
            byte[] source1;
            this.AdisBase.Base.getStreamDataU8(ref source1);
            sourceArray = source1.Cast<ushort>().ToArray<ushort>();
            break;
          case 16 /*0x10*/:
            this.AdisBase.Base.getStreamDataU16(ref sourceArray);
            break;
          case 32 /*0x20*/:
            if (this.frameProtocol == iSensorSpi.frameProtocolType.xc32bit)
            {
              uint[] source2;
              this.AdisBase.Base.getStreamDataU32(ref source2);
              sourceArray = new ushort[((IEnumerable<uint>) source2).Count<uint>()];
              for (int index2 = 0; index2 < ((IEnumerable<uint>) source2).Count<uint>(); ++index2)
                sourceArray[index2] = (ushort) (source2[index2] >> 16 /*0x10*/);
              break;
            }
            break;
          default:
            throw new Exception("Invalid Word Size");
        }
        Array.Copy((Array) sourceArray, 0, (Array) destinationArray, index1 * num, num);
      }
    }
    catch (SdpDriverInterfaceException ex)
    {
      this.StopStream();
      this.CancelStream = false;
      this.DrActive = false;
      if (ex.number == 3001)
        throw new TimeoutException((Exception) ex);
      throw;
    }
    this.StopStream();
    this.CancelStream = false;
    this.DrActive = false;
    return destinationArray;
  }

  /// <summary>Stops an unbuffered stream.</summary>
  public void StopStream()
  {
    ((CsaController) this.AdisBase.Base).stopStream();
    if (!this.AutoPinDisable)
      return;
    this.DisablePins();
  }

  /// <summary>
  /// Starts a variable length, unbuffered streaming operation.
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  public void StartStream(IEnumerable<uint> addr, uint numCaptures)
  {
    ushort[] commandWordArray = this.getCommandWordArray(addr);
    int numWords = this.BurstMode <= (ushort) 0 ? commandWordArray.Length * (int) numCaptures : (int) this.BurstMode * (int) numCaptures;
    this.initializeStream(commandWordArray, numCaptures, 0U, iSensorSpi.TransferType.streamVarLength);
    this.StreamFrom(iSensorSpi.getStreamPacketSize(numWords));
  }

  /// <summary>Gets an unbuffered stream packet.</summary>
  /// <returns></returns>
  public ushort[] GetStreamDataPacketU16()
  {
    ushort[] streamDataPacketU16;
    this.AdisBase.Base.getStreamDataU16(ref streamDataPacketU16);
    return streamDataPacketU16;
  }

  /// <summary>
  /// Starts a streaming operation in the SDP formware. (internal)
  /// </summary>
  /// <param name="writeData"></param>
  /// <param name="numBursts"></param>
  /// <param name="numPackets"></param>
  /// <param name="transferMode"></param>
  private void initializeStream(
    ushort[] writeData,
    uint numBursts,
    uint numPackets,
    iSensorSpi.TransferType transferMode)
  {
    ushort[] numArray1;
    if (writeData.Length <= 64 /*0x40*/)
    {
      switch (this.WordSize)
      {
        case 8:
          byte[] numArray2;
          this.AdisBase.Base.userCmdU8(this.spiStreamInit, this.GetParameterArray(transferMode, writeData, (uint) writeData.Length, numBursts, numPackets), (byte[]) null, 0, ref numArray2);
          break;
        case 16 /*0x10*/:
          this.AdisBase.Base.userCmdU16(this.spiStreamInit, this.GetParameterArray(transferMode, writeData, (uint) writeData.Length, numBursts, numPackets), (ushort[]) null, 0, ref numArray1);
          break;
      }
      if (this.frameProtocol != iSensorSpi.frameProtocolType.xc32bit)
        return;
      uint[] numArray3;
      this.AdisBase.Base.userCmdU32(this.spiStreamInit, this.GetParameterArray(transferMode, writeData, (uint) writeData.Length, numBursts, numPackets), (uint[]) null, 0, ref numArray3);
    }
    else
      this.AdisBase.Base.userCmdU16(this.spiStreamInit, this.GetParameterArray(transferMode, (uint) writeData.Length, numBursts, numPackets), writeData, 0, ref numArray1);
  }

  /// <summary>
  /// Starts a streaming transfer using the specified packet size.
  /// </summary>
  /// <param name="packetWords">number of 16 bit words in a streaming packet.</param>
  private void StreamFrom(int packetWords)
  {
    this.AdisBase.Base.userStartStream(this.spiStreamStart, new uint[1]
    {
      (uint) (2 * packetWords)
    }, 2 * packetWords, this.StreamTimeoutSeconds * 1000);
  }

  /// <summary>
  /// Returns the appropriate packet size for a given number of words.
  /// </summary>
  /// <param name="numWords"></param>
  /// <returns></returns>
  private static int getStreamPacketSize(int numWords)
  {
    int num = numWords / 256 /*0x0100*/;
    if (numWords % 256 /*0x0100*/ != 0)
      ++num;
    return num * 256 /*0x0100*/;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  public ushort[] ReadRegArray(IEnumerable<uint> addr) => this.ReadRegArray(addr, 1U);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  public ushort[] ReadRegArray(IEnumerable<uint> addr, uint numCaptures)
  {
    return this.WriteReadArray(this.getCommandWordArray(addr), numCaptures);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addrData"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  public ushort[] ReadRegArray(IEnumerable<AddrDataPair> addrData, uint numCaptures)
  {
    return this.WriteReadArray(this.getCommandWordArray(addrData), numCaptures);
  }

  /// <summary>Reads a byte from the specified register.</summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  public ushort ReadRegByte(uint addr)
  {
    ushort num = this.ReadRegWord(addr);
    return this.WordSize == 8 ? (ushort) ((uint) num & (uint) byte.MaxValue) : (addr % 2U == 0U ? (ushort) ((int) num & (int) byte.MaxValue) : (ushort) ((int) num >> 8));
  }

  /// <summary>Reads a word from the specified register.</summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  public ushort ReadRegWord(uint addr)
  {
    return this.WriteReadArray(new ushort[1]
    {
      this.getCommandWord(addr)
    })[0];
  }

  /// <summary>Writes bytes to the specified registers.</summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  public void WriteRegByte(uint addr, uint data) => this.Write(this.getCommandWord(addr, data));

  /// <summary>Writes bytes to the specified registers.</summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  public void WriteRegByte(IEnumerable<uint> addr, IEnumerable<uint> data)
  {
    this.Write(this.getCommandWordArray(addr, data));
  }

  /// <summary>Writes bytes to the specified registers.</summary>
  /// <param name="addrData"></param>
  public void WriteRegByte(IEnumerable<AddrDataPair> addrData)
  {
    this.Write(this.getCommandWordArray(addrData));
  }

  /// <summary>Writes a byte to the specified register.</summary>
  /// <param name="addrData"></param>
  public void WriteRegByte(AddrDataPair addrData)
  {
    this.Write(this.getCommandWord(addrData.addr, addrData.data));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  [Obsolete("This may be Obsoleted. Please use WriteRegByte instead.")]
  public void WriteRegWord(uint addr, uint data)
  {
    if (addr % 2U > 0U)
      throw new ArgumentException("WriteRegWord must be called with an even address.");
    this.Write(new ushort[2]
    {
      this.getCommandWord(addr, data & (uint) byte.MaxValue),
      this.getCommandWord(addr | 1U, data >> 8 & (uint) byte.MaxValue)
    });
  }

  /// <summary>
  /// Sends a low pulse to the reset line, if it is defined.
  /// </summary>
  public void Reset()
  {
    if (this.ResetPin == null)
      return;
    this.PinFunctions.PulseDrive((IPinObject) this.ResetPin, 0U, 10.0, 1U);
  }

  /// <summary>Sets the reset line high, if defined.</summary>
  public void Start()
  {
    if (this.ResetPin == null)
      return;
    this.PinFunctions.SetPin((IPinObject) this.ResetPin, 1U);
  }

  /// <summary>Write one unsigned 16 bit word through SPI interface</summary>
  /// <param name="writeData">Data word to write.</param>
  private void Write(ushort writeData)
  {
    this.Write(new ushort[1]{ writeData });
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <param name="CSpolarity"></param>
  /// <returns></returns>
  public ushort[] WriteMsrTime(ushort[] writeData, uint CSpolarity)
  {
    ushort[] numArray = new ushort[1];
    if (writeData.Length <= 64 /*0x40*/)
    {
      uint[] parameterArray = this.GetParameterArray(iSensorSpi.TransferType.writeOnly, writeData, (uint) writeData.Length, 1U);
      parameterArray[16 /*0x10*/] = 1U;
      parameterArray[17] = CSpolarity;
      this.AdisBase.Base.userCmdU16(this.spiTranfer, parameterArray, new ushort[0], 2, ref numArray);
      if (this.AutoPinDisable)
        this.DisablePins();
      return numArray;
    }
    uint[] parameterArray1 = this.GetParameterArray(iSensorSpi.TransferType.writeOnly, (uint) writeData.Length, 1U);
    parameterArray1[16 /*0x10*/] = 1U;
    parameterArray1[17] = CSpolarity;
    this.AdisBase.Base.userCmdU16(this.spiTranfer, parameterArray1, writeData, 2, ref numArray);
    if (this.AutoPinDisable)
      this.DisablePins();
    return numArray;
  }

  private void Write(ushort[] writeData)
  {
    ushort[] numArray1 = new ushort[0];
    uint[] numArray2 = new uint[0];
    if (writeData.Length <= 64 /*0x40*/)
    {
      uint[] parameterArray = this.GetParameterArray(iSensorSpi.TransferType.writeOnly, writeData, (uint) writeData.Length, 1U);
      switch (this.WordSize)
      {
        case 16 /*0x10*/:
          this.AdisBase.Base.userCmdU16(this.spiTranfer, parameterArray, new ushort[0], numArray1.Length, ref numArray1);
          break;
        case 32 /*0x20*/:
          this.AdisBase.Base.userCmdU32(this.spiTranfer, parameterArray, new uint[0], numArray1.Length, ref numArray2);
          break;
      }
    }
    else
      this.AdisBase.Base.userCmdU16(this.spiTranfer, this.GetParameterArray(iSensorSpi.TransferType.writeOnly, (uint) writeData.Length, 1U), writeData, numArray1.Length, ref numArray1);
    if (!this.AutoPinDisable)
      return;
    this.DisablePins();
  }

  /// <summary>
  /// Reads an array of 16 bit words from spi interface in to an ushort array.
  /// </summary>
  /// <param name="numCaptures">Number of words to read.</param>
  /// <param name="writeData">Data words to be sent during each read operations.</param>
  /// <returns>Words read from SPI interface.</returns>
  public ushort[] WriteReadArray(ushort[] writeData, uint numCaptures)
  {
    int num;
    if (this.BurstMode > (ushort) 0)
    {
      if (writeData.Length != 1)
        throw new ArgumentException("writedata length must be 1 in burst mode.");
      num = (int) this.BurstMode;
    }
    else
      num = writeData.Length;
    int length = num * (int) numCaptures;
    ushort[] numArray = new ushort[length];
    byte[] source1;
    if (this.WordSize == 8)
      source1 = new byte[length];
    if (writeData.Length <= 64 /*0x40*/)
    {
      uint[] parameterArray = this.GetParameterArray(iSensorSpi.TransferType.singlePacket, writeData, (uint) writeData.Length, numCaptures);
      switch (this.WordSize)
      {
        case 8:
          this.AdisBase.Base.userCmdU8(this.spiTranfer, parameterArray, new byte[0], length, ref source1);
          numArray = source1.Cast<ushort>().ToArray<ushort>();
          break;
        case 16 /*0x10*/:
          this.AdisBase.Base.userCmdU16(this.spiTranfer, parameterArray, new ushort[0], length, ref numArray);
          break;
        case 32 /*0x20*/:
          uint[] source2;
          this.AdisBase.Base.userCmdU32(this.spiTranfer, parameterArray, new uint[0], length, ref source2);
          numArray = new ushort[((IEnumerable<uint>) source2).Count<uint>()];
          for (int index = 0; index < ((IEnumerable<uint>) source2).Count<uint>(); ++index)
            numArray[index] = (ushort) (source2[index] >> 16 /*0x10*/);
          break;
      }
    }
    else
      this.AdisBase.Base.userCmdU16(this.spiTranfer, this.GetParameterArray(iSensorSpi.TransferType.singlePacket, (uint) writeData.Length, numCaptures), writeData, length, ref numArray);
    if (this.AutoPinDisable)
      this.DisablePins();
    return numArray;
  }

  /// <summary>
  /// Reads an array of 16 bit words from spi interface in to an ushort array the same length as data.
  /// </summary>
  /// <param name="writeData">Data words to be sent during each read operations.</param>
  /// <returns>Words read from SPI interface.</returns>
  public ushort[] WriteReadArray(ushort[] writeData) => this.WriteReadArray(writeData, 1U);

  /// <summary>
  /// Disables the pins used in this SPI interface and leave pins in tri-state mode.
  /// </summary>
  public void DisablePins()
  {
    if (this.PinDisableDelegate != null)
      this.PinDisableDelegate();
    else
      this.DisablePinsDefault();
  }

  /// <summary>
  /// Disables the SDP pins used in this SPI interface and leave pins in tri-state mode.
  /// </summary>
  private void DisablePinsDefault()
  {
    List<IPinObject> pins = new List<IPinObject>()
    {
      (IPinObject) this.SclkPin,
      (IPinObject) this.MosiPin,
      (IPinObject) this.CsPin
    };
    if (this.ResetPin != null)
      pins.Add((IPinObject) this.ResetPin);
    int num = (int) this.PinFunctions.ReadPins((IEnumerable<IPinObject>) pins);
  }

  /// <summary>Obsolete.</summary>
  /// <param name="pin"></param>
  /// <returns></returns>
  [Obsolete("Obsolete - replace with call to PinFunctions.ReadPin for this object.", true)]
  public bool ReadPin(PinObject pin) => false;

  /// <summary>Makes an array of READ command words.</summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  private ushort[] getCommandWordArray(IEnumerable<uint> addr)
  {
    return addr.Select<uint, ushort>((Func<uint, ushort>) (a => this.getCommandWord(a))).ToArray<ushort>();
  }

  /// <summary>
  /// Makes an array of WRITE or READ command words based on tranfer direction of each word.
  /// </summary>
  /// <param name="addrData"></param>
  /// <returns></returns>
  private ushort[] getCommandWordArray(IEnumerable<AddrDataPair> addrData)
  {
    return addrData.Select<AddrDataPair, ushort>((Func<AddrDataPair, ushort>) (a => this.getCommandWord(a.addr, a.data))).ToArray<ushort>();
  }

  /// <summary>Makes an array of WRITE command words.</summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  private ushort[] getCommandWordArray(IEnumerable<uint> addr, IEnumerable<uint> data)
  {
    List<uint> dataList = data.ToList<uint>();
    return addr.Select<uint, ushort>((Func<uint, int, ushort>) ((a, i) => this.getCommandWord(a, dataList[i]))).ToArray<ushort>();
  }

  /// <summary>Return a READ command word.</summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  private ushort getCommandWord(uint addr)
  {
    int num1 = 8;
    uint num2 = 32768 /*0x8000*/;
    if (this.WordSize == 8)
    {
      num1 = 0;
      num2 = 128U /*0x80*/;
    }
    return !this.Read0Write1Flip ? (ushort) (((int) addr & (int) sbyte.MaxValue) << num1) : (ushort) (num2 | (uint) (((int) addr & (int) sbyte.MaxValue) << num1));
  }

  /// <summary>Return a WRITE command word.</summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  private ushort getCommandWord(uint addr, uint data)
  {
    return !this.Read0Write1Flip ? (ushort) (32768 /*0x8000*/ | ((int) addr & (int) sbyte.MaxValue) << 8 | (int) data & (int) byte.MaxValue) : (ushort) (((int) addr & (int) sbyte.MaxValue) << 8 | (int) data & (int) byte.MaxValue);
  }

  /// <summary>
  /// Return a WRITE command word if data is not null, READ if data is null.
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  /// <returns></returns>
  private ushort getCommandWord(uint addr, uint? data)
  {
    return data.HasValue ? this.getCommandWord(addr, data.Value) : this.getCommandWord(addr);
  }

  private uint[] GetParameterArray(
    iSensorSpi.TransferType transferMode,
    uint numRegs,
    uint numBursts)
  {
    return this.GetParameterArray(transferMode, numRegs, numBursts, 1U);
  }

  private uint[] GetParameterArray(
    iSensorSpi.TransferType transferMode,
    uint numRegs,
    uint numBursts,
    uint numPackets)
  {
    return this.GetParameterArray(transferMode, new uint[0], numRegs, numBursts, numPackets);
  }

  private uint[] GetParameterArray(
    iSensorSpi.TransferType transferMode,
    ushort[] outData,
    uint numRegs,
    uint numBursts)
  {
    return this.GetParameterArray(transferMode, outData, numRegs, numBursts, 1U);
  }

  private uint[] GetParameterArray(
    iSensorSpi.TransferType transferMode,
    ushort[] outData,
    uint numRegs,
    uint numBursts,
    uint numPackets)
  {
    uint[] numArray = new uint[outData.Length];
    Array.Copy((Array) outData, (Array) numArray, outData.Length);
    return this.GetParameterArray(transferMode, numArray, numRegs, numBursts, numPackets);
  }

  /// <summary>
  /// Creates and returns a Parameter array for a user command.
  /// </summary>
  /// <param name="transferMode"></param>
  /// <param name="outData"></param>
  /// <param name="numRegs"></param>
  /// <param name="numBursts"></param>
  /// <returns></returns>
  private uint[] GetParameterArray(
    iSensorSpi.TransferType transferMode,
    uint[] outData,
    uint numRegs,
    uint numBursts)
  {
    return this.GetParameterArray(transferMode, outData, numRegs, numBursts, 1U);
  }

  /// <summary>
  /// Creates and returns a Parameter array for a user command.
  /// </summary>
  /// <param name="transferMode"></param>
  /// <param name="outData"></param>
  /// <param name="numRegs"></param>
  /// <param name="numBursts"></param>
  /// <param name="numPackets"></param>
  /// <returns></returns>
  private uint[] GetParameterArray(
    iSensorSpi.TransferType transferMode,
    uint[] outData,
    uint numRegs,
    uint numBursts,
    uint numPackets)
  {
    uint[] header = new uint[123];
    if ((transferMode & iSensorSpi.TransferType.streamFixLength) == iSensorSpi.TransferType.streamFixLength && numPackets <= 0U && numPackets <= 0U)
      throw new ArgumentException("Number of packets must be positive for fixed length streaming.");
    if (numRegs <= 0U)
      throw new ArgumentException("Number of registers must be positive at GetParameterArray call.");
    this.insertConfigParameters(ref header);
    this.insertDataParameters(ref header, outData);
    header[6] = (uint) transferMode;
    header[7] = numPackets;
    header[8] = numBursts;
    header[10] = numRegs;
    return header;
  }

  /// <summary>inserts data into parameter array, sets numRegisters.</summary>
  /// <param name="header"></param>
  /// <param name="outData"></param>
  private void insertDataParameters(ref uint[] header, uint[] outData)
  {
    if (outData.Length > 83)
      throw new ArgumentException($"SPI Transfers limited to {83} sixteen bit registers per data ready.");
    Array.Copy((Array) outData, 0, (Array) header, 40, outData.Length);
  }

  /// <summary>
  /// Inserts parameters for configuration into header based on instance properties.
  /// </summary>
  /// <param name="header"></param>
  private void insertConfigParameters(ref uint[] header)
  {
    uint num = (uint) ((this.LoopMode ? 2 : 0) | (this.DrActive ? 1 : 0));
    header[0] = (uint) this.sclkFrequency;
    header[1] = (uint) this.StallCycles;
    header[2] = (uint) this.WordSize;
    header[3] = num;
    header[4] = this.Cpha ? 1U : 0U;
    header[5] = this.Cpol ? 1U : 0U;
    header[19] = (uint) this.BurstMode;
    header[20] = (uint) this.TriggerDrRegCommand;
    header[21] = this.sampleRate;
    try
    {
      header[11] = this.SclkPin.pinConfig;
      header[12] = this.CsPin.pinConfig;
      header[13] = this.MisoPin.pinConfig;
      header[14] = this.MosiPin.pinConfig;
      if (this.ReadyPin == null)
        header[15] = 0U;
      else
        header[15] = this.ReadyPin.pinConfig ^ (this.DrPolarity ? 512U /*0x0200*/ : 0U);
    }
    catch (NullReferenceException ex)
    {
      throw new Exception("SPI interface pins must be initialized before issuing commands.");
    }
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="WriteData"></param>
  /// <returns></returns>
  public uint Transfer(uint WriteData)
  {
    uint[] numArray = new uint[1];
    return this.TransferArray((IEnumerable<uint>) new uint[1]
    {
      WriteData
    }, 1U, 1U)[0];
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="WriteData"></param>
  /// <returns></returns>
  public uint[] TransferArray(IEnumerable<uint> WriteData)
  {
    uint[] numArray = new uint[WriteData.Count<uint>()];
    return this.TransferArray(WriteData, 1U, 1U);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="WriteData"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  public uint[] TransferArray(IEnumerable<uint> WriteData, uint numCaptures)
  {
    uint[] numArray = new uint[WriteData.Count<uint>()];
    return this.TransferArray(WriteData, numCaptures, 1U);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="WriteData"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  public uint[] TransferArray(IEnumerable<uint> WriteData, uint numCaptures, uint numBuffers)
  {
    this.frameProtocol = iSensorSpi.frameProtocolType.xc32bit;
    uint[] outData = new uint[WriteData.Count<uint>()];
    int index = 0;
    foreach (uint num in WriteData)
    {
      outData[index] = num;
      ++index;
    }
    uint[] destinationArray = new uint[WriteData.Count<uint>()];
    destinationArray[0] = 10U;
    uint[] numArray1 = new uint[1];
    uint[] numArray2 = new uint[123];
    uint numBursts = 1;
    uint numPackets = 1;
    uint numRegs = (uint) WriteData.Count<uint>();
    int num1 = (int) numRegs;
    this.AdisBase.Base.userCmdU32(this.spiTranfer, this.GetParameterArray(iSensorSpi.TransferType.singlePacket, outData, numRegs, numBursts, numPackets), new uint[0], num1, ref numArray1);
    long num2 = (long) ((IEnumerable<uint>) numArray1).Count<uint>();
    Array.Copy((Array) numArray1, 0L, (Array) destinationArray, 1L, num2 - 1L);
    return destinationArray;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  uint[] ISpi32Interface.GetBufferedStreamDataPacket() => throw new NotImplementedException();

  [Flags]
  private enum TransferType
  {
    singlePacket = 0,
    streamFixLength = 1,
    streamVarLength = 2,
    writeOnly = streamVarLength | streamFixLength, // 0x00000003
  }

  /// <summary>SPI frame protocol identifiers.</summary>
  public enum frameProtocolType
  {
    /// <summary>Standard legacy 16 bit frame.</summary>
    adis16bit,
    /// <summary>A 32 bit SPI frame used by the adxc1500 sensor.</summary>
    xc32bit,
  }
}
