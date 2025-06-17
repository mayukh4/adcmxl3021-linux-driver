// Decompiled with JetBrains decompiler
// Type: adisInterface.IBufferedStreamProducer
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using RegMapClasses;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace adisInterface;

public interface IBufferedStreamProducer
{
  uint[] ConvertReadDataToU32(IEnumerable<RegClass> regList, IEnumerable<ushort> u16data);

  ushort[] GetBufferedStreamDataPacket();

  double[] ScaleRegData(IEnumerable<RegClass> regList, IEnumerable<uint> uintData);

  void StartBufferedStream(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker);
}
