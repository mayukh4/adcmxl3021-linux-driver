// Decompiled with JetBrains decompiler
// Type: FX3Api.EndpointAddresses
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

#nullable disable
namespace FX3Api;

/// <summary>
/// This enum lists all USB endpoints generated and used by the application firmware.
/// </summary>
public enum EndpointAddresses
{
  ADI_FROM_PC_ENDPOINT = 1,
  ADI_STREAMING_ENDPOINT = 129, // 0x00000081
  ADI_TO_PC_ENDPOINT = 130, // 0x00000082
}
