// Decompiled with JetBrains decompiler
// Type: AdisApi.AdisVersion
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System.Text;

#nullable disable
namespace AdisApi;

/// <summary>
/// Class containing version info for SDP software and firmware.
/// </summary>
public class AdisVersion
{
  /// <summary>
  /// 
  /// </summary>
  public int SdpMajorRev;
  /// <summary>
  /// 
  /// </summary>
  public int SdpMinorRev;
  /// <summary>
  /// 
  /// </summary>
  public int SdpHostRev;
  /// <summary>
  /// 
  /// </summary>
  public int SdpBaseRev;
  /// <summary>
  /// 
  /// </summary>
  public int AdisMajorRev;
  /// <summary>
  /// 
  /// </summary>
  public int AdisMinorRev;
  /// <summary>
  /// 
  /// </summary>
  public int AdisBuildRev;
  /// <summary>
  /// 
  /// </summary>
  public string CompileDate;
  /// <summary>
  /// 
  /// </summary>
  public string CompileTime;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    stringBuilder.AppendFormat("Base  Rev: {0}", (object) this.SdpBaseRev).AppendLine();
    stringBuilder.AppendFormat("Major Rev: {0}", (object) this.SdpMajorRev).AppendLine();
    stringBuilder.AppendFormat("Minor Rev: {0}", (object) this.SdpMinorRev).AppendLine();
    stringBuilder.AppendFormat("Host  Rev: {0}", (object) this.SdpHostRev).AppendLine();
    stringBuilder.AppendFormat("User  Rev: {0}", (object) this.AdisMajorRev).AppendLine();
    stringBuilder.AppendFormat("Comp Date: {0}", (object) this.CompileDate).AppendLine();
    stringBuilder.AppendFormat("Comp Time: {0}", (object) this.CompileTime);
    return stringBuilder.ToString();
  }
}
