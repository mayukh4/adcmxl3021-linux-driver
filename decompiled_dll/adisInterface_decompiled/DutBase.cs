// Decompiled with JetBrains decompiler
// Type: adisInterface.DutBase
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using AdisApi;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

#nullable disable
namespace adisInterface;

/// <summary>Base Class for Dut Interface Implementations</summary>
public abstract class DutBase : IDutInterface, IBufferedStreamProducer
{
  internal IRegInterface adis;
  private int resetDelay;
  private IBurstMode m_BurstMode;
  private uint m_DeviceAddressincrement;
  private bool m_IsLowerFirst;
  private Action<uint> m_SelectionRoutine;
  private uint m_DeviceReadWordSize;
  private uint m_DeviceWriteWordSize;
  private uint m_Selection;

  IBurstMode IDutInterface.BurstMode
  {
    get => this.m_BurstMode;
    set => this.m_BurstMode = value;
  }

  /// <summary>Number of bytes that are represented by the LSB of the register address</summary>
  /// <returns></returns>
  uint IDutInterface.DeviceAddressIncrement
  {
    get => this.m_DeviceAddressincrement;
    set
    {
      this.m_DeviceAddressincrement = ((IEnumerable<uint>) new uint[2]
      {
        1U,
        2U
      }).Contains<uint>(value) ? value : throw new ArgumentException("DeviceAddressIncrement must be set to 1 or 2.");
    }
  }

  /// <summary>
  /// For multi-word registers (32-bit), select if the lower half of the value
  /// is at address n (true) or n + 1 (false)
  /// </summary>
  /// <returns></returns>
  bool IDutInterface.IsLowerFirst
  {
    get => this.m_IsLowerFirst;
    set => this.m_IsLowerFirst = value;
  }

  public Action<uint> SelectionRoutine
  {
    get => this.m_SelectionRoutine;
    set => this.m_SelectionRoutine = value;
  }

  /// <summary>
  /// SPI data word size used to write to a device. May be 8 or 16 bits data per SPi transaction, defaults to 8.
  /// </summary>
  /// <returns></returns>
  virtual uint IDutInterface.DeviceReadWordSize
  {
    get => this.m_DeviceReadWordSize;
    set
    {
      this.m_DeviceReadWordSize = ((IEnumerable<uint>) new uint[2]
      {
        8U,
        16U /*0x10*/
      }).Contains<uint>(value) ? value : throw new ArgumentException("WordSizeForDeviceWrite must be set to 8 or 16.");
    }
  }

  /// <summary>
  /// SPI data word size used to write to a device. May be 8 or 16 bits data per SPi transaction, defaults to 8.
  /// </summary>
  /// <returns></returns>
  virtual uint IDutInterface.DeviceWriteWordSize
  {
    get => this.m_DeviceWriteWordSize;
    set
    {
      this.m_DeviceWriteWordSize = value == 8U | value == 16U /*0x10*/ ? value : throw new ArgumentException("WordSizeForDeviceWrite must be set to 8 or 16.");
    }
  }

  public uint Selection
  {
    get => this.m_Selection;
    set => this.m_Selection = value;
  }

  /// <summary>Create a new instance of a Dut interface class.</summary>
  /// <param name="adis">Reg interface object to use for dut comminication.</param>
  /// <param name="burstMode">Burst Class to use to support burst mode.  Nothing/null if Dut does not support burst mode.</param>
  public DutBase(IRegInterface adis, BurstBase burstMode)
  {
    this.resetDelay = 700;
    this.m_DeviceAddressincrement = 1U;
    this.m_IsLowerFirst = true;
    this.m_SelectionRoutine = (Action<uint>) null;
    this.m_DeviceReadWordSize = 16U /*0x10*/;
    this.m_DeviceWriteWordSize = 8U;
    this.m_Selection = 1U;
    this.adis = adis;
    this.BurstMode = (IBurstMode) burstMode;
    if (this.BurstMode == null)
      return;
    burstMode.@base = this;
  }

  /// <summary>
  /// Create a new instance of a Dut interface class.  Burst mode hardcoded to aducBurst.
  /// </summary>
  /// <param name="adis">Reg interface object to use for dut comminication.</param>
  [Obsolete("Please use constructor with burstMode parameter and set burst class as appropriate.", true)]
  public DutBase(IRegInterface adis)
  {
    this.resetDelay = 700;
    this.m_DeviceAddressincrement = 1U;
    this.m_IsLowerFirst = true;
    this.m_SelectionRoutine = (Action<uint>) null;
    this.m_DeviceReadWordSize = 16U /*0x10*/;
    this.m_DeviceWriteWordSize = 8U;
    this.m_Selection = 1U;
  }

  public void Start()
  {
    this.SelectDevice();
    this.adis.Start();
  }

  public void Reset()
  {
    this.SelectDevice();
    this.adis.Reset();
    Thread.Sleep(this.resetDelay);
  }

  /// <summary>nSamples of RegList = numBuffers * numCaptures per buffer</summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <param name="timeoutSeconds"></param>
  /// <param name="worker"></param>
  /// <remarks></remarks>
  public abstract void StartBufferedStream(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker);

