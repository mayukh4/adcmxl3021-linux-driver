// Decompiled with JetBrains decompiler
// Type: FX3Api.FX3BoardType
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

#nullable disable
namespace FX3Api;

/// <summary>
/// Possible FX3 board types. Used to differentiate iSensors FX3 board from Cypress Explorer kit. This list enumerates
/// all possible hardware ID's provided by the FX3 ID pins. Most are not used.
/// </summary>
public enum FX3BoardType
{
  CypressFX3Board,
  iSensorFX3Board_A,
  iSensorFX3Board_B,
  iSensorFX3Board_C,
  iSensorFX3Board_D,
  iSensorFX3Board_E,
  iSensorFX3Board_F,
  iSensorFX3Board_G,
  iSensorFX3Board_H,
}
