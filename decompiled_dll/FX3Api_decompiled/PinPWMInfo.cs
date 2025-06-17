// Decompiled with JetBrains decompiler
// Type: FX3Api.PinPWMInfo
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

using AdisApi;

#nullable disable
namespace FX3Api;

/// <summary>Structure which contains all the info about the PWM status of a given pin</summary>
public class PinPWMInfo
{
  private int m_FX3GPIONumber;
  private int m_FX3TimerBlock;
  private double m_IdealFrequency;
  private double m_RealFrequency;
  private double m_IdealDutyCycle;
  private double m_RealDutyCycle;

  /// <summary>Constructor sets defaults</summary>
  public PinPWMInfo()
  {
    this.m_FX3GPIONumber = -1;
    this.m_FX3TimerBlock = -1;
    this.m_IdealDutyCycle = -1.0;
    this.m_RealFrequency = -1.0;
    this.m_IdealDutyCycle = -1.0;
    this.m_RealDutyCycle = -1.0;
  }

  /// <summary>Overload of toString for a PinPWMInfo</summary>
  /// <returns>String with all pertinent data about the pin PWM</returns>
  public override string ToString()
  {
    return $"Pin: {this.FX3GPIONumber.ToString()} Timer Block: {this.FX3TimerBlock.ToString()} Freq: {this.IdealFrequency.ToString()} Duty Cycle: {this.IdealDutyCycle.ToString()}";
  }

  internal void SetValues(
    IPinObject Pin,
    double SelectedFreq,
    double RealFreq,
    double SelectedDutyCycle,
    double RealDutyCycle)
  {
    this.m_FX3GPIONumber = checked ((int) (uint) (unchecked ((int) Pin.pinConfig) & (int) byte.MaxValue));
    this.m_FX3TimerBlock = this.m_FX3GPIONumber % 8;
    this.m_IdealFrequency = SelectedFreq;
    this.m_RealFrequency = RealFreq;
    this.m_IdealDutyCycle = SelectedDutyCycle;
    this.m_RealDutyCycle = RealDutyCycle;
  }

  /// <summary>The FX3 GPIO number for the pin (0-63)</summary>
  public int FX3GPIONumber => this.m_FX3GPIONumber;

  /// <summary>The associated complex timer block used to drive the PWM signal (0-7)</summary>
  public int FX3TimerBlock => this.m_FX3TimerBlock;

  /// <summary>The selected frequency (in Hz)</summary>
  public double IdealFrequency => this.m_IdealFrequency;

  /// <summary>The actual frequency the PWM signal should be (in Hz).</summary>
  public double RealFrequency => this.m_RealFrequency;

  /// <summary>The selected duty cycle</summary>
  public double IdealDutyCycle => this.m_IdealDutyCycle;

  /// <summary>The actual duty cycle of the PWM pin.</summary>
  public double RealDutyCycle => this.m_RealDutyCycle;
}
