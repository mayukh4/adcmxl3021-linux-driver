// Decompiled with JetBrains decompiler
// Type: AdisApi.PinMapObject
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

#nullable disable
namespace AdisApi;

/// <summary>
/// A group of SdpPinObjects that describe a SpiInterface.
/// </summary>
public class PinMapObject
{
  /// <summary>
  /// 
  /// </summary>
  public DioPinArray DioPins;

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
  public PinObject Cs2Pin { get; set; }

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
  public PinObject SyncPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public PinObject TckPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public PinObject TmsPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public PinObject TdiPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public PinObject TdoPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="numPins"></param>
  public PinMapObject(int numPins) => this.DioPins = new DioPinArray(numPins);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public static PinMapObject createDcTestCpldPinMap()
  {
    return new PinMapObject(0)
    {
      SclkPin = new PinObject(PortType.G, 2U),
      MisoPin = new PinObject(PortType.G, 3U),
      MosiPin = new PinObject(PortType.G, 4U),
      CsPin = new PinObject(PortType.H, 8U),
      Cs2Pin = (PinObject) null,
      ReadyPin = (PinObject) null,
      ResetPin = new PinObject(PortType.H, 3U),
      SyncPin = (PinObject) null,
      TckPin = new PinObject(PortType.H, 6U),
      TmsPin = new PinObject(PortType.H, 7U),
      TdiPin = new PinObject(PortType.H, 5U),
      TdoPin = new PinObject(PortType.H, 3U)
    };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public static PinMapObject createDcTestDutPinMap()
  {
    return new PinMapObject(0)
    {
      SclkPin = new PinObject(PortType.G, 2U),
      MisoPin = new PinObject(PortType.G, 3U),
      MosiPin = new PinObject(PortType.G, 4U),
      CsPin = new PinObject(PortType.F, 9U),
      Cs2Pin = (PinObject) null,
      ReadyPin = new PinObject(PortType.H, 0U),
      ResetPin = new PinObject(PortType.H, 4U),
      SyncPin = new PinObject(PortType.H, 2U),
      TckPin = new PinObject(PortType.F, 5U),
      TmsPin = new PinObject(PortType.F, 3U),
      TdiPin = new PinObject(PortType.F, 4U),
      TdoPin = new PinObject(PortType.F, 1U)
    };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public static PinMapObject createEvalAdisPinMap()
  {
    return new PinMapObject(4)
    {
      SclkPin = new PinObject(PortType.F, 2U),
      MisoPin = new PinObject(PortType.F, 0U),
      MosiPin = new PinObject(PortType.F, 3U),
      CsPin = new PinObject(PortType.F, 1U),
      Cs2Pin = new PinObject(PortType.F, 8U),
      ReadyPin = new PinObject(PortType.H, 1U),
      ResetPin = new PinObject(PortType.H, 0U),
      SyncPin = new PinObject(PortType.H, 2U),
      DioPins = {
        [1] = new PinObject(PortType.H, 1U),
        [2] = new PinObject(PortType.H, 2U),
        [3] = new PinObject(PortType.H, 3U),
        [4] = new PinObject(PortType.H, 4U)
      }
    };
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public static PinMapObject createCharacAdisPinMap()
  {
    return new PinMapObject(4)
    {
      SclkPin = new PinObject(PortType.F, 2U),
      MisoPin = new PinObject(PortType.F, 0U),
      MosiPin = new PinObject(PortType.F, 3U),
      CsPin = new PinObject(PortType.F, 1U),
      Cs2Pin = new PinObject(PortType.F, 8U),
      ReadyPin = new PinObject(PortType.H, 1U),
      ResetPin = new PinObject(PortType.H, 0U),
      SyncPin = new PinObject(PortType.H, 2U),
      DioPins = {
        [1] = new PinObject(PortType.H, 1U),
        [2] = new PinObject(PortType.H, 2U),
        [3] = new PinObject(PortType.H, 3U),
        [4] = new PinObject(PortType.H, 4U)
      }
    };
  }
}
