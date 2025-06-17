// Decompiled with JetBrains decompiler
// Type: adisInterface.ComponentSpi
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using AdisApi;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace adisInterface;

public class ComponentSpi : IRegInterface
{
  private ISpi32Interface m_Spi32;
  private int m_AddrPosition;
  private int m_DataPosition;
  private int m_WriteBitPosition;
  private bool m_WriteBitPolarity;
  private bool m_HasPageWrite;
  private List<uint> txAddr;

  /// <summary>Overloaded constructor which takes an instance of the ISpi32Interface.</summary>
  /// <param name="SpiInterface">The SPI interface to be used.</param>
  public ComponentSpi(ISpi32Interface SpiInterface)
  {
    this.m_HasPageWrite = false;
    this.txAddr = new List<uint>();
    this.m_Spi32 = SpiInterface;
    this.SetDefaultValues();
  }

  private void SetDefaultValues()
  {
    this.m_WriteBitPosition = 15;
    this.m_AddrPosition = 8;
    this.m_DataPosition = 0;
    this.m_WriteBitPolarity = true;
  }

  private uint BuildTxData(uint addr, uint data, bool isWrite)
  {
    addr &= (uint) sbyte.MaxValue;
    data &= (uint) byte.MaxValue;
    uint num = addr << this.m_AddrPosition;
    return (!isWrite ? num | (uint) ((-(!this.m_WriteBitPolarity ? 1 : 0) & 1) << this.WriteBitPosition) : num | data << this.m_DataPosition | (uint) ((-(this.m_WriteBitPolarity ? 1 : 0) & 1) << this.WriteBitPosition)) & (uint) ushort.MaxValue;
  }

  private ushort ParseRxData(uint addr, uint data)
  {
    return ((ulong) addr & 1UL) > 0UL ? (ushort) ((uint) checked ((ushort) (unchecked ((int) (data >> this.m_DataPosition)) & (int) byte.MaxValue)) << 8) : checked ((ushort) (unchecked ((int) (data >> this.m_DataPosition)) & (int) byte.MaxValue));
  }

  /// <summary>Starting position of the 7-bit address field</summary>
  /// <returns></returns>
  public int AddrPosition
  {
    get => this.m_AddrPosition;
    set
    {
      this.m_AddrPosition = !(value > 15 | value < 0) ? value : throw new ArgumentException("Invalid address position. This library supports only 16-bit transfers");
    }
  }

  /// <summary>Starting position of the 8-bit data field</summary>
  /// <returns></returns>
  public int DataPosition
  {
    get => this.m_DataPosition;
    set
    {
      this.m_DataPosition = !(value > 15 | value < 0) ? value : throw new ArgumentException("Invalid data position. This library supports only 16-bit transfers");
    }
  }

  /// <summary>Position of the 1-bit write flag</summary>
  /// <returns></returns>
  public int WriteBitPosition
  {
    get => this.m_WriteBitPosition;
    set
    {
      this.m_WriteBitPosition = !(value > 15 | value < 0) ? value : throw new ArgumentException("Invalid write bit position. This library supports only 16-bit transfers");
    }
  }

  /// <summary>
  /// Write bit polarity.
  /// True -&gt; set bit to 1 to write
  /// False -&gt; set bit to 0 to write
  /// </summary>
  /// <returns></returns>
  public bool WriteBitPolarity
  {
    get => this.m_WriteBitPolarity;
    set => this.m_WriteBitPolarity = value;
  }

  public bool DrActive
  {
    get => this.m_Spi32.DrActive;
    set => this.m_Spi32.DrActive = value;
  }

  public int StreamTimeoutSeconds
  {
    get => this.m_Spi32.StreamTimeoutSeconds;
    set
    {
      this.m_Spi32.StreamTimeoutSeconds = value >= 1 ? value : throw new ArgumentException("Invalid stream timeout time: " + value.ToString());
    }
  }

