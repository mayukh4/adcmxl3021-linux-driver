// Decompiled with JetBrains decompiler
// Type: adisInterface.aducInterface
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using AdisApi;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace adisInterface;

public class aducInterface : DutBase
{
  private int resetDelay;

  /// <summary>Create a new instance of a Dut interface class.</summary>
  /// <param name="adis">Reg interface object to use for dut comminication.</param>
  /// <param name="burstMode">Burst Class to use to support burst mode.  Nothing/null if Dut does not support burst mode.</param>
  public aducInterface(IRegInterface adis, BurstBase burstMode)
    : base(adis, burstMode)
  {
    this.resetDelay = 500;
    this.DeviceAddressIncrement = 1U;
    this.DeviceWriteWordSize = 8U;
  }

  /// <summary>This overload requires an IEnumerable(Of RegDataU32 pairs)</summary>
  /// <param name="RegandDataList"></param>
  /// <remarks></remarks>
  public override void WriteUnsigned(IEnumerable<RegDataU32> RegAndDataList)
  {
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    try
    {
      foreach (RegDataU32 regAndData in RegAndDataList)
      {
        if (regAndData.reg.IsEmbedded)
        {
          List<AddrDataPair> addrDataPairList = new List<AddrDataPair>();
          List<AddrDataPair> memWordDataPairs = this.GetEmbMemWordDataPairs(regAndData.reg, regAndData.dat.Value);
          addrData.AddRange((IEnumerable<AddrDataPair>) memWordDataPairs);
        }
        else
          addrData.AddRange((IEnumerable<AddrDataPair>) this.GetAddrDataPairs(regAndData.reg, regAndData.dat.Value));
      }
    }
    finally
    {
      IEnumerator<RegDataU32> enumerator;
      enumerator?.Dispose();
    }
    this.adis.WriteRegByte((IEnumerable<AddrDataPair>) addrData);
  }

  /// <summary>
  /// Writes a Word; low byte to the specified address, high byte is written to the address + 1
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="dat"></param>
  /// <remarks></remarks>
  private void WriteEmbeddedWord(RegClass reg, uint dat)
  {
    this.Write_EmbMemAddr(reg);
    uint data1 = checked ((uint) ((long) dat & (long) byte.MaxValue));
    this.adis.WriteRegByte(reg.Address, data1);
    this.Write_EmbMemAddr(reg, 1U);
    uint data2 = checked ((uint) (((long) dat & 65280L) >> 8));
    this.adis.WriteRegByte(checked ((uint) ((long) reg.Address + 1L)), data2);
  }

  private void Write_EmbMemAddr(RegClass reg, uint byteNum = 0)
  {
    uint page = reg.Page;
    uint num = checked (reg.AuxAddress + byteNum);
    this.adis.WriteRegByte(reg.Page, checked (reg.AuxAddress + byteNum));
  }

  /// <summary>Creates 4 address byte, data byte pairs. For Embedded Register write.</summary>
  /// <param name="reg"></param>
  /// <param name="dat"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  private List<AddrDataPair> GetEmbMemWordDataPairs(RegClass reg, uint dat)
  {
    List<AddrDataPair> memWordDataPairs = new List<AddrDataPair>();
    memWordDataPairs.Add(new AddrDataPair()
    {
      addr = reg.Page,
      data = new uint?(checked ((uint) ((long) reg.AuxAddress + 0L)))
    });
    AddrDataPair addrDataPair1 = new AddrDataPair();
    uint num1 = checked ((uint) ((long) dat & (long) byte.MaxValue));
    addrDataPair1.addr = reg.Address;
    addrDataPair1.data = new uint?(num1);
    memWordDataPairs.Add(addrDataPair1);
    memWordDataPairs.Add(new AddrDataPair()
    {
      addr = reg.Page,
      data = new uint?(checked ((uint) ((long) reg.AuxAddress + 1L)))
    });
    AddrDataPair addrDataPair2 = new AddrDataPair();
    uint num2 = checked ((uint) (((long) dat & 65280L) >> 8));
    addrDataPair2.addr = checked ((uint) ((long) reg.Address + 1L));
    addrDataPair2.data = new uint?(num2);
    memWordDataPairs.Add(addrDataPair2);
    return memWordDataPairs;
  }

