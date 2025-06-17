// Decompiled with JetBrains decompiler
// Type: adisInterface.PagedDutBase
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using AdisApi;
using RegMapClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace adisInterface;

/// <summary>
/// Base class for devices with paged register access.  Builds pageing functionality on to DutBase.
/// Does not implement streaming routines, this is left fro inheriting classes.
/// </summary>
public abstract class PagedDutBase : DutBase
{
  protected List<AddrDataPair> StreamAddrDataList;
  private int resetDelay;
  private RegClass m_PageReg;

  /// <summary>Create a new instance of a Dut interface class.</summary>
  /// <param name="adis">Reg interface object to use for dut communication.</param>
  /// <param name="burstMode">Burst Class to use to support burst mode.  Nothing/null if Dut does not support burst mode.</param>
  public PagedDutBase(IRegInterface adis, BurstBase burstMode)
    : base(adis, burstMode)
  {
    this.StreamAddrDataList = (List<AddrDataPair>) null;
    this.resetDelay = 700;
    this.m_PageReg = new RegClass()
    {
      Address = 0U,
      NumBytes = 1U
    };
  }

  /// <summary>
  /// RegClass to describe page register to be used.  Defaults to reg with adress of 0 and length of 8 bits.
  /// Set this property to describe register if a non-zero address or non 8 bit write is necessary.
  /// </summary>
  /// <returns></returns>
  public RegClass PageReg
  {
    get => this.m_PageReg;
    set => this.m_PageReg = value;
  }

  /// <summary>Returns the current memory page.</summary>
  /// <returns></returns>
  /// <remarks></remarks>
  public int ReadPageRegister() => checked ((int) this.ReadUnsigned(this.PageReg));

  /// <summary>Sets the memory page.</summary>
  /// <param name="page"></param>
  /// <remarks></remarks>
  public void WritePageRegister(uint page)
  {
    this.SelectDevice();
    this.adis.WriteRegByte((IEnumerable<AddrDataPair>) this.GetPageRegPairs(page));
  }

  /// <summary>Writes page, creates address list, returns Unsigned data.</summary>
  /// <param name="regDataList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public override uint[] WriteReadUnsigned(IEnumerable<RegDataU32> regDataList, uint numCaptures)
  {
    this.WritePageRegister(regDataList.ElementAtOrDefault<RegDataU32>(0).reg.Page);
    IEnumerable<RegDataU32> source1 = regDataList;
    Func<RegDataU32, AddrDataPair> selector1;
    // ISSUE: reference to a compiler-generated field
    if (PagedDutBase._Closure\u0024__.\u0024I9\u002D0 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector1 = PagedDutBase._Closure\u0024__.\u0024I9\u002D0;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      PagedDutBase._Closure\u0024__.\u0024I9\u002D0 = selector1 = (Func<RegDataU32, AddrDataPair>) ([SpecialName] (r) => new AddrDataPair(r.reg.Address, r.dat));
    }
    ushort[] u16data = this.adis.ReadRegArray((IEnumerable<AddrDataPair>) source1.Select<RegDataU32, AddrDataPair>(selector1).ToList<AddrDataPair>(), numCaptures);
    IEnumerable<RegDataU32> source2 = regDataList;
    Func<RegDataU32, RegClass> selector2;
    // ISSUE: reference to a compiler-generated field
    if (PagedDutBase._Closure\u0024__.\u0024I9\u002D1 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector2 = PagedDutBase._Closure\u0024__.\u0024I9\u002D1;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      PagedDutBase._Closure\u0024__.\u0024I9\u002D1 = selector2 = (Func<RegDataU32, RegClass>) ([SpecialName] (r) => r.reg);
    }
    return this.ConvertReadDataToU32(source2.Select<RegDataU32, RegClass>(selector2), (IEnumerable<ushort>) u16data);
  }

  internal override uint[] ReadUnsignedImplementation(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint? numBuffers)
  {
    IEnumerable<RegClass> source1 = regList;
    Func<RegClass, bool> predicate;
    // ISSUE: reference to a compiler-generated field
    if (PagedDutBase._Closure\u0024__.\u0024I10\u002D0 != null)
    {
      // ISSUE: reference to a compiler-generated field
      predicate = PagedDutBase._Closure\u0024__.\u0024I10\u002D0;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      PagedDutBase._Closure\u0024__.\u0024I10\u002D0 = predicate = (Func<RegClass, bool>) ([SpecialName] (r) => !r.IsReadable);
    }
    if (source1.Where<RegClass>(predicate).Any<RegClass>())
      throw new ArgumentException("All registers passed to ReadUnsigned must be readable.");
    uint num = checked (numBuffers.HasValue ? numBuffers.GetValueOrDefault() : 1U * numCaptures);
    IEnumerable<RegClass> source2 = regList;
    Func<RegClass, uint> selector;
    // ISSUE: reference to a compiler-generated field
    if (PagedDutBase._Closure\u0024__.\u0024I10\u002D1 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector = PagedDutBase._Closure\u0024__.\u0024I10\u002D1;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      PagedDutBase._Closure\u0024__.\u0024I10\u002D1 = selector = (Func<RegClass, uint>) ([SpecialName] (r) => r.Page);
    }
    if (source2.Select<RegClass, uint>(selector).Distinct<uint>().Count<uint>() > 1 || num == 1U)
    {
      this.SelectDevice();
      return this.ReadUnsignedMultiPage(regList, numCaptures, numBuffers);
    }
    this.WritePageRegister(regList.ElementAtOrDefault<RegClass>(0).Page);
    return this.ReadUnsignedSinglePage(regList, numCaptures, numBuffers);
  }

