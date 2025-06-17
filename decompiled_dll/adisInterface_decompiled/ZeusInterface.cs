// Decompiled with JetBrains decompiler
// Type: adisInterface.ZeusInterface
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using AdisApi;
using RegMapClasses;

#nullable disable
namespace adisInterface;

/// <summary>
/// Interface for devices with paged register access with 16 bit write read operations.
/// </summary>
public class ZeusInterface : PagedDutBase
{
  /// <summary>Create a new instance of a Dut interface class.</summary>
  /// <param name="adis">Reg interface object to use for dut comminication.</param>
  /// <param name="burstMode">Burst Class to use to support burst mode.  Nothing/null if Dut does not support burst mode.</param>
  public ZeusInterface(IRegInterface adis, BurstBase burstMode)
    : base(adis, burstMode)
  {
    this.DeviceAddressIncrement = 2U;
    this.DeviceWriteWordSize = 16U /*0x10*/;
    this.PageReg = new RegClass()
    {
      Address = 0U,
      NumBytes = 2U
    };
  }
}
