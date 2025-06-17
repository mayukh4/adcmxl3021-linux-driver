// Decompiled with JetBrains decompiler
// Type: adisInterface.AdcmInterface2Axis
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using AdisApi;
using Microsoft.VisualBasic;
using RegMapClasses;
using System;
using System.Collections.Generic;

#nullable disable
namespace adisInterface;

/// <summary>Create a new instance of a Dut interface class.</summary>
/// <param name="adis">Reg interface object to use for dut comminication.</param>
public class AdcmInterface2Axis(IRegInterface adis) : AdcmInterfaceBase(adis, (BurstBase) null)
{
  private List<RegClass> _rawRegList;
  private List<RegClass> _rtsRegList;

  /// <summary>Stores the real time sampling frane frame size for</summary>
  /// <returns></returns>
  protected override int RealTimeSamplingFrameSize => 76;

  /// <summary>Returns the register list to use for ADcmXL Real Time Streaming</summary>
  /// <returns></returns>
  public override IEnumerable<RegClass> RealTimeSamplingRegList
  {
    get
    {
      if (this.StreamRawRealTimeSamplingData)
      {
        if (Information.IsNothing((object) this._rawRegList))
        {
          this._rawRegList = new List<RegClass>();
          this._rawRegList.Add(new RegClass()
          {
            Label = "Raw Data",
            Offset = 0.0,
            Scale = 0.0
          });
        }
        return (IEnumerable<RegClass>) this._rawRegList;
      }
      if (Information.IsNothing((object) this._rtsRegList))
      {
        this._rtsRegList = new List<RegClass>();
        this._rtsRegList.Add(new RegClass()
        {
          Label = "X",
          Offset = 0.0,
          Scale = 1.0,
          EvalLabel = "X"
        });
        this._rtsRegList.Add(new RegClass()
        {
          Label = "Y",
          Offset = 0.0,
          Scale = 1.0,
          EvalLabel = "Y"
        });
        this._rtsRegList.Add(new RegClass()
        {
          Label = "Temp",
          Offset = 0.0,
          Scale = 1.0,
          EvalLabel = "Temp"
        });
        this._rtsRegList.Add(new RegClass()
        {
          Label = "Status",
          Offset = 0.0,
          Scale = 1.0,
          EvalLabel = "Status"
        });
        this._rtsRegList.Add(new RegClass()
        {
          Label = "CRC",
          Offset = 0.0,
          Scale = 1.0,
          EvalLabel = "CRC"
        });
        this._rtsRegList.Add(new RegClass()
        {
          Label = "Sequence Number",
          Offset = 0.0,
          Scale = 1.0,
          EvalLabel = "Sequence Number"
        });
      }
      return (IEnumerable<RegClass>) this._rtsRegList;
    }
  }

  /// <summary>Create a new instance of a Dut interface class.</summary>
  /// <param name="adis">Reg interface object to use for dut comminication.</param>
  /// <param name="burstMode">Burst Class to use to support burst mode.  Nothing/null for ADcmXL.</param>
  public AdcmInterface2Axis(IRegInterface adis, BurstBase burstMode)
    : this(adis)
  {
    if (!Information.IsNothing((object) burstMode))
      throw new ArgumentException("burstMode must be null for ADcmXL products");
  }

  /// <summary>
  /// Transforms a single raw RTS Frame into multiColumn data packet for datalogging, adds it to specified list
  /// </summary>
  /// <param name="rtsFrame"></param>
  /// <param name="list"></param>
  protected override void AddTransletedRtsFrameToList(ushort[] rtsFrame, List<ushort> list)
  {
    List<ushort> ushortList = new List<ushort>();
    int num = 0;
    do
    {
      list.Add(rtsFrame[checked (num + 9)]);
      list.Add(rtsFrame[checked (num + 41)]);
      list.Add(rtsFrame[73]);
      list.Add(rtsFrame[74]);
      list.Add(rtsFrame[75]);
      list.Add(rtsFrame[8]);
      checked { ++num; }
    }
    while (num <= 63 /*0x3F*/);
  }
}
