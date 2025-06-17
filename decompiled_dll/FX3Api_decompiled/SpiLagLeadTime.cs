// Decompiled with JetBrains decompiler
// Type: FX3Api.SpiLagLeadTime
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

#nullable disable
namespace FX3Api;

/// <summary>Enum for the possible chip select lag/lead times, in SPI clock cycles</summary>
public enum SpiLagLeadTime
{
  SPI_SSN_LAG_LEAD_ZERO_CLK,
  SPI_SSN_LAG_LEAD_HALF_CLK,
  SPI_SSN_LAG_LEAD_ONE_CLK,
  SPI_SSN_LAG_LEAD_ONE_HALF_CLK,
}
