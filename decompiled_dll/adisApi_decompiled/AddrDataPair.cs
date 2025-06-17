// Decompiled with JetBrains decompiler
// Type: AdisApi.AddrDataPair
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

#nullable disable
namespace AdisApi;

/// <summary>
/// Stores an address and the data associated with that address.
/// </summary>
[System.Diagnostics.DebuggerDisplay("{DebuggerDisplay}")]
public class AddrDataPair
{
  /// <summary>
  /// 
  /// </summary>
  public uint addr;
  /// <summary>
  /// 
  /// </summary>
  public uint? data;

  /// <summary>Creates a new instance of AddressDataPair</summary>
  public AddrDataPair()
  {
  }

  /// <summary>Creates a new instance of AddrDataPair</summary>
  public AddrDataPair(uint addr, uint? data)
  {
    this.addr = addr;
    this.data = data;
  }

  /// <summary>
  /// Create a string representation for the Visual Studio Debugger
  /// </summary>
  private string DebuggerDisplay
  {
    get
    {
      string str = this.data.HasValue ? this.data.Value.ToString("X") : "null";
      return $"{this.addr.ToString("X")}, {str}";
    }
  }

  /// <summary>Returns a string representation of the AddrDataPair</summary>
  /// <returns></returns>
  public override string ToString() => this.DebuggerDisplay;
}
