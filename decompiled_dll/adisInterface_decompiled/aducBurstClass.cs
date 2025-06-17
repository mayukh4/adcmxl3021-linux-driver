// Decompiled with JetBrains decompiler
// Type: adisInterface.aducBurstClass
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace adisInterface;

/// <summary>
/// Supports reading iSensor device in Burst Mode.  Supports CRC mode for ADiS16448.
/// </summary>
/// <remarks></remarks>
public class aducBurstClass : BurstBase
{
  /// <summary>Creates a new instance of the burst class.</summary>
  /// <remarks></remarks>
  public aducBurstClass()
  {
  }

  /// <summary>Creates a new instance of the burst class.</summary>
  /// <param name="base"></param>
  /// <remarks></remarks>
  public aducBurstClass(DutBase @base)
    : base(@base)
  {
  }

  /// <summary>Calculates a CRC using a collection of bytes.</summary>
  /// <param name="data"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  internal override ushort CalculateDataCrc(IEnumerable<ushort> data)
  {
    List<byte> byteList = new List<byte>();
    try
    {
      foreach (ushort num in data)
      {
        byteList.Add(checked ((byte) ((int) num & (int) byte.MaxValue)));
        byteList.Add(checked ((byte) ((int) unchecked ((ushort) ((uint) num >> 8)) & (int) byte.MaxValue)));
      }
    }
    finally
    {
      IEnumerator<ushort> enumerator;
      enumerator?.Dispose();
    }
    return this.CalculateByteCrc((IEnumerable<byte>) byteList);
  }

  private ushort CalculateByteCrc(IEnumerable<byte> bytes)
  {
    uint num1 = (uint) ushort.MaxValue;
    if (bytes == null)
      throw new NullReferenceException("bytes must not be null.");
    IEnumerator<uint> enumerator;
    try
    {
      IEnumerable<byte> source = bytes;
      Func<byte, uint> selector;
      // ISSUE: reference to a compiler-generated field
      if (aducBurstClass._Closure\u0024__.\u0024I3\u002D0 != null)
      {
        // ISSUE: reference to a compiler-generated field
        selector = aducBurstClass._Closure\u0024__.\u0024I3\u002D0;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        aducBurstClass._Closure\u0024__.\u0024I3\u002D0 = selector = (Func<byte, uint>) ([SpecialName] (b) => (uint) b);
      }
      enumerator = source.Select<byte, uint>(selector).GetEnumerator();
label_11:
      if (enumerator.MoveNext())
      {
        uint current = enumerator.Current;
        int num2 = 0;
        do
        {
          if (((int) num1 & 1 ^ (int) current & 1) != 0)
            num1 = num1 >> 1 ^ 4129U;
          else
            num1 >>= 1;
          current >>= 1;
          checked { ++num2; }
        }
        while (num2 <= 7);
        goto label_11;
      }
    }
    finally
    {
      enumerator?.Dispose();
    }
    uint num3 = ~num1;
    uint num4 = num3;
    return checked ((ushort) ((unchecked ((int) num3) & (int) byte.MaxValue) << 8 | unchecked ((int) (num4 >> 8)) & (int) byte.MaxValue));
  }
}
