// Decompiled with JetBrains decompiler
// Type: FX3Api.FX3Connection
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

using AdisApi;
using CyUSB;
using FX3USB;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;
using StreamDataLogger;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Threading;

#nullable disable
namespace FX3Api;

/// <summary>
/// This is the primary class for interfacing with the FX3 based eval platform. Implements IRegInterface, ISpi32Interface, and IPinFcns,
/// in addition to a superset of extra interfacing functions specific to the FX3 platform.
/// </summary>
public class FX3Connection : IRegInterface, IPinFcns, ISpi32Interface, IStreamEventProducer
{
  private const string CYPRESS_BOOTLOADER_NAME = "Cypress FX3 USB BootLoader Device";
  private const string ADI_BOOTLOADER_NAME = "Analog Devices iSensor FX3 Bootloader";
  private const string APPLICATION_NAME = "Analog Devices iSensor FX3 Demonstration Platform";
  private const string FLASH_PROGRAMMER_NAME = "Cypress FX3 USB BootProgrammer Device";
  private const int PROGRAMMING_TIMEOUT = 15000;
  private const int DEVICE_LIST_DELAY = 200;
  private const int MAX_REGLIST_SIZE = 1000;
  private CyControlEndPoint FX3ControlEndPt;
  private CyUSBEndPoint StreamingEndPt;
  private CyUSBEndPoint DataInEndPt;
  private CyUSBEndPoint DataOutEndPt;
  private EventWaitHandle m_AppBoardHandle;
  private EventWaitHandle m_BootloaderBoardHandle;
  private bool m_BoardConnecting;
  private FX3Board m_ActiveFX3Info;
  private Thread BootloaderThread;
  private BlockingCollection<CyFX3Device> BootloaderQueue;
  private bool m_FX3Connected;
  private FX3SPIConfig m_FX3SPIConfig;
  private CyFX3Device m_ActiveFX3;
  private string m_ActiveFX3SN;
  private int m_StreamTimeout;
  private string m_FirmwarePath;
  private string m_BlinkFirmwarePath;
  private string m_FlashProgrammerPath;
  private string m_BootloaderVersion;
  private bool m_DrPolarity;
  private ConcurrentQueue<ushort[]> m_StreamData;
  private ConcurrentQueue<uint[]> m_TransferStreamData;
  private ConcurrentQueue<byte[]> m_I2CStreamData;
  private long m_FramesRead;
  private Thread m_StreamThread;
  private bool m_StreamThreadRunning;
  private Mutex m_StreamMutex;
  private Mutex m_ControlMutex;
  private uint m_TotalBuffersToRead;
  private ushort m_BytesPerBulkRead;
  private USBDeviceList m_usbList;
  private long m_numBadFrames;
  private long m_numFrameSkips;
  private Stopwatch m_streamTimeoutTimer;
  private bool m_pinExit;
  private bool m_pinStart;
  private DeviceType m_sensorType;
  private uint m_burstMode;
  private Stopwatch m_disconnectTimer;
  private string m_disconnectedFX3SN;
  private int m_disconnectEvents;
  private PinList m_PinPwmInfoList;
  private BitBangSpiConfig m_BitBangSpi;
  private bool m_StripBurstTriggerWord;
  private StreamType m_StreamType;
  private ushort m_WatchdogTime;
  private bool m_WatchdogEnable;
  private DutVoltage m_DutSupplyMode;
  private uint m_i2cbitrate;
  private ushort m_i2cRetryCount;
  private ushort RESET_PIN;
  private ushort DIO1_PIN;
  private ushort DIO2_PIN;
  private ushort DIO3_PIN;
  private ushort DIO4_PIN;
  private ushort FX3_GPIO1_PIN;
  private ushort FX3_GPIO2_PIN;
  private ushort FX3_GPIO3_PIN;
  private ushort FX3_GPIO4_PIN;
  private ushort FX3_LOOP1_PIN;
  private ushort FX3_LOOP2_PIN;
  private int m_CrcFirstIndex;
  private int m_CrcLastIndex;
  private int m_CrcResultIndex;
  private int m_BurstByteCount;
  private RegClass m_TriggerReg;
  private byte[] m_burstMOSIData;
  /// <summary>
  /// SPI data to transmit at the start of a transfer stream. No read
  /// back performed
  /// </summary>
  private List<uint> m_SPI32InitialMOSI;
  /// <summary>Track if initial MOSI data is transfered with DrActive</summary>
  private bool m_SPI32InitialDrActive;

  /// <summary>
  /// This event is raised when the active board is disconnected unexpectedly (IE unplugged)
  /// </summary>
  /// <param name="FX3SerialNum">Serial number of the board which was disconnected</param>
  public event FX3Connection.UnexpectedDisconnectEventHandler UnexpectedDisconnect;

  /// <summary>
  /// This event is raised when the disconnect event for a board has finished, and it is reprogrammed with the ADI bootloader. This event only is triggered for boards
  /// which were explicitly disconnected (boards which were physically reset will not trigger this event).
  /// </summary>
  /// <param name="FX3SerialNum">Serial number of the board</param>
  /// <param name="DisconnectTime">Time (in ms) elapsed between the disconnect call and board re-enumeration</param>
  public event FX3Connection.DisconnectFinishedEventHandler DisconnectFinished;

  /// <summary>
  /// This event is raised when there is a new buffer available from a buffered stream
  /// </summary>
  public event IStreamEventProducer.NewBufferAvailableEventHandler NewBufferAvailable;

  /// <summary>This event is raised when a stream is finished</summary>
  public event IStreamEventProducer.StreamFinishedEventHandler StreamFinished;

  /// <summary>
  /// Class Constructor. Loads SPI settings and default values for the interface, and starts a background thread to manage programming newly
  /// connected boards with the ADI bootloader.
  /// </summary>
  /// <param name="FX3FirmwarePath">The path to the FX3 application firmware image file.</param>
  /// <param name="FX3BootloaderPath">The path to the ADI FX3 bootloader image file.</param>
  /// <param name="FX3ProgrammerPath">The path to the flash programmer application image file.</param>
  /// <param name="SensorType">The sensor type. Valid inputs are IMU and ADcmXL. Default is IMU.</param>
  public FX3Connection(
    string FX3FirmwarePath,
    string FX3BootloaderPath,
    string FX3ProgrammerPath,
    DeviceType SensorType = DeviceType.IMU)
  {
    this.m_ActiveFX3 = (CyFX3Device) null;
    this.m_ActiveFX3SN = (string) null;
    this.m_FirmwarePath = "PathNotSet";
    this.m_BlinkFirmwarePath = "PathNotSet";
    this.m_FlashProgrammerPath = "PathNotSet";
    this.m_BootloaderVersion = "1.0.0";
    this.m_DrPolarity = true;
    this.m_FramesRead = 0L;
    this.m_TotalBuffersToRead = 0U;
    this.m_pinExit = false;
    this.m_pinStart = false;
    this.m_burstMode = 0U;
    this.RESET_PIN = (ushort) 10;
    this.DIO1_PIN = (ushort) 3;
    this.DIO2_PIN = (ushort) 2;
    this.DIO3_PIN = (ushort) 1;
    this.DIO4_PIN = (ushort) 0;
    this.FX3_GPIO1_PIN = (ushort) 4;
    this.FX3_GPIO2_PIN = (ushort) 5;
    this.FX3_GPIO3_PIN = (ushort) 6;
    this.FX3_GPIO4_PIN = (ushort) 12;
    this.FX3_LOOP1_PIN = (ushort) 25;
    this.FX3_LOOP2_PIN = (ushort) 26;
    this.m_SPI32InitialMOSI = new List<uint>();
    this.m_sensorType = SensorType;
    this.FirmwarePath = FX3FirmwarePath;
    this.BootloaderPath = FX3BootloaderPath;
    this.FlashProgrammerPath = FX3ProgrammerPath;
    this.SetDefaultValues(this.m_sensorType);
    this.m_AppBoardHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
    this.m_BootloaderBoardHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
    this.BootloaderQueue = new BlockingCollection<CyFX3Device>();
    this.BootloaderThread = new Thread(new ThreadStart(this.ProgramBootloaderThread));
    this.BootloaderThread.IsBackground = true;
    this.BootloaderThread.Start();
    this.InitBoardList();
  }

  /// <summary>
  /// Sets the default values for the interface. Used in constructor and after FX3 reset.
  /// </summary>
  /// <param name="SensorType">Parameter to specify default device SPI settings. Valid options are IMU and ADcmXL</param>
  private void SetDefaultValues(DeviceType SensorType)
  {
    this.m_FX3SPIConfig = new FX3SPIConfig(SensorType);
    this.m_FX3Connected = false;
    this.m_ActiveFX3 = (CyFX3Device) null;
    this.m_ActiveFX3SN = (string) null;
    this.m_ActiveFX3Info = (FX3Board) null;
    this.m_StreamData = new ConcurrentQueue<ushort[]>();
    this.m_StreamThreadRunning = false;
    this.m_StreamMutex = new Mutex();
    this.m_TotalBuffersToRead = 0U;
    this.m_numBadFrames = 0L;
    this.m_StreamTimeout = 10;
    this.m_StreamType = StreamType.None;
    this.m_ControlMutex = new Mutex();
    this.m_streamTimeoutTimer = new Stopwatch();
    this.m_BoardConnecting = false;
    this.m_PinPwmInfoList = new PinList();
    this.m_BitBangSpi = new BitBangSpiConfig(false);
    this.m_StripBurstTriggerWord = true;
    this.m_WatchdogEnable = true;
    this.m_WatchdogTime = (ushort) 20;
    this.m_i2cbitrate = 100000U;
    this.m_i2cRetryCount = (ushort) 1;
    this.m_DutSupplyMode = DutVoltage.On3_3Volts;
  }

  /// <summary>
  /// Property to get or set the FX3 SPI clock frequency setting.
  /// Reqcode:   B2
  /// Value:     Don't Care
  /// Index:     0
  /// Length:    4
  /// Data:      Clock Frequency to be set
  /// </summary>
  /// <returns>The current SPI clock frequency, in MHZ. Valid values are in the range 1 to 40,000,000</returns>
  public int SclkFrequency
  {
    get => this.m_FX3SPIConfig.SCLKFrequency;
    set
    {
      this.m_FX3SPIConfig.SCLKFrequency = !(Information.IsNothing((object) value) | value > 40000000 | value < 1) ? value : throw new FX3ConfigurationException("ERROR: Invalid Sclk Frequency entered. Must be in the range (1-40000000)");
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 0;
      this.ConfigureSPI(this.m_FX3SPIConfig.SCLKFrequency);
    }
  }

  /// <summary>
  /// Property to get or set the FX3 SPI controller clock polarity setting (True - Idles High, False - Idles Low)
  /// Reqcode:   B2
  /// Value:     Polarity (0 active low, 1 active high)
  /// 'Index:    1
  /// Length:    4
  /// Data:      None
  /// </summary>
  /// <returns>The current polarity setting</returns>
  public bool Cpol
  {
    get => this.m_FX3SPIConfig.Cpol;
    set
    {
      this.m_FX3SPIConfig.Cpol = value;
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 1;
      this.m_ActiveFX3.ControlEndPt.Value = (ushort) -(this.m_FX3SPIConfig.Cpol ? 1 : 0);
      this.ConfigureSPI();
    }
  }

  /// <summary>
  /// Property to get or set the FX3 SPI controller chip select phase
  /// Reqcode:   B2
  /// Value:     Polarity (0 active low, 1 active high)
  /// Index:     2
  /// Length:    4
  /// Data:      None
  /// </summary>
  /// <returns>The current chip select phase setting</returns>
  public bool Cpha
  {
    get => this.m_FX3SPIConfig.Cpha;
    set
    {
      this.m_FX3SPIConfig.Cpha = value;
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 2;
      this.m_ActiveFX3.ControlEndPt.Value = (ushort) -(this.m_FX3SPIConfig.Cpha ? 1 : 0);
      this.ConfigureSPI();
    }
  }

  /// <summary>
  /// Property to get or set the FX3 SPI chip select polarity (True - Active High, False - Active Low)
  /// Reqcode:   B2
  /// Value:     Polarity (0 active low, 1 active high)
  /// Index:     3
  /// Length:    4
  /// Data:      None
  /// </summary>
  /// <returns>The current chip select polarity</returns>
  public bool ChipSelectPolarity
  {
    get => this.m_FX3SPIConfig.ChipSelectPolarity;
    set
    {
      this.m_FX3SPIConfig.ChipSelectPolarity = value;
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 3;
      this.m_ActiveFX3.ControlEndPt.Value = (ushort) -(this.m_FX3SPIConfig.ChipSelectPolarity ? 1 : 0);
      this.ConfigureSPI();
    }
  }

  /// <summary>
  /// Property to get or set the FX3 SPI controller chip select setting. Should be left on hardware control, changing modes will likely cause unexpected behavior.
  /// Reqcode:   B2
  /// Value:     Desired setting (as SpiChipselectControl )
  /// Index:     4
  /// Length:    4
  /// Data:      None
  /// </summary>
  /// <returns>The current chip select control mode</returns>
  public SpiChipselectControl ChipSelectControl
  {
    get => this.m_FX3SPIConfig.ChipSelectControl;
    set
    {
      this.m_FX3SPIConfig.ChipSelectControl = value;
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 4;
      this.m_ActiveFX3.ControlEndPt.Value = checked ((ushort) (uint) this.m_FX3SPIConfig.ChipSelectControl);
      this.ConfigureSPI();
    }
  }

  /// <summary>
  /// The number of SPI clock cycles before the SPI transaction that chip select is toggled to active.
  /// Reqcode:   B2
  /// Value:     Desired Setting (as SpiLagLeadTime )
  /// Index:     5
  /// Length:    4
  /// Data:      None
  /// </summary>
  /// <returns>The current chip select lead time setting</returns>
  public SpiLagLeadTime ChipSelectLeadTime
  {
    get => this.m_FX3SPIConfig.ChipSelectLeadTime;
    set
    {
      this.m_FX3SPIConfig.ChipSelectLeadTime = value != SpiLagLeadTime.SPI_SSN_LAG_LEAD_ZERO_CLK ? value : throw new FX3ConfigurationException("ERROR: Chip select lead time of 0 clocks not supported!");
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 5;
      this.m_ActiveFX3.ControlEndPt.Value = checked ((ushort) (uint) this.m_FX3SPIConfig.ChipSelectLeadTime);
      this.ConfigureSPI();
    }
  }

  /// <summary>
  /// The number of SPI clock cycles after the transaction ends that chip select is toggled to idle.
  /// Reqcode:   B2
  /// Value:     Desired Setting (as SpiLagLeadTime )
  /// Index:     6
  /// Length:    4
  /// Data:      None
  /// </summary>
  /// <returns>The current chip select lag time setting</returns>
  public SpiLagLeadTime ChipSelectLagTime
  {
    get => this.m_FX3SPIConfig.ChipSelectLagTime;
    set
    {
      this.m_FX3SPIConfig.ChipSelectLagTime = value;
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 6;
      this.m_ActiveFX3.ControlEndPt.Value = checked ((ushort) (uint) this.m_FX3SPIConfig.ChipSelectLagTime);
      this.ConfigureSPI();
    }
  }

  /// <summary>
  /// The FX3 SPI Controller LSB setting. The controller flips the bits depending on this setting.
  /// Reqcode:   B2
  /// Value:     Polarity (0 MSB first, 1 LSB first)
  /// Index:     7
  /// Length:    4
  /// Data:      None
  /// </summary>
  /// <returns>The current LSB First setting, as a boolean</returns>
  public bool IsLSBFirst
  {
    get => this.m_FX3SPIConfig.IsLSBFirst;
    set
    {
      this.m_FX3SPIConfig.IsLSBFirst = value;
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 7;
      this.m_ActiveFX3.ControlEndPt.Value = (ushort) -(this.m_FX3SPIConfig.IsLSBFirst ? 1 : 0);
      this.ConfigureSPI();
    }
  }

  /// <summary>
  /// The FX3 SPI controller word length. Default is 8 (1 byte per word)
  /// Reqcode:   B2
  /// Value:     Word length (as int8)
  /// Index:     8
  /// Length:    4
  /// Data:      None
  /// </summary>
  /// <returns>The current word length</returns>
  public byte WordLength
  {
    get => this.m_FX3SPIConfig.WordLength;
    set
    {
      this.m_FX3SPIConfig.WordLength = (uint) value % 8U <= 0U ? value : throw new FX3ConfigurationException("ERROR: Word length must by a multiple of 8 bits");
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 8;
      this.m_ActiveFX3.ControlEndPt.Value = (ushort) this.m_FX3SPIConfig.WordLength;
      this.ConfigureSPI();
    }
  }

  /// <summary>
  /// Property to get/set the number of microseconds between words
  /// Reqcode:   B2
  /// Value:     Stall time in microseconds (as int16)
  /// Index:     9
  /// Length:    4
  /// Data:      None
  /// </summary>
  /// <returns>The current stall time, in microseconds</returns>
  public ushort StallTime
  {
    get => this.m_FX3SPIConfig.StallTime;
    set
    {
      this.m_FX3SPIConfig.StallTime = value;
      if (this.m_FX3Connected)
      {
        this.m_ActiveFX3.ControlEndPt.Index = (ushort) 9;
        this.m_ActiveFX3.ControlEndPt.Value = this.m_FX3SPIConfig.StallTime;
        this.ConfigureSPI();
      }
      if (Information.IsNothing((object) this.m_BitBangSpi))
        return;
      this.SetBitBangStallTime((double) value);
    }
  }

  /// <summary>
  /// The DUT type connected to the board.
  /// Reqcode:   B2
  /// Value:     Part type to set
  /// Index:     10
  /// Length:    4
  /// Data:      None
  /// </summary>
  /// <returns>Returns the DUTType. Defaults to 3 axis</returns>
  public DUTType PartType
  {
    get => this.m_FX3SPIConfig.DUTType;
    set
    {
      this.m_FX3SPIConfig.DUTType = value;
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 10;
      this.m_ActiveFX3.ControlEndPt.Value = checked ((ushort) (uint) this.m_FX3SPIConfig.DUTType);
      this.ConfigureSPI();
    }
  }

  /// <summary>The Data Ready polarity for streaming mode (index 11)</summary>
  /// <returns>The data ready polarity, as a boolean (True - low to high, False - high to low)</returns>
  public bool DrPolarity
  {
    get => this.m_FX3SPIConfig.DrPolarity;
    set
    {
      this.m_FX3SPIConfig.DrPolarity = value;
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 11;
      this.m_ActiveFX3.ControlEndPt.Value = (ushort) -(this.m_FX3SPIConfig.DrPolarity ? 1 : 0);
      this.ConfigureSPI();
    }
  }

  /// <summary>Property to get or set the DUT data ready pin.</summary>
  /// <returns>The IPinObject of the pin currently configured as the data ready</returns>
  public IPinObject ReadyPin
  {
    get => (IPinObject) this.m_FX3SPIConfig.DataReadyPin;
    set
    {
      this.m_FX3SPIConfig.DataReadyPin = this.IsFX3Pin(value) ? (FX3PinObject) value : throw new FX3ConfigurationException("ERROR: FX3 Connection must take an FX3 pin object");
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 13;
      this.m_ActiveFX3.ControlEndPt.Value = checked ((ushort) (unchecked ((int) this.m_FX3SPIConfig.DataReadyPinFX3GPIO) & (int) ushort.MaxValue));
      this.ConfigureSPI();
    }
  }

  /// <summary>
  /// Read only property to get the timer tick scale factor used for converting ticks to ms.
  /// </summary>
  /// <returns></returns>
  public uint TimerTickScaleFactor => this.m_FX3SPIConfig.SecondsToTimerTicks;

  /// <summary>Function to read the current SPI parameters from the FX3 board</summary>
  /// <returns>Returns a FX3SPIConfig struct representing the current board configuration</returns>
  private FX3SPIConfig GetBoardSpiParameters()
  {
    byte[] Buf = new byte[23];
    FX3SPIConfig boardSpiParameters = new FX3SPIConfig();
    this.m_ActiveFX3.ControlEndPt.Target = (byte) 0;
    this.m_ActiveFX3.ControlEndPt.Direction = (byte) 128 /*0x80*/;
    this.m_ActiveFX3.ControlEndPt.ReqType = (byte) 64 /*0x40*/;
    this.m_ActiveFX3.ControlEndPt.ReqCode = (byte) 179;
    this.m_ActiveFX3.ControlEndPt.Value = (ushort) 0;
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 0;
    boardSpiParameters.SCLKFrequency = this.XferControlData(ref Buf, 23, 2000) ? BitConverter.ToInt32(Buf, 0) : throw new FX3CommunicationException("ERROR: Control endpoint transfer for SPI configuration failed.");
    boardSpiParameters.Cpha = Buf[4] > (byte) 0;
    boardSpiParameters.Cpol = Buf[5] > (byte) 0;
    boardSpiParameters.IsLSBFirst = Buf[6] > (byte) 0;
    boardSpiParameters.ChipSelectLagTime = (SpiLagLeadTime) Buf[7];
    boardSpiParameters.ChipSelectLeadTime = (SpiLagLeadTime) Buf[8];
    boardSpiParameters.ChipSelectControl = (SpiChipselectControl) Buf[9];
    boardSpiParameters.ChipSelectPolarity = Buf[10] > (byte) 0;
    boardSpiParameters.WordLength = Buf[11];
    boardSpiParameters.StallTime = BitConverter.ToUInt16(Buf, 12);
    boardSpiParameters.DUTType = (DUTType) Buf[14];
    boardSpiParameters.DrActive = Buf[15] > (byte) 0;
    boardSpiParameters.DrPolarity = Buf[16 /*0x10*/] > (byte) 0;
    boardSpiParameters.DataReadyPinFX3GPIO = (uint) BitConverter.ToUInt16(Buf, 17);
    boardSpiParameters.SecondsToTimerTicks = BitConverter.ToUInt32(Buf, 19);
    return boardSpiParameters;
  }

  /// <summary>Function which writes the current SPI config to the FX3</summary>
  private void WriteBoardSpiParameters()
  {
    FX3SPIConfig boardSpiParameters = this.GetBoardSpiParameters();
    if (boardSpiParameters.SCLKFrequency != this.m_FX3SPIConfig.SCLKFrequency)
      this.SclkFrequency = this.m_FX3SPIConfig.SCLKFrequency;
    if (boardSpiParameters.Cpha != this.m_FX3SPIConfig.Cpha)
      this.Cpha = this.m_FX3SPIConfig.Cpha;
    if (boardSpiParameters.Cpol != this.m_FX3SPIConfig.Cpol)
      this.Cpol = this.m_FX3SPIConfig.Cpol;
    if ((int) boardSpiParameters.StallTime != (int) this.m_FX3SPIConfig.StallTime)
      this.StallTime = this.m_FX3SPIConfig.StallTime;
    if (boardSpiParameters.ChipSelectLagTime != this.m_FX3SPIConfig.ChipSelectLagTime)
      this.ChipSelectLagTime = this.m_FX3SPIConfig.ChipSelectLagTime;
    if (boardSpiParameters.ChipSelectLeadTime != this.m_FX3SPIConfig.ChipSelectLeadTime)
      this.ChipSelectLeadTime = this.m_FX3SPIConfig.ChipSelectLeadTime;
    if (boardSpiParameters.ChipSelectControl != this.m_FX3SPIConfig.ChipSelectControl)
      this.ChipSelectControl = this.m_FX3SPIConfig.ChipSelectControl;
    if (boardSpiParameters.ChipSelectPolarity != this.m_FX3SPIConfig.ChipSelectPolarity)
      this.ChipSelectPolarity = this.m_FX3SPIConfig.ChipSelectPolarity;
    if (boardSpiParameters.DUTType != this.m_FX3SPIConfig.DUTType)
      this.PartType = this.m_FX3SPIConfig.DUTType;
    if (boardSpiParameters.IsLSBFirst != this.m_FX3SPIConfig.IsLSBFirst)
      this.IsLSBFirst = this.m_FX3SPIConfig.IsLSBFirst;
    if ((int) boardSpiParameters.WordLength != (int) this.m_FX3SPIConfig.WordLength)
      this.WordLength = this.m_FX3SPIConfig.WordLength;
    if ((int) boardSpiParameters.DataReadyPinFX3GPIO != (int) this.m_FX3SPIConfig.DataReadyPinFX3GPIO)
      this.ReadyPin = (IPinObject) this.m_FX3SPIConfig.DataReadyPin;
    if (boardSpiParameters.DrActive != this.m_FX3SPIConfig.DrActive)
      this.DrActive = this.m_FX3SPIConfig.DrActive;
    if (boardSpiParameters.DrPolarity != this.m_FX3SPIConfig.DrPolarity)
      this.DrPolarity = this.m_FX3SPIConfig.DrPolarity;
    this.m_FX3SPIConfig.SecondsToTimerTicks = boardSpiParameters.SecondsToTimerTicks;
  }

