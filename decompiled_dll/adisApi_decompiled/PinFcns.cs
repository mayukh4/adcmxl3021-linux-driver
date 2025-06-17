// Decompiled with JetBrains decompiler
// Type: AdisApi.PinFcns
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

#nullable disable
namespace AdisApi;

/// <summary>
/// Functions to Read and Write to GPIOs (General Purpose IO pins)
/// </summary>
public class PinFcns : IPinFcns
{
  /// <summary>Used to exit loops</summary>
  public bool Cancel { get; set; }

  /// <summary>
  /// Creates a new instance of PinFcns for a specified SDP based board.
  /// </summary>
  public PinFcns(AdisBase AdisBase) => this.AdisBase = AdisBase;

  /// <summary>
  /// Gets the AdisBase object associated with this instanc of PinFcns.
  /// </summary>
  public AdisBase AdisBase { get; set; }

  /// <summary>Reads the value of a group of pins</summary>
  /// <param name="pins">IPinObjects to use for PulseWait Operation</param>
  /// <returns></returns>
  public uint ReadPins(params IPinObject[] pins) => this.ReadPins(pins);

  /// <summary>Reads the value of a group of pins</summary>
  /// <param name="pins">IPinObjects to use for PulseWait Operation</param>
  /// <returns></returns>
  public uint ReadPins(IEnumerable<IPinObject> pins)
  {
    if (pins.Count<IPinObject>() > 32 /*0x20*/)
      throw new ArgumentException("Only 32 pins may be read.");
    uint num = 0;
    foreach (IPinObject pin in pins)
    {
      num <<= 1;
      num |= this.ReadPin(pin);
    }
    return num;
  }

  /// <summary>Reads the value of a pin</summary>
  /// <param name="pin">IPinObject to use for PulseWait Operation</param>
  /// <returns></returns>
  public uint ReadPin(IPinObject pin)
  {
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749584U /*0xF8000010*/, new uint[1]
    {
      pin.pinConfig
    }, (uint[]) null, 1, ref numArray);
    return numArray[0];
  }

  /// <summary>Sets the value of a pin</summary>
  /// <param name="pin">IPinObject to use for PulseWait Operation</param>
  /// <param name="value"></param>
  /// <returns></returns>
  public void SetPin(IPinObject pin, uint value)
  {
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749585U, new uint[2]
    {
      pin.pinConfig,
      value
    }, (uint[]) null, 0, ref numArray);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="startPin">Start Pin Trigger</param>
  /// <param name="startPol">Polarity for start pin trigger; 0 for falling, 1 for rising</param>
  /// <param name="stopPin">Stop Pin Trigger</param>
  /// <param name="stopPol">Polarity for stop pin trigger; 0 for falling, 1 for rising</param>
  /// <param name="delay">Delay after first pin trigger before waiting for final pin trigger</param>
  public void StartIntervalMeasurement(
    PinObject startPin,
    uint startPol,
    PinObject stopPin,
    uint stopPol,
    uint delay)
  {
    ushort[] numArray;
    this.AdisBase.Base.userCmdU16(4160749577U /*0xF8000009*/, new uint[5]
    {
      startPin.pinConfig,
      startPol,
      stopPin.pinConfig,
      stopPol,
      delay
    }, (ushort[]) null, 2, ref numArray);
  }

  /// <summary>Returns the results form an interval measurement.</summary>
  /// <returns>Two element array. Time is in uSeconds. Element 0 is MSBs, element 1 id LSBs.</returns>
  public uint CheckIntervalMeasurement()
  {
    ushort[] numArray;
    this.AdisBase.Base.userCmdU16(4160749577U /*0xF8000009*/, new uint[5]
    {
      (uint) byte.MaxValue,
      0U,
      0U,
      0U,
      0U
    }, (ushort[]) null, 2, ref numArray);
    return (uint) numArray[1] << 16 /*0x10*/ | (uint) numArray[0];
  }

  /// <summary>
  /// Measures the time elapsed between the initial triggered pin and the final triggered pin
  /// </summary>
  /// <param name="start_pin">Start Pin Trigger</param>
  /// <param name="start_polarity">Polarity for start pin trigger; 0 for falling, 1 for rising</param>
  /// <param name="stop_pin">Stop Pin Trigger</param>
  /// <param name="stop_polarity">Polarity for stop pin trigger; 0 for falling, 1 for rising</param>
  /// <param name="delay">Delay after first pin trigger before waiting for final pin trigger</param>
  /// <returns>Two element array. Time is in uSeconds. Element 0 is MSBs, element 1 id LSBs.</returns>
  public ushort[] ReadTime(
    uint start_pin,
    uint start_polarity,
    uint stop_pin,
    uint stop_polarity,
    uint delay)
  {
    ushort[] numArray;
    this.AdisBase.Base.userCmdU16(4160749577U /*0xF8000009*/, new uint[5]
    {
      start_pin,
      start_polarity,
      stop_pin,
      stop_polarity,
      delay
    }, (ushort[]) null, 2, ref numArray);
    return numArray;
  }

  /// <summary>Sends a pulse to the specified pin</summary>
  /// <param name="pin">IPinObject to use for PulseWait Operation</param>
  /// <param name="polarity">0 for falling edge, 1 for rising edge</param>
  /// <param name="pperiod">Period of pulse</param>
  /// <param name="mode">Number of times the pulse appears</param>
  public void PulseDrive(IPinObject pin, uint polarity, double pperiod, uint mode)
  {
    ushort[] numArray;
    this.AdisBase.Base.userCmdU16(4160749578U /*0xF800000A*/, new uint[4]
    {
      pin.pinConfig,
      polarity,
      (uint) (1.0 / pperiod),
      mode
    }, (ushort[]) null, 0, ref numArray);
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
  /// <param name="pin">IPinObject to use for PulseWait Operation</param>
  /// <param name="polarity">0 for falling, 1 for rising</param>
  /// <param name="delayInMs">Delay / time elapsed (ms) before looking for edge trigger</param>
  /// <param name="timeoutInMs">Max time to wait for edge before exiting the wait for the edge trigger</param>
  /// <returns></returns>
  public double PulseWait(IPinObject pin, uint polarity, uint delayInMs, uint timeoutInMs)
  {
    uint[] numArray1 = new uint[4];
    this.Cancel = false;
    numArray1[0] = pin.pinConfig;
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
