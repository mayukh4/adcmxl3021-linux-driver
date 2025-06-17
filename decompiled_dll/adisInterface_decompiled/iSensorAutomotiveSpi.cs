// Decompiled with JetBrains decompiler
// Type: adisInterface.iSensorAutomotiveSpi
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using AdisApi;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace adisInterface;

/// <summary>
/// This class allows for interfacing with parts using the ADI robust automotive SPI protocol. The automotive SPI protocol is designed to interface with
/// 16 bit registers, with an additional 16 bits of meta data appended to each SPI word.
/// 
/// The general protocol is described below:
/// 
/// Master to DUT (MOSI) - Set via ISpi32Interface Write or Transfer commands
/// 31 |30 |29 |28 |27 |26 |25|24|23|22|21|20|19|18|17|16|15|14|13|12|11|10|9 |8 |7  |6|5|4|3   |2   |1   |0
/// D15|D14|D13|D12|D11|D10|D9|D8|D7|D6|D5|D4|D3|D2|D1|D0|A7|A6|A5|A4|A3|A2|A1|A0|R/W|0|0|0|CRC3|CRC2|CRC1|CRC0
/// 
/// DUT to Master (MISO) - Read via ISpi32Interface Read or Transfer commands
/// 31 |30 |29 |28 |27 |26 |25|24|23|22|21|20|19|18|17|16|15|14|13|12|11|10|9 |8 |7  |6  |5  |4  |3   |2   |1   |0
/// D15|D14|D13|D12|D11|D10|D9|D8|D7|D6|D5|D4|D3|D2|D1|D0|A7|A6|A5|A4|A3|A2|A1|A0|SV1|SV0|TC1|TC0|CRC3|CRC2|CRC1|CRC0
/// 
/// </summary>
public class iSensorAutomotiveSpi : IRegInterface
{
  public ISpi32Interface Spi32Interface;
  private ISpi32Interface m_Spi32;
  private bool m_DrActive;
  private int m_StreamTimeout;
  private bool m_IgnoreSV;
  private bool m_IgnoreCRC;
  private bool m_IgnoreAddr;
  private bool m_IgnoreTC;
  private bool m_logExceptions;
  private Queue<SpiException> m_exceptions;
  private DataFormatArray m_StreamWriteParameters;
  private BufferStreamInfo m_BufferInfo;
  private const int CRC_POSITION = 0;
  private const int TC_POSITION = 4;
  private const int SV_POSITION = 6;
  private const int ADDR_POSITION = 8;
  private const int DATA_POSITION = 16 /*0x10*/;
  private const int WRITEBIT_POSITION = 7;
  private const int STREAM_TIMEOUT_MAX = 100;

  /// <summary>Overloaded constructor which takes an instance of the ISpi32Interface.</summary>
  /// <param name="SpiInterface">The SPI interface to be used.</param>
  public iSensorAutomotiveSpi(ISpi32Interface SpiInterface)
  {
    this.m_Spi32 = SpiInterface;
    this.Spi32Interface = (ISpi32Interface) new Spi32Wrapper(this.m_Spi32);
    this.m_exceptions = new Queue<SpiException>();
    this.SetDefaultValues();
  }

  /// <summary>
  /// Empty constructor. Must set the Spi32Interface afterwards if this constructor is used
  /// </summary>
  public iSensorAutomotiveSpi()
  {
    this.m_exceptions = new Queue<SpiException>();
    this.SetDefaultValues();
  }

  /// <summary>
  /// Property to enable error exceptions. When enabled, exceptions are thrown for invalid CRC, state vector, transaction counter, address.
  /// </summary>
  /// <returns></returns>
  public bool IgnoreExceptions
  {
    get => this.m_IgnoreAddr & this.m_IgnoreCRC & this.m_IgnoreSV & this.m_IgnoreTC;
    set
    {
      this.m_IgnoreTC = value;
      this.m_IgnoreSV = value;
      this.m_IgnoreCRC = value;
      this.m_IgnoreAddr = value;
    }
  }

  /// <summary>Selectively ignores SPI state vector exceptions</summary>
  /// <returns></returns>
  public bool IgnoreStateVectorExceptions
  {
    get => this.m_IgnoreSV;
    set => this.m_IgnoreSV = value;
  }