  /// <summary>
  /// Function which performs the SPI configuration option based on the current control endpoint setting
  /// </summary>
  /// <param name="clockFrequency">The SPI clock frequency, if it needs to be set</param>
  private void ConfigureSPI(int clockFrequency = 0)
  {
    byte[] Buf = new byte[4];
    if (!this.m_FX3Connected)
      return;
    this.m_ActiveFX3.ControlEndPt.Target = (byte) 0;
    this.m_ActiveFX3.ControlEndPt.Direction = (byte) 0;
    this.m_ActiveFX3.ControlEndPt.ReqType = (byte) 64 /*0x40*/;
    this.m_ActiveFX3.ControlEndPt.ReqCode = (byte) 178;
    if (clockFrequency != 0)
    {
      Buf[0] = checked ((byte) ((long) (clockFrequency >> 24) & (long) byte.MaxValue));
      Buf[1] = checked ((byte) ((long) (clockFrequency >> 16 /*0x10*/) & (long) byte.MaxValue));
      Buf[2] = checked ((byte) ((long) (clockFrequency >> 8) & (long) byte.MaxValue));
      Buf[3] = checked ((byte) ((long) clockFrequency & (long) byte.MaxValue));
    }
    if (!this.XferControlData(ref Buf, 4, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer for SPI configuration timed out.");
  }

  /// <summary>
  /// This function reads the current value from the 10MHz timer running on the FX3
  /// </summary>
  /// <returns>The 32-bit timer value</returns>
  public uint GetTimerValue()
  {
    byte[] Buf = new byte[8];
    this.ConfigureControlEndpoint(USBCommands.ADI_READ_TIMER_VALUE, false);
    uint num = this.XferControlData(ref Buf, 8, 2000) ? BitConverter.ToUInt32(Buf, 0) : throw new FX3CommunicationException("ERROR: Control endpoint transfer to read timer value timed out!");
    if (num != 0)
      throw new FX3BadStatusException("ERROR: Bad status code after reading timer. Error code: 0x" + num.ToString("X4"));
    return BitConverter.ToUInt32(Buf, 4);
  }

  /// <summary>
  /// Set the FX3 GPIO input stage pull up or pull down resistor setting. All FX3 GPIOs have a software configurable
  /// pull up / pull down resistor (10KOhm).
  /// </summary>
  /// <param name="Pin">The pin to set (FX3PinObject)</param>
  /// <param name="Setting">The pin resistor setting to apply</param>
  public void SetPinResistorSetting(IPinObject Pin, FX3PinResistorSetting Setting)
  {
    byte[] Buf = new byte[4];
    if (!this.IsFX3Pin(Pin))
      throw new FX3ConfigurationException("ERROR: FX3 pin functions must operate with FX3PinObjects");
    this.ConfigureControlEndpoint(USBCommands.ADI_SET_PIN_RESISTOR, false);
    this.FX3ControlEndPt.Value = checked ((ushort) (uint) Setting);
    this.FX3ControlEndPt.Index = checked ((ushort) (unchecked ((int) Pin.pinConfig) & (int) ushort.MaxValue));
    uint num = this.XferControlData(ref Buf, 4, 2000) ? BitConverter.ToUInt32(Buf, 0) : throw new FX3CommunicationException("ERROR: Control endpoint transfer to set pin resistor configuration failed!");
    if (num != 0)
      throw new FX3BadStatusException("ERROR: Bad status code after setting pin resistor configuration. Error code: 0x" + num.ToString("X4"));
  }

  /// <summary>
  /// Set the FX3 firmware watchdog timeout period (in seconds). If the watchdog is triggered the FX3 will reset.
  /// </summary>
  /// <returns></returns>
  public ushort WatchdogTimeoutSeconds
  {
    get => this.m_WatchdogTime;
    set
    {
      if (value < (ushort) 10)
        throw new FX3ConfigurationException($"ERROR: Invalid watchdog timeout period of {value.ToString()}s. Must be at least 10 seconds");
      this.m_WatchdogTime = value <= ushort.MaxValue ? value : throw new FX3ConfigurationException($"ERROR: Invalid watchdog timeout period {value.ToString()}s");
      this.UpdateWatchdog();
    }
  }

  /// <summary>Enable or disable the FX3 firmware watchdog.</summary>
  /// <returns></returns>
  public bool WatchdogEnable
  {
    get => this.m_WatchdogEnable;
    set
    {
      this.m_WatchdogEnable = value;
      this.UpdateWatchdog();
    }
  }

  /// <summary>
  /// Get or set the DUT supply mode on the FX3. Available options are regulated 3.3V, USB 5V, and off. This feature is only available on the
  /// ADI in-house iSensor FX3 eval platform, not the platform based on the Cypress Explorer kit. If a Cypress Explorer kit is connected, the
  /// setter for this property will be disabled.
  /// </summary>
  /// <returns>The DUT supply voltage setting</returns>
  public DutVoltage DutSupplyMode
  {
    get => this.m_DutSupplyMode;
    set
    {
      if (this.m_ActiveFX3Info.BoardType == FX3BoardType.CypressFX3Board)
        return;
      this.ConfigureControlEndpoint(USBCommands.ADI_SET_DUT_SUPPLY, false);
      this.FX3ControlEndPt.Index = (ushort) 0;
      this.FX3ControlEndPt.Value = checked ((ushort) (uint) value);
      byte[] Buf = new byte[4];
      uint num = this.XferControlData(ref Buf, 4, 2000) ? BitConverter.ToUInt32(Buf, 0) : throw new FX3CommunicationException("ERROR: Setting DUT supply mode timed out!");
      if (num != 0)
        throw new FX3BadStatusException("ERROR: Bad status code after power supply set. Error code: 0x" + num.ToString("X4"));
      this.m_DutSupplyMode = value;
    }
  }

  private void UpdateWatchdog()
  {
    this.ConfigureControlEndpoint(USBCommands.ADI_SET_SPI_CONFIG, true);
    if (this.m_WatchdogEnable)
    {
      this.FX3ControlEndPt.Index = (ushort) 14;
      this.FX3ControlEndPt.Value = this.m_WatchdogTime;
    }
    else
    {
      this.FX3ControlEndPt.Index = (ushort) 15;
      this.FX3ControlEndPt.Value = this.m_WatchdogTime;
    }
    byte[] Buf = new byte[4];
    if (!this.XferControlData(ref Buf, 4, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer for watchdog configuration failed.");
  }

  /// <summary>Get the firmware build date and time</summary>
  /// <returns></returns>
  private string GetFirmwareBuildDate()
  {
    this.ConfigureControlEndpoint(USBCommands.ADI_GET_BUILD_DATE, false);
    byte[] Buf = new byte[20];
    return this.XferControlData(ref Buf, 20, 2000) ? Encoding.UTF8.GetString(Buf) : throw new FX3CommunicationException("ERROR: Control endpoint transfer to get firmware build date/time failed.");
  }

  /// <summary>Set the boot unix timestamp in the FX3 application firmware</summary>
  private void SetBootTimeStamp()
  {
    uint num = checked ((uint) Math.Round((unchecked (DateTime.UtcNow - new DateTime(621355968000000000L))).TotalSeconds));
    byte[] Buf = new byte[4]
    {
      checked ((byte) (unchecked ((int) num) & (int) byte.MaxValue)),
      checked ((byte) ((num & 65280U) >> 8)),
      checked ((byte) ((num & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)),
      checked ((byte) ((num & 4278190080U /*0xFF000000*/) >> 24))
    };
    this.ConfigureControlEndpoint(USBCommands.ADI_SET_BOOT_TIME, true);
    if (!this.XferControlData(ref Buf, 4, 2000))
      throw new FX3CommunicationException("ERROR: Setting FX3 boot time failed!");
  }

  /// <summary>Gets the current status code from the FX3.</summary>
  /// <param name="VerboseMode">Return by reference of the verbose mode of the FX3</param>
  private uint GetBoardStatus(ref bool VerboseMode)
  {
    byte[] Buf = new byte[5];
    this.ConfigureControlEndpoint(USBCommands.ADI_GET_STATUS, false);
    if (!this.XferControlData(ref Buf, 5, 2000))
      throw new FX3CommunicationException("ERROR: Pin set operation timed out");
    VerboseMode = Buf[4] > (byte) 0;
    return BitConverter.ToUInt32(Buf, 0);
  }

  /// <summary>
  /// This property returns a class containing some useful information about the current FX3 Dll. Some of the
  /// information is available as a attribute of the DLL, while others (build date/time and git revision) are
  /// generated at compile time using a pre-build batch file script.
  /// </summary>
  /// <returns></returns>
  public FX3ApiInfo GetFX3ApiInfo
  {
    get
    {
      FX3ApiInfo getFx3ApiInfo = new FX3ApiInfo();
      AssemblyInfo assemblyInfo = new AssemblyInfo(Assembly.GetAssembly(typeof (FX3Connection)));
      getFx3ApiInfo.Name = assemblyInfo.AssemblyName;
      string str1 = assemblyInfo.Version.ToString();
      string str2 = str1.Remove(checked (str1.Length - 2));
      getFx3ApiInfo.VersionNumber = str2 + "-PUB";
      getFx3ApiInfo.Description = assemblyInfo.Description;
      getFx3ApiInfo.BuildDateTime = FX3Api.My.Resources.Resources.BuildDate;
      getFx3ApiInfo.GitURL = FX3Api.My.Resources.Resources.CurrentURL;
      getFx3ApiInfo.GitBranch = FX3Api.My.Resources.Resources.CurrentBranch;
      getFx3ApiInfo.GitCommitSHA1 = FX3Api.My.Resources.Resources.CurrentCommit;
      return getFx3ApiInfo;
    }
  }

  /// <summary>
  /// Read-only property to get the number of bad frames purged with a call to PurgeBadFrameData. Frames are purged when the CRC appended to the end of
  /// the frame does not match the expected CRC.
  /// </summary>
  /// <returns>Number of frames purged from data array</returns>
  public long NumFramesPurged => this.m_numBadFrames;

  /// <summary>Property to get the number of frame skips in an ADcmXL real time stream</summary>
  /// <returns></returns>
  public long NumFramesSkipped => this.m_numFrameSkips;

  /// <summary>
  /// Property to get the device family type the FX3 was initialized for. Setting this property restores all SPI settings to the
  /// default for the selected device family.
  /// </summary>
  /// <returns>The current device mode, as an FX3Interface.DeviceType</returns>
  public DeviceType SensorType
  {
    get => this.m_sensorType;
    set
    {
      if (this.m_sensorType == value)
        return;
      this.m_sensorType = value;
      this.m_FX3SPIConfig = new FX3SPIConfig(this.m_sensorType, this.m_ActiveFX3Info.BoardType);
      this.WriteBoardSpiParameters();
    }
  }

  /// <summary>
  /// Gets and sets the sync pin exit configuration for exiting real-time stream mode on ADcmXL DUT's.
  /// </summary>
  /// <returns>RTS pin exit configuration (false = Pin Exit Disabled, true = Pin Exit Enabled)</returns>
  public bool PinExit
  {
    get => this.m_pinExit;
    set => this.m_pinExit = value;
  }

  /// <summary>
  /// Gets and sets the sync pin start configuration for starting real-time stream mode on ADcmXL DUT's.
  /// </summary>
  /// <returns>RTS pin start configuration (false = Pin Start Disabled, true = Pin Start Enabled</returns>
  public bool PinStart
  {
    get => this.m_pinStart;
    set => this.m_pinStart = value;
  }

  /// <summary>
  /// Checks if a streaming frame is available, or will be available soon in thread safe queue. If there is no data in the queue
  /// and the streaming thread is not currently running, it will return false.
  /// </summary>
  /// <returns>The frame availability</returns>
  public bool BufferAvailable
  {
    get
    {
      return !Information.IsNothing((object) this.m_StreamData) && this.m_StreamData.Count > 0 | this.m_StreamThreadRunning;
    }
  }

  /// <summary>
  /// Gets one frame from the thread safe queue. Waits to return until a frame is available if there is a stream running. If
  /// there is not a stream running, and there is no data in the queue this call returns "Nothing".
  /// </summary>
  /// <returns>The frame, as a byte array</returns>
  public ushort[] GetBuffer()
  {
    if (Information.IsNothing((object) this.m_StreamData) || this.m_StreamData.Count == 0 & !this.m_StreamThreadRunning)
      return (ushort[]) null;
    ushort[] result = (ushort[]) null;
    this.m_streamTimeoutTimer.Restart();
    this.m_StreamData.TryDequeue(out result);
    return result;
  }

  /// <summary>
  /// Read-only property to get the number of buffers read in from the DUT in buffered streaming mode
  /// </summary>
  /// <returns>The current buffer read count</returns>
  public long GetNumBuffersRead => Interlocked.Read(ref this.m_FramesRead);

  /// <summary>
  /// Expects bytes in the order they are clocked out of ADcmXLx021
  /// CRC-16-CCITT, initialized with CRC = 0xFFFF, No final XOR.
  /// Limit crc accumulation to 16 bits to prevent U32 overflow.
  ///  </summary>
  /// <param name="ByteData">The input data set to calculate the CRC of</param>
  /// <returns>The CRC value for the input array</returns>
  private uint calcCCITT16(byte[] ByteData)
  {
    uint num1 = (uint) ushort.MaxValue;
    uint num2 = 4129;
    int num3 = checked (((IEnumerable<byte>) ByteData).Count<byte>() - 1);
    int num4 = 0;
    while (num4 <= num3)
    {
      int num5 = 1;
      do
      {
        uint num6 = (uint) ByteData[checked (num4 + num5)];
        num1 ^= num6 << 8;
        int num7 = 1;
        do
        {
          num1 = (((long) num1 & 32768L /*0x8000*/) != 32768L /*0x8000*/ ? checked (num1 * 2U) : checked (num1 * 2U) ^ num2) & (uint) ushort.MaxValue;
          checked { ++num7; }
        }
        while (num7 <= 8);
        checked { num5 += -1; }
      }
      while (num5 >= 0);
      checked { num4 += 2; }
    }
    return num1;
  }

  /// <summary>Overload for CRC calculation which takes UShort array</summary>
  /// <param name="UShortData">The data to calculate CRC for</param>
  /// <returns>The CRC value</returns>
  private uint calcCCITT16(ushort[] UShortData)
  {
    uint num1 = (uint) ushort.MaxValue;
    uint num2 = 4129;
    int num3 = checked (((IEnumerable<ushort>) UShortData).Count<ushort>() - 1);
    int index = 0;
    while (index <= num3)
    {
      int num4 = 0;
      do
      {
        uint num5 = num4 != 1 ? (uint) UShortData[index] & (uint) byte.MaxValue : ((uint) UShortData[index] & 65280U) >> 8;
        num1 ^= num5 << 8;
        int num6 = 1;
        do
        {
          num1 = (((long) num1 & 32768L /*0x8000*/) != 32768L /*0x8000*/ ? checked (num1 * 2U) : checked (num1 * 2U) ^ num2) & (uint) ushort.MaxValue;
          checked { ++num6; }
        }
        while (num6 <= 8);
        checked { ++num4; }
      }
      while (num4 <= 1);
      checked { ++index; }
    }
    return num1;
  }

  /// <summary>Checks the CRC for a real time frame</summary>
  /// <param name="frame">The frame to check</param>
  /// <returns>A boolean indicating if the accelerometer data CRC matches the frame CRC</returns>
  private bool CheckDUTCRC(ref ushort[] frame)
  {
    ushort num1 = frame[checked (((IEnumerable<ushort>) frame).Count<ushort>() - 1)];
    ushort num2 = (ushort) ((uint) num1 >> 8);
    ushort num3 = checked ((ushort) unchecked ((int) (ushort) ((uint) num1 << 8) + (int) num2));
    List<ushort> ushortList = new List<ushort>();
    ushortList.Clear();
    if (this.m_FX3SPIConfig.DUTType == DUTType.ADcmXL3021)
    {
      int num4 = checked (((IEnumerable<ushort>) frame).Count<ushort>() - 4);
      int index = 1;
      while (index <= num4)
      {
        ushortList.Add(frame[index]);
        checked { ++index; }
      }
    }
    else
    {
      if (this.m_FX3SPIConfig.DUTType != DUTType.ADcmXL1021)
        throw new FX3Exception("ERROR: Validating DUT CRC only supported for ADcmXL1021 and ADcmXL3021");
      int num5 = checked (((IEnumerable<ushort>) frame).Count<ushort>() - 4);
      int index = 9;
      while (index <= num5)
      {
        ushortList.Add(frame[index]);
        checked { ++index; }
      }
    }
    return (int) this.calcCCITT16(ushortList.ToArray()) == (int) num3;
  }

  /// <summary>
  /// Property to get or set the bit bang SPI configuration. Can select pins, timings, etc
  /// </summary>
  /// <returns></returns>
  public BitBangSpiConfig BitBangSpiConfig
  {
    get => this.m_BitBangSpi;
    set
    {
      this.m_BitBangSpi = value;
      if (!this.m_BitBangSpi.UpdatePinsRequired)
        return;
      this.m_BitBangSpi.SCLK = (FX3PinObject) this.FX3_GPIO1;
      this.m_BitBangSpi.CS = (FX3PinObject) this.FX3_GPIO2;
      this.m_BitBangSpi.MOSI = (FX3PinObject) this.FX3_GPIO3;
      this.m_BitBangSpi.MISO = (FX3PinObject) this.FX3_GPIO4;
    }
  }

  /// <summary>
  /// Perform a bit banged SPI transfer, using the config set in BitBangSpiConfig.
  /// </summary>
  /// <param name="BitsPerTransfer">The total number of bits to clock in a single transfer. Can be any number greater than 0.</param>
  /// <param name="NumTransfers">The number of separate SPI transfers to clock out</param>
  /// <param name="MOSIData">The MOSI data to clock out. Each SPI transfer must be byte aligned. Data is clocked out MSB first</param>
  /// <param name="TimeoutInMs">The time to wait on the bulk endpoint for a return transfer (in ms)</param>
  /// <returns>The data received over the selected MISO line</returns>
  public byte[] BitBangSpi(
    uint BitsPerTransfer,
    uint NumTransfers,
    byte[] MOSIData,
    uint TimeoutInMs)
  {
    List<byte> byteList1 = new List<byte>();
    Stopwatch stopwatch = new Stopwatch();
    List<byte> collection = new List<byte>();
    List<byte> byteList2 = new List<byte>();
    if (BitsPerTransfer == 0U)
      throw new FX3ConfigurationException("ERROR: Bits per transfer must be non-zero in a bit banged SPI transfer");
    if (checked (BitsPerTransfer * NumTransfers) > 4096U /*0x1000*/)
      throw new FX3ConfigurationException("ERROR: Too many bits in a single bit banged SPI transaction. Max value allowed 4080");
    if ((long) checked (BitsPerTransfer * NumTransfers) > (long) checked (((IEnumerable<byte>) MOSIData).Count<byte>() * 8))
      throw new FX3ConfigurationException("ERROR: MOSI data size must meet or exceed total transfer size");
    if (NumTransfers == 0U)
      return byteList1.ToArray();
    int index1 = 0;
    int num1 = 7;
    uint num2 = checked (NumTransfers - 1U);
    uint num3 = 0;
    while (num3 <= num2)
    {
      uint num4 = checked (BitsPerTransfer - 1U);
      uint num5 = 0;
      while (num5 <= num4)
      {
        collection.Add(checked ((byte) ((int) unchecked ((byte) ((uint) MOSIData[index1] >> (num1 & 7))) & 1)));
        checked { --num1; }
        if (num1 < 0)
        {
          num1 = 7;
          checked { ++index1; }
        }
        checked { ++num5; }
      }
      checked { ++num3; }
    }
    byteList1.AddRange((IEnumerable<byte>) this.m_BitBangSpi.GetParameterArray());
    byteList1.Add(checked ((byte) (unchecked ((int) BitsPerTransfer) & (int) byte.MaxValue)));
    byteList1.Add(checked ((byte) ((BitsPerTransfer & 65280U) >> 8)));
    byteList1.Add(checked ((byte) ((BitsPerTransfer & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)));
    byteList1.Add(checked ((byte) ((BitsPerTransfer & 4278190080U /*0xFF000000*/) >> 24)));
    byteList1.Add(checked ((byte) ((long) NumTransfers & (long) byte.MaxValue)));
    byteList1.Add(checked ((byte) ((NumTransfers & 65280U) >> 8)));
    byteList1.Add(checked ((byte) ((NumTransfers & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)));
    byteList1.Add(checked ((byte) ((NumTransfers & 4278190080U /*0xFF000000*/) >> 24)));
    byteList1.AddRange((IEnumerable<byte>) collection);
    this.ConfigureControlEndpoint(USBCommands.ADI_BITBANG_SPI, true);
    byte[] array = byteList1.ToArray();
    if (!this.XferControlData(ref array, byteList1.Count, 2000))
      throw new FX3CommunicationException("ERROR: Control Endpoint transfer failed for SPI bit bang setup");
    bool flag = false;
    stopwatch.Start();
    byte[] source = new byte[checked ((int) (BitsPerTransfer * NumTransfers - 1U) + 1)];
    int index2;
    // ISSUE: variable of a reference type
    byte[]& local1;
    // ISSUE: variable of a reference type
    int& local2;
    // ISSUE: variable of a reference type
    CyUSBEndPoint& local3;
    for (; !flag & stopwatch.ElapsedMilliseconds < (long) TimeoutInMs; flag = USB.XferData(ref local1, ref local2, ref local3))
    {
      local1 = ref source;
      index2 = ((IEnumerable<byte>) source).Count<byte>();
      local2 = ref index2;
      local3 = ref this.DataInEndPt;
    }
    stopwatch.Stop();
    if (!flag)
      Console.WriteLine("ERROR: Bit bang SPI transfer timed out");
    int num6 = checked (((IEnumerable<byte>) source).Count<byte>() - 1);
    int index3 = 0;
    while (index3 <= num6)
    {
      source[index3] = checked ((byte) ((int) unchecked ((byte) ((uint) source[index3] >> 1)) & 1));
      checked { ++index3; }
    }
    int num7 = 7;
    int num8 = 0;
    byteList2.Add((byte) 0);
    int num9 = checked (((IEnumerable<byte>) source).Count<byte>() - 1);
    int index4 = 0;
    while (index4 <= num9)
    {
      List<byte> byteList3;
      (byteList3 = byteList2)[index2 = num8] = checked ((byte) unchecked ((int) byteList3[index2] + (int) (byte) ((uint) source[index4] << (num7 & 7))));
      checked { --num7; }
      if (num7 < 0 & index4 != checked (((IEnumerable<byte>) source).Count<byte>() - 1))
      {
        byteList2.Add((byte) 0);
        num7 = 7;
        checked { ++num8; }
      }
      checked { ++index4; }
    }
    return byteList2.ToArray();
  }

  /// <summary>Read a standard iSensors 16-bit register using a bitbang SPI connection</summary>
  /// <param name="addr">The address of the register to read (7 bit) </param>
  /// <returns>The register value</returns>
  public ushort BitBangReadReg16(uint addr)
  {
    byte[] numArray = this.BitBangSpi(16U /*0x10*/, 2U, new List<byte>()
    {
      checked ((byte) (unchecked ((int) addr) & (int) sbyte.MaxValue)),
      (byte) 0,
      (byte) 0,
      (byte) 0
    }.ToArray(), checked ((uint) (1000 * this.m_StreamTimeout)));
    return checked ((ushort) unchecked ((int) (ushort) ((uint) (ushort) numArray[2] << 8) + (int) numArray[3]));
  }

  /// <summary>Write a byte to an iSensor register using a bitbang SPI connection</summary>
  /// <param name="addr">The address of the register to write to</param>
  /// <param name="data">The data to write to the register</param>
  public void BitBangWriteRegByte(byte addr, byte data)
  {
    List<byte> byteList = new List<byte>();
    checked { addr &= (byte) 127 /*0x7F*/; }
    checked { addr |= (byte) 128 /*0x80*/; }
    byteList.Add(addr);
    byteList.Add(data);
    this.BitBangSpi(16U /*0x10*/, 1U, byteList.ToArray(), checked ((uint) (1000 * this.m_StreamTimeout)));
  }

  /// <summary>
  /// Resets the hardware SPI pins to their default operating mode. Can be used to recover the SPI functionality after a bit-bang SPI transaction over the hardware SPI pins
  /// without having to reboot the FX3.
  /// </summary>
  public void RestoreHardwareSpi()
  {
    byte[] Buf = new byte[4];
    this.ConfigureControlEndpoint(USBCommands.ADI_RESET_SPI, false);
    uint num = this.XferControlData(ref Buf, 4, 2000) ? BitConverter.ToUInt32(Buf, 0) : throw new FX3CommunicationException("ERROR: Control Endpoint transfer failed for SPI hardware controller reset");
    if (num != 0)
      throw new FX3BadStatusException("ERROR: Invalid status received after SPI hardware controller reset: 0x" + num.ToString("X4"));
  }

  /// <summary>
  /// Set the SCLK frequency for a bit banged SPI connection. Overloaded to allow for a UInt
  /// </summary>
  /// <param name="Freq">The SPI frequency, in Hz</param>
  /// <returns>A boolean indicating if the frequency could be set.</returns>
  public bool SetBitBangSpiFreq(uint Freq) => this.SetBitBangSpiFreq(Convert.ToDouble(Freq));

  /// <summary>
  /// Set the bit bang SPI stall time. Driven by a clock with resolution of 49.3ns
  /// </summary>
  /// <param name="MicroSecondsStall">Stall time desired, in microseconds. Minimum of 0.7us</param>
  /// <returns>A boolean indicating if value is good or not. Defaults to closest possible value</returns>
  public bool SetBitBangStallTime(double MicroSecondsStall)
  {
    bool flag;
    try
    {
      this.m_BitBangSpi.StallTicks = checked ((uint) Math.Round(unchecked (MicroSecondsStall * 1000.0 / 49.61)));
    }
    catch (Exception ex)
    {
      ProjectData.SetProjectError(ex);
      flag = false;
      ProjectData.ClearProjectError();
      goto label_4;
    }
    flag = true;
label_4:
    return flag;
  }

  /// <summary>Sets the SCLK frequency for a bit bang SPI connection.</summary>
  /// <param name="Freq">The desired SPI frequency. Can go from 1.5MHz to approx 0.001Hz</param>
  /// <returns></returns>
  public bool SetBitBangSpiFreq(double Freq)
  {
    if (Freq > 1428571.4285714284)
    {
      this.m_BitBangSpi.SCLKHalfPeriodTicks = 0U;
      return false;
    }
    this.m_BitBangSpi.SCLKHalfPeriodTicks = checked ((uint) Math.Round(Math.Floor(unchecked (Math.Pow(10.0, 9.0) / Freq / 2.0 - 350.0 / 49.61))));
    return true;
  }

  /// <summary>
  /// Gets or sets the index of the first burst data word used in CRC calculations.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public int CrcFirstIndex
  {
    get => this.m_CrcFirstIndex;
    set => this.m_CrcFirstIndex = value;
  }

  /// <summary>
  /// Gets or sets the index of the last burst data word used in CRC calculations.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public int CrcLastIndex
  {
    get => this.m_CrcLastIndex;
    set => this.m_CrcLastIndex = value;
  }

  /// <summary>Gets or sets the index of the word that contains the CRC result.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public int CrcResultIndex
  {
    get => this.m_CrcResultIndex;
    set => this.m_CrcResultIndex = value;
  }

  /// <summary>
  /// Gets or sets the number of 16 bit words that are read during the burst. Does not include trigger, real transfer will be 2 bytes larger.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public int WordCount
  {
    get
    {
      return checked ((int) Math.Round(unchecked ((double) checked (this.m_BurstByteCount - 2) / 2.0)));
    }
    set
    {
      if (value < 1 | value > (int) ushort.MaxValue)
        throw new ArgumentException($"WordCount must be between 1 and {ushort.MaxValue.ToString()}.");
      this.m_BurstByteCount = checked (value + 1 * 2);
    }
  }

  /// <summary>
  /// Get or set the burst word length, in bytes. Is the total count of bytes transfered, including trigger
  /// </summary>
  /// <returns></returns>
  public int BurstByteCount
  {
    get => this.m_BurstByteCount;
    set => this.m_BurstByteCount = value;
  }

  /// <summary>Gets or sets register that is used to trigger burst operation.</summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public RegClass TriggerReg
  {
    get => this.m_TriggerReg;
    set
    {
      this.m_TriggerReg = value;
      this.m_burstMOSIData = new List<byte>()
      {
        checked ((byte) (unchecked ((int) this.TriggerReg.Address) & (int) byte.MaxValue)),
        checked ((byte) ((this.TriggerReg.Address & 65280U) >> 8))
      }.ToArray();
    }
  }

  /// <summary>
  /// Data to transmit on the MOSI line during a burst read operation. This value is over written
  /// if you set the trigger reg, since trigger reg is given priority.
  /// </summary>
  /// <returns></returns>
  public byte[] BurstMOSIData
  {
    get => this.m_burstMOSIData;
    set => this.m_burstMOSIData = value;
  }

  /// <summary>Takes interface out of burst mode by setting BurstMode to zero.</summary>
  /// <remarks></remarks>
  public void ClearBurstMode() => this.BurstMode = (ushort) 0;

  /// <summary>Puts interface into burst mode by setting burstMode to match word count.</summary>
  /// <remarks></remarks>
  /// <exception cref="T:System.InvalidOperationException">Thrown if word count has not been set.</exception>
  public void SetupBurstMode()
  {
    if (this.WordCount == 0)
      throw new InvalidOperationException("WordCount must be set before performing a burst read operation.");
    if (this.TriggerReg == null)
      throw new InvalidOperationException("Trigger register must be set before performing a burst read operation.");
    this.BurstMode = checked ((ushort) this.WordCount);
  }

  /// <summary>
  /// Overload of WaitForStreamCompletion which blocks indefinitely
  /// until a stream completion event is seen.
  /// </summary>
  public bool WaitForStreamCompletion() => this.WaitForStreamCompletion(0);

  /// <summary>
  /// Blocks until the streaming endpoint mutex can be acquired. Allows a user to synchronize external application
  /// the completion of a stream. Returns false if there is not a stream running, or if the timeout is reached without
  /// the stream mutex being acquired.
  /// </summary>
  /// <param name="MillisecondsTimeout">The time to wait trying to acquire the stream mutex (ms) </param>
  /// <returns>If the stream wait was successful</returns>
  public bool WaitForStreamCompletion(int MillisecondsTimeout)
  {
    Stopwatch stopwatch = new Stopwatch();
    if (!this.m_StreamThreadRunning)
    {
      stopwatch.Start();
      while (stopwatch.ElapsedMilliseconds < (long) MillisecondsTimeout & !this.m_StreamThreadRunning)
        Thread.Sleep(1);
      stopwatch.Stop();
      if (!this.m_StreamThreadRunning)
        return false;
    }
    checked { MillisecondsTimeout -= (int) stopwatch.ElapsedMilliseconds; }
    bool flag;
    if (MillisecondsTimeout <= 0)
    {
      flag = true;
      this.m_StreamMutex.WaitOne();
    }
    else
      flag = this.m_StreamMutex.WaitOne(MillisecondsTimeout);
    if (flag)
      this.m_StreamMutex.ReleaseMutex();
    return flag;
  }

  /// <summary>Place data in the thread safe queue and raise a buffer available event</summary>
  /// <param name="buf"></param>
  private void EnqueueStreamData(ref ushort[] buf)
  {
    this.m_StreamData.Enqueue(buf);
    // ISSUE: reference to a compiler-generated field
    IStreamEventProducer.NewBufferAvailableEventHandler bufferAvailableEvent = this.NewBufferAvailableEvent;
    if (bufferAvailableEvent == null)
      return;
    bufferAvailableEvent(this.m_StreamData.Count);
  }

  /// <summary>Cancel a any running stream</summary>
  public void CancelStreamAsync() => this.StopStream();

  /// <summary>Function to exit any stream thread and clean up stream state variables</summary>
  private void ExitStreamThread()
  {
    this.m_StreamThreadRunning = false;
    this.m_StreamType = StreamType.None;
    this.m_StreamMutex.ReleaseMutex();
    // ISSUE: reference to a compiler-generated field
    IStreamEventProducer.StreamFinishedEventHandler streamFinishedEvent = this.StreamFinishedEvent;
    if (streamFinishedEvent == null)
      return;
    streamFinishedEvent();
  }

  /// <summary>Cancel running stream</summary>
  /// <param name="ReqCode">ReqCode for the type of stream to cancel</param>
  private void CancelStreamImplementation(USBCommands ReqCode)
  {
    byte[] Buf = new byte[4];
    this.m_StreamThreadRunning = false;
    this.ConfigureControlEndpoint(ReqCode, false);
    this.m_ActiveFX3.ControlEndPt.Value = (ushort) 0;
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 2;
    uint num = this.XferControlData(ref Buf, 4, 2000) ? BitConverter.ToUInt32(Buf, 0) : throw new FX3CommunicationException("ERROR: Timeout occurred while canceling a stream. Cancel USB ReqCode: 0x" + (checked ((ushort) (uint) ReqCode)).ToString("X4"));
    if (num != 0 & num != 66U)
      throw new FX3BadStatusException("ERROR: Bad status code after stream cancel operation. Status: 0x" + num.ToString("X4"));
  }

  /// <summary>Function to start a burst read using the BurstStreamManager</summary>
  /// <param name="numBuffers">The number of buffers to read in the stream operation</param>
  public void StartBurstStream(uint numBuffers, IEnumerable<byte> burstTrigger)
  {
    byte[] Buf = new byte[checked (burstTrigger.Count<byte>() + 7 + 1)];
    Buf[0] = checked ((byte) (unchecked ((int) numBuffers) & (int) byte.MaxValue));
    Buf[1] = checked ((byte) ((numBuffers & 65280U) >> 8));
    Buf[2] = checked ((byte) ((numBuffers & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[3] = checked ((byte) ((numBuffers & 4278190080U /*0xFF000000*/) >> 24));
    Buf[4] = checked ((byte) ((long) this.BurstByteCount & (long) byte.MaxValue));
    Buf[5] = checked ((byte) (((long) this.BurstByteCount & 65280L) >> 8));
    Buf[6] = checked ((byte) (((long) this.BurstByteCount & 16711680L /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[7] = checked ((byte) (((long) this.BurstByteCount & 4278190080L /*0xFF000000*/) >> 24));
    int num = checked (burstTrigger.Count<byte>() - 1);
    int index = 0;
    while (index <= num)
    {
      Buf[checked (index + 8)] = burstTrigger.ElementAtOrDefault<byte>(index);
      checked { ++index; }
    }
    this.ConfigureControlEndpoint(USBCommands.ADI_STREAM_BURST_DATA, true);
    this.m_ActiveFX3.ControlEndPt.Value = (ushort) 0;
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 1;
    if (!this.XferControlData(ref Buf, Buf.Length, 2000))
      throw new FX3CommunicationException("ERROR: Timeout occurred while starting burst stream");
    this.m_TotalBuffersToRead = numBuffers;
    this.m_FramesRead = 0L;
    this.m_StreamData = new ConcurrentQueue<ushort[]>();
    this.m_StreamThread = new Thread(new ThreadStart(this.BurstStreamManager));
    this.m_StreamThread.Start();
  }

  /// <summary>
  /// Cleanup function for when a burst stream is done. This function frees resources on the FX3 firmware.
  /// </summary>
  private void BurstStreamDone()
  {
    byte[] Buf = new byte[4];
    this.ConfigureControlEndpoint(USBCommands.ADI_STREAM_BURST_DATA, true);
    this.m_ActiveFX3.ControlEndPt.Value = (ushort) 0;
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 0;
    if (!this.XferControlData(ref Buf, 4, 2000))
      throw new FX3CommunicationException("ERROR: Timeout occurred when cleaning up a burst stream thread on the FX3");
  }

  /// <summary>
  /// Property to choose if the readback from the 16 bit trigger word at the start of each burst is discarded or not
  /// </summary>
  /// <returns></returns>
  public bool StripBurstTriggerWord
  {
    get => this.m_StripBurstTriggerWord;
    set => this.m_StripBurstTriggerWord = value;
  }

  /// <summary>
  /// This function reads burst stream data from the DUT over the streaming endpoint. It is intended to operate in its own thread, and should not be called directly.
  /// </summary>
  private void BurstStreamManager()
  {
    int num1 = 0;
    List<ushort> ushortList = new List<ushort>();
    int len;
    if (this.m_ActiveFX3.bSuperSpeed)
    {
      len = 1024 /*0x0400*/;
    }
    else
    {
      if (!this.m_ActiveFX3.bHighSpeed)
        throw new FX3Exception("ERROR: Streaming application requires USB 2.0 or 3.0 connection to function");
      len = 512 /*0x0200*/;
    }
    byte[] buf = new byte[checked (len - 1 + 1)];
    if (this.m_TotalBuffersToRead < 1U)
      this.m_TotalBuffersToRead = uint.MaxValue;
    int burstByteCount = this.BurstByteCount;
    this.m_StreamThreadRunning = false;
    this.m_StreamMutex.WaitOne();
    this.m_StreamType = StreamType.BurstStream;
    this.m_StreamThreadRunning = true;
    int num2 = 0;
    while (this.m_StreamThreadRunning)
    {
      if (USB.XferData(ref buf, ref len, ref this.StreamingEndPt))
      {
        int num3 = checked (len - 2);
        int index = 0;
        while (index <= num3)
        {
          ushort num4 = checked ((ushort) unchecked ((int) (ushort) ((uint) (ushort) buf[index] << 8) + (int) buf[checked (index + 1)]));
          ushortList.Add(num4);
          checked { num1 += 2; }
          if (num1 >= burstByteCount)
          {
            if (this.m_StripBurstTriggerWord)
              ushortList.RemoveAt(0);
            ushort[] array = ushortList.ToArray();
            this.EnqueueStreamData(ref array);
            Interlocked.Increment(ref this.m_FramesRead);
            checked { ++num2; }
            num1 = 0;
            ushortList.Clear();
            if ((long) num2 >= (long) this.m_TotalBuffersToRead)
            {
              this.BurstStreamDone();
              goto label_20;
            }
          }
          checked { index += 2; }
        }
      }
      else
      {
        if (this.m_StreamThreadRunning)
        {
          string[] strArray = new string[5]
          {
            "Transfer failed during burst stream. Error code: ",
            null,
            null,
            null,
            null
          };
          uint lastError = this.StreamingEndPt.LastError;
          strArray[1] = lastError.ToString();
          strArray[2] = " (0x";
          lastError = this.StreamingEndPt.LastError;
          strArray[3] = lastError.ToString("X4");
          strArray[4] = ")";
          Console.WriteLine(string.Concat(strArray));
          this.CancelStreamImplementation(USBCommands.ADI_STREAM_BURST_DATA);
          break;
        }
        break;
      }
    }
label_20:
    this.ExitStreamThread();
  }

  /// <summary>Validate burst stream SPI config</summary>
  private void ValidateBurstStreamConfig()
  {
    if (this.m_FX3SPIConfig.ChipSelectControl != SpiChipselectControl.SPI_SSN_CTRL_HW_END_OF_XFER)
      throw new FX3ConfigurationException("ERROR: Chip select hardware control must be enabled for real time streaming");
  }

  /// <summary>
  /// Cleanup function for when a generic stream is done. Frees resources on FX3 firmware
  /// </summary>
  private void GenericStreamDone()
  {
    byte[] Buf = new byte[4];
    this.ConfigureControlEndpoint(USBCommands.ADI_STREAM_GENERIC_DATA, true);
    this.m_ActiveFX3.ControlEndPt.Value = (ushort) 0;
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 0;
    if (!this.XferControlData(ref Buf, 4, 2000))
      throw new FX3CommunicationException("ERROR: Timeout occurred when cleaning up a generic stream thread on the FX3!");
  }

  /// <summary>
  /// Starts a generic data stream. This allows you to read/write a set of registers on the DUT, triggering off the data ready if needed.
  /// The data read is placed in the thread-safe queue and can be retrieved with a call to GetBuffer. Each "buffer" is the result of
  /// reading the addr list of registers numCaptures times. For example, if addr is set to [0, 2, 4] and numCaptures is set to 10, each
  /// buffer will contain the 30 register values. The total number of register reads performed is numCaptures * numBuffers
  /// </summary>
  /// <param name="addr">The list of registers to </param>
  /// <param name="numCaptures">The number of captures of the register list per data ready</param>
  /// <param name="numBuffers">The total number of capture sequences to perform</param>
  public void StartGenericStream(IEnumerable<AddrDataPair> addr, uint numCaptures, uint numBuffers)
  {
    uint parameter = checked ((uint) ((long) addr.Count<AddrDataPair>() * (long) numCaptures * 2L));
    if (checked (addr.Count<AddrDataPair>() * 2) > 1000)
      throw new FX3ConfigurationException($"ERROR: Generic stream capture size too large- {checked (addr.Count<AddrDataPair>() * 2).ToString()} bytes per register list exceeds maximum size of {1000.ToString()} bytes.");
    this.GenericStreamSetup(addr, numCaptures, numBuffers);
    this.m_TotalBuffersToRead = numBuffers;
    this.m_FramesRead = 0L;
    this.m_StreamData = new ConcurrentQueue<ushort[]>();
    this.m_StreamThread = new Thread(new ParameterizedThreadStart(this.GenericStreamManager));
    this.m_StreamThread.Start((object) parameter);
  }

  /// <summary>Set up for a generic register read stream</summary>
  /// <param name="addrData">AddrDataPairs to read/write</param>
  /// <param name="numCaptures">Num captures to perform per buffer</param>
  /// <param name="numBuffers">Number of buffers to read</param>
  private void GenericStreamSetup(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers)
  {
    List<byte> byteList1 = new List<byte>();
    if (Information.IsNothing((object) numBuffers) | numBuffers < 1U)
      throw new FX3ConfigurationException("ERROR: Invalid number of buffers for a generic register stream: " + numBuffers.ToString());
    if (Information.IsNothing((object) addrData) | addrData.Count<AddrDataPair>() == 0)
      throw new FX3ConfigurationException("ERROR: Invalid address list for generic stream");
    if (Information.IsNothing((object) numCaptures) | numCaptures < 1U)
      throw new FX3ConfigurationException("ERROR: Invalid number of captures for a generic register stream: " + numBuffers.ToString());
    byteList1.Add(checked ((byte) (unchecked ((int) numBuffers) & (int) byte.MaxValue)));
    byteList1.Add(checked ((byte) ((numBuffers & 65280U) >> 8)));
    byteList1.Add(checked ((byte) ((numBuffers & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)));
    byteList1.Add(checked ((byte) ((numBuffers & 4278190080U /*0xFF000000*/) >> 24)));
    byteList1.Add(checked ((byte) (unchecked ((int) numCaptures) & (int) byte.MaxValue)));
    byteList1.Add(checked ((byte) ((numCaptures & 65280U) >> 8)));
    byteList1.Add(checked ((byte) ((numCaptures & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)));
    byteList1.Add(checked ((byte) ((numCaptures & 4278190080U /*0xFF000000*/) >> 24)));
    try
    {
      foreach (AddrDataPair addrDataPair in addrData)
      {
        if (!addrDataPair.data.HasValue)
        {
          byteList1.Add((byte) 0);
          byteList1.Add(checked ((byte) (unchecked ((int) addrDataPair.addr) & (int) sbyte.MaxValue)));
        }
        else
        {
          List<byte> byteList2 = byteList1;
          uint? nullable = addrDataPair.data;
          nullable = nullable.HasValue ? new uint?(nullable.GetValueOrDefault() & (uint) byte.MaxValue) : new uint?();
          int num = (int) checked ((byte) nullable.Value);
          byteList2.Add((byte) num);
          byteList1.Add(checked ((byte) (unchecked ((int) addrDataPair.addr) | 128 /*0x80*/)));
        }
      }
    }
    finally
    {
      IEnumerator<AddrDataPair> enumerator;
      enumerator?.Dispose();
    }
    this.ConfigureControlEndpoint(USBCommands.ADI_STREAM_GENERIC_DATA, true);
    this.m_ActiveFX3.ControlEndPt.Value = (ushort) 0;
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 1;
    byte[] array = byteList1.ToArray();
    if (!this.XferControlData(ref array, byteList1.Count, 5000))
      throw new FX3CommunicationException("ERROR: Control Endpoint transfer timed out when starting generic stream");
  }

  /// <summary>
  /// This function pulls generic stream data from the FX3 over a bulk endpoint (DataIn). It is intended to run in its own thread,
  /// and should not be called by itself.
  /// </summary>
  /// <param name="BytesPerBuffer">Number of bytes per generic stream buffer</param>
  private void GenericStreamManager(object BytesPerBuffer)
  {
    int num1 = 0;
    List<ushort> ushortList = new List<ushort>();
    uint uinteger = Conversions.ToUInteger(BytesPerBuffer);
    int len;
    if (this.m_ActiveFX3.bSuperSpeed)
    {
      len = 1024 /*0x0400*/;
    }
    else
    {
      if (!this.m_ActiveFX3.bHighSpeed)
        throw new FX3Exception("ERROR: Streaming application requires USB 2.0 or 3.0 connection to function");
      len = 512 /*0x0200*/;
    }
    byte[] buf = new byte[checked (len - 1 + 1)];
    this.m_StreamThreadRunning = false;
    this.m_StreamMutex.WaitOne();
    this.m_StreamType = StreamType.GenericStream;
    this.m_StreamThreadRunning = true;
    while (this.m_StreamThreadRunning)
    {
      if (USB.XferData(ref buf, ref len, ref this.StreamingEndPt))
      {
        int num2 = checked (len - 2);
        int startIndex = 0;
        while (startIndex <= num2)
        {
          ushortList.Add(BitConverter.ToUInt16(buf, startIndex));
          if ((long) checked (ushortList.Count * 2) >= (long) uinteger)
          {
            ushort[] array = ushortList.ToArray();
            this.EnqueueStreamData(ref array);
            Interlocked.Increment(ref this.m_FramesRead);
            ushortList.Clear();
            checked { ++num1; }
            if ((long) num1 >= (long) this.m_TotalBuffersToRead)
            {
              this.GenericStreamDone();
              goto label_18;
            }
            if ((long) checked (len - startIndex) < (long) uinteger & (long) uinteger <= (long) len)
              break;
          }
          checked { startIndex += 2; }
        }
      }
      else
      {
        if (this.m_StreamThreadRunning)
        {
          Console.WriteLine($"Transfer failed during generic stream. Error code: {this.StreamingEndPt.LastError.ToString()} (0x{this.StreamingEndPt.LastError.ToString("X4")})");
          this.CancelStreamImplementation(USBCommands.ADI_STREAM_GENERIC_DATA);
          break;
        }
        break;
      }
    }
label_18:
    this.ExitStreamThread();
  }

  /// <summary>Validate the SPI configuration for a generic stream</summary>
  private void ValidateGenericStreamConfig()
  {
    if (this.m_FX3SPIConfig.WordLength != (byte) 16 /*0x10*/)
      throw new FX3ConfigurationException("ERROR: Generic stream only supported for a word length of 16 bits");
  }

  /// <summary>
  /// This function starts real time streaming on the ADcmXLx021 (interface and FX3). Specifying pin exit is optional and must be 0 (disabled) or 1 (enabled)
  /// </summary>
  public void StartRealTimeStreaming(uint numFrames)
  {
    byte[] Buf = new byte[5];
    this.ValidateRealTimeStreamConfig();
    Buf[0] = checked ((byte) (unchecked ((int) numFrames) & (int) byte.MaxValue));
    Buf[1] = checked ((byte) ((numFrames & 65280U) >> 8));
    Buf[2] = checked ((byte) ((numFrames & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[3] = checked ((byte) ((numFrames & 4278190080U /*0xFF000000*/) >> 24));
    Buf[4] = (byte) -(this.m_pinStart ? 1 : 0);
    this.m_StreamData = new ConcurrentQueue<ushort[]>();
    this.ConfigureControlEndpoint(USBCommands.ADI_STREAM_REALTIME, true);
    this.m_ActiveFX3.ControlEndPt.Value = (ushort) -(this.m_pinExit ? 1 : 0);
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 1;
    if (!this.XferControlData(ref Buf, 5, 2000))
      throw new FX3CommunicationException("ERROR: Timeout occurred while starting an ADcmXL real time stream!");
    this.m_FramesRead = 0L;
    this.m_numBadFrames = 0L;
    this.m_TotalBuffersToRead = numFrames;
    this.m_StreamThread = new Thread(new ThreadStart(this.RealTimeStreamManager));
    this.m_StreamThread.Start();
  }

  /// <summary>
  /// This function stops real time streaming on the ADcmXLx021 (stream thread in FX3 API and FX3 firmware)
  /// </summary>
  public void StopRealTimeStreaming()
  {
    byte[] Buf = new byte[4];
    this.m_StreamThreadRunning = false;
    this.ConfigureControlEndpoint(USBCommands.ADI_STREAM_REALTIME, false);
    this.m_ActiveFX3.ControlEndPt.Value = (ushort) -(this.m_pinExit ? 1 : 0);
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 2;
    if (!this.XferControlData(ref Buf, 4, 5000))
      throw new FX3CommunicationException("ERROR: Timeout occurred while stopping a real time stream");
  }

  /// <summary>
  /// Clean up function when real time streaming is done. Frees required resources on FX3
  /// </summary>
  private void RealTimeStreamingDone()
  {
    byte[] Buf = new byte[4];
    this.ConfigureControlEndpoint(USBCommands.ADI_STREAM_REALTIME, true);
    this.m_ActiveFX3.ControlEndPt.Value = (ushort) -(this.m_pinExit ? 1 : 0);
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 0;
    if (!this.XferControlData(ref Buf, 4, 2000))
      throw new FX3CommunicationException("ERROR: Timeout occurred when cleaning up a real time stream thread on the FX3");
  }

  /// <summary>
  /// This function pulls real time data from the DUT over the streaming endpoint. It is intended to operate in its own thread, and should not be called directly
  /// </summary>
  private void RealTimeStreamManager()
  {
    int num1 = 0;
    List<ushort> ushortList = new List<ushort>();
    int len;
    if (this.m_ActiveFX3.bSuperSpeed)
    {
      len = 1024 /*0x0400*/;
    }
    else
    {
      if (!this.m_ActiveFX3.bHighSpeed)
        throw new FX3Exception("ERROR: Streaming application requires USB 2.0 or 3.0 connection to function");
      len = 512 /*0x0200*/;
    }
    byte[] buf = new byte[checked (len - 1 + 1)];
    if (this.m_TotalBuffersToRead < 1U)
      this.m_TotalBuffersToRead = (uint) int.MaxValue;
    int num2 = this.m_FX3SPIConfig.DUTType != DUTType.ADcmXL1021 ? (this.m_FX3SPIConfig.DUTType != DUTType.ADcmXL2021 ? 200 : 152) : 88;
    this.m_StreamThreadRunning = false;
    this.m_StreamMutex.WaitOne();
    this.m_StreamType = StreamType.RealTimeStream;
    this.m_StreamThreadRunning = true;
    int num3 = 0;
    while (this.m_StreamThreadRunning)
    {
      if (USB.XferData(ref buf, ref len, ref this.StreamingEndPt))
      {
        int num4 = checked (len - 2);
        int index = 0;
        while (index <= num4)
        {
          ushort num5 = checked ((ushort) unchecked ((int) (ushort) ((uint) (ushort) buf[index] << 8) + (int) buf[checked (index + 1)]));
          ushortList.Add(num5);
          checked { num1 += 2; }
          if (num1 >= num2)
          {
            ushort[] array = ushortList.ToArray();
            this.EnqueueStreamData(ref array);
            Interlocked.Increment(ref this.m_FramesRead);
            checked { ++num3; }
            num1 = 0;
            ushortList.Clear();
            if ((long) num3 >= (long) this.m_TotalBuffersToRead)
            {
              this.RealTimeStreamingDone();
              goto label_18;
            }
          }
          checked { index += 2; }
        }
      }
      else
      {
        if (this.m_StreamThreadRunning)
        {
          string[] strArray = new string[5]
          {
            "Transfer failed during AdCMXL real time stream. Error code: ",
            null,
            null,
            null,
            null
          };
          uint lastError = this.StreamingEndPt.LastError;
          strArray[1] = lastError.ToString();
          strArray[2] = " (0x";
          lastError = this.StreamingEndPt.LastError;
          strArray[3] = lastError.ToString("X4");
          strArray[4] = ")";
          Console.WriteLine(string.Concat(strArray));
          this.StopRealTimeStreaming();
          break;
        }
        break;
      }
    }
label_18:
    this.ExitStreamThread();
  }

  /// <summary>
  /// This function checks the CRC of each real time streaming frame stored in the Stream Data Queue,
  /// and purges the bad ones. This operation is only valid for an ADcmXL series DUT.
  /// </summary>
  /// <returns>The success of the data purge operation</returns>
  public bool PurgeBadFrameData()
  {
    bool flag1 = true;
    ConcurrentQueue<ushort[]> concurrentQueue = new ConcurrentQueue<ushort[]>();
    ushort[] result = (ushort[]) null;
    if (!(this.PartType == DUTType.ADcmXL1021 | this.PartType == DUTType.ADcmXL2021 | this.PartType == DUTType.ADcmXL3021) || this.m_StreamThreadRunning)
      return false;
    this.m_numBadFrames = 0L;
    this.m_numFrameSkips = 0L;
    ushort num1 = 0;
    bool flag2 = true;
    while (this.m_StreamData.Count != 0)
    {
      bool flag3 = false;
      while (!flag3 & this.m_StreamData.Count > 0)
        flag3 = this.m_StreamData.TryDequeue(out result);
      if (this.CheckDUTCRC(ref result))
        concurrentQueue.Enqueue(result);
      else
        checked { ++this.m_numBadFrames; }
      ushort num2 = checked ((ushort) unchecked (checked ((uint) num1 + 1U) % 256U /*0x0100*/));
      num1 = this.PartType != DUTType.ADcmXL1021 ? checked ((ushort) (((uint) result[0] & 65280U) >> 8)) : checked ((ushort) (((uint) result[8] & 65280U) >> 8));
      if ((int) num1 != (int) num2 & !flag2)
      {
        // ISSUE: variable of a reference type
        long& local;
        // ISSUE: explicit reference operation
        long num3 = checked (^(local = ref this.m_numFrameSkips) + 1L);
        local = num3;
      }
      flag2 = false;
    }
    this.m_StreamData = concurrentQueue;
    this.m_FramesRead = (long) this.m_StreamData.Count;
    return flag1;
  }

  /// <summary>
  /// This function validates the current SPI settings to ensure that they are compatible with the machine health
  /// real time streaming mode. If the settings are not compatible, a FX3ConfigException is thrown.
  /// </summary>
  private void ValidateRealTimeStreamConfig()
  {
    if (this.m_FX3SPIConfig.ChipSelectControl != SpiChipselectControl.SPI_SSN_CTRL_HW_END_OF_XFER)
      throw new FX3ConfigurationException("ERROR: Chip select hardware control must be enabled for real time streaming");
    if (!this.m_FX3SPIConfig.Cpha | !this.m_FX3SPIConfig.Cpol)
      throw new FX3ConfigurationException("ERROR: Cpol and Cpha must both be set to true for real time streaming");
    if (this.m_FX3SPIConfig.ChipSelectPolarity)
      throw new FX3ConfigurationException("ERROR: Chip select polarity must be false (active low) for real time streaming");
  }

  /// <summary>Read data from the FX3 non-volatile memory</summary>
  /// <param name="ByteAddress">The flash byte address to read from (valid range 0x0 - 0x40000)</param>
  /// <param name="ReadLength">The number of bytes to read. Max 4096</param>
  /// <returns>The data read from the FX3 flash memory</returns>
  public byte[] ReadFlash(uint ByteAddress, ushort ReadLength)
  {
    byte[] Buf = new byte[checked ((int) ReadLength - 1 + 1)];
    if (ByteAddress > 262144U /*0x040000*/)
      throw new FX3ConfigurationException($"ERROR: Invalid flash read address 0x{ByteAddress.ToString("X4")}. Max allowed address 0x40000");
    if (ReadLength > (ushort) 4096 /*0x1000*/)
      throw new FX3ConfigurationException($"ERROR: Invalid flash read length of {ReadLength.ToString()}bytes. Max allowed 4096 bytes per transfer");
    this.ConfigureControlEndpoint(USBCommands.ADI_READ_FLASH, false);
    this.FX3ControlEndPt.Value = checked ((ushort) (unchecked ((int) ByteAddress) & (int) ushort.MaxValue));
    this.FX3ControlEndPt.Index = checked ((ushort) (unchecked ((int) (ByteAddress >> 16 /*0x10*/)) & (int) ushort.MaxValue));
    return this.XferControlData(ref Buf, (int) ReadLength, 5000) ? Buf : throw new FX3CommunicationException("ERROR: Control endpoint transfer failed for flash read");
  }

  /// <summary>Clear the error log stored in flash</summary>
  public void ClearErrorLog()
  {
    byte[] Buf = new byte[4];
    this.ConfigureControlEndpoint(USBCommands.ADI_CLEAR_FLASH_LOG, true);
    if (!this.XferControlData(ref Buf, 4, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer failed for flash log clear!");
  }

  /// <summary>Get the number of errors logged to the FX3 flash memory</summary>
  /// <returns>The error log count in flash</returns>
  public uint GetErrorLogCount()
  {
    uint errorLogCount = BitConverter.ToUInt32(this.ReadFlash(212992U /*0x034000*/, (ushort) 4), 0);
    if (errorLogCount == uint.MaxValue)
      errorLogCount = 0U;
    return errorLogCount;
  }

  /// <summary>Gets the current error log from FX3 flash memory</summary>
  /// <returns>The stored error log, as a list of FX3ErrorLog objects</returns>
  public List<FX3ErrorLog> GetErrorLog()
  {
    List<FX3ErrorLog> errorLog = new List<FX3ErrorLog>();
    List<byte> byteList = new List<byte>();
    uint num1 = BitConverter.ToUInt32(this.ReadFlash(212992U /*0x034000*/, (ushort) 4), 0);
    if (num1 == 0U)
      return errorLog;
    if (num1 > 1500U)
      num1 = 1500U;
    int val2 = checked ((int) (32L /*0x20*/ * (long) num1));
    uint ByteAddress = 213056;
    while (val2 > 0)
    {
      ushort ReadLength = checked ((ushort) Math.Min(4096 /*0x1000*/, val2));
      byteList.AddRange((IEnumerable<byte>) this.ReadFlash(ByteAddress, ReadLength));
      checked { ByteAddress += (uint) ReadLength; }
      checked { val2 -= (int) ReadLength; }
    }
    uint index = 0;
    uint num2 = num1;
    uint num3 = 1;
    while (num3 <= num2)
    {
      errorLog.Add(new FX3ErrorLog(byteList.GetRange(checked ((int) index), 32 /*0x20*/).ToArray()));
      checked { index += 32U /*0x20*/; }
      checked { ++num3; }
    }
    return errorLog;
  }

  /// <summary>Get/Set the FX3 I2C bit rate. Valid range 100KHz - 1MHz. Defaults to 100KHz</summary>
  /// <returns>Current I2C bit rate setting</returns>
  public uint I2CBitRate
  {
    get => this.m_i2cbitrate;
    set
    {
      if (value < 100000U)
        throw new FX3ConfigurationException("ERROR: Invalid I2C bit rate. Must be at least 100KHz");
      if (value > 1000000U)
        throw new FX3ConfigurationException("ERROR: Invalid I2C bit rate. Must be at 1MHz or less");
      this.SetI2CBitRate(value);
      this.m_i2cbitrate = value;
    }
  }

  /// <summary>
  /// Get/Set the FX3 I2C retry count. This is the number of times the FX3
  /// will retry a read/write when a NAK is received from the slave device
  /// being addressed.
  /// </summary>
  /// <returns></returns>
  public ushort I2CRetryCount
  {
    get => this.m_i2cRetryCount;
    set
    {
      this.SetI2CRetryCount(value);
      this.m_i2cRetryCount = value;
    }
  }

  /// <summary>Read bytes from an I2C slave device attached to the FX3.</summary>
  /// <param name="Preamble">The I2C preamble to transmit at the start of the read operation</param>
  /// <param name="NumBytes">The number of bytes to read over I2C after sending the preamble</param>
  /// <param name="TimeoutInMs">Read timeout period, in ms</param>
  /// <returns>The data read from the I2C slave device</returns>
  public byte[] I2CReadBytes(I2CPreamble Preamble, uint NumBytes, uint TimeoutInMs)
  {
    List<byte> byteList = new List<byte>();
    byte[] buf = new byte[512 /*0x0200*/];
    Stopwatch stopwatch = new Stopwatch();
    byteList.Add(checked ((byte) (unchecked ((int) NumBytes) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (NumBytes >> 8)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (NumBytes >> 16 /*0x10*/)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (NumBytes >> 24)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) TimeoutInMs) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (TimeoutInMs >> 8)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (TimeoutInMs >> 16 /*0x10*/)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (TimeoutInMs >> 24)) & (int) byte.MaxValue)));
    byteList.AddRange(Preamble.Serialize());
    this.ConfigureControlEndpoint(USBCommands.ADI_I2C_READ_BYTES, true);
    byte[] array = byteList.ToArray();
    if (!this.XferControlData(ref array, byteList.Count, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer timed out while starting I2C read command");
    bool flag = false;
    byteList.Clear();
    stopwatch.Start();
    while (!flag & stopwatch.ElapsedMilliseconds < (long) TimeoutInMs)
    {
      if ((long) byteList.Count >= (long) NumBytes)
      {
        flag = true;
      }
      else
      {
        int len = checked ((int) Math.Min(512L /*0x0200*/, (long) NumBytes - (long) byteList.Count));
        if (USB.XferData(ref buf, ref len, ref this.DataInEndPt))
        {
          int num = checked (len - 1);
          int index = 0;
          while (index <= num)
          {
            byteList.Add(buf[index]);
            checked { ++index; }
          }
        }
      }
    }
    if (!flag)
      throw new FX3CommunicationException($"ERROR: I2C read timed out. Timeout period {TimeoutInMs.ToString()}ms");
    return byteList.ToArray();
  }

  /// <summary>Write bytes to an I2C slave device attached to the FX3</summary>
  /// <param name="Preamble">The I2C preamble to transmit at the start of the write operation</param>
  /// <param name="WriteData">The write data to transmit on SDA after finishing the preamble</param>
  /// <param name="TimeoutInMs">Write timeout period, in ms</param>
  public void I2CWriteBytes(I2CPreamble Preamble, IEnumerable<byte> WriteData, uint TimeoutInMs)
  {
    List<byte> byteList = new List<byte>();
    byte[] numArray = new byte[4];
    Stopwatch stopwatch = new Stopwatch();
    if (WriteData.Count<byte>() > 4077)
      throw new FX3ConfigurationException("ERROR: Invalid write data length. Cannot total command transfer size cannot be more than 4096 bytes");
    byteList.Add(checked ((byte) ((long) WriteData.Count<byte>() & (long) byte.MaxValue)));
    byteList.Add(checked ((byte) ((long) (WriteData.Count<byte>() >> 8) & (long) byte.MaxValue)));
    byteList.Add(checked ((byte) ((long) (WriteData.Count<byte>() >> 16 /*0x10*/) & (long) byte.MaxValue)));
    byteList.Add(checked ((byte) ((long) (WriteData.Count<byte>() >> 24) & (long) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) TimeoutInMs) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (TimeoutInMs >> 8)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (TimeoutInMs >> 16 /*0x10*/)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (TimeoutInMs >> 24)) & (int) byte.MaxValue)));
    byteList.AddRange(Preamble.Serialize());
    byteList.AddRange(WriteData);
    this.ConfigureControlEndpoint(USBCommands.ADI_I2C_WRITE_BYTES, true);
    byte[] array = byteList.ToArray();
    if (!this.XferControlData(ref array, byteList.Count, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer timed out while starting I2C write command");
    bool flag = false;
    stopwatch.Start();
    // ISSUE: variable of a reference type
    byte[]& local1;
    // ISSUE: variable of a reference type
    int& local2;
    // ISSUE: variable of a reference type
    CyUSBEndPoint& local3;
    for (; !flag & stopwatch.ElapsedMilliseconds < (long) TimeoutInMs; flag = USB.XferData(ref local1, ref local2, ref local3))
    {
      local1 = ref numArray;
      int num = 4;
      local2 = ref num;
      local3 = ref this.DataInEndPt;
    }
    if (!flag)
      throw new FX3CommunicationException($"ERROR: I2C write timed out. Timeout period {TimeoutInMs.ToString()}ms");
    uint uint32 = BitConverter.ToUInt32(numArray, 0);
    if (uint32 != 0)
      throw new FX3BadStatusException("ERROR: Bad status code after I2C write. Error code: 0x" + uint32.ToString("X4"));
  }

  /// <summary>Helper function to set i2c bit rate on FX3</summary>
  /// <param name="BitRate">Bit rate setting</param>
  private void SetI2CBitRate(uint BitRate)
  {
    byte[] Buf = new byte[4];
    this.ConfigureControlEndpoint(USBCommands.ADI_I2C_SET_BIT_RATE, false);
    this.FX3ControlEndPt.Value = checked ((ushort) (unchecked ((int) BitRate) & (int) ushort.MaxValue));
    this.FX3ControlEndPt.Index = checked ((ushort) ((BitRate & 4294901760U) >> 16 /*0x10*/));
    uint num = this.XferControlData(ref Buf, 4, 2000) ? BitConverter.ToUInt32(Buf, 0) : throw new FX3CommunicationException("ERROR: Control endpoint transfer timed out while setting I2C bit rate");
    if (num != 0)
      throw new FX3BadStatusException("ERROR: Bad status code after setting I2C bit rate. Error code: 0x" + num.ToString("X4"));
  }

  /// <summary>Helper function to set i2c retry count in case of NAK on FX3</summary>
  /// <param name="Count">Number of times to transfer</param>
  private void SetI2CRetryCount(ushort Count)
  {
    byte[] Buf = new byte[4];
    this.ConfigureControlEndpoint(USBCommands.ADI_I2C_RETRY_COUNT, false);
    this.FX3ControlEndPt.Value = Count;
    uint num = this.XferControlData(ref Buf, 4, 2000) ? BitConverter.ToUInt32(Buf, 0) : throw new FX3CommunicationException("ERROR: Control endpoint transfer timed out while setting I2C retry count");
    if (num != 0)
      throw new FX3BadStatusException("ERROR: Bad status code after setting I2C retry count. Error code: 0x" + num.ToString("X4"));
  }

  /// <summary>
  /// Start an asynchronous I2C read stream. This stream runs on the stream thread
  /// and places all data in a thread safe queue. The data can be retrieved using
  /// GetI2CBuffer()
  /// </summary>
  /// <param name="Preamble">The preamble to send at the start of the read</param>
  /// <param name="BytesPerRead">Number of read bytes following the preamble</param>
  /// <param name="numBuffers">Total number of separate I2C transactions to send</param>
  public void StartI2CStream(I2CPreamble Preamble, uint BytesPerRead, uint numBuffers)
  {
    List<byte> byteList = new List<byte>();
    byte[] numArray = new byte[512 /*0x0200*/];
    uint num = checked ((uint) (1000 * this.m_StreamTimeout));
    byteList.Add(checked ((byte) (unchecked ((int) BytesPerRead) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (BytesPerRead >> 8)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (BytesPerRead >> 16 /*0x10*/)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (BytesPerRead >> 24)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) num) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (num >> 8)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (num >> 16 /*0x10*/)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (num >> 24)) & (int) byte.MaxValue)));
    byteList.AddRange(Preamble.Serialize());
    byteList.Add(checked ((byte) (unchecked ((int) numBuffers) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (numBuffers >> 8)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (numBuffers >> 16 /*0x10*/)) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (unchecked ((int) (numBuffers >> 24)) & (int) byte.MaxValue)));
    this.ConfigureControlEndpoint(USBCommands.ADI_I2C_READ_STREAM, true);
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 1;
    byte[] array = byteList.ToArray();
    if (!this.XferControlData(ref array, byteList.Count, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer timed out while starting I2C stream");
    this.m_TotalBuffersToRead = numBuffers;
    this.m_FramesRead = 0L;
    this.m_TotalBuffersToRead = numBuffers;
    this.m_I2CStreamData = new ConcurrentQueue<byte[]>();
    this.m_StreamType = StreamType.I2CReadStream;
    this.m_StreamThread = new Thread(new ParameterizedThreadStart(this.I2CStreamManager));
    this.m_StreamThread.Start((object) BytesPerRead);
  }

  /// <summary>Stream thread function for I2C stream</summary>
  /// <param name="BytesPerBuffer">Number of bytes to read</param>
  private void I2CStreamManager(object BytesPerBuffer)
  {
    int integer = Conversions.ToInteger(BytesPerBuffer);
    byte[] buf = new byte[checked (integer - 1 + 1)];
    if (this.m_TotalBuffersToRead < 1U)
      this.m_TotalBuffersToRead = uint.MaxValue;
    this.m_StreamThreadRunning = false;
    this.m_StreamMutex.WaitOne();
    this.m_StreamThreadRunning = true;
    uint num = 0;
    while (this.m_StreamThreadRunning)
    {
      if (USB.XferData(ref buf, ref integer, ref this.StreamingEndPt))
      {
        this.m_I2CStreamData.Enqueue(((IEnumerable<byte>) buf).ToArray<byte>());
        // ISSUE: reference to a compiler-generated field
        IStreamEventProducer.NewBufferAvailableEventHandler bufferAvailableEvent = this.NewBufferAvailableEvent;
        if (bufferAvailableEvent != null)
          bufferAvailableEvent(this.m_I2CStreamData.Count);
        checked { ++num; }
        Interlocked.Increment(ref this.m_FramesRead);
        if (num >= this.m_TotalBuffersToRead)
        {
          this.I2CStreamDone();
          break;
        }
      }
      else
      {
        if (this.m_StreamThreadRunning)
        {
          Console.WriteLine($"Transfer failed during I2C stream. Error code: {this.StreamingEndPt.LastError.ToString()} (0x{this.StreamingEndPt.LastError.ToString("X4")})");
          this.CancelStreamImplementation(USBCommands.ADI_I2C_READ_STREAM);
          break;
        }
        break;
      }
    }
    this.ExitStreamThread();
  }

  /// <summary>Cleanup function when I2C stream is done</summary>
  private void I2CStreamDone()
  {
    byte[] Buf = new byte[4];
    this.ConfigureControlEndpoint(USBCommands.ADI_I2C_READ_STREAM, true);
    this.m_ActiveFX3.ControlEndPt.Value = (ushort) 0;
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 0;
    if (!this.XferControlData(ref Buf, 4, 2000))
      throw new FX3CommunicationException("ERROR: Timeout occurred when cleaning up I2C stream thread on the FX3!");
  }

  /// <summary>Get I2C buffer from I2C read stream</summary>
  /// <returns>I2C data read</returns>
  public byte[] GetI2CBuffer()
  {
    byte[] result = (byte[]) null;
    if (Information.IsNothing((object) this.m_I2CStreamData) || this.m_I2CStreamData.Count == 0)
      return (byte[]) null;
    this.m_I2CStreamData.TryDequeue(out result);
    return result;
  }

  /// <summary>
  /// This function drives a pin to the specified level for a given time interval in ms
  /// </summary>
  /// <param name="pin">The FX3PinObject for the pin to drive</param>
  /// <param name="polarity">The level to drive the pin to. 1 - high, 0 - low</param>
  /// <param name="pperiod">The time to drive the pin for, in ms. Minimum of 3us.</param>
  /// <param name="mode">Not implemented</param>
  public void PulseDrive(IPinObject pin, uint polarity, double pperiod, uint mode)
  {
    this.ConfigureControlEndpoint(USBCommands.ADI_PULSE_DRIVE, true);
    byte[] Buf = new byte[11];
    if (this.isPWMPin(pin))
      throw new FX3ConfigurationException("ERROR: The selected pin is currently configured to drive a PWM signal. Please call StopPWM(pin) before interfacing with the pin further");
    if (pperiod < 0.003)
      throw new FX3ConfigurationException($"ERROR: Invalid Pulse Drive period {pperiod.ToString()}ms. Minimum possible drive time is 3 microseconds");
    pperiod -= 0.0028;
    double num1 = pperiod / 1000.0 * (double) this.m_FX3SPIConfig.SecondsToTimerTicks;
    uint num2 = checked ((uint) Math.Round(Math.Floor(unchecked (num1 / (double) uint.MaxValue))));
    uint num3 = checked ((uint) Math.Round(unchecked (num1 % (double) uint.MaxValue)));
    if (polarity != 0)
      polarity = 1U;
    Buf[0] = checked ((byte) (unchecked ((int) pin.pinConfig) & (int) byte.MaxValue));
    Buf[1] = (byte) 0;
    Buf[2] = checked ((byte) (unchecked ((int) polarity) & 1));
    Buf[3] = checked ((byte) (unchecked ((int) num3) & (int) byte.MaxValue));
    Buf[4] = checked ((byte) ((num3 & 65280U) >> 8));
    Buf[5] = checked ((byte) ((num3 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[6] = checked ((byte) ((num3 & 4278190080U /*0xFF000000*/) >> 24));
    Buf[7] = checked ((byte) (unchecked ((int) num2) & (int) byte.MaxValue));
    Buf[8] = checked ((byte) ((num2 & 65280U) >> 8));
    Buf[9] = checked ((byte) ((num2 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[10] = checked ((byte) ((num2 & 4278190080U /*0xFF000000*/) >> 24));
    if (!this.XferControlData(ref Buf, 11, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer to start pulse drive timed out!");
    Thread.Sleep(checked ((int) Math.Round(pperiod)));
    Array.Clear((Array) Buf, 0, Buf.Length);
    ref byte[] local1 = ref Buf;
    int num4 = 4;
    ref int local2 = ref num4;
    ref CyUSBEndPoint local3 = ref this.DataInEndPt;
    if (!USB.XferData(ref local1, ref local2, ref local3))
      throw new FX3CommunicationException("ERROR: Transfer from FX3 after pulse drive failed!");
    uint uint32 = BitConverter.ToUInt32(Buf, 0);
    if (uint32 != 0)
      throw new FX3BadStatusException("ERROR: Pin Drive Failed, Status: " + uint32.ToString("X4"));
  }

  /// <summary>This function waits for a pin to reach a specified level</summary>
  /// <param name="pin">The pin to poll</param>
  /// <param name="polarity">The level to wait for. 1 - high, 0 - low</param>
  /// <param name="delayInMs">The delay from the start of the function call to when the pin polling starts</param>
  /// <param name="timeoutInMs">The timeout from when the pin polling starts to when the function returns, if the desired level is never reached</param>
  /// <returns>The total time waited (including delay) in ms</returns>
  public double PulseWait(IPinObject pin, uint polarity, uint delayInMs, uint timeoutInMs)
  {
    byte[] Buf = new byte[16 /*0x10*/];
    Stopwatch stopwatch = new Stopwatch();
    if (this.isPWMPin(pin))
      throw new FX3ConfigurationException("ERROR: The selected pin is currently configured to drive a PWM signal. Please call StopPWM(pin) before interfacing with the pin further");
    long num1 = (long) checked (delayInMs + timeoutInMs);
    Buf[0] = checked ((byte) (unchecked ((int) pin.pinConfig) & (int) byte.MaxValue));
    Buf[1] = (byte) 0;
    if (polarity != 0)
      polarity = 1U;
    Buf[2] = checked ((byte) polarity);
    bool flag1 = false;
    uint num2 = delayInMs;
    if ((double) delayInMs > (double) uint.MaxValue / ((double) this.m_FX3SPIConfig.SecondsToTimerTicks / 1000.0))
    {
      flag1 = true;
      Thread.Sleep(checked ((int) delayInMs));
      num2 = 0U;
      num1 = (long) timeoutInMs;
    }
    Buf[3] = checked ((byte) (unchecked ((int) num2) & (int) byte.MaxValue));
    Buf[4] = checked ((byte) ((num2 & 65280U) >> 8));
    Buf[5] = checked ((byte) ((num2 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[6] = checked ((byte) ((num2 & 4278190080U /*0xFF000000*/) >> 24));
    ulong num3 = checked ((ulong) Math.Round(unchecked ((double) timeoutInMs * (double) this.m_FX3SPIConfig.SecondsToTimerTicks / 1000.0)));
    uint num4 = checked ((uint) Math.Round(Math.Floor(unchecked ((double) num3 / (double) uint.MaxValue))));
    uint num5 = checked ((uint) unchecked (num3 % (ulong) uint.MaxValue));
    Buf[7] = checked ((byte) (unchecked ((int) num5) & (int) byte.MaxValue));
    Buf[8] = checked ((byte) ((num5 & 65280U) >> 8));
    Buf[9] = checked ((byte) ((num5 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[10] = checked ((byte) ((num5 & 4278190080U /*0xFF000000*/) >> 24));
    Buf[11] = checked ((byte) (unchecked ((int) num4) & (int) byte.MaxValue));
    Buf[12] = checked ((byte) ((num4 & 65280U) >> 8));
    Buf[13] = checked ((byte) ((num4 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[14] = checked ((byte) ((num4 & 4278190080U /*0xFF000000*/) >> 24));
    stopwatch.Start();
    this.ConfigureControlEndpoint(USBCommands.ADI_PULSE_WAIT, true);
    if (!this.XferControlData(ref Buf, 15, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer for pulse wait timed out!");
    Array.Clear((Array) Buf, 0, Buf.Length);
    bool flag2 = false;
    // ISSUE: variable of a reference type
    byte[]& local1;
    // ISSUE: variable of a reference type
    int& local2;
    // ISSUE: variable of a reference type
    CyUSBEndPoint& local3;
    if (num1 == 0L)
    {
      ref byte[] local4 = ref Buf;
      int num6 = 12;
      ref int local5 = ref num6;
      ref CyUSBEndPoint local6 = ref this.DataInEndPt;
      flag2 = USB.XferData(ref local4, ref local5, ref local6);
    }
    else
    {
      for (; !flag2 & stopwatch.ElapsedMilliseconds < num1; flag2 = USB.XferData(ref local1, ref local2, ref local3))
      {
        local1 = ref Buf;
        int num7 = 12;
        local2 = ref num7;
        local3 = ref this.DataInEndPt;
      }
    }
    stopwatch.Stop();
    if (!flag2)
      return flag1 ? Convert.ToDouble(checked (stopwatch.ElapsedMilliseconds + (long) delayInMs)) : Convert.ToDouble(stopwatch.ElapsedMilliseconds);
    uint uint32_1 = BitConverter.ToUInt32(Buf, 0);
    if (uint32_1 != 0)
      throw new FX3BadStatusException($"ERROR: Failed to configure PulseWait pin FX3 GPIO {pin.pinConfig.ToString()} as input, error code: 0x{uint32_1.ToString("X4")}");
    uint uint32_2 = BitConverter.ToUInt32(Buf, 4);
    return Math.Round(1000.0 * Convert.ToDouble(checked ((ulong) BitConverter.ToUInt32(Buf, 8) * (ulong) uint.MaxValue + (ulong) uint32_2)) / (double) this.m_FX3SPIConfig.SecondsToTimerTicks, 3);
  }

  /// <summary>Reads the value of a GPIO pin on the FX3</summary>
  /// <param name="pin">The pin to read, as a FX3PinObject</param>
  /// <returns>The pin value - 1 is high, 0 is low</returns>
  public uint ReadPin(IPinObject pin)
  {
    byte[] Buf = new byte[5];
    if (this.isPWMPin(pin))
      throw new FX3ConfigurationException("ERROR: The selected pin is currently configured to drive a PWM signal. Please call StopPWM(pin) before interfacing with the pin further");
    this.ConfigureControlEndpoint(USBCommands.ADI_READ_PIN, false);
    this.FX3ControlEndPt.Index = checked ((ushort) (unchecked ((int) pin.pinConfig) & (int) byte.MaxValue));
    if (!this.XferControlData(ref Buf, 5, 1000))
      throw new FX3CommunicationException("ERROR: Pin read timed out");
    uint num = checked ((uint) Buf[1] + ((uint) Buf[2] << 8) + ((uint) Buf[3] << 16 /*0x10*/) + (uint) Buf[4] << 24);
    if (num != 0)
      throw new FX3BadStatusException("ERROR: Pin read failed, status - " + num.ToString("X4"));
    return (uint) Buf[0];
  }

  /// <summary>
  /// Reads a list of FX3 GPIO pins. This function calls the overload which takes an IEnumerable
  /// </summary>
  /// <param name="pins">An array of FX3PinObjects to read</param>
  /// <returns>The pin values, as a UInteger. The first pin is in bit 0, second is in bit 1, and so on</returns>
  public uint ReadPins(params IPinObject[] pins) => this.ReadPins((IEnumerable<IPinObject>) pins);

  /// <summary>Reads a list of FX3 GPIO pins</summary>
  /// <param name="pins">An enumerable list of FX3PinObjects to read (maximum of 32)</param>
  /// <returns>The pin values, as a UInteger. The first pin is in bit 0, second is in bit 1, and so on</returns>
  public uint ReadPins(IEnumerable<IPinObject> pins)
  {
    if (pins.Count<IPinObject>() > 32 /*0x20*/)
      throw new FX3ConfigurationException("ERROR: Cannot read more than 32 pins in one call to ReadPins");
    int num1 = 0;
    uint num2 = 0;
    try
    {
      foreach (IPinObject pin in pins)
      {
        num2 &= this.ReadPin(pin) << num1;
        checked { ++num1; }
      }
    }
    finally
    {
      IEnumerator<IPinObject> enumerator;
      enumerator?.Dispose();
    }
    return num2;
  }

  /// <summary>Not implemented</summary>
  /// <param name="start_pin">Not implemented</param>
  /// <param name="start_polarity">Not implemented</param>
  /// <param name="stop_pin">Not implemented</param>
  /// <param name="stop_polarity">Not implemented</param>
  /// <param name="delay">Not implemented</param>
  /// <returns>Not implemented</returns>
  public ushort[] ReadTime(
    uint start_pin,
    uint start_polarity,
    uint stop_pin,
    uint stop_polarity,
    uint delay)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// Sets the value of a FX3 GPIO pin. This value will persist until the pin is set to a different value, or read from
  /// </summary>
  /// <param name="pin">The FX3PinObject pin to read</param>
  /// <param name="value">The polarity to set the pin to, 1 - high, 0 - low</param>
  public void SetPin(IPinObject pin, uint value)
  {
    byte[] Buf = new byte[4];
    if (this.isPWMPin(pin))
      throw new FX3ConfigurationException("ERROR: The selected pin is currently configured to drive a PWM signal. Please call StopPWM(pin) before interfacing with the pin further");
    if (value != 0)
      value = 1U;
    this.ConfigureControlEndpoint(USBCommands.ADI_SET_PIN, false);
    this.FX3ControlEndPt.Index = checked ((ushort) (unchecked ((int) pin.pinConfig) & (int) byte.MaxValue));
    this.FX3ControlEndPt.Value = checked ((ushort) value);
    if (!this.XferControlData(ref Buf, 4, 2000))
      throw new FX3CommunicationException("ERROR: Pin set operation timed out!");
    uint num = checked ((uint) Buf[0] + ((uint) Buf[1] << 8) + ((uint) Buf[2] << 16 /*0x10*/) + (uint) Buf[3] << 24);
    if (num != 0)
      throw new FX3BadStatusException("ERROR: Pin set failed, status: 0x" + num.ToString("X4"));
  }

  /// <summary>Measures the frequency of an input signal to the selected pin.</summary>
  /// <param name="pin">The pin to measure. Must be an FX3 pin object</param>
  /// <param name="polarity">THe edge to measure from. 0 - falling edge, 1 - rising edge</param>
  /// <param name="timeoutInMs">The time to wait for the FX3 to return values before defaulting to infinity (in ms)</param>
  /// <param name="numPeriods">THe number of periods to sample for. Minimum value of 1</param>
  /// <returns>The signal frequency, in Hz. Goes to infinity if no signal found.</returns>
  public double MeasurePinFreq(IPinObject pin, uint polarity, uint timeoutInMs, ushort numPeriods)
  {
    byte[] Buf = new byte[13];
    Stopwatch stopwatch = new Stopwatch();
    if (!this.IsFX3Pin(pin))
      throw new FX3Exception("ERROR: Data ready pin type must be an FX3PinObject");
    if (numPeriods == (ushort) 0)
      throw new FX3ConfigurationException("ERRROR: NumPeriods cannot be 0");
    if (polarity != 0)
      polarity = 1U;
    Buf[0] = checked ((byte) (unchecked ((int) pin.pinConfig) & (int) byte.MaxValue));
    Buf[1] = checked ((byte) ((pin.pinConfig & 65280U) >> 8));
    Buf[2] = checked ((byte) polarity);
    ulong num1 = checked ((ulong) Math.Round(unchecked ((double) timeoutInMs * (double) this.m_FX3SPIConfig.SecondsToTimerTicks / 1000.0)));
    uint num2 = checked ((uint) Math.Round(Math.Floor(unchecked ((double) num1 / (double) uint.MaxValue))));
    uint num3 = checked ((uint) unchecked (num1 % (ulong) uint.MaxValue));
    Buf[3] = checked ((byte) (unchecked ((int) num3) & (int) byte.MaxValue));
    Buf[4] = checked ((byte) ((num3 & 65280U) >> 8));
    Buf[5] = checked ((byte) ((num3 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[6] = checked ((byte) ((num3 & 4278190080U /*0xFF000000*/) >> 24));
    Buf[7] = checked ((byte) (unchecked ((int) num2) & (int) byte.MaxValue));
    Buf[8] = checked ((byte) ((num2 & 65280U) >> 8));
    Buf[9] = checked ((byte) ((num2 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[10] = checked ((byte) ((num2 & 4278190080U /*0xFF000000*/) >> 24));
    Buf[11] = checked ((byte) ((int) numPeriods & (int) byte.MaxValue));
    Buf[12] = checked ((byte) (((uint) numPeriods & 65280U) >> 8));
    stopwatch.Start();
    this.ConfigureControlEndpoint(USBCommands.ADI_MEASURE_DR, true);
    if (!this.XferControlData(ref Buf, 13, 2000))
      throw new FX3CommunicationException("ERROR: DR frequency read timed out");
    Array.Clear((Array) Buf, 0, Buf.Length);
    bool flag = false;
    // ISSUE: variable of a reference type
    byte[]& local1;
    // ISSUE: variable of a reference type
    int& local2;
    // ISSUE: variable of a reference type
    CyUSBEndPoint& local3;
    if (timeoutInMs == 0U)
    {
      ref byte[] local4 = ref Buf;
      int num4 = 12;
      ref int local5 = ref num4;
      ref CyUSBEndPoint local6 = ref this.DataInEndPt;
      flag = USB.XferData(ref local4, ref local5, ref local6);
    }
    else
    {
      for (; !flag & stopwatch.ElapsedMilliseconds < (long) timeoutInMs; flag = USB.XferData(ref local1, ref local2, ref local3))
      {
        local1 = ref Buf;
        int num5 = 12;
        local2 = ref num5;
        local3 = ref this.DataInEndPt;
      }
    }
    stopwatch.Stop();
    uint uint32_1 = BitConverter.ToUInt32(Buf, 0);
    uint uint32_2 = BitConverter.ToUInt32(Buf, 4);
    double num6 = 1.0 / ((double) checked ((ulong) BitConverter.ToUInt32(Buf, 8) * (ulong) uint.MaxValue + (ulong) uint32_2) / (double) this.m_FX3SPIConfig.SecondsToTimerTicks / (double) numPeriods);
    if (!flag)
      num6 = double.PositiveInfinity;
    if (uint32_1 == 69U)
      num6 = double.PositiveInfinity;
    else if (uint32_1 != 0)
      throw new FX3BadStatusException("ERROR: Bad status code after pin frequency measure. Status: 0x" + uint32_1.ToString("X"));
    return num6;
  }

  /// <summary>Reads the measured DR value</summary>
  /// <param name="pin">The DR pin to measure</param>
  /// <param name="polarity">The edge to measure from. 1 - low to high, 0 - high to low</param>
  /// <param name="timeoutInMs">The timeout from when the pin measurement starts to when the function returns if the signal cannot be found</param>
  /// <returns>The DR frequency in Hz</returns>
  public double ReadDRFreq(IPinObject pin, uint polarity, uint timeoutInMs)
  {
    return this.MeasurePinFreq(pin, polarity, timeoutInMs, (ushort) 8);
  }

  /// <summary>
  /// This function measures the time delay between toggling a trigger pin, and a state change on the busy pin. This can be used to measure
  /// the propagation delay between a sync edge and data ready being de-asserted.
  /// </summary>
  /// <param name="TriggerPin">The pin to toggle. When this pin is driven to the selected polarity the delay timer starts</param>
  /// <param name="TriggerDrivePolarity">The polarity to drive the trigger pin to. 1- high, 0 - low</param>
  /// <param name="BusyPin">The pin to measure.</param>
  /// <param name="Timeout">Operation timeout period, in ms</param>
  /// <returns>The delay time, in ms</returns>
  public double MeasurePinDelay(
    IPinObject TriggerPin,
    uint TriggerDrivePolarity,
    IPinObject BusyPin,
    uint Timeout)
  {
    byte[] Buf = new byte[16 /*0x10*/];
    Stopwatch stopwatch = new Stopwatch();
    if (this.isPWMPin(BusyPin) | this.isPWMPin(TriggerPin))
      throw new FX3ConfigurationException("ERROR: The selected pin is currently configured to drive a PWM signal. Please call StopPWM(pin) before interfacing with the pin further");
    if ((int) TriggerPin.pinConfig == (int) BusyPin.pinConfig)
      throw new FX3ConfigurationException("ERROR: The BUSY pin cannot be used as the TRIGGER pin in a MeasurePinDelay function call");
    Buf[0] = checked ((byte) (unchecked ((int) TriggerPin.pinConfig) & (int) byte.MaxValue));
    Buf[1] = checked ((byte) ((TriggerPin.pinConfig & 65280U) >> 8));
    if (TriggerDrivePolarity != 0)
      TriggerDrivePolarity = 1U;
    Buf[2] = checked ((byte) TriggerDrivePolarity);
    Buf[3] = checked ((byte) (unchecked ((int) BusyPin.pinConfig) & (int) byte.MaxValue));
    Buf[4] = checked ((byte) ((BusyPin.pinConfig & 65280U) >> 8));
    uint num1 = Timeout;
    if ((double) Timeout > (double) uint.MaxValue / (double) this.m_FX3SPIConfig.SecondsToTimerTicks * 1000.0)
      num1 = 0U;
    Buf[5] = checked ((byte) (unchecked ((int) num1) & (int) byte.MaxValue));
    Buf[6] = checked ((byte) ((num1 & 65280U) >> 8));
    Buf[7] = checked ((byte) ((num1 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[8] = checked ((byte) ((num1 & 4278190080U /*0xFF000000*/) >> 24));
    this.ConfigureControlEndpoint(USBCommands.ADI_PIN_DELAY_MEASURE, true);
    if (!this.XferControlData(ref Buf, 9, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer to measure pin delay timed out!");
    Array.Clear((Array) Buf, 0, Buf.Length);
    bool flag = false;
    // ISSUE: variable of a reference type
    byte[]& local1;
    // ISSUE: variable of a reference type
    int& local2;
    // ISSUE: variable of a reference type
    CyUSBEndPoint& local3;
    if (Timeout == 0U)
    {
      ref byte[] local4 = ref Buf;
      int num2 = 12;
      ref int local5 = ref num2;
      ref CyUSBEndPoint local6 = ref this.DataInEndPt;
      flag = USB.XferData(ref local4, ref local5, ref local6);
    }
    else
    {
      for (; !flag & stopwatch.ElapsedMilliseconds < (long) Timeout; flag = USB.XferData(ref local1, ref local2, ref local3))
      {
        local1 = ref Buf;
        int num3 = 12;
        local2 = ref num3;
        local3 = ref this.DataInEndPt;
      }
    }
    stopwatch.Stop();
    if (!flag)
      return double.PositiveInfinity;
    uint uint32_1 = BitConverter.ToUInt32(Buf, 0);
    if (uint32_1 != 0)
      throw new FX3BadStatusException("ERROR: Failed to configure pin as input, error code: " + uint32_1.ToString("X4"));
    uint uint32_2 = BitConverter.ToUInt32(Buf, 4);
    return Math.Round(Convert.ToDouble(checked ((ulong) BitConverter.ToUInt32(Buf, 8) * (ulong) uint.MaxValue + (ulong) uint32_2)) * 1000.0 / (double) this.m_FX3SPIConfig.SecondsToTimerTicks, 4);
  }

  /// <summary>
  /// This function triggers a DUT action using a pulse drive, and then measures the following pulse width on a separate busy line.
  /// The pulse time on the busy pin is measured using a 10MHz timer with approx. 0.1us accuracy.
  /// </summary>
  /// <param name="TriggerPin">The pin to drive for the trigger condition (for example a sync pin)</param>
  /// <param name="TriggerDriveTime">The time, in ms, to drive the trigger pin for</param>
  /// <param name="TriggerDrivePolarity">The polarity to drive the trigger pin at (0 - low, 1 - high)</param>
  /// <param name="BusyPin">The pin to measure a busy pulse on</param>
  /// <param name="BusyPolarity">The polarity of the pulse being measured (0 will measure a low pulse, 1 will measure a high pulse)</param>
  /// <param name="Timeout">The timeout, in ms, to wait before canceling, if the pulse is never detected</param>
  /// <returns>The pulse width, in ms. Accurate to approx. 1us</returns>
  public double MeasureBusyPulse(
    IPinObject TriggerPin,
    uint TriggerDriveTime,
    uint TriggerDrivePolarity,
    IPinObject BusyPin,
    uint BusyPolarity,
    uint Timeout)
  {
    byte[] Buf = new byte[15];
    Stopwatch stopwatch = new Stopwatch();
    uint num1 = checked ((uint) Math.Round(unchecked (1000.0 * (double) uint.MaxValue / (double) this.m_FX3SPIConfig.SecondsToTimerTicks)));
    if (this.isPWMPin(BusyPin))
      throw new FX3ConfigurationException("ERROR: The selected busy pin is currently configured to drive a PWM signal. Please call StopPWM(pin) before interfacing with the pin further");
    if ((long) (BusyPin.pinConfig & (uint) byte.MaxValue) % 8L == 0L)
      throw new FX3ConfigurationException("ERROR: The selected busy pin shares a timer peripheral with the FX3 10MHz timebase timer. This pin cannot be used for measurements");
    if (TriggerDriveTime > num1)
      throw new FX3ConfigurationException($"ERROR: Invalid trigger pin drive time of {TriggerDriveTime.ToString()}ms. Max allowed is {num1.ToString()}ms");
    if (Timeout > num1)
      throw new FX3ConfigurationException($"ERROR: Invalid timeout time of {Timeout.ToString()}ms. Max allowed is {num1.ToString()}ms");
    if ((int) TriggerPin.pinConfig == (int) BusyPin.pinConfig)
      throw new FX3ConfigurationException("ERROR: The BUSY pin cannot be used as the TRIGGER pin in a MeasureBusyPulse function call");
    Buf[0] = checked ((byte) (unchecked ((int) BusyPin.pinConfig) & (int) byte.MaxValue));
    Buf[1] = checked ((byte) ((BusyPin.pinConfig & 65280U) >> 8));
    if (BusyPolarity != 0)
      BusyPolarity = 1U;
    Buf[2] = checked ((byte) BusyPolarity);
    Buf[3] = checked ((byte) (unchecked ((int) Timeout) & (int) byte.MaxValue));
    Buf[4] = checked ((byte) ((Timeout & 65280U) >> 8));
    Buf[5] = checked ((byte) ((Timeout & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[6] = checked ((byte) ((Timeout & 4278190080U /*0xFF000000*/) >> 24));
    Buf[7] = (byte) 0;
    Buf[8] = checked ((byte) (unchecked ((int) TriggerPin.pinConfig) & (int) byte.MaxValue));
    Buf[9] = checked ((byte) ((TriggerPin.pinConfig & 65280U) >> 8));
    Buf[10] = checked ((byte) TriggerDrivePolarity);
    Buf[11] = checked ((byte) (unchecked ((int) TriggerDriveTime) & (int) byte.MaxValue));
    Buf[12] = checked ((byte) ((TriggerDriveTime & 65280U) >> 8));
    Buf[13] = checked ((byte) ((TriggerDriveTime & 16711680U /*0xFF0000*/) >> 16 /*0x10*/));
    Buf[14] = checked ((byte) ((TriggerDriveTime & 4278190080U /*0xFF000000*/) >> 24));
    stopwatch.Start();
    this.ConfigureControlEndpoint(USBCommands.ADI_BUSY_MEASURE, true);
    if (!this.XferControlData(ref Buf, 15, 2000))
      throw new FX3CommunicationException("ERROR: Control Endpoint transfer timed out");
    Array.Clear((Array) Buf, 0, Buf.Length);
    bool flag = false;
    // ISSUE: variable of a reference type
    byte[]& local1;
    // ISSUE: variable of a reference type
    int& local2;
    // ISSUE: variable of a reference type
    CyUSBEndPoint& local3;
    if (Timeout == 0U)
    {
      ref byte[] local4 = ref Buf;
      int num2 = 8;
      ref int local5 = ref num2;
      ref CyUSBEndPoint local6 = ref this.DataInEndPt;
      flag = USB.XferData(ref local4, ref local5, ref local6);
    }
    else
    {
      for (; !flag & stopwatch.ElapsedMilliseconds < (long) Timeout; flag = USB.XferData(ref local1, ref local2, ref local3))
      {
        local1 = ref Buf;
        int num3 = 8;
        local2 = ref num3;
        local3 = ref this.DataInEndPt;
      }
    }
    stopwatch.Stop();
    if (!flag)
      return double.PositiveInfinity;
    uint uint32 = BitConverter.ToUInt32(Buf, 0);
    if (uint32 == 69U)
      return double.PositiveInfinity;
    if (uint32 != 0)
      throw new FX3BadStatusException("ERROR: Busy pin pulse measure failed, error code: 0x" + uint32.ToString("X4"));
    return Math.Round(Convert.ToDouble(Decimal.Divide(Decimal.Multiply(1000M, new Decimal((ulong) BitConverter.ToUInt32(Buf, 4))), new Decimal(this.m_FX3SPIConfig.SecondsToTimerTicks))), 4);
  }

  /// <summary>
  /// Overload of measure busy pulse which triggers the DUT event using a SPI write instead of a pin drive.
  /// </summary>
  /// <param name="SpiTriggerData">The data to transmit on the MOSI line, to trigger the operation being measured</param>
  /// <param name="BusyPin">The pin to measure a busy pulse on</param>
  /// <param name="BusyPolarity">The polarity of the pulse being measured (0 will measure a low pulse, 1 will measure a high pulse)</param>
  /// <param name="Timeout">The timeout, in ms, to wait before canceling, if the pulse is never detected</param>
  /// <returns>The pulse width, in ms. Accurate to approx. 1us</returns>
  public double MeasureBusyPulse(
    byte[] SpiTriggerData,
    IPinObject BusyPin,
    uint BusyPolarity,
    uint Timeout)
  {
    List<byte> byteList = new List<byte>();
    byte[] numArray = new byte[8];
    Stopwatch stopwatch = new Stopwatch();
    uint num1 = checked ((uint) Math.Round(unchecked (1000.0 * (double) uint.MaxValue / (double) this.m_FX3SPIConfig.SecondsToTimerTicks)));
    if (this.isPWMPin(BusyPin))
      throw new FX3ConfigurationException("ERROR: The selected pin is currently configured to drive a PWM signal. Please call StopPWM(pin) before interfacing with the pin further");
    if ((uint) checked (((IEnumerable<byte>) SpiTriggerData).Count<byte>() * 8) % (uint) this.m_FX3SPIConfig.WordLength > 0U)
      throw new FX3ConfigurationException("ERROR: SPI trigger data must be a multiple of the SPI word length.");
    if (this.isPWMPin(BusyPin))
      throw new FX3ConfigurationException("ERROR: The selected busy pin is currently configured to drive a PWM signal. Please call StopPWM(pin) before interfacing with the pin further");
    if ((long) (BusyPin.pinConfig & (uint) byte.MaxValue) % 8L == 0L)
      throw new FX3ConfigurationException("ERROR: The selected busy pin shares a timer peripheral with the FX3 10MHz timebase timer. This pin cannot be used for measurements");
    if (Timeout > num1)
      throw new FX3ConfigurationException($"ERROR: Invalid timeout time of {Timeout.ToString()}ms. Max allowed is {num1.ToString()}ms");
    byteList.Add(checked ((byte) (unchecked ((int) BusyPin.pinConfig) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) ((BusyPin.pinConfig & 65280U) >> 8)));
    if (BusyPolarity != 0)
      BusyPolarity = 1U;
    byteList.Add(checked ((byte) BusyPolarity));
    byteList.Add(checked ((byte) (unchecked ((int) Timeout) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) ((Timeout & 65280U) >> 8)));
    byteList.Add(checked ((byte) ((Timeout & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)));
    byteList.Add(checked ((byte) ((Timeout & 4278190080U /*0xFF000000*/) >> 24)));
    byteList.Add((byte) 1);
    byteList.Add(checked ((byte) ((long) ((IEnumerable<byte>) SpiTriggerData).Count<byte>() & (long) byte.MaxValue)));
    byteList.Add(checked ((byte) (((long) ((IEnumerable<byte>) SpiTriggerData).Count<byte>() & 65280L) >> 8)));
    byteList.AddRange((IEnumerable<byte>) SpiTriggerData);
    stopwatch.Start();
    this.ConfigureControlEndpoint(USBCommands.ADI_BUSY_MEASURE, true);
    byte[] array = byteList.ToArray();
    if (!this.XferControlData(ref array, byteList.Count, 2000))
      throw new FX3CommunicationException("ERROR: Control Endpoint transfer timed out");
    bool flag = false;
    // ISSUE: variable of a reference type
    byte[]& local1;
    // ISSUE: variable of a reference type
    int& local2;
    // ISSUE: variable of a reference type
    CyUSBEndPoint& local3;
    if (Timeout == 0U)
    {
      ref byte[] local4 = ref numArray;
      int num2 = 8;
      ref int local5 = ref num2;
      ref CyUSBEndPoint local6 = ref this.DataInEndPt;
      flag = USB.XferData(ref local4, ref local5, ref local6);
    }
    else
    {
      for (; !flag & stopwatch.ElapsedMilliseconds < (long) Timeout; flag = USB.XferData(ref local1, ref local2, ref local3))
      {
        local1 = ref numArray;
        int num3 = 8;
        local2 = ref num3;
        local3 = ref this.DataInEndPt;
      }
    }
    stopwatch.Stop();
    if (!flag)
      return double.PositiveInfinity;
    uint uint32 = BitConverter.ToUInt32(numArray, 0);
    if (uint32 == 69U)
      return double.PositiveInfinity;
    if (uint32 != 0)
      throw new FX3BadStatusException("ERROR: Busy pin pulse measure failed, error code: 0x" + uint32.ToString("X4"));
    return Math.Round(Convert.ToDouble(Decimal.Divide(Decimal.Multiply(1000M, new Decimal((ulong) BitConverter.ToUInt32(numArray, 4))), new Decimal(this.m_FX3SPIConfig.SecondsToTimerTicks))), 4);
  }

  /// <summary>
  /// This function configures the selected pin to drive a pulse width modulated output.
  /// </summary>
  /// <param name="Frequency">The desired PWM frequency, in Hz. Valid values are in the range of 0.05Hz (0.05) - 50MHz (20000000.0)</param>
  /// <param name="DutyCycle">The PWM duty cycle. Valid values are in the range 0.0 - 1.0. To achieve a "clock" signal set the duty cycle to 0.5</param>
  /// <param name="Pin">The pin to configure as a PWM signal.</param>
  public void StartPWM(double Frequency, double DutyCycle, IPinObject Pin)
  {
    if (!this.IsFX3Pin(Pin))
      throw new FX3Exception("ERROR: All pin objects used with the FX3 API must be of type FX3PinObject");
    uint num1 = Pin.pinConfig % 8U;
    try
    {
      foreach (PinPWMInfo pinPwmInfo in (List<PinPWMInfo>) this.m_PinPwmInfoList)
      {
        if ((long) pinPwmInfo.FX3GPIONumber != (long) Pin.pinConfig & (long) num1 == (long) pinPwmInfo.FX3TimerBlock)
          throw new FX3ConfigurationException("ERROR: The PWM hardware for the pin selected is currently being used by pin number " + pinPwmInfo.FX3GPIONumber.ToString());
      }
    }
    finally
    {
      List<PinPWMInfo>.Enumerator enumerator;
      enumerator.Dispose();
    }
    if (Frequency < 0.05 | Frequency > 50000000.0)
      throw new FX3ConfigurationException($"ERROR: Invalid PWM frequency: {Frequency.ToString()}Hz");
    if (DutyCycle < 0.0 | DutyCycle > 1.0)
      throw new FX3ConfigurationException($"ERROR: Invalid duty cycle: {(100.0 * DutyCycle).ToString()}%");
    if (num1 == 0U)
      throw new FX3ConfigurationException($"ERROR: The selected {Pin.ToString()} pin cannot be used as a PWM");
    double num2 = 201567700.0;
    uint num3 = checked (Convert.ToUInt32(unchecked (num2 / Frequency)) - 1U);
    uint uint32 = Convert.ToUInt32(num2 / Frequency * DutyCycle);
    if (uint32 != 0)
      checked { --uint32; }
    if (uint32 < 1U)
      throw new FX3ConfigurationException($"ERROR: The selected PWM setting (Freq: {Frequency.ToString()}Hz, Duty Cycle: {(DutyCycle * 100.0).ToString()}%) is not achievable using a 200MHz clock");
    byte[] Buf = new byte[10]
    {
      checked ((byte) (unchecked ((int) Pin.pinConfig) & (int) byte.MaxValue)),
      (byte) 0,
      checked ((byte) (unchecked ((int) num3) & (int) byte.MaxValue)),
      checked ((byte) ((num3 & 65280U) >> 8)),
      checked ((byte) ((num3 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)),
      checked ((byte) ((num3 & 4278190080U /*0xFF000000*/) >> 24)),
      checked ((byte) (unchecked ((int) uint32) & (int) byte.MaxValue)),
      checked ((byte) ((uint32 & 65280U) >> 8)),
      checked ((byte) ((uint32 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)),
      checked ((byte) ((uint32 & 4278190080U /*0xFF000000*/) >> 24))
    };
    this.ConfigureControlEndpoint(USBCommands.ADI_PWM_CMD, true);
    this.FX3ControlEndPt.Index = (ushort) 1;
    if (!this.XferControlData(ref Buf, 10, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer timed out while setting up a PWM signal!");
    PinPWMInfo Pin1 = new PinPWMInfo();
    double RealFreq = num2 / (double) num3;
    double RealDutyCycle = (double) uint32 / (double) num3;
    Pin1.SetValues(Pin, Frequency, RealFreq, DutyCycle, RealDutyCycle);
    this.m_PinPwmInfoList.AddReplace(Pin1);
  }

  /// <summary>
  /// This function call disables the PWM output from the FX3 and returns the pin to a tri-stated mode.
  /// </summary>
  public void StopPWM(IPinObject Pin)
  {
    if (!this.isPWMPin(Pin))
      return;
    byte[] Buf = new byte[2]
    {
      checked ((byte) (unchecked ((int) Pin.pinConfig) & (int) byte.MaxValue)),
      (byte) 0
    };
    this.ConfigureControlEndpoint(USBCommands.ADI_PWM_CMD, true);
    this.FX3ControlEndPt.Index = (ushort) 0;
    if (!this.XferControlData(ref Buf, 2, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer timed out while stopping a PWM signal");
    try
    {
      foreach (PinPWMInfo pinPwmInfo in (List<PinPWMInfo>) this.m_PinPwmInfoList)
      {
        if ((long) pinPwmInfo.FX3GPIONumber == (long) Pin.pinConfig)
        {
          this.m_PinPwmInfoList.Remove(pinPwmInfo);
          break;
        }
      }
    }
    finally
    {
      List<PinPWMInfo>.Enumerator enumerator;
      enumerator.Dispose();
    }
  }

  /// <summary>
  /// This function checks to see if the selected pin has already been configured to act as a PWM output pin.
  /// </summary>
  /// <param name="Pin">The pin to check. Must be an FX3PinObject pin</param>
  /// <returns>True if the pin is configured as a PWM pin, false otherwise</returns>
  public bool isPWMPin(IPinObject Pin) => this.IsFX3Pin(Pin) && this.m_PinPwmInfoList.Contains(Pin);

  /// <summary>
  /// Allows the user to retrieve a set of information about the current pin PWM configuration.
  /// </summary>
  /// <param name="Pin">The pin to pull from the PinPWMInfo List</param>
  /// <returns>The PinPWMInfo corresponding to the selected pin. If the pin is not found all fields will be -1</returns>
  public PinPWMInfo GetPinPWMInfo(IPinObject Pin) => this.m_PinPwmInfoList.GetPinPWMInfo(Pin);

  /// <summary>
  /// This function determines if the pin object being passed is an FX3 version of the IPinObject (as opposed to a blackfin pin for the SDP).
  /// </summary>
  /// <param name="Pin">The pin to check</param>
  /// <returns>True if Pin is an FX3 pin, false if not</returns>
  private bool IsFX3Pin(IPinObject Pin)
  {
    bool flag = false;
    if (Operators.CompareString(Pin.ToString().Substring(0, 3), "FX3", false) == 0 & Pin.GetType() == typeof (FX3PinObject))
      flag = true;
    return flag;
  }

  /// <summary>Turn on user LED (not available on Cypress Explorer FX3 board)</summary>
  public void UserLEDOn()
  {
    FX3PinObject fx3PinObject = new FX3PinObject(13U);
    if (this.isPWMPin((IPinObject) fx3PinObject))
      this.StopPWM((IPinObject) fx3PinObject);
    this.SetPin((IPinObject) fx3PinObject, 0U);
  }

  /// <summary>Turn off user LED (not available on Cypress Explorer FX3 board)</summary>
  public void UserLEDOff()
  {
    FX3PinObject fx3PinObject = new FX3PinObject(13U);
    if (this.isPWMPin((IPinObject) fx3PinObject))
      this.StopPWM((IPinObject) fx3PinObject);
    this.SetPin((IPinObject) fx3PinObject, 1U);
  }

  /// <summary>
  /// Blink user LED using timer hardware (not available on Cypress Explorer FX3 board)
  /// </summary>
  /// <param name="BlinkFreq">Frequency to blink LED at</param>
  public void UserLEDBlink(double BlinkFreq)
  {
    this.StartPWM(BlinkFreq, 0.5, (IPinObject) new FX3PinObject(13U));
  }

  /// <summary>Read-only property to get the reset pin</summary>
  /// <returns>The reset pin, as an IPinObject</returns>
  public IPinObject ResetPin => (IPinObject) new FX3PinObject((uint) this.RESET_PIN);

  /// <summary>Read-only property to get the DIO1 pin</summary>
  /// <returns>Returns the DIO1 pin, as an IPinObject</returns>
  public IPinObject DIO1 => (IPinObject) new FX3PinObject((uint) this.DIO1_PIN);

  /// <summary>Read-only property to get the DIO2 pin</summary>
  /// <returns>Returns the DIO2 pin, as an IPinObject</returns>
  public IPinObject DIO2 => (IPinObject) new FX3PinObject((uint) this.DIO2_PIN);

  /// <summary>Read-only property to get the DIO3 pin</summary>
  /// <returns>Returns the DIO3 pin, as an IPinObject</returns>
  public IPinObject DIO3 => (IPinObject) new FX3PinObject((uint) this.DIO3_PIN);

  /// <summary>Read-only property to get the DIO4 pin</summary>
  /// <returns>Returns the DIO4 pin, as an IPinObject</returns>
  public IPinObject DIO4 => (IPinObject) new FX3PinObject((uint) this.DIO4_PIN);

  /// <summary>
  /// Read-only property to get the FX3_GPIO1 pin. This pin does not map to the standard iSensor breakout,
  /// and should be used for other general purpose interfacing.
  /// </summary>
  /// <returns>Returns the GPIO pin, as an IPinObject</returns>
  public IPinObject FX3_GPIO1 => (IPinObject) new FX3PinObject((uint) this.FX3_GPIO1_PIN);

  /// <summary>
  /// Read-only property to get the FX3_GPIO2 pin. This pin does not map to the standard iSensor breakout,
  /// and should be used for other general purpose interfacing.
  /// </summary>
  /// <returns>Returns the GPIO pin, as an IPinObject</returns>
  public IPinObject FX3_GPIO2 => (IPinObject) new FX3PinObject((uint) this.FX3_GPIO2_PIN);

  /// <summary>
  /// Read-only property to get the FX3_GPIO3 pin. This pin does not map to the standard iSensor breakout,
  /// and should be used for other general purpose interfacing.
  /// </summary>
  /// <returns>Returns the GPIO pin, as an IPinObject</returns>
  public IPinObject FX3_GPIO3 => (IPinObject) new FX3PinObject((uint) this.FX3_GPIO3_PIN);

  /// <summary>
  /// Read-only property to get the FX3_GPIO4 pin. This pin does not map to the standard iSensor breakout,
  /// and should be used for other general purpose interfacing. This pin shares a complex GPIO block with DIO1. If DIO1 is being used
  /// as a clock source, via the StartPWM function, then this pin cannot be used as a clock source.
  /// </summary>
  /// <returns>Returns the GPIO pin, as an IPinObject</returns>
  public IPinObject FX3_GPIO4 => (IPinObject) new FX3PinObject((uint) this.FX3_GPIO4_PIN);

  /// <summary>
  /// Read-only property to get loop back pin 1. This pin (CTL8) is wired directly to
  /// loop back pin 2 (CTL9) on the iSensor FX3 Board, Revision C or newer. These loop
  /// back pins allow for fixed transaction timing on "asynchronous" SPI/I2C reads. One
  /// of the loop back pins can be configured as a PWM output using the "StartPWM" API,
  /// and the other loop back pin can be set as the DrPin, allowing the FX3 to trigger
  /// itself at a known rate.
  /// </summary>
  /// <returns>The first loop back pin, as an IPinObject</returns>
  public IPinObject FX3_LOOPBACK1 => (IPinObject) new FX3PinObject((uint) this.FX3_LOOP1_PIN);

  /// <summary>
  /// Read-only property to get loop back pin 2. This pin (CTL9) is wired directly to
  /// loop back pin 1 (CTL8) on the iSensor FX3 Board, Revision C or newer. These loop
  /// back pins allow for fixed transaction timing on "asynchronous" SPI/I2C reads. One
  /// of the loop back pins can be configured as a PWM output using the "StartPWM" API,
  /// and the other loop back pin can be set as the DrPin, allowing the FX3 to trigger
  /// itself at a known rate.
  /// </summary>
  /// <returns>The second loop back pin, as an IPinObject</returns>
  public IPinObject FX3_LOOPBACK2 => (IPinObject) new FX3PinObject((uint) this.FX3_LOOP2_PIN);

  /// <summary>If the data ready is used for register reads</summary>
  /// <returns>The current data ready usage setting</returns>
  public bool DrActive
  {
    get => this.m_FX3SPIConfig.DrActive;
    set
    {
      this.m_FX3SPIConfig.DrActive = value;
      if (!this.m_FX3Connected)
        return;
      this.m_ActiveFX3.ControlEndPt.Index = (ushort) 12;
      this.m_ActiveFX3.ControlEndPt.Value = (ushort) -(this.m_FX3SPIConfig.DrActive ? 1 : 0);
      this.ConfigureSPI();
    }
  }

  /// <summary>
  /// Switches burstMode on and off. Set burstMode to the number of burst read registers.
  /// </summary>
  /// <returns>The number of burst read registers.</returns>
  public ushort BurstMode
  {
    get => checked ((ushort) this.m_burstMode);
    set => this.m_burstMode = (uint) value;
  }

  /// <summary>Sets the timeout for the Bulk Endpoint used in real time streaming modes.</summary>
  /// <returns>The timeout time, in seconds</returns>
  public int StreamTimeoutSeconds
  {
    get
    {
      if (Information.IsNothing((object) this.m_StreamTimeout))
        this.m_StreamTimeout = 3;
      return this.m_StreamTimeout;
    }
    set
    {
      this.m_StreamTimeout = value >= 1 ? value : throw new FX3ConfigurationException($"ERROR: Stream timeout {value.ToString()}s invalid!");
    }
  }

  /// <summary>
  /// Starts a buffered stream for only a single buffer.
  /// This is equivalent to StartBufferedStream(addr, numCaptures, 1, CurrentTimeout, Nothing)
  /// </summary>
  /// <param name="addr">The address list to read from</param>
  /// <param name="numCaptures">The number of times to capture that address list</param>
  public void StartStream(IEnumerable<uint> addr, uint numCaptures)
  {
    this.StartBufferedStream(addr, numCaptures, 1U, this.StreamTimeoutSeconds, (BackgroundWorker) null);
  }

  /// <summary>
  /// Starts a buffered stream operation. The registers listed in addr are read numCaptures times per register buffer. This process is repeated numBuffers times.
  /// </summary>
  /// <param name="addr">List of register addresses to read</param>
  /// <param name="numCaptures">Number of times to read the register list per buffer.</param>
  /// <param name="numBuffers">Number of total register buffers to read.</param>
  /// <param name="timeoutSeconds">Stream timeout, in seconds</param>
  /// <param name="worker">Background worker to handle progress updates</param>
  public void StartBufferedStream(
    IEnumerable<uint> addr,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    try
    {
      foreach (uint addr1 in addr)
        addrData.Add(new AddrDataPair(addr1, new uint?()));
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
    this.StartBufferedStream((IEnumerable<AddrDataPair>) addrData, numCaptures, numBuffers, timeoutSeconds, worker);
  }

  /// <summary>
  /// Starts a buffered stream operation. This is usually called from the TextFileStreamManager. DUTType must be set before executing.
  /// </summary>
  /// <param name="addrData">The list of register addresses to read from, when PartType is not ADcmXLx021</param>
  /// <param name="numCaptures">The number of reads to perform on each register listed in addr</param>
  /// <param name="numBuffers">The total number of buffers to read. One buffer is either a frame or a set of register reads</param>
  /// <param name="timeoutSeconds">The bulk endpoint timeout time</param>
  /// <param name="worker">A Background worker object which can be used by a GUI to track the current stream status and send cancellation requests</param>
  public void StartBufferedStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    Stopwatch stopwatch = new Stopwatch();
    bool flag1 = !Information.IsNothing((object) worker);
    if (flag1)
      flag1 &= worker.WorkerReportsProgress;
    bool flag2 = !Information.IsNothing((object) worker);
    if (flag2)
      flag2 &= worker.WorkerSupportsCancellation;
    try
    {
      foreach (AddrDataPair addrDataPair in addrData)
      {
        if (addrDataPair.data.HasValue)
          addrDataPair.addr |= 128U /*0x80*/;
      }
    }
    finally
    {
      IEnumerator<AddrDataPair> enumerator;
      enumerator?.Dispose();
    }
    this.StreamTimeoutSeconds = timeoutSeconds;
    if (numBuffers < 1U)
      throw new FX3ConfigurationException("ERROR: numBuffers must be at least one");
    int num = 0;
    if (this.PartType == DUTType.ADcmXL1021 | this.PartType == DUTType.ADcmXL2021 | this.PartType == DUTType.ADcmXL3021)
      this.StartRealTimeStreaming(numBuffers);
    else if (this.BurstMode == (ushort) 0)
      this.StartGenericStream(addrData, numCaptures, numBuffers);
    else
      this.StartBurstStream(numBuffers, (IEnumerable<byte>) this.BurstMOSIData);
    if (Information.IsNothing((object) worker))
      return;
    stopwatch.Start();
    do
      ;
    while (!this.m_StreamThreadRunning & stopwatch.ElapsedMilliseconds < (long) checked (1000 * timeoutSeconds));
    while (this.GetNumBuffersRead < (long) numBuffers & this.m_StreamThreadRunning)
    {
      if (flag2 && worker.CancellationPending)
      {
        this.StopStream();
        break;
      }
      if (flag1)
      {
        int percentProgress = checked ((int) Math.Round(unchecked ((double) checked (this.GetNumBuffersRead * 100L) / (double) numBuffers)));
        if (percentProgress > num)
        {
          worker.ReportProgress(percentProgress);
          num = percentProgress;
        }
      }
      Thread.Sleep(25);
    }
  }

  /// <summary>Stops the currently running data stream, if any.</summary>
  public void StopStream()
  {
    switch (this.m_StreamType)
    {
      case StreamType.BurstStream:
        this.CancelStreamImplementation(USBCommands.ADI_STREAM_BURST_DATA);
        break;
      case StreamType.RealTimeStream:
        this.StopRealTimeStreaming();
        break;
      case StreamType.GenericStream:
        this.CancelStreamImplementation(USBCommands.ADI_STREAM_GENERIC_DATA);
        break;
      case StreamType.TransferStream:
        this.CancelStreamImplementation(USBCommands.ADI_TRANSFER_STREAM);
        break;
      case StreamType.I2CReadStream:
        this.CancelStreamImplementation(USBCommands.ADI_I2C_READ_STREAM);
        break;
    }
    Thread.Sleep(40);
  }

  /// <summary>
  /// This function returns a single buffered stream packet. Needed for IBufferedStreamProducer
  /// </summary>
  /// <returns>The stream data packet, as a short</returns>
  public ushort[] GetBufferedStreamDataPacket() => this.GetBuffer();

  /// <summary>This function does the same thing as GetBufferedStreamDataPacket()</summary>
  /// <returns>The last buffer read from the DUT</returns>
  public ushort[] GetStreamDataPacketU16() => this.GetBuffer();

  /// <summary>
  /// This is the most general ReadRegByte. Other implementations are based on this.
  /// </summary>
  /// <param name="addr">The address to read</param>
  /// <returns>Returns the value read in over SPI as a short</returns>
  public ushort ReadRegByte(uint addr)
  {
    ushort num = this.ReadRegWord(addr);
    return (long) addr % 2L == 0L ? checked ((ushort) ((int) num & (int) byte.MaxValue)) : (ushort) ((uint) num << 8);
  }

  /// <summary>Reads a single 16 bit register on the DUT</summary>
  /// <param name="addr">The address of the register to read</param>
  /// <returns>The 16 bit register value, as a UShort</returns>
  public ushort ReadRegWord(uint addr)
  {
    byte[] Buf = new byte[6];
    this.ConfigureControlEndpoint(USBCommands.ADI_READ_BYTES, false);
    this.FX3ControlEndPt.Value = (ushort) 0;
    this.FX3ControlEndPt.Index = checked ((ushort) addr);
    ushort num = this.XferControlData(ref Buf, 6, 2000) ? BitConverter.ToUInt16(Buf, 4) : throw new FX3CommunicationException("ERROR: FX3 is not responding to transfer request");
    uint uint32 = BitConverter.ToUInt32(Buf, 0);
    if (uint32 != 0)
      throw new FX3BadStatusException("ERROR: Bad read command - " + uint32.ToString("X4"));
    return num;
  }

  /// <summary>This is the most general WriteRegByte, which the others are based on</summary>
  /// <param name="addr">The address to write to</param>
  /// <param name="data">The byte of data to write</param>
  public void WriteRegByte(uint addr, uint data)
  {
    byte[] Buf = new byte[4];
    this.ConfigureControlEndpoint(USBCommands.ADI_WRITE_BYTE, false);
    this.FX3ControlEndPt.Value = checked ((ushort) (unchecked ((int) data) & (int) ushort.MaxValue));
    this.FX3ControlEndPt.Index = checked ((ushort) (unchecked ((int) addr) & (int) ushort.MaxValue));
    uint num = this.XferControlData(ref Buf, 4, 2000) ? BitConverter.ToUInt32(Buf, 0) : throw new FX3CommunicationException("ERROR: WriteRegByte timed out - Check board connection");
    if (num != 0)
      throw new FX3BadStatusException("ERROR: Bad write command - " + num.ToString("X4"));
  }

  /// <summary>
  /// This function writes a single register byte, given as an Address / Data pair
  /// </summary>
  /// <param name="addrData">The AddrDataPair to be written</param>
  public void WriteRegByte(AddrDataPair addrData)
  {
    this.WriteRegByte(addrData.addr, addrData.data.Value);
  }

  /// <summary>
  /// This function is not currently implemented. Calling it will throw a NotImplementedException.
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="data"></param>
  public void WriteRegWord(uint addr, uint data) => throw new NotImplementedException();

  /// <summary>
  /// This is the most generic array register function. All other array read/write functions call down to this one.
  /// </summary>
  /// <param name="addrData">The list of register addresses and optional write data for each capture</param>
  /// <param name="numCaptures">The number of times to iterate through addrData per DUT data ready (if DrActive is set)</param>
  /// <param name="numBuffers">The total number of buffers to read, where one buffer is considered numCaptures iterations through addrData</param>
  /// <returns>An array of 16 bit values read back from the DUT. The size will be addrData.Count() * numCaptures * numBuffers</returns>
  public ushort[] ReadRegArrayStream(
    IEnumerable<AddrDataPair> addrData,
    uint numCaptures,
    uint numBuffers)
  {
    List<ushort> ushortList = new List<ushort>();
    int len;
    if (this.m_ActiveFX3.bSuperSpeed)
    {
      len = 1024 /*0x0400*/;
    }
    else
    {
      if (!this.m_ActiveFX3.bHighSpeed)
        throw new FX3Exception("ERROR: Streaming application requires USB 2.0 or 3.0 connection to function");
      len = 512 /*0x0200*/;
    }
    byte[] buf = new byte[checked (len - 1 + 1)];
    int num1 = checked ((int) ((long) addrData.Count<AddrDataPair>() * (long) numCaptures * (long) numBuffers));
    int num2 = checked ((int) ((long) addrData.Count<AddrDataPair>() * (long) numCaptures * 2L));
    int num3 = num2 <= len ? checked ((int) Math.Round(unchecked (Math.Floor((double) len / (double) num2) * (double) num2))) : len;
    this.m_StreamThreadRunning = false;
    this.m_StreamMutex.WaitOne();
    this.GenericStreamSetup(addrData, numCaptures, numBuffers);
    while (ushortList.Count < num1)
    {
      if (USB.XferData(ref buf, ref len, ref this.StreamingEndPt))
      {
        int num4 = checked (num3 - 2);
        int startIndex = 0;
        while (startIndex <= num4)
        {
          ushortList.Add(BitConverter.ToUInt16(buf, startIndex));
          if (ushortList.Count != num1)
            checked { startIndex += 2; }
          else
            break;
        }
      }
      else
      {
        this.m_StreamMutex.ReleaseMutex();
        this.CancelStreamImplementation(USBCommands.ADI_STREAM_GENERIC_DATA);
        string[] strArray = new string[5]
        {
          "ERROR: Transfer failed during register array read/write. Error code: ",
          null,
          null,
          null,
          null
        };
        uint lastError = this.StreamingEndPt.LastError;
        strArray[1] = lastError.ToString();
        strArray[2] = " (0x";
        lastError = this.StreamingEndPt.LastError;
        strArray[3] = lastError.ToString("X4");
        strArray[4] = ")";
        throw new FX3CommunicationException(string.Concat(strArray));
      }
    }
    this.GenericStreamDone();
    this.m_StreamMutex.ReleaseMutex();
    return ushortList.ToArray();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="addr"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  public ushort[] ReadRegArrayStream(IEnumerable<uint> addr, uint numCaptures, uint numBuffers)
  {
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    try
    {
      foreach (uint addr1 in addr)
        addrData.Add(new AddrDataPair(addr1, new uint?()));
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
    return this.ReadRegArrayStream((IEnumerable<AddrDataPair>) addrData, numCaptures, numBuffers);
  }

  /// <summary>
  /// Overload of ReadRegArray which builds a new IEnumerable of addr and call the overload which takes an enumerable of addr
  /// </summary>
  /// <param name="addr">List of register address's to read</param>
  /// <param name="numCaptures">Number of captures to perform on the register list</param>
  /// <returns>The register values, as a short array</returns>
  public ushort[] ReadRegArray(IEnumerable<uint> addr, uint numCaptures)
  {
    return this.ReadRegArrayStream(addr, numCaptures, 1U);
  }

  /// <summary>This function writes an enumerable list of data to the DUT as AddrDataPairs</summary>
  /// <param name="addrData">The list of AddrDataPair to be written to DUT</param>
  public void WriteRegByte(IEnumerable<AddrDataPair> addrData)
  {
    this.ReadRegArrayStream(addrData, 1U, 1U);
  }

  /// <summary>
  /// Overload of WriteRegByte which allows for multiple registers to be specified to write to, as an IEnumerable list of register addresses.
  /// </summary>
  /// <param name="addr">The list of register addresses to write to.</param>
  /// <param name="data">The data to write to each register in the address list.</param>
  public void WriteRegByte(IEnumerable<uint> addr, IEnumerable<uint> data)
  {
    if (addr.Count<uint>() != data.Count<uint>())
      throw new FX3ConfigurationException("ERROR: WriteRegByte must take the same number of addresses and data values");
    List<AddrDataPair> addrData = new List<AddrDataPair>();
    int num = checked (addr.Count<uint>() - 1);
    int index = 0;
    while (index <= num)
    {
      addrData.Add(new AddrDataPair()
      {
        addr = addr.ElementAtOrDefault<uint>(index),
        data = new uint?(data.ElementAtOrDefault<uint>(index))
      });
      checked { ++index; }
    }
    this.ReadRegArrayStream((IEnumerable<AddrDataPair>) addrData, 1U, 1U);
  }

  /// <summary>Reads an array of 16 bit register values.</summary>
  /// <param name="addr">The list of registers to read</param>
  /// <returns>The register values, as a UShort array</returns>
  public ushort[] ReadRegArray(IEnumerable<uint> addr) => this.ReadRegArrayStream(addr, 1U, 1U);

  /// <summary>
  /// ReadRegArray overload which includes register writes. Breaks the call into multiple calls of readRegByte and writeRegByte
  /// </summary>
  /// <param name="addrData">The data to read/write</param>
  /// <param name="numCaptures">The number of times to perform the read/write operation</param>
  /// <returns>The output data, as a UShort array</returns>
  public ushort[] ReadRegArray(IEnumerable<AddrDataPair> addrData, uint numCaptures)
  {
    return this.ReadRegArrayStream(addrData, numCaptures, 1U);
  }

  /// <summary>
  /// Drives the Reset pin low for 10ms, sleeps for 100ms, and then blocks until the ReadyPin is high (500ms timeout)
  /// </summary>
  public void Reset()
  {
    this.PulseDrive(this.ResetPin, 0U, 10.0, 1U);
    Thread.Sleep(100);
    this.PulseWait(this.ReadyPin, 1U, 0U, 500U);
  }

  /// <summary>
  /// This function is not currently implemented. Calling it will throw a NotImplementedException.
  /// </summary>
  public void Start() => throw new NotImplementedException();

  /// <summary>
  /// Attempts to program the selected FX3 board with the application firmware. The FX3 board should be programmed
  /// with the ADI bootloader.
  /// </summary>
  /// <param name="FX3SerialNumber">Serial number of the device being connected to.</param>
  public void Connect(string FX3SerialNumber)
  {
    // ISSUE: variable of a compiler-generated type
    FX3Connection._Closure\u0024__322\u002D0 closure3220_1;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    FX3Connection._Closure\u0024__322\u002D0 closure3220_2 = new FX3Connection._Closure\u0024__322\u002D0(closure3220_1);
    // ISSUE: reference to a compiler-generated field
    closure3220_2.\u0024VB\u0024Me = this;
    CyUSBDevice selectedBoard = (CyUSBDevice) null;
    // ISSUE: reference to a compiler-generated field
    closure3220_2.\u0024VB\u0024Local_boardProgrammed = false;
    bool VerboseMode = false;
    if (this.m_FX3Connected)
      return;
    try
    {
      foreach (CyFX3Device usb in this.m_usbList)
      {
        if (string.Equals(usb.SerialNumber, FX3SerialNumber))
          selectedBoard = (CyUSBDevice) usb;
      }
    }
    finally
    {
      IEnumerator enumerator;
      if (enumerator is IDisposable)
        (enumerator as IDisposable).Dispose();
    }
    if (selectedBoard == null)
    {
      this.SetDefaultValues(this.m_sensorType);
      throw new FX3ProgrammingException("ERROR: Could not find the board selected to connect to. Was it removed?");
    }
    this.m_BoardConnecting = false;
    if (string.Equals(selectedBoard.SerialNumber, FX3SerialNumber))
    {
      if (string.Equals(selectedBoard.FriendlyName, "Analog Devices iSensor FX3 Demonstration Platform"))
      {
        this.m_FX3Connected = true;
      }
      else
      {
        this.m_BoardConnecting = true;
        this.ProgramAppFirmware((CyFX3Device) selectedBoard);
      }
    }
    this.m_ActiveFX3SN = FX3SerialNumber;
    if (this.m_BoardConnecting)
    {
      // ISSUE: variable of a compiler-generated type
      FX3Connection._Closure\u0024__322\u002D1 closure3221_1;
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FX3Connection._Closure\u0024__322\u002D1 closure3221_2 = new FX3Connection._Closure\u0024__322\u002D1(closure3221_1);
      // ISSUE: reference to a compiler-generated field
      closure3221_2.\u0024VB\u0024NonLocal_\u0024VB\u0024Closure_2 = closure3220_2;
      // ISSUE: reference to a compiler-generated field
      closure3221_2.\u0024VB\u0024Local_originalFrame = new DispatcherFrame();
      // ISSUE: reference to a compiler-generated method
      Thread thread = new Thread(new ThreadStart(closure3221_2._Lambda\u0024__0));
      this.m_AppBoardHandle.Reset();
      thread.Start();
      // ISSUE: reference to a compiler-generated field
      Dispatcher.PushFrame(closure3221_2.\u0024VB\u0024Local_originalFrame);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (!closure3221_2.\u0024VB\u0024NonLocal_\u0024VB\u0024Closure_2.\u0024VB\u0024Local_boardProgrammed)
      {
        this.m_BoardConnecting = false;
        throw new FX3ProgrammingException("ERROR: Timeout occurred during the FX3 re-enumeration process");
      }
    }
    this.m_BoardConnecting = false;
    // ISSUE: reference to a compiler-generated field
    closure3220_2.\u0024VB\u0024Local_boardProgrammed = false;
    this.m_ActiveFX3SN = (string) null;
    this.RefreshDeviceList();
    try
    {
      foreach (CyUSBDevice usb in this.m_usbList)
      {
        if (string.Equals(usb.FriendlyName, "Analog Devices iSensor FX3 Demonstration Platform") & string.Equals(usb.SerialNumber, FX3SerialNumber))
        {
          // ISSUE: reference to a compiler-generated field
          closure3220_2.\u0024VB\u0024Local_boardProgrammed = true;
          this.m_ActiveFX3 = (CyFX3Device) usb;
          this.m_ActiveFX3SN = FX3SerialNumber;
          this.m_FX3Connected = true;
          break;
        }
      }
    }
    finally
    {
      IEnumerator enumerator;
      if (enumerator is IDisposable)
        (enumerator as IDisposable).Dispose();
    }
    // ISSUE: reference to a compiler-generated field
    if (!closure3220_2.\u0024VB\u0024Local_boardProgrammed)
      throw new FX3ProgrammingException("ERROR: No application firmware found with the correct serial number");
    if (this.m_ActiveFX3SN == null)
    {
      this.SetDefaultValues(this.m_sensorType);
      throw new FX3ProgrammingException("ERROR: Could not find the board selected to connect to. Was it removed?");
    }
    if (!this.FX3CodeRunningOnTarget())
    {
      this.SetDefaultValues(this.m_sensorType);
      throw new FX3ProgrammingException("ERROR: FX3 Board not successfully connected");
    }
    this.CheckConnectionSpeedOnTarget();
    this.EnumerateEndpointsOnTarget();
    if (!this.CheckEndpointStatus())
    {
      this.SetDefaultValues(this.m_sensorType);
      throw new FX3Exception("ERROR: Unable to configure endpoints");
    }
    this.UpdateWatchdog();
    this.m_ActiveFX3Info = new FX3Board(FX3SerialNumber, DateTime.Now);
    this.m_ActiveFX3Info.SetFirmwareVersion(this.GetFirmwareID());
    this.m_ActiveFX3Info.SetBootloaderVersion(this.m_BootloaderVersion);
    this.GetFX3BoardType();
    this.m_FX3SPIConfig.DataReadyPin = this.m_sensorType == DeviceType.ADcmXL ? (FX3PinObject) this.DIO2 : (FX3PinObject) this.DIO1;
    this.WriteBoardSpiParameters();
    this.SetPinResistorSetting(this.ResetPin, FX3PinResistorSetting.PullUp);
    this.SetI2CBitRate(this.m_i2cbitrate);
    this.SetI2CRetryCount(this.m_i2cRetryCount);
    this.SetBootTimeStamp();
    int boardStatus = (int) this.GetBoardStatus(ref VerboseMode);
    this.m_ActiveFX3Info.SetVerboseMode(VerboseMode);
    this.m_ActiveFX3Info.SetDateTime(this.GetFirmwareBuildDate());
    string versionNumber = this.GetFX3ApiInfo.VersionNumber;
    if (!versionNumber.Equals(this.m_ActiveFX3Info.FirmwareVersionNumber, StringComparison.OrdinalIgnoreCase))
      throw new FX3Exception($"ERROR: FX3 Api version {versionNumber} requires matching firmware version. Supplied firmware file is version {this.m_ActiveFX3Info.FirmwareVersionNumber}");
  }

  /// <summary>
  /// This function sends a reset command to the specified FX3 board, or does nothing if no board is connected
  /// </summary>
  public void Disconnect()
  {
    if (!this.m_FX3Connected)
      return;
    this.m_disconnectTimer = new Stopwatch();
    this.m_disconnectTimer.Start();
    this.m_disconnectedFX3SN = this.m_ActiveFX3SN;
    this.m_disconnectEvents = 0;
    this.ResetFX3Firmware(this.m_ActiveFX3);
    this.SetDefaultValues(this.m_sensorType);
  }

  /// <summary>
  /// Overload of Disconnect which lets you specify the FX3 serial number to disconnect. Returns a boolean
  /// indicating if the board was disconnected.
  /// </summary>
  /// <param name="FX3SerialNumber">Serial number of board to disconnect from</param>
  /// <returns>Success status of the board disconnect operation</returns>
  public bool Disconnect(string FX3SerialNumber)
  {
    if (Operators.CompareString(FX3SerialNumber, this.m_ActiveFX3SN, false) == 0)
    {
      bool fx3Connected = this.m_FX3Connected;
      this.Disconnect();
      return fx3Connected;
    }
    this.RefreshDeviceList();
    bool flag = false;
    try
    {
      foreach (CyFX3Device usb in this.m_usbList)
      {
        if (Operators.CompareString(usb.SerialNumber, FX3SerialNumber, false) == 0 & Operators.CompareString(usb.FriendlyName, "Analog Devices iSensor FX3 Demonstration Platform", false) == 0)
        {
          flag = true;
          this.ResetFX3Firmware(usb);
          break;
        }
      }
    }
    finally
    {
      IEnumerator enumerator;
      if (enumerator is IDisposable)
        (enumerator as IDisposable).Dispose();
    }
    return flag;
  }

  /// <summary>Get the FX3 board type from the connected firmware.</summary>
  private void GetFX3BoardType()
  {
    if (!this.m_FX3Connected)
      return;
    this.ConfigureControlEndpoint(USBCommands.ADI_GET_BOARD_TYPE, false);
    byte[] Buf = new byte[22];
    if (!this.XferControlData(ref Buf, 22, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer to get firmware type and pin mapping failed.");
    this.m_ActiveFX3Info.SetBoardType((FX3BoardType) checked ((int) BitConverter.ToUInt32(Buf, 0)));
    this.RESET_PIN = BitConverter.ToUInt16(Buf, 4);
    this.DIO1_PIN = BitConverter.ToUInt16(Buf, 6);
    this.DIO2_PIN = BitConverter.ToUInt16(Buf, 8);
    this.DIO3_PIN = BitConverter.ToUInt16(Buf, 10);
    this.DIO4_PIN = BitConverter.ToUInt16(Buf, 12);
    this.FX3_GPIO1_PIN = BitConverter.ToUInt16(Buf, 14);
    this.FX3_GPIO2_PIN = BitConverter.ToUInt16(Buf, 16 /*0x10*/);
    this.FX3_GPIO3_PIN = BitConverter.ToUInt16(Buf, 18);
    this.FX3_GPIO4_PIN = BitConverter.ToUInt16(Buf, 20);
  }

  /// <summary>
  /// This function is used to wait for an FX3 to be programmed with the ADI bootloader. In general, the programming model would go as follows,
  /// to connect and program the first board attached:
  /// 
  /// Dim myFX3 as FX3Connection = New FX3Connection(firmwarepath, bootloaderpath, devicetype)
  /// If Not myFX3.WaitForBoard(10) Then
  ///     Msgbox("No boards found")
  ///     Exit Sub
  /// End If
  /// myFX3.Connect(myFX3.AvailableFX3s(0))
  /// </summary>
  /// <param name="TimeoutInSeconds">The timeout to wait for a board to connect, in seconds</param>
  /// <returns>If there is a board available (false indicates timeout occurred)</returns>
  public bool WaitForBoard(int TimeoutInSeconds)
  {
    // ISSUE: variable of a compiler-generated type
    FX3Connection._Closure\u0024__326\u002D0 closure3260_1;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    FX3Connection._Closure\u0024__326\u002D0 closure3260_2 = new FX3Connection._Closure\u0024__326\u002D0(closure3260_1);
    // ISSUE: reference to a compiler-generated field
    closure3260_2.\u0024VB\u0024Me = this;
    // ISSUE: reference to a compiler-generated field
    closure3260_2.\u0024VB\u0024Local_TimeoutInSeconds = TimeoutInSeconds;
    // ISSUE: reference to a compiler-generated field
    closure3260_2.\u0024VB\u0024Local_boardattached = false;
    // ISSUE: reference to a compiler-generated field
    closure3260_2.\u0024VB\u0024Local_waitTime = 3.0;
    // ISSUE: reference to a compiler-generated field
    if (closure3260_2.\u0024VB\u0024Local_TimeoutInSeconds < 0)
    {
      // ISSUE: reference to a compiler-generated field
      throw new FX3ConfigurationException($"ERROR: Invalid timeout of {closure3260_2.\u0024VB\u0024Local_TimeoutInSeconds.ToString()} seconds when waiting for bootloader");
    }
    if (!Information.IsNothing((object) this.m_usbList))
    {
      try
      {
        foreach (USBDevice usb in this.m_usbList)
        {
          if (Operators.CompareString(usb.FriendlyName, "Analog Devices iSensor FX3 Bootloader", false) == 0)
            return true;
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
    }
    else
      this.RefreshDeviceList();
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    int num1 = checked ((int) Math.Round(Math.Floor(unchecked ((double) closure3260_2.\u0024VB\u0024Local_TimeoutInSeconds / closure3260_2.\u0024VB\u0024Local_waitTime))));
    this.m_BootloaderBoardHandle.Reset();
    int num2 = 0;
    // ISSUE: reference to a compiler-generated field
    while (num2 < num1 & !closure3260_2.\u0024VB\u0024Local_boardattached)
    {
      // ISSUE: reference to a compiler-generated field
      closure3260_2.\u0024VB\u0024Local_originalFrame = new DispatcherFrame();
      // ISSUE: reference to a compiler-generated method
      new Thread(new ThreadStart(closure3260_2._Lambda\u0024__0)).Start();
      // ISSUE: reference to a compiler-generated field
      Dispatcher.PushFrame(closure3260_2.\u0024VB\u0024Local_originalFrame);
      this.RefreshDeviceList();
      try
      {
        foreach (USBDevice usb in this.m_usbList)
        {
          if (Operators.CompareString(usb.FriendlyName, "Analog Devices iSensor FX3 Bootloader", false) == 0)
          {
            // ISSUE: reference to a compiler-generated field
            closure3260_2.\u0024VB\u0024Local_boardattached = true;
          }
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      checked { ++num2; }
    }
    // ISSUE: reference to a compiler-generated field
    if (!closure3260_2.\u0024VB\u0024Local_boardattached)
    {
      // ISSUE: reference to a compiler-generated field
      closure3260_2.\u0024VB\u0024Local_originalFrame = new DispatcherFrame();
      // ISSUE: reference to a compiler-generated method
      new Thread(new ThreadStart(closure3260_2._Lambda\u0024__1)).Start();
      // ISSUE: reference to a compiler-generated field
      Dispatcher.PushFrame(closure3260_2.\u0024VB\u0024Local_originalFrame);
    }
    // ISSUE: reference to a compiler-generated field
    return closure3260_2.\u0024VB\u0024Local_boardattached;
  }

  /// <summary>
  /// Property which returns the active FX3 board. Returns nothing if there is not a board connected.
  /// </summary>
  /// <returns>Returns active FX3 board if enumeration has been completed. Returns nothing otherwise.</returns>
  public FX3Board ActiveFX3 => this.m_FX3Connected ? this.m_ActiveFX3Info : (FX3Board) null;

  /// <summary>Property which returns the serial number of the active FX3 board.</summary>
  /// <returns>Returns the serial number of the active FX3 device.</returns>
  public string ActiveFX3SerialNumber
  {
    get => this.m_ActiveFX3SN;
    set => this.m_ActiveFX3SN = value;
  }

  /// <summary>
  /// Property which returns a list of the serial numbers of all FX3 boards running the ADI bootloader
  /// </summary>
  /// <returns>All detected FX3 boards.</returns>
  public List<string> AvailableFX3s
  {
    get
    {
      List<string> availableFx3s = new List<string>();
      this.RefreshDeviceList();
      try
      {
        foreach (CyFX3Device usb in this.m_usbList)
        {
          if (Operators.CompareString(usb.FriendlyName, "Analog Devices iSensor FX3 Bootloader", false) == 0)
            availableFx3s.Add(usb.SerialNumber);
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      return availableFx3s;
    }
  }

  /// <summary>
  /// Property which returns a list of the serial numbers of all FX3 boards currently in use, running the application firmware.
  /// </summary>
  /// <returns>The list of board serial numbers</returns>
  public List<string> BusyFX3s
  {
    get
    {
      List<string> busyFx3s = new List<string>();
      this.RefreshDeviceList();
      try
      {
        foreach (CyFX3Device usb in this.m_usbList)
        {
          if (Operators.CompareString(usb.FriendlyName, "Analog Devices iSensor FX3 Demonstration Platform", false) == 0)
            busyFx3s.Add(usb.SerialNumber);
        }
      }
      finally
      {
        IEnumerator enumerator;
        if (enumerator is IDisposable)
          (enumerator as IDisposable).Dispose();
      }
      return busyFx3s;
    }
  }

  /// <summary>Property which reads the firmware version from the FX3</summary>
  /// <returns>The firmware version, as a string</returns>
  public string GetFirmwareVersion => this.GetFirmwareID();

  /// <summary>Read-only property to get the serial number of the active FX3 board</summary>
  /// <returns>The current serial number, as a string</returns>
  public string GetTargetSerialNumber => this.GetSerialNumber();

  /// <summary>
  /// Initializes the interrupt handlers for connecting/disconnecting boards and forces an FX3 list refresh
  /// </summary>
  private void InitBoardList()
  {
    USBDeviceList usbDeviceList = new USBDeviceList((byte) 1);
    usbDeviceList.DeviceRemoved += new EventHandler(this.usbDevices_DeviceRemoved);
    usbDeviceList.DeviceAttached += new EventHandler(this.usbDevices_DeviceAttached);
    this.RefreshDeviceList();
  }

  /// <summary>Handles connect events generated by the Cypress USB library</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void usbDevices_DeviceAttached(object sender, EventArgs e)
  {
    this.CheckConnectEvent(e as USBEventArgs);
    this.RefreshDeviceList();
  }

  /// <summary>Handles disconnect events generated by the cypress USB library</summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  private void usbDevices_DeviceRemoved(object sender, EventArgs e)
  {
    this.CheckDisconnectEvent(e as USBEventArgs);
  }

  /// <summary>
  /// This function checks the event arguments when a USB disconnect occurs. If the FX3 which was
  /// disconnected is marked as the active device, this function attempts to gracefully update the
  /// interface state to prevent application lockup from accessing a disconnected board.
  /// </summary>
  /// <param name="usbEvent">The event to handle</param>
  private void CheckDisconnectEvent(USBEventArgs usbEvent)
  {
    if (Information.IsNothing((object) this.m_ActiveFX3) || !(Operators.CompareString(usbEvent.FriendlyName, "Analog Devices iSensor FX3 Demonstration Platform", false) == 0 & Operators.CompareString(usbEvent.SerialNum, this.m_ActiveFX3SN, false) == 0))
      return;
    this.SetDefaultValues(this.m_sensorType);
    this.RefreshDeviceList();
    // ISSUE: reference to a compiler-generated field
    FX3Connection.UnexpectedDisconnectEventHandler unexpectedDisconnectEvent = this.UnexpectedDisconnectEvent;
    if (unexpectedDisconnectEvent != null)
      unexpectedDisconnectEvent(usbEvent.SerialNum);
  }

  /// <summary>
  /// This function parses connect events. If the board connecting is running the ADI bootloader,
  /// and has a serial number which matches that of the most recently disconnected FX3, a disconnect
  /// finished event is raised. This allows GUIs or applications up the stack to better manage their
  /// event flow (rather than blocking in a disconnect call).
  /// </summary>
  /// <param name="usbEvent">The event to handle</param>
  private void CheckConnectEvent(USBEventArgs usbEvent)
  {
    if (Operators.CompareString(usbEvent.SerialNum, this.m_ActiveFX3SN, false) == 0 & !Information.IsNothing((object) this.m_ActiveFX3SN))
      this.m_AppBoardHandle.Set();
    else if (Operators.CompareString(usbEvent.SerialNum, "", false) == 0 & this.m_BoardConnecting)
    {
      this.m_AppBoardHandle.Set();
    }
    else
    {
      if (Operators.CompareString(usbEvent.FriendlyName, "Analog Devices iSensor FX3 Bootloader", false) == 0)
        this.m_BootloaderBoardHandle.Set();
      if (Information.IsNothing((object) this.m_disconnectedFX3SN))
        return;
      if (Operators.CompareString(usbEvent.FriendlyName, "Analog Devices iSensor FX3 Bootloader", false) == 0 & Operators.CompareString(usbEvent.SerialNum, this.m_disconnectedFX3SN, false) == 0)
      {
        // ISSUE: reference to a compiler-generated field
        FX3Connection.DisconnectFinishedEventHandler disconnectFinishedEvent = this.DisconnectFinishedEvent;
        if (disconnectFinishedEvent != null)
          disconnectFinishedEvent(this.m_disconnectedFX3SN, checked ((int) this.m_disconnectTimer.ElapsedMilliseconds));
        this.m_disconnectTimer.Reset();
        this.m_disconnectedFX3SN = (string) null;
      }
      else
      {
        try
        {
          foreach (CyFX3Device usb in this.m_usbList)
          {
            if (Operators.CompareString(usb.FriendlyName, "Analog Devices iSensor FX3 Bootloader", false) == 0 & Operators.CompareString(usb.SerialNumber, this.m_disconnectedFX3SN, false) == 0)
            {
              // ISSUE: reference to a compiler-generated field
              FX3Connection.DisconnectFinishedEventHandler disconnectFinishedEvent = this.DisconnectFinishedEvent;
              if (disconnectFinishedEvent != null)
                disconnectFinishedEvent(this.m_disconnectedFX3SN, checked ((int) this.m_disconnectTimer.ElapsedMilliseconds));
              this.m_disconnectTimer.Reset();
              this.m_disconnectedFX3SN = (string) null;
              return;
            }
          }
        }
        finally
        {
          IEnumerator enumerator;
          if (enumerator is IDisposable)
            (enumerator as IDisposable).Dispose();
        }
        if (Operators.CompareString(usbEvent.FriendlyName, "", false) == 0 | Operators.CompareString(usbEvent.SerialNum, "", false) == 0 & !Information.IsNothing((object) this.m_disconnectedFX3SN))
        {
          if (this.m_disconnectEvents == 1)
          {
            // ISSUE: reference to a compiler-generated field
            FX3Connection.DisconnectFinishedEventHandler disconnectFinishedEvent = this.DisconnectFinishedEvent;
            if (disconnectFinishedEvent != null)
              disconnectFinishedEvent(this.m_disconnectedFX3SN, checked ((int) this.m_disconnectTimer.ElapsedMilliseconds));
            this.m_disconnectTimer.Reset();
            this.m_disconnectedFX3SN = (string) null;
            this.m_BootloaderBoardHandle.Set();
          }
          else
            checked { ++this.m_disconnectEvents; }
        }
      }
    }
  }

  /// <summary>
  /// Refreshes the list of FX3 boards connected to the PC and indicates to bootloader programmer thread if any need to be programmed
  /// </summary>
  private void RefreshDeviceList()
  {
    this.m_usbList = new USBDeviceList((byte) 1);
    try
    {
      foreach (CyFX3Device usb in this.m_usbList)
      {
        if (string.Equals(usb.FriendlyName, "Cypress FX3 USB BootLoader Device"))
          this.ProgramFlashFirmware(usb);
        else if (string.Equals(usb.FriendlyName, "Analog Devices iSensor FX3 Bootloader"))
        {
          bool flag;
          try
          {
            string input = usb.Product.Substring(checked (usb.Product.IndexOf("v") + 1));
            flag = Version.Parse(this.m_BootloaderVersion) > Version.Parse(input);
          }
          catch (Exception ex)
          {
            ProjectData.SetProjectError(ex);
            flag = true;
            ProjectData.ClearProjectError();
          }
          if (flag)
            this.ProgramFlashFirmware(usb);
        }
        else if (!string.Equals(usb.FriendlyName, "Analog Devices iSensor FX3 Demonstration Platform"))
        {
          if (Operators.CompareString(usb.FriendlyName, "Cypress FX3 USB BootProgrammer Device", false) == 0)
            this.BootloaderQueue.Add(usb);
          else
            this.ProgramFlashFirmware(usb);
        }
      }
    }
    finally
    {
      IEnumerator enumerator;
      if (enumerator is IDisposable)
        (enumerator as IDisposable).Dispose();
    }
  }

  /// <summary>
  /// This function runs in a separate thread from the main application. When a new, un-programmed board
  /// is connected to the system, the device identifier is placed in a queue, indicating to this thread
  /// that a new board needs to be programmed with the ADI bootloader.
  /// </summary>
  private void ProgramBootloaderThread()
  {
    while (true)
    {
      CyFX3Device SelectedBoard = this.BootloaderQueue.Take();
      try
      {
        this.ProgramBootloader(SelectedBoard);
      }
      catch (FX3ProgrammingException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        ProjectData.ClearProjectError();
      }
    }
  }

  /// <summary>This function programs the bootloader of a single board</summary>
  /// <param name="selectedBoard">The handle for the FX3 board to be programmed with the ADI bootloader firmware</param>
  private void ProgramBootloader(CyFX3Device SelectedBoard)
  {
    FX3_FWDWNLOAD_ERROR_CODE fwdwnloadErrorCode = Operators.CompareString(SelectedBoard.FriendlyName, "Cypress FX3 USB BootProgrammer Device", false) == 0 ? SelectedBoard.DownloadFw(this.BootloaderPath, FX3_FWDWNLOAD_MEDIA_TYPE.I2CE2PROM) : throw new FX3ProgrammingException("ERROR: Selected FX3 is not in flash programmer mode.");
    if (fwdwnloadErrorCode != 0)
      throw new FX3ProgrammingException("ERROR: Bootloader download failed with code " + fwdwnloadErrorCode.ToString());
    byte[] numArray = new byte[4];
    SelectedBoard.ControlEndPt.ReqCode = (byte) 177;
    SelectedBoard.ControlEndPt.ReqType = (byte) 64 /*0x40*/;
    SelectedBoard.ControlEndPt.Target = (byte) 0;
    SelectedBoard.ControlEndPt.Value = (ushort) 0;
    SelectedBoard.ControlEndPt.Index = (ushort) 0;
    SelectedBoard.ControlEndPt.Direction = (byte) 0;
    CyControlEndPoint controlEndPt = SelectedBoard.ControlEndPt;
    ref byte[] local1 = ref numArray;
    int num = 4;
    ref int local2 = ref num;
    controlEndPt.XferData(ref local1, ref local2);
  }

  /// <summary>
  /// This function programs a single board running the ADI bootloader with the ADI application firmware.
  /// </summary>
  /// <param name="selectedBoard">The handle for the board to be programmed with the ADI application firmware</param>
  private void ProgramAppFirmware(CyFX3Device selectedBoard)
  {
    Stopwatch stopwatch = new Stopwatch();
    string serialNumber = selectedBoard.SerialNumber;
    FX3_FWDWNLOAD_ERROR_CODE fwdwnloadErrorCode = selectedBoard.DownloadFw(this.FirmwarePath, FX3_FWDWNLOAD_MEDIA_TYPE.RAM);
    if (fwdwnloadErrorCode != 0)
      throw new FX3ProgrammingException("ERROR: Application firmware download failed with code " + fwdwnloadErrorCode.ToString());
  }

  private void ProgramFlashFirmware(CyFX3Device SelectedBoard)
  {
    int num = (int) SelectedBoard.DownloadFw(this.FlashProgrammerPath, FX3_FWDWNLOAD_MEDIA_TYPE.RAM);
  }

  /// <summary>Function which checks if the FX3 is connected and programmed</summary>
  /// <returns>A boolean indicating if the board is programmed</returns>
  public bool FX3CodeRunningOnTarget()
  {
    return this.m_FX3Connected && string.Equals(this.m_ActiveFX3.FriendlyName, "Analog Devices iSensor FX3 Demonstration Platform") && string.Equals(this.m_ActiveFX3SN, this.GetSerialNumber()) && this.GetFirmwareID().IndexOf("FX3") != -1;
  }

  /// <summary>
  /// The path to the firmware .img file. Needs to be set before the FX3 can be programmed
  /// </summary>
  /// <returns>A string, representing the path</returns>
  public string FirmwarePath
  {
    get => this.m_FirmwarePath;
    set
    {
      bool flag = this.IsFirmwarePathValid(ref value);
      if (!flag && Directory.Exists(value))
      {
        value = Path.Combine(value, "FX3_Firmware.img");
        flag = this.IsFirmwarePathValid(ref value);
      }
      if (flag)
      {
        this.m_FirmwarePath = value;
      }
      else
      {
        this.m_FirmwarePath = "";
        throw new FX3ConfigurationException("ERROR: Invalid application firmware path provided: " + value);
      }
    }
  }

  /// <summary>
  /// Set/get the blink USB bootloader firmware .img file used for multi-board identification
  /// </summary>
  /// <returns>A string representing the path to the firmware on the user machine</returns>
  public string BootloaderPath
  {
    get => this.m_BlinkFirmwarePath;
    set
    {
      bool flag = this.IsFirmwarePathValid(ref value);
      if (!flag && Directory.Exists(value))
      {
        value = Path.Combine(value, "boot_fw.img");
        flag = this.IsFirmwarePathValid(ref value);
      }
      if (flag)
      {
        this.m_BlinkFirmwarePath = value;
        this.m_BootloaderVersion = this.GetBootloaderVersion(value);
      }
      else
      {
        this.m_BootloaderVersion = "Not Set";
        this.m_BlinkFirmwarePath = "";
        throw new FX3ConfigurationException("ERROR: Invalid bootloader firmware path provided: " + value);
      }
    }
  }

  /// <summary>
  /// Path to the programmer firmware which is loaded in RAM to allow flashing the EEPROM with the bootloader.
  /// </summary>
  /// <returns></returns>
  public string FlashProgrammerPath
  {
    get => this.m_FlashProgrammerPath;
    set
    {
      bool flag = this.IsFirmwarePathValid(ref value);
      if (!flag && Directory.Exists(value))
      {
        value = Path.Combine(value, "USBFlashProg.img");
        flag = this.IsFirmwarePathValid(ref value);
      }
      if (flag)
      {
        this.m_FlashProgrammerPath = value;
      }
      else
      {
        this.m_FlashProgrammerPath = "";
        throw new FX3ConfigurationException("ERROR: Invalid flash programmer firmware path provided: " + value);
      }
    }
  }

  /// <summary>Checks the boot status of the FX3 board by sending a vendor request</summary>
  /// <returns>The current connection status</returns>
  public string GetBootStatus
  {
    get
    {
      return this.FX3CodeRunningOnTarget() ? "Application firmware running on FX3" : "Application firmware Not running on FX3";
    }
  }

  /// <summary>Checks if there is a Cypress FX3 USB device connected to the system</summary>
  /// <returns>A boolean indicating if there is an FX3 attached</returns>
  public bool FX3BoardAttached
  {
    get
    {
      this.RefreshDeviceList();
      return this.m_usbList.Count != 0;
    }
  }

  /// <summary>
  /// Send a reset command to the FX3 firmware. This command works for either the application or bootloader firmware.
  /// </summary>
  /// <param name="BoardHandle">Handle of the board to be reset.</param>
  private void ResetFX3Firmware(CyFX3Device BoardHandle)
  {
    byte[] numArray = new byte[4];
    BoardHandle.ControlEndPt.ReqCode = (byte) 177;
    BoardHandle.ControlEndPt.ReqType = (byte) 64 /*0x40*/;
    BoardHandle.ControlEndPt.Target = (byte) 2;
    BoardHandle.ControlEndPt.Value = (ushort) 0;
    BoardHandle.ControlEndPt.Index = (ushort) 0;
    BoardHandle.ControlEndPt.Direction = (byte) 0;
    CyControlEndPoint controlEndPt = BoardHandle.ControlEndPt;
    ref byte[] local1 = ref numArray;
    int num = 4;
    ref int local2 = ref num;
    controlEndPt.XferData(ref local1, ref local2);
  }

  /// <summary>
  /// Looks for and resets boards in application mode. Should only be called at program start, after InitBoardList()
  /// Note: Should not be used if running multiple instances of the GUI.
  /// </summary>
  /// <returns>The number of boards running the application firmware which were reset</returns>
  public int ResetAllFX3s()
  {
    int num = 0;
    this.RefreshDeviceList();
    try
    {
      foreach (CyFX3Device usb in this.m_usbList)
      {
        if (string.Equals(usb.FriendlyName, "Analog Devices iSensor FX3 Demonstration Platform"))
        {
          this.ResetFX3Firmware(usb);
          checked { ++num; }
        }
      }
    }
    finally
    {
      IEnumerator enumerator;
      if (enumerator is IDisposable)
        (enumerator as IDisposable).Dispose();
    }
    return num;
  }

  /// <summary>
  /// Checks to see if a provided firmware path is valid. Throws exception if it is not.
  /// </summary>
  /// <param name="Path">The firmware path to check</param>
  /// <returns>A boolean indicating if the firmware path is valid</returns>
  private bool IsFirmwarePathValid(ref string Path)
  {
    return Path.Length > 4 && Operators.CompareString(Path.Substring(checked (Path.Length - 4), 4), ".img", false) == 0 && File.Exists(Path);
  }

  /// <summary>
  /// Performs a data transfer on the control endpoint with a check to see if the transaction times out
  /// </summary>
  /// <param name="Buf">The buffer to transfer</param>
  /// <param name="NumBytes">The number of bytes to transfer</param>
  /// <param name="Timeout">The timeout time (in milliseconds)</param>
  /// <returns>Returns a boolean indicating if the transfer timed out or not</returns>
  private bool XferControlData(ref byte[] Buf, int NumBytes, int Timeout)
  {
    Stopwatch stopwatch = new Stopwatch();
    bool flag1 = true;
    if (!this.m_ControlMutex.WaitOne(Timeout))
    {
      Console.WriteLine("Could not acquire control endpoint mutex lock");
      return false;
    }
    this.FX3ControlEndPt = this.m_ActiveFX3.ControlEndPt;
    stopwatch.Start();
    flag1 = this.FX3ControlEndPt.XferData(ref Buf, ref NumBytes);
    stopwatch.Stop();
    bool flag2 = stopwatch.ElapsedMilliseconds <= (long) Timeout;
    this.m_ControlMutex.ReleaseMutex();
    return flag2;
  }

  /// <summary>
  /// Validates that the control endpoint is enumerated and configures it with some default values
  /// </summary>
  /// <param name="Reqcode">The vendor command reqcode to provide</param>
  /// <param name="toDevice">Whether the transaction is DIR_TO_DEVICE (true) or DIR_FROM_DEVICE(false)</param>
  private void ConfigureControlEndpoint(USBCommands ReqCode, bool ToDevice)
  {
    if (Information.IsNothing((object) this.m_ActiveFX3))
      throw new FX3Exception("ERROR: Attempted to configure control endpoint without FX3 being enumerated.");
    if (!this.m_FX3Connected)
      throw new FX3Exception("ERROR: Attempted to configure control endpoint without FX3 connected.");
    this.FX3ControlEndPt = this.m_ActiveFX3.ControlEndPt;
    this.FX3ControlEndPt.ReqCode = checked ((byte) (uint) ReqCode);
    this.FX3ControlEndPt.ReqType = (byte) 64 /*0x40*/;
    this.FX3ControlEndPt.Target = (byte) 0;
    this.FX3ControlEndPt.Value = (ushort) 0;
    this.FX3ControlEndPt.Index = (ushort) 0;
    if (ToDevice)
      this.FX3ControlEndPt.Direction = (byte) 0;
    else
      this.FX3ControlEndPt.Direction = (byte) 128 /*0x80*/;
  }

  /// <summary>Gets the current firmware ID from the FX3</summary>
  /// <returns>Returns the firmware ID, as a string</returns>
  private string GetFirmwareID()
  {
    byte[] Buf = new byte[32 /*0x20*/];
    this.ConfigureControlEndpoint(USBCommands.ADI_FIRMWARE_ID_CHECK, false);
    if (!this.XferControlData(ref Buf, 32 /*0x20*/, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer timed out while reading firmware ID");
    string firmwareId;
    try
    {
      string str = Encoding.UTF8.GetString(Buf);
      firmwareId = str.Substring(0, Math.Max(0, str.IndexOf("\0")));
    }
    catch (Exception ex)
    {
      ProjectData.SetProjectError(ex);
      throw new FX3Exception("ERROR: Parsing firmware ID failed", ex);
    }
    return firmwareId;
  }

  /// <summary>Gets the serial number of the target FX3 using the control endpoint</summary>
  /// <returns>The unique FX3 serial number</returns>
  private string GetSerialNumber()
  {
    byte[] Buf = new byte[32 /*0x20*/];
    this.ConfigureControlEndpoint(USBCommands.ADI_SERIAL_NUMBER_CHECK, false);
    if (!this.XferControlData(ref Buf, 32 /*0x20*/, 2000))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer timed out while reading FX3 serial number");
    string serialNumber;
    try
    {
      serialNumber = Encoding.Unicode.GetString(Buf);
    }
    catch (Exception ex)
    {
      ProjectData.SetProjectError(ex);
      throw new FX3Exception("ERROR: Parsing FX3 serial number failed", ex);
    }
    return serialNumber;
  }

  /// <summary>Checks that all the endpoints are properly enumerated</summary>
  /// <returns>A boolean indicating if the endpoints are properly enumerated</returns>
  private bool CheckEndpointStatus()
  {
    return this.FX3ControlEndPt != null && this.StreamingEndPt != null && this.DataInEndPt != null && this.DataOutEndPt != null;
  }

  /// <summary>Resets all the currently configured endpoints on the FX3.</summary>
  private void ResetEndpoints()
  {
    if (this.m_ActiveFX3 == null)
      return;
    CyUSBEndPoint[] endPoints = this.m_ActiveFX3.EndPoints;
    int index = 0;
    while (index < endPoints.Length)
    {
      endPoints[index].Reset();
      checked { ++index; }
    }
  }

  /// <summary>Enumerates all the FX3 endpoints used</summary>
  private void EnumerateEndpointsOnTarget()
  {
    CyUSBEndPoint[] endPoints = this.m_ActiveFX3.EndPoints;
    int index = 0;
    while (index < endPoints.Length)
    {
      CyUSBEndPoint cyUsbEndPoint = endPoints[index];
      if (cyUsbEndPoint.Address == (byte) 1)
        this.DataOutEndPt = cyUsbEndPoint;
      else if (cyUsbEndPoint.Address == (byte) 129)
        this.StreamingEndPt = cyUsbEndPoint;
      else if (cyUsbEndPoint.Address == (byte) 130)
        this.DataInEndPt = cyUsbEndPoint;
      checked { ++index; }
    }
    this.FX3ControlEndPt = this.m_ActiveFX3.ControlEndPt;
  }

  /// <summary>
  /// Checks that the board is enumerated and connected via USB 2.0 or 3.0. Throws general exceptions for an invalid speed.
  /// </summary>
  private void CheckConnectionSpeedOnTarget()
  {
    if (Information.IsNothing((object) this.m_ActiveFX3))
      throw new FX3Exception("ERROR: FX3 Board not enumerated");
    if (!(this.m_ActiveFX3.bHighSpeed | this.m_ActiveFX3.bSuperSpeed))
    {
      this.m_ActiveFX3 = (CyFX3Device) null;
      throw new FX3Exception("ERROR: FX3 must be connected with USB 2.0 or better");
    }
  }

  /// <summary>Read bootloader image and pull the version number</summary>
  /// <param name="Path">Path to the image</param>
  /// <returns>The version image string</returns>
  private string GetBootloaderVersion(string Path)
  {
    string str1;
    string bootloaderVersion;
    try
    {
      str1 = File.ReadAllText(Path);
    }
    catch (Exception ex)
    {
      ProjectData.SetProjectError(ex);
      bootloaderVersion = "";
      ProjectData.ClearProjectError();
      goto label_6;
    }
    string str2 = str1.Replace(Conversions.ToString(Convert.ToChar(0)), "");
    string source = "FX3 Bootloader v";
    int num = str2.IndexOf(source);
    if (num == -1)
    {
      bootloaderVersion = "";
    }
    else
    {
      int startIndex = checked (num + source.Count<char>());
      bootloaderVersion = str2.Substring(startIndex, 5);
    }
label_6:
    return bootloaderVersion;
  }

  /// <summary>BOOTLOADER FW: Blink the on-board LED</summary>
  /// <param name="SerialNumber">Serial number of the selected board</param>
  public void BootloaderBlinkLED(string SerialNumber)
  {
    byte[] numArray = new byte[4];
    CyFX3Device cyFx3Device = (CyFX3Device) null;
    bool flag = false;
    try
    {
      foreach (object usb in this.m_usbList)
      {
        object objectValue = RuntimeHelpers.GetObjectValue(usb);
        if (string.Equals(((USBDevice) objectValue).SerialNumber, SerialNumber))
        {
          cyFx3Device = (CyFX3Device) objectValue;
          flag = true;
        }
      }
    }
    finally
    {
      IEnumerator enumerator;
      if (enumerator is IDisposable)
        (enumerator as IDisposable).Dispose();
    }
    if (!string.Equals(cyFx3Device.FriendlyName, "Analog Devices iSensor FX3 Bootloader"))
      throw new FX3Exception("ERROR: The selected board is not in bootloader mode");
    if (!flag)
      throw new FX3Exception("ERROR: Could not find the board ID matching the serial number specified");
    this.FX3ControlEndPt = cyFx3Device.ControlEndPt;
    this.FX3ControlEndPt.ReqCode = (byte) 239;
    this.FX3ControlEndPt.ReqType = (byte) 64 /*0x40*/;
    this.FX3ControlEndPt.Target = (byte) 2;
    this.FX3ControlEndPt.Value = (ushort) 0;
    this.FX3ControlEndPt.Index = (ushort) 0;
    this.FX3ControlEndPt.Direction = (byte) 0;
    CyControlEndPoint fx3ControlEndPt = this.FX3ControlEndPt;
    ref byte[] local1 = ref numArray;
    int num = 4;
    ref int local2 = ref num;
    if (!fx3ControlEndPt.XferData(ref local1, ref local2))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer failed when sending LED blink command to bootloader.");
  }

  /// <summary>BOOTLOADER FW: Turn off the LED</summary>
  /// <param name="SerialNumber">Serial number of the selected board</param>
  public void BootloaderTurnOffLED(string SerialNumber)
  {
    byte[] numArray = new byte[4];
    CyFX3Device cyFx3Device = (CyFX3Device) null;
    bool flag = false;
    try
    {
      foreach (object usb in this.m_usbList)
      {
        object objectValue = RuntimeHelpers.GetObjectValue(usb);
        if (string.Equals(((USBDevice) objectValue).SerialNumber, SerialNumber))
        {
          cyFx3Device = (CyFX3Device) objectValue;
          flag = true;
        }
      }
    }
    finally
    {
      IEnumerator enumerator;
      if (enumerator is IDisposable)
        (enumerator as IDisposable).Dispose();
    }
    if (!string.Equals(cyFx3Device.FriendlyName, "Analog Devices iSensor FX3 Bootloader"))
      throw new FX3Exception("ERROR: The selected board is not in bootloader mode");
    if (!flag)
      throw new FX3Exception("ERROR: Could not find the board ID matching the serial number specified");
    this.FX3ControlEndPt = cyFx3Device.ControlEndPt;
    this.FX3ControlEndPt.ReqCode = (byte) 237;
    this.FX3ControlEndPt.ReqType = (byte) 64 /*0x40*/;
    this.FX3ControlEndPt.Target = (byte) 2;
    this.FX3ControlEndPt.Value = (ushort) 0;
    this.FX3ControlEndPt.Index = (ushort) 0;
    this.FX3ControlEndPt.Direction = (byte) 0;
    CyControlEndPoint fx3ControlEndPt = this.FX3ControlEndPt;
    ref byte[] local1 = ref numArray;
    int num = 4;
    ref int local2 = ref num;
    if (!fx3ControlEndPt.XferData(ref local1, ref local2))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer failed when sending LED off command to bootloader.");
  }

  /// <summary>BOOTLOADER FW: Turn on the LED</summary>
  /// <param name="SerialNumber">Serial number of the selected board</param>
  public void BootloaderTurnOnLED(string SerialNumber)
  {
    byte[] numArray = new byte[4];
    CyFX3Device cyFx3Device = (CyFX3Device) null;
    bool flag = false;
    try
    {
      foreach (object usb in this.m_usbList)
      {
        object objectValue = RuntimeHelpers.GetObjectValue(usb);
        if (string.Equals(((USBDevice) objectValue).SerialNumber, SerialNumber))
        {
          cyFx3Device = (CyFX3Device) objectValue;
          flag = true;
        }
      }
    }
    finally
    {
      IEnumerator enumerator;
      if (enumerator is IDisposable)
        (enumerator as IDisposable).Dispose();
    }
    if (!string.Equals(cyFx3Device.FriendlyName, "Analog Devices iSensor FX3 Bootloader"))
      throw new FX3Exception("ERROR: The selected board is not in bootloader mode");
    if (!flag)
      throw new FX3Exception("ERROR: Could not find the board ID matching the serial number specified");
    this.FX3ControlEndPt = cyFx3Device.ControlEndPt;
    this.FX3ControlEndPt.ReqCode = (byte) 236;
    this.FX3ControlEndPt.ReqType = (byte) 64 /*0x40*/;
    this.FX3ControlEndPt.Target = (byte) 2;
    this.FX3ControlEndPt.Value = (ushort) 0;
    this.FX3ControlEndPt.Index = (ushort) 0;
    this.FX3ControlEndPt.Direction = (byte) 0;
    CyControlEndPoint fx3ControlEndPt = this.FX3ControlEndPt;
    ref byte[] local1 = ref numArray;
    int num = 4;
    ref int local2 = ref num;
    if (!fx3ControlEndPt.XferData(ref local1, ref local2))
      throw new FX3CommunicationException("ERROR: Control endpoint transfer failed when sending LED on command to bootloader.");
  }

  /// <summary>
  /// This function performs a single bi-directional 32 bit SPI transaction. If DrActive is set to false the transfer is performed asynchronously. If DrActive is set to true,
  /// the transfer should wait until a data ready condition (determined by DrPin and DrPolarity) is true.
  /// </summary>
  /// <param name="WriteData">The 32 bit data to be send to the slave on the MOSI line</param>
  /// <returns>The 32 bit data sent to the master over the MISO line during the SPI transaction</returns>
  public uint Transfer(uint WriteData)
  {
    byte[] Buf = new byte[8];
    this.ConfigureControlEndpoint(USBCommands.ADI_TRANSFER_BYTES, false);
    this.FX3ControlEndPt.Index = checked ((ushort) ((WriteData & 4294901760U) >> 16 /*0x10*/));
    this.FX3ControlEndPt.Value = checked ((ushort) (unchecked ((int) WriteData) & (int) ushort.MaxValue));
    uint num = this.XferControlData(ref Buf, 8, 2000) ? BitConverter.ToUInt32(Buf, 4) : throw new FX3CommunicationException("ERROR: Timeout during control endpoint transfer for SPI byte transfer");
    uint uint32 = BitConverter.ToUInt32(Buf, 0);
    if (uint32 != 0)
      throw new FX3BadStatusException("ERROR: Bad read command - " + uint32.ToString("X4"));
    return num;
  }

  /// <summary>
  /// This function performs an array bi-directional SPI transfer. WriteData.Count() total SPI transfers are performed. If DrActive is set to true, the transfer should wait
  /// until a data ready condition (determined by DrPin and DrPolarity) is true, and then perform all SPI transfers. If DrActive is false it is performed asynchronously.
  /// </summary>
  /// <param name="WriteData">The data to be written to the slave on the MOSI line in each SPI transaction. The total number of transfers performed is determined by the size of WriteData.</param>
  /// <returns>The data received from the slave device on the MISO line, as an array</returns>
  public uint[] TransferArray(IEnumerable<uint> WriteData) => this.TransferArray(WriteData, 1U, 1U);

  /// <summary>
  /// This function performs an array bi-directional SPI transfer. This overload transfers all the data in WriteData numCaptures times. The total
  /// number of SPI words transfered is WriteData.Count() * numCaptures.
  /// If DrActive is set to true, the transfer should wait until a data ready condition (determined by DrPin and DrPolarity) is true, and
  /// then perform all SPI transfers. If DrActive is false it is performed asynchronously.
  /// </summary>
  /// <param name="WriteData">The data to be written to the slave on the MOSI line in each SPI transaction.</param>
  /// <param name="numCaptures">The number of transfers of the WriteData array performed.</param>
  /// <returns>The data received from the slave device on the MISO line, as an array. The total size is WriteData.Count() * numCaptures</returns>
  public uint[] TransferArray(IEnumerable<uint> WriteData, uint numCaptures)
  {
    return this.TransferArray(WriteData, numCaptures, 1U);
  }

  /// <summary>
  /// This function performs an array bi-directional SPI transfer. If DrActive is set to true, this overload transfers all the data in WriteData
  /// numCaptures times per data ready condition being met. It captures data from numBuffers data ready signals. If DrActive is set to false, all the
  /// transfers are performed asynchronously. The total number of SPI transfers is WriteData.Count()*numCaptures*numBuffers.
  /// 
  /// The following pseduo-code snippet would perform 400 total SPI transfers, across 100 data ready conditions.
  /// 
  /// MOSI = {0x1234, 0x5678}
  /// myISpi32.DrActive = True
  /// MISO = myISpi32.TransferArray(MOSI, 2, 100)
  /// 
  /// During the transfers, the SPI bus would look like the following:
  /// 
  /// MOSI: ---(0x1234)---(0x5678)---(0x1234)---(0x5678)-----------------(0x1234)---(0x5678)---(0x1234)---(0x5678)--...-----(0x1234)-----(0x5678)-----(0x1234)-----(0x5678)--
  /// MISO:----MISO(0)----MISO(1)----MISO(2)----MISO(3)------------------MISO(4)----MISO(5)----MISO(6)----MISO(7)---...-----MISO(196)----MISO(197)----MISO(198)----MISO(199)-
  /// DR:   ___|¯¯¯|_____________________________________________________|¯¯¯|______________________________________..._____|¯¯¯|____________________________________________
  /// </summary>
  /// <param name="WriteData">The data to be written to the slave over the MOSI line in each SPI transaction</param>
  /// <param name="numCaptures">The number of transfers of the WriteData array performed on each data ready (if enabled).</param>
  /// <param name="numBuffers">The total number of data ready's to capture.</param>
  /// <returns></returns>
  public uint[] TransferArray(IEnumerable<uint> WriteData, uint numCaptures, uint numBuffers)
  {
    this.m_SPI32InitialMOSI.Clear();
    return this.TransferArrayImplementation(WriteData, numCaptures, numBuffers);
  }

  /// <summary>Transfer array low level implementation</summary>
  /// <param name="WriteData"></param>
  /// <param name="numCaptures"></param>
  /// <param name="numBuffers"></param>
  /// <returns></returns>
  private uint[] TransferArrayImplementation(
    IEnumerable<uint> WriteData,
    uint numCaptures,
    uint numBuffers)
  {
    List<uint> uintList = new List<uint>();
    uint num1 = this.ISpi32TransferStreamSetup(WriteData, numCaptures, numBuffers);
    uint num2 = checked ((uint) Math.Round(Math.Ceiling(unchecked ((double) checked ((long) (WriteData.Count<uint>() * 4) * (long) numCaptures * (long) numBuffers) / (double) num1))));
    int len;
    if (this.m_ActiveFX3.bSuperSpeed)
    {
      len = 1024 /*0x0400*/;
    }
    else
    {
      if (!this.m_ActiveFX3.bHighSpeed)
        throw new FX3Exception("ERROR: Streaming application requires USB 2.0 or 3.0 connection to function");
      len = 512 /*0x0200*/;
    }
    byte[] buf = new byte[checked (len - 1 + 1)];
    this.m_StreamMutex.WaitOne();
    uint num3 = checked (num2 - 1U);
    uint num4 = 0;
    while (num4 <= num3)
    {
      if (USB.XferData(ref buf, ref len, ref this.StreamingEndPt))
      {
        uint num5 = checked (num1 - 4U);
        uint startIndex = 0;
        while (startIndex <= num5)
        {
          uintList.Add(BitConverter.ToUInt32(buf, checked ((int) startIndex)));
          if ((long) uintList.Count < checked ((long) WriteData.Count<uint>() * (long) numBuffers * (long) numCaptures))
            checked { startIndex += 4U; }
          else
            break;
        }
      }
      checked { ++num4; }
    }
    this.m_StreamMutex.ReleaseMutex();
    this.ISpi32TransferStreamDone();
    return uintList.ToArray();
  }

  /// <summary>
  /// Array transfer which performs an initial SPI transmit (MOSI only, no read back) then
  /// starts a read stream. This is useful if you need to issue a write to the DUT, then
  /// immediately start reading back data, without the USB transfer overhead of approx 150us
  /// </summary>
  /// <param name="InitialMOSIData">MOSI data to transmit before read (SPI words)</param>
  /// <param name="InitialDrActive">If the initial transmission is DR active or not</param>
  /// <param name="MOSIReadData">Data sent over the MOSI line during the transfer</param>
  /// <param name="NumBuffers">Number of buffers (samples) to read</param>
  /// <returns>MISO data from the DUT during the whole transfer</returns>
  public uint[] WriteReadTransferArray(
    IEnumerable<uint> InitialMOSIData,
    bool InitialDrActive,
    IEnumerable<uint> MOSIReadData,
    uint NumBuffers)
  {
    this.m_SPI32InitialMOSI.Clear();
    if (!Information.IsNothing((object) InitialMOSIData))
      this.m_SPI32InitialMOSI.AddRange(InitialMOSIData);
    this.m_SPI32InitialDrActive = InitialDrActive;
    return this.TransferArrayImplementation(MOSIReadData, 1U, NumBuffers);
  }

  void ISpi32Interface.ISpi32Interface_StartBufferedStream(
    IEnumerable<uint> WriteData,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    List<uint> parameter = new List<uint>();
    this.m_SPI32InitialMOSI.Clear();
    this.m_TotalBuffersToRead = numBuffers;
    this.m_FramesRead = 0L;
    this.m_TransferStreamData = new ConcurrentQueue<uint[]>();
    uint num1 = this.ISpi32TransferStreamSetup(WriteData, numCaptures, numBuffers);
    parameter.Add(num1);
    parameter.Add(checked ((uint) ((long) (WriteData.Count<uint>() * 4) * (long) numCaptures)));
    this.m_StreamThread = new Thread(new ParameterizedThreadStart(this.ISpi32InterfaceStreamWorker));
    this.m_StreamThread.Start((object) parameter);
    if (Information.IsNothing((object) worker))
      return;
    bool flag = false;
    int num2 = 0;
    while (!flag)
    {
      flag = !worker.WorkerSupportsCancellation ? this.GetNumBuffersRead >= (long) numBuffers : this.GetNumBuffersRead >= (long) numBuffers | worker.CancellationPending;
      if (worker.WorkerReportsProgress)
      {
        int percentProgress = checked ((int) Math.Round(unchecked ((double) checked (this.GetNumBuffersRead * 100L) / (double) numBuffers)));
        if (percentProgress > num2)
        {
          worker.ReportProgress(percentProgress);
          num2 = percentProgress;
        }
      }
      Thread.Sleep(25);
    }
    if (worker.WorkerSupportsCancellation & worker.CancellationPending)
      this.StopStream();
  }

  /// <summary>ISpi32 StopStream implementation. Calls generic version.</summary>
  public void ISpi32StopStream()
  {
    this.CancelStreamImplementation(USBCommands.ADI_TRANSFER_STREAM);
  }

  private void ISpi32TransferStreamDone()
  {
    byte[] Buf = new byte[4];
    this.ConfigureControlEndpoint(USBCommands.ADI_TRANSFER_STREAM, true);
    this.m_ActiveFX3.ControlEndPt.Value = (ushort) 0;
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 0;
    if (!this.XferControlData(ref Buf, 4, 2000))
      throw new FX3CommunicationException("ERROR: Timeout occurred when cleaning up a transfer stream thread on the FX3");
  }

  private uint ISpi32TransferStreamSetup(
    IEnumerable<uint> WriteData,
    uint numCaptures,
    uint numBuffers)
  {
    if (Information.IsNothing((object) WriteData))
      throw new FX3ConfigurationException("ERROR: WriteData must not be null");
    if (WriteData.Count<uint>() == 0)
      throw new FX3ConfigurationException("ERROR: WriteData must contain at least one element");
    List<byte> byteList = new List<byte>();
    byteList.Add(checked ((byte) (unchecked ((int) numCaptures) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) ((numCaptures & 65280U) >> 8)));
    byteList.Add(checked ((byte) ((numCaptures & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)));
    byteList.Add(checked ((byte) ((numCaptures & 4278190080U /*0xFF000000*/) >> 24)));
    byteList.Add(checked ((byte) (unchecked ((int) numBuffers) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) ((numBuffers & 65280U) >> 8)));
    byteList.Add(checked ((byte) ((numBuffers & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)));
    byteList.Add(checked ((byte) ((numBuffers & 4278190080U /*0xFF000000*/) >> 24)));
    uint num1 = checked ((uint) ((long) WriteData.Count<uint>() * 4L * (long) numCaptures));
    uint num2;
    if (this.m_ActiveFX3.bSuperSpeed)
    {
      num2 = 1024U /*0x0400*/;
    }
    else
    {
      if (!this.m_ActiveFX3.bHighSpeed)
        throw new FX3Exception("ERROR: Streaming application requires USB 2.0 or 3.0 connection to function");
      num2 = 512U /*0x0200*/;
    }
    uint num3 = num1 <= num2 ? checked ((uint) Math.Round(unchecked (Math.Floor((double) num2 / (double) num1) * (double) num1))) : num2;
    byteList.Add(checked ((byte) (unchecked ((int) num3) & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) ((num3 & 65280U) >> 8)));
    byteList.Add(checked ((byte) ((num3 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)));
    byteList.Add(checked ((byte) ((num3 & 4278190080U /*0xFF000000*/) >> 24)));
    ushort num4 = checked ((ushort) ((long) WriteData.Count<uint>() * 4L));
    byteList.Add(checked ((byte) ((int) num4 & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (((uint) num4 & 65280U) >> 8)));
    ushort num5 = checked ((ushort) ((long) this.m_SPI32InitialMOSI.Count * 4L));
    byteList.Add(checked ((byte) ((int) num5 & (int) byte.MaxValue)));
    byteList.Add(checked ((byte) (((uint) num5 & 65280U) >> 8)));
    byteList.Add((byte) -(this.m_SPI32InitialDrActive ? 1 : 0));
    try
    {
      foreach (uint num6 in WriteData)
      {
        byteList.Add(checked ((byte) (unchecked ((int) num6) & (int) byte.MaxValue)));
        byteList.Add(checked ((byte) ((num6 & 65280U) >> 8)));
        byteList.Add(checked ((byte) ((num6 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)));
        byteList.Add(checked ((byte) ((num6 & 4278190080U /*0xFF000000*/) >> 24)));
      }
    }
    finally
    {
      IEnumerator<uint> enumerator;
      enumerator?.Dispose();
    }
    try
    {
      foreach (uint num7 in this.m_SPI32InitialMOSI)
      {
        byteList.Add(checked ((byte) (unchecked ((int) num7) & (int) byte.MaxValue)));
        byteList.Add(checked ((byte) ((num7 & 65280U) >> 8)));
        byteList.Add(checked ((byte) ((num7 & 16711680U /*0xFF0000*/) >> 16 /*0x10*/)));
        byteList.Add(checked ((byte) ((num7 & 4278190080U /*0xFF000000*/) >> 24)));
      }
    }
    finally
    {
      List<uint>.Enumerator enumerator;
      enumerator.Dispose();
    }
    if (byteList.Count > 4096 /*0x1000*/)
      throw new FX3ConfigurationException($"ERROR: Invalid WriteData provided to StartTransferStream. Transfer count of {byteList.Count.ToString()}bytes exceeds allowed amount of 4096 bytes");
    this.ConfigureControlEndpoint(USBCommands.ADI_TRANSFER_STREAM, true);
    this.m_ActiveFX3.ControlEndPt.Index = (ushort) 1;
    byte[] array = byteList.ToArray();
    if (!this.XferControlData(ref array, byteList.Count, 2000))
      throw new FX3CommunicationException("ERROR: Timeout occurred during control endpoint transfer for SPI transfer stream");
    return num3;
  }

  private void ISpi32InterfaceStreamWorker(object StreamArgs)
  {
    int num1 = 0;
    List<uint> uintList1 = new List<uint>();
    List<uint> uintList2 = (List<uint>) StreamArgs;
    uint num2 = uintList2[0];
    uint num3 = uintList2[1];
    int len;
    if (this.m_ActiveFX3.bSuperSpeed)
    {
      len = 1024 /*0x0400*/;
    }
    else
    {
      if (!this.m_ActiveFX3.bHighSpeed)
        throw new FX3Exception("ERROR: Streaming application requires USB 2.0 or 3.0 connection to function");
      len = 512 /*0x0200*/;
    }
    byte[] buf = new byte[checked (len - 1 + 1)];
    this.m_StreamThreadRunning = false;
    this.m_StreamMutex.WaitOne();
    this.m_StreamType = StreamType.TransferStream;
    this.m_StreamThreadRunning = true;
    while (this.m_StreamThreadRunning)
    {
      if (USB.XferData(ref buf, ref len, ref this.StreamingEndPt))
      {
        uint num4 = checked (num2 - 3U);
        uint startIndex = 0;
        while (startIndex <= num4)
        {
          uintList1.Add(BitConverter.ToUInt32(buf, checked ((int) startIndex)));
          if ((long) checked (uintList1.Count * 4) >= (long) num3)
          {
            this.m_TransferStreamData.Enqueue(uintList1.ToArray());
            // ISSUE: reference to a compiler-generated field
            IStreamEventProducer.NewBufferAvailableEventHandler bufferAvailableEvent = this.NewBufferAvailableEvent;
            if (bufferAvailableEvent != null)
              bufferAvailableEvent(this.m_TransferStreamData.Count);
            Interlocked.Increment(ref this.m_FramesRead);
            uintList1.Clear();
            checked { ++num1; }
            if ((long) num1 >= (long) this.m_TotalBuffersToRead)
            {
              this.ISpi32TransferStreamDone();
              goto label_18;
            }
          }
          checked { startIndex += 4U; }
        }
      }
      else
      {
        if (this.m_StreamThreadRunning)
        {
          Console.WriteLine($"Transfer failed during transfer stream. Error code: {this.StreamingEndPt.LastError.ToString()} (0x{this.StreamingEndPt.LastError.ToString("X4")})");
          this.CancelStreamImplementation(USBCommands.ADI_TRANSFER_STREAM);
          break;
        }
        break;
      }
    }
label_18:
    this.ExitStreamThread();
  }

  uint[] ISpi32Interface.ISpi32Interface_GetBufferedStreamDataPacket()
  {
    if (Information.IsNothing((object) this.m_TransferStreamData) || this.m_TransferStreamData.Count == 0 & !this.m_StreamThreadRunning)
      return (uint[]) null;
    uint[] result = (uint[]) null;
    this.m_TransferStreamData.TryDequeue(out result);
    return result;
  }

  int ISpi32Interface.ISpi32Interface_StreamTimeoutSeconds
  {
    get => this.StreamTimeoutSeconds;
    set => this.StreamTimeoutSeconds = value;
  }

  bool ISpi32Interface.ISpi32Interface_DrActive
  {
    get => this.DrActive;
    set => this.DrActive = value;
  }

  /// <summary>
  /// This property is used to get or set the data ready pin. Is tied to the ReadyPin property
  /// </summary>
  /// <returns></returns>
  public IPinObject DrPin
  {
    get => this.ReadyPin;
    set => this.ReadyPin = value;
  }

  bool ISpi32Interface.ISpi32Interface_DrPolarity
  {
    get => this.DrPolarity;
    set => this.DrPolarity = value;
  }

  public delegate void UnexpectedDisconnectEventHandler(string FX3SerialNum);

  public delegate void DisconnectFinishedEventHandler(string FX3SerialNum, int DisconnectTime);
}
