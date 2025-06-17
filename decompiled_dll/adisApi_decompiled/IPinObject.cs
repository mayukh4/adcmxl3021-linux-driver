// Decompiled with JetBrains decompiler
// Type: AdisApi.IPinObject
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

#nullable disable
namespace AdisApi;

/// <summary>An inteface for pin objects.</summary>
public interface IPinObject
{
  /// <summary>True if pin logic is to be inverted.</summary>
  bool Invert { get; set; }

  /// <summary>
  /// Returns a unit that contains an internal represention of the pin location and pin invert bit.
  /// </summary>
  uint pinConfig { get; }

  /// <summary>Returns true if instances refer to the same pin.</summary>
  /// <param name="obj"></param>
  /// <returns></returns>
  bool Equals(object obj);

  /// <summary>Returns a hash code</summary>
  /// <returns></returns>
  int GetHashCode();

  /// <summary>Returns a string representation of the PinObject.</summary>
  /// <returns></returns>
  string ToString();
}
