// Decompiled with JetBrains decompiler
// Type: AdisApi.OTPFcns
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

#nullable disable
namespace AdisApi;

/// <summary>Functions for Read and Write to OTP memory</summary>
public class OTPFcns
{
  /// <summary>
  /// Set properties to values for pulse fcns and edge timing measure
  /// </summary>
  public OTPFcns() => this.AdisBase = new AdisBase();

  /// <summary>
  /// 
  /// </summary>
  /// <param name="AdisBase"></param>
  public OTPFcns(AdisBase AdisBase) => this.AdisBase = AdisBase;

  /// <summary>
  /// Gets the AdisBase object associated with thie Spi interface.
  /// </summary>
  public AdisBase AdisBase { get; private set; }

  /// <summary>Reads the OTP memory</summary>
  /// <param name="pageAddr">Page Address</param>
  /// <param name="halfLoc">Lower half (0) or upper half (1) of page address</param>
  /// <param name="readSpd">Read speed, default value is 0x1485</param>
  /// <returns></returns>
  public uint[] ReadOTP(uint pageAddr, uint halfLoc, uint readSpd)
  {
    uint[] numArray1 = new uint[3]
    {
      pageAddr,
      halfLoc,
      readSpd
    };
    uint[] numArray2 = new uint[3];
    this.AdisBase.Base.userCmdU32(4160749583U /*0xF800000F*/, numArray1, (uint[]) null, 3, ref numArray2);
    return numArray2;
  }

  /// <summary>Writes to the OTP memory</summary>
  /// <param name="pageAddr">Page Address</param>
  /// <param name="halfLoc">0 for lower half of page address; 1 for upper half</param>
  /// <param name="value">64 bit value to write to OTP page address</param>
  /// <param name="writeSpd">Write speed to OTP memory, default value is 0xA548878</param>
  /// <param name="prevWriteCheck">Check if page address half has already been written to; should always be 1 unless the user knows what he/she is doing</param>
  /// <returns></returns>
  public uint[] WriteOTP(
    uint pageAddr,
    uint halfLoc,
    ulong value,
    uint writeSpd,
    uint prevWriteCheck)
  {
    uint[] numArray;
    this.AdisBase.Base.userCmdU32(4160749582U /*0xF800000E*/, new uint[6]
    {
      pageAddr,
      halfLoc,
      (uint) value,
      (uint) (value >> 32 /*0x20*/),
      writeSpd,
      prevWriteCheck
    }, (uint[]) null, 1, ref numArray);
    return numArray;
  }
}
