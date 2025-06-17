// Decompiled with JetBrains decompiler
// Type: FX3Api.FX3Board
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;

#nullable disable
namespace FX3Api;

/// <summary>This class contains information about the connected FX3 board</summary>
public class FX3Board
{
  private string m_SerialNumber;
  private DateTime m_bootTime;
  private string m_firmwareVersion;
  private string m_versionNumber;
  private bool m_verboseMode;
  private string m_bootloaderVersion;
  private string m_buildDateTime;
  private FX3BoardType m_boardType;

  /// <summary>Constructor, should be only called by FX3Connection instance</summary>
  /// <param name="SerialNumber">Board SN</param>
  /// <param name="BootTime">Board boot time</param>
  public FX3Board(string SerialNumber, DateTime BootTime)
  {
    this.m_SerialNumber = SerialNumber;
    this.m_bootTime = BootTime;
    this.m_verboseMode = false;
  }

  /// <summary>The FX3 board type (iSensor FX3 board or Cypress eval FX3 board)</summary>
  /// <returns></returns>
  public FX3BoardType BoardType => this.m_boardType;

  /// <summary>Override of the ToString function</summary>
  /// <returns>String with board information</returns>
  public override string ToString()
  {
    return $"Firmware Version: {this.FirmwareVersion}{Environment.NewLine}Board Type: {Enum.GetName(typeof (FX3BoardType), (object) this.BoardType)}{Environment.NewLine}Build Date and Time: {this.BuildDateTime}{Environment.NewLine}Serial Number: {this.SerialNumber}{Environment.NewLine}Debug Mode: {this.VerboseMode.ToString()}{Environment.NewLine}Uptime: {this.Uptime.ToString()}ms{Environment.NewLine}Bootloader Version: {this.BootloaderVersion}";
  }

  /// <summary>Read-only property to get the current board uptime</summary>
  /// <returns>The board uptime, in ms, as a long</returns>
  public long Uptime
  {
    get => checked ((long) Math.Round(DateTime.Now.Subtract(this.m_bootTime).TotalMilliseconds));
  }

  /// <summary>Gets the date and time the FX3 application firmware was compiled.</summary>
  /// <returns>The date time string</returns>
  public string BuildDateTime => this.m_buildDateTime;

  /// <summary>Read-only property to get the active FX3 serial number</summary>
  /// <returns>The board serial number, as a string</returns>
  public string SerialNumber => this.m_SerialNumber;

  /// <summary>
  /// Read-only property to get the current application firmware version on the FX3
  /// </summary>
  /// <returns>The firmware version, as a string</returns>
  public string FirmwareVersion => this.m_firmwareVersion;

  /// <summary>Read-only property to get the firmware version number</summary>
  /// <returns></returns>
  public string FirmwareVersionNumber => this.m_versionNumber;

  /// <summary>
  /// Read-only property to check if the firmware version was compiled with verbose mode enabled. When verbose mode
  /// is enabled, much more data will be logged to the UART output. This is useful for debugging, but causes significant
  /// performance loss for high throughput applications.
  /// </summary>
  /// <returns></returns>
  public bool VerboseMode => this.m_verboseMode;

  /// <summary>
  /// Get the FX3 bootloader version. This is a second stage bootloader which is stored on the I2C EEPROM
  /// </summary>
  /// <returns>The bootloader version, as a string</returns>
  public string BootloaderVersion => this.m_bootloaderVersion;

  /// <summary>
  /// Set the firmware version. Is friend so as to not be accessible to outside classes.
  /// </summary>
  /// <param name="FirmwareVersion">The firmware version to set, as a string</param>
  internal void SetFirmwareVersion(string FirmwareVersion)
  {
    this.m_firmwareVersion = !(Information.IsNothing((object) FirmwareVersion) | Operators.CompareString(FirmwareVersion, "", false) == 0) ? FirmwareVersion : throw new FX3ConfigurationException("Error: Bad firmware version number");
    this.m_versionNumber = this.m_firmwareVersion.Substring(checked (this.m_firmwareVersion.IndexOf("REV") + 4));
    this.m_versionNumber = this.m_versionNumber.Replace(" ", "");
  }

  /// <summary>
  /// Sets if the firmware is currently running in verbose mode. Should NOT be used in verbose mode in normal operating conditions.
  /// </summary>
  /// <param name="isVerbose">If the board is in verbose mode or not</param>
  internal void SetVerboseMode(bool isVerbose) => this.m_verboseMode = isVerbose;

  /// <summary>Sets the bootloader version</summary>
  /// <param name="BootloaderVersion">The current bootloader version</param>
  internal void SetBootloaderVersion(string BootloaderVersion)
  {
    this.m_bootloaderVersion = BootloaderVersion;
  }

  /// <summary>Set the Date/time string after connecting</summary>
  /// <param name="DateTime"></param>
  internal void SetDateTime(string DateTime) => this.m_buildDateTime = DateTime;

  internal void SetBoardType(FX3BoardType BoardType) => this.m_boardType = BoardType;
}
