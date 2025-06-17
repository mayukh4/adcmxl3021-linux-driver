// Decompiled with JetBrains decompiler
// Type: FX3Api.FX3ErrorLog
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

#nullable disable
namespace FX3Api;

/// <summary>FX3 flash error log class. These are generated and stored on the FX3 board</summary>
public class FX3ErrorLog
{
  /// <summary>The FX3 firmware line which generated the error</summary>
  public uint Line;
  /// <summary>
  /// The FX3 firmware file which generated the error. See FileIdentifier enum in firmware docs for details
  /// </summary>
  public uint FileIdentifier;
  /// <summary>The FX3 boot time when the error occurred. This is a 32-bit unix timestamp</summary>
  public uint BootTimeStamp;
  /// <summary>The error code supplied with the error. Usually sourced from RTOS</summary>
  public uint ErrorCode;
  /// <summary>
  /// The firmware revision the FX3 was running when the error log was generated. Can be used with the File/Line to track down exact source of error
  /// </summary>
  public string FirmwareRev;
  /// <summary>FX3 ThreadX RTOS uptime when the error event occurred</summary>
  public uint OSUptime;

  /// <summary>Error log constructor</summary>
  /// <param name="FlashData">The 32 byte block of data read from flash which contains the error log struct</param>
  public FX3ErrorLog(byte[] FlashData)
  {
    this.OSUptime = ((IEnumerable<byte>) FlashData).Count<byte>() >= 32 /*0x20*/ ? BitConverter.ToUInt32(FlashData, 0) : throw new FX3ConfigurationException("ERROR: Flash log must be instantiated from a 32 byte array");
    this.Line = BitConverter.ToUInt32(FlashData, 4);
    this.ErrorCode = BitConverter.ToUInt32(FlashData, 8);
    this.BootTimeStamp = BitConverter.ToUInt32(FlashData, 12);
    this.FileIdentifier = BitConverter.ToUInt32(FlashData, 16 /*0x10*/);
    this.FirmwareRev = Encoding.UTF8.GetString(((IEnumerable<byte>) FlashData).ToList<byte>().GetRange(20, 12).ToArray());
    this.FirmwareRev = this.FirmwareRev.TrimEnd(new char[1]);
  }

  /// <summary>Value-wise equality comparison</summary>
  /// <param name="Right">FX3ErrorLog object to compare to left for equality</param>
  /// <param name="Left">FX3ErrorLog object to compare to right for equality</param>
  /// <returns>True if all fields are equal, else false</returns>
  public static bool operator ==(FX3ErrorLog Right, FX3ErrorLog Left)
  {
    return (int) Right.BootTimeStamp == (int) Left.BootTimeStamp && (int) Right.ErrorCode == (int) Left.ErrorCode && (int) Right.FileIdentifier == (int) Left.FileIdentifier && Operators.CompareString(Right.FirmwareRev, Left.FirmwareRev, false) == 0 && (int) Right.Line == (int) Left.Line && (int) Right.OSUptime == (int) Left.OSUptime;
  }

  public new static bool Equals(object obj0, object obj1)
  {
    return (object) (obj0 as FX3ErrorLog) != null && (object) (obj1 as FX3ErrorLog) != null && (FX3ErrorLog) obj0 == (FX3ErrorLog) obj1;
  }

  public override bool Equals(object obj)
  {
    return FX3ErrorLog.Equals((object) this, RuntimeHelpers.GetObjectValue(obj));
  }

  /// <summary>Inequality operator for FX3ErrorLog object</summary>
  /// <param name="Right">FX3ErrorLog object to compare to left for inequality</param>
  /// <param name="Left">FX3ErrorLog object to compare to right for inequality</param>
  /// <returns>!(Right == Left)</returns>
  public static bool operator !=(FX3ErrorLog Right, FX3ErrorLog Left) => !(Right == Left);

  public override string ToString()
  {
    return $"File: {this.FileIdentifier.ToString()}{Environment.NewLine}Line: {this.Line.ToString()}{Environment.NewLine}Error Code: 0x{this.ErrorCode.ToString("X4")}{Environment.NewLine}Firmware Version: {this.FirmwareRev}{Environment.NewLine}Boot Timestamp: {this.BootTimeStamp.ToString()}{Environment.NewLine}OS Uptime: {this.OSUptime.ToString()}";
  }
}