  /// <summary>Starts a streaming operation.</summary>
  /// <param name="regList">Registers (16 and/or 32 bit) to include in stream.</param>
  /// <param name="numCaptures">Number of captures in each stream packet.</param>
  /// <remarks></remarks>
  public abstract void StartStream(IEnumerable<RegClass> regList, uint numCaptures);

  /// <summary>Gets buffered stream data packet.</summary>
  /// <returns></returns>
  /// <remarks></remarks>
  public virtual ushort[] GetBufferedStreamDataPacket() => this.adis.GetBufferedStreamDataPacket();

  /// <summary>Gets an unbuffered stream data packet.</summary>
  /// <returns></returns>
  /// <remarks></remarks>
  public virtual ushort[] GetStreamDataPacketU16() => this.adis.GetStreamDataPacketU16();

  /// <summary>Stops a Streaming Operation</summary>
  /// <remarks></remarks>
  public void StopStream() => this.adis.StopStream();

  /// <summary>Reads the scaled value of a single register, 16 and 32 bit capable.</summary>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double ReadScaledValue(RegClass reg)
  {
    return this.ReadScaledValue((IEnumerable<RegClass>) new List<RegClass>()
    {
      reg
    })[0];
  }

  /// <summary>Reads the scaled value of a list of registers, 16 and 32 bit capable.</summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ReadScaledValue(IEnumerable<RegClass> regList)
  {
    return this.ReadScaledValue(regList, 1U);
  }

