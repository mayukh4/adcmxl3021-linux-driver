// Decompiled with JetBrains decompiler
// Type: AdisApi.SpiCortexM4
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace AdisApi;

/// <summary>
/// 
/// </summary>
public class SpiCortexM4
{
  /// <summary>
  /// Creates a new instance of the SpiCortexM4 class using the specified SDP base object.
  /// </summary>
  /// <param name="AdisBase"></param>
  public SpiCortexM4(AdisBase AdisBase)
  {
    this.AdisBase = AdisBase;
    this.BootCompleted = false;
    this.SclkFrequency = 1000000U;
  }

  /// <summary>
  /// Gets the AdisBase object associated with thie Spi interface.
  /// </summary>
  public AdisBase AdisBase { get; private set; }

  /// <summary>
  /// pinObject that specifies the configuration of the target SPI_CLK pin.
  /// </summary>
  public PinObject SclkPin { get; set; }

  /// <summary>
  /// pinObject that specifies the configuration of the target SPI_MISO pin.
  /// </summary>
  public PinObject MisoPin { get; set; }

  /// <summary>
  /// pinObject that specifies the configuration of the target SPI_MOSI pin.
  /// </summary>
  public PinObject MosiPin { get; set; }

  /// <summary>
  /// pinObject that specifies the configuration of the target SPI_SEL pin.
  /// </summary>
  public PinObject CsPin { get; set; }

  /// <summary>
  /// pinObject that specifies the configuration of the target SYS_HWRESET pin.
  /// </summary>
  public PinObject ResetPin { get; set; }

  /// <summary>
  /// pinObject that specifies the configuration of the target SPI_RDY pin.
  /// </summary>
  public PinObject ReadyPin { get; set; }

  /// <summary>Returns True if the boot was sucessful.</summary>
  public bool BootCompleted { get; private set; }

  /// <summary>
  /// Gets or sets the SPI Clock Frequency (in Hz) to use for this SPI interface.
  /// </summary>
  public uint SclkFrequency { get; set; }

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
  /// Initiaies a SPI Slave Boot of the target using data from named binary file.
  /// </summary>
  /// <param name="fileName">Binary file containing boot loader stream data.</param>
  public void Boot(string fileName)
  {
    this.BootCompleted = false;
    int num = (int) this.AdisBase.StoreData(fileName);
    this.BootWithStoredData();
  }

  /// <summary>
  /// Initiaies a SPI Slave Boot of the target using data from named binary file.
  /// </summary>
  /// <param name="buffer">IEnumerable of bytes containing boot loader stream data.</param>
  public void Boot(IEnumerable<byte> buffer)
  {
    this.BootCompleted = false;
    int num = (int) this.AdisBase.StoreData((IEnumerable<byte>) buffer.ToArray<byte>());
    this.BootWithStoredData();
  }

  /// <summary>
  /// Writes data currently stored in SDP SDRAM to the flash.
  /// </summary>
  public void HardwareReset()
  {
    byte[] numArray;
    this.AdisBase.Base.userCmdU8(4160749591U, this.CreateConfigParameters(SpiCortexM4.CortexCommand.HardwareReset, 0U, 0U), (byte[]) null, 0, ref numArray);
  }

  /// <summary>Reads data from the device.</summary>
  /// <param name="address">Address to start reading from.</param>
  /// <param name="numBytes">Number of bytes to read.</param>
  /// <returns></returns>
  public IList<byte> ReadData(uint address, uint numBytes)
  {
    byte[] numArray;
    this.AdisBase.Base.userCmdU8(4160749587U, this.CreateConfigParameters(SpiCortexM4.CortexCommand.Read, address, 0U), (byte[]) null, (int) numBytes, ref numArray);
    return (IList<byte>) numArray;
  }

  /// <summary>
  /// Writes data from named binary file into the Flash starting at the given address.
  /// </summary>
  /// <param name="address"></param>
  /// <param name="fileName"></param>
  public void WriteData(uint address, string fileName)
  {
    int num = (int) this.AdisBase.StoreData(fileName);
    this.WriteStoredData(address);
  }

  /// <summary>
  /// Writes data from specified buffer into the Flash starting at the given address.
  /// </summary>
  /// <param name="address"></param>
  /// <param name="buffer"></param>
  public void WriteData(uint address, IEnumerable<byte> buffer)
  {
    int num = (int) this.AdisBase.StoreData((IEnumerable<byte>) buffer.ToArray<byte>());
    this.WriteStoredData(address);
  }

