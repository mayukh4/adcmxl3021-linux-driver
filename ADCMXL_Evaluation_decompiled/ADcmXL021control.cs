using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using AdisApi;
using adisInterface;
using FX3Api;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;

namespace Vibration_Evaluation
{
	// Token: 0x02000009 RID: 9
	public class ADcmXL021control : EventArgs, iVIBEcontrol
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002FB4 File Offset: 0x000011B4
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002FBE File Offset: 0x000011BE
		private virtual TextFileStreamManager tfsm { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x06000040 RID: 64 RVA: 0x00002FC8 File Offset: 0x000011C8
		public ADcmXL021control(RegMapCollection Reg, ref IDutInterface dutCom, ref CommandCollection cmdCollection, ref FX3Connection fxSpi)
		{
			this.Reg = new RegMapCollection();
			this.flashReg = "COMMAND";
			this.flashWord = 8U;
			this.flashDelay = 1200;
			this.cmdCollection = new CommandCollection();
			this._alarms = new AlarmsClass();
			this._recinfo = new iVIBEcontrol.RecInfoClass();
			this._plot = new iVIBEcontrol.PlotClass();
			this.AlarmStatus = new iVIBEcontrol.AlarmStatusClass[4];
			this.tfsm = new TextFileStreamManager();
			this.RunAsyncCompletedEventArgs = new RunAsyncCompletedEventArgs();
			this.BurstData = new List<ushort>();
			this._LogScale = true;
			this._plotFmax = 110000.0;
			this._statusCode = 0;
			this._axisSelected = iVIBEcontrol.axis.x;
			this._sensor = new iVIBEcontrol.SensorClass[2];
			this._SpiConfig = "30MHz";
			this._sensorSelected = 1;
			this._SensorsOnNetwork = new iVIBEcontrol.SensorOnNetworkClass();
			this._recordMode = iVIBEcontrol.CapMode.manualFFT;
			this._bufferWidth = 256;
			try
			{
				this.Dut = dutCom;
				this.Reg = Reg;
				this.cmdCollection = cmdCollection;
				this.fxSpi = fxSpi;
				this.AlarmStatus[1] = new iVIBEcontrol.AlarmStatusClass();
				this.AlarmStatus[2] = new iVIBEcontrol.AlarmStatusClass();
				this.AlarmStatus[3] = new iVIBEcontrol.AlarmStatusClass();
				this.SensorsOnNetwork.setActive(1);
				this.SensorSelected = 1;
				this.Sensor[1] = new iVIBEcontrol.SensorClass();
				this.ScaleData = true;
			}
			catch (Exception ex)
			{
				Interaction.MsgBox(ex.Message + "\r\nADcmcontrolcontrol.vb sub New()", MsgBoxStyle.OkOnly, null);
			}
			this.BFpinFunc = new PinFcns(this.SDP);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003188 File Offset: 0x00001388
		public void initializeDUT(int prodNumber, ref FX3Connection Spi)
		{
			this.SensorsOnNetwork.setActive(1);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003198 File Offset: 0x00001398
		public void getBurstData(int count)
		{
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000319C File Offset: 0x0000139C
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000031B4 File Offset: 0x000013B4
		public bool LogScale
		{
			get
			{
				return this._LogScale;
			}
			set
			{
				this._LogScale = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000031C0 File Offset: 0x000013C0
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000031D8 File Offset: 0x000013D8
		public bool ExtTrigger
		{
			get
			{
				return this._ExtTrigger;
			}
			set
			{
				this._ExtTrigger = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000031E2 File Offset: 0x000013E2
		// (set) Token: 0x06000048 RID: 72 RVA: 0x000031EC File Offset: 0x000013EC
		public int recPrdSec { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000031F8 File Offset: 0x000013F8
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00003210 File Offset: 0x00001410
		public double plotFmax
		{
			get
			{
				return this._plotFmax;
			}
			set
			{
				this._plotFmax = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000321C File Offset: 0x0000141C
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00003234 File Offset: 0x00001434
		public bool ScaleData
		{
			get
			{
				return this._ScaleData;
			}
			set
			{
				this._ScaleData = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003240 File Offset: 0x00001440
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00003258 File Offset: 0x00001458
		public bool userCancel
		{
			get
			{
				return this._userCancel;
			}
			set
			{
				this._userCancel = value;
				bool userCancel = this.userCancel;
				if (userCancel)
				{
				}
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000327C File Offset: 0x0000147C
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003294 File Offset: 0x00001494
		public double upDateIntervalSec
		{
			get
			{
				return this._upDateIntervalSec;
			}
			set
			{
				this._upDateIntervalSec = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000032A0 File Offset: 0x000014A0
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000032B8 File Offset: 0x000014B8
		public int statusCode
		{
			get
			{
				return this._statusCode;
			}
			set
			{
				this._statusCode = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000053 RID: 83 RVA: 0x000032C4 File Offset: 0x000014C4
		// (set) Token: 0x06000054 RID: 84 RVA: 0x000032DC File Offset: 0x000014DC
		public iVIBEcontrol.axis axisSelected
		{
			get
			{
				return this._axisSelected;
			}
			set
			{
				this._axisSelected = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000032E8 File Offset: 0x000014E8
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003300 File Offset: 0x00001500
		public string ProductSelected
		{
			get
			{
				return this._ProductSelected;
			}
			set
			{
				this._ProductSelected = value;
				string left = Strings.UCase(this.ProductSelected);
				if (Operators.CompareString(left, "ADCMXL1021", false) != 0)
				{
					if (Operators.CompareString(left, "ADCMXL2021", false) != 0)
					{
						if (Operators.CompareString(left, "ADCMXL3021", false) != 0)
						{
							Interaction.MsgBox("ADcmXL021control Property ProductSelected; unknown selected product string.", MsgBoxStyle.OkOnly, null);
						}
						else
						{
							this.axisCount = 3;
						}
					}
					else
					{
						this.axisCount = 2;
					}
				}
				else
				{
					this.axisCount = 1;
				}
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003380 File Offset: 0x00001580
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003398 File Offset: 0x00001598
		public int axisCount
		{
			get
			{
				return this._axisCount;
			}
			set
			{
				this._axisCount = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000033A4 File Offset: 0x000015A4
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000033BC File Offset: 0x000015BC
		public iVIBEcontrol.SensorClass[] Sensor
		{
			get
			{
				return this._sensor;
			}
			set
			{
				this._sensor = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000033C8 File Offset: 0x000015C8
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000033E0 File Offset: 0x000015E0
		public AdisBase SDP
		{
			get
			{
				return this._SDP;
			}
			set
			{
				this._SDP = value;
			}
		}

		/// <summary>
		/// Sets SPI signals to GPIO pins "5MHz" or SPI Peripheral pins "30MHz".
		/// </summary>
		/// <returns></returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000033EC File Offset: 0x000015EC
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003198 File Offset: 0x00001398
		public string SpiConfig
		{
			get
			{
				return this._SpiConfig;
			}
			set
			{
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003198 File Offset: 0x00001398
		public void PeriodicModeSet(int upDateSec)
		{
		}

		/// <summary>
		/// Starts pin polling DIO3. Cancel by setting userCancel = True
		/// Calss Wait for Capture, 
		/// </summary>
		// Token: 0x06000060 RID: 96 RVA: 0x00003404 File Offset: 0x00001604
		public void ExtTriggerWait()
		{
			PinObject pin = new PinObject(PortType.H, 3U);
			bool flag = false;
			this.userCancel = false;
			Thread.Sleep(1);
			while (!flag & this.ExtTrigger)
			{
				flag = (GlobalDeclarations.FX3comm.ReadPin(pin) > 0U);
				Thread.Sleep(1);
				Application.DoEvents();
			}
			BackgroundWorker bgWorker = new BackgroundWorker();
			bool extTrigger = this.ExtTrigger;
			if (extTrigger)
			{
				this.StartCmd(bgWorker);
				this.WaitForCapture(bgWorker, false);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003480 File Offset: 0x00001680
		public bool waitForRealTimeData()
		{
			int num = 256;
			checked
			{
				double[] array = new double[num - 1 + 1];
				double[] array2 = new double[2];
				this.Reg["X_BUF"].IsTwosComp = true;
				this.Reg["Y_BUF"].IsTwosComp = true;
				this.Reg["Z_BUF"].IsTwosComp = true;
				int num2 = num - 1;
				for (int i = 0; i <= num2; i++)
				{
					switch (this.axisSelected)
					{
					case iVIBEcontrol.axis.x:
						array2 = this.Dut.ReadScaledValue(this.Reg["X_BUF"], 1U);
						break;
					case iVIBEcontrol.axis.y:
						array2 = this.Dut.ReadScaledValue(this.Reg["Y_BUF"], 1U);
						break;
					case iVIBEcontrol.axis.z:
						array2 = this.Dut.ReadScaledValue(this.Reg["Z_BUF"], 1U);
						break;
					}
					array[i] = array2[0];
				}
				switch (this.axisSelected)
				{
				case iVIBEcontrol.axis.x:
					this.Sensor[this.SensorSelected].dataX = array;
					break;
				case iVIBEcontrol.axis.y:
					this.Sensor[this.SensorSelected].dataY = array;
					break;
				case iVIBEcontrol.axis.z:
					this.Sensor[this.SensorSelected].dataZ = array;
					break;
				}
				this.Reg["X_BUF"].IsTwosComp = false;
				this.Reg["Y_BUF"].IsTwosComp = false;
				this.Reg["Z_BUF"].IsTwosComp = false;
				return true;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003644 File Offset: 0x00001844
		// (set) Token: 0x06000063 RID: 99 RVA: 0x0000365C File Offset: 0x0000185C
		public int SensorSelected
		{
			get
			{
				return this._sensorSelected;
			}
			set
			{
				bool flag = value == 1;
				if (flag)
				{
					this._sensorSelected = value;
				}
				else
				{
					string text = "ERROR: ADcmXL021control.vb property SensorSelected.\r\n";
					text = text + "ADcmXL021 cannot select sensor ( " + value.ToString() + " )";
					Interaction.MsgBox(text, MsgBoxStyle.Exclamation, null);
				}
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000036A8 File Offset: 0x000018A8
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000036C0 File Offset: 0x000018C0
		public iVIBEcontrol.SensorOnNetworkClass SensorsOnNetwork
		{
			get
			{
				return this._SensorsOnNetwork;
			}
			set
			{
				this._SensorsOnNetwork = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000036CC File Offset: 0x000018CC
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000036E4 File Offset: 0x000018E4
		public FX3Connection fxSpi
		{
			get
			{
				return this._fxspi;
			}
			set
			{
				this._fxspi = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000036F0 File Offset: 0x000018F0
		// (set) Token: 0x06000069 RID: 105 RVA: 0x00003708 File Offset: 0x00001908
		public AlarmsClass Alarms
		{
			get
			{
				return this._alarms;
			}
			set
			{
				this._alarms = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003714 File Offset: 0x00001914
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000372C File Offset: 0x0000192C
		public iVIBEcontrol.RecInfoClass RecInfo
		{
			get
			{
				return this._recinfo;
			}
			set
			{
				this._recinfo = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003738 File Offset: 0x00001938
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003750 File Offset: 0x00001950
		public iVIBEcontrol.PlotClass Plot
		{
			get
			{
				return this._plot;
			}
			set
			{
				this._plot = value;
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000375C File Offset: 0x0000195C
		public List<uint> AlarmStatusRead()
		{
			List<uint> list = new List<uint>();
			uint item = this.Dut.ReadUnsigned(this.Reg["ALM_X_STAT"]);
			uint item2 = this.Dut.ReadUnsigned(this.Reg["ALM_Y_STAT"]);
			uint item3 = this.Dut.ReadUnsigned(this.Reg["ALM_Z_STAT"]);
			list.Add(item);
			list.Add(item2);
			list.Add(item3);
			return list;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000037E8 File Offset: 0x000019E8
		public virtual AlarmsClass.LsbValuesArray[] AlarmsReadAll()
		{
			AlarmsClass.LsbValuesArray[] array = new AlarmsClass.LsbValuesArray[]
			{
				new AlarmsClass.LsbValuesArray(),
				new AlarmsClass.LsbValuesArray(),
				new AlarmsClass.LsbValuesArray(),
				new AlarmsClass.LsbValuesArray()
			};
			int num = 0;
			checked
			{
				do
				{
					array[num] = this.AlarmsReadSRO(num);
					num++;
				}
				while (num <= 3);
				return array;
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003848 File Offset: 0x00001A48
		public AlarmsClass.LsbValuesArray AlarmsReadSRO(int SROpt)
		{
			AlarmsClass.LsbValuesArray lsbValuesArray = new AlarmsClass.LsbValuesArray();
			uint num = 1U;
			checked
			{
				do
				{
					uint num2 = (uint)Math.Round(unchecked((double)SROpt * 256.0));
					uint dat = num2 | num;
					this.Dut.WriteUnsigned(this.Reg["ALM_PNTR"], dat);
					this.Dut.WriteUnsigned(this.Reg["COMMAND"], 16384U);
					uint num3 = this.Dut.ReadUnsigned(this.Reg["ALM_F_LOW"]);
					lsbValuesArray.frequencyLow[(int)num] = num3;
					lsbValuesArray.frequencyHigh[(int)num] = this.Dut.ReadUnsigned(this.Reg["ALM_F_HIGH"]);
					lsbValuesArray.X1[(int)num] = this.Dut.ReadUnsigned(this.Reg["ALM_X_MAG1"]);
					lsbValuesArray.X2[(int)num] = this.Dut.ReadUnsigned(this.Reg["ALM_X_MAG2"]);
					lsbValuesArray.Y1[(int)num] = this.Dut.ReadUnsigned(this.Reg["ALM_Y_MAG1"]);
					lsbValuesArray.Y2[(int)num] = this.Dut.ReadUnsigned(this.Reg["ALM_Y_MAG2"]);
					lsbValuesArray.Z1[(int)num] = this.Dut.ReadUnsigned(this.Reg["ALM_Z_MAG1"]);
					lsbValuesArray.Z2[(int)num] = this.Dut.ReadUnsigned(this.Reg["ALM_Z_MAG2"]);
					num += 1U;
				}
				while (num <= 6U);
				return lsbValuesArray;
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003A00 File Offset: 0x00001C00
		public virtual void AlarmsWrite(AlarmsClass.LsbValuesArray[] aLsbs)
		{
			AlarmsClass.LsbValuesArray[] array = new AlarmsClass.LsbValuesArray[1];
			this.Dut.WriteUnsigned(this.Reg["COMMAND"], 512U);
			Thread.Sleep(220);
			int num = 0;
			checked
			{
				do
				{
					int num2 = 1;
					do
					{
						uint num3 = (uint)Math.Round(unchecked((double)num * 256.0));
						uint dat = (uint)(unchecked((ulong)num3 | (ulong)((long)num2)));
						this.Dut.WriteUnsigned(this.Reg["ALM_PNTR"], dat);
						this.Dut.WriteUnsigned(this.Reg["ALM_F_LOW"], aLsbs[num].frequencyLow[num2]);
						this.Dut.WriteUnsigned(this.Reg["ALM_F_HIGH"], aLsbs[num].frequencyHigh[num2]);
						this.Dut.WriteUnsigned(this.Reg["ALM_X_MAG1"], aLsbs[num].X1[num2]);
						this.Dut.WriteUnsigned(this.Reg["ALM_X_MAG2"], aLsbs[num].X2[num2]);
						this.Dut.WriteUnsigned(this.Reg["ALM_Y_MAG1"], aLsbs[num].Y1[num2]);
						this.Dut.WriteUnsigned(this.Reg["ALM_Y_MAG2"], aLsbs[num].Y2[num2]);
						this.Dut.WriteUnsigned(this.Reg["ALM_Z_MAG1"], aLsbs[num].Z1[num2]);
						this.Dut.WriteUnsigned(this.Reg["ALM_Z_MAG2"], aLsbs[num].Z2[num2]);
						this.Dut.WriteUnsigned(this.Reg["COMMAND"], 4096U);
						Thread.Sleep(20);
						num2++;
					}
					while (num2 <= 6);
					num++;
				}
				while (num <= 3);
			}
		}

		/// <summary>
		/// Reads REC_INFO1 and REC_INFO2. Set properties of RecInfo object after cpature.
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x06000072 RID: 114 RVA: 0x00003C04 File Offset: 0x00001E04
		public void RecInfo_Read()
		{
			uint info = this.Dut.ReadUnsigned(this.Reg["REC_INFO1"]);
			uint info2 = this.Dut.ReadUnsigned(this.Reg["REC_INFO2"]);
			this.RecInfo_Decode(info, info2);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003C54 File Offset: 0x00001E54
		public void RecInfo_Decode(uint Info1, uint Info2)
		{
			double a = 49152.0;
			checked
			{
				this.RecInfo.sampleRateOpt = (int)Math.Round((double)(unchecked((ulong)Info1) & (ulong)((long)Math.Round(a))) / 16384.0);
				a = 3072.0;
				double num = (double)(unchecked((ulong)Info1) & (ulong)((long)Math.Round(a))) / 1024.0;
				double num2 = num;
				bool flag = num2 == 0.0;
				if (flag)
				{
					this.RecInfo.RangeG = 6;
				}
				else
				{
					flag = (num2 == 1.0);
					if (flag)
					{
						this.RecInfo.RangeG = 12;
					}
					else
					{
						flag = (num2 == 2.0);
						if (flag)
						{
							this.RecInfo.RangeG = 25;
						}
						else
						{
							flag = (num2 == 3.0);
							if (flag)
							{
								this.RecInfo.RangeG = 50;
							}
						}
					}
				}
				a = 15.0;
				this.RecInfo.AvgCnt = (int)Math.Round((double)(unchecked((ulong)Info2) & (ulong)((long)Math.Round(a))) / 1.0);
				float num3 = 110000f;
				this.RecInfo.Fmax = (float)((double)num3 / Math.Pow(2.0, (double)this.RecInfo.AvgCnt));
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003DBC File Offset: 0x00001FBC
		public virtual double[] readSelectedAxis(IEnumerable<RegClass> reglist, uint nSamples)
		{
			double[] array = new double[0];
			return this.Dut.ReadScaledValue(reglist, nSamples);
		}

		/// <summary>
		/// Reads REC_CTRL. Sets RecordMode and powerDown property.
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x06000075 RID: 117 RVA: 0x00003DE4 File Offset: 0x00001FE4
		public void RecControl_Read()
		{
			uint num = this.Dut.ReadUnsigned(this.Reg["REC_CTRL"]);
			switch (checked((int)(unchecked((ulong)num) & 3UL)))
			{
			case 0:
				this.RecordMode = iVIBEcontrol.CapMode.manualFFT;
				break;
			case 1:
				this.RecordMode = iVIBEcontrol.CapMode.AutomaticFFT;
				break;
			case 2:
				this.RecordMode = iVIBEcontrol.CapMode.manualTimeDomain;
				break;
			case 3:
				this.RecordMode = iVIBEcontrol.CapMode.realTimeDomain;
				break;
			}
			this.powerDownConfig = false;
			bool flag = ((ulong)num & 128UL) == 128UL;
			if (flag)
			{
				this.powerDownConfig = true;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E84 File Offset: 0x00002084
		public bool REC_PRD_check()
		{
			bool result = true;
			string[] array = new string[8];
			uint num = this.Dut.ReadUnsigned(this.Reg["REC_PRD"]);
			int num4;
			int num6;
			int num7;
			int num9;
			int num10;
			double num11;
			double num12;
			double num13;
			double num14;
			int num16;
			int num17;
			int num18;
			int num19;
			checked
			{
				int num2 = (int)(unchecked((ulong)num) & 768UL);
				int num3 = 0;
				if (num2 != 0)
				{
					if (num2 != 256)
					{
						if (num2 == 512)
						{
							num3 = 3600000;
						}
					}
					else
					{
						num3 = 60000;
					}
				}
				else
				{
					num3 = 1000;
				}
				num4 = (int)(unchecked((ulong)num) * (ulong)(unchecked((long)num3)));
				uint num5 = this.Dut.ReadUnsigned(this.Reg["FFT_AVG1"]);
				num6 = (int)(unchecked((ulong)num5) & 255UL);
				num7 = (int)(unchecked((ulong)num5) & 65280UL) / 256;
				uint num8 = this.Dut.ReadUnsigned(this.Reg["FFT_AVG2"]);
				num9 = (int)(unchecked((ulong)num8) & 255UL);
				num10 = (int)(unchecked((ulong)num8) & 65280UL) / 256;
				bool flag = num6 == 0;
				if (flag)
				{
					num6 = 1;
				}
				bool flag2 = num7 == 0;
				if (flag2)
				{
					num7 = 1;
				}
				bool flag3 = num9 == 0;
				if (flag3)
				{
					num9 = 1;
				}
				bool flag4 = num10 == 0;
				if (flag4)
				{
					num10 = 1;
				}
				num11 = -0.447961904762824;
				num12 = 0.814910185185927;
				num13 = -0.249096428571606;
				num14 = 0.0282148148148279;
				uint num15 = this.Dut.ReadUnsigned(this.Reg["AVG_CNT"]);
				num16 = (int)((unchecked((ulong)num15) & 15UL) / 1UL);
				num17 = (int)((unchecked((ulong)num15) & 240UL) / 16UL);
				num18 = (int)((unchecked((ulong)num15) & 3840UL) / 256UL);
				num19 = (int)((unchecked((ulong)num15) & 61440UL) / 4096UL);
				bool flag5 = num16 == 0;
				if (flag5)
				{
					num16 = 1;
				}
				bool flag6 = num17 == 0;
				if (flag6)
				{
					num17 = 1;
				}
				bool flag7 = num18 == 0;
				if (flag7)
				{
					num18 = 1;
				}
				bool flag8 = num19 == 0;
				if (flag8)
				{
					num19 = 1;
				}
			}
			double num20 = num14 * Math.Pow((double)num16, 3.0) + num13 * Math.Pow((double)num16, 2.0) + num12 * (double)num16 + num11;
			double num21 = num14 * Math.Pow((double)num17, 3.0) + num13 * Math.Pow((double)num17, 2.0) + num12 * (double)num17 + num11;
			double num22 = num14 * Math.Pow((double)num18, 3.0) + num13 * Math.Pow((double)num18, 2.0) + num12 * (double)num18 + num11;
			double num23 = num14 * Math.Pow((double)num19, 3.0) + num13 * Math.Pow((double)num19, 2.0) + num12 * (double)num19 + num11;
			double num24 = (num20 * (double)num6 + 1.0) * 1000.0;
			double num25 = (num21 * (double)num7 + 1.0) * 1000.0;
			double num26 = (num22 * (double)num9 + 1.0) * 1000.0;
			double num27 = (num23 * (double)num10 + 1.0) * 1000.0;
			uint num28 = this.Dut.ReadUnsigned(this.Reg["REC_CTRL"]);
			checked
			{
				int num29 = (int)(unchecked((ulong)num28) & 256UL);
				int num30 = (int)(unchecked((ulong)num28) & 512UL);
				int num31 = (int)(unchecked((ulong)num28) & 1024UL);
				int num32 = (int)(unchecked((ulong)num28) & 2048UL);
				array[0] = "OK";
				array[1] = "Invalid automatic mode capture configuration.";
				array[2] = "Capture time(msec) is greater than the REC_PRD time(msec).";
				bool flag9 = num24 > (double)num4 & num29 == 256;
				if (flag9)
				{
					array[0] = "ERROR";
					array[3] = string.Concat(new string[]
					{
						"Sample rate option(0) AVG_FFT1 low byte msec. = ",
						num24.ToString("####"),
						" > ",
						num4.ToString(),
						" REC_PRD msec."
					});
				}
				bool flag10 = num25 > (double)num4 & num30 == 512;
				if (flag10)
				{
					array[0] = "ERROR";
					array[4] = string.Concat(new string[]
					{
						"Sample rate option(1) AVG_FFT1 high byte msec. = ",
						num25.ToString("####"),
						" > ",
						num4.ToString(),
						" REC_PRD msec."
					});
				}
				bool flag11 = num26 > (double)num4 & num31 == 1024;
				if (flag11)
				{
					array[0] = "ERROR";
					array[5] = string.Concat(new string[]
					{
						"Sample rate option(2) AVG_FFT2 low byte msec. = ",
						num26.ToString("####"),
						" > ",
						num4.ToString(),
						" REC_PRD msec."
					});
				}
				bool flag12 = num27 > (double)num4 & num32 == 2048;
				if (flag12)
				{
					array[0] = "ERROR";
					array[6] = string.Concat(new string[]
					{
						"Sample rate option(3) AVG_FFT2 high byte msec. = ",
						num27.ToString("####"),
						" > ",
						num4.ToString(),
						" REC_PRD msec."
					});
				}
				array[7] = "Please reduce the FFT_AVG value or reduce the AVG_CNT value or increase the REC_PRD value.";
				bool flag13 = Operators.CompareString(array[0], "ERROR", false) == 0;
				if (flag13)
				{
					result = false;
					string text = "";
					int num33 = 0;
					do
					{
						text = text + "\r\n" + array[num33];
						num33++;
					}
					while (num33 <= 7);
					Interaction.MsgBox(text, MsgBoxStyle.OkOnly, null);
				}
				return result;
			}
		}

		/// <summary>
		/// Set in RecControl_Read(). REC_CTRL bit 7
		/// </summary>
		/// <returns></returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004442 File Offset: 0x00002642
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000444C File Offset: 0x0000264C
		public bool powerDownConfig { get; set; }

		/// <summary>
		/// Stores only the configured capture mode. SR option is read from REC_INFO1
		/// </summary>
		/// <returns></returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004458 File Offset: 0x00002658
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00004470 File Offset: 0x00002670
		iVIBEcontrol.CapMode iVIBEcontrol.RecordMode
		{
			get
			{
				return this._recordMode;
			}
			set
			{
				this._recordMode = value;
				switch (value)
				{
				case iVIBEcontrol.CapMode.manualFFT:
					this.BufferWidth = 2048;
					this.writeCommand("Record Mode Manual FFT");
					break;
				case iVIBEcontrol.CapMode.AutomaticFFT:
					this.BufferWidth = 2048;
					this.writeCommand("Record Mode Automatic FFT");
					break;
				case iVIBEcontrol.CapMode.manualTimeDomain:
					this.BufferWidth = 4096;
					this.writeCommand("Record Mode Manual Time");
					break;
				}
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000044F8 File Offset: 0x000026F8
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00004510 File Offset: 0x00002710
		public int BufferWidth
		{
			get
			{
				return this._bufferWidth;
			}
			set
			{
				this._bufferWidth = value;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000451C File Offset: 0x0000271C
		public virtual void writeEscape()
		{
			RegClass regClass = new RegClass();
			regClass.Address = 232U;
			this.Dut.WriteUnsigned(regClass, 232U);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004550 File Offset: 0x00002750
		public void RealTimeSamplingStart()
		{
			this.writeCommand("Record Mode Real Time");
			bool flag = this.axisSelected == iVIBEcontrol.axis.x;
			if (flag)
			{
				this.Sensor[this.SensorSelected].dataX = this.Dut.ReadScaledValue(this.Reg["X_BUF"], 116U);
			}
			else
			{
				bool flag2 = this.axisSelected == iVIBEcontrol.axis.y;
				if (flag2)
				{
					this.Sensor[this.SensorSelected].dataX = this.Dut.ReadScaledValue(this.Reg["Y_BUF"], 116U);
				}
				else
				{
					bool flag3 = this.axisSelected == iVIBEcontrol.axis.z;
					if (flag3)
					{
						this.Sensor[this.SensorSelected].dataX = this.Dut.ReadScaledValue(this.Reg["Z_BUF"], 116U);
					}
				}
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003198 File Offset: 0x00001398
		public void RealTimeStop()
		{
		}

		/// <summary>
		/// Sends command bit 2^11.
		/// </summary>
		// Token: 0x06000080 RID: 128 RVA: 0x00004628 File Offset: 0x00002828
		public void StartCmd(BackgroundWorker bgWorker)
		{
			this.userCancel = false;
			checked
			{
				this.recPrdSec = (int)this.Dut.ReadUnsigned(this.Reg["REC_PRD"]);
				switch (this.RecordMode)
				{
				case iVIBEcontrol.CapMode.manualFFT:
					GlobalDeclarations.FX3comm.SclkFrequency = GlobalDeclarations.SPIsclkUser;
					Thread.Sleep(100);
					this.BufferWidth = 2048;
					this.Plot.Xmax = 2048f;
					this.Plot.XscaleMax = 2048f;
					this.Dut.WriteUnsigned(this.Reg["COMMAND"], 2048U);
					this.WaitForCapture(bgWorker, false);
					break;
				case iVIBEcontrol.CapMode.AutomaticFFT:
					GlobalDeclarations.FX3comm.SclkFrequency = GlobalDeclarations.SPIsclkUser;
					Thread.Sleep(100);
					this.BufferWidth = 2048;
					this.Plot.Xmax = 2048f;
					this.Plot.XscaleMax = 2048f;
					this.Dut.WriteUnsigned(this.Reg["COMMAND"], 2048U);
					this.WaitForCapture(bgWorker, false);
					break;
				case iVIBEcontrol.CapMode.manualTimeDomain:
					GlobalDeclarations.FX3comm.SclkFrequency = GlobalDeclarations.SPIsclkUser;
					Thread.Sleep(100);
					this.BufferWidth = 4096;
					this.Plot.Xmax = 4096f;
					this.Plot.XscaleMax = 4096f;
					this.Dut.WriteUnsigned(this.Reg["COMMAND"], 2048U);
					this.WaitForCapture(bgWorker, false);
					break;
				case iVIBEcontrol.CapMode.realTimeDomain:
					switch (GlobalDeclarations.BoardID)
					{
					case GlobalDeclarations.BoardIDtype.SDPEVAL:
					{
						this.Dut.WriteUnsigned(this.Reg["REC_CTRL"], 32771U);
						this.Dut.WriteUnsigned(this.Reg["COMMAND"], 2048U);
						this.ReadBurstData(3);
						int num = 1;
						num++;
						break;
					}
					}
					break;
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000485C File Offset: 0x00002A5C
		public void StopCmd()
		{
			this.userCancel = true;
			switch (this.RecordMode)
			{
			case iVIBEcontrol.CapMode.manualFFT:
				this.Dut.WriteUnsigned(this.Reg["COMMAND"], 232U);
				this.Dut.WriteUnsigned(this.Reg["COMMAND"], 232U);
				break;
			case iVIBEcontrol.CapMode.AutomaticFFT:
			{
				bool powerDownConfig = this.powerDownConfig;
				if (powerDownConfig)
				{
					this.Dut.WriteUnsigned(this.Reg["COMMAND"], 232U);
					this.Dut.WriteUnsigned(this.Reg["COMMAND"], 232U);
				}
				else
				{
					this.Dut.WriteUnsigned(this.Reg["COMMAND"], 232U);
					this.Dut.WriteUnsigned(this.Reg["COMMAND"], 232U);
				}
				break;
			}
			case iVIBEcontrol.CapMode.manualTimeDomain:
			{
				bool powerDownConfig2 = this.powerDownConfig;
				if (powerDownConfig2)
				{
					this.Dut.WriteUnsigned(this.Reg["COMMAND"], 232U);
					this.Dut.WriteUnsigned(this.Reg["COMMAND"], 232U);
				}
				else
				{
					this.userCancel = this.userCancel;
				}
				break;
			}
			}
		}

		/// <summary>
		/// Calls DrWait.Bypasses ReadBuffers if DataReady is False. TwoLevels is used for automatic mode captures.
		/// </summary>
		/// <param name="TwoLevels"></param>
		/// <returns></returns>
		// Token: 0x06000082 RID: 130 RVA: 0x000049D8 File Offset: 0x00002BD8
		public bool WaitForCapture(BackgroundWorker bgWorker, bool TwoLevels = false)
		{
			bool flag = false;
			if (TwoLevels)
			{
				flag = this.DrPollPinLow(bgWorker);
			}
			bool flag2 = !bgWorker.CancellationPending;
			if (flag2)
			{
				flag = this.DrPollPinHigh(bgWorker);
				bool flag3 = !flag;
				if (flag3)
				{
					return false;
				}
				this.ReadBuffers();
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004A2C File Offset: 0x00002C2C
		public void ReadBuffers()
		{
			bool flag = true;
			bool flag2 = this.RecordMode == iVIBEcontrol.CapMode.manualFFT | this.RecordMode == iVIBEcontrol.CapMode.AutomaticFFT;
			checked
			{
				if (flag2)
				{
					flag = false;
					int num = (int)Math.Round(this.Dut.ReadScaledValue(this.Reg["AVG_CNT"])) & 15;
					this.plotFmax = 110000.0 / Math.Pow(2.0, (double)num);
					this.Alarms.scaleFactor[0].Fmax = 110000.0 / Math.Pow(2.0, (double)num);
				}
				this.Reg["X_BUF"].IsTwosComp = flag;
				this.Reg["Y_BUF"].IsTwosComp = flag;
				this.Reg["Z_BUF"].IsTwosComp = flag;
				this.RecInfo_Read();
				this.Dut.WriteUnsigned(this.Reg["BUF_PNTR"], 0U);
				bool scaleData = this.ScaleData;
				if (scaleData)
				{
					bool flag3 = flag;
					if (flag3)
					{
						this.Sensor[this.SensorSelected].dataX = this.Dut.ReadScaledValue(this.Reg["X_BUF"], (uint)this.BufferWidth);
						this.Sensor[this.SensorSelected].dataY = this.Dut.ReadScaledValue(this.Reg["Y_BUF"], (uint)this.BufferWidth);
						this.Sensor[this.SensorSelected].dataZ = this.Dut.ReadScaledValue(this.Reg["Z_BUF"], (uint)this.BufferWidth);
					}
					else
					{
						this.Sensor[this.SensorSelected].dataXui = this.Dut.ReadUnsigned(this.Reg["X_BUF"], (uint)this.BufferWidth);
						this.Sensor[this.SensorSelected].dataYui = this.Dut.ReadUnsigned(this.Reg["Y_BUF"], (uint)this.BufferWidth);
						this.Sensor[this.SensorSelected].dataZui = this.Dut.ReadUnsigned(this.Reg["Z_BUF"], (uint)this.BufferWidth);
						int num2 = this.Sensor[this.SensorSelected].dataXui.Count<uint>() - 1;
						for (int i = 0; i <= num2; i++)
						{
							this.Sensor[this.SensorSelected].dataX[i] = Convert.ToDouble(this.Sensor[this.SensorSelected].dataXui[i]);
							this.Sensor[this.SensorSelected].dataY[i] = Convert.ToDouble(this.Sensor[this.SensorSelected].dataYui[i]);
							this.Sensor[this.SensorSelected].dataZ[i] = Convert.ToDouble(this.Sensor[this.SensorSelected].dataZui[i]);
						}
					}
				}
				else
				{
					this.Sensor[this.SensorSelected].dataXui = this.Dut.ReadUnsigned(this.Reg["X_BUF"], (uint)this.BufferWidth);
					this.Sensor[this.SensorSelected].dataYui = this.Dut.ReadUnsigned(this.Reg["Y_BUF"], (uint)this.BufferWidth);
					this.Sensor[this.SensorSelected].dataZui = this.Dut.ReadUnsigned(this.Reg["Z_BUF"], (uint)this.BufferWidth);
				}
				this.Reg["X_BUF"].IsTwosComp = true;
				this.Reg["Y_BUF"].IsTwosComp = true;
				this.Reg["Z_BUF"].IsTwosComp = true;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004E4C File Offset: 0x0000304C
		public void ReadBurstData(int BurstCount)
		{
			int num = 4;
			bool flag = this.axisCount == 1;
			if (flag)
			{
				num = 12;
			}
			ushort[] array = new ushort[101];
			checked
			{
				ushort[] array2 = new ushort[this.axisCount * 32 + num - 1 + 1];
				ushort[] array3 = new ushort[2];
				this.BurstData.Clear();
				ushort num2 = (ushort)((this.axisCount * 32 + num) * BurstCount);
				int num3 = array3.Count<ushort>() - 1;
				for (int i = 0; i <= num3; i++)
				{
					this.BurstData.Add(array3[i]);
				}
			}
		}

		/// <summary>
		/// Converts G to 16 bit register values.
		/// </summary>
		/// <param name="gdata"></param>
		// Token: 0x06000085 RID: 133 RVA: 0x00004EE4 File Offset: 0x000030E4
		public uint[] GtoLog2(double[] gData)
		{
			checked
			{
				uint[] array = new uint[gData.Count<double>() + 1];
				double num = this.Reg["X_BUF"].Scale / 2.0;
				int num2 = gData.Count<double>() - 1;
				for (int i = 0; i <= num2; i++)
				{
					double num3 = Math.Log(gData[i] / num) / Math.Log(2.0);
					array[i] = (uint)Math.Round(unchecked(num3 * 2048.0));
				}
				return array;
			}
		}

		/// <summary>
		/// Scales log2 data; /2048 , /fftAvg1, * RegScale/2; returns a log2 nolinear scale.
		/// </summary>
		/// <param name="regData"></param>
		/// <param name="fftAvg"></param>
		/// <returns></returns>
		// Token: 0x06000086 RID: 134 RVA: 0x00004F70 File Offset: 0x00003170
		public double[] Log2toFFT(uint[] regData, uint fftAvg)
		{
			checked
			{
				double[] array = new double[regData.Count<uint>() + 1];
				double num = this.Reg["X_BUF"].Scale / 2.0;
				int num2 = regData.Count<uint>() - 1;
				for (int i = 0; i <= num2; i++)
				{
					array[i] = unchecked(regData[i] / 2048.0 / fftAvg * num);
				}
				return array;
			}
		}

		/// <summary>
		/// Converts register values (frequency domain Log2) to linear G.
		/// </summary>
		/// <param name="regData"></param>
		/// <param name="fftAvg"></param>
		/// <returns></returns>
		// Token: 0x06000087 RID: 135 RVA: 0x00004FE4 File Offset: 0x000031E4
		public double[] Log2toG(uint[] regData, uint fftAvg)
		{
			checked
			{
				double[] array = new double[regData.Count<uint>() + 1];
				double num = this.Reg["X_BUF"].Scale / 2.0;
				int num2 = regData.Count<uint>() - 1;
				for (int i = 0; i <= num2; i++)
				{
					array[i] = unchecked(Math.Pow(2.0, regData[i] / 2048.0) / fftAvg * num);
				}
				return array;
			}
		}

		/// <summary>
		/// Returns an uinteger containing cal data as MMDDYYY
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		// Token: 0x06000088 RID: 136 RVA: 0x00005068 File Offset: 0x00003268
		public int GetCalDate()
		{
			RegClass[] regList = new RegClass[]
			{
				this.Reg["DAY_MONTH"],
				this.Reg["YEAR"]
			};
			uint[] array = this.Dut.ReadUnsigned(regList);
			int num = this.bcd2dec(array[0]);
			int num2 = this.bcd2dec(array[1]);
			bool flag = num < 0 | num2 < 0;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				result = checked(10000 * num + num2);
			}
			return result;
		}

		/// <summary>
		/// Returns the programmed sample rate in Hz.
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		// Token: 0x06000089 RID: 137 RVA: 0x000050E8 File Offset: 0x000032E8
		public virtual double SampleRateGet(double externalFrequency = 0.0)
		{
			bool flag = externalFrequency > 0.0;
			double result;
			if (flag)
			{
				result = externalFrequency;
			}
			else
			{
				double y = (double)((ulong)this.Dut.ReadUnsigned(this.Reg["SMPL_PRD"]) & 65280UL) / 256.0;
				result = 819.2 / Math.Pow(2.0, y);
			}
			return result;
		}

		/// <summary>
		/// Reads RecInfo1 ,2 , DiagStat, ALM_SAT, ALM Peak , .. . Converts values to csv format. Returns rows of strings.
		/// </summary>
		/// <param name="delim"></param>
		/// <returns></returns>
		// Token: 0x0600008A RID: 138 RVA: 0x0000515C File Offset: 0x0000335C
		public List<string> GetDataLogText(string delim)
		{
			List<string> list = new List<string>();
			int num = 4;
			bool flag = this.axisCount == 1;
			if (flag)
			{
				num = 12;
			}
			checked
			{
				int num2 = this.axisCount * 32 + num;
				bool flag2 = this.RecordMode == iVIBEcontrol.CapMode.realTimeDomain;
				if (flag2)
				{
					int num3 = (int)Math.Round((double)this.BurstData.Count / (double)num2);
					int num4 = 0;
					int num5 = 0;
					int num6 = num3;
					for (int i = 1; i <= num6; i++)
					{
						bool flag3 = this.axisCount == 1;
						if (flag3)
						{
							num5 = 8;
							int num7 = 0;
							do
							{
								list.Add("pad " + (num7 + 1).ToString() + delim + this.BurstData[num4 + num7].ToString());
								num7++;
							}
							while (num7 <= 7);
						}
						list.Add("Marker " + i.ToString() + delim + this.BurstData[num4 + num5].ToString());
						int num8 = 1;
						do
						{
							list.Add("X" + delim + this.BurstData[num4 + num5 + num8].ToString());
							num8++;
						}
						while (num8 <= 32);
						bool flag4 = this.axisCount > 1;
						if (flag4)
						{
							int num9 = 1;
							do
							{
								list.Add("Y" + delim + this.BurstData[num4 + num5 + num9 + 32].ToString());
								num9++;
							}
							while (num9 <= 32);
						}
						bool flag5 = this.axisCount > 2;
						if (flag5)
						{
							int num10 = 1;
							do
							{
								list.Add("Z" + delim + this.BurstData[num4 + num5 + num10 + 64].ToString());
								num10++;
							}
							while (num10 <= 32);
						}
						list.Add("Temperature" + delim + this.BurstData[num4 + (num2 - 3)].ToString());
						list.Add("Status" + delim + this.BurstData[num4 + (num2 - 2)].ToString());
						list.Add("CRC" + delim + this.BurstData[num4 + (num2 - 1)].ToString());
						num4 += num2;
					}
				}
				else
				{
					list.Add("REC_INFO1" + delim + this.Dut.ReadUnsigned(this.Reg["REC_INFO1"]).ToString());
					list.Add("REC_INFO2" + delim + this.Dut.ReadUnsigned(this.Reg["REC_INFO2"]).ToString());
					list.Add("DIAG_STAT" + delim + this.Dut.ReadUnsigned(this.Reg["STATUS"]).ToString());
					list.Add("ALM_X_STAT" + delim + this.Dut.ReadUnsigned(this.Reg["ALM_X_STAT"]).ToString());
					list.Add("ALM_Y_STAT" + delim + this.Dut.ReadUnsigned(this.Reg["ALM_Y_STAT"]).ToString());
					list.Add("ALM_Z_STAT" + delim + this.Dut.ReadUnsigned(this.Reg["ALM_Z_STAT"]).ToString());
					list.Add("ALM_X_PEAK" + delim + this.Dut.ReadUnsigned(this.Reg["ALM_X_PEAK"]).ToString());
					list.Add("ALM_Y_PEAK" + delim + this.Dut.ReadUnsigned(this.Reg["ALM_Y_PEAK"]).ToString());
					list.Add("ALM_Z_PEAK" + delim + this.Dut.ReadUnsigned(this.Reg["ALM_Z_PEAK"]).ToString());
					list.Add("ALM_X_FREQ" + delim + this.Dut.ReadUnsigned(this.Reg["ALM_X_FREQ"]).ToString());
					list.Add("ALM_Y_FREQ" + delim + this.Dut.ReadUnsigned(this.Reg["ALM_Y_FREQ"]).ToString());
					list.Add("ALM_Z_FREQ" + delim + this.Dut.ReadUnsigned(this.Reg["ALM_Z_FREQ"]).ToString());
					list.Add("TIME_STAMP_H" + delim + this.Dut.ReadUnsigned(this.Reg["TIME_STAMP_H"]).ToString());
					list.Add("TIME_STAMP_L" + delim + this.Dut.ReadUnsigned(this.Reg["TIME_STAMP_L"]).ToString());
					list.Add(string.Concat(new string[]
					{
						"Sample #",
						delim,
						"X axis",
						delim,
						"Y axis",
						delim,
						"Z axis"
					}));
					int num11 = this.BufferWidth - 1;
					for (int j = 0; j <= num11; j++)
					{
						bool scaleData = this.ScaleData;
						double num12;
						double num13;
						double num14;
						if (scaleData)
						{
							num12 = this.Sensor[this.SensorSelected].dataX[j];
							num13 = this.Sensor[this.SensorSelected].dataY[j];
							num14 = this.Sensor[this.SensorSelected].dataZ[j];
						}
						else
						{
							num12 = this.Sensor[this.SensorSelected].dataXui[j];
							num13 = this.Sensor[this.SensorSelected].dataYui[j];
							num14 = this.Sensor[this.SensorSelected].dataZui[j];
						}
						list.Add(string.Concat(new string[]
						{
							(j + 1).ToString(),
							delim,
							num12.ToString(),
							delim,
							num13.ToString(),
							delim,
							num14.ToString()
						}));
					}
				}
				return list;
			}
		}

		/// <summary>
		/// Returns product number stored in PROD_ID
		/// </summary>
		/// <returns></returns>
		/// <remarks></remarks>
		// Token: 0x0600008B RID: 139 RVA: 0x0000581C File Offset: 0x00003A1C
		public virtual int GetProductNumber()
		{
			return checked((int)this.Dut.ReadUnsigned(this.Reg["PROD_ID"]));
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000584C File Offset: 0x00003A4C
		public void UpdateFlash()
		{
			this.Dut.WriteUnsigned(this.Reg[this.flashReg], this.flashWord);
			Thread.Sleep(this.flashDelay);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005880 File Offset: 0x00003A80
		public virtual void SetupDataReady(int dio, bool Enable, bool Polarity)
		{
			bool flag = !(1 <= dio & dio <= 2);
			if (flag)
			{
				throw new ArgumentException("DIO must be in the range 1 to 2.");
			}
			uint num = this.Dut.ReadUnsigned(this.Reg["MSC_CTRL"]) & 65528U;
			if (Enable)
			{
				num |= 4U;
			}
			if (Polarity)
			{
				num |= 2U;
			}
			num |= checked((uint)(dio - 1));
			this.Dut.WriteUnsigned(this.Reg["MSC_CTRL"], num);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003198 File Offset: 0x00001398
		public virtual void ExternalTriggerEnabled(bool enable)
		{
		}

		/// <summary>
		/// software reset the DUT firmware through Command register
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x0600008F RID: 143 RVA: 0x00005907 File Offset: 0x00003B07
		public void ResetDUT()
		{
			this.Dut.WriteUnsigned(this.Reg["COMMAND"], 128U);
		}

		/// <summary>
		/// set register bit field values defined in CmdDataFile_16xxx.csv
		/// </summary>
		/// <param name="cmdKey"></param>
		/// <remarks></remarks>
		// Token: 0x06000090 RID: 144 RVA: 0x0000592C File Offset: 0x00003B2C
		public virtual void writeCommand(string cmdKey)
		{
			try
			{
				CommandClass commandClass = this.cmdCollection[cmdKey];
				RegClass reg = this.Reg[commandClass.RegLabel];
				bool flag = commandClass.Delay > 500;
				this.Dut.WriteCommand(reg, commandClass);
			}
			catch (Exception ex)
			{
				string str = "\r\nADcmXL021control.vb writeCommand (" + cmdKey + ") was not executed.";
				Interaction.MsgBox(ex.Message + str, MsgBoxStyle.Critical, null);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000059C4 File Offset: 0x00003BC4
		public bool waitForGPIO()
		{
			Interaction.MsgBox("ADcmXL021control.vb Sub waitForGpio is no longer implemented.", MsgBoxStyle.Critical, null);
			return false;
		}

		/// <summary>
		/// Uses Spi.ReadPin. Cancel avalible if ran in background worker.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000092 RID: 146 RVA: 0x000059E8 File Offset: 0x00003BE8
		public bool DrPollPinHigh(BackgroundWorker bgWorker)
		{
			bool cancellationPending = bgWorker.CancellationPending;
			bool result;
			if (cancellationPending)
			{
				result = false;
			}
			else
			{
				bool flag = false;
				GlobalDeclarations.BoardIDtype boardID = GlobalDeclarations.BoardID;
				if (boardID != GlobalDeclarations.BoardIDtype.SDPEVAL)
				{
					if (boardID != GlobalDeclarations.BoardIDtype.FX3)
					{
						Interaction.MsgBox("ADcmXl021 module, sub DRPollPinHigh, Unknown board type" + Conversions.ToString((int)GlobalDeclarations.BoardID), MsgBoxStyle.OkOnly, null);
					}
					else
					{
						while (!flag & !this.userCancel & !bgWorker.CancellationPending)
						{
							try
							{
								uint num = GlobalDeclarations.FX3comm.ReadPin(GlobalDeclarations.FX3comm.DIO2);
								bool flag2 = (ulong)num == 1UL;
								if (flag2)
								{
									flag = true;
								}
								Thread.Sleep(1);
							}
							catch (Exception ex)
							{
								string str = "ADcmXl021 module, sub DRPollPinHigh.";
								str = str + "\n" + ex.Message;
								Interaction.MsgBox("", MsgBoxStyle.OkOnly, null);
							}
						}
					}
				}
				result = !this.userCancel;
			}
			return result;
		}

		/// <summary>
		/// Uses Spi.ReadPin. Cancel avalible if ran in background worker.
		/// </summary>
		/// <returns></returns>
		// Token: 0x06000093 RID: 147 RVA: 0x00005AF8 File Offset: 0x00003CF8
		public bool DrPollPinLow(BackgroundWorker bgWorker)
		{
			bool cancellationPending = bgWorker.CancellationPending;
			bool result;
			if (cancellationPending)
			{
				result = false;
			}
			else
			{
				bool flag = true;
				Thread.Sleep(1);
				while (flag & !this.userCancel & !bgWorker.CancellationPending)
				{
					GlobalDeclarations.BoardIDtype boardID = GlobalDeclarations.BoardID;
					if (boardID != GlobalDeclarations.BoardIDtype.SDPEVAL)
					{
						if (boardID == GlobalDeclarations.BoardIDtype.FX3)
						{
							try
							{
								flag = (GlobalDeclarations.FX3comm.ReadPin(GlobalDeclarations.FX3comm.ReadyPin) > 0U);
								Thread.Sleep(1);
							}
							catch (Exception ex)
							{
								string str = "ADcmXl021 module, sub DRPollPinLow.";
								str = str + "\n" + ex.Message;
								Interaction.MsgBox("", MsgBoxStyle.OkOnly, null);
							}
						}
					}
				}
				result = !this.userCancel;
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public void SensorAdd(int sNum)
		{
			Interaction.MsgBox("ADcmXL021control.vb cannot add sensors", MsgBoxStyle.Critical, null);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005BE5 File Offset: 0x00003DE5
		public void SensorRemovePreserveID(int sNum)
		{
			Interaction.MsgBox("ADcmXL021control.vb cannot remove sensors", MsgBoxStyle.Critical, null);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005BF6 File Offset: 0x00003DF6
		public void SensorEraseNetID(int sNum)
		{
			Interaction.MsgBox("ADcmXL021control.vb cannot release sensors", MsgBoxStyle.Critical, null);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005C07 File Offset: 0x00003E07
		public void SensorDetectAll()
		{
			Interaction.MsgBox("ADcmXL021control.vb detect all sensors", MsgBoxStyle.Critical, null);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005C18 File Offset: 0x00003E18
		public bool checkCRC(int[] burstData)
		{
			byte[] array = new byte[192];
			int num = 0;
			int num2 = 1;
			checked
			{
				do
				{
					array[num + 1] = (byte)(burstData[num2] & 255);
					array[num] = (byte)((burstData[num2] & 65280) >> 8);
					num += 2;
					num2++;
				}
				while (num2 <= 96);
				uint num3 = this.calcCCITT16(array);
				bool result = false;
				bool flag = num3 == (uint)array[99];
				if (flag)
				{
					result = true;
				}
				return result;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005C88 File Offset: 0x00003E88
		public uint calcCCITT16(byte[] byt)
		{
			uint num = 65535U;
			uint num2 = 4129U;
			int num3 = 0;
			do
			{
				num ^= (uint)((byte)(byt[num3] << (8 & 7)));
				int num4 = 1;
				checked
				{
					do
					{
						bool flag = (unchecked((ulong)num) & 32768UL) == 32768UL;
						if (flag)
						{
							num <<= 1;
							num ^= num2;
						}
						else
						{
							num <<= 1;
						}
						num = (uint)(unchecked((ulong)num) & 65535UL);
						num4++;
					}
					while (num4 <= 8);
					num3++;
				}
			}
			while (num3 <= 8);
			return num;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00005D04 File Offset: 0x00003F04
		public int bcd2dec(uint word)
		{
			int num = 0;
			uint num2 = 0U;
			checked
			{
				for (;;)
				{
					int num3 = (int)(word & 15U);
					bool flag = num3 > 9;
					if (flag)
					{
						break;
					}
					num += num3 * (int)Math.Round(Math.Pow(10.0, num2));
					word >>= 4;
					num2 += 1U;
					if (num2 > 3U)
					{
						goto Block_2;
					}
				}
				return -1;
				Block_2:
				return num;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00005D5C File Offset: 0x00003F5C
		public ushort dec2bcd(int dec)
		{
			ushort num = 0;
			bool flag = dec < 0 | dec > 9999;
			if (flag)
			{
				throw new ArgumentException("Value passed to dec2bcd out of range. Valid range is 0<=dec<=9999");
			}
			int num2 = 0;
			do
			{
				ushort num3 = Convert.ToUInt16(dec % 10);
				num |= (ushort)(num3 << (checked(num2 * 4) & 15));
				dec /= 10;
				checked
				{
					num2++;
				}
			}
			while (num2 <= 3);
			return num;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00005DB9 File Offset: 0x00003FB9
		protected void Delay(int ms)
		{
			Thread.Sleep(ms);
		}

		// Token: 0x04000012 RID: 18
		protected IDutInterface Dut;

		// Token: 0x04000013 RID: 19
		protected RegMapCollection Reg;

		// Token: 0x04000014 RID: 20
		protected string flashReg;

		// Token: 0x04000015 RID: 21
		protected uint flashWord;

		// Token: 0x04000016 RID: 22
		protected int flashDelay;

		// Token: 0x04000017 RID: 23
		protected CommandCollection cmdCollection;

		// Token: 0x04000018 RID: 24
		protected AlarmsClass _alarms;

		// Token: 0x04000019 RID: 25
		protected iVIBEcontrol.RecInfoClass _recinfo;

		// Token: 0x0400001A RID: 26
		protected iVIBEcontrol.PlotClass _plot;

		// Token: 0x0400001B RID: 27
		public iVIBEcontrol.AlarmStatusClass[] AlarmStatus;

		// Token: 0x0400001C RID: 28
		private Gpio GPIO;

		// Token: 0x0400001D RID: 29
		private gpioBit ioPin;

		// Token: 0x0400001E RID: 30
		private PinFcns BFpinFunc;

		// Token: 0x04000020 RID: 32
		private RunAsyncCompletedEventArgs RunAsyncCompletedEventArgs;

		// Token: 0x04000021 RID: 33
		private List<ushort> BurstData;

		// Token: 0x04000022 RID: 34
		private bool _LogScale;

		// Token: 0x04000023 RID: 35
		private bool _ExtTrigger;

		// Token: 0x04000025 RID: 37
		private double _plotFmax;

		// Token: 0x04000026 RID: 38
		private bool _ScaleData;

		// Token: 0x04000027 RID: 39
		private bool _userCancel;

		// Token: 0x04000028 RID: 40
		private double _upDateIntervalSec;

		// Token: 0x04000029 RID: 41
		private int _statusCode;

		// Token: 0x0400002A RID: 42
		private iVIBEcontrol.axis _axisSelected;

		/// <summary>
		/// The User interface, FormMain should update this value. Values are defined in the DeviceCatalog.csv file.
		/// </summary>
		// Token: 0x0400002B RID: 43
		private string _ProductSelected;

		// Token: 0x0400002C RID: 44
		private int _axisCount;

		// Token: 0x0400002D RID: 45
		private iVIBEcontrol.SensorClass[] _sensor;

		// Token: 0x0400002E RID: 46
		private AdisBase _SDP;

		// Token: 0x0400002F RID: 47
		private string _SpiConfig;

		// Token: 0x04000030 RID: 48
		private int _sensorSelected;

		// Token: 0x04000031 RID: 49
		private iVIBEcontrol.SensorOnNetworkClass _SensorsOnNetwork;

		// Token: 0x04000032 RID: 50
		private FX3Connection _fxspi;

		// Token: 0x04000034 RID: 52
		protected iVIBEcontrol.CapMode _recordMode;

		// Token: 0x04000035 RID: 53
		protected int _bufferWidth;
	}
}
