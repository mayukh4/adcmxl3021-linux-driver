// Decompiled with JetBrains decompiler
// Type: AdisApi.SdpDaughter
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using sdpApi1;

#nullable disable
namespace AdisApi;

/// <summary>
/// 
/// </summary>
public class SdpDaughter
{
  private const string Key = "Munster";
  private SdpConnector connector;
  private DaughterManufacture daughter;
  private byte eepromAddress;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sdp"></param>
  public SdpDaughter(SdpBase sdp) => sdp.newDaughterManufacture("Munster", ref this.daughter);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="adis"></param>
  public SdpDaughter(AdisBase adis)
  {
    adis.Base.newDaughterManufacture("Munster", ref this.daughter);
  }

  /// <summary>
  /// 
  /// </summary>
  public byte EepromAddress
  {
    get => this.eepromAddress;
    set => this.eepromAddress = value;
  }

  /// <summary>
  /// 
  /// </summary>
  public SdpConnector Connector
  {
    get => this.connector;
    set => this.connector = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="programFilePath"></param>
  /// <param name="reboot"></param>
  /// <param name="reconnect"></param>
  /// <param name="flashDev"></param>
  public void ProgramSPIFlash(
    string programFilePath,
    bool reboot,
    bool reconnect,
    FlashDev flashDev)
  {
    this.daughter.programUserFlash(programFilePath, reboot, reconnect, flashDev);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="programFilePath"></param>
  public void WriteEEPROM(string programFilePath)
  {
    this.daughter.writeEepromFromFile(this.connector, this.eepromAddress, programFilePath);
  }
}