  private uint[] ReadUnsignedMultiPage(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint? numBuffers)
  {
    List<AddrDataPair> addrDataPairList = this.MakeReadWriteAddrDataList(regList);
    ushort[] rawData = !numBuffers.HasValue ? this.adis.ReadRegArray((IEnumerable<AddrDataPair>) addrDataPairList, numCaptures) : this.adis.ReadRegArrayStream((IEnumerable<AddrDataPair>) addrDataPairList, numCaptures, numBuffers.Value);
    List<ushort> u16data = PagedDutBase.RemovePageWriteShiftData(addrDataPairList, rawData);
    return this.ConvertReadDataToU32(regList, (IEnumerable<ushort>) u16data);
  }

  protected static List<ushort> RemovePageWriteShiftData(
    List<AddrDataPair> addrDataList,
    ushort[] rawData)
  {
    List<ushort> ushortList = (List<ushort>) null;
    if (rawData != null)
    {
      ushortList = new List<ushort>();
      int num = checked (((IEnumerable<ushort>) rawData).Count<ushort>() - 1);
      int index = 0;
      while (index <= num)
      {
        if (!addrDataList[index % addrDataList.Count].data.HasValue)
          ushortList.Add(rawData[index]);
        checked { ++index; }
      }
    }
    return ushortList;
  }

  /// <summary>
  /// Makes an address/data list for multi page reads.  Usable for streaming or non streaming reads.
  /// </summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  protected List<AddrDataPair> MakeReadWriteAddrDataList(IEnumerable<RegClass> regList)
  {
    uint? nullable = new uint?();
    List<AddrDataPair> addrDataPairList = new List<AddrDataPair>();
    try
    {
      foreach (RegClass reg in regList)
      {
        if (!reg.IsReadable)
          throw new Exception("Attempt made to read a non-readable register.");
        if (!nullable.HasValue || (int) reg.Page != (int) nullable.Value)
        {
          nullable = new uint?(reg.Page);
          addrDataPairList.AddRange((IEnumerable<AddrDataPair>) this.GetPageRegPairs(nullable.Value));
        }
        addrDataPairList.Add(new AddrDataPair(reg.Address, new uint?()));
        if (reg.NumBytes == 4U)
          addrDataPairList.Add(new AddrDataPair(checked (reg.Address + (uint) Math.Round(unchecked (2.0 / (double) this.DeviceAddressIncrement))), new uint?()));
      }
    }
    finally
    {
      IEnumerator<RegClass> enumerator;
      enumerator?.Dispose();
    }
    return addrDataPairList;
  }