  /// <summary>Reads the scaled value of a single register, multiple passes.</summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ReadScaledValue(RegClass reg, uint numCaptures)
  {
    return this.ReadScaledValue((IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures);
  }

  /// <summary>Reads the scaled value of a list of registers, multiple passes.</summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ReadScaledValue(IEnumerable<RegClass> regList, uint numCaptures)
  {
    int num1 = checked ((int) ((long) regList.Count<RegClass>() * (long) numCaptures));
    double[] numArray1 = new double[checked (num1 - 1 + 1)];
    uint[] numArray2 = this.ReadUnsigned(regList, numCaptures);
    int num2 = checked (num1 - 1);
    int index = 0;
    while (index <= num2)
    {
      numArray1[index] = this.ScaleRegData(regList.ElementAtOrDefault<RegClass>(index % regList.Count<RegClass>()), numArray2[index]);
      checked { ++index; }
    }
    return numArray1;
  }

  /// <summary>
  /// Reads the scaled value of a single register, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ReadScaledValue(RegClass reg, uint numCaptures, uint numBuffers)
  {
    return this.ReadScaledValue((IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures, numBuffers);
  }

  /// <summary>
  /// Reads the scaled value of a list of registers, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ReadScaledValue(IEnumerable<RegClass> regList, uint numCaptures, uint numBuffers)
  {
    int num1 = checked ((int) ((long) regList.Count<RegClass>() * (long) numCaptures * (long) numBuffers));
    double[] numArray1 = new double[checked (num1 - 1 + 1)];
    uint[] numArray2 = this.ReadUnsigned(regList, numCaptures, numBuffers);
    int num2 = checked (num1 - 1);
    int index = 0;
    while (index <= num2)
    {
      numArray1[index] = this.ScaleRegData(regList.ElementAtOrDefault<RegClass>(index % regList.Count<RegClass>()), numArray2[index]);
      checked { ++index; }
    }
    return numArray1;
  }

  /// <summary>Returns the scaled value, or null if no value was read (timeout occurred).</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks>The recieving variable MUST be declared as Double? Nullable. Otherwise 'Nothing' will be translated to value 0.0</remarks>
  public double? ReadScaledValue(int timeOutSeconds, RegClass reg)
  {
    return this.ReadScaledValue(timeOutSeconds, (IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, 1U, 1U)?[0];
  }

  /// <summary>Returns the scaled value, or null if no value was read (timeout occurred).</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ReadScaledValue(int timeOutSeconds, IEnumerable<RegClass> regList)
  {
    return this.ReadScaledValue(timeOutSeconds, regList, 1U, 1U);
  }

  /// <summary>Returns the scaled values, or null if no value was read (timeout occurred).</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ReadScaledValue(int timeOutSeconds, RegClass reg, uint numCaptures)
  {
    return this.ReadScaledValue(timeOutSeconds, (IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures, 1U);
  }

  /// <summary>Returns the scaled values, or null if no value was read (timeout occurred).</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ReadScaledValue(
    int timeOutSeconds,
    IEnumerable<RegClass> regList,
    uint numCaptures)
  {
    return this.ReadScaledValue(timeOutSeconds, regList, numCaptures, 1U);
  }

  /// <summary>Reads scaled values from a register, returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ReadScaledValue(
    int timeOutSeconds,
    RegClass reg,
    uint numCaptures,
    uint numBuffers)
  {
    return this.ReadScaledValue(timeOutSeconds, (IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures, numBuffers);
  }

  /// <summary>Reads scaled values from registers, returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds">Number of Seconds between packets that will trigger a timeout.</param>
  /// <param name="regList">Collection of registers to be captured.</param>
  /// <param name="numCaptures">Number of Captures Per USB transfer packet.</param>
  /// <param name="numBuffers">Number of Packets to be returned.</param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ReadScaledValue(
    int timeOutSeconds,
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers)
  {
    uint[] uintData = this.ReadUnsigned(timeOutSeconds, regList, numCaptures, numBuffers);
    return uintData == null ? (double[]) null : this.ScaleRegData(regList, (IEnumerable<uint>) uintData);
  }

  /// <summary>Reads the Signed value of a single register, 16 and 32 bit capable.</summary>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public long ReadSigned(RegClass reg)
  {
    return this.ReadSigned((IEnumerable<RegClass>) new List<RegClass>()
    {
      reg
    })[0];
  }

  /// <summary>Reads the Signed value of a list of registers, 16 and 32 bit capable.</summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public long[] ReadSigned(IEnumerable<RegClass> regList) => this.ReadSigned(regList, 1U);

  /// <summary>Reads the Signed value of a single register, multiple passes.</summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public long[] ReadSigned(RegClass reg, uint numCaptures)
  {
    return this.ReadSigned((IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures);
  }

  /// <summary>Reads the Signed value of a list of registers, multiple passes.</summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public long[] ReadSigned(IEnumerable<RegClass> regList, uint numCaptures)
  {
    int num1 = checked ((int) ((long) regList.Count<RegClass>() * (long) numCaptures));
    long[] numArray1 = new long[checked (num1 - 1 + 1)];
    uint[] numArray2 = this.ReadUnsigned(regList, numCaptures);
    int num2 = checked (num1 - 1);
    int index = 0;
    while (index <= num2)
    {
      numArray1[index] = Functions.DecodeTwosComp(regList.ElementAtOrDefault<RegClass>(index % regList.Count<RegClass>()), numArray2[index]);
      checked { ++index; }
    }
    return numArray1;
  }

  /// <summary>
  /// Reads the Signed value of a single register, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public long[] ReadSigned(RegClass reg, uint numCaptures, uint numBuffers)
  {
    return this.ReadSigned((IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures, numBuffers);
  }

  /// <summary>
  /// Reads the Signed value of a list of registers, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public long[] ReadSigned(IEnumerable<RegClass> regList, uint numCaptures, uint numBuffers)
  {
    int num1 = checked ((int) ((long) regList.Count<RegClass>() * (long) numCaptures * (long) numBuffers));
    long[] numArray1 = new long[checked (num1 - 1 + 1)];
    uint[] numArray2 = this.ReadUnsigned(regList, numCaptures, numBuffers);
    int num2 = checked (num1 - 1);
    int index = 0;
    while (index <= num2)
    {
      numArray1[index] = Functions.DecodeTwosComp(regList.ElementAtOrDefault<RegClass>(index % regList.Count<RegClass>()), numArray2[index]);
      checked { ++index; }
    }
    return numArray1;
  }

  /// <summary>Returns signed value, or null if timeout occurred.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks>The recieving variable MUST be declared as Double? Nullable. Otherwise 'Nothing' will be translated to value 0.0</remarks>
  public long? ReadSigned(int timeOutSeconds, RegClass reg)
  {
    return this.ReadSigned(timeOutSeconds, (IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, 1U, 1U)?[0];
  }

  /// <summary>Returns signed values, or null if timeout occurred.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks>Returns 'Nothing' if timeout occured.</remarks>
  public long[] ReadSigned(int timeOutSeconds, IEnumerable<RegClass> regList)
  {
    return this.ReadSigned(timeOutSeconds, regList, 1U, 1U);
  }

  /// <summary>Returns signed values, or null if timeout occurred.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks>Returns 'Nothing' if timeout occured.</remarks>
  public long[] ReadSigned(int timeOutSeconds, RegClass reg, uint numCaptures)
  {
    return this.ReadSigned(timeOutSeconds, (IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures, 1U);
  }

  /// <summary>Returns signed values, or null if timeout occurred.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks>Returns 'Nothing' if timeout occured.</remarks>
  public long[] ReadSigned(int timeOutSeconds, IEnumerable<RegClass> regList, uint numCaptures)
  {
    return this.ReadSigned(timeOutSeconds, regList, numCaptures, 1U);
  }

  /// <summary>Reads Signed values from a register, returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks>Returns 'Nothing' if timeout occured.</remarks>
  public long[] ReadSigned(int timeOutSeconds, RegClass reg, uint numCaptures, uint numBuffers)
  {
    return this.ReadSigned(timeOutSeconds, (IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures, numBuffers);
  }

  /// <summary>Reads Signed values from registers, returns 'Nothing' if timeout occured.</summary>
  /// <param name="timeOutSeconds">Number of Seconds between packets that will trigger a timeout.</param>
  /// <param name="regList">Collection of registers to be captured.</param>
  /// <param name="numCaptures">Number of Captures Per USB transfer packet.</param>
  /// <param name="numBuffers">Number of Packets to be returned.</param>
  /// <returns></returns>
  /// <remarks></remarks>
  public long[] ReadSigned(
    int timeOutSeconds,
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers)
  {
    int streamTimeoutSeconds = this.adis.StreamTimeoutSeconds;
    this.adis.StreamTimeoutSeconds = timeOutSeconds;
    long[] numArray = this.ReadSigned(regList, numCaptures, numBuffers);
    this.adis.StreamTimeoutSeconds = streamTimeoutSeconds;
    return numArray ?? (long[]) null;
  }

  /// <summary>Reads raw data (unsigned, unmasked, no twos-comp) from a register.</summary>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint ReadUnsigned(RegClass reg)
  {
    return ((IEnumerable<uint>) this.ReadUnsigned((IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    })).First<uint>();
  }

  /// <summary>Reads raw data (unsigned, unmasked, no twos-comp) from a list of registers.</summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(IEnumerable<RegClass> regList) => this.ReadUnsigned(regList, 1U);

  /// <summary>
  /// Reads raw data (unsigned, unmasked, no twos-comp) from a list of registers, multiple passes.
  /// </summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(IEnumerable<RegClass> regList, uint numCaptures)
  {
    return this.ReadUnsignedImplementation(regList, numCaptures, new uint?());
  }

  /// <summary>
  /// Reads raw data (unsigned, unmasked, no twos-comp) from a list of registers, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(IEnumerable<RegClass> regList, uint numCaptures, uint numBuffers)
  {
    return this.ReadUnsignedImplementation(regList, numCaptures, new uint?(numBuffers));
  }

  /// <summary>
  /// Reads raw data (unsigned, unmasked, no twos-comp) from a single register, multiple passes, multiple stream packets.
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(RegClass reg, uint numCaptures, uint numBuffers)
  {
    return this.ReadUnsigned((IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures, numBuffers);
  }

  /// <summary>
  /// Reads raw data (unsigned, unmasked, no twos-comp) from a single register, multiple passes.
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(RegClass reg, uint numCaptures)
  {
    return this.ReadUnsigned((IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures);
  }

  /// <summary>Reads and Returns Unsigned data, or null if timeout occurred.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <returns></returns>
  /// <remarks>The recieving variable MUST be declared as UInteger? Nullable. Otherwise 'Nothing' will be translated to value 0.0</remarks>
  public uint? ReadUnsigned(int timeOutSeconds, RegClass reg)
  {
    return this.ReadUnsigned(timeOutSeconds, (IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, 1U, 1U)?[0];
  }

  /// <summary>Reads and Returns Unsigned data, or null if timeout occurred.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(int timeOutSeconds, IEnumerable<RegClass> regList)
  {
    return this.ReadUnsigned(timeOutSeconds, regList, 1U, 1U);
  }

  /// <summary>Reads and Returns Unsigned data, or null if timeout occurred.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(int timeOutSeconds, IEnumerable<RegClass> regList, uint numCaptures)
  {
    return this.ReadUnsigned(timeOutSeconds, regList, numCaptures, 1U);
  }

  /// <summary>Reads and Returns Unsigned data, or null if timeout occurred.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(int timeOutSeconds, RegClass reg, uint numCaptures)
  {
    return this.ReadUnsigned(timeOutSeconds, (IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures, 1U);
  }

  /// <summary>Reads and Returns Unsigned data, or null if timeout occurred.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="reg"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(int timeOutSeconds, RegClass reg, uint numCaptures, uint numBuffers)
  {
    return this.ReadUnsigned(timeOutSeconds, (IEnumerable<RegClass>) new RegClass[1]
    {
      reg
    }, numCaptures, numBuffers);
  }

  /// <summary>Reads and Returns Unsigned data, or null if timeout occurred.</summary>
  /// <param name="timeOutSeconds"></param>
  /// <param name="regList"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] ReadUnsigned(
    int timeOutSeconds,
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers)
  {
    int streamTimeoutSeconds = this.adis.StreamTimeoutSeconds;
    this.adis.StreamTimeoutSeconds = timeOutSeconds;
    uint[] numArray;
    try
    {
      numArray = this.ReadUnsignedImplementation(regList, numCaptures, new uint?(numBuffers));
    }
    catch (AdisApi.TimeoutException ex)
    {
      ProjectData.SetProjectError((Exception) ex);
      numArray = (uint[]) null;
      ProjectData.ClearProjectError();
    }
    finally
    {
      this.adis.StreamTimeoutSeconds = streamTimeoutSeconds;
    }
    return numArray;
  }

  /// <summary>Writes page, creates address list, returns Unsigned data.</summary>
  /// <param name="regDataList"></param>
  /// <param name="numCaptures"></param>
  /// <returns></returns>
  /// <remarks>Implementation must be provided my inheriting classes.</remarks>
  public abstract uint[] WriteReadUnsigned(IEnumerable<RegDataU32> regDataList, uint numCaptures);

  internal abstract uint[] ReadUnsignedImplementation(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint? numBuffers);

  void IDutInterface.WriteClampedInteger(RegClass reg, int dat)
  {
    this.WriteUnsigned(reg, Functions.EncodeTwosCompClamped(reg, (long) dat));
  }

  void IDutInterface.WriteClampedInteger(IEnumerable<RegClass> regList, IEnumerable<int> datList)
  {
    List<RegDataU32> RegandDataList = new List<RegDataU32>();
    if (regList == null | datList == null)
      throw new ArgumentNullException("Parameters must not be null.");
    if (regList.Count<RegClass>() != datList.Count<int>())
      throw new ArgumentException("RegList and datList must be the same size.");
    if (regList.Count<RegClass>() == 0)
      return;
    int num = checked (regList.Count<RegClass>() - 1);
    int index = 0;
    while (index <= num)
    {
      RegandDataList.Add(new RegDataU32(regList.ElementAtOrDefault<RegClass>(index), new uint?(Functions.EncodeTwosCompClamped(regList.ElementAtOrDefault<RegClass>(index), (long) datList.ElementAtOrDefault<int>(index)))));
      checked { ++index; }
    }
    this.WriteUnsigned((IEnumerable<RegDataU32>) RegandDataList);
  }

  void IDutInterface.WriteClampedInteger(IEnumerable<RegDataI32> regDatList)
  {
    List<RegDataU32> RegandDataList = new List<RegDataU32>();
    if (regDatList == null)
      throw new ArgumentNullException("Parameters must not be null.");
    if (regDatList.Count<RegDataI32>() == 0)
      return;
    try
    {
      foreach (RegDataI32 regDat in regDatList)
        RegandDataList.Add(new RegDataU32(regDat.reg, new uint?(Functions.EncodeTwosCompClamped(regDat.reg, (long) regDat.dat))));
    }
    finally
    {
      IEnumerator<RegDataI32> enumerator;
      enumerator?.Dispose();
    }
    this.WriteUnsigned((IEnumerable<RegDataU32>) RegandDataList);
  }

  public void WriteSigned(RegClass reg, int dat)
  {
    this.WriteUnsigned(reg, Functions.EncodeTwosComp(reg, (long) dat));
  }

  public void WriteSigned(IEnumerable<RegClass> regList, IEnumerable<int> datList)
  {
    List<RegDataU32> RegandDataList = new List<RegDataU32>();
    if (regList == null | datList == null)
      throw new ArgumentNullException("Parameters must not be null.");
    if (regList.Count<RegClass>() != datList.Count<int>())
      throw new ArgumentException("RegList and datList must be the same size.");
    if (regList.Count<RegClass>() == 0)
      return;
    int num = checked (regList.Count<RegClass>() - 1);
    int index = 0;
    while (index <= num)
    {
      RegandDataList.Add(new RegDataU32(regList.ElementAtOrDefault<RegClass>(index), new uint?(Functions.EncodeTwosComp(regList.ElementAtOrDefault<RegClass>(index), (long) datList.ElementAtOrDefault<int>(index)))));
      checked { ++index; }
    }
    this.WriteUnsigned((IEnumerable<RegDataU32>) RegandDataList);
  }

  /// <summary>Write Signed unscaled data to register.</summary>
  /// <param name="regDat"></param>
  /// <remarks></remarks>
  public void WriteSigned(RegDataI32 regDat)
  {
    this.WriteUnsigned(new RegDataU32(regDat.reg, new uint?(Functions.EncodeTwosComp(regDat.reg, (long) regDat.dat))));
  }

  public void WriteSigned(IEnumerable<RegDataI32> regDatList)
  {
    List<RegDataU32> RegandDataList = new List<RegDataU32>();
    if (regDatList == null)
      throw new ArgumentNullException("Parameters must not be null.");
    if (regDatList.Count<RegDataI32>() == 0)
      return;
    try
    {
      foreach (RegDataI32 regDat in regDatList)
        RegandDataList.Add(new RegDataU32(regDat.reg, new uint?(Functions.EncodeTwosComp(regDat.reg, (long) regDat.dat))));
    }
    finally
    {
      IEnumerator<RegDataI32> enumerator;
      enumerator?.Dispose();
    }
    this.WriteUnsigned((IEnumerable<RegDataU32>) RegandDataList);
  }

  /// <summary>De-scales a value per register scale, writes to register.</summary>
  /// <param name="Reg"></param>
  /// <param name="Value"></param>
  /// <remarks></remarks>
  public void WriteScaledValue(RegClass Reg, double Value)
  {
    this.WriteUnsigned(Reg, this.UnscaleRegData(Reg, Value));
  }

  /// <summary>De-scales a value per register scale, writes to register.</summary>
  /// <param name="regList"></param>
  /// <param name="valList"></param>
  /// <remarks></remarks>
  public void WriteScaledValue(IEnumerable<RegClass> regList, IEnumerable<double> valList)
  {
    if (regList.Count<RegClass>() != valList.Count<double>())
      throw new Exception("regList and valList must be same length.");
    this.WriteUnsigned(regList, (IEnumerable<uint>) this.UnscaleRegData(regList, valList));
  }

  void IDutInterface.WriteScaledValue(IEnumerable<RegDataDbl> regDatList)
  {
    List<RegDataU32> RegandDataList = new List<RegDataU32>();
    if (regDatList == null)
      throw new ArgumentNullException("Parameters must not be null.");
    if (regDatList.Count<RegDataDbl>() == 0)
      return;
    try
    {
      foreach (RegDataDbl regDat in regDatList)
        RegandDataList.Add(new RegDataU32(regDat.reg, new uint?(this.UnscaleRegData(regDat.reg, regDat.dat))));
    }
    finally
    {
      IEnumerator<RegDataDbl> enumerator;
      enumerator?.Dispose();
    }
    this.WriteUnsigned((IEnumerable<RegDataU32>) RegandDataList);
  }

  /// <summary>
  /// Writes a masked bit pattern followed by a delay specified in a Command csv file.
  /// </summary>
  /// <param name="map"></param>
  /// <param name="cmd"></param>
  /// <remarks></remarks>
  public void WriteCommand(RegMapCollection map, CommandClass cmd)
  {
    if (map == null)
      throw new ArgumentNullException("map must not be null.");
    if (cmd == null)
      throw new ArgumentNullException("cmd must not be null.");
    this.WriteCommand(map[cmd.RegLabel], cmd);
  }

  /// <summary>
  /// Writes a masked bit pattern followed by a delay specified in a Command csv file.
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="cmd"></param>
  /// <remarks></remarks>
  public void WriteCommand(RegClass reg, CommandClass cmd)
  {
    if (reg == null)
      throw new ArgumentNullException("reg must not be null.");
    if (cmd == null)
      throw new ArgumentNullException("cmd must not be null.");
    uint num = (uint) ~(-1 << checked ((int) reg.NumBytes * 8));
    if (((int) cmd.Mask & (int) num) == (int) num)
    {
      this.WriteUnsigned(reg, cmd.Value);
    }
    else
    {
      uint dat = this.ReadUnsigned(reg) & ~cmd.Mask | cmd.Mask & cmd.Value;
      this.WriteUnsigned(reg, dat);
    }
    if (cmd.Delay <= 0)
      return;
    Thread.Sleep(cmd.Delay);
  }

  /// <summary>Write page of register, Write unscaled data to register.</summary>
  /// <param name="reg"></param>
  /// <param name="dat"></param>
  /// <remarks></remarks>
  public virtual void WriteUnsigned(RegClass reg, uint dat)
  {
    List<RegDataU32> RegandDataList = new List<RegDataU32>();
    if (reg == null)
      throw new ArgumentNullException("reg must not be null.");
    if (!reg.IsWriteable)
      throw new Exception("Attempted to write a read only register.");
    RegandDataList.Add(new RegDataU32()
    {
      reg = reg,
      dat = new uint?(dat)
    });
    this.WriteUnsigned((IEnumerable<RegDataU32>) RegandDataList);
  }

  /// <summary>Writes unscaled data to registers.</summary>
  /// <param name="regList"></param>
  /// <param name="datList"></param>
  /// <remarks></remarks>
  public void WriteUnsigned(IEnumerable<RegClass> regList, IEnumerable<uint> datList)
  {
    List<RegDataU32> RegandDataList = new List<RegDataU32>();
    if (regList == null | datList == null)
      throw new ArgumentNullException("Parameters must not be null.");
    if (regList.Count<RegClass>() != datList.Count<uint>())
      throw new ArgumentException("RegList and datList must be the same size.");
    if (regList.Count<RegClass>() == 0)
      return;
    int num = checked (regList.Count<RegClass>() - 1);
    int index = 0;
    while (index <= num)
    {
      RegandDataList.Add(new RegDataU32(regList.ElementAtOrDefault<RegClass>(index), new uint?(datList.ElementAtOrDefault<uint>(index))));
      checked { ++index; }
    }
    this.WriteUnsigned((IEnumerable<RegDataU32>) RegandDataList);
  }

  /// <summary>Write unscaled data to register.</summary>
  /// <param name="RegandData"></param>
  /// <remarks></remarks>
  public void WriteUnsigned(RegDataU32 RegandData)
  {
    this.WriteUnsigned((IEnumerable<RegDataU32>) new RegDataU32[1]
    {
      RegandData
    });
  }

  /// <summary>
  /// Writes page register, writes data list element to corrosponding registers list element.
  /// </summary>
  /// <remarks></remarks>
  public abstract void WriteUnsigned(IEnumerable<RegDataU32> RegandDataList);

  /// <summary>
  /// Returns a masked, twos complement decoded and scaled register value based on a data word read from a SPI interface.
  /// </summary>
  /// <param name="Reg">Reg object provides data length, twos complement flag, and scaling factors.</param>
  /// <param name="Data">Data word read back from SPI interface.</param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double ScaleRegData(RegClass Reg, uint Data)
  {
    return !Reg.IsFloat ? (double) Functions.DecodeTwosComp(Reg, Data) * Reg.Scale - Reg.Offset : (double) Functions.DecodeFloat(Data) * Reg.Scale - Reg.Offset;
  }

  /// <summary>Scales multiple read data values.</summary>
  /// <param name="regList"></param>
  /// <param name="uintData"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public double[] ScaleRegData(IEnumerable<RegClass> regList, IEnumerable<uint> uintData)
  {
    DutBase dutBase = this;
    IEnumerable<RegClass> source = regList;
    int num = source.Count<RegClass>();
    // ISSUE: explicit non-virtual call
    return uintData.Select<uint, double>((Func<uint, int, double>) ([SpecialName] (d, i) => __nonvirtual (dutBase.ScaleRegData(source.ElementAtOrDefault<RegClass>(i % num), d)))).ToArray<double>();
  }

  /// <summary>
  /// Uses register properties to select between unscaled float and two's comp. data.
  /// </summary>
  /// <param name="Reg"></param>
  /// <param name="Data"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint UnscaleRegData(RegClass Reg, double Data)
  {
    return !Reg.IsFloat ? Functions.EncodeTwosComp(Reg, checked ((long) Math.Round(unchecked (Data + Reg.Offset / Reg.Scale)))) : Functions.EncodeFloat((Data + Reg.Offset) / Reg.Scale);
  }

  /// <summary>Returns a list of unscaled values read from the specified registers.</summary>
  /// <param name="regList"></param>
  /// <param name="valList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  public uint[] UnscaleRegData(IEnumerable<RegClass> regList, IEnumerable<double> valList)
  {
    uint[] numArray = new uint[checked (valList.Count<double>() - 1 + 1)];
    int num = checked (valList.Count<double>() - 1);
    int index = 0;
    while (index <= num)
    {
      numArray[index] = this.UnscaleRegData(regList.ElementAtOrDefault<RegClass>(index), valList.ElementAtOrDefault<double>(index));
      checked { ++index; }
    }
    return numArray;
  }

  /// <summary>
  /// Converts 16 bit reg data reads to 32, 16, or 8 bit data based on reg List items.
  /// </summary>
  /// <param name="regList">regList used to read the Data.</param>
  /// <param name="u16data">16 bit data from a reg read or get stream data operation.</param>
  /// <returns></returns>
  /// <remarks></remarks>
  public virtual uint[] ConvertReadDataToU32(
    IEnumerable<RegClass> regList,
    IEnumerable<ushort> u16data)
  {
    int index1 = 0;
    int index2 = 0;
    uint[] arySrc = new uint[checked (u16data.Count<ushort>() - 1 + 1)];
    if (u16data.Count<ushort>() % regList.SpiTransferCount() != 0)
      throw new Exception("u16data list size is not a multiple of the reglist data transfer size.");
    while (index2 < u16data.Count<ushort>())
    {
      switch (regList.ElementAtOrDefault<RegClass>(index1 % regList.Count<RegClass>()).NumBytes)
      {
        case 1:
          arySrc[index1] = (long) regList.ElementAtOrDefault<RegClass>(index1 % regList.Count<RegClass>()).Address % 2L != 0L ? (uint) (ushort) ((uint) u16data.ElementAtOrDefault<ushort>(index2) >> 8) & (uint) byte.MaxValue : (uint) u16data.ElementAtOrDefault<ushort>(index2) & (uint) byte.MaxValue;
          break;
        case 2:
          arySrc[index1] = (uint) u16data.ElementAtOrDefault<ushort>(index2);
          break;
        case 4:
          arySrc[index1] = !this.IsLowerFirst ? DutBase.CombineRegWords((uint) u16data.ElementAtOrDefault<ushort>(index2), (uint) u16data.ElementAtOrDefault<ushort>(checked (index2 + 1))) : DutBase.CombineRegWords((uint) u16data.ElementAtOrDefault<ushort>(checked (index2 + 1)), (uint) u16data.ElementAtOrDefault<ushort>(index2));
          checked { ++index2; }
          break;
        default:
          throw new Exception("Numbytes must be 1, 2, or 4.");
      }
      checked { ++index2; }
      checked { ++index1; }
    }
    return (uint[]) Utils.CopyArray((Array) arySrc, (Array) new uint[checked (index1 - 1 + 1)]);
  }

  public void SelectDevice()
  {
    if (this.m_SelectionRoutine == null)
      return;
    this.m_SelectionRoutine(this.m_Selection);
  }

  /// <summary>
  /// Returns a list of address data pairs necessary to write the specified register.
  /// </summary>
  /// <param name="reg"></param>
  /// <param name="dat"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  protected List<AddrDataPair> GetAddrDataPairs(RegClass reg, uint dat)
  {
    List<AddrDataPair> addrDataPairs = new List<AddrDataPair>();
    if (!((IEnumerable<uint>) new uint[3]{ 1U, 2U, 4U }).Contains<uint>(reg.NumBytes))
      throw new ArgumentException("Invalid numBytes in reg. Must be 1, 2, or 4.");
    if (reg.NumBytes < this.DeviceAddressIncrement)
      throw new ArgumentException("Number of bytes in register smaller than addressable register size.");
    if (this.DeviceAddressIncrement == 1U && reg.NumBytes != 1U & (ulong) reg.Address % 2UL > 0UL)
      throw new ArgumentException("Two and four byte registers must have an even address.");
    uint num1 = checked (reg.NumBytes - 1U);
    uint num2 = 0;
    while (num2 <= num1)
    {
      uint num3 = num2 / this.DeviceAddressIncrement;
      addrDataPairs.Add(new AddrDataPair(checked (reg.Address + num3), new uint?(dat >> checked ((int) (8U * num2)) & (uint) byte.MaxValue)));
      checked { ++num2; }
    }
    return addrDataPairs;
  }

  /// <summary>
  /// Creates an array of 16 bit addresses for 16/32 bit register reads, limited to registers on a single page.
  /// </summary>
  /// <param name="regList"></param>
  /// <returns></returns>
  /// <remarks></remarks>
  protected List<uint> MakeReadAddressList(IEnumerable<RegClass> regList)
  {
    List<uint> uintList = new List<uint>();
    uint page = regList.ElementAtOrDefault<RegClass>(0).Page;
    try
    {
      foreach (RegClass reg in regList)
      {
        if (!reg.IsReadable)
          throw new Exception("Attempt made to read a non-readable register.");
        if ((int) reg.Page != (int) page)
          throw new ArgumentException("Attempt made to read registers on multiple pages.");
        uintList.Add(reg.Address);
        if (reg.NumBytes == 4U)
          uintList.Add(checked (reg.Address + (uint) Math.Round(unchecked (2.0 / (double) this.DeviceAddressIncrement))));
      }
    }
    finally
    {
      IEnumerator<RegClass> enumerator;
      enumerator?.Dispose();
    }
    return uintList;
  }

  private static uint CombineRegWords(uint HiWord, uint LoWord)
  {
    HiWord <<= 16 /*0x10*/;
    return HiWord | LoWord;
  }

  [Obsolete("Please use constructor with burstMode parameter and set burst class as appropriate.", true)]
  public DutBase(IRegInterface regInterface, RegMapCollection Reg)
  {
    this.resetDelay = 700;
    this.m_DeviceAddressincrement = 1U;
    this.m_IsLowerFirst = true;
    this.m_SelectionRoutine = (Action<uint>) null;
    this.m_DeviceReadWordSize = 16U /*0x10*/;
    this.m_DeviceWriteWordSize = 8U;
    this.m_Selection = 1U;
  }

  [Obsolete("Please use constructor with burstMode parameter and set burst class as appropriate.", true)]
  public DutBase(Spi spi, gpioBit resetPin, RegMapCollection Reg)
  {
    this.resetDelay = 700;
    this.m_DeviceAddressincrement = 1U;
    this.m_IsLowerFirst = true;
    this.m_SelectionRoutine = (Action<uint>) null;
    this.m_DeviceReadWordSize = 16U /*0x10*/;
    this.m_DeviceWriteWordSize = 8U;
    this.m_Selection = 1U;
  }

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public double[] ReadRegValueArray(IEnumerable<RegClass> regArray) => (double[]) null;

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public double[] ReadRegValueArray(IEnumerable<RegClass> regArray, int numCaptures)
  {
    return (double[]) null;
  }

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public double[] ReadRegValueArray(
    IEnumerable<RegClass> regArray,
    int numCaptures,
    int numBuffers)
  {
    return (double[]) null;
  }

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public uint[] ReadRegDataArray32(IEnumerable<RegClass> regArray, int numCaptures)
  {
    return (uint[]) null;
  }

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public ushort[] ReadRegDataArray(RegClass reg, int numCaptures) => (ushort[]) null;

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public ushort[] ReadRegDataArray(IEnumerable<RegClass> regArray) => (ushort[]) null;

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public ushort[] ReadRegDataArray(IEnumerable<RegClass> regArray, int numCaptures)
  {
    return (ushort[]) null;
  }

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public uint[] ReadRegDataArray32(IEnumerable<RegClass> regArray, int numCaptures, int numBuffers)
  {
    return (uint[]) null;
  }

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public long ReadRegWord(RegClass reg) => 0;

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public ushort ReadRegData(RegClass reg) => 0;

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public double ReadRegDataAverage(RegClass reg, int Averages) => 0.0;

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public double ReadRegValue(RegClass reg) => 0.0;

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  public double ReadRegValue(RegClass reg, int Averages) => 0.0;

  [Obsolete("Please use WriteScaledValue() instead", true)]
  public void WriteRegValue(RegClass Reg, double Value)
  {
  }

  [Obsolete("Please use ReadScaledValue() or ReadUnsigned() instead", true)]
  private ushort ReadReg(uint addr) => 0;

  [Obsolete("This function is obsolete.  Please use DecodeTwosComp()", true)]
  public int TwosComp(RegClass Reg, uint Data)
  {
    int num;
    return num;
  }

  [Obsolete("Please Use WriteUnsigned()", true)]
  public void WriteByte(RegClass reg, uint index, uint dat)
  {
  }

  [Obsolete("Please Use WriteUnsigned().", true)]
  public void WriteRegByte(RegClass reg, uint data)
  {
  }

  [Obsolete("Please Use WriteUnsigned()", true)]
  public void WriteRegByte(uint addr, uint data)
  {
  }

  [Obsolete("Please Use WriteUnsigned()", true)]
  public void WriteRegByte(IEnumerable<uint> addr, IEnumerable<uint> data)
  {
  }

  [Obsolete("Please Use WriteUnsigned()", true)]
  public void WriteRegWord(RegClass reg, uint data)
  {
  }

  [Obsolete("Please Use WriteUnsigned()", true)]
  public void WriteRegByte(uint addr, int data)
  {
  }

  ~DutBase() => base.Finalize();
}
