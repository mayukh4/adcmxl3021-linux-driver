// Decompiled with JetBrains decompiler
// Type: AdisApi.Spi
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using sdpApi1;
using System;

#nullable disable
namespace AdisApi;

/// <summary>
/// Basic SPI configuration, reads, and writes using ADiS spi code on Blackfin.
/// </summary>
public class Spi : ISpiInterface
{
  private AdisBase adisBase;
  private SdpConnector connector;
  private bool cpha;
  private bool cpol;
  private int sclkFrequency;
  private SpiSel slaveSelect;
  private int wordSize;
  private int stallCycles;
  private uint csTrigger;
  private uint drConfig;
  private const uint drPolMask = 512 /*0x0200*/;
  private const uint drActMask = 256 /*0x0100*/;
  private const uint drPinMask = 15;

  /// <summary>Initializes a new instance of the Spi class.</summary>
  /// <param name="adisBase">AdisBase object for adp board to use.</param>
  /// <param name="sdpConn">SDP Connector being used.</param>
  /// <param name="slaveSelect">SDP slave select line to use.</param>
  /// <param name="wordSize">SPI Word size in bits (number of SCLKs).</param>
  /// <param name="cpha">SPI Clock Phase setting to use.</param>
  /// <param name="cpol">SPI Clock polarity setting to use.</param>
  /// <param name="sclkFrequency">SPI clock frequency in Hz.</param>
  public Spi(
    AdisBase adisBase,
    int sdpConn,
    int slaveSelect,
    int wordSize,
    bool cpha,
    bool cpol,
    int sclkFrequency)
  {
    this.adisBase = adisBase;
    this.Cpha = cpha;
    this.Cpol = cpol;
    this.Connector = (SdpConnector) sdpConn;
    this.SclkFrequency = sclkFrequency;
    this.SlaveSelect = (SpiSel) slaveSelect;
    this.WordSize = wordSize;
    this.StallCycles = 1;
    this.drConfig = 0U;
    this.CsTrigger = 0U;
  }

  /// <summary>Initializes a new instance of the Spi class.</summary>
  /// <param name="adisBase">AdisBase object for adp board to use.</param>
  /// <param name="sdpConn">SDP Connector being used.</param>
  /// <param name="slaveSelect">SDP slave select line to use.</param>
  /// <param name="wordSize">SPI Word size in bits (number of SCLKs).</param>
  /// <param name="cpha">SPI Clock Phase setting to use.</param>
  /// <param name="cpol">SPI Clock polarity setting to use.</param>
  /// <param name="sclkFrequency">SPI clock frequency in Hz.</param>
  public Spi(
    AdisBase adisBase,
    SdpConnector sdpConn,
    SpiSel slaveSelect,
    int wordSize,
    bool cpha,
    bool cpol,
    int sclkFrequency)
  {
    this.adisBase = adisBase;
    this.Cpha = cpha;
    this.Cpol = cpol;
    this.Connector = sdpConn;
    this.SclkFrequency = sclkFrequency;
    this.SlaveSelect = slaveSelect;
    this.WordSize = wordSize;
    this.StallCycles = 1;
    this.drConfig = 0U;
    this.CsTrigger = 0U;
  }

  /// <summary>
  /// Gets the AdisBase object associated with thie Spi interface.
  /// </summary>
  public AdisBase AdisBase => this.adisBase;

  /// <summary>
  /// Gets or sets a value indicating whether the Clock Phase is 0 or 1.
  /// </summary>
  public bool Cpha
  {
    get => this.cpha;
    set => this.cpha = value;
  }

  /// <summary>
  /// Gets or sets a value indicating whether the Clock Polarity is 0 or 1.
  /// </summary>
  public bool Cpol
  {
    get => this.cpol;
    set => this.cpol = value;
  }

  /// <summary>
  /// Gets or sets the SDP connector to use for this SPI interface.
  /// </summary>
  public SdpConnector Connector
  {
    get => this.connector;
    set => this.connector = value;
  }

  /// <summary>
  /// Gets or sets the SPI Clock Frequency (in Hz) to use for this SPI interface.
  /// </summary>
  public int SclkFrequency
  {
    get => this.sclkFrequency;
    set
    {
      this.sclkFrequency = value >= 1 ? value : throw new ArgumentOutOfRangeException("Spi Sclk Frequency must be positive.");
    }
  }

