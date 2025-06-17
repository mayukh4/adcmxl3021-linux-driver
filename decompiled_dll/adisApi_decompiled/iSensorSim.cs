// Decompiled with JetBrains decompiler
// Type: AdisApi.iSensorSim
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace AdisApi;

/// <summary>
/// Basic SPI configuration, reads, and writes using ADiS spi code on Blackfin.
/// </summary>
public class iSensorSim : IRegInterface
{
  private int sclkFrequency;
  private int stallCycles;
  private List<ushort> simWriteDataList = new List<ushort>();
  private int streamRegCount;
  private uint streamNumCaptures;
  private uint streamNumBuffers;

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
    this.SimThrowTimeoutException = false;
  }

  /// <summary>
  /// 
  /// </summary>
  public iSensorSim()
  {
    this.AdisBase = new AdisBase();
    this.setDefaultProperties();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="AdisBase"></param>
  public iSensorSim(AdisBase AdisBase)
  {
    this.AdisBase = AdisBase;
    this.setDefaultProperties();
  }

  /// <summary>Gets or sets flag for burst mode.</summary>
  public ushort BurstMode { get; set; }

  /// <summary>
  /// Gets the AdisBase object associated with thie Spi interface.
  /// </summary>
  public AdisBase AdisBase { get; private set; }

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
  /// If False, Read is Active Low and  Write is Active High. If true, Read is Active High, Write is Active Low
  /// </summary>
  public bool Read0Write1Flip { get; set; }

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
  /// Gets or sets the Slave Select line to use for this SPI interface.
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
  public int WordSize { get; private set; }

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
  /// Simulator will throw TimeoutException upon next applicble call.
  /// </summary>
  public bool SimThrowTimeoutException { get; set; }

  /// <summary>Buffer of data to be returned by read commands</summary>
  public ushort[] SimReadData { get; set; }

  /// <summary>Buffer of data to be returned by read commands</summary>
  public ushort[] SimRtsData { get; set; }

  /// <summary>
  /// Set to true if GetBufferedStreamDataPacket should return null
  /// </summary>
  public bool SimRtsNull { get; set; }

  /// <summary>
  /// Buffer of data that was written in last simulated write command call.
  /// </summary>
  public ushort[] SimWriteData => this.simWriteDataList.ToArray();

  /// <summary>
  /// Clears the accumulated write data and method call counts.
  /// </summary>
  public void SimClear()
  {
    this.simWriteDataList = new List<ushort>();
    this.SimResetMethodCallCount = 0;
    this.SimStartMethodCallCount = 0;
  }

  /// <summary>
  /// Returns the number of calls made to the Reset method since counts were cleared.
  /// </summary>
  public int SimResetMethodCallCount { get; private set; }

  /// <summary>
  /// Returns the number of calls made to the Start method since counts were cleared.
  /// </summary>
  public int SimStartMethodCallCount { get; private set; }

  /// <summary>
  /// Starts, reads and returns data from a fixed number of buffers from SDP stream.
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  public ushort[] ReadRegArrayStream(IEnumerable<uint> addr, uint numCaptures, uint numBuffers)
  {
    return this.ReadRegArrayStream((IEnumerable<ushort>) this.getCommandWordArray(addr), numCaptures, numBuffers);
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
    return this.ReadRegArrayStream((IEnumerable<ushort>) this.getCommandWordArray(addrData), numCaptures, numBuffers);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  public ushort[] ReadRegArrayStream(
    IEnumerable<ushort> writeData,
    uint numCaptures,
    uint numBuffers)
  {
    if (this.SimThrowTimeoutException)
      throw new TimeoutException();
    this.simWriteDataList.AddRange(writeData);
    return this.generateFakeReadData(writeData, numCaptures, numBuffers);
  }

  private ushort[] generateFakeReadData(
    IEnumerable<ushort> writeData,
    uint numCaptures,
    uint numBuffers)
  {
    return this.BurstMode > (ushort) 0 ? this.generateFakeReadData_BurstMode(writeData, numCaptures, numBuffers) : this.generateFakeReadData_Burst_Off(writeData, numCaptures, numBuffers);
  }

  private ushort[] generateFakeReadData_BurstMode(
    IEnumerable<ushort> writeData,
    uint numCaptures,
    uint numBuffers)
  {
    if (writeData.Count<ushort>() != 1)
      throw new ArgumentException("writedata length must be 1 in burst mode.");
    int count = (int) this.BurstMode * (int) numCaptures * (int) numBuffers;
    if (this.SimReadData == null || this.SimReadData.Length < count)
      throw new Exception("SimReadData is null or of insufficient length for operation.");
    return ((IEnumerable<ushort>) this.SimReadData).Take<ushort>(count).ToArray<ushort>();
  }

  private ushort[] generateFakeReadData_Burst_Off(
    IEnumerable<ushort> writeData,
    uint numCaptures,
    uint numBuffers)
  {
    bool[] array = writeData.Select<ushort, bool>((Func<ushort, bool>) (d => ((int) d & 32768 /*0x8000*/) == 0)).ToArray<bool>();
    int num1 = writeData.Count<ushort>();
    int num2 = num1 * (int) numCaptures * (int) numBuffers;
    int num3 = ((IEnumerable<bool>) array).Count<bool>((Func<bool, bool>) (b => b)) * (int) numCaptures * (int) numBuffers;
    if (this.SimReadData == null || this.SimReadData.Length < num3)
      throw new Exception("SimReadData is null or of insufficient length for operation.");
    int num4 = 0;
    List<ushort> ushortList = new List<ushort>();
    for (int index = 0; index < num2; ++index)
      ushortList.Add(array[index % num1] ? this.SimReadData[num4++] : (ushort) 0);
    return ushortList.ToArray();
  }

  private ushort[] generateFakeReadData_Stream(int regCount, uint numCaptures, uint numBuffers)
  {
    int num = regCount * (int) numCaptures * (int) numBuffers;
    if (this.SimReadData == null || this.SimReadData.Length < num)
      throw new Exception("SimReadData is null or of insufficient length for operation.");
    List<ushort> ushortList = new List<ushort>();
    for (int index = 0; index < num; ++index)
      ushortList.Add(this.SimReadData[index]);
    return ushortList.ToArray();
  }

  /// <summary>
  /// 
  /// </summary>
  public void StopStream()
  {
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
    this.initializeStream(this.getCommandWordArray(addrData), numCaptures, numBuffers, iSensorSim.TransferType.streamFixLength);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <param name="timeoutSeconds"></param>
  /// <param name="worker"></param>
  public void StartBufferedStream(
    IEnumerable<uint> addr,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    this.initializeStream(this.getCommandWordArray(addr), numCaptures, numBuffers, iSensorSim.TransferType.streamFixLength);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  public void StartStream(IEnumerable<uint> addr, uint numCaptures)
  {
    this.initializeStream(this.getCommandWordArray(addr), numCaptures, 1U, iSensorSim.TransferType.streamVarLength);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public ushort[] GetBufferedStreamDataPacket()
  {
    if (this.SimRtsNull)
      return (ushort[]) null;
    return this.SimRtsData != null ? this.SimRtsData : this.generateFakeReadData_Stream(this.streamRegCount, this.streamNumCaptures, this.streamNumBuffers);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public ushort[] GetStreamDataPacketU16() => this.GetBufferedStreamDataPacket();

  private void initializeStream(
    ushort[] writeData,
    uint numCaptures,
    uint numBuffers,
    iSensorSim.TransferType transferMode)
  {
    this.streamRegCount = ((IEnumerable<ushort>) writeData).Count<ushort>();
    this.streamNumBuffers = numBuffers;
    this.streamNumCaptures = numCaptures;
    this.simWriteDataList.AddRange((IEnumerable<ushort>) writeData);
  }

  private static int getStreamPacketSize(uint numWords)
  {
    int num = (int) (numWords / 256U /*0x0100*/);
    if (numWords % 256U /*0x0100*/ > 0U)
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

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  public ushort ReadRegByte(uint addr)
  {
    ushort num = this.ReadRegWord(addr);
    return addr % 2U == 0U ? (ushort) ((int) num & (int) byte.MaxValue) : (ushort) ((int) num >> 8);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  public ushort ReadRegWord(uint addr)
  {
    return this.WriteReadArray(new ushort[1]
    {
      this.getCommandWord(addr)
    })[0];
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  public void WriteRegByte(uint addr, uint data) => this.Write(this.getCommandWord(addr, data));

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  public void WriteRegByte(IEnumerable<uint> addr, IEnumerable<uint> data)
  {
    this.Write(this.getCommandWordArray(addr, data));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addrData"></param>
  public void WriteRegByte(IEnumerable<AddrDataPair> addrData)
  {
    this.Write(this.getCommandWordArray(addrData));
  }

  /// <summary>
  /// 
  /// </summary>
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
  /// 
  /// </summary>
  public void Reset() => ++this.SimResetMethodCallCount;

  /// <summary>
  /// 
  /// </summary>
  public void Start() => ++this.SimStartMethodCallCount;

  /// <summary>
  /// Writes data that would be sent to iSensorSpi to SimWriteData Array.
  /// </summary>
  /// <param name="writeData">Data word to write.</param>
  private void Write(ushort writeData)
  {
    this.Write(new ushort[1]{ writeData });
  }

  /// <summary>
  /// Writes data that would be sent to iSensorSpi to SimWriteData Array.
  /// </summary>
  /// <param name="writeData"></param>
  private void Write(ushort[] writeData)
  {
    this.simWriteDataList.AddRange((IEnumerable<ushort>) writeData);
  }

  /// <summary>
  /// Reads an array of 16 bit words from spi interface in to an ushort array.
  /// </summary>
  /// <returns>Words read from SPI interface.</returns>
  public ushort[] WriteReadArray(ushort[] writeData, uint numCaptures)
  {
    if (this.SimThrowTimeoutException)
      throw new TimeoutException();
    this.simWriteDataList.AddRange((IEnumerable<ushort>) writeData);
    return this.generateFakeReadData((IEnumerable<ushort>) writeData, numCaptures, 1U);
  }

  /// <summary>
  /// Reads an array of 16 bit words from spi interface in to an ushort array the same length as data.
  /// </summary>
  /// <returns>Words read from SPI interface.</returns>
  public ushort[] WriteReadArray(ushort[] writeData) => this.WriteReadArray(writeData, 1U);

  /// <summary>Makes an array of READ command words..</summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  private ushort[] getCommandWordArray(IEnumerable<uint> addr)
  {
    List<uint> list = addr.ToList<uint>();
    ushort[] commandWordArray = new ushort[list.Count<uint>()];
    for (int index = 0; index < list.Count<uint>(); ++index)
      commandWordArray[index] = this.getCommandWord(list[index]);
    return commandWordArray;
  }

  /// <summary>Makes an array of WRITE command words.</summary>
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
    List<uint> list1 = addr.ToList<uint>();
    List<uint> list2 = data.ToList<uint>();
    ushort[] commandWordArray = new ushort[list1.Count<uint>()];
    for (int index = 0; index < list1.Count<uint>(); ++index)
      commandWordArray[index] = this.getCommandWord(list1[index], list2[index]);
    return commandWordArray;
  }

  private ushort getCommandWord(uint addr) => (ushort) (((int) addr & (int) sbyte.MaxValue) << 8);

  /// <summary>Return a WRITE command word if data is provided.</summary>
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

  [Flags]
  private enum TransferType
  {
    singlePacket = 0,
    streamFixLength = 1,
    streamVarLength = 2,
    writeOnly = streamVarLength | streamFixLength, // 0x00000003
  }
}
