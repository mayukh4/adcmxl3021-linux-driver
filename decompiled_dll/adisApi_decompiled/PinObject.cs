// Decompiled with JetBrains decompiler
// Type: AdisApi.PinObject
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System;
using System.Diagnostics;

#nullable disable
namespace AdisApi;

/// <summary>Specifies properties for a Blackfin GPIO pin.</summary>
[DebuggerDisplay("{ToString}")]
public class PinObject : IPinObject
{
  private uint bit;

  /// <summary>
  /// Provides an SDP configuration word for the parameter array.
  /// </summary>
  public uint pinConfig
  {
    get
    {
      uint pinConfig = this.Bit | (uint) this.Port << 4;
      if (this.Invert)
        pinConfig |= 512U /*0x0200*/;
      return pinConfig;
    }
  }

  /// <summary>The bit (0 to 15) associated with the Blackfin pin.</summary>
  public uint Bit
  {
    get => this.bit;
    set
    {
      this.bit = value <= 15U ? value : throw new ArgumentOutOfRangeException("Bit must be in the range 0 to 15.");
    }
  }

  /// <summary>The port (F, G, H) that contains the Blackfin pin.</summary>
  public PortType Port { get; set; }

  /// <summary>True if pin logic is to be inverted.</summary>
  public bool Invert { get; set; }

  /// <summary>Creates a new instance of PinObject</summary>
  public PinObject()
  {
  }

  /// <summary>
  /// Creates a new instance of PinObject with the given port and bit.
  /// </summary>
  /// <param name="port"></param>
  /// <param name="bit"></param>
  public PinObject(PortType port, uint bit)
  {
    this.Bit = bit;
    this.Port = port;
  }

  /// <summary>
  /// Creates a new instance of PinObject with the given port, bit, and inversion.
  /// </summary>
  /// <param name="port"></param>
  /// <param name="bit"></param>
  /// <param name="invert"></param>
  public PinObject(PortType port, uint bit, bool invert)
  {
    this.Bit = bit;
    this.Port = port;
    this.Invert = invert;
  }

  /// <summary>Returns a string representation of the PinObject.</summary>
  /// <returns></returns>
  public override string ToString()
  {
    return $"Port: {Enum.GetName(typeof (PortType), (object) this.Port)}  Bit: {this.Bit:D2}  Invert: {this.Invert}.";
  }

  /// <summary>
  /// Returns true if instances contain the same pin configuration.
  /// </summary>
  /// <param name="obj">Object to be compared.</param>
  /// <returns></returns>
  public override bool Equals(object obj)
  {
    return obj != null && obj is PinObject pinObject && (int) pinObject.pinConfig == (int) this.pinConfig;
  }

  /// <summary>Returns a hash code</summary>
  /// <returns></returns>
  public override int GetHashCode() => (int) this.pinConfig;
}
