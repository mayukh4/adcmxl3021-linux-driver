// Decompiled with JetBrains decompiler
// Type: RegMapClasses.CommandClass
// Assembly: RegMapClasses, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A5F8C6F-7050-4AF9-8F46-4262EBB69E5D
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\RegMapClasses.dll

using System;
using System.Diagnostics;

#nullable disable
namespace RegMapClasses;

[DebuggerDisplay("{Label,nq}")]
public class CommandClass
{
  private string _Label;
  private uint _Mask;
  private uint _Value;
  private int _Delay;
  private string _RegLabel;
  private Action<string> _Routine;

  public string Label
  {
    get => this._Label;
    set => this._Label = value;
  }

  public uint Mask
  {
    get => this._Mask;
    set => this._Mask = value;
  }

  public uint Value
  {
    get => this._Value;
    set => this._Value = value;
  }

  public int Delay
  {
    get => this._Delay;
    set => this._Delay = value;
  }

  public string RegLabel
  {
    get => this._RegLabel;
    set => this._RegLabel = value;
  }

  public Action<string> Routine
  {
    get => this._Routine;
    set => this._Routine = value;
  }

  public CommandClass()
  {
    this.Label = "";
    this.RegLabel = "";
  }

  public override string ToString() => this.Label;
}
