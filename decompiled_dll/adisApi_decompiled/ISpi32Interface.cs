// Decompiled with JetBrains decompiler
// Type: AdisApi.ISpi32Interface
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace AdisApi;

/// <summary>
/// This interface is used to allow for communication using 32 bit SPI transactions. It does not perform any "protocol" management,
/// and deals only with raw data.
/// </summary>
public interface ISpi32Interface
{
  /// <summary>
  /// This function performs a single bi-directional 32 bit SPI transaction. If DrActive is set to false the transfer is performed asynchronously. If DrActive is set to true,
  /// the transfer should wait until a data ready condition (determined by DrPin and DrPolarity) is true.
  /// </summary>
  /// <param name="WriteData">The 32 bit data to be send to the slave on the MOSI line</param>
  /// <returns>The 32 bit data sent to the master over the MISO line during the SPI transaction</returns>
  uint Transfer(uint WriteData);

  /// <summary>
  /// This function performs an array bi-directional SPI transfer. WriteData.Count() total SPI transfers are performed. If DrActive is set to true, the transfer should wait
  /// until a data ready condition (determined by DrPin and DrPolarity) is true, and then perform all SPI transfers. If DrActive is false it is performed asynchronously.
  /// </summary>
  /// <param name="WriteData">The data to be written to the slave on the MOSI line in each SPI transaction. The total number of transfers performed is determined by the size of WriteData.</param>
  /// <returns>The data received from the slave device on the MISO line, as an array</returns>
  uint[] TransferArray(IEnumerable<uint> WriteData);

  /// <summary>
  /// This function performs an array bi-directional SPI transfer. This overload transfers all the data in WriteData numCaptures times. The total
  /// number of SPI words transfered is WriteData.Count() * numCaptures.
  /// If DrActive is set to true, the transfer should wait until a data ready condition (determined by DrPin and DrPolarity) is true, and
  /// then perform all SPI transfers. If DrActive is false it is performed asynchronously.
  /// </summary>
  /// <param name="WriteData">The data to be written to the slave on the MOSI line in each SPI transaction.</param>
  /// <param name="numCaptures">The number of transfers of the WriteData array performed.</param>
  /// <returns>The data received from the slave device on the MISO line, as an array. The total size is WriteData.Count() * numCaptures</returns>
  uint[] TransferArray(IEnumerable<uint> WriteData, uint numCaptures);

  uint[] TransferArray(IEnumerable<uint> WriteData, uint numCaptures, uint numBuffers);

  /// <summary>
  /// This property is used to get/set the buffered stream timeout, in seconds. This is needed to implement the IRegInterface.
  /// </summary>
  int StreamTimeoutSeconds { get; set; }

  /// <summary>
  /// This property is used to get/set the data ready triggering for buffered streams. If set to true, each bufferd stream "packet"
  /// will start on a data ready posedge. This is needed to implement the IRegInterface.
  /// </summary>
  bool DrActive { get; set; }

  /// <summary>
  /// This property is used to get or set the pin used to trigger data ready captures.
  /// </summary>
  IPinObject DrPin { get; set; }

  /// <summary>
  /// This property is used to get or set the data ready polarity. True means data ready is triggered on
  /// a rising edge of the data ready pin. False means that data ready is triggered on a falling edge of the data ready pin.
  /// </summary>
  bool DrPolarity { get; set; }

  /// <summary>
  /// This is similar to the most general streaming function used by the IRegInterface, and all other buffered streaming functions can be derived from
  /// it with a little glue logic. When a stream is started, a second thread should be started to pull buffers from the interfacing board asynchronously.
  /// Each buffer will consist of WriteData.count() * numCaptures 32-bit words, which are just the raw data read back from the DUT over the MISO line.
  /// The stream is expected to produce numBuffers total buffers.
  /// </summary>
  /// <param name="WriteData">The data to send over the MOSI line</param>
  /// <param name="numCaptures">The number of interations of the WriteData array to perform in a single buffer</param>
  /// <param name="numBuffers">The total number of buffers to capture</param>
  /// <param name="timeoutSeconds">The time to wait on the interfacing board before stopping the stream</param>
  /// <param name="worker">A background worker used to notify the caller of progress made in the stream. You MUST check that this parameter has been initialized</param>
  void StartBufferedStream(
    IEnumerable<uint> WriteData,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker);

  /// <summary>
  /// Retrieve a data buffer produced by the StartBufferedStream function.
  /// </summary>
  /// <returns>The data packet, as a uint array. Should return nothing if there are no buffers available</returns>
  uint[] GetBufferedStreamDataPacket();

  /// <summary>
  /// This function is used to aynchronously stop a buffered stream.
  /// </summary>
  void StopStream();
}
