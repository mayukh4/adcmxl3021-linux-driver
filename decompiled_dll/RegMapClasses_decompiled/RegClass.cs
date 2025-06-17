// Decompiled with JetBrains decompiler
// Type: RegMapClasses.RegClass
// Assembly: RegMapClasses, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A5F8C6F-7050-4AF9-8F46-4262EBB69E5D
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\RegMapClasses.dll

using System;
using System.Diagnostics;

#nullable disable
namespace RegMapClasses;

[DebuggerDisplay("{Label,nq}")]
public class RegClass
{
  private string _Label;
  private string _EvalLabel;
  private uint _Page;
  private uint _Address;
  private uint _NumBytes;
  private double _Offset;
  private double _Scale;
  private uint _ReadLen;
  private bool _ReadFlag;
  private bool _WriteFlag;
  private bool _TwosCompFlag;
  private bool _FloatFlag;
  private bool _ReadProtected;
  private bool _WriteProtected;
  private uint? _DefaultValue;
  private bool _CalReg;
  private RegClass.RegType _Type;
  private RegClass.RegType _EvalType;
  private int _CalScale;
  private bool _EmbMemFlag;
  private uint _AuxAddress;

  public string Label
  {
    get => this._Label;
    set => this._Label = value;
  }

  public string EvalLabel
  {
    get => this._EvalLabel;
    set => this._EvalLabel = value;
  }

  public uint Page
  {
    get => this._Page;
    set => this._Page = value;
  }

  public uint Address
  {
    get => this._Address;
    set => this._Address = value;
  }

  public uint AuxAddress
  {
    get => this._AuxAddress;
    set => this._AuxAddress = value;
  }

  public uint NumBytes
  {
    get => this._NumBytes;
    set => this._NumBytes = value;
  }

  public double Offset
  {
    get => this._Offset;
    set => this._Offset = value;
  }

  public double Scale
  {
    get => this._Scale;
    set => this._Scale = value;
  }

  public uint ReadLen
  {
    get => this._ReadLen;
    set => this._ReadLen = value;
  }

  public bool IsReadable
  {
    get => this._ReadFlag;
    set => this._ReadFlag = value;
  }

  public bool IsReadProtected
  {
    get => this._ReadProtected;
    set => this._ReadProtected = value;
  }

  [Obsolete("Use IsReadable instead.")]
  public bool ReadFlag
  {
    get => this._ReadFlag;
    set => this._ReadFlag = value;
  }

  public bool IsWriteable
  {
    get => this._WriteFlag;
    set => this._WriteFlag = value;
  }

  public bool IsWriteProtected
  {
    get => this._WriteProtected;
    set => this._WriteProtected = value;
  }

  [Obsolete("Use IsWriteable instead.")]
  public bool WriteFlag
  {
    get => this._WriteFlag;
    set => this._WriteFlag = value;
  }

  [Obsolete("Use IsTwosComp instead.")]
  public bool TwosCompFlag
  {
    get => this._TwosCompFlag;
    set => this._TwosCompFlag = value;
  }

  public bool IsTwosComp
  {
    get => this._TwosCompFlag;
    set => this._TwosCompFlag = value;
  }

  public bool IsFloat
  {
    get => this._FloatFlag;
    set => this._FloatFlag = value;
  }

  [Obsolete("Use IsReadProtected and/or IsWriteProtected instead.")]
  public bool Hidden
  {
    get => this._ReadProtected;
    set => this._ReadProtected = value;
  }

  [Obsolete("Use IsReadProtected and/or IsWriteProtected instead.")]
  public bool IsHidden
  {
    get => this._ReadProtected;
    set => this._ReadProtected = value;
  }

  public uint? DefaultValue
  {
    get => this._DefaultValue;
    set => this._DefaultValue = value;
  }

  [Obsolete("Use IsCalReg instead.")]
  public bool CalReg
  {
    get => this._CalReg;
    set => this._CalReg = value;
  }

  public bool IsCalReg
  {
    get => this._CalReg;
    set => this._CalReg = value;
  }

  public RegClass.RegType EvalType
  {
    get => this._EvalType;
    set => this._EvalType = value;
  }

  public RegClass.RegType Type
  {
    get => this._Type;
    set => this._Type = value;
  }

  public bool IsEmbedded
  {
    get => this._EmbMemFlag;
    set => this._EmbMemFlag = value;
  }

  public int CalScale
  {
    get => this._CalScale;
    set => this._CalScale = value;
  }

  public int SpiTransferCount
  {
    get
    {
      switch (this.NumBytes)
      {
        case 1:
        case 2:
          return 1;
        case 4:
          return 2;
        default:
          throw new Exception("Invalid number of bytes in register.");
      }
    }
  }

  public string HexFormat => $"X{this.NumBytes}";

  public RegClass()
  {
    this.ReadLen = 16U /*0x10*/;
    this.Label = "";
    this.NumBytes = 2U;
    this.Scale = 1.0;
    this.IsReadable = true;
    this.IsWriteable = true;
    this.DefaultValue = new uint?();
    this.Type = RegClass.RegType.Undefined;
    this.EvalType = RegClass.RegType.Undefined;
  }

  public override string ToString() => this.Label;

  public enum RegType
  {
    Undefined,
    PageId,
    Status,
    Control,
    UserOutput,
    UserOutputLow,
    UserOutputFlag,
    UserCal,
    FactoryControl,
    FactoryStatus,
    FactoryOuput,
    FactoryCal,
    FilterA,
    FilterB,
    FilterC,
    FilterD,
    FilterE,
    FilterF,
  }
}
