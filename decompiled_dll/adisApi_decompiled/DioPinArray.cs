// Decompiled with JetBrains decompiler
// Type: AdisApi.DioPinArray
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace AdisApi;

/// <summary>Holds pin information for DUT DIO pins in a pin map.</summary>
public class DioPinArray
{
  private PinObject[] pins;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="numPins"></param>
  public DioPinArray(int numPins) => this.pins = new PinObject[numPins];

  /// <summary>
  /// 
  /// </summary>
  /// <param name="pinNum"></param>
  /// <returns></returns>
  public PinObject this[int pinNum]
  {
    get
    {
      this.validatePinNum(pinNum);
      return this.pins[pinNum - 1];
    }
    set
    {
      this.validatePinNum(pinNum);
      this.pins[pinNum - 1] = value;
    }
  }

  private void validatePinNum(int pinNum)
  {
    if (pinNum < 1 || pinNum > ((IEnumerable<PinObject>) this.pins).Count<PinObject>())
      throw new ArgumentOutOfRangeException($"DIO Pin must be in the range of 1 to {((IEnumerable<PinObject>) this.pins).Count<PinObject>()}, was {pinNum}.");
  }
}
