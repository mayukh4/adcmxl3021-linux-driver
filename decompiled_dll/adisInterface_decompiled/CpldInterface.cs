// Decompiled with JetBrains decompiler
// Type: adisInterface.CpldInterface
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

public class CpldInterface : DutBase
{
  private int resetDelay;

  /// <summary>Create a new instance of a Dut interface class.</summary>
  /// <param name="adis">Reg interface object to use for dut comminication.</param>
  /// <param name="burstMode">Burst Class to use to support burst mode.  Nothing/null if Dut does not support burst mode.</param>
  public CpldInterface(IRegInterface adis, BurstBase burstMode)
    : base(adis, burstMode)
  {
    this.resetDelay = 700;
    this.DeviceWriteWordSize = 8U;
    this.DeviceAddressIncrement = 1U;
  }

  /// <summary>
  /// Starts a buffered stream capture of registers in regList of numBuffers * numCaptures per buffer.
  /// </summary>
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
    this.SelectDevice();
    try
    {
      this.adis.StartBufferedStream((IEnumerable<uint>) this.MakeReadAddressList(regList), numCaptures, numBuffers, timeoutSeconds, worker);
    }
    catch (AdisApi.TimeoutException ex)
    {
      ProjectData.SetProjectError((Exception) ex);
      ProjectData.ClearProjectError();
    }
  }

  /// <summary>Starts an unbuffered streaming operation.</summary>
  /// <param name="regList">Registers (16 and/or 32 bit) to include in stream.</param>
  /// <param name="numCaptures">Number of captures in each stream packet.</param>
  /// <remarks></remarks>
  public override void StartStream(IEnumerable<RegClass> regList, uint numCaptures)
  {
    this.SelectDevice();
    this.adis.StartStream((IEnumerable<uint>) this.MakeReadAddressList(regList), numCaptures);
  }

  /// <summary>Creates address list, returns Unsigned data.</summary>
  /// <param name="regDataList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public override uint[] WriteReadUnsigned(IEnumerable<RegDataU32> regDataList, uint numCaptures)
  {
    this.SelectDevice();
    IEnumerable<RegDataU32> source1 = regDataList;
    Func<RegDataU32, AddrDataPair> selector1;
    // ISSUE: reference to a compiler-generated field
    if (CpldInterface._Closure\u0024__.\u0024I4\u002D0 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector1 = CpldInterface._Closure\u0024__.\u0024I4\u002D0;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      CpldInterface._Closure\u0024__.\u0024I4\u002D0 = selector1 = (Func<RegDataU32, AddrDataPair>) ([SpecialName] (r) => new AddrDataPair(r.reg.Address, r.dat));
    }
    ushort[] u16data = this.adis.ReadRegArray((IEnumerable<AddrDataPair>) source1.Select<RegDataU32, AddrDataPair>(selector1).ToList<AddrDataPair>(), numCaptures);
    IEnumerable<RegDataU32> source2 = regDataList;
    Func<RegDataU32, RegClass> selector2;
    // ISSUE: reference to a compiler-generated field
    if (CpldInterface._Closure\u0024__.\u0024I4\u002D1 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector2 = CpldInterface._Closure\u0024__.\u0024I4\u002D1;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      CpldInterface._Closure\u0024__.\u0024I4\u002D1 = selector2 = (Func<RegDataU32, RegClass>) ([SpecialName] (r) => r.reg);
    }
    return this.ConvertReadDataToU32(source2.Select<RegDataU32, RegClass>(selector2), (IEnumerable<ushort>) u16data);
  }

  internal override uint[] ReadUnsignedImplementation(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint? numBuffers)
  {
    try
    {
      foreach (RegClass reg in regList)
      {
        if (reg.NumBytes != 1U && reg.NumBytes != 2U && reg.NumBytes != 4U)
          throw new ArgumentException("All registers passed to ReadUnsigned must have 1, 2, or 4 bytes.");
        if (!reg.IsReadable)
          throw new ArgumentException("All registers passed to ReadUnsigned must be readable.");
        if (reg.Page != 0)
          throw new ArgumentException("CpldInterface does not support paged or embedded registers.  Reg.Page must me zero for all registers.");
      }
    }
    finally
    {
      IEnumerator<RegClass> enumerator;
      enumerator?.Dispose();
    }
    this.SelectDevice();
    List<uint> addr = this.MakeReadAddressList(regList);
    ushort[] u16data = !numBuffers.HasValue ? this.adis.ReadRegArray((IEnumerable<uint>) addr, numCaptures) : this.adis.ReadRegArrayStream((IEnumerable<uint>) addr, numCaptures, numBuffers.Value);
    return this.ConvertReadDataToU32(regList, (IEnumerable<ushort>) u16data);
  }

  /// <summary>
  /// Writes page register, writes data list element to corrosponding registers list element.
  /// </summary>
  /// <param name="regDatList"></param>
  /// <remarks></remarks>
  public override void WriteUnsigned(IEnumerable<RegDataU32> regDatList)
  {
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    if (regDatList == null)
      throw new ArgumentNullException("regDataList must not be null.");
    if (regDatList.Count<RegDataU32>() == 0)
      return;
    try
    {
      foreach (RegDataU32 regDat in regDatList)
      {
        if (regDat == null)
          throw new ArgumentNullException("regDataList element must not be null.");
        if (regDat.reg == null)
          throw new ArgumentNullException("reg must not be null.");
        if (!regDat.dat.HasValue)
          throw new ArgumentNullException("dat must not be null.");
        if (!regDat.reg.IsWriteable)
          throw new ApplicationException("Attempted to write a read only register.");
        addrData.AddRange((IEnumerable<AddrDataPair>) this.GetAddrDataPairs(regDat.reg, regDat.dat.Value));
      }
    }
    finally
    {
      IEnumerator<RegDataU32> enumerator;
      enumerator?.Dispose();
    }
    this.SelectDevice();
    this.adis.WriteRegByte((IEnumerable<AddrDataPair>) addrData);
  }
}
