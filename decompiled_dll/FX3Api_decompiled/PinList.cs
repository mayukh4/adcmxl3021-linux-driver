// Decompiled with JetBrains decompiler
// Type: FX3Api.PinList
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

using AdisApi;
using System.Collections.Generic;

#nullable disable
namespace FX3Api;

/// <summary>Custom list class with some extra ease of use functions added.</summary>
public class PinList : List<PinPWMInfo>
{
  /// <summary>
  /// Adds a pin to the list. Replaces any existing pin with the same FX3 GPIO number.
  /// </summary>
  /// <param name="Pin">The PinPWMInfo to add</param>
  public void AddReplace(PinPWMInfo Pin)
  {
    try
    {
      foreach (PinPWMInfo pinPwmInfo in (List<PinPWMInfo>) this)
      {
        if (pinPwmInfo.FX3GPIONumber == Pin.FX3GPIONumber)
        {
          this.Remove(pinPwmInfo);
          this.Add(Pin);
          return;
        }
      }
    }
    finally
    {
      List<PinPWMInfo>.Enumerator enumerator;
      enumerator.Dispose();
    }
    this.Add(Pin);
  }

  /// <summary>Gets the info for the selected pin</summary>
  /// <param name="Pin">The pin to get the info for, as an IPinObject</param>
  /// <returns>The pin info, as PinPWMInfo. Will have -1 for all fields if not found</returns>
  public PinPWMInfo GetPinPWMInfo(IPinObject Pin) => this.GetPinPWMInfo(Pin.pinConfig);

  /// <summary>Gets the info for the selected pin</summary>
  /// <param name="Pin">The pin to get the info for, as a UInteger (FX3 GPIO number)</param>
  /// <returns>The pin info, as PinPWMInfo. Will have -1 for all fields if not found</returns>
  public PinPWMInfo GetPinPWMInfo(uint Pin)
  {
    try
    {
      foreach (PinPWMInfo pinPwmInfo in (List<PinPWMInfo>) this)
      {
        if ((long) pinPwmInfo.FX3GPIONumber == (long) Pin)
          return pinPwmInfo;
      }
    }
    finally
    {
      List<PinPWMInfo>.Enumerator enumerator;
      enumerator.Dispose();
    }
    return new PinPWMInfo();
  }

  /// <summary>Overload of contains which checks if the list contains the given Pin</summary>
  /// <param name="Pin">The pin to look for (As IPinObject)</param>
  /// <returns>If the pin is contained in the list</returns>
  public bool Contains(IPinObject Pin) => this.Contains(Pin.pinConfig);

  /// <summary>Overload of contains which checks if the list contains the given Pin</summary>
  /// <param name="Pin">The pin to look for (As Integer)</param>
  /// <returns>If the pin is contained in the list</returns>
  public bool Contains(uint Pin)
  {
    try
    {
      foreach (PinPWMInfo pinPwmInfo in (List<PinPWMInfo>) this)
      {
        if ((long) pinPwmInfo.FX3GPIONumber == (long) Pin)
          return true;
      }
    }
    finally
    {
      List<PinPWMInfo>.Enumerator enumerator;
      enumerator.Dispose();
    }
    return false;
  }
}
