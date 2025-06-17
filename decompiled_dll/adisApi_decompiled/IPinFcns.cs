// Decompiled with JetBrains decompiler
// Type: AdisApi.IPinFcns
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System.Collections.Generic;

#nullable disable
namespace AdisApi;

/// <summary>An inteface for pin functions.</summary>
public interface IPinFcns
{
  /// <summary>Sends a pulse to the specified pin</summary>
  /// <param name="pin">IPinObject to use for PulseDrive Operation</param>
  /// <param name="polarity">0 for falling edge, 1 for rising edge</param>
  /// <param name="pperiod">Period of pulse</param>
  /// <param name="mode">Number of times the pulse appears</param>
  void PulseDrive(IPinObject pin, uint polarity, double pperiod, uint mode);

  /// <summary>Waits for an edge / pulse</summary>
  /// <param name="pin">IPinObject to use for PulseWait Operation</param>
  /// <param name="polarity">0 for falling, 1 for rising</param>
  /// <param name="delayInMs">Delay / time elapsed (ms) before looking for edge trigger</param>
  /// <param name="timeoutInMs">Max time to wait for edge before exiting the wait for the edge trigger</param>
  /// <returns></returns>
  double PulseWait(IPinObject pin, uint polarity, uint delayInMs, uint timeoutInMs);

  /// <summary>Reads the value of a group of pins</summary>
  /// <param name="pin">IPinObject to use for ReadPin Operation</param>
  /// <returns></returns>
  uint ReadPin(IPinObject pin);

  /// <summary>Sets the value of a pin</summary>
  /// <param name="pins">IPinObjects to use for ReadPin Operation</param>
  /// <returns></returns>
  uint ReadPins(params IPinObject[] pins);

  /// <summary>Sets the value of a pin</summary>
  /// <param name="pins">IPinObjects to use for ReadPin Operation</param>
  /// <returns></returns>
  uint ReadPins(IEnumerable<IPinObject> pins);

  /// <summary>
  /// Measures the time elapsed between the initial triggered pin and the final triggered pin
  /// </summary>
  /// <param name="start_pin">Start Pin Trigger</param>
  /// <param name="start_polarity">Polarity for start pin trigger; 0 for falling, 1 for rising</param>
  /// <param name="stop_pin">Stop Pin Trigger</param>
  /// <param name="stop_polarity">Polarity for stop pin trigger; 0 for falling, 1 for rising</param>
  /// <param name="delay">Delay after first pin trigger before waiting for final pin trigger</param>
  /// <returns>Two element array. Time is in uSeconds. Element 0 is MSBs, element 1 id LSBs.</returns>
  ushort[] ReadTime(
    uint start_pin,
    uint start_polarity,
    uint stop_pin,
    uint stop_polarity,
    uint delay);

  /// <summary>Sets the value of a pin</summary>
  /// <param name="pin">IPinObject to use for SetPin Operation</param>
  /// <param name="value"></param>
  /// <returns></returns>
  void SetPin(IPinObject pin, uint value);
}
