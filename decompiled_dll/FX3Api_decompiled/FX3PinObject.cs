// Decompiled with JetBrains decompiler
// Type: FX3Api.FX3PinObject
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

using AdisApi;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace FX3Api;

/// <summary>Object to store configuration information for a Cypress FX3 GPIO pin.</summary>
[DebuggerDisplay("ToString")]
public class FX3PinObject : IPinObject
{
  private uint _pinNumber;

  /// <summary>Creates a new instance of FX3PinObject</summary>
  public FX3PinObject()
  {
    this._pinNumber = 0U;
    this.Invert = false;
  }

  /// <summary>Creates a new instance of PinObject with the given pin Number.</summary>
  /// <param name="pinNumber">Number of FX3 GPIO Pin to Use</param>
  public FX3PinObject(uint pinNumber)
  {
    this._pinNumber = 0U;
    this.Invert = false;
    this.PinNumber = pinNumber;
  }

  /// <summary>
  /// creates a new instance of PinObject with the given pin number and inversion.
  /// </summary>
  /// <param name="pinNumber">Number of FX3 GPIO Pin to Use</param>
  /// <param name="invert"></param>
  public FX3PinObject(uint pinNumber, bool invert)
  {
    this._pinNumber = 0U;
    this.Invert = false;
    this.PinNumber = pinNumber;
    this.Invert = invert;
  }

  /// <summary>GPIO pin number for the FX3.</summary>
  /// <returns></returns>
  public uint PinNumber
  {
    get => this._pinNumber;
    set
    {
      this._pinNumber = value <= 63U /*0x3F*/ ? value : throw new ArgumentOutOfRangeException("Pin must be in the range of 0-63");
    }
  }

  /// <summary>True if pin logic is to be inverted.</summary>
  /// <returns></returns>
  public bool Invert { get; set; }

  /// <summary>Provides a FX3 Configuration word for the parameter array.</summary>
  /// <returns></returns>
  public uint pinConfig
  {
    get
    {
      uint pinNumber = this.PinNumber;
      if (this.Invert)
        pinNumber |= 512U /*0x0200*/;
      return pinNumber;
    }
  }

  bool IPinObject.IPinObject_Equals(object obj)
  {
    bool flag;
    if (Information.IsNothing(RuntimeHelpers.GetObjectValue(obj)))
    {
      flag = false;
    }
    else
    {
      try
      {
        flag = (int) ((FX3PinObject) obj).pinConfig == (int) this.pinConfig;
      }
      catch (InvalidCastException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        flag = false;
        ProjectData.ClearProjectError();
      }
    }
    return flag;
  }

  int IPinObject.IPinObject_GetHashCode() => checked ((int) this.pinConfig);

  string IPinObject.IPinObject_ToString() => $"FX3 Pin: {this.PinNumber}  Invert: {this.Invert}.";
}
