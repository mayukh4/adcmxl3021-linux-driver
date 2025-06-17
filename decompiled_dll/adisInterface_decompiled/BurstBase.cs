// Decompiled with JetBrains decompiler
// Type: adisInterface.BurstBase
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using RegMapClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace adisInterface;

/// <summary>
/// Supports reading iSensor device in Burst Mode.  Supports CRC mode for ADiS16448.
/// </summary>
/// <remarks></remarks>
public abstract class BurstBase : IBufferedStreamProducer, IBurstMode
{
  internal DutBase @base;
  private int m_CrcFirstIndex;
  private int m_CrcLastIndex;
  private int m_CrcResultIndex;
  private int m_WordCount;
  private RegClass m_TriggerReg;

  /// <summary>
  /// Gets or sets the index of the first burst data word used in COC calculations.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public int CrcFirstIndex
  {
    get => this.m_CrcFirstIndex;
    set => this.m_CrcFirstIndex = value;
  }

  /// <summary>
  /// Gets or sets the index of the last burst data word used in COC calculations.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public int CrcLastIndex
  {
    get => this.m_CrcLastIndex;
    set => this.m_CrcLastIndex = value;
  }

  /// <summary>Gets or sets the index of the word that contains the CRC result.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public int CrcResultIndex
  {
    get => this.m_CrcResultIndex;
    set => this.m_CrcResultIndex = value;
  }

  /// <summary>
  /// Gets or sets the number of 16 bit words that are transferred during the burst.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public int WordCount
  {
    get => this.m_WordCount;
    set
    {
      this.m_WordCount = !(value < 1 | value > (int) ushort.MaxValue) ? value : throw new ArgumentException($"WordCount must be between 1 and {ushort.MaxValue.ToString()}.");
    }
  }

  /// <summary>Gets or sets register that is used to trigger burst operation.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public RegClass TriggerReg
  {
    get => this.m_TriggerReg;
    set => this.m_TriggerReg = value;
  }

  /// <summary>
  /// Gets a one element array containing a modified trigger reg object to use for aducInterface calls.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  private RegClass[] Trigger
  {
    get
    {
      if (this.TriggerReg == null)
        throw new InvalidOperationException("Trigger reg must be set before performing a burst read Operaton.");
      return new RegClass[1]
      {
        new RegClass()
        {
          Address = this.TriggerReg.Address,
          Page = this.TriggerReg.Page,
          NumBytes = 2U,
          IsReadable = true
        }
      };
    }
  }

  /// <summary>Takes interface out of burst mode by setting burstMode to zero.</summary>
  /// <remarks></remarks>
  private void ClearBurstMode() => this.@base.adis.BurstMode = (ushort) 0;

  /// <summary>Puts interface into burst mode by setting burstMode to match word count.</summary>
  /// <remarks></remarks>
  /// <exception cref="T:System.InvalidOperationException">Thrown if word count has not been set.</exception>
  private void SetupBurstMode()
  {
    this.@base.adis.BurstMode = this.WordCount != 0 ? checked ((ushort) this.WordCount) : throw new InvalidOperationException("WordCount must be set before performing a burst read Operaton.");
  }

  /// <summary>Creates a new instance of the burst class.</summary>
  /// <remarks></remarks>
  public BurstBase()
  {
  }

  /// <summary>Creates a new instance of the burst class.</summary>
  /// <param name="base"></param>
  /// <remarks></remarks>
  public BurstBase(DutBase @base) => this.@base = @base;

  /// <summary>Calculates the CRC of a list of values.</summary>
  /// <param name="values"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public ushort CalculateBurstCrc(IEnumerable<uint> values)
  {
    if (values == null)
      throw new NullReferenceException("values must not be null.");
    if (this.CrcFirstIndex < 0 | this.CrcLastIndex < this.CrcFirstIndex | this.CrcLastIndex >= values.Count<uint>())
      throw new ArgumentException("Invalid firstIndex and/or lastIndex value(s).");
    List<ushort> data = new List<ushort>();
    int crcFirstIndex = this.CrcFirstIndex;
    int crcLastIndex = this.CrcLastIndex;
    int index = crcFirstIndex;
    while (index <= crcLastIndex)
    {
      data.Add(checked ((ushort) values.ElementAtOrDefault<uint>(index)));
      checked { ++index; }
    }
    return this.CalculateDataCrc((IEnumerable<ushort>) data);
  }

