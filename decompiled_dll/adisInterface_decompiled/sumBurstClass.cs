// Decompiled with JetBrains decompiler
// Type: adisInterface.sumBurstClass
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace adisInterface;

/// <summary>
/// Supports reading iSensor device in Burst Mode.  Supports checksum mode for ADiS16470.
/// </summary>
/// <remarks></remarks>
public class sumBurstClass : BurstBase
{
  /// <summary>Creates a new instance of the burst class.</summary>
  /// <remarks></remarks>
  public sumBurstClass()
  {
  }

  /// <summary>Creates a new instance of the burst class.</summary>
  /// <param name="base"></param>
  /// <remarks></remarks>
  public sumBurstClass(DutBase @base)
    : base(@base)
  {
  }

  /// <summary>Calculates a CRC using a collection of bytes.</summary>
  /// <param name="data"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  internal override ushort CalculateDataCrc(IEnumerable<ushort> data)
  {
    if (data == null)
      throw new NullReferenceException("data must not be null.");
    ushort dataCrc = 0;
    try
    {
      foreach (ushort num in data)
      {
        checked { dataCrc += unchecked ((ushort) ((int) num & (int) byte.MaxValue)); }
        checked { dataCrc += unchecked ((ushort) ((int) (ushort) ((uint) num >> 8) & (int) byte.MaxValue)); }
      }
    }
    finally
    {
      IEnumerator<ushort> enumerator;
      enumerator?.Dispose();
    }
    return dataCrc;
  }
}
