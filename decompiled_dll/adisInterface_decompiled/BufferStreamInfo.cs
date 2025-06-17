// Decompiled with JetBrains decompiler
// Type: adisInterface.BufferStreamInfo
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

#nullable disable
namespace adisInterface;

/// <summary>
/// Structure to store info about current running buffered stream. Used for parsing result buffer.
/// </summary>
internal struct BufferStreamInfo
{
  public uint NumCaptures;
  public int MOSILength;
}
