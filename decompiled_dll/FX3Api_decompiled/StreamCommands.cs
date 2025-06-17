// Decompiled with JetBrains decompiler
// Type: FX3Api.StreamCommands
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

#nullable disable
namespace FX3Api;

/// <summary>
/// This enum lists all the available streaming commands which can be sent to the FX3 (in the endpoint index)
/// </summary>
public enum StreamCommands
{
  ADI_STREAM_DONE_CMD,
  ADI_STREAM_START_CMD,
  ADI_STREAM_STOP_CMD,
}
