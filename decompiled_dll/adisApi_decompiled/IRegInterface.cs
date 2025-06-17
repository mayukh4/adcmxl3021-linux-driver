// Decompiled with JetBrains decompiler
// Type: AdisApi.IRegInterface
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace AdisApi;

/// <summary>
/// 
/// </summary>
public interface IRegInterface
{
  /// <summary>
  /// Gets or sets the size of the burst data packet in words.  Zero indicates burst mode inactinve.
  /// </summary>
  ushort BurstMode { get; set; }

  /// <summary>Gets or sets the data ready active bit</summary>
  bool DrActive { get; set; }

  /// <summary>
  /// 
  /// </summary>
  int StreamTimeoutSeconds { get; set; }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  ushort[] GetBufferedStreamDataPacket();

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  ushort[] GetStreamDataPacketU16();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  ushort[] ReadRegArray(IEnumerable<uint> addr);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  ushort[] ReadRegArray(IEnumerable<uint> addr, uint numCaptures);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addrData"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  ushort[] ReadRegArray(IEnumerable<AddrDataPair> addrData, uint numCaptures);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  ushort[] ReadRegArrayStream(IEnumerable<uint> addr, uint numCaptures, uint numBuffers);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addrData"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  ushort[] ReadRegArrayStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  ushort ReadRegByte(uint addr);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <returns></returns>
  ushort ReadRegWord(uint addr);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <param name="timeoutSeconds"></param>
  /// <param name="worker"></param>
  void StartBufferedStream(
    IEnumerable<uint> addr,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker);

  /// <summary>
  /// Starts a buffered stream read, uses the background worker handle to update progress.
  /// </summary>
  /// <param name="addrData">Adresses to read/write.</param>
  /// <param name="numCaptures">Number of captures in each buffer for SDP transfer.</param>
  /// <param name="numBuffers">Number of buffers to Be captured.</param>
  /// <param name="timeoutSeconds">Timeout for transmission of one stream packet.</param>
  /// <param name="worker">BackgroundWorker that routine can report progress to (may be null).</param>
  /// <remarks>
  /// </remarks>
  void StartBufferedStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  void StartStream(IEnumerable<uint> addr, uint numCaptures);

  /// <summary>
  /// 
  /// </summary>
  void StopStream();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addrData"></param>
  void WriteRegByte(IEnumerable<AddrDataPair> addrData);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addrData"></param>
  void WriteRegByte(AddrDataPair addrData);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  void WriteRegByte(IEnumerable<uint> addr, IEnumerable<uint> data);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  void WriteRegByte(uint addr, uint data);

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  [Obsolete("This may be Obsoleted. Please use WriteRegByte instead.")]
  void WriteRegWord(uint addr, uint data);

  /// <summary>
  /// 
  /// </summary>
  void Reset();

  /// <summary>
  /// 
  /// </summary>
  void Start();
}
