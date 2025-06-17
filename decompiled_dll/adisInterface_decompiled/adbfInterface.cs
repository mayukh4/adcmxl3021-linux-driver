// Decompiled with JetBrains decompiler
// Type: adisInterface.adbfInterface
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using AdisApi;
using System;

#nullable disable
namespace adisInterface;

/// <summary>
/// Interface for devices with paged register access with 16 bit write and 8 bit read operations.
/// </summary>
public class adbfInterface : PagedDutBase
{
  /// <summary>Create a new instance of a Dut interface class.</summary>
  /// <param name="adis">Reg interface object to use for dut comminication.</param>
  /// <param name="burstMode">Burst Class to use to support burst mode.  Nothing/null if Dut does not support burst mode.</param>
  public adbfInterface(IRegInterface adis, BurstBase burstMode)
    : base(adis, burstMode)
  {
    this.DeviceAddressIncrement = 1U;
    this.DeviceWriteWordSize = 8U;
  }

  /// <summary>Create a new instance of a Dut interface class.</summary>
  /// <param name="adis">Reg interface object to use for dut comminication.</param>
  [Obsolete("Use other constructor and provide a burst class refernce or Nothing/null as appropriate.", true)]
  public adbfInterface(IRegInterface adis)
    : base((IRegInterface) null, (BurstBase) null)
  {
  }
}
