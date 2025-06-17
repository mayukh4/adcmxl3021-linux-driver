// Decompiled with JetBrains decompiler
// Type: StreamDataLogger.SignedConverter
// Assembly: StreamDataLogger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 328A96D1-45A7-47F9-A7ED-7DBD0C49147E
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.xml

using System;

#nullable disable
namespace StreamDataLogger;

public class SignedConverter
{
  public static short ToSignedByte(ushort value, bool isUpper)
  {
    short signedByte = !isUpper ? checked ((short) ((int) value & (int) byte.MaxValue)) : checked ((short) unchecked ((ushort) ((uint) value >> 8)));
    if ((double) signedByte >= 128.0)
      signedByte = checked ((short) Math.Round(unchecked ((double) signedByte - 256.0)));
    return signedByte;
  }

  public static ushort ToUnsignedByte(ushort value, bool isUpper)
  {
    return !isUpper ? (ushort) checked ((byte) (ushort) ((int) value & (int) byte.MaxValue)) : (ushort) checked ((byte) unchecked ((ushort) ((uint) value >> 8)));
  }

  public static int ToSignedShort(ushort value)
  {
    int signedShort = (int) value;
    if ((double) signedShort >= 32768.0)
      signedShort = checked ((int) Math.Round(unchecked ((double) signedShort - 65536.0)));
    return signedShort;
  }

  public static long ToSignedInt(ushort upper, ushort lower)
  {
    long signedInt = checked ((long) upper << 16 /*0x10*/ + (long) lower);
    if ((double) signedInt >= 2147483648.0)
      signedInt = checked ((long) Math.Round(unchecked ((double) signedInt - 4294967296.0)));
    return signedInt;
  }

  public static ulong ToUnsignedInt(ushort upper, ushort lower)
  {
    return checked ((ulong) upper << 16 /*0x10*/ + (ulong) lower);
  }
}