  private uint[] ReadUnsignedSinglePage(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint? numBuffers)
  {
    IEnumerable<RegClass> source1 = regList;
    Func<RegClass, uint> selector;
    // ISSUE: reference to a compiler-generated field
    if (PagedDutBase._Closure\u0024__.\u0024I14\u002D0 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector = PagedDutBase._Closure\u0024__.\u0024I14\u002D0;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      PagedDutBase._Closure\u0024__.\u0024I14\u002D0 = selector = (Func<RegClass, uint>) ([SpecialName] (r) => r.Page);
    }
    Debug.Assert(source1.Select<RegClass, uint>(selector).Distinct<uint>().Count<uint>() == 1, "all items must be on one page.");
    IEnumerable<RegClass> source2 = regList;
    Func<RegClass, bool> predicate;
    // ISSUE: reference to a compiler-generated field
    if (PagedDutBase._Closure\u0024__.\u0024I14\u002D1 != null)
    {
      // ISSUE: reference to a compiler-generated field
      predicate = PagedDutBase._Closure\u0024__.\u0024I14\u002D1;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      PagedDutBase._Closure\u0024__.\u0024I14\u002D1 = predicate = (Func<RegClass, bool>) ([SpecialName] (r) => r.IsReadable);
    }
    Debug.Assert(source2.All<RegClass>(predicate), "non readable register found.");
    List<uint> addr = new List<uint>();
    try
    {
      foreach (RegClass reg in regList)
      {
        addr.Add(reg.Address);
        if (reg.NumBytes == 4U)
          addr.Add(checked (reg.Address + (uint) Math.Round(unchecked (2.0 / (double) this.DeviceAddressIncrement))));
      }
    }
    finally
    {
      IEnumerator<RegClass> enumerator;
      enumerator?.Dispose();
    }
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
      throw new ArgumentNullException("Parameters must not be null.");
    if (regDatList.Count<RegDataU32>() == 0)
      return;
    IEnumerable<RegDataU32> source = regDatList;
    Func<RegDataU32, bool> selector;
    // ISSUE: reference to a compiler-generated field
    if (PagedDutBase._Closure\u0024__.\u0024I15\u002D0 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector = PagedDutBase._Closure\u0024__.\u0024I15\u002D0;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      PagedDutBase._Closure\u0024__.\u0024I15\u002D0 = selector = (Func<RegDataU32, bool>) ([SpecialName] (r) => r.dat.HasValue);
    }
    if (source.Select<RegDataU32, bool>(selector).Contains<bool>(false))
      throw new ArgumentException("All regDataList elements must have values.");
    uint page = regDatList.ElementAtOrDefault<RegDataU32>(0).reg.Page;
    addrData.AddRange((IEnumerable<AddrDataPair>) this.GetPageRegPairs(page));
    int num = checked (regDatList.Count<RegDataU32>() - 1);
    int index = 0;
    while (index <= num)
    {
      if (regDatList.ElementAtOrDefault<RegDataU32>(index).reg == null)
        throw new ArgumentNullException("reg must not be null.");
      if (!regDatList.ElementAtOrDefault<RegDataU32>(index).reg.IsWriteable)
        throw new Exception("Attempted to write a read only register.");
      if ((int) regDatList.ElementAtOrDefault<RegDataU32>(index).reg.Page != (int) page)
      {
        page = regDatList.ElementAtOrDefault<RegDataU32>(index).reg.Page;
        addrData.AddRange((IEnumerable<AddrDataPair>) this.GetPageRegPairs(page));
      }
      addrData.AddRange((IEnumerable<AddrDataPair>) this.GetAddrDataPairs(regDatList.ElementAtOrDefault<RegDataU32>(index).reg, regDatList.ElementAtOrDefault<RegDataU32>(index).dat.Value));
      checked { ++index; }
    }
    this.SelectDevice();
    this.adis.WriteRegByte((IEnumerable<AddrDataPair>) addrData);
  }

  /// <summary>Returns a list of address data pairs for the page register.</summary>
  /// <param name="page"></param>
  /// <returns></returns>
  private List<AddrDataPair> GetPageRegPairs(uint page)
  {
    return this.GetAddrDataPairs(this.PageReg, page);
  }

  /// <summary>nSamples of RegList = numBuffers * numCaptures per buffer</summary>
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
    IEnumerable<RegClass> source = regList;
    Func<RegClass, uint> selector;
    // ISSUE: reference to a compiler-generated field
    if (PagedDutBase._Closure\u0024__.\u0024I17\u002D0 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector = PagedDutBase._Closure\u0024__.\u0024I17\u002D0;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      PagedDutBase._Closure\u0024__.\u0024I17\u002D0 = selector = (Func<RegClass, uint>) ([SpecialName] (r) => r.Page);
    }
    if (source.Select<RegClass, uint>(selector).Distinct<uint>().Count<uint>().Equals(1))
    {
      List<uint> addr = this.MakeReadAddressList(regList);
      this.WritePageRegister(regList.ElementAtOrDefault<RegClass>(0).Page);
      this.StreamAddrDataList = (List<AddrDataPair>) null;
      this.adis.StartBufferedStream((IEnumerable<uint>) addr, numCaptures, numBuffers, timeoutSeconds, worker);
    }
    else
    {
      List<AddrDataPair> addrData = this.MakeReadWriteAddrDataList(regList);
      this.SelectDevice();
      this.StreamAddrDataList = this.MakeReadWriteAddrDataList(regList);
      this.adis.StartBufferedStream((IEnumerable<AddrDataPair>) addrData, numCaptures, numBuffers, timeoutSeconds, worker);
    }
  }

  /// <summary>Gets buffered stream data packet.</summary>
  /// <returns></returns>
  /// <remarks></remarks>
  ushort[] IBufferedStreamProducer.GetBufferedStreamDataPacket()
  {
    ushort[] rawData = this.adis.GetBufferedStreamDataPacket();
    if (rawData != null && this.StreamAddrDataList != null)
      rawData = PagedDutBase.RemovePageWriteShiftData(this.StreamAddrDataList, rawData).ToArray();
    return rawData;
  }

  /// <summary>Starts a streaming operation.</summary>
  /// <param name="regList">Registers (16 and/or 32 bit) to include in stream.</param>
  /// <param name="numCaptures">Number of captures in each stream packet.</param>
  /// <remarks></remarks>
  [Obsolete("This will be obsoleted. Please use StartBufferedStream instead.  This routine does not support milti-page reads.", false)]
  public override void StartStream(IEnumerable<RegClass> regList, uint numCaptures)
  {
    this.WritePageRegister(regList.ElementAtOrDefault<RegClass>(0).Page);
    this.adis.StartStream((IEnumerable<uint>) this.MakeReadAddressList(regList), numCaptures);
  }
}
