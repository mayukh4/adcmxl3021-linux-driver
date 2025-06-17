// Decompiled with JetBrains decompiler
// Type: RegMapClasses.RegMapExtensions
// Assembly: RegMapClasses, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A5F8C6F-7050-4AF9-8F46-4262EBB69E5D
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\RegMapClasses.dll

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace RegMapClasses;

[StandardModule]
public sealed class RegMapExtensions
{
  public static int SpiTransferCount(this IEnumerable<RegClass> regs)
  {
    IEnumerable<RegClass> source = regs;
    Func<RegClass, int> selector;
    // ISSUE: reference to a compiler-generated field
    if (RegMapExtensions._Closure\u0024__.\u0024I0\u002D0 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector = RegMapExtensions._Closure\u0024__.\u0024I0\u002D0;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      RegMapExtensions._Closure\u0024__.\u0024I0\u002D0 = selector = (Func<RegClass, int>) ([SpecialName] (r) => r.SpiTransferCount);
    }
    return source.Sum<RegClass>(selector);
  }

  public static bool HasSingleBitSet(this byte value)
  {
    return value != (byte) 0 && ((int) value & checked ((int) value - 1)) == 0;
  }

  public static bool HasSingleBitSet(this ushort value)
  {
    return value != (ushort) 0 && ((int) value & (int) checked ((ushort) unchecked ((int) value - 1))) == 0;
  }

  public static bool HasSingleBitSet(this uint value)
  {
    return value != 0U && ((int) value & (int) checked (value - 1U)) == 0;
  }

  public static bool HasSingleBitSet(this ulong value)
  {
    return value != 0UL && ((long) value & (long) checked (value - 1UL)) == 0L;
  }

  public static byte GetMaskedBit(this byte value, byte mask)
  {
    if (!mask.HasSingleBitSet())
      throw new Exception("Mask must contain 1 bit.");
    return ((int) value & (int) mask) == (int) mask ? (byte) 1 : (byte) 0;
  }

  public static ushort GetMaskedBit(this ushort value, ushort mask)
  {
    if (!mask.HasSingleBitSet())
      throw new ArgumentException("Mask must contain 1 bit.");
    return ((int) value & (int) mask) == (int) mask ? (ushort) 1 : (ushort) 0;
  }

  public static uint GetMaskedBit(this uint value, uint mask)
  {
    if (!mask.HasSingleBitSet())
      throw new ArgumentException("Mask must contain 1 bit.");
    return ((int) value & (int) mask) == (int) mask ? 1U : 0U;
  }

  public static ulong GetMaskedBit(this ulong value, ulong mask)
  {
    if (!mask.HasSingleBitSet())
      throw new ArgumentException("Mask must contain 1 bit.");
    return ((long) value & (long) mask) == (long) mask ? 1UL : 0UL;
  }
}
