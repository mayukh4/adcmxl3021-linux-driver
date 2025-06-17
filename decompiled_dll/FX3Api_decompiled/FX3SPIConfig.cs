// Decompiled with JetBrains decompiler
// Type: FX3Api.FX3SPIConfig
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

using System;

#nullable disable
namespace FX3Api;

/// <summary>Class for all the programmable SPI configuration options on the FX3.</summary>
public class FX3SPIConfig
{
  /// <summary>SPI word length (in bits)</summary>
  public byte WordLength;
  /// <summary>SCLK polarity</summary>
  public bool Cpol;
  /// <summary>CS polarity</summary>
  public bool ChipSelectPolarity;
  /// <summary>Clock phase</summary>
  public bool Cpha;
  /// <summary>Select if SPI controller works LSB first or MSB first</summary>
  public bool IsLSBFirst;
  /// <summary>Chip select control mode</summary>
  public SpiChipselectControl ChipSelectControl;
  /// <summary>Chip select lead delay mode</summary>
  public SpiLagLeadTime ChipSelectLeadTime;
  /// <summary>Chip select lag delay mode</summary>
  public SpiLagLeadTime ChipSelectLagTime;
  /// <summary>Connected DUT type</summary>
  public DUTType DUTType;
  /// <summary>Enable/Disable data ready interrupt triggering for SPI</summary>
  public bool DrActive;
  /// <summary>Data ready polarity for interrupt triggering (posedge or negedge)</summary>
  public bool DrPolarity;
  /// <summary>Scale factor to convert seconds to timer ticks)</summary>
  public uint SecondsToTimerTicks;
  private double SclkPeriod;
  private double StallSeconds;
  private int ClockFrequency;
  private uint m_ReadyPinGPIO;
  private ushort m_StallTime;
  private ushort m_StallCycles;
  private FX3PinObject m_ReadyPin;

  /// <summary>Property to store the current SPI clock. Updates the StallTime when set.</summary>
  /// <returns>The current SPI clock frequency</returns>
  public int SCLKFrequency
  {
    get => this.ClockFrequency;
    set
    {
      this.ClockFrequency = value;
      this.SclkPeriod = 1.0 / (double) this.ClockFrequency;
      this.m_StallCycles = checked ((ushort) Math.Round(unchecked ((double) this.m_StallTime / this.SclkPeriod * 1000000.0)));
    }
  }

  /// <summary>Property to get/set the stall time (in microseconds)</summary>
  /// <returns>The current stall time setting, in microseconds</returns>
  public ushort StallTime
  {
    get => this.m_StallTime;
    set
    {
      this.m_StallTime = (double) value <= 426172.583349871 ? value : throw new FX3ConfigurationException($"ERROR: Stall time of {value.ToString()} not supported");
      this.SclkPeriod = 1.0 / (double) this.ClockFrequency;
      this.StallSeconds = (double) this.m_StallTime / 1000000.0;
      this.m_StallCycles = Convert.ToUInt16(this.StallSeconds / this.SclkPeriod);
    }
  }

  /// <summary>Property to set the stall time, in terms of SPI clock cycles</summary>
  /// <returns>The current stall cycles</returns>
  public ushort StallCycles => this.m_StallCycles;

  /// <summary>Property to get/set the data ready pin</summary>
  /// <returns>The ready pin, as an FX3PinObject</returns>
  public FX3PinObject DataReadyPin
  {
    get => this.m_ReadyPin;
    set
    {
      this.m_ReadyPin = value;
      this.m_ReadyPinGPIO = this.m_ReadyPin.PinNumber;
    }
  }

  /// <summary>Property to get/set the data ready FX3 GPIO number</summary>
  /// <returns>The GPIO number, as a UInteger</returns>
  public uint DataReadyPinFX3GPIO
  {
    get => this.m_ReadyPinGPIO;
    set
    {
      this.m_ReadyPinGPIO = value;
      this.m_ReadyPin = new FX3PinObject(this.m_ReadyPinGPIO);
    }
  }

  /// <summary>
  /// Class Constructor, sets reasonable default values for IMU and ADcmXL devices
  /// </summary>
  /// <param name="SensorType">Optional parameter to specify default device SPI settings. Valid options are IMU and ADcmXL</param>
  public FX3SPIConfig(DeviceType SensorType = DeviceType.IMU, FX3BoardType BoardType = FX3BoardType.CypressFX3Board)
  {
    this.m_StallTime = (ushort) 50;
    this.m_StallCycles = (ushort) 100;
    this.Cpol = true;
    this.Cpha = true;
    this.ChipSelectPolarity = false;
    this.ChipSelectControl = SpiChipselectControl.SPI_SSN_CTRL_HW_END_OF_XFER;
    this.ChipSelectLagTime = SpiLagLeadTime.SPI_SSN_LAG_LEAD_ONE_CLK;
    this.ChipSelectLeadTime = SpiLagLeadTime.SPI_SSN_LAG_LEAD_ONE_CLK;
    this.IsLSBFirst = false;
    this.DrPolarity = true;
    this.DrActive = false;
    this.DataReadyPinFX3GPIO = 4U;
    this.DataReadyPinFX3GPIO = BoardType != FX3BoardType.CypressFX3Board ? 5U : 4U;
    switch (SensorType)
    {
      case DeviceType.IMU:
        this.ClockFrequency = 2000000;
        this.WordLength = (byte) 16 /*0x10*/;
        this.m_StallTime = (ushort) 15;
        this.DUTType = DUTType.IMU;
        break;
      case DeviceType.ADcmXL:
        this.m_StallTime = (ushort) 15;
        this.ClockFrequency = 14000000;
        this.WordLength = (byte) 16 /*0x10*/;
        this.DUTType = DUTType.ADcmXL3021;
        if (BoardType == FX3BoardType.CypressFX3Board)
        {
          this.DataReadyPinFX3GPIO = 3U;
          break;
        }
        this.DataReadyPinFX3GPIO = 4U;
        break;
      case DeviceType.ComponentSensor:
        this.ClockFrequency = 5000000;
        this.m_StallTime = (ushort) 5;
        this.WordLength = (byte) 16 /*0x10*/;
        this.DUTType = DUTType.IMU;
        break;
      default:
        this.ClockFrequency = 4000000;
        this.WordLength = (byte) 32 /*0x20*/;
        this.m_StallTime = (ushort) 5;
        this.DUTType = DUTType.IMU;
        break;
    }
  }
}
