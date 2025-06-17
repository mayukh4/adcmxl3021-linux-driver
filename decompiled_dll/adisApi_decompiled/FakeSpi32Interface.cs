// Decompiled with JetBrains decompiler
// Type: AdisApi.FakeSpi32Interface
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace AdisApi;

/// <summary>
/// This class is a fale implementation of the ISpi32Interface for unit testing.
/// </summary>
public class FakeSpi32Interface : ISpi32Interface
{
  private int fakeReadDataOffset;
  private uint[] fakeReadData;
  private List<uint> fakeWriteDataList = new List<uint>();

  /// <summary>
  /// This property is used to get/set the data ready triggering for buffered streams. If set to true, each bufferd stream "packet"
  /// will start on a data ready posedge. This is needed to implement the IRegInterface.
  /// </summary>
  public bool DrActive { get; set; }

  /// <summary>
  /// This property is used to get or set the pin used to trigger data ready captures.
  /// </summary>
  public IPinObject DrPin { get; set; }

  /// <summary>
  /// This property is used to get or set the data ready polarity. True means data ready is triggered on
  /// a rising edge of the data ready pin. False means that data ready is triggered on a falling edge of the data ready pin.
  /// </summary>
  public bool DrPolarity { get; set; }

  /// <summary>
  /// This property is used to get/set the buffered stream timeout, in seconds. This is needed to implement the IRegInterface.
  /// </summary>
  public int StreamTimeoutSeconds { get; set; }

  /// <summary>
  /// Retrieve a Simulated data buffer produced by the StartBufferedStream function.
  /// </summary>
  /// <returns>The data packet, as a uint array. Should return nothing if there are no buffers available</returns>
  public uint[] GetBufferedStreamDataPacket() => throw new NotImplementedException();

  public void StartBufferedStream(
    IEnumerable<uint> WriteData,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    throw new NotImplementedException();
  }

  public void StopStream() => throw new NotImplementedException();

  public uint Transfer(uint WriteData)
  {
    return this.TransferArray((IEnumerable<uint>) new uint[1]
    {
      WriteData
    }, 1U, 1U)[0];
  }

  public uint[] TransferArray(IEnumerable<uint> WriteData) => this.TransferArray(WriteData, 1U, 1U);

  public uint[] TransferArray(IEnumerable<uint> WriteData, uint numCaptures)
  {
    return this.TransferArray(WriteData, numCaptures, 1U);
  }

  /// <summary>
  /// Emulates Transfer Functionality, utilizing fake read data and fake write data buffers.
  /// </summary>
  /// <param name="WriteData"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  public uint[] TransferArray(IEnumerable<uint> WriteData, uint numCaptures, uint numBuffers)
  {
    this.AddFakeWriteData(WriteData);
    return this.GetFakeReadData(WriteData.Count<uint>() * (int) numCaptures * (int) numBuffers);
  }

  /// <summary>Buffer of data to be returned by read commands.</summary>
  public uint[] FakeReadData
  {
    private get => this.fakeReadData;
    set
    {
      this.fakeReadDataOffset = 0;
      this.fakeReadData = value;
    }
  }

  /// <summary>
  /// Buffer of data that was written in last simulated write command call.
  /// </summary>
  public uint[] FakeWriteData => this.fakeWriteDataList.ToArray();

  /// <summary>Adds data to the buffer that tracks fals write data</summary>
  /// <param name="data"></param>
  protected void AddFakeWriteData(IEnumerable<uint> data) => this.fakeWriteDataList.AddRange(data);

  /// <summary>
  /// Clears the buffer of accumulated data from write operations.
  /// </summary>
  public void ClearFakeWriteData() => this.fakeWriteDataList = new List<uint>();

  /// <summary>
  /// Gets specified number of words from the fake read data buffer and updates read data offset for next transfer
  /// </summary>
  /// <param name="dataLength"></param>
  /// <returns></returns>
  private uint[] GetFakeReadData(int dataLength)
  {
    if (this.fakeReadDataOffset + dataLength > ((IEnumerable<uint>) this.FakeReadData).Count<uint>())
      throw new Exception("Insufficient fake read data for transfer operation.");
    uint[] array = ((IEnumerable<uint>) this.FakeReadData).Skip<uint>(this.fakeReadDataOffset).Take<uint>(dataLength).ToArray<uint>();
    this.fakeReadDataOffset += dataLength;
    return array;
  }
}