  /// <summary>Calculates a CRC using a collection of data words.</summary>
  /// <param name="data"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  internal abstract ushort CalculateDataCrc(IEnumerable<ushort> data);

  public bool VerifyCrc(IEnumerable<uint> values)
  {
    if (this.CrcResultIndex < 0 | this.CrcResultIndex >= values.Count<uint>() | this.CrcResultIndex >= this.CrcFirstIndex & this.CrcResultIndex <= this.CrcLastIndex)
      throw new ArgumentException("Invalid crcIndex value.");
    return (int) values.ElementAtOrDefault<uint>(this.CrcResultIndex) == (int) this.CalculateBurstCrc(values);
  }

  /// <summary>
  /// Reads multiple data words (unsigned, unmasked, no twos-comp) using burst mode.  Multiple stream transfers packets.
  /// </summary>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(uint numCaptures, uint numbuffers)
  {
    try
    {
      this.SetupBurstMode();
      return this.@base.ReadUnsigned((IEnumerable<RegClass>) this.Trigger, numCaptures, numbuffers);
    }
    finally
    {
      this.ClearBurstMode();
    }
  }

  /// <summary>
  /// Reads multiple data words (unsigned, unmasked, no twos-comp) using burst mode.  Multiple Burst Reads.
  /// </summary>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(uint numCaptures) => this.ReadUnsigned(numCaptures, 1U);

  /// <summary>
  /// Reads multiple data words (unsigned, unmasked, no twos-comp) using burst mode.  Single Burst.
  /// </summary>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned() => this.ReadUnsigned(1U, 1U);

  /// <summary>Starts an unbuffered streaming operation.</summary>
  /// <param name="numCaptures">Number of captures in each stream packet.</param>
  /// <remarks></remarks>
  public void StartStream(uint numCaptures)
  {
    this.SetupBurstMode();
    this.@base.StartStream((IEnumerable<RegClass>) this.Trigger, numCaptures);
  }

  /// <summary>Starts a buffered streaming operation.</summary>
  /// <param name="numCaptures">Number of captures in each stream packet.</param>
  /// <remarks></remarks>
  public void StartBufferedStream(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    this.SetupBurstMode();
    try
    {
      this.@base.StartBufferedStream((IEnumerable<RegClass>) this.Trigger, numCaptures, numBuffers, timeoutSeconds, worker);
    }
    finally
    {
      this.ClearBurstMode();
    }
  }

  /// <summary>Gets a stream data packet.</summary>
  /// <returns></returns>
  /// <remarks></remarks>
  public ushort[] GetStreamDataPacketU16() => this.@base.GetStreamDataPacketU16();

  /// <summary>Stops a Streaming Operation</summary>
  /// <remarks></remarks>
  public void StopStream()
  {
    this.@base.StopStream();
    this.ClearBurstMode();
  }

  /// <summary>
  /// Converts 16 bit reg data reads to Uinteger array, processing 16, or 8 bit data based on reg List item numBytes.
  /// </summary>
  /// <param name="regList"></param>
  /// <param name="u16data"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ConvertReadDataToU32(IEnumerable<RegClass> regList, IEnumerable<ushort> u16data)
  {
    return this.@base.ConvertReadDataToU32(regList, u16data);
  }

  /// <summary>Gets a buffered stream data packet.</summary>
  /// <returns></returns>
  /// <remarks></remarks>
  public ushort[] GetBufferedStreamDataPacket() => this.@base.GetBufferedStreamDataPacket();

  /// <summary>Scales data values read from an enumerable collection of registers.</summary>
  /// <param name="regList">Collection of registers that are included in the burst read sequence.</param>
  /// <param name="uintData">Data collected in butse mod read operation.</param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ScaleRegData(IEnumerable<RegClass> regList, IEnumerable<uint> uintData)
  {
    return this.@base.ScaleRegData(regList, uintData);
  }
}
