// Decompiled with JetBrains decompiler
// Type: AdisApi.AdisBase
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using sdpApi1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;

#nullable disable
namespace AdisApi;

/// <summary>
/// Class containig information and methods related to the Base Class for the ADiS SDP functionality.
/// </summary>
public class AdisBase
{
  /// <summary>Expected GUID of adis user code in Blackfin</summary>
  private readonly Guid ExpectedGuid = new Guid("1f094ce4-9ca0-4b39-bc2e-bff182ae0f90");
  private readonly Version minDriverVersion = new Version(2, 0, 0, 0);
  private SdpBase sdpBase;

  /// <summary>Initializes a new instance of the adisBase class.</summary>
  public AdisBase() => this.sdpBase = new SdpBase();

  /// <summary>Initializes a new instance of the adisBase class.</summary>
  /// <param name="sdpBase">sdpBase object used for this instance.</param>
  public AdisBase(SdpBase sdpBase)
  {
    this.sdpBase = sdpBase;
    this.Verbose = false;
  }

  internal SdpBase Base => this.sdpBase;

  /// <summary>
  /// Suppresses driver version mismatch message if set to False.
  /// </summary>
  public bool Verbose { get; set; }

  /// <summary>Stores the USB connection state of the SDP board.</summary>
  /// <returns></returns>
  public bool IsConncted()
  {
    bool flag = true;
    try
    {
      ulong num;
      this.sdpBase.readMacAddress(ref num);
    }
    catch
    {
      flag = false;
    }
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="id1"></param>
  /// <param name="id2"></param>
  [Obsolete("Use Connect Instead")]
  public void ConnectWithDialog(long id1, long id2) => this.Connect(id1, id2);

  /// <summary>Establishes USB connection with SDP hardware.</summary>
  /// <param name="id">id of matching hardware</param>
  public void Connect(long id) => this.Connect(id, 0L);

  /// <summary>Establishes USB connection with SDP hardware.</summary>
  /// <param name="id1">id of matching hardware</param>
  /// <param name="id2">id of matching hardware</param>
  public void Connect(long id1, long id2)
  {
    Version sdpDriverVersion = AdisBase.FindSdpDriverVersion();
    if (sdpDriverVersion == (Version) null)
    {
      if (AdisBase.CountSdpBoards(10, 1) == 0)
        throw new Exception("Could not find board connected to system.\nCheck USB cable, disconnect and reconnect board, and/or wait for board to boot.");
    }
    else if (sdpDriverVersion < this.minDriverVersion)
      throw new Exception($"This AdisApi version requires SDP drivers version {this.minDriverVersion.ToString()} or higher.\nDetected SDP driver version was {sdpDriverVersion.ToString()}");
    SdpManager.connectVisualStudioDialog(id1 == 0L ? "" : id1.ToString("X"), id2 == 0L ? "" : id2.ToString("X"), false, ref this.sdpBase);
  }

  /// <summary>
  /// Establishes USB connection with any piece of SDP hardware.
  /// </summary>
  public void Connect() => this.Connect(6946082283742394623L, 0L);

  /// <summary>
  /// Returns a multi-lin string containing information about the firmware running on the sdp.
  /// </summary>
  /// <returns></returns>
  public string GetInfo()
  {
    ulong num1;
    this.sdpBase.readMacAddress(ref num1);
    string str1 = num1.ToString("X8").Insert(8, ":").Insert(6, ":").Insert(4, ":").Insert(2, ":");
    int num2;
    int num3;
    int num4;
    int num5;
    string str2;
    string str3;
    this.sdpBase.readFirmwareVersion(ref num2, ref num3, ref num4, ref num5, ref str2, ref str3);
    return $"{$"{$"{$"{$"{$"{$"MAC Address: {str1}{Environment.NewLine}"}Major Rev: {num2.ToString()}{Environment.NewLine}"}Minor Rev: {num3.ToString()}{Environment.NewLine}"}Host Code Rev: {num4.ToString()}{Environment.NewLine}"}BlackFin Code Rev: {num5.ToString()}{Environment.NewLine}"}Compile Date: {str2}{Environment.NewLine}"}Compile Time: {str3}";
  }

  /// <summary>
  /// Returns the boot status of the firmware running on the sdp.
  /// </summary>
  /// <returns></returns>
  [Obsolete("Use GetVersion Instead")]
  public string GetBootStatus() => "";

  /// <summary>
  /// Returns the GUID of the user code in the firmware loaded on the sdp.
  /// </summary>
  /// <returns></returns>
  public Guid GetUserGuid()
  {
    Guid userGuid;
    this.sdpBase.getUserCodeGuid(ref userGuid);
    return userGuid;
  }

  /// <summary>
  /// Returns versions of firmware and software for a board.
  /// </summary>
  /// <returns></returns>
  public AdisVersion GetVersion()
  {
    AdisVersion version = new AdisVersion();
    this.sdpBase.readFirmwareVersion(ref version.SdpMajorRev, ref version.SdpMinorRev, ref version.SdpHostRev, ref version.SdpBaseRev, ref version.CompileDate, ref version.CompileTime);
    uint[] userVersion = this.GetUserVersion();
    version.AdisMajorRev = (int) userVersion[0];
    version.AdisMinorRev = (int) userVersion[1];
    version.AdisBuildRev = (int) userVersion[2];
    return version;
  }

  /// <summary>
  /// Gets the version of the user code used in the firmware running on the sdp.
  /// </summary>
  private uint[] GetUserVersion()
  {
    uint[] userVersion = new uint[3];
    this.sdpBase.userCmdU32(4160749570U /*0xF8000002*/, new uint[0], new uint[0], 3, ref userVersion);
    return userVersion;
  }

  /// <summary>Reboots the sdp.</summary>
  public void RebootfromSpi()
  {
    uint[] numArray1 = new uint[0];
    uint[] numArray2 = new uint[0];
    this.sdpBase.userCmdU32(4160749586U, numArray1, new uint[0], 0, ref numArray2);
  }

  /// <summary>Disconnects the sdp.</summary>
  public void Disconnect() => this.sdpBase.resetAndDisconnect();

  /// <summary>Resets, then disconnects the sdp.</summary>
  public void ResetAndDisconnect() => this.sdpBase.resetAndDisconnect();

  /// <summary>Downloads a new program to the SDP hardware SDRAM.</summary>
  /// <param name="fileName">Path to file containing the Loader (.ldr) data for the new firmware.</param>
  /// <param name="reboot">Set to true if SDP should reboot using new firmware.</param>
  public void ProgramSDRAM(string fileName, bool reboot)
  {
    bool flag = false;
    try
    {
      using (StreamReader sr = new StreamReader(fileName))
      {
        try
        {
          this.ProgramSDRAM(sr, reboot);
        }
        catch (Exception ex)
        {
          flag = true;
          throw;
        }
      }
    }
    catch (Exception ex)
    {
      if (flag)
        throw new Exception("Error programming SDP SDRAM.", ex);
      throw new Exception("Error opening SDP LDR file.", ex);
    }
  }

  /// <summary>Downloads a new program to the SDP hardware SDRAM.</summary>
  /// <param name="sr">Stream reader attached to the Loader (.ldr) data for the new firmware.</param>
  /// <param name="reboot">Set to true if SDP should reboot using new firmware.</param>
  public void ProgramSDRAM(StreamReader sr, bool reboot)
  {
    bool flag = false;
    int num = 0;
    while (!flag)
    {
      try
      {
        this.sdpBase.programSdram(sr, reboot, reboot);
        flag = true;
      }
      catch
      {
        ++num;
        if (num > 1)
          throw;
      }
    }
  }

  /// <summary>Downloads a new program to the SDP hardware SDRAM.</summary>
  /// <param name="ldr">Loader (.ldr) data for the new firmware.</param>
  /// <param name="reboot">Set to true if SDP should reboot using new firmware.</param>
  public void ProgramSDRAM(byte[] ldr, bool reboot)
  {
    using (MemoryStream memoryStream = new MemoryStream(ldr))
    {
      using (StreamReader sr = new StreamReader((Stream) memoryStream))
        this.ProgramSDRAM(sr, reboot);
    }
  }

  /// <summary>
  /// Returns CRC calculated on data previously stored in SDRAM.
  /// </summary>
  /// <returns>CRC-32 of data as stored in the SDP SDRAM buffer.</returns>
  public uint CheckData(uint byteCount)
  {
    byte[] numArray1 = new byte[0];
    uint[] numArray2 = new uint[5];
    if (byteCount == 0U)
      throw new ArgumentException("CheckData called with zero byteCount.");
    numArray2[0] = 0U;
    numArray2[1] = 0U;
    numArray2[2] = byteCount;
    numArray2[3] = 0U;
    numArray2[4] = 1U;
    this.Base.userCmdU8(4160749580U /*0xF800000C*/, numArray2, (byte[]) null, 8, ref numArray1);
    return BitConverter.ToUInt32(numArray1, 4);
  }

  /// <summary>Loads a data onto the SDP board SDRAM.</summary>
  /// <returns>CRC-32 of data as stored in the SDP SDRAM buffer.</returns>
  public uint StoreData(IEnumerable<byte> data)
  {
    using (BinaryReader b = new BinaryReader((Stream) new MemoryStream(data.ToArray<byte>())))
      return this.StoreData(b);
  }

  /// <summary>Loads a data onto the SDP board SDRAM.</summary>
  /// <returns>CRC-32 of data as stored in the SDP SDRAM buffer.</returns>
  public uint StoreData(string fileName)
  {
    using (BinaryReader b = new BinaryReader((Stream) File.Open(fileName, FileMode.Open)))
      return this.StoreData(b);
  }

  /// <summary>Loads a data onto the SDP board SDRAM.</summary>
  /// <returns>CRC-32 of data as stored in the SDP SDRAM buffer.</returns>
  public uint StoreData(BinaryReader b)
  {
    byte[] numArray1 = new byte[0];
    uint[] numArray2 = new uint[5];
    uint length = (uint) b.BaseStream.Length;
    if (length == 0U)
      throw new ArgumentException("StoreData: Data length must be non zero.");
    uint num = length <= 4194304U /*0x400000*/ ? (uint) Math.Ceiling((double) length / 65536.0) : throw new ArgumentException("StoreData: Data size exceeds 4MB.");
    for (uint index = 1; index < num; ++index)
    {
      byte[] numArray3 = b.ReadBytes(65536 /*0x010000*/);
      numArray2[0] = index;
      numArray2[1] = num;
      numArray2[2] = length;
      numArray2[3] = 0U;
      numArray2[4] = 0U;
      this.Base.userCmdU8(4160749580U /*0xF800000C*/, numArray2, numArray3, 1, ref numArray1);
      if (numArray1[0] > (byte) 0)
        this.throwStoreDataError(numArray1[0]);
    }
    byte[] numArray4 = b.ReadBytes((int) (b.BaseStream.Length - b.BaseStream.Position));
    numArray2[0] = num;
    numArray2[1] = num;
    numArray2[2] = length;
    numArray2[3] = 0U;
    numArray2[4] = 1U;
    this.Base.userCmdU8(4160749580U /*0xF800000C*/, numArray2, numArray4, 8, ref numArray1);
    if (numArray1[0] > (byte) 0)
      this.throwStoreDataError(numArray1[0]);
    return BitConverter.ToUInt32(numArray1, 4);
  }

  private void throwStoreDataError(byte errorCode)
  {
    throw new Exception($"SDP returned error code {errorCode} while storing data.");
  }

  /// <summary>Flashes the LED on the target SDP or Eval board.</summary>
  public void FlashLed() => this.sdpBase.flashLed1();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sdpConn"></param>
  /// <returns></returns>
  public Gpio NewGpio(int sdpConn) => new Gpio(this, sdpConn);

  /// <summary>Check to see if Blackfin has correct user code.</summary>
  /// <returns>true if SDP Blackfin user code has expected guid.</returns>
  public bool AdisCodeRunning() => this.GetUserGuid() == this.ExpectedGuid;

  /// <summary>Returns Total Number of SDP boards found.</summary>
  /// <returns></returns>
  public static int CountSdpBoards() => ((IEnumerable<int>) AdisBase.FindSdpBoard()).Sum();

  /// <summary>
  /// Counts the number of (booted) SDP boards plugged into a PC, while waiting for boards to be detected."
  /// </summary>
  /// <param name="msTimeOut">Milliseconds to time out waiting for boards to be found.</param>
  /// <param name="minBoards">Minimum number of boards that the routine will wait to find.</param>
  /// <returns></returns>
  public static int CountSdpBoards(int msTimeOut, int minBoards)
  {
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    int num;
    do
    {
      num = AdisBase.CountSdpBoards();
    }
    while (num < minBoards && stopwatch.ElapsedMilliseconds <= (long) msTimeOut);
    stopwatch.Stop();
    return num;
  }

  /// <summary>
  /// Counts the number of (booted) SDP boards plugged into a PC, while waiting for at least one board to be detected."
  /// </summary>
  /// <param name="msTimeOut">Milliseconds to time out waiting for at least one board to be found.</param>
  /// <returns></returns>
  public static int CountSdpBoards(int msTimeOut) => AdisBase.CountSdpBoards(msTimeOut, 1);

  /// <summary>
  /// Returns an array of u32.
  /// Element 0 returns # of Analog Devices iSensor Evaluation Boards.
  /// Element 1 returns # of Analog Devices Custom SDP boards (could be sdp-b or not programmed OTP Eval boards)
  /// Element 2 returns # of Analog Devices System Demonstration Platform SDP-B (fresh from factory; not programmed by us)
  /// </summary>
  /// <returns></returns>
  public static int[] FindSdpBoard()
  {
    int[] sdpBoard = new int[3];
    foreach (ManagementObject managementObject in new ManagementObjectSearcher("Select * From Win32_PnpEntity where Name like '%Analog Devices iSensor Evaluation Board%' or name like '%Analog Devices Custom Sdp%' or name like '%Analog Devices System%'").Get())
    {
      string a = managementObject["Name"].ToString();
      if (string.Equals(a, "Analog Devices iSensor Evaluation Board", StringComparison.OrdinalIgnoreCase))
        ++sdpBoard[0];
      else if (string.Equals(a, "Analog Devices Custom SDP", StringComparison.OrdinalIgnoreCase))
        ++sdpBoard[1];
      else if (managementObject["Name"].ToString().ToUpper().Contains("ANALOG DEVICES SYSTEM"))
        ++sdpBoard[2];
    }
    return sdpBoard;
  }

  /// <summary>
  /// Returns the version of SDP board driver. SDP board must be attached to computer.  Returns null if driver not found.
  /// </summary>
  /// <returns></returns>
  private static Version FindSdpDriverVersion()
  {
    new ManagementScope("\\\\.\\root\\cimv2").Connect();
    using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("root\\CIMV2", "Select * From Win32_PnPSignedDriver where DeviceName like '%Analog Devices%'").Get().GetEnumerator())
    {
      if (enumerator.MoveNext())
        return new Version(enumerator.Current["DriverVersion"].ToString());
    }
    return (Version) null;
  }
}
