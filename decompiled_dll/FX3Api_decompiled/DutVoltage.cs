// Decompiled with JetBrains decompiler
// Type: FX3Api.DutVoltage
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

#nullable disable
namespace FX3Api;

/// <summary>
/// Enum for DUT power supply modes. These power supply modes are only available on the ADI in-house
/// FX3 Demonstration platform.
/// </summary>
public enum DutVoltage
{
  Off,
  On3_3Volts,
  On5_0Volts,
}
