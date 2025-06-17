// Decompiled with JetBrains decompiler
// Type: adisInterface.RegDataI32
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using RegMapClasses;

#nullable disable
namespace adisInterface;

public class RegDataI32
{
  public RegClass reg;
  public int dat;

  public RegDataI32()
  {
  }

  public RegDataI32(RegClass reg, int dat)
  {
    this.reg = reg;
    this.dat = dat;
  }
}
