// Decompiled with JetBrains decompiler
// Type: AdisApi.ISpiInterface
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using sdpApi1;

#nullable disable
namespace AdisApi;

/// <summary>
/// 
/// </summary>
public interface ISpiInterface
{
  /// <summary>
  /// 
  /// </summary>
  AdisBase AdisBase { get; }

  /// <summary>
  /// 
  /// </summary>
  SdpConnector Connector { get; set; }

  /// <summary>
  /// 
  /// </summary>
  bool DrActive { get; set; }

  /// <summary>
  /// 
  /// </summary>
  uint DrPin { get; set; }

  /// <summary>
  /// 
  /// </summary>
  bool DrPolarity { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="readLength"></param>
  /// <param name="writeData"></param>
  /// <returns></returns>
  ushort[] ReadArrayU16(int readLength, ushort writeData);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="readLength"></param>
  /// <param name="writeData"></param>
  /// <returns></returns>
  uint[] ReadArrayU32(int readLength, uint writeData);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="mosiState"></param>
  /// <returns></returns>
  ushort ReadWordU16(bool mosiState);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  ushort ReadWordU16();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <returns></returns>
  ushort ReadWordU16(ushort writeData);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  uint ReadWordU32();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="mosiState"></param>
  /// <returns></returns>
  uint ReadWordU32(bool mosiState);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <returns></returns>
  uint ReadWordU32(uint writeData);

  /// <summary>
  /// 
  /// </summary>
  int SclkFrequency { get; set; }

  /// <summary>
  /// 
  /// </summary>
  SdpBase SdpBase { get; }

  /// <summary>
  /// 
  /// </summary>
  int WordSize { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <returns></returns>
  ushort[] WriteReadArrayU16(ushort[] writeData);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  ushort[] WriteReadArrayU16(ushort[] writeData, uint numCaptures);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  void WriteWordU16(ushort writeData);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  void WriteWordU32(uint writeData);

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  ushort[] GetStreamDataPacketU16();

  /// <summary>
  /// 
  /// </summary>
  void StopStream();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <param name="numCaptures"></param>
  void StreamFromU16(ushort[] writeData, uint numCaptures);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="writeData"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  ushort[] WriteReadStreamU16(ushort[] writeData, uint numCaptures, uint numBuffers);
}