  /// <summary>Selectively ignores SPI CRC exceptions</summary>
  /// <returns></returns>
  public bool IgnoreCRCExceptions
  {
    get => this.m_IgnoreCRC;
    set => this.m_IgnoreCRC = value;
  }

  /// <summary>Selectively ignores SPI transaction counter exceptions</summary>
  /// <returns></returns>
  public bool IgnoreTransactionCounterExceptions
  {
    get => this.m_IgnoreTC;
    set => this.m_IgnoreTC = value;
  }

  /// <summary>Selectively ignores SPI address echo exceptions</summary>
  /// <returns></returns>
  public bool IgnoreSpiAddressExceptions
  {
    get => this.m_IgnoreAddr;
    set => this.m_IgnoreAddr = value;
  }

  /// <summary>
  /// Enables or disables exception logging. When exception logging is turned on, exceptions are placed into a queue. This allows for down the
  /// line analysis/notification of any SPI protocol errors, without having to wrap everything in a try catch / interrupt program flow.
  /// </summary>
  /// <returns></returns>
  public bool LogExceptions
  {
    get => this.m_logExceptions;
    set
    {
      if (!value)
        this.m_exceptions.Clear();
      this.m_logExceptions = value;
    }
  }

  /// <summary>Number of logged exceptions</summary>
  /// <returns></returns>
  public int LoggedExceptionCount => this.m_exceptions.Count;

  /// <summary>
  /// Dequeue a logged exception. Returns nothing if there is no exception to dequeue
  /// </summary>
  /// <returns></returns>
  public SpiException DequeueLoggedException()
  {
    return this.m_exceptions.Count > 0 ? this.m_exceptions.Dequeue() : (SpiException) null;
  }

  /// <summary>This function sets all the default values for the class</summary>
  private void SetDefaultValues()
  {
    this.m_DrActive = false;
    this.m_StreamTimeout = 10;
    this.IgnoreExceptions = false;
    this.LogExceptions = false;
    this.m_exceptions.Clear();
    this.m_StreamWriteParameters = new DataFormatArray();
  }

  /// <summary>Calculates a 4 bit CRC on 28 bits of input data</summary>
  /// <param name="inData">The data to calculate the CRC of. Should be the 28 most significant bits of the SPI word</param>
  /// <returns>The CRC value (0 - 15)</returns>
  private int CalcCRC28Bit(uint inData)
  {
    bool[] flagArray = new bool[4]
    {
      false,
      true,
      false,
      true
    };
    int num = 0;
    do
    {
      bool flag = ((long) (inData >> checked (27 - num)) & 1L) == 1L ^ flagArray[3];
      flagArray[3] = flagArray[2];
      flagArray[2] = flagArray[1];
      flagArray[1] = flagArray[0];
      flagArray[0] = flag;
      checked { ++num; }
    }
    while (num <= 27);
    return checked (0 + (Convert.ToInt32(flagArray[3]) << 3) + (Convert.ToInt32(flagArray[2]) << 2) + (Convert.ToInt32(flagArray[1]) << 1) + Convert.ToInt32(flagArray[0]));
  }

  /// <summary>Builds a command word to send to the DUT, over the MOST line</summary>
  /// <param name="Data">The 16 bit data field to send</param>
  /// <param name="Address">The address to read/write</param>
  /// <param name="Write">If the command is a write command. Set the false for a read.</param>
  /// <returns>The 32 bit command word</returns>
  public uint BuildCommandWord(uint Data, uint Address, bool Write)
  {
    long num1 = (long) (Data << 16 /*0x10*/) | (long) (Address << 8) | (long) (Convert.ToInt32(Write) << 7);
    int num2 = this.CalcCRC28Bit(checked ((uint) (num1 >> 4)));
    return checked ((uint) (num1 | (long) (num2 << 0)));
  }

