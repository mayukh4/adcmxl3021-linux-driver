// Decompiled with JetBrains decompiler
// Type: AdisApi.AdisCommand
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

#nullable disable
namespace AdisApi;

/// <summary>
/// Constants for Blackfin commands in adis / user command space
/// </summary>
internal static class AdisCommand
{
  public const uint GetAdisFirmwareGuid = 4160749569 /*0xF8000001*/;
  public const uint GetAdisFirmwareVersion = 4160749570 /*0xF8000002*/;
  public const uint iSensorSpiStreamStart = 4160749571 /*0xF8000003*/;
  public const uint iSensorSpiStreamInit = 4160749572 /*0xF8000004*/;
  public const uint iSensorSpiTransfer = 4160749573 /*0xF8000005*/;
  public const uint SportStreamInit = 4160749574 /*0xF8000006*/;
  public const uint SportSpiTransfer = 4160749575 /*0xF8000007*/;
  public const uint BasicSpiTransfer = 4160749576 /*0xF8000008*/;
  public const uint EdgeToEdgeMeasure = 4160749577 /*0xF8000009*/;
  public const uint PulseDrive = 4160749578 /*0xF800000A*/;
  public const uint PulseWait = 4160749579 /*0xF800000B*/;
  public const uint StoreData = 4160749580 /*0xF800000C*/;
  public const uint PlayXsvf = 4160749581 /*0xF800000D*/;
  public const uint WriteOtp = 4160749582 /*0xF800000E*/;
  public const uint ReadOtp = 4160749583 /*0xF800000F*/;
  public const uint ReadInputPin = 4160749584 /*0xF8000010*/;
  public const uint SetOutputPin = 4160749585;
  public const uint RebootfromSpi = 4160749586;
  public const uint ReadSpiFlash = 4160749587;
  public const uint WriteSpiFlash = 4160749588;
  public const uint EraseSpiFlash = 4160749589;
  public const uint ConfigIce40 = 4160749590;
  public const uint BootCortex4 = 4160749591;
  public const uint SpiGernericTransfer = 4160749592;
  public const uint SpiGenericInit = 4160749593;
  public const uint SpiGenericStart = 4160749600 /*0xF8000020*/;
}
