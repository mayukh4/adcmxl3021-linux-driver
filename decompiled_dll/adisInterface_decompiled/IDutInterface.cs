// Decompiled with JetBrains decompiler
// Type: adisInterface.IDutInterface
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using RegMapClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace adisInterface;

public interface IDutInterface
{
  /// <summary>Number of bytes that are represented by the LSB of the register address</summary>
  /// <returns></returns>
  uint DeviceAddressIncrement { get; set; }

  /// <summary>
  /// For multi-word registers (32-bit), select if the lower half of the value
  /// is at address n (true) or n + 1 (false)
  /// </summary>
  /// <returns></returns>
  bool IsLowerFirst { get; set; }

  uint DeviceReadWordSize { get; set; }

  uint DeviceWriteWordSize { get; set; }

  IBurstMode BurstMode { get; set; }

  Action<uint> SelectionRoutine { get; set; }

  uint Selection { get; set; }

  /// <summary>Scales raw binary data read from a SPI interface.</summary>
  /// <param name="Reg"></param>
  /// <param name="Data"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double ScaleRegData(RegClass Reg, uint Data);

  /// <summary>Scales raw binary data read from a SPI interface.</summary>
  /// <param name="regList"></param>
  /// <param name="datList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double[] ScaleRegData(IEnumerable<RegClass> regList, IEnumerable<uint> datList);

  /// <summary>Removes scaling from number to prepare for write.</summary>
  /// <param name="Reg"></param>
  /// <param name="Data"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint UnscaleRegData(RegClass Reg, double Data);

  uint[] UnscaleRegData(IEnumerable<RegClass> regList, IEnumerable<double> valList);

  /// <summary>Reads the scaled value of a single register, 16 and 32 bit capable.</summary>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double ReadScaledValue(RegClass reg);

  /// <summary>Reads the scaled value of a list of registers, 16 and 32 bit capable.</summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double[] ReadScaledValue(IEnumerable<RegClass> regList);

  /// <summary>Reads the scaled value of a list of registers, multiple passes.</summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double[] ReadScaledValue(IEnumerable<RegClass> regList, uint numCaptures);

  /// <summary>Reads the scaled value of a single register, multiple passes.</summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double[] ReadScaledValue(RegClass reg, uint numCaptures);

  /// <summary>
  /// Reads the scaled value of a single register, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double[] ReadScaledValue(RegClass reg, uint numCaptures, uint numBuffers);

  /// <summary>
  /// Reads the scaled value of a list of registers, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double[] ReadScaledValue(IEnumerable<RegClass> regList, uint numCaptures, uint numBuffers);

