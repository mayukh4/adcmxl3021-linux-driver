// Decompiled with JetBrains decompiler
// Type: AdisApi.SpiFlash
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
/// Class that supports the SPI flash programming functions using SDP hardware.
/// </summary>
public class SpiFlash
{
  /// <summary>
  /// Creates a new instance of the SpiFlash class using the specified SDP base object.
  /// </summary>
  /// <param name="AdisBase"></param>
  public SpiFlash(AdisBase AdisBase)
  {
    this.AdisBase = AdisBase;
    this.SclkFrequency = 1000000U;
  }

  /// <summary>
  /// Gets the AdisBase object associated with thie Spi interface.
  /// </summary>
  public AdisBase AdisBase { get; private set; }

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
  }

  /// <summary>
  /// Returns a count of non-erased bytes read from the device.
  /// </summary>
  /// <returns></returns>
  public uint BlankCheck()
  {
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 8U;
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749589U, configParameters, (uint[]) null, 1, ref numArray);
    Debug.Assert(numArray[0] != uint.MaxValue, "unexpected return value from sdp.");
    return numArray[0];
  }

  /// <summary>
  /// Returns a count of non-erased bytes read from a device sector.
  /// </summary>
  /// <param name="sector">Number of sector to check.</param>
  /// <returns></returns>
  public uint BlankCheck(byte sector)
  {
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 4U;
    configParameters[1] = (uint) sector;
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749589U, configParameters, (uint[]) null, 1, ref numArray);
    Debug.Assert(numArray[0] != uint.MaxValue, "unexpected return value from sdp.");
    return numArray[0];
  }

  /// <summary>
  /// Erases the device and waits for erase operation to complete.
  /// </summary>
  public void Erase()
  {
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 2U;
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749589U, configParameters, (uint[]) null, 1, ref numArray);
    Debug.Assert(numArray[0] == uint.MaxValue, "unexpected return value from sdp.");
  }

  /// <summary>
  /// Erases a sector of the device and waits for erase operation to complete.
  /// </summary>
  /// <param name="sector">Number of the sector to erase.</param>
  public void Erase(byte sector)
  {
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 1U;
    configParameters[1] = (uint) sector;
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749589U, configParameters, (uint[]) null, 1, ref numArray);
    Debug.Assert(numArray[0] == uint.MaxValue, "unexpected return value from sdp.");
  }

  /// <summary>
  /// Erases the device, waits for completion, and returns a count of the number of bytes that were not sucessfully erased.
  /// </summary>
  /// <returns> Returns a count of non-erased bytes read from the device.</returns>
  public uint EraseAndBlankCheck()
  {
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 10U;
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749589U, configParameters, (uint[]) null, 1, ref numArray);
    Debug.Assert(numArray[0] != uint.MaxValue, "unexpected return value from sdp.");
    return numArray[0];
  }

  /// <summary>
  /// Erases a sector, waits for completion, and returns a count of the number of bytes that were not sucessfully erased.
  /// </summary>
  /// <returns> Returns a count of non-erased bytes read from the sector.</returns>
  public uint EraseAndBlankCheck(byte sector)
  {
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 5U;
    configParameters[1] = (uint) sector;
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749589U, configParameters, (uint[]) null, 1, ref numArray);
    Debug.Assert(numArray[0] != uint.MaxValue, "unexpected return value from sdp.");
    return numArray[0];
  }

  /// <summary>
  /// Starts a device erase process, does NOT wait unti process is complete.
  /// </summary>
  public void StartErase()
  {
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 2U;
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749589U, configParameters, (uint[]) null, 0, ref numArray);
  }

  /// <summary>
  /// Starts a sector erase process, does NOT wait unti process is complete.
  /// </summary>
  /// <param name="sector">Number of the sector to erase.</param>
  public void StartErase(byte sector)
  {
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 1U;
    configParameters[1] = (uint) sector;
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749589U, configParameters, (uint[]) null, 0, ref numArray);
  }

  /// <summary>
  /// Returns a crc32 calculated using the specified number of bytes stating at the given sector and address.
  /// </summary>
  /// <param name="sector">Number of sector to start crc comutation from.</param>
  /// <param name="address">Address to start  crc comutation  from.</param>
  /// <param name="numBytes">Number of bytes to process.</param>
  /// <returns></returns>
  public uint ReadCrc(byte sector, ushort address, uint numBytes)
  {
    if (numBytes < 0U)
      throw new ArgumentOutOfRangeException("bytes");
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 3U;
    configParameters[1] = (uint) sector;
    configParameters[2] = (uint) address;
    configParameters[3] = numBytes;
    byte[] numArray;
    this.AdisBase.Base.userCmdU8(4160749587U, configParameters, (byte[]) null, 4, ref numArray);
    return (((uint) numArray[3] << 8 & (uint) numArray[2]) << 8 & (uint) numArray[1]) << 8 & (uint) numArray[0];
  }

  /// <summary>Reads data from the device.</summary>
  /// <param name="sector">Number of sector to start read from.</param>
  /// <param name="address">Address to start reading from.</param>
  /// <param name="numBytes">Number of bytes to read.</param>
  /// <returns></returns>
  public IList<byte> ReadData(byte sector, ushort address, uint numBytes)
  {
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 0U;
    configParameters[1] = (uint) sector;
    configParameters[2] = (uint) address;
    byte[] numArray;
    this.AdisBase.Base.userCmdU8(4160749587U, configParameters, (byte[]) null, (int) numBytes, ref numArray);
    return (IList<byte>) numArray;
  }

  /// <summary>Reads the device identification information.</summary>
  /// <returns>Bytes: [0]=Mfg ID, [1]=Memory Type, [2]=Capacity, [3]=CFD length, [n..4]= CFD cntent.</returns>
  public IList<byte> ReadIdentification()
  {
    int num1 = 512 /*0x0200*/;
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 2U;
    byte[] array;
    this.AdisBase.Base.userCmdU8(4160749587U, configParameters, (byte[]) null, num1, ref array);
    byte num2 = array[3];
    Array.Resize<byte>(ref array, (int) num2 + 4);
    return (IList<byte>) array;
  }

  /// <summary>Reads the 8 bit status register of the device.</summary>
  /// <returns>Value read from status register.</returns>
  public byte ReadStatusRegister() => this.ReadStatusRegister(false);

  /// <summary>Reads the 8 bit status register of the device.</summary>
  /// <returns>Value read from status register.</returns>
  public byte ReadStatusRegister(bool waitForWipClear)
  {
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = waitForWipClear ? 4U : 1U;
    byte[] numArray;
    this.AdisBase.Base.userCmdU8(4160749587U, configParameters, (byte[]) null, 1, ref numArray);
    return numArray[0];
  }

  /// <summary>
  /// Writes data currently stored in SDP SDRAM to the flash.
  /// </summary>
  /// <param name="sector"></param>
  /// <param name="address"></param>
  private void WriteStoredData(byte sector, uint address)
  {
    if ((address & (uint) byte.MaxValue) > 0U)
      throw new ArgumentException("Data address must be at a page boundary.");
    uint[] configParameters = this.CreateDefaultConfigParameters();
    configParameters[0] = 0U;
    configParameters[1] = (uint) sector;
    configParameters[2] = address;
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749588U, configParameters, (uint[]) null, 1, ref numArray);
    Debug.Assert(numArray[0] == 0U, "Unexpected value returned from sdp.");
  }

  /// <summary>
  /// Writes data from named binary file into the Flash starting at the given address.
  /// </summary>
  /// <param name="sector"></param>
  /// <param name="address"></param>
  /// <param name="fileName"></param>
  public void WriteData(byte sector, uint address, string fileName)
  {
    int num = (int) this.AdisBase.StoreData(fileName);
    this.WriteStoredData(sector, address);
  }

  /// <summary>
  /// Writes data from specified buffer into the Flash starting at the given address.
  /// </summary>
  /// <param name="sector"></param>
  /// <param name="address"></param>
  /// <param name="buffer"></param>
  public void WriteData(byte sector, uint address, IEnumerable<byte> buffer)
  {
    int num = (int) this.AdisBase.StoreData((IEnumerable<byte>) buffer.ToArray<byte>());
    this.WriteStoredData(sector, address);
  }

  /// <summary>
  /// Creates a parameter array populated with default values based on instance properties.
  /// </summary>
  private uint[] CreateDefaultConfigParameters()
  {
    uint[] configParameters = new uint[123];
    configParameters[10] = this.SclkFrequency;
    try
    {
      configParameters[11] = this.SclkPin.pinConfig;
      configParameters[12] = this.CsPin.pinConfig;
      configParameters[13] = this.MisoPin.pinConfig;
      configParameters[14] = this.MosiPin.pinConfig;
    }
    catch (NullReferenceException ex)
    {
      throw new Exception("SPI interface pins must be initialized before issuing commands.");
    }
    return configParameters;
  }
}
