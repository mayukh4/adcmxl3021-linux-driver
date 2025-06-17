// Decompiled with JetBrains decompiler
// Type: AdisApi.SpiIce40
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace AdisApi;

/// <summary>
/// 
/// </summary>
public class SpiIce40
{
  /// <summary>
  /// Creates a new instance of the SpiIce40 class using the specified SDP base object.
  /// </summary>
  /// <param name="AdisBase"></param>
  public SpiIce40(AdisBase AdisBase)
  {
    this.AdisBase = AdisBase;
    this.ConfigurationDone = false;
    this.SclkFrequency = 1000000U;
  }

  /// <summary>
  /// Gets the AdisBase object associated with thie Spi interface.
  /// </summary>
  public AdisBase AdisBase { get; private set; }

  /// <summary>
  /// pinObject that specifies the configuration of the FPGA SPI_SCK pin.
  /// </summary>
  public PinObject SclkPin { get; set; }

  /// <summary>
  /// pinObject that specifies the configuration of the FPGA SPI_SI pin.
  /// </summary>
  public PinObject MosiPin { get; set; }

  /// <summary>
  /// pinObject that specifies the configuration of the FPGA SPI_SS_B pin.
  /// </summary>
  public PinObject CsPin { get; set; }

  /// <summary>
  /// pinObject that specifies the configuration of the FPGA CRESET_B pin.
  /// </summary>
  public PinObject ResetPin { get; set; }

  /// <summary>
  /// pinObject that specifies the configuration of the FPGA CDONE pin.
  /// </summary>
  public PinObject ReadyPin { get; set; }

  /// <summary>
  /// Returns True if the CDONE pin was high after the previous FPGA configuration attempt.
  /// </summary>
  public bool ConfigurationDone { get; private set; }

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
    this.MosiPin = map.MosiPin;
    this.CsPin = map.CsPin;
    this.ReadyPin = map.ReadyPin;
    this.ResetPin = map.ResetPin;
  }

  /// <summary>
  /// Configures the CPLD using data from file with the specified name.
  /// </summary>
  /// <param name="fileName">Binary file containing configuration data.</param>
  public void Configure(string fileName)
  {
    int num = (int) this.AdisBase.StoreData(fileName);
    this.ConfigWithStoredData();
  }

  /// <summary>Configures the CPLD using data from specified buffer.</summary>
  /// <param name="buffer">IEnumerable of bytes containing configuration data.</param>
  public void Configure(IEnumerable<byte> buffer)
  {
    int num = (int) this.AdisBase.StoreData((IEnumerable<byte>) buffer.ToArray<byte>());
    this.ConfigWithStoredData();
  }

  /// <summary>
  /// Configures the CPLD using data currently stored in SDP SDRAM to the flash.
  /// </summary>
  private void ConfigWithStoredData()
  {
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749590U, this.CreateConfigParameters(), (uint[]) null, 1, ref numArray);
    this.ConfigurationDone = numArray[0] == 1U;
  }

  /// <summary>
  /// Creates a parameter array populated with default values based on instance properties.
  /// </summary>
  private uint[] CreateConfigParameters()
  {
    uint[] configParameters = new uint[123];
    configParameters[10] = this.SclkFrequency;
    try
    {
      configParameters[11] = this.SclkPin.pinConfig;
      configParameters[12] = this.CsPin.pinConfig;
      configParameters[14] = this.MosiPin.pinConfig;
      configParameters[15] = this.ResetPin.pinConfig;
      configParameters[16 /*0x10*/] = this.ReadyPin.pinConfig;
    }
    catch (NullReferenceException ex)
    {
      throw new Exception("SPI interface pins must be initialized before issuing commands.");
    }
    return configParameters;
  }
}
