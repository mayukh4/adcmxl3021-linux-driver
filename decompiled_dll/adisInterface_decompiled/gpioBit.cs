// Decompiled with JetBrains decompiler
// Type: adisInterface.gpioBit
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using AdisApi;
using System.Threading;

#nullable disable
namespace adisInterface;

/// <summary>Methods to configure, read, and write a single SDP GPIO bit.</summary>
/// <remarks></remarks>
public class gpioBit
{
  private Gpio gpio;
  private byte bitMask;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="gpio">Reference to sdp gpio object.</param>
  /// <param name="bitMask">Bit Mask of the gpio bit being used.</param>
  /// <remarks></remarks>
  public gpioBit(Gpio gpio, byte bitMask)
  {
    this.gpio = gpio;
    this.bitMask = bitMask;
  }

  public gpioBit(AdisBase sdp, int sdpConn, byte bitMask)
  {
    this.gpio = sdp.NewGpio(sdpConn);
    this.bitMask = bitMask;
  }

  public void configInput() => this.gpio.configInput(this.bitMask);

  public void configOutput() => this.gpio.configOutput(this.bitMask);

  public void driveLo() => this.gpio.bitClear(this.bitMask);

  public void driveHi() => this.gpio.bitSet(this.bitMask);

  public void drive(bool state)
  {
    if (state)
      this.driveHi();
    else
      this.driveLo();
  }

  public void Toggle() => this.gpio.bitToggle(this.bitMask);

  public void PulseLo(int mSec) => this.Pulse(mSec, false);

  public void PulseHi(int mSec) => this.Pulse(mSec, true);

  public bool Read()
  {
    byte bitsMask;
    this.gpio.dataRead(out bitsMask);
    return ((int) bitsMask & (int) this.bitMask) == (int) this.bitMask;
  }

  public void Pulse(int mSec, bool state)
  {
    this.drive(state);
    Thread.Sleep(mSec);
    this.drive(!state);
  }
}
