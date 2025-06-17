// Decompiled with JetBrains decompiler
// Type: AdisApi.Gpio
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using sdpApi1;

#nullable disable
namespace AdisApi;

/// <summary>Gpio object, functions like sdpApi1.Gpio.</summary>
/// <summary>
/// 
/// </summary>
/// <param name="adis"></param>
/// <param name="sdpConn"></param>
public class Gpio(AdisBase adis, int sdpConn) : Gpio(adis.Base, (SdpConnector) sdpConn)
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="bitsMask"></param>
  /// <returns></returns>
  public virtual int configInput(byte bitsMask) => base.configInput(bitsMask);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="bitsMask"></param>
  /// <returns></returns>
  public virtual int configOutput(byte bitsMask) => base.configOutput(bitsMask);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="bitsMask"></param>
  /// <returns></returns>
  public virtual int bitClear(byte bitsMask) => base.bitClear(bitsMask);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="bitsMask"></param>
  /// <returns></returns>
  public virtual int bitSet(byte bitsMask) => base.bitSet(bitsMask);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="bitsMask"></param>
  /// <returns></returns>
  public virtual int bitToggle(byte bitsMask) => base.bitToggle(bitsMask);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="bitsMask"></param>
  /// <returns></returns>
  public virtual int dataRead(out byte bitsMask) => base.dataRead(ref bitsMask);
}
