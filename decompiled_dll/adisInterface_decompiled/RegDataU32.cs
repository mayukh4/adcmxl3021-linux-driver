// Decompiled with JetBrains decompiler
// Type: adisInterface.RegDataU32
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using RegMapClasses;

#nullable disable
namespace adisInterface;

public class RegDataU32
{
  public RegClass reg;
  public uint? dat;

  public RegDataU32() => this.dat = new uint?();

  public RegDataU32(RegClass reg, uint? dat)
  {
    this.reg = reg;
    this.dat = dat;
  }
}
