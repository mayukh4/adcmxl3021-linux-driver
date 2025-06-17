// Decompiled with JetBrains decompiler
// Type: adisInterface.AdcmInterfaceBase
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using AdisApi;
using Microsoft.VisualBasic;
using RegMapClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace adisInterface;

/// <summary>
/// Interface class for ADcmXL family.
/// Adds ADcmXL specific burst functionality to paged register base class.
/// </summary>
public abstract class AdcmInterfaceBase : PagedDutBase
{
  /// <summary>
  /// Selects whether GetBufferedStreamDataPacket returns raw or translated data.  Default is False.
  /// </summary>
  /// <returns></returns>
  public bool StreamRawRealTimeSamplingData { get; set; }

  /// <summary>
  /// Selects whether to swap words in each word read in real time sampling mode.  Default is True.
  /// </summary>
  /// <returns></returns>
  public bool SwapRealTimeSamplingDataBytes { get; set; }

  /// <summary>Stores the real time sampling frane frame size for</summary>
  /// <returns></returns>
  protected abstract int RealTimeSamplingFrameSize { get; }

  /// <summary>Returns the register list to use for ADcmXL Real Time Streaming</summary>
  /// <returns></returns>
  public abstract IEnumerable<RegClass> RealTimeSamplingRegList { get; }

  /// <summary>Create a new instance of a Dut interface class.</summary>
  /// <param name="adis">Reg interface object to use for dut comminication.</param>
  /// <param name="burstMode">Burst Class to use to support burst mode.  Nothing/null for ADcmXL.</param>
  public AdcmInterfaceBase(IRegInterface adis, BurstBase burstMode)
    : base(adis, burstMode)
  {
    this.StreamRawRealTimeSamplingData = false;
    this.SwapRealTimeSamplingDataBytes = true;
    this.DeviceAddressIncrement = 1U;
    this.DeviceWriteWordSize = 8U;
    if (!Information.IsNothing((object) burstMode))
      throw new ArgumentException("burstMode must be null for ADcmXL products");
  }

  /// <summary>
  /// Starts the buffered stream capture process (nSamples of RegList = numBuffers * numCaptures per buffer)
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
    this.WritePageRegister(regList.ElementAtOrDefault<RegClass>(0).Page);
    this.adis.StartBufferedStream((IEnumerable<uint>) this.MakeReadAddressList(regList), numCaptures, numBuffers, timeoutSeconds, worker);
  }

  /// <summary>Starts a streaming operation.</summary>
  /// <param name="regList">Registers (16 and/or 32 bit) to include in stream.</param>
  /// <param name="numCaptures">Number of captures in each stream packet.</param>
  /// <remarks></remarks>
  public override void StartStream(IEnumerable<RegClass> regList, uint numCaptures)
  {
    this.WritePageRegister(regList.ElementAtOrDefault<RegClass>(0).Page);
    this.adis.StartStream((IEnumerable<uint>) this.MakeReadAddressList(regList), numCaptures);
  }

  /// <summary>Returns the next packet from the buffer, as transleted for datalogging.</summary>
  /// <returns></returns>
  ushort[] IBufferedStreamProducer.GetBufferedStreamDataPacket()
  {
    List<ushort> list = new List<ushort>();
    ushort[] streamDataPacket1 = this.adis.GetBufferedStreamDataPacket();
    if (Information.IsNothing((object) streamDataPacket1))
      return streamDataPacket1;
    int num1 = ((IEnumerable<ushort>) streamDataPacket1).Count<ushort>() / this.RealTimeSamplingFrameSize;
    if (((IEnumerable<ushort>) streamDataPacket1).Count<ushort>() % this.RealTimeSamplingFrameSize != 0 | ((IEnumerable<ushort>) streamDataPacket1).Count<ushort>() == 0)
      throw new Exception("Invalid packet size received.");
    if (this.SwapRealTimeSamplingDataBytes)
    {
      int num2 = checked (((IEnumerable<ushort>) streamDataPacket1).Count<ushort>() - 1);
      int index = 0;
      while (index <= num2)
      {
        streamDataPacket1[index] = (ushort) ((int) (ushort) ((uint) streamDataPacket1[index] << 8) | (int) (ushort) ((uint) streamDataPacket1[index] >> 8));
        checked { ++index; }
      }
    }
    ushort[] streamDataPacket2;
    if (this.StreamRawRealTimeSamplingData)
    {
      streamDataPacket2 = streamDataPacket1;
    }
    else
    {
      int num3 = checked (num1 - 1);
      int num4 = 0;
      while (num4 <= num3)
      {
        this.AddTransletedRtsFrameToList(((IEnumerable<ushort>) streamDataPacket1).Skip<ushort>(checked (num4 * this.RealTimeSamplingFrameSize)).Take<ushort>(this.RealTimeSamplingFrameSize).ToArray<ushort>(), list);
        checked { ++num4; }
      }
      streamDataPacket2 = list.ToArray();
    }
    return streamDataPacket2;
  }

  /// <summary>
  /// Transforms a raw RTS Frame into multiColumn data packet for datalogging, adds it to specified list
  /// </summary>
  /// <param name="rtsFrame"></param>
  /// <param name="list"></param>
  protected abstract void AddTransletedRtsFrameToList(ushort[] rtsFrame, List<ushort> list);
}
