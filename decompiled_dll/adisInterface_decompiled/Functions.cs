// Decompiled with JetBrains decompiler
// Type: adisInterface.Functions
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;
using System;

#nullable disable
namespace adisInterface;

[StandardModule]
internal sealed class Functions
{
  /// <summary>Performs conversion from raw data to single format</summary>
  /// <param name="Data">Data word read back from SPI interface.</param>
  /// <returns></returns>
  /// <remarks></remarks>
  internal static float DecodeFloat(uint Data)
  {
    return BitConverter.ToSingle(BitConverter.GetBytes(Data), 0);
  }

  /// <summary>Performs conversion from double to raw data</summary>
  /// <param name="value">Value to be encoded.</param>
  /// <returns></returns>
  /// <remarks></remarks>
  internal static uint EncodeFloat(double value)
  {
    float single;
    try
    {
      single = Convert.ToSingle(value);
    }
    catch (OverflowException ex)
    {
      ProjectData.SetProjectError((Exception) ex);
      throw new ArgumentOutOfRangeException("Value is out of range for a 32 bit Float register.", (Exception) ex);
    }
    return Functions.EncodeFloat(single);
  }

  /// <summary>Performs conversion from single to raw data</summary>
  /// <param name="value">Value to be encoded.</param>
  /// <returns></returns>
  /// <remarks></remarks>
  internal static uint EncodeFloat(float value)
  {
    return BitConverter.ToUInt32(BitConverter.GetBytes(value), 0);
  }

  /// <summary>
  /// Masks and performs twos complement decoding on a data word read from a SPI interface. No scaling is performed.
  /// </summary>
  /// <param name="Reg">Reg object provides data length and twos complement flag.</param>
  /// <param name="Data">Data word read back from SPI interface.</param>
  /// <returns></returns>
  /// <remarks></remarks>
  public static long DecodeTwosComp(RegClass Reg, uint Data)
  {
    int readLen = checked ((int) Reg.ReadLen);
    long num1 = ~(-1L << readLen);
    long num2 = (long) Data & num1;
    if (Reg.IsTwosComp && (num2 & 1L << checked (readLen - 1)) != 0L)
      num2 |= ~num1;
    return num2;
  }

  /// <summary>
  /// Returns encoded data for a SPI write operation. Twos Comp data is NOT sign-extended.
  /// </summary>
  /// <param name="Reg">Provides readlength and twos complement flag for encoding.</param>
  /// <param name="data">Data to be encoded.</param>
  /// <returns></returns>
  /// <remarks></remarks>
  public static uint EncodeTwosComp(RegClass Reg, long data)
  {
    int readLen = checked ((int) Reg.ReadLen);
    long num1 = 0;
    long num2 = ~(-1L << readLen);
    uint num3 = checked ((uint) num2);
    if (Reg.IsTwosComp)
    {
      num2 = (long) ~(-1 << checked (readLen - 1));
      num1 = checked (-num2 - 1L);
    }
    if (data < num1 | data > num2)
      throw new ArgumentOutOfRangeException($"Unscaled reg value must be between {num1} and {num2}.");
    return checked ((uint) (data & (long) num3));
  }

  /// <summary>
  /// Returns encoded data for a SPI write operation. Twos Comp data is NOT sign-extended.
  /// </summary>
  /// <param name="Reg">Provides readlength and twos complement flag for encoding.</param>
  /// <param name="data">Data to be encoded.</param>
  /// <returns></returns>
  /// <remarks></remarks>
  public static uint EncodeTwosCompClamped(RegClass Reg, long data)
  {
    int readLen = checked ((int) Reg.ReadLen);
    long num1 = 0;
    long num2 = ~(-1L << readLen);
    uint num3 = checked ((uint) num2);
    if (Reg.IsTwosComp)
    {
      num2 = (long) ~(-1 << checked (readLen - 1));
      num1 = checked (-num2 - 1L);
    }
    if (data < num1)
      data = num1;
    if (data > num2)
      data = num2;
    return checked ((uint) (data & (long) num3));
  }
}
