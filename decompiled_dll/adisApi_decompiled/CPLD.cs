// Decompiled with JetBrains decompiler
// Type: AdisApi.CPLD
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System.Collections.Generic;

#nullable disable
namespace AdisApi;

/// <summary>
/// Basic SPI configuration, reads, and writes using ADiS spi code on Blackfin.
/// </summary>
public class CPLD
{
  /// <summary>
  /// Creates a new instance of the CPLD class using the specified SDP base object.
  /// </summary>
  /// <param name="AdisBase"></param>
  public CPLD(AdisBase AdisBase)
  {
    this.AdisBase = AdisBase;
    this.Filename = "";
  }

  /// <summary>
  /// Gets the AdisBase object associated with the Spi interface.
  /// </summary>
  public AdisBase AdisBase { get; set; }

  /// <summary>
  /// PinObject that describes the SDP Blackfin gpio pin connected CPLD TCK pin.
  /// </summary>
  public PinObject TckPin { get; set; }

  /// <summary>
  /// PinObject that describes the SDP Blackfin gpio pin connected CPLD TMS pin.
  /// </summary>
  public PinObject TmsPin { get; set; }

  /// <summary>
  /// PinObject that describes the SDP Blackfin gpio pin connected CPLD TDI pin.
  /// </summary>
  public PinObject TdiPin { get; set; }

  /// <summary>
  /// PinObject that describes the SDP Blackfin gpio pin connected CPLD TDO pin.
  /// </summary>
  public PinObject TdoPin { get; set; }

  /// <summary>
  /// File name that will be used for programming with next ProgramCpld() call.
  /// </summary>
  public string Filename { get; set; }

  /// <summary>
  /// Fills the TckPin, TmsPin, TdiPin, and TdoPin properties from the given pin map.
  /// </summary>
  /// <param name="map"></param>
  public void initializePins(PinMapObject map)
  {
    this.TckPin = map.TckPin;
    this.TmsPin = map.TmsPin;
    this.TdiPin = map.TdiPin;
    this.TdoPin = map.TdoPin;
  }

  /// <summary>Loads the XSVF file onto the SDP board SDRAM.</summary>
  /// <returns></returns>
  public void StoreXsvf()
  {
    int num = (int) this.AdisBase.StoreData(this.Filename);
  }

  /// <summary>Loads the XSVF file onto the SDP board SDRAM.</summary>
  /// <returns></returns>
  public void StoreXsvf(IEnumerable<byte> bData)
  {
    int num = (int) this.AdisBase.StoreData(bData);
  }

  /// <summary>
  /// Sets the Filename property and loads the specified XSVF file onto the SDP board SDRAM.
  /// </summary>
  /// <param name="filename">Path to XSVF file.</param>
  public void StoreXsvf(string filename)
  {
    this.Filename = filename;
    this.StoreXsvf();
  }

  /// <summary>
  /// <para>Plays an XSVF file image from the SDP card SDRAM to a Xilinx CPLD.</para><br />
  /// <para>See Object Browser for Error Retrun Codes.</para><br />
  /// </summary>
  /// <param name="TCKpinConfig">Test Clock</param>
  /// <param name="TMSpinConfig">Test Mode Select</param>
  /// <param name="TDIpinConfig">Test Data Input</param>
  /// <param name="TDOpinConfig">Test Data Output</param>
  /// <returns>
  /// <para>0: Success.</para><br />
  /// <para>1: Unknown error. </para><br />
  /// <para>2: TDO mismatch.</para><br />
  /// <para>3: TDO mismatch after max retries.</para><br />
  /// <para>4: Illegal command in XSVF file.</para><br />
  /// <para>5: Illegal state.</para><br />
  /// <para>6: Data overflow.</para><br />
  /// </returns>
  protected int PlayXsvf(
    uint TCKpinConfig,
    uint TMSpinConfig,
    uint TDIpinConfig,
    uint TDOpinConfig)
  {
    byte[] numArray;
    this.AdisBase.Base.userCmdU8(4160749581U /*0xF800000D*/, new uint[4]
    {
      TCKpinConfig,
      TMSpinConfig,
      TDIpinConfig,
      TDOpinConfig
    }, (byte[]) null, 1, ref numArray);
    return (int) numArray[0];
  }

  /// <summary>
  /// <para>Plays an XSVF file image from the SDP card SDRAM to a Xilinx CPLD.</para><br />
  /// <para>See Object Browser for Error Retrun Codes.</para><br />
  /// </summary>
  /// <returns>
  /// <para>0: Success.</para><br />
  /// <para>1: Unknown error. </para><br />
  /// <para>2: TDO mismatch.</para><br />
  /// <para>3: TDO mismatch after max retries.</para><br />
  /// <para>4: Illegal command in XSVF file.</para><br />
  /// <para>5: Illegal state.</para><br />
  /// <para>6: Data overflow.</para><br />
  /// </returns>
  public int PlayXsvf()
  {
    return this.PlayXsvf(this.TckPin.pinConfig, this.TmsPin.pinConfig, this.TdiPin.pinConfig, this.TdoPin.pinConfig);
  }

  /// <summary>
  /// <para>Transfer specified in Filename property to SDP, and programs CPLD with transferred file.</para><br />
  /// <para>See Object Browser for Error Retrun Codes.</para><br />
  /// </summary>
  /// <returns>
  /// <para>0: Success.</para><br />
  /// <para>1: Unknown error. </para><br />
  /// <para>2: TDO mismatch.</para><br />
  /// <para>3: TDO mismatch after max retries.</para><br />
  /// <para>4: Illegal command in XSVF file.</para><br />
  /// <para>5: Illegal state.</para><br />
  /// <para>6: Data overflow.</para><br />
  /// </returns>
  public int ProgramCpld()
  {
    this.StoreXsvf(this.Filename);
    return this.PlayXsvf();
  }

  /// <summary>
  /// <para>Sets CPLD Filename property, transfers file to SDP, and programs CPLD with transferred file.</para><br />
  /// <para>See Object Browser for Error Retrun Codes.</para><br />
  /// </summary>
  /// <param name="filename">Location and filename of XSVF to program CPLD with</param>
  /// <returns>
  /// <para>0: Success.</para><br />
  /// <para>1: Unknown error. </para><br />
  /// <para>2: TDO mismatch.</para><br />
  /// <para>3: TDO mismatch after max retries.</para><br />
  /// <para>4: Illegal command in XSVF file.</para><br />
  /// <para>5: Illegal state.</para><br />
  /// <para>6: Data overflow.</para><br />
  /// </returns>
  public int ProgramCpld(string filename)
  {
    this.Filename = filename;
    return this.ProgramCpld();
  }

  /// <summary>
  /// <para>Stores bytes to SDP then Plays code from SDP to CPLD.</para>
  /// <para>Set these properties before calling: this.TckPin.pinConfig, this.TmsPin.pinConfig,</para>
  /// <para>this.TdiPin.pinConfig, this.TdoPin.pinConfig </para>
  /// <para>See Object Browser for Error Retrun Codes.</para>
  /// </summary>
  /// <returns></returns>
  public int ProgramCpld(IEnumerable<byte> bData)
  {
    int num = (int) this.AdisBase.StoreData(bData);
    return this.PlayXsvf();
  }
}