  /// <summary>
  /// Reads the scaled value of a single register, 16 and 32 bit capable.
  /// The recieving variable MUST be declared as Double? Nullable. Otherwise a return value of 'Nothing' will be translated to value 0.0
  /// </summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double? ReadScaledValue(int timeOutSeconds, RegClass reg);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double[] ReadScaledValue(int timeOutSeconds, IEnumerable<RegClass> regList);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double[] ReadScaledValue(int timeOutSeconds, IEnumerable<RegClass> regList, uint numCaptures);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double[] ReadScaledValue(int timeOutSeconds, RegClass reg, uint numCaptures);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double[] ReadScaledValue(int timeOutSeconds, RegClass reg, uint numCaptures, uint numBuffers);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  double[] ReadScaledValue(
    int timeOutSeconds,
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers);

  /// <summary>Reads the scaled value of a single register, 16 and 32 bit capable.</summary>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long ReadSigned(RegClass reg);

  /// <summary>Reads the Integer value of a list of registers, 16 and 32 bit capable.</summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long[] ReadSigned(IEnumerable<RegClass> regList);

  /// <summary>Reads the Integer value of a list of registers, multiple passes.</summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long[] ReadSigned(IEnumerable<RegClass> regList, uint numCaptures);

  /// <summary>Reads the Integer value of a single register, multiple passes.</summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long[] ReadSigned(RegClass reg, uint numCaptures);

  /// <summary>
  /// Reads the Integer value of a single register, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long[] ReadSigned(RegClass reg, uint numCaptures, uint numBuffers);

  /// <summary>
  /// Reads the scaled value of a list of registers, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long[] ReadSigned(IEnumerable<RegClass> regList, uint numCaptures, uint numBuffers);

  /// <summary>
  /// Reads the Integer value of a single register, 16 and 32 bit capable.
  /// The recieving variable MUST be declared as Double? Nullable. Otherwise a return value of 'Nothing' will be translated to value 0.0
  /// </summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long? ReadSigned(int timeOutSeconds, RegClass reg);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long[] ReadSigned(int timeOutSeconds, IEnumerable<RegClass> regList);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long[] ReadSigned(int timeOutSeconds, IEnumerable<RegClass> regList, uint numCaptures);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long[] ReadSigned(int timeOutSeconds, RegClass reg, uint numCaptures);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long[] ReadSigned(int timeOutSeconds, RegClass reg, uint numCaptures, uint numBuffers);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  long[] ReadSigned(
    int timeOutSeconds,
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers);

  /// <summary>Reads raw data (unsigned, unmasked, no twos-comp) from a register.</summary>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint ReadUnsigned(RegClass reg);

  /// <summary>Reads raw data (unsigned, unmasked, no twos-comp) from a list of registers.</summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint[] ReadUnsigned(IEnumerable<RegClass> regList);

  /// <summary>
  /// Reads raw data (unsigned, unmasked, no twos-comp) from a list of registers, multiple passes.
  /// </summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint[] ReadUnsigned(IEnumerable<RegClass> regList, uint numCaptures);

  /// <summary>
  /// Reads Raw data (unsigned, unmasked, no twos comp) from a list of registers, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint[] ReadUnsigned(IEnumerable<RegClass> regList, uint numCaptures, uint numBuffers);

  /// <summary>
  /// Reads Raw data (unsigned, unmasked, no twos comp) from a register, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint[] ReadUnsigned(RegClass reg, uint numCaptures, uint numBuffers);

  /// <summary>
  /// Reads raw data (unsigned, unmasked, no twos-comp) from a register, multiple passes.
  /// </summary>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint[] ReadUnsigned(RegClass reg, uint numCaptures);

  /// <summary>
  /// The recieving variable MUST be declared as UInteger? Nullable. Otherwise 'Nothing' will be translated to value 0.0
  /// </summary>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint? ReadUnsigned(int timeOutSeconds, RegClass reg);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint[] ReadUnsigned(int timeOutSeconds, IEnumerable<RegClass> regList);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint[] ReadUnsigned(int timeOutSeconds, IEnumerable<RegClass> regList, uint numCaptures);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint[] ReadUnsigned(
    int timeOutSeconds,
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers);

  /// <summary>Returns 'Nothing' if timeout occured.</summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint[] ReadUnsigned(int timeOutSeconds, RegClass reg, uint numCaptures, uint numBuffers);

  /// <summary>
  /// Reads raw data (unsigned, unmasked, no twos-comp) from a register, multiple passes.
  /// </summary>
  /// <param name="timeOutSeconds"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint[] ReadUnsigned(int timeOutSeconds, RegClass reg, uint numCaptures);

  /// <summary>Writes data to one byte of a register.</summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  /// <remarks></remarks>
  [Obsolete("Please Use WriteUinteger),True")]
  void WriteRegByte(uint addr, uint data);

  /// <summary>Writes data to the byte at the specified index in the specified register.</summary>
  /// <param name="reg"></param>
  /// <param name="index"></param>
  /// <param name="dat"></param>
  /// <remarks></remarks>
  [Obsolete("Please Use WriteUinteger),True")]
  void WriteByte(RegClass reg, uint index, uint dat);

  /// <summary>
  /// Forces a valid register value determined by the readlength and twos compliment properties of the register.
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="dat"></param>
  /// <remarks></remarks>
  void WriteClampedInteger(RegClass reg, int dat);

  void WriteClampedInteger(IEnumerable<RegClass> regList, IEnumerable<int> datList);

  void WriteClampedInteger(IEnumerable<RegDataI32> regDatList);

  /// <summary>
  /// Writes a masked bit pattern followed by a delay specified in a command csv file.
  /// </summary>
  /// <param name="map"></param>
  /// <param name="cmd"></param>
  /// <remarks></remarks>
  void WriteCommand(RegMapCollection map, CommandClass cmd);

  void WriteCommand(RegClass reg, CommandClass cmd);

  void WriteSigned(RegDataI32 regDat);

  void WriteSigned(IEnumerable<RegClass> regList, IEnumerable<int> datList);

  void WriteSigned(IEnumerable<RegDataI32> regDatList);

  void WriteSigned(RegClass reg, int dat);

  void WriteScaledValue(RegClass Reg, double Value);

  void WriteScaledValue(IEnumerable<RegClass> regList, IEnumerable<double> valList);

  void WriteScaledValue(IEnumerable<RegDataDbl> regDatList);

  /// <summary>
  /// Writes the specified data (unsigned, unmasked, no twos-comp) to the specified register.
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="dat"></param>
  /// <remarks></remarks>
  void WriteUnsigned(RegClass reg, uint dat);

  /// <summary>
  /// Writes the specified data words (unsigned, unmasked, no twos-comp) to the specified registers.
  /// </summary>
  /// <param name="regList"></param>
  /// <param name="datList"></param>
  /// <remarks></remarks>
  void WriteUnsigned(IEnumerable<RegClass> regList, IEnumerable<uint> datList);

  /// <summary>
  /// Writes the specified data word (unsigned, unmasked, no twos-comp) to the specified registers.
  /// </summary>
  /// <param name="regDat"></param>
  /// <remarks></remarks>
  void WriteUnsigned(RegDataU32 regDat);

  /// <summary>
  /// Writes the specified data words (unsigned, unmasked, no twos-comp) to the specified registers.
  /// </summary>
  /// <param name="RegDataList"></param>
  /// <remarks></remarks>
  void WriteUnsigned(IEnumerable<RegDataU32> RegDataList);

  /// <summary>Starts a buffered streaming operation.</summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <param name="timeoutSeconds"></param>
  /// <param name="worker"></param>
  /// <remarks></remarks>
  void StartBufferedStream(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker);

  /// <summary>Starts a streaming operation.</summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <remarks></remarks>
  void StartStream(IEnumerable<RegClass> regList, uint numCaptures);

  /// <summary>Returns a buffered stream data packet.</summary>
  /// <returns></returns>
  /// <remarks></remarks>
  ushort[] GetBufferedStreamDataPacket();

  /// <summary>Returns a stream data packet.</summary>
  /// <returns></returns>
  /// <remarks></remarks>
  ushort[] GetStreamDataPacketU16();

  /// <summary>Terminates a stream operation.</summary>
  /// <remarks></remarks>
  void StopStream();

  /// <summary>
  /// Converts 16 bit reg data reads to Uinteger array, processing 16, or 8 bit data based on reg List item numBytes.
  /// </summary>
  /// <param name="regList"></param>
  /// <param name="u16data"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  uint[] ConvertReadDataToU32(IEnumerable<RegClass> regList, IEnumerable<ushort> u16data);
}
