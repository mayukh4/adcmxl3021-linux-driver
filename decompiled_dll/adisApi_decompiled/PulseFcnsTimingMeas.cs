// Decompiled with JetBrains decompiler
// Type: AdisApi.PulseFcnsTimingMeas
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System;
using System.Diagnostics;

#nullable disable
namespace AdisApi;

/// <summary>
/// Basic SPI configuration, reads, and writes using ADiS spi code on Blackfin.
/// </summary>
[Obsolete("Please use the PinFcns class.")]
public class PulseFcnsTimingMeas
{
  /// <summary>
  /// Set properties to values for pulse fcns and edge timing measure
  /// </summary>
  public PulseFcnsTimingMeas(AdisBase AdisBase) => this.AdisBase = AdisBase;

  /// <summary>
  /// Gets the AdisBase object associated with thie Spi interface.
  /// </summary>
  public AdisBase AdisBase { get; private set; }

  /// <summary>Used to exit loops</summary>
  public bool Cancel { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="startPin"></param>
  /// <param name="startPol"></param>
  /// <param name="stopPin"></param>
  /// <param name="stopPol"></param>
  /// <param name="delay"></param>
  public void StartIntervalMeasurement(
    PinObject startPin,
    uint startPol,
    PinObject stopPin,
    uint stopPol,
    uint delay)
  {
    uint[] numArray1 = new uint[5]
    {
      startPin.pinConfig,
      startPol,
      stopPin.pinConfig,
      stopPol,
      delay
    };
    ushort[] numArray2 = new ushort[1];
    this.AdisBase.Base.userCmdU16(4160749577U /*0xF8000009*/, numArray1, new ushort[0], 2, ref numArray2);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public uint CheckIntervalMeasurement()
  {
    uint[] numArray1 = new uint[5]
    {
      (uint) byte.MaxValue,
      0U,
      0U,
      0U,
      0U
    };
    ushort[] numArray2 = new ushort[1];
    this.AdisBase.Base.userCmdU16(4160749577U /*0xF8000009*/, numArray1, new ushort[0], 2, ref numArray2);
    return (uint) numArray2[1] << 16 /*0x10*/ | (uint) numArray2[0];
  }

  /// <summary>
  /// Measures the time elapsed between the initial triggered pin and the final triggered pin
  /// </summary>
  /// <param name="start_pin">Start Pin Trigger</param>
  /// <param name="start_polarity">Polarity for start pin trigger; 0 for falling, 1 for rising</param>
  /// <param name="stop_pin">Stop Pin Trigger</param>
  /// <param name="stop_polarity">Polarity for stop pin trigger; 0 for falling, 1 for rising</param>
  /// <param name="delay">Delay after first pin trigger before waiting for final pin trigger</param>
  /// <returns>Two element array. Time is in us</returns>
  public ushort[] ReadTime(
    uint start_pin,
    uint start_polarity,
    uint stop_pin,
    uint stop_polarity,
    uint delay)
  {
    ushort[] numArray1 = new ushort[0];
    uint[] numArray2 = new uint[10]
    {
      start_pin,
      start_polarity,
      stop_pin,
      stop_polarity,
      delay,
      0U,
      0U,
      0U,
      0U,
      0U
    };
    ushort[] numArray3 = new ushort[1];
    this.AdisBase.Base.userCmdU16(4160749577U /*0xF8000009*/, numArray2, new ushort[0], 2, ref numArray3);
    return numArray3;
  }

  /// <summary>Sends a pulse to the specified pin</summary>
  /// <param name="pin">First nibble is port, second nibble is pin</param>
  /// <param name="polarity">0 for falling edge, 1 for rising edge</param>
  /// <param name="pperiod">Period of pulse</param>
  /// <param name="mode">Number of times the pulse appears</param>
  public void PulseDrive(PinObject pin, uint polarity, double pperiod, uint mode)
  {
    uint[] numArray1 = new uint[4]
    {
      pin.pinConfig,
      polarity,
      0U,
      0U
    };
    uint num = (uint) (1.0 / pperiod);
    numArray1[2] = num;
    numArray1[3] = mode;
    ushort[] numArray2 = new ushort[0];
    this.AdisBase.Base.userCmdU16(4160749578U /*0xF800000A*/, numArray1, new ushort[0], 0, ref numArray2);
  }

  /// <summary>
  /// Measures the time elapsed after reset falling edge and the next DR falling edge. This has not been tested
  /// </summary>
  /// <returns></returns>
  public double ResetMsr()
  {
    uint[] numArray1 = new uint[4];
    this.Cancel = false;
    uint num1 = 0;
    numArray1[0] = 32U /*0x20*/;
    numArray1[1] = 0U;
    uint num2 = 50;
    numArray1[2] = num2;
    numArray1[3] = 1U;
    numArray1[4] = 1U;
    numArray1[5] = 33U;
    numArray1[6] = num1;
    ushort[] numArray2 = new ushort[2];
    this.AdisBase.Base.userCmdU16(4160749578U /*0xF800000A*/, numArray1, new ushort[0], 2, ref numArray2);
    double num3 = (double) (((uint) numArray2[0] | (uint) numArray2[1] << 16 /*0x10*/) / 1000U);
    if (num3 != 0.0)
      return num3;
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    uint[] numArray3 = new uint[4]
    {
      (uint) byte.MaxValue,
      num1,
      0U,
      0U
    };
    int num4 = 1000;
    while (stopwatch.ElapsedMilliseconds < (long) num4 && !this.Cancel)
    {
      this.AdisBase.Base.userCmdU16(4160749579U /*0xF800000B*/, numArray3, new ushort[0], 2, ref numArray2);
      double num5 = (double) (((uint) numArray2[0] | (uint) numArray2[1] << 16 /*0x10*/) / 1000U);
      if (num5 != 0.0)
      {
        stopwatch.Stop();
        return num5;
      }
    }
    stopwatch.Stop();
    numArray1[3] = 1U;
    this.AdisBase.Base.userCmdU16(4160749579U /*0xF800000B*/, numArray3, new ushort[0], 2, ref numArray2);
    return 1.8446744073709552E+19;
  }

  /// <summary>Waits for an edge / pulse</summary>
  /// <param name="pinConfig">Upper nibble is port, lower nibble is pin</param>
  /// <param name="polarity">0 for falling, 1 for rising</param>
  /// <param name="delayInMs">Delay / time elapsed (ms) before looking for edge trigger</param>
  /// <param name="timeoutInMs">Max time to wait for edge before exiting the wait for the edge trigger</param>
  /// <returns></returns>
  public double PulseWait(uint pinConfig, uint polarity, uint delayInMs, uint timeoutInMs)
  {
    uint[] numArray1 = new uint[4];
    this.Cancel = false;
    numArray1[0] = pinConfig;
    numArray1[1] = polarity;
    numArray1[2] = delayInMs;
    numArray1[3] = 0U;
    ushort[] numArray2 = new ushort[2];
    this.AdisBase.Base.userCmdU16(4160749579U /*0xF800000B*/, numArray1, new ushort[0], 2, ref numArray2);
    double num1 = (double) ((uint) numArray2[0] | (uint) numArray2[1] << 16 /*0x10*/) / 1000.0;
    if (num1 != 0.0)
      return num1;
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    numArray1[0] = (uint) byte.MaxValue;
    while (stopwatch.ElapsedMilliseconds < (long) timeoutInMs && !this.Cancel)
    {
      this.AdisBase.Base.userCmdU16(4160749579U /*0xF800000B*/, numArray1, new ushort[0], 2, ref numArray2);
      double num2 = (double) ((uint) numArray2[0] | (uint) numArray2[1] << 16 /*0x10*/) / 1000.0;
      if (num2 != 0.0)
      {
        stopwatch.Stop();
        return num2;
      }
    }
    stopwatch.Stop();
    numArray1[3] = 1U;
    this.AdisBase.Base.userCmdU16(4160749579U /*0xF800000B*/, numArray1, new ushort[0], 2, ref numArray2);
    return 1.8446744073709552E+19;
  }
}
