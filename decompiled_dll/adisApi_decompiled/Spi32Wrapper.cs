// Decompiled with JetBrains decompiler
// Type: AdisApi.Spi32Wrapper
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace AdisApi;

/// <summary>
/// This class acts as a wrapper around a base ISpi32Interface implementation. It asserts that the base implementation produces the correct size data.
/// </summary>
public class Spi32Wrapper : ISpi32Interface
{
  private ISpi32Interface m_SPI32;
  private int m_expectedBufferSize = 0;

  /// <summary>Constructor for a Spi32Wrapper</summary>
  /// <param name="SpiInterface">The ISpi32Interface implementation to wrap around</param>
  public Spi32Wrapper(ISpi32Interface SpiInterface) => this.m_SPI32 = SpiInterface;

  public bool DrActive
  {
    get => this.m_SPI32.DrActive;
    set => this.m_SPI32.DrActive = value;
  }

  public IPinObject DrPin
  {
    get => this.m_SPI32.DrPin;
    set => this.m_SPI32.DrPin = value;
  }

  public bool DrPolarity
  {
    get => this.m_SPI32.DrPolarity;
    set => this.m_SPI32.DrPolarity = value;
  }

  public int StreamTimeoutSeconds
  {
    get => this.m_SPI32.StreamTimeoutSeconds;
    set => this.m_SPI32.StreamTimeoutSeconds = value;
  }

  public uint[] GetBufferedStreamDataPacket()
  {
    uint[] streamDataPacket = this.m_SPI32.GetBufferedStreamDataPacket();
    return streamDataPacket == null || ((IEnumerable<uint>) streamDataPacket).Count<uint>() == this.m_expectedBufferSize ? streamDataPacket : throw new ISpi32InterfaceException($"ERROR: Invalid array buffered data packet received. Expected {this.m_expectedBufferSize.ToString()} words, received: {((IEnumerable<uint>) streamDataPacket).Count<uint>().ToString()} words.");
  }

  public void StartBufferedStream(
    IEnumerable<uint> WriteData,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    this.m_expectedBufferSize = WriteData.Count<uint>() * (int) numCaptures;
    this.m_SPI32.StartBufferedStream(WriteData, numCaptures, numBuffers, timeoutSeconds, worker);
  }

  public void StopStream() => this.m_SPI32.StopStream();

  public uint Transfer(uint WriteData) => this.m_SPI32.Transfer(WriteData);

  public uint[] TransferArray(IEnumerable<uint> WriteData)
  {
    uint[] source = this.m_SPI32.TransferArray(WriteData);
    return ((IEnumerable<uint>) source).Count<uint>() == WriteData.Count<uint>() ? source : throw new ISpi32InterfaceException($"ERROR: Invalid array MISO array received. Expected {WriteData.Count<uint>().ToString()} words, received: {((IEnumerable<uint>) source).Count<uint>().ToString()} words.");
  }

  public uint[] TransferArray(IEnumerable<uint> WriteData, uint numCaptures)
  {
    int num = WriteData.Count<uint>() * (int) numCaptures;
    uint[] source = this.m_SPI32.TransferArray(WriteData, numCaptures);
    return ((IEnumerable<uint>) source).Count<uint>() == num ? source : throw new ISpi32InterfaceException($"ERROR: Invalid array MISO array received. Expected {num.ToString()} words, received: {((IEnumerable<uint>) source).Count<uint>().ToString()} words.");
  }

  public uint[] TransferArray(IEnumerable<uint> WriteData, uint numCaptures, uint numBuffers)
  {
    int num = WriteData.Count<uint>() * (int) numCaptures * (int) numBuffers;
    uint[] source = this.m_SPI32.TransferArray(WriteData, numCaptures, numBuffers);
    return ((IEnumerable<uint>) source).Count<uint>() == num ? source : throw new ISpi32InterfaceException($"ERROR: Invalid array MISO array received. Expected {num.ToString()} words, received: {((IEnumerable<uint>) source).Count<uint>().ToString()} words.");
  }
}