  /// <summary>Returns true if all bytes in flash are blank.</summary>
  /// <returns></returns>
  public bool IsBlank()
  {
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749591U, this.CreateConfigParameters(SpiCortexM4.CortexCommand.BlankCheckAll, 0U, 0U), (uint[]) null, 1, ref numArray);
    return numArray[0] == 0U;
  }

  /// <summary>
  /// Returns true is all bytes in the sector containing address are blank.
  /// </summary>
  /// <returns></returns>
  public bool IsBlank(uint address) => this.IsBlank(address, address);

  /// <summary>
  /// Returns true is all bytes in the sector containing startAddress through the sector containing stopAddress are blank
  /// </summary>
  /// <returns></returns>
  public bool IsBlank(uint startAddress, uint stopAddress)
  {
    if (stopAddress < startAddress)
      throw new ArgumentException("stop Address must be greater or equal to start Address.");
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749589U, this.CreateConfigParameters(SpiCortexM4.CortexCommand.BlankCheck, startAddress, stopAddress), (uint[]) null, 1, ref numArray);
    Debug.Assert(((int) numArray[0] | -2) == 0, "unexpected return value from sdp.");
    return numArray[0] == 0U;
  }

  /// <summary>
  /// Erases the device and waits for erase operation to complete.
  /// </summary>
  public void Erase()
  {
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749589U, this.CreateConfigParameters(SpiCortexM4.CortexCommand.EraseAll, 0U, 0U), (uint[]) null, 1, ref numArray);
    Debug.Assert(((int) numArray[0] | -2) == 0, "unexpected return value from sdp.");
  }

  /// <summary>
  /// Erases a sector of the device and waits for erase operation to complete.
  /// </summary>
  /// <param name="startAddress">Number of the sector to erase.</param>
  /// 
  ///             /// <param name="stopAddress">Number of the sector to erase.</param>
  public void Erase(uint startAddress, uint stopAddress)
  {
    if (stopAddress < startAddress)
      throw new ArgumentException("stop Address must be greater or equal to start Address.");
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749589U, this.CreateConfigParameters(SpiCortexM4.CortexCommand.Erase, startAddress, stopAddress), (uint[]) null, 1, ref numArray);
    Debug.Assert(numArray[0] == uint.MaxValue, "unexpected return value from sdp.");
  }

  /// <summary>
  /// Writes data currently stored in SDP SDRAM to the flash.
  /// </summary>
  private void BootWithStoredData()
  {
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749591U, this.CreateConfigParameters(SpiCortexM4.CortexCommand.Boot, 0U, 0U), (uint[]) null, 1, ref numArray);
    this.BootCompleted = numArray[0] == 0U;
  }

  /// <summary>
  /// Writes data currently stored in SDP SDRAM to the flash.
  /// </summary>
  private void WriteStoredData(uint addr)
  {
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749591U, this.CreateConfigParameters(SpiCortexM4.CortexCommand.Flash, addr, 0U), (uint[]) null, 1, ref numArray);
  }

  /// <summary>
  /// Creates a parameter array populated using parameters and instance properties.
  /// </summary>
  private uint[] CreateConfigParameters(SpiCortexM4.CortexCommand command, uint p1, uint p2)
  {
    uint[] configParameters = new uint[20];
    configParameters[0] = (uint) command;
    configParameters[1] = p1;
    configParameters[2] = p2;
    configParameters[10] = this.SclkFrequency;
    try
    {
      configParameters[11] = this.SclkPin.pinConfig;
      configParameters[12] = this.CsPin.pinConfig;
      configParameters[13] = this.MisoPin.pinConfig;
      configParameters[14] = this.MosiPin.pinConfig;
      configParameters[15] = this.ResetPin.pinConfig;
      configParameters[16 /*0x10*/] = this.ReadyPin.pinConfig;
    }
    catch (NullReferenceException ex)
    {
      throw new Exception("SPI interface pins must be initialized before issuing commands.", (Exception) ex);
    }
    return configParameters;
  }

  private enum CortexCommand : uint
  {
    Boot,
    Flash,
    Read,
    HardwareReset,
    Erase,
    EraseAll,
    BlankCheck,
    BlankCheckAll,
  }
}