  /// <summary>
  /// Builds an array of 32 bit MOSI values based on the address/data array. Inserts an additional read back at the end to get the last value.
  /// </summary>
  /// <param name="addrData">The addresses to read from. Expects 1 address per MOSI word. If data is not nothing, performs a write for that word</param>
  /// <returns>An array of data to transmit on the MOSI line, with an associated formatting array</returns>
  private DataFormatArray BuildMOSIDataArray(IEnumerable<AddrDataPair> addrData)
  {
    List<uint> uintList = new List<uint>();
    List<int> intList = new List<int>();
    intList.Add(0);
    int num = checked (addrData.Count<AddrDataPair>() - 1);
    int index = 0;
    while (index <= num)
    {
      bool Write;
      uint Data;
      if (Information.IsNothing((object) addrData.ElementAtOrDefault<AddrDataPair>(index).data))
      {
        Write = false;
        Data = 0U;
      }
      else
      {
        Write = true;
        if ((int) addrData.ElementAtOrDefault<AddrDataPair>(index).addr != (int) addrData.ElementAtOrDefault<AddrDataPair>(checked (index + 1)).addr | Information.IsNothing((object) addrData.ElementAtOrDefault<AddrDataPair>(checked (index + 1)).data))
          throw new Exception("ERROR: The Analog Devices automotive SPI protocol expects bytes to be written in sets of 2, to form a 16 bit register value");
        uint? data = addrData.ElementAtOrDefault<AddrDataPair>(index).data;
        uint? nullable1 = addrData.ElementAtOrDefault<AddrDataPair>(checked (index + 1)).data;
        uint? nullable2 = nullable1.HasValue ? new uint?(nullable1.GetValueOrDefault() << 8) : new uint?();
        uint? nullable3;
        if (!(data.HasValue & nullable2.HasValue))
        {
          nullable1 = new uint?();
          nullable3 = nullable1;
        }
        else
          nullable3 = new uint?(checked (data.GetValueOrDefault() + nullable2.GetValueOrDefault()));
        nullable2 = nullable3;
        Data = nullable2.Value;
        checked { ++index; }
      }
      uintList.Add(this.BuildCommandWord(Data, addrData.ElementAtOrDefault<AddrDataPair>(index).addr, Write));
      if (Write)
        intList.Add(2);
      else
        intList.Add(1);
      checked { ++index; }
    }
    uintList.Add(this.BuildCommandWord(0U, 0U, false));
    DataFormatArray dataFormatArray;
    dataFormatArray.MOSIData = uintList.ToArray();
    dataFormatArray.NumRepeats = intList.ToArray();
    return dataFormatArray;
  }

