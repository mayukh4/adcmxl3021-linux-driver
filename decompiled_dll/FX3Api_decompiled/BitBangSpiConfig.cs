// Decompiled with JetBrains decompiler
// Type: FX3Api.BitBangSpiConfig
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

using System.Collections.Generic;

#nullable disable
namespace FX3Api;

/// <summary>
/// This class stores all the relevant information about a given bit bang SPI connection.
/// </summary>
public class BitBangSpiConfig
{
  /// <summary>Chip select pin for bit bang SPI</summary>
  public FX3PinObject CS;
  /// <summary>SCLK pin for bit bang SPI</summary>
  public FX3PinObject SCLK;
  /// <summary>MOSI (master out, slave in) pin for bit bang SPI</summary>
  public FX3PinObject MOSI;
  /// <summary>MISO (master is, slave out) pin for bit bang SPI</summary>
  public FX3PinObject MISO;
  /// <summary>Number of timer ticks from CS falling edge to first sclk edge</summary>
  public ushort CSLeadTicks;
  /// <summary>Number of timer ticks from last sclk edge to CS rising edge</summary>
  public ushort CSLagTicks;
  /// <summary>Half SCLK period timer ticks</summary>
  public uint SCLKHalfPeriodTicks;
  /// <summary>Stall time timer ticks</summary>
  public uint StallTicks;
  /// <summary>
  /// Bit bang SPI clock phase. If set to false (0) data is sampled
  /// on the clock active to inactive edge, and updated on the inactive to active
  /// edge. If set to true (1), data is updated on the clock
  /// active to inactive transition, and data is sampled on the inactive to
  /// active edge. The CPHA/CPOL settings used are based on this diagram:
  /// https://en.wikipedia.org/wiki/Serial_Peripheral_Interface#/media/File:SPI_timing_diagram2.svg
  /// </summary>
  public bool CPHA;
  /// <summary>
  /// Bit bang SPI clock polarity. True (1) is idle high, False (0) is
  /// idle low.
  /// </summary>
  public bool CPOL;
  private bool m_UpdatePins;

  /// <summary>
  /// Constructor which lets you specify set of default pins to use as bit bang SPI pins
  /// </summary>
  /// <param name="OverrideHardwareSpi">If the constructed BitBangSpiConfig should use hardware SPI pins, or FX3GPIO</param>
  public BitBangSpiConfig(bool OverrideHardwareSpi)
  {
    this.m_UpdatePins = false;
    if (OverrideHardwareSpi)
    {
      this.SCLK = new FX3PinObject(53U);
      this.CS = new FX3PinObject(54U);
      this.MOSI = new FX3PinObject(56U);
      this.MISO = new FX3PinObject(55U);
    }
    else
    {
      this.SCLK = new FX3PinObject(5U);
      this.CS = new FX3PinObject(6U);
      this.MOSI = new FX3PinObject(7U);
      this.MISO = new FX3PinObject(12U);
      this.m_UpdatePins = true;
    }
    this.CPHA = true;
    this.CPOL = true;
    this.CSLeadTicks = (ushort) 5;
    this.CSLagTicks = (ushort) 5;
    this.SCLKHalfPeriodTicks = 5U;
    this.StallTicks = 82U;
  }

  /// <summary>Get a parameters array for the current bit bang SPI configuration</summary>
  /// <returns>The parameter array to send to the FX3 for a bit bang vendor command</returns>
  public byte[] GetParameterArray()
  {
    return new List<byte>()
    {
      checked ((byte) (unchecked ((int) this.SCLK.pinConfig) & (int) byte.MaxValue)),
      checked ((byte) (unchecked ((int) this.CS.pinConfig) & (int) byte.MaxValue)),
      checked ((byte) (unchecked ((int) this.MOSI.pinConfig) & (int) byte.MaxValue)),
      checked ((byte) (unchecked ((int) this.MISO.pinConfig) & (int) byte.MaxValue)),
      checked ((byte) (unchecked ((int) this.SCLKHalfPeriodTicks) & (int) byte.MaxValue)),
      checked ((byte) ((this.SCLKHalfPeriodTicks & 65280U) >> 8)),
      checked ((byte) ((this.SCLKHalfPeriodTicks & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)),
      checked ((byte) ((this.SCLKHalfPeriodTicks & 4278190080U /*0xFF000000*/) >> 24)),
      checked ((byte) ((int) this.CSLeadTicks & (int) byte.MaxValue)),
      checked ((byte) (((uint) this.CSLeadTicks & 65280U) >> 8)),
      checked ((byte) ((int) this.CSLagTicks & (int) byte.MaxValue)),
      checked ((byte) (((uint) this.CSLagTicks & 65280U) >> 8)),
      checked ((byte) (unchecked ((int) this.StallTicks) & (int) byte.MaxValue)),
      checked ((byte) ((this.StallTicks & 65280U) >> 8)),
      checked ((byte) ((this.StallTicks & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)),
      checked ((byte) ((this.StallTicks & 4278190080U /*0xFF000000*/) >> 24)),
      (byte) -(this.CPHA ? 1 : 0),
      (byte) -(this.CPOL ? 1 : 0)
    }.ToArray();
  }

  /// <summary>
  /// Kinda jank. Flag pin update required at construction so FX3 Connection can fix pin assignments.
  /// Will only return true one time (if pin update is required)
  /// </summary>
  /// <returns>If GPIO re-assignment is required</returns>
  internal bool UpdatePinsRequired
  {
    get
    {
      bool updatePins = this.m_UpdatePins;
      this.m_UpdatePins = false;
      return updatePins;
    }
  }
}