  public void StartBufferedStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    if (Information.IsNothing((object) numCaptures) | numCaptures < 1U)
      throw new ArgumentException("ERROR: Invalid number of captures provided");
    if (Information.IsNothing((object) addrData) | addrData.Count<AddrDataPair>() == 0)
      throw new ArgumentException("ERROR: Invalid address data array provided");
    if (Information.IsNothing((object) numBuffers) | numBuffers < 1U)
      throw new ArgumentException("ERROR: Invalid number of buffers provided");
    List<uint> WriteData = new List<uint>();
    this.m_HasPageWrite = false;
    this.txAddr.Clear();
    try
    {
      foreach (AddrDataPair addrDataPair in addrData)
      {
        bool isWrite = !Information.IsNothing((object) addrDataPair.data);
        uint data = !isWrite ? 0U : addrDataPair.data.Value;
        if (isWrite & addrDataPair.addr == 0U)
          this.m_HasPageWrite = true;
        else
          WriteData.Add(this.BuildTxData(addrDataPair.addr, data, isWrite));
        this.txAddr.Add(addrDataPair.addr);
      }
    }
    finally
    {
      IEnumerator<AddrDataPair> enumerator;
      enumerator?.Dispose();
    }
    this.m_Spi32.StartBufferedStream((IEnumerable<uint>) WriteData, numCaptures, numBuffers, timeoutSeconds, worker);
  }

  public void StartBufferedStream(
    IEnumerable<uint> addr,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    try
    {
      foreach (uint num in addr)
        addrData.Add(new AddrDataPair()
        {
          addr = num,
          data = new uint?()
        });
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
    this.StartBufferedStream((IEnumerable<AddrDataPair>) addrData, numCaptures, numBuffers, timeoutSeconds, worker);
  }

  public void StartStream(IEnumerable<uint> addr, uint numCaptures)
  {
    this.StartBufferedStream(addr, numCaptures, 1U, this.m_Spi32.StreamTimeoutSeconds, (BackgroundWorker) null);
  }

  public void StopStream() => this.m_Spi32.StopStream();

  public void WriteRegByte(AddrDataPair addrData)
  {
    this.WriteRegByte((IEnumerable<AddrDataPair>) new AddrDataPair[1]
    {
      addrData
    });
  }

  public void WriteRegByte(IEnumerable<AddrDataPair> addrData)
  {
    if (Information.IsNothing((object) addrData) | addrData.Count<AddrDataPair>() == 0)
      throw new ArgumentException("ERROR: Invalid address data array provided");
    List<uint> WriteData = new List<uint>();
    this.m_HasPageWrite = false;
    try
    {
      foreach (AddrDataPair addrDataPair in addrData)
      {
        bool isWrite = !Information.IsNothing((object) addrDataPair.data);
        uint data = !isWrite ? 0U : addrDataPair.data.Value;
        if (isWrite & addrDataPair.addr == 0U)
          this.m_HasPageWrite = true;
        else
          WriteData.Add(this.BuildTxData(addrDataPair.addr, data, isWrite));
      }
    }
    finally
    {
      IEnumerator<AddrDataPair> enumerator;
      enumerator?.Dispose();
    }
    if (WriteData.Count <= 0)
      return;
    this.m_Spi32.TransferArray((IEnumerable<uint>) WriteData, 1U, 1U);
  }

  public void WriteRegByte(uint addr, uint data)
  {
    this.WriteRegByte((IEnumerable<AddrDataPair>) new List<AddrDataPair>()
    {
      new AddrDataPair() { addr = addr, data = new uint?(data) }
    });
  }

  public void WriteRegByte(IEnumerable<uint> addr, IEnumerable<uint> data)
  {
    if (addr.Count<uint>() != data.Count<uint>())
      throw new ArgumentException("ERROR: Addr and Data counts must match for write");
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    int num = addr.Count<uint>();
    int index = 0;
    while (index <= num)
    {
      addrData.Add(new AddrDataPair()
      {
        addr = addr.ElementAtOrDefault<uint>(index),
        data = new uint?(data.ElementAtOrDefault<uint>(index))
      });
      checked { ++index; }
    }
    this.WriteRegByte((IEnumerable<AddrDataPair>) addrData);
  }

  public ushort[] GetBufferedStreamDataPacket()
  {
    uint[] streamDataPacket = this.m_Spi32.GetBufferedStreamDataPacket();
    List<ushort> ushortList1 = new List<ushort>();
    if (Information.IsNothing((object) streamDataPacket))
      return (ushort[]) null;
    List<ushort> ushortList2 = new List<ushort>();
    int index1 = 0;
    if (this.m_HasPageWrite)
    {
      ushortList2.Add((ushort) 0);
      checked { ++index1; }
    }
    uint[] numArray = streamDataPacket;
    int index2 = 0;
    while (index2 < numArray.Length)
    {
      uint data = numArray[index2];
      ushortList2.Add(this.ParseRxData(this.txAddr[index1], data));
      checked { ++index1; }
      checked { ++index2; }
    }
    return ushortList2.ToArray();
  }

  public ushort[] GetStreamDataPacketU16() => this.GetBufferedStreamDataPacket();

  public ushort[] ReadRegArray(IEnumerable<uint> addr)
  {
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    try
    {
      foreach (uint num in addr)
        addrData.Add(new AddrDataPair()
        {
          addr = num,
          data = new uint?()
        });
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
    return this.ReadRegArrayStream((IEnumerable<AddrDataPair>) addrData, 1U, 1U);
  }

  public ushort[] ReadRegArray(IEnumerable<AddrDataPair> addrData, uint numCaptures)
  {
    return this.ReadRegArrayStream(addrData, numCaptures, 1U);
  }

  public ushort[] ReadRegArray(IEnumerable<uint> addr, uint numCaptures)
  {
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    try
    {
      foreach (uint num in addr)
        addrData.Add(new AddrDataPair()
        {
          addr = num,
          data = new uint?()
        });
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
    return this.ReadRegArrayStream((IEnumerable<AddrDataPair>) addrData, numCaptures, 1U);
  }

  public ushort[] ReadRegArrayStream(IEnumerable<uint> addr, uint numCaptures, uint numBuffers)
  {
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    try
    {
      foreach (uint num in addr)
        addrData.Add(new AddrDataPair()
        {
          addr = num,
          data = new uint?()
        });
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
    return this.ReadRegArrayStream((IEnumerable<AddrDataPair>) addrData, numCaptures, numBuffers);
  }

  public ushort[] ReadRegArrayStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers)
  {
    if (Information.IsNothing((object) numCaptures) | numCaptures < 1U)
      throw new ArgumentException("ERROR: Invalid number of captures provided");
    if (Information.IsNothing((object) addrData) | addrData.Count<AddrDataPair>() == 0)
      throw new ArgumentException("ERROR: Invalid address data array provided");
    if (Information.IsNothing((object) numBuffers) | numBuffers < 1U)
      throw new ArgumentException("ERROR: Invalid number of buffers provided");
    List<uint> WriteData = new List<uint>();
    this.m_HasPageWrite = false;
    this.txAddr.Clear();
    try
    {
      foreach (AddrDataPair addrDataPair in addrData)
      {
        bool isWrite = !Information.IsNothing((object) addrDataPair.data);
        uint data = !isWrite ? 0U : addrDataPair.data.Value;
        if (isWrite & addrDataPair.addr == 0U)
          this.m_HasPageWrite = true;
        else
          WriteData.Add(this.BuildTxData(addrDataPair.addr, data, isWrite));
        this.txAddr.Add(addrDataPair.addr);
      }
    }
    finally
    {
      IEnumerator<AddrDataPair> enumerator;
      enumerator?.Dispose();
    }
    uint[] numArray1 = this.m_Spi32.TransferArray((IEnumerable<uint>) WriteData, numCaptures, numBuffers);
    List<ushort> ushortList = new List<ushort>();
    int index1 = 0;
    if (this.m_HasPageWrite)
    {
      ushortList.Add((ushort) 0);
      checked { ++index1; }
    }
    uint[] numArray2 = numArray1;
    int index2 = 0;
    while (index2 < numArray2.Length)
    {
      uint data = numArray2[index2];
      ushortList.Add(this.ParseRxData(this.txAddr[index1], data));
      checked { ++index1; }
      checked { ++index2; }
    }
    return ushortList.ToArray();
  }

  public ushort ReadRegByte(uint addr)
  {
    return this.ParseRxData(addr, this.m_Spi32.Transfer(this.BuildTxData(addr, 0U, false)));
  }

  public ushort ReadRegWord(uint addr)
  {
    return (ushort) ((int) (ushort) ((uint) this.ReadRegByte(addr) << 8) & 65280 | (int) this.ReadRegByte(checked (addr + 1U)));
  }

  /// <summary>Burst mode property. Currently not supported for component sensor interface</summary>
  /// <returns></returns>
  public ushort BurstMode
  {
    get => 0;
    set
    {
      if (value > (ushort) 0)
        throw new ArgumentException("Burst Mode Limited to Zero for this implementation.");
    }
  }

  public void WriteRegWord(uint addr, uint data) => throw new NotImplementedException();

  public void Reset() => throw new NotImplementedException();

  public void Start() => throw new NotImplementedException();
}