  /// <summary>Internal method called by all Public ReadUnsigned overloads.</summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  internal override uint[] ReadUnsignedImplementation(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint? numBuffers)
  {
    bool flag = false;
    List<uint> uintList = new List<uint>();
    ushort[] numArray1 = new ushort[0];
    try
    {
      foreach (RegClass reg in regList)
      {
        if (reg.NumBytes != 1U && reg.NumBytes != 2U && reg.NumBytes != 4U)
          throw new ArgumentException("All registers passed to ReadUnsigned must have 1, 2, or 4 bytes.");
        if (!reg.IsReadable)
          throw new ArgumentException("All registers passed to ReadUnsigned must be readable.");
        if (reg.IsEmbedded)
          flag = true;
      }
    }
    finally
    {
      IEnumerator<RegClass> enumerator;
      enumerator?.Dispose();
    }
    this.SelectDevice();
    if (numBuffers.HasValue)
    {
      if (flag)
        throw new ArgumentException("ADUCinterface error Buffers > 0 and RegList contains embedded registers.");
      numArray1 = this.adis.ReadRegArrayStream((IEnumerable<uint>) this.MakeReadAddressList(regList), numCaptures, numBuffers.Value);
    }
    else if (flag)
    {
      int num1 = checked ((int) numCaptures);
      int num2 = 1;
      while (num2 <= num1)
      {
        ushort[] numArray2 = this.ReadEmbedded(regList);
        int length = numArray1.Length;
        numArray1 = (ushort[]) Utils.CopyArray((Array) numArray1, (Array) new ushort[checked (numArray1.Length + (numArray2.Length - 1) + 1)]);
        numArray2.CopyTo((Array) numArray1, length);
        checked { ++num2; }
      }
    }
    else
      numArray1 = this.adis.ReadRegArray((IEnumerable<uint>) this.MakeReadAddressList(regList), numCaptures);
    return this.ConvertReadDataToU32(regList, (IEnumerable<ushort>) numArray1);
  }

  /// <summary>Write the embedded Pointer then Read a data word, for each list member.</summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  private ushort[] ReadEmbedded(IEnumerable<RegClass> regList)
  {
    ushort[] numArray = new ushort[checked (regList.Count<RegClass>() - 1 + 1)];
    try
    {
      foreach (RegClass reg in regList)
      {
        this.adis.WriteRegByte(reg.Page, reg.AuxAddress);
        uint num = (uint) this.adis.ReadRegWord(reg.Address);
        int index;
        numArray[index] = checked ((ushort) ((long) num & (long) ushort.MaxValue));
        checked { ++index; }
      }
    }
    finally
    {
      IEnumerator<RegClass> enumerator;
      enumerator?.Dispose();
    }
    return numArray;
  }

  /// <summary>Starts a buffered streaming operation.</summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <param name="timeoutSeconds"></param>
  /// <param name="worker"></param>
  /// <remarks></remarks>
  public override void StartBufferedStream(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    this.adis.StartBufferedStream((IEnumerable<uint>) this.MakeReadAddressList(regList), numCaptures, numBuffers, timeoutSeconds, worker);
  }

  /// <summary>Starts a streaming operation.</summary>
  /// <param name="regList">Registers (16 and/ot 32 bit) to include in stream.</param>
  /// <param name="numCaptures">Number of captures in each stream packet.</param>
  /// <remarks></remarks>
  public override void StartStream(IEnumerable<RegClass> regList, uint numCaptures)
  {
    this.SelectDevice();
    this.adis.StartStream((IEnumerable<uint>) this.MakeReadAddressList(regList), numCaptures);
  }

  /// <summary>Writes page, creates address list, returns Unsigned data.</summary>
  /// <param name="regDataList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public override uint[] WriteReadUnsigned(IEnumerable<RegDataU32> regDataList, uint numCaptures)
  {
    IEnumerable<RegDataU32> source1 = regDataList;
    Func<RegDataU32, AddrDataPair> selector1;
    // ISSUE: reference to a compiler-generated field
    if (aducInterface._Closure\u0024__.\u0024I10\u002D0 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector1 = aducInterface._Closure\u0024__.\u0024I10\u002D0;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      aducInterface._Closure\u0024__.\u0024I10\u002D0 = selector1 = (Func<RegDataU32, AddrDataPair>) ([SpecialName] (r) => new AddrDataPair(r.reg.Address, r.dat));
    }
    ushort[] u16data = this.adis.ReadRegArray((IEnumerable<AddrDataPair>) source1.Select<RegDataU32, AddrDataPair>(selector1).ToList<AddrDataPair>(), numCaptures);
    IEnumerable<RegDataU32> source2 = regDataList;
    Func<RegDataU32, RegClass> selector2;
    // ISSUE: reference to a compiler-generated field
    if (aducInterface._Closure\u0024__.\u0024I10\u002D1 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector2 = aducInterface._Closure\u0024__.\u0024I10\u002D1;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      aducInterface._Closure\u0024__.\u0024I10\u002D1 = selector2 = (Func<RegDataU32, RegClass>) ([SpecialName] (r) => r.reg);
    }
    return this.ConvertReadDataToU32(source2.Select<RegDataU32, RegClass>(selector2), (IEnumerable<ushort>) u16data);
  }
}