  /// <summary>
  /// Builds an array of 32 bit MOSI values based on the address array. Inserts an additional read back at the end to get the last value.
  /// </summary>
  /// <param name="addr">The addresses to read from. Expects 1 address per MOSI word</param>
  /// <returns>An array of data to transmit on the MOSI line. Will be of size addr.Count() + 1</returns>
  private uint[] BuildMOSIDataArray(IEnumerable<uint> addr)
  {
    List<uint> uintList = new List<uint>();
    try
    {
      foreach (uint Address in addr)
        uintList.Add(this.BuildCommandWord(0U, Address, false));
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
    uintList.Add(this.BuildCommandWord(0U, 0U, false));
    return uintList.ToArray();
  }

  /// <summary>
  /// Validates an array of data received from the DUT, using the corresponding MOSI data as a reference for address checking
  /// </summary>
  /// <param name="MISOData">The data from the DUT to the master</param>
  /// <param name="MOSIData">The data sent from the master to the DUT for the same sequence</param>
  private void ValidateMISOData(IEnumerable<uint> MISOData, IEnumerable<uint> MOSIData)
  {
    if (this.IgnoreExceptions & !this.LogExceptions)
      return;
    this.ValidateMISOData(MISOData);
    if (MISOData.Count<uint>() != MOSIData.Count<uint>())
      throw new ArgumentException("ERROR: Invalid MISO/MOSI data pair. Must have the same size");
    if (MOSIData.Count<uint>() < 2)
      return;
    uint num1 = MISOData.ElementAtOrDefault<uint>(0) >> 4 & 3U;
    uint expectedAddress = MOSIData.ElementAtOrDefault<uint>(0) >> 8 & (uint) byte.MaxValue;
    int num2 = checked (MISOData.Count<uint>() - 1);
    int index = 1;
    while (index <= num2)
    {
      uint receivedTransactionCounter = MISOData.ElementAtOrDefault<uint>(index) >> 4 & 3U;
      uint receivedAddress = MISOData.ElementAtOrDefault<uint>(index) >> 8 & (uint) byte.MaxValue;
      if ((int) receivedAddress != (int) expectedAddress)
      {
        SpiAddressException addressException = new SpiAddressException($"ERROR: Expected to receive address 0x{expectedAddress.ToString("X2")} but received 0x{receivedAddress.ToString("X2")}", receivedAddress, expectedAddress);
        if (this.LogExceptions)
          this.m_exceptions.Enqueue((SpiException) addressException);
        if (!this.m_IgnoreAddr)
          throw addressException;
      }
      uint expectedTransactionCounter = checked (num1 + 1U) % 4U;
      if (!((int) receivedTransactionCounter == (int) num1 | (int) receivedTransactionCounter == (int) expectedTransactionCounter))
      {
        TransactionCounterException counterException = new TransactionCounterException("ERROR: Invalid transaction counter sequence received", receivedTransactionCounter, expectedTransactionCounter);
        if (this.LogExceptions)
          this.m_exceptions.Enqueue((SpiException) counterException);
        if (!this.m_IgnoreTC)
          throw counterException;
      }
      num1 = receivedTransactionCounter;
      expectedAddress = MOSIData.ElementAtOrDefault<uint>(index) >> 8 & (uint) byte.MaxValue;
      checked { ++index; }
    }
  }

  /// <summary>Checks the CRC and state vector for an array of MOSI data</summary>
  /// <param name="MISOData">The DUT output data to check</param>
  private void ValidateMISOData(IEnumerable<uint> MISOData)
  {
    if (this.IgnoreExceptions & !this.LogExceptions)
      return;
    if (Information.IsNothing((object) MISOData))
      return;
    try
    {
      foreach (uint SpiWord in MISOData)
      {
        this.ValidateCRC(SpiWord);
        this.ValidateStateVector(SpiWord);
      }
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
  }

  /// <summary>Checks the CRC and state vector for a single 32 bit word of MOSI data</summary>
  /// <param name="MISOData">The DUT output data to check</param>
  private void ValidateMISOData(uint MISOData)
  {
    this.ValidateMISOData((IEnumerable<uint>) new List<uint>()
    {
      MISOData
    });
  }

  /// <summary>
  /// This function validates that the four bit CRC appended to the given SPI word matches the expected CRC. If the CRC does not match
  /// the expected value, an InvalidCRCException is thrown.
  /// </summary>
  /// <param name="SpiWord">The SPI word to check (MISO data)</param>
  private void ValidateCRC(uint SpiWord)
  {
    int expectedCRC = this.CalcCRC28Bit(SpiWord >> 4);
    int invalidCRC = checked ((int) ((long) (SpiWord >> 0) & 15L));
    if (invalidCRC == expectedCRC)
      return;
    InvalidCRCException invalidCrcException = new InvalidCRCException($"ERROR: Invalid SPI CRC in SPI word 0x{SpiWord.ToString("X8")}. CRC received 0x{invalidCRC.ToString("X1")}, expected: 0x{expectedCRC.ToString("X1")}", checked ((uint) invalidCRC), checked ((uint) expectedCRC));
    if (this.LogExceptions)
      this.m_exceptions.Enqueue((SpiException) invalidCrcException);
    if (!this.m_IgnoreCRC)
      throw invalidCrcException;
  }

  /// <summary>
  /// This function validates that the state vector of the given SPI word does not indicate an error condition. If the state vector indicates an
  /// error condition, a StateVectorException is thrown.
  /// </summary>
  /// <param name="SpiWord">The SPI word to check (MISO data)</param>
  private void ValidateStateVector(uint SpiWord)
  {
    StateVectorException Expression = (StateVectorException) null;
    int stateVector = checked ((int) ((long) (SpiWord >> 6) & 3L));
    if (stateVector == 2)
      Expression = new StateVectorException("ERROR: Device not OK flag in SPI word 0x" + SpiWord.ToString("X8"), checked ((uint) stateVector));
    if (stateVector == 3)
      Expression = new StateVectorException("ERROR: SPI error flag in SPI word 0x" + SpiWord.ToString("X8"), checked ((uint) stateVector));
    if (Information.IsNothing((object) Expression))
      return;
    if (this.LogExceptions)
      this.m_exceptions.Enqueue((SpiException) Expression);
    if (!this.m_IgnoreSV)
      throw Expression;
  }

  /// <summary>
  /// Burst mode property. Currently not supported for ADIS16550 (burst implementation not finalized)
  /// </summary>
  /// <returns></returns>
  public ushort BurstMode
  {
    get => 0;
    set
    {
      if (value > (ushort) 0)
        throw new ArgumentException("Burst Mode Limited to Zero for this implementation.");
    }
  }

  /// <summary>
  /// This property controls if stream packets trigger on a data ready or go at full speed. This implementation acts as a wrapper for
  /// the ISpi32Interface implementation of DrActive.
  /// </summary>
  /// <returns></returns>
  public bool DrActive
  {
    get => this.Spi32Interface.DrActive;
    set => this.Spi32Interface.DrActive = value;
  }

  /// <summary>
  /// This property controls the number of seconds which a stream will wait without data before timing out. This implementation
  /// acts as a wrapper for the ISpi32Interface implementation of stream timeout seconds.
  /// </summary>
  /// <returns></returns>
  public int StreamTimeoutSeconds
  {
    get => this.Spi32Interface.StreamTimeoutSeconds;
    set
    {
      this.Spi32Interface.StreamTimeoutSeconds = value > 0 & value <= 100 ? value : throw new ArgumentException($"ERROR: Invalid stream timeout of {value.ToString()} seconds");
    }
  }

  /// <summary>
  /// This is the most general interfacing function. All others call down to this implementation. Performs a blocking stream read/write to the DUT.
  /// </summary>
  /// <param name="addrData">The base list of registers/values to read or write from</param>
  /// <param name="numCaptures">The number of times to iterate through addrData in a single data ready, if DrActive is true</param>
  /// <param name="numBuffers">The total number of sets of the addrData list iterated through numCaptures times to read.</param>
  /// <returns></returns>
  public ushort[] ReadRegArrayStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers)
  {
    if (Information.IsNothing((object) numCaptures) | numCaptures < 1U)
      throw new ArgumentException("ERROR: Invalid number of captures provided");
    if (Information.IsNothing((object) addrData) | addrData.Count<AddrDataPair>() == 0)
      throw new ArgumentException("ERROR: Invalid address data array provided");
    if (Information.IsNothing((object) numBuffers) | numBuffers < 1U)
      throw new ArgumentException("ERROR: Invalid number of buffers provided");
    DataFormatArray dataFormatArray = this.BuildMOSIDataArray(addrData);
    uint[] numArray = this.Spi32Interface.TransferArray((IEnumerable<uint>) dataFormatArray.MOSIData, numCaptures, numBuffers);
    List<uint> MOSIData = new List<uint>();
    uint num1 = checked (numBuffers * numCaptures - 1U);
    uint num2 = 0;
    while (num2 <= num1)
    {
      uint[] mosiData = dataFormatArray.MOSIData;
      int index = 0;
      while (index < mosiData.Length)
      {
        uint num3 = mosiData[index];
        MOSIData.Add(num3);
        checked { ++index; }
      }
      checked { ++num2; }
    }
    this.ValidateMISOData((IEnumerable<uint>) numArray, (IEnumerable<uint>) MOSIData);
    List<ushort> ushortList = new List<ushort>();
    int num4 = checked (((IEnumerable<uint>) numArray).Count<uint>() - 1);
    int index1 = 0;
    while (index1 <= num4)
    {
      int num5 = 0;
      while (num5 < dataFormatArray.NumRepeats[index1 % ((IEnumerable<int>) dataFormatArray.NumRepeats).Count<int>()])
      {
        ushortList.Add(checked ((ushort) ((long) (numArray[index1] >> 16 /*0x10*/) & (long) ushort.MaxValue)));
        checked { ++num5; }
      }
      checked { ++index1; }
    }
    return ushortList.ToArray();
  }

  /// <summary>
  /// Builds a automotive SPI formatted MOSI data list and calls the ISpi32Interface start buffered stream function
  /// </summary>
  /// <param name="addrData"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <param name="timeoutSeconds"></param>
  /// <param name="worker"></param>
  public void StartBufferedStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    this.m_StreamWriteParameters = this.BuildMOSIDataArray(addrData);
    this.m_BufferInfo.MOSILength = ((IEnumerable<uint>) this.m_StreamWriteParameters.MOSIData).Count<uint>();
    this.m_BufferInfo.NumCaptures = numCaptures;
    this.Spi32Interface.StartBufferedStream((IEnumerable<uint>) this.m_StreamWriteParameters.MOSIData, numCaptures, numBuffers, timeoutSeconds, worker);
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
    uint[] numArray = this.BuildMOSIDataArray(addr);
    this.m_BufferInfo.MOSILength = ((IEnumerable<uint>) numArray).Count<uint>();
    this.m_BufferInfo.NumCaptures = numCaptures;
    this.m_StreamWriteParameters = new DataFormatArray();
    this.Spi32Interface.StartBufferedStream((IEnumerable<uint>) numArray, numCaptures, numBuffers, timeoutSeconds, worker);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  public void StartStream(IEnumerable<uint> addr, uint numCaptures)
  {
    uint[] numArray = this.BuildMOSIDataArray(addr);
    this.m_StreamWriteParameters = new DataFormatArray();
    this.m_BufferInfo.MOSILength = ((IEnumerable<uint>) numArray).Count<uint>();
    this.m_BufferInfo.NumCaptures = numCaptures;
    this.Spi32Interface.StartBufferedStream((IEnumerable<uint>) numArray, numCaptures, 1U, this.m_StreamTimeout, (BackgroundWorker) null);
  }

  /// <summary>Asynchronously stops any given stream.</summary>
  public void StopStream() => this.Spi32Interface.StopStream();

  /// <summary>
  /// Writes a list of bytes to the DUT. Bytes must come in sets of two with the same address, LSB first, to maintain compatibility with
  /// the iSensor Automotive SPI protocol.
  /// </summary>
  /// <param name="addrData"></param>
  public void WriteRegByte(IEnumerable<AddrDataPair> addrData)
  {
    List<uint> uintList = new List<uint>();
    List<ushort> ushortList1 = new List<ushort>();
    if (addrData.Count<AddrDataPair>() % 2 != 0)
      throw new Exception("ERROR: The Analog Devices automotive SPI protocol expects bytes to be written in sets of 2, to form a 16 bit register value");
    int num1 = checked (addrData.Count<AddrDataPair>() - 2);
    int index1 = 0;
    while (index1 <= num1)
    {
      if ((int) addrData.ElementAtOrDefault<AddrDataPair>(index1).addr != (int) addrData.ElementAtOrDefault<AddrDataPair>(checked (index1 + 1)).addr)
        throw new Exception("ERROR: The Analog Devices automotive SPI protocol expects bytes to be written in sets of 2, to form a 16 bit register value");
      uintList.Add(addrData.ElementAtOrDefault<AddrDataPair>(index1).addr);
      List<ushort> ushortList2 = ushortList1;
      uint? data = addrData.ElementAtOrDefault<AddrDataPair>(index1).data;
      uint? nullable1 = addrData.ElementAtOrDefault<AddrDataPair>(checked (index1 + 1)).data;
      uint? nullable2 = nullable1.HasValue ? new uint?(nullable1.GetValueOrDefault() << 8) : new uint?();
      uint? nullable3;
      if (!(data.HasValue & nullable2.HasValue))
      {
        nullable1 = new uint?();
        nullable3 = nullable1;
      }
      else
        nullable3 = new uint?(checked (data.GetValueOrDefault() + nullable2.GetValueOrDefault()));
      nullable2 = nullable3;
      int num2 = (int) checked ((ushort) nullable2.Value);
      ushortList2.Add((ushort) num2);
      checked { index1 += 2; }
    }
    List<uint> WriteData = new List<uint>();
    int num3 = checked (uintList.Count - 1);
    int index2 = 0;
    while (index2 <= num3)
    {
      WriteData.Add(this.BuildCommandWord((uint) ushortList1[index2], uintList[index2], true));
      checked { ++index2; }
    }
    this.Spi32Interface.TransferArray((IEnumerable<uint>) WriteData);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  public void WriteRegByte(IEnumerable<uint> addr, IEnumerable<uint> data)
  {
    if (addr.Count<uint>() != data.Count<uint>())
      throw new Exception("ERROR: WriteRegByte must be called with one data word for each address");
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    int num = checked (addr.Count<uint>() - 1);
    int index = 0;
    while (index <= num)
    {
      addrData.Add(new AddrDataPair()
      {
        addr = addr.ElementAtOrDefault<uint>(index),
        data = new uint?(data.ElementAtOrDefault<uint>(index))
      });
      checked { ++index; }
    }
    this.WriteRegByte((IEnumerable<AddrDataPair>) addrData);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public ushort[] GetBufferedStreamDataPacket()
  {
    List<ushort> ushortList = new List<ushort>();
    uint[] streamDataPacket = this.Spi32Interface.GetBufferedStreamDataPacket();
    if (Information.IsNothing((object) streamDataPacket))
      return (ushort[]) null;
    this.ValidateMISOData((IEnumerable<uint>) streamDataPacket);
    if (!Information.IsNothing((object) this.m_StreamWriteParameters.NumRepeats))
    {
      int num1 = checked (((IEnumerable<uint>) streamDataPacket).Count<uint>() - 1);
      int index = 0;
      while (index <= num1)
      {
        int num2 = 0;
        while (num2 < this.m_StreamWriteParameters.NumRepeats[index % ((IEnumerable<int>) this.m_StreamWriteParameters.NumRepeats).Count<int>()])
        {
          ushortList.Add(checked ((ushort) ((long) (streamDataPacket[index] >> 16 /*0x10*/) & (long) ushort.MaxValue)));
          checked { ++num2; }
        }
        checked { ++index; }
      }
    }
    else
    {
      int index = 0;
      uint numCaptures = this.m_BufferInfo.NumCaptures;
      uint num3 = 1;
      while (num3 <= numCaptures)
      {
        checked { ++index; }
        int num4 = checked (this.m_BufferInfo.MOSILength - 1);
        int num5 = 1;
        while (num5 <= num4)
        {
          ushortList.Add(checked ((ushort) (streamDataPacket[index] >> 16 /*0x10*/)));
          checked { ++index; }
          checked { ++num5; }
        }
        checked { ++num3; }
      }
    }
    return ushortList.ToArray();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public ushort[] GetStreamDataPacketU16() => this.GetBufferedStreamDataPacket();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  public ushort[] ReadRegArray(IEnumerable<uint> addr)
  {
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    try
    {
      foreach (uint num in addr)
        addrData.Add(new AddrDataPair()
        {
          addr = num,
          data = new uint?()
        });
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
    return this.ReadRegArrayStream((IEnumerable<AddrDataPair>) addrData, 1U, 1U);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addrData"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  public ushort[] ReadRegArray(IEnumerable<AddrDataPair> addrData, uint numCaptures)
  {
    return this.ReadRegArrayStream(addrData, numCaptures, 1U);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  public ushort[] ReadRegArray(IEnumerable<uint> addr, uint numCaptures)
  {
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    try
    {
      foreach (uint num in addr)
        addrData.Add(new AddrDataPair()
        {
          addr = num,
          data = new uint?()
        });
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
    return this.ReadRegArrayStream((IEnumerable<AddrDataPair>) addrData, numCaptures, 1U);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  public ushort[] ReadRegArrayStream(IEnumerable<uint> addr, uint numCaptures, uint numBuffers)
  {
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    try
    {
      foreach (uint num in addr)
        addrData.Add(new AddrDataPair()
        {
          addr = num,
          data = new uint?()
        });
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
    return this.ReadRegArrayStream((IEnumerable<AddrDataPair>) addrData, numCaptures, numBuffers);
  }

  /// <summary>Reads a single ADIS16550 register word (16 bits)</summary>
  /// <param name="addr">The word address to read from</param>
  /// <returns>The 16 bit data field parsed from the Dut response</returns>
  public ushort ReadRegWord(uint addr)
  {
    return this.ReadRegArrayStream((IEnumerable<AddrDataPair>) new List<AddrDataPair>()
    {
      new AddrDataPair() { addr = addr, data = new uint?() }
    }, 1U, 1U)[0];
  }

  public void WriteRegByte(uint addr, uint data)
  {
    throw new Exception("ERROR: The Analog Devices automotive SPI protocol does not support single byte writes. Please use an array overload of WriteRegByte");
  }

  public void WriteRegByte(AddrDataPair addrData)
  {
    throw new Exception("ERROR: The Analog Devices automotive SPI protocol does not support single byte writes. Please use an array overload of WriteRegByte");
  }

  public ushort ReadRegByte(uint addr)
  {
    throw new Exception("ERROR: The Analog Devices automotive SPI protocol does not support single byte reads. Please use ReadRegWord instead");
  }

  public void WriteRegWord(uint addr, uint data)
  {
    throw new NotImplementedException("This function has been deprecated");
  }

  public void Reset() => throw new NotImplementedException();

  public void Start() => throw new NotImplementedException();
}