  /// <summary>
  /// Gets or sets the Slave Select line to use for this SPI interface.
  /// </summary>
  public SpiSel SlaveSelect
  {
    get => this.slaveSelect;
    set => this.slaveSelect = value;
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
  /// Gets or sets the word size (number of SCLS per read/write) for this SPI interface.
  /// </summary>
  public int WordSize
  {
    get => this.wordSize;
    set
    {
      this.wordSize = value >= 1 && value <= 32 /*0x20*/ ? value : throw new ArgumentOutOfRangeException("Spi Word Size must be in the range 1 - 32.");
    }
  }

  /// <summary>
  /// Gets the SDP Base object for this interface, use as shorthand for AdisBase.Base.
  /// </summary>
  public SdpBase SdpBase => this.adisBase.Base;

  /// <summary>Gets or sets the data ready pin (PH0 - PH15)</summary>
  public uint DrPin
  {
    get => this.drConfig & 15U;
    set
    {
      if (value > 15U)
        throw new Exception("Pin must be in the range 0 to 15.");
      this.drConfig &= 4294967280U;
      this.drConfig |= value & 15U;
    }
  }

  /// <summary>Gets or sets the data ready active bit</summary>
  public bool DrActive
  {
    get => ((int) this.drConfig & 256 /*0x0100*/) == 256 /*0x0100*/;
    set
    {
      if (value)
        this.drConfig |= 256U /*0x0100*/;
      else
        this.drConfig &= 4294967039U;
    }
  }

  /// <summary>Gets or sets the data ready polarity</summary>
  public bool DrPolarity
  {
    get => ((int) this.drConfig & 512 /*0x0200*/) == 512 /*0x0200*/;
    set
    {
      if (value)
        this.drConfig |= 512U /*0x0200*/;
      else
        this.drConfig &= 4294966783U;
    }
  }

  /// <summary>
  /// Byte 0 = 2 means set din low before sclk toggle, byte 0 = 1 means set din high before sclk toggle; byte 1 = 1 means cs high during sclk cycle, byte 2 = 0 means cs low during sclk cycle
  /// All 0's mean cs low during sclk cycle without setting din high or low before cs and sclk cycle toggles(this mode is not activated)
  /// </summary>
  public uint CsTrigger
  {
    get => this.csTrigger;
    set => this.csTrigger = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  public void WriteWordU8(byte writeData)
  {
    if (this.wordSize < 1 || this.wordSize > 8)
      throw new Exception("SPI word size must be between 1 and 8 to use WriteWordU8.");
    byte[] numArray1 = new byte[0];
    uint[] header = new uint[10];
    this.insertConfigParameters(ref header);
    byte[] numArray2 = new byte[1]{ writeData };
    this.SdpBase.userCmdU8(4160749576U /*0xF8000008*/, header, numArray2, 0, ref numArray1);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public byte ReadWordU8() => this.ReadWordU8(byte.MaxValue);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="mosiState"></param>
  /// <returns></returns>
  public byte ReadWordU8(bool mosiState) => this.ReadWordU8(mosiState ? byte.MaxValue : (byte) 0);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <returns></returns>
  public byte ReadWordU8(byte writeData) => this.ReadArrayU8(1, writeData)[0];

  /// <summary>
  /// 
  /// </summary>
  /// <param name="readLength"></param>
  /// <param name="writeData"></param>
  /// <returns></returns>
  public byte[] ReadArrayU8(int readLength, byte writeData)
  {
    if (this.wordSize < 1 || this.wordSize > 8)
      throw new Exception("SPI word size must be between 1 and 8 to use U8 SPI routines.");
    uint[] header = new uint[11];
    this.insertConfigParameters(ref header);
    header[10] = (uint) writeData;
    byte[] numArray = new byte[readLength];
    this.SdpBase.userCmdU8(4160749576U /*0xF8000008*/, header, new byte[0], readLength, ref numArray);
    return numArray;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  public byte[] WriteReadArrayU8(byte[] writeData, uint numCaptures)
  {
    if (this.wordSize < 1 || this.wordSize > 8)
      throw new Exception("SPI word size must be between 1 and 8 to use U8 SPI routines.");
    int length = (int) ((long) writeData.Length * (long) numCaptures);
    byte[] numArray = new byte[length];
    uint[] header = new uint[12];
    this.insertConfigParameters(ref header);
    this.SdpBase.userCmdU8(4160749576U /*0xF8000008*/, header, writeData, length, ref numArray);
    return numArray;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <returns></returns>
  public byte[] WriteReadArrayU8(byte[] writeData) => this.WriteReadArrayU8(writeData, 1U);

  /// <summary>Write one unsigned 16 bit word through SPI interface</summary>
  /// <param name="writeData">Data word to write.</param>
  public void WriteWordU16(ushort writeData)
  {
    if (this.wordSize < 9 || this.wordSize > 16 /*0x10*/)
      throw new Exception("SPI word size must be between 9 and 16 to use WriteWordU16.");
    uint[] header = new uint[10];
    this.insertConfigParameters(ref header);
    ushort[] numArray1 = new ushort[1]{ writeData };
    ushort[] numArray2;
    this.SdpBase.userCmdU16(4160749576U /*0xF8000008*/, header, numArray1, 0, ref numArray2);
  }

  /// <summary>
  /// Reads one 16 bit word from spi interface into a ushort.
  /// </summary>
  /// <returns>Word read from SPI interface.</returns>
  public ushort ReadWordU16() => this.ReadWordU16(ushort.MaxValue);

  /// <summary>
  /// Reads one 16 bit word from spi interface into a ushort.
  /// </summary>
  /// <returns>Word read from SPI interface.</returns>
  public ushort ReadWordU16(bool mosiState)
  {
    return this.ReadWordU16(mosiState ? ushort.MaxValue : (ushort) 0);
  }

  /// <summary>
  /// Reads one unsigned 16 bit word from spi interface into a ushort.
  /// </summary>
  /// <param name="writeData">Data word to be sent during the read operation.</param>
  /// <returns>Word read from SPI interface.</returns>
  public ushort ReadWordU16(ushort writeData) => this.ReadArrayU16(1, writeData)[0];

  /// <summary>
  /// Reads an array of 16 bit words from spi interface in to an ushort array.
  /// </summary>
  /// <param name="numCaptures">Number of words to read.</param>
  /// <param name="writeData">Data words to be sent during each read operations.</param>
  /// <returns>Words read from SPI interface.</returns>
  public ushort[] WriteReadArrayU16(ushort[] writeData, uint numCaptures)
  {
    if (this.wordSize < 9 || this.wordSize > 16 /*0x10*/)
      throw new Exception("SPI word size must be between 9 and 16 to use U16 SPI routines.");
    int length = (int) ((long) writeData.Length * (long) numCaptures);
    ushort[] numArray = new ushort[length];
    uint[] header = new uint[12];
    this.insertConfigParameters(ref header);
    this.SdpBase.userCmdU16(4160749576U /*0xF8000008*/, header, writeData, length, ref numArray);
    return numArray;
  }

  /// <summary>
  /// Reads an array of 16 bit words from spi interface in to an ushort array the same length as data.
  /// </summary>
  /// <param name="writeData">Data words to be sent during each read operations.</param>
  /// <returns>Words read from SPI interface.</returns>
  public ushort[] WriteReadArrayU16(ushort[] writeData) => this.WriteReadArrayU16(writeData, 1U);

  /// <summary>
  /// Reads an array of 16 bit words from spi interface in to an ushort array.
  /// </summary>
  /// <param name="readLength">Number of words to read.</param>
  /// <param name="writeData">Data word to be sent during each read operation.</param>
  /// <returns>Words read from SPI interface.</returns>
  public ushort[] ReadArrayU16(int readLength, ushort writeData)
  {
    if (this.wordSize < 9 || this.wordSize > 16 /*0x10*/)
      throw new Exception("SPI word size must be between 9 and 16 to use U16 SPI routines.");
    uint[] header = new uint[11];
    this.insertConfigParameters(ref header);
    header[10] = (uint) writeData;
    ushort[] numArray = new ushort[readLength];
    this.SdpBase.userCmdU16(4160749576U /*0xF8000008*/, header, new ushort[0], readLength, ref numArray);
    return numArray;
  }

  /// <summary>Write one unsigned32 bit word through SPI interface</summary>
  /// <param name="writeData">Data word to write.</param>
  public void WriteWordU32(uint writeData)
  {
    if (this.wordSize < 17 || this.wordSize > 32 /*0x20*/)
      throw new Exception("SPI word size must be between 17 and 32 to use WriteWordU32.");
    uint[] header = new uint[10];
    this.insertConfigParameters(ref header);
    uint[] numArray1 = new uint[1]{ writeData };
    uint[] numArray2;
    this.SdpBase.userCmdU32(4160749576U /*0xF8000008*/, header, numArray1, 0, ref numArray2);
  }

  /// <summary>
  /// Reads an array of 32 bit words from spi interface in to an uint array.
  /// </summary>
  /// <param name="readLength">Number of words to read.</param>
  /// <param name="writeData">Data word to be sent during each read operation.</param>
  /// <returns>Words read from SPI interface.</returns>
  public uint[] ReadArrayU32(int readLength, uint writeData)
  {
    if (this.wordSize < 17 || this.wordSize > 32 /*0x20*/)
      throw new Exception("SPI word size must be between 17 and 32 to use U32 SPI routines.");
    uint[] header = new uint[11];
    this.insertConfigParameters(ref header);
    header[10] = writeData;
    uint[] numArray = new uint[readLength];
    this.SdpBase.userCmdU32(4160749576U /*0xF8000008*/, header, (uint[]) null, readLength, ref numArray);
    return numArray;
  }

  /// <summary>Reads one 32 bit word from spi interface into a uint.</summary>
  /// <returns>Word read from SPI interface.</returns>
  public uint ReadWordU32() => this.ReadWordU32(uint.MaxValue);

  /// <summary>Reads one 32 bit word from spi interface into a uint.</summary>
  /// <returns>Word read from SPI interface.</returns>
  public uint ReadWordU32(bool mosiState) => this.ReadWordU32(mosiState ? uint.MaxValue : 0U);

  /// <summary>Reads one 32 bit word from spi interface into a uint.</summary>
  /// <param name="writeData">Data word to be sent during the read operation.</param>
  /// <returns>Word read from SPI interface.</returns>
  public uint ReadWordU32(uint writeData) => this.ReadArrayU32(1, writeData)[0];

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  public uint[] WriteReadArrayU32(uint[] writeData, uint numCaptures)
  {
    if (this.wordSize < 16 /*0x10*/ || this.wordSize > 32 /*0x20*/)
      throw new Exception("SPI word size must be between 16 and 32 to use U32 SPI routines.");
    int num = (int) ((long) writeData.Length * (long) numCaptures);
    uint[] header = new uint[12];
    this.insertConfigParameters(ref header);
    uint[] numArray;
    this.SdpBase.userCmdU32(4160749576U /*0xF8000008*/, header, writeData, num, ref numArray);
    return numArray;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <returns></returns>
  public uint[] WriteReadArrayU32(uint[] writeData) => this.WriteReadArrayU32(writeData, 1U);

  /// <summary>
  /// 
  /// </summary>
  public void StopStream()
  {
    throw new NotSupportedException("Streaming not supported on bit-bang Spi Interface");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <param name="numCaptures"></param>
  public void StreamFromU16(ushort[] writeData, uint numCaptures)
  {
    throw new NotSupportedException("Streaming not supported on bit-bang Spi Interface");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public ushort[] GetStreamDataPacketU16()
  {
    throw new NotSupportedException("Streaming not supported on bit-bang Spi Interface");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  public ushort[] WriteReadStreamU16(ushort[] writeData, uint numCaptures, uint numBuffers)
  {
    throw new NotSupportedException("Streaming not supported on bit-bang Spi Interface");
  }

  /// <summary>
  /// Inserts parameters for configuration into header based on instance properties.
  /// </summary>
  /// <param name="header"></param>
  private void insertConfigParameters(ref uint[] header)
  {
    header[0] = (uint) this.sclkFrequency;
    header[1] = this.SpiSelParam();
    header[2] = (uint) this.WordSize;
    header[3] = this.drConfig;
    header[4] = this.Cpha ? 1U : 0U;
    header[5] = this.Cpol ? 1U : 0U;
    header[7] = 0U;
    header[8] = 0U;
    header[9] = (uint) this.StallCycles;
    header[6] = this.CsTrigger;
  }

  /// <summary>
  /// Returns the parameter to pass to Blackfin for spi select, based on connector and select line
  /// </summary>
  /// <returns>Parameter for spi select.</returns>
  private uint SpiSelParam()
  {
    uint num = 0;
    SdpConnector connector = this.Connector;
    if (connector != null)
    {
      if (connector == 1)
      {
        switch ((int) this.SlaveSelect)
        {
          case 0:
            num = 32U /*0x20*/;
            break;
          case 1:
            num = 128U /*0x80*/;
            break;
          case 2:
            num = 8U;
            break;
          case 3:
            num = 2U;
            break;
        }
      }
    }
    else
    {
      switch ((int) this.SlaveSelect)
      {
        case 0:
          num = 16U /*0x10*/;
          break;
        case 1:
          num = 64U /*0x40*/;
          break;
        case 2:
          num = 4U;
          break;
        case 3:
          num = 2U;
          break;
      }
    }
    return num;
  }
}
