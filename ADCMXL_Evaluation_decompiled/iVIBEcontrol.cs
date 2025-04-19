using System;
using System.Collections.Generic;
using System.ComponentModel;
using AdisApi;
using FX3Api;
using RegMapClasses;

namespace Vibration_Evaluation
{
	// Token: 0x0200001F RID: 31
	public interface iVIBEcontrol
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000306 RID: 774
		// (set) Token: 0x06000307 RID: 775
		bool powerDownConfig { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000308 RID: 776
		// (set) Token: 0x06000309 RID: 777
		int recPrdSec { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600030A RID: 778
		// (set) Token: 0x0600030B RID: 779
		AdisBase dConSdp { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600030C RID: 780
		// (set) Token: 0x0600030D RID: 781
		bool LogScale { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600030E RID: 782
		// (set) Token: 0x0600030F RID: 783
		double plotFmax { get; set; }

		/// <summary>
		/// For derivative product groups. The user selected specific product. 
		/// </summary>
		/// <returns></returns>
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000310 RID: 784
		// (set) Token: 0x06000311 RID: 785
		string ProductSelected { get; set; }

		/// <summary>
		/// Sets SPI signals to GPIO pins "5MHz" or SPI Peripheral pins "30MHz".
		/// </summary>
		/// <returns></returns>
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000312 RID: 786
		// (set) Token: 0x06000313 RID: 787
		string SpiConfig { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000314 RID: 788
		// (set) Token: 0x06000315 RID: 789
		AlarmsClass Alarms { get; set; }

		// Token: 0x06000316 RID: 790
		List<uint> AlarmStatusRead();

		// Token: 0x06000317 RID: 791
		AlarmsClass.LsbValuesArray AlarmsReadSRO(int SROpt);

		// Token: 0x06000318 RID: 792
		AlarmsClass.LsbValuesArray[] AlarmsReadAll();

		// Token: 0x06000319 RID: 793
		void AlarmsWrite(AlarmsClass.LsbValuesArray[] aLsbs);

		/// <summary>
		/// Sends Start/Stop code to DUT. Normal called from a background thread.
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x0600031A RID: 794
		void StartCmd(BackgroundWorker bgWorker);

		/// <summary>
		/// Sends Stop commnds to the DUT. May be dependent on the Record Mode.
		/// </summary>
		// Token: 0x0600031B RID: 795
		void StopCmd();

		// Token: 0x0600031C RID: 796
		void getBurstData(int count);

		// Token: 0x0600031D RID: 797
		bool REC_PRD_check();

		// Token: 0x0600031E RID: 798
		void EnableExternalTrigger(bool TF);

		// Token: 0x0600031F RID: 799
		int ProductNumberGet();

		// Token: 0x06000320 RID: 800
		double[] readSelectedAxis(IEnumerable<RegClass> reglist, uint nSamples);

		/// <summary>
		/// Stores Record Mode in Device Object. Writes mode to the DUT.
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000321 RID: 801
		// (set) Token: 0x06000322 RID: 802
		iVIBEcontrol.CapMode RecordMode { get; set; }

		/// <summary>
		/// True causes a start Capture command to wait on a High on DIO3. Port H pin 3
		/// </summary>
		/// <returns></returns>
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000323 RID: 803
		// (set) Token: 0x06000324 RID: 804
		bool ExtTrigger { get; set; }

		// Token: 0x06000325 RID: 805
		void ExtTriggerWait();

		/// <summary>
		/// Reads REC_INFO1 and REC_INFO2. Sets properties of Obj.
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x06000326 RID: 806
		void RecInfo_Read();

		// Token: 0x06000327 RID: 807
		void RecInfo_Decode(uint Info1, uint Info2);

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000328 RID: 808
		// (set) Token: 0x06000329 RID: 809
		iVIBEcontrol.RecInfoClass recInfo { get; set; }

		/// <summary>
		/// Reads REC_CTRL Sets RecordMode Property
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x0600032A RID: 810
		void RecordControl_Read();

		// Token: 0x0600032B RID: 811
		void ResetDut();

		// Token: 0x0600032C RID: 812
		double SampleRateGet(double externalFrequency = 0.0);

		// Token: 0x0600032D RID: 813
		void SetupDataReady(int dio, bool Enable, bool Polarity);

		// Token: 0x0600032E RID: 814
		void UpdateFlash();

		// Token: 0x0600032F RID: 815
		bool waitForCapture(BackgroundWorker bgWorker, bool TwoLevels = false);

		// Token: 0x06000330 RID: 816
		void writeCommand(string cmdKey);

		/// <summary>
		/// Checks Prod ID. Sets Sensor count. Sets Burst Mode parameters.
		/// </summary>
		/// <param name="prodNumber"></param>
		/// <remarks></remarks>
		// Token: 0x06000331 RID: 817
		void initializeDUT(int prodNumber, ref FX3Connection Spi);

		// Token: 0x06000332 RID: 818
		void RealTimeSamplingStart();

		// Token: 0x06000333 RID: 819
		void writeEscape();

		/// <summary>
		/// Resets all active sensors on network to manual mode.
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000334 RID: 820
		// (set) Token: 0x06000335 RID: 821
		int sensorSelected { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000336 RID: 822
		// (set) Token: 0x06000337 RID: 823
		FX3Connection fxSpi { get; set; }

		// Token: 0x06000338 RID: 824
		void SensorAdd(int num);

		// Token: 0x06000339 RID: 825
		void SensorRemovePreserveID(int num);

		// Token: 0x0600033A RID: 826
		void SensorEraseNetID(int sNum);

		// Token: 0x0600033B RID: 827
		void SensorDetectAll();

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600033C RID: 828
		// (set) Token: 0x0600033D RID: 829
		iVIBEcontrol.SensorOnNetworkClass SensorsOnNetwork { get; set; }

		/// <summary>
		/// 228 send escape code, 16K write stop and set manual mode
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x0600033E RID: 830
		void RealTimeStop();

		// Token: 0x0600033F RID: 831
		void PeriodicModeSet(int upDateSec);

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000340 RID: 832
		// (set) Token: 0x06000341 RID: 833
		int BufferWidth { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000342 RID: 834
		// (set) Token: 0x06000343 RID: 835
		iVIBEcontrol.SensorClass[] Sensor { get; set; }

		// Token: 0x06000344 RID: 836
		bool waitForRealTimeData();

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000345 RID: 837
		// (set) Token: 0x06000346 RID: 838
		iVIBEcontrol.axis axisSelected { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000347 RID: 839
		// (set) Token: 0x06000348 RID: 840
		int axisCount { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000349 RID: 841
		// (set) Token: 0x0600034A RID: 842
		int statusCode { get; set; }

		// Token: 0x0600034B RID: 843
		List<string> GetDataLogText(string delim);

		/// <summary>
		/// Set False in StartC()
		/// </summary>
		/// <returns></returns>
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600034C RID: 844
		// (set) Token: 0x0600034D RID: 845
		bool userCancel { get; set; }

		// Token: 0x0600034E RID: 846
		bool waitForGPIO();

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600034F RID: 847
		// (set) Token: 0x06000350 RID: 848
		double upDateIntervalSec { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000351 RID: 849
		// (set) Token: 0x06000352 RID: 850
		bool ScaleData { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000353 RID: 851
		// (set) Token: 0x06000354 RID: 852
		iVIBEcontrol.PlotClass Plot { get; set; }

		// Token: 0x0200003B RID: 59
		public enum CapMode
		{
			// Token: 0x04000207 RID: 519
			manualFFT,
			// Token: 0x04000208 RID: 520
			AutomaticFFT,
			// Token: 0x04000209 RID: 521
			manualTimeDomain,
			// Token: 0x0400020A RID: 522
			realTimeDomain
		}

		// Token: 0x0200003C RID: 60
		public enum axis
		{
			// Token: 0x0400020C RID: 524
			x,
			// Token: 0x0400020D RID: 525
			y,
			// Token: 0x0400020E RID: 526
			z
		}

		// Token: 0x0200003D RID: 61
		public class PlotClass
		{
			// Token: 0x1700017C RID: 380
			// (get) Token: 0x06000474 RID: 1140 RVA: 0x0001C564 File Offset: 0x0001A764
			// (set) Token: 0x06000475 RID: 1141 RVA: 0x0001C57C File Offset: 0x0001A77C
			public float Xmax
			{
				get
				{
					return this._Xmax;
				}
				set
				{
					this._Xmax = value;
				}
			}

			// Token: 0x1700017D RID: 381
			// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001C588 File Offset: 0x0001A788
			// (set) Token: 0x06000477 RID: 1143 RVA: 0x0001C5A0 File Offset: 0x0001A7A0
			public float XscaleMax
			{
				get
				{
					return this._XscaleMax;
				}
				set
				{
					this._XscaleMax = value;
				}
			}

			// Token: 0x0400020F RID: 527
			private float _Xmax;

			// Token: 0x04000210 RID: 528
			private float _XscaleMax;
		}

		// Token: 0x0200003E RID: 62
		public class RecInfoClass
		{
			// Token: 0x1700017E RID: 382
			// (get) Token: 0x06000479 RID: 1145 RVA: 0x0001C5AC File Offset: 0x0001A7AC
			// (set) Token: 0x0600047A RID: 1146 RVA: 0x0001C5C4 File Offset: 0x0001A7C4
			public float Fmax
			{
				get
				{
					return this._Fmax;
				}
				set
				{
					this._Fmax = value;
				}
			}

			// Token: 0x1700017F RID: 383
			// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
			// (set) Token: 0x0600047C RID: 1148 RVA: 0x0001C5E8 File Offset: 0x0001A7E8
			public int RangeG
			{
				get
				{
					return this._RangeG;
				}
				set
				{
					this._RangeG = value;
				}
			}

			// Token: 0x17000180 RID: 384
			// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
			// (set) Token: 0x0600047E RID: 1150 RVA: 0x0001C60C File Offset: 0x0001A80C
			public int AvgCnt
			{
				get
				{
					return this._AvgCnt;
				}
				set
				{
					this._AvgCnt = value;
				}
			}

			// Token: 0x17000181 RID: 385
			// (get) Token: 0x0600047F RID: 1151 RVA: 0x0001C618 File Offset: 0x0001A818
			// (set) Token: 0x06000480 RID: 1152 RVA: 0x0001C630 File Offset: 0x0001A830
			public int sampleRateOpt
			{
				get
				{
					return this._sampleRateOpt;
				}
				set
				{
					this._sampleRateOpt = value;
				}
			}

			// Token: 0x04000211 RID: 529
			private int _AvgCnt;

			// Token: 0x04000212 RID: 530
			private float _Fmax;

			// Token: 0x04000213 RID: 531
			private int _RangeG;

			// Token: 0x04000214 RID: 532
			private int _RangeCode;

			// Token: 0x04000215 RID: 533
			private int _sampleRateOpt;
		}

		// Token: 0x0200003F RID: 63
		public class AlarmStatusClass
		{
			// Token: 0x06000481 RID: 1153 RVA: 0x0001C63C File Offset: 0x0001A83C
			public AlarmStatusClass()
			{
				this.Band = new iVIBEcontrol.AlarmStatusClass.BandClass[7];
				int num = 1;
				checked
				{
					do
					{
						this.Band[num] = new iVIBEcontrol.AlarmStatusClass.BandClass();
						num++;
					}
					while (num <= 6);
				}
			}

			// Token: 0x04000216 RID: 534
			public iVIBEcontrol.AlarmStatusClass.BandClass[] Band;

			// Token: 0x0200004D RID: 77
			public class BandClass
			{
				// Token: 0x060004DD RID: 1245 RVA: 0x0001D21A File Offset: 0x0001B41A
				public BandClass()
				{
					this.Level = new int[3];
					this.Level[1] = 0;
					this.Level[2] = 0;
				}

				// Token: 0x0400024E RID: 590
				public int[] Level;
			}
		}

		// Token: 0x02000040 RID: 64
		public class SensorOnNetworkClass
		{
			// Token: 0x06000482 RID: 1154 RVA: 0x0001C674 File Offset: 0x0001A874
			public SensorOnNetworkClass()
			{
				this._sAct = new bool[7];
				this._sNewData = new bool[7];
				this.ClearAllToNotActive();
			}

			// Token: 0x17000182 RID: 386
			// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001C6A0 File Offset: 0x0001A8A0
			public int countActive
			{
				get
				{
					return this._countActive;
				}
			}

			// Token: 0x06000484 RID: 1156 RVA: 0x0001C6B8 File Offset: 0x0001A8B8
			public bool IsActive(int sID)
			{
				return this._sAct[sID];
			}

			// Token: 0x06000485 RID: 1157 RVA: 0x0001C6D4 File Offset: 0x0001A8D4
			public void setActive(int sID)
			{
				bool flag = !this._sAct[sID];
				checked
				{
					if (flag)
					{
						this._sAct[sID] = true;
						this._countActive++;
					}
				}
			}

			// Token: 0x06000486 RID: 1158 RVA: 0x0001C70C File Offset: 0x0001A90C
			public void setNotActive(int sID)
			{
				bool flag = this._sAct[sID];
				checked
				{
					if (flag)
					{
						this._sAct[sID] = false;
						this._countActive--;
					}
				}
			}

			// Token: 0x06000487 RID: 1159 RVA: 0x0001C740 File Offset: 0x0001A940
			public void ClearAllToNotActive()
			{
				int num = 1;
				checked
				{
					do
					{
						this._sAct[num] = false;
						num++;
					}
					while (num <= 6);
					this._countActive = 0;
				}
			}

			// Token: 0x17000183 RID: 387
			// (get) Token: 0x06000488 RID: 1160 RVA: 0x0001C768 File Offset: 0x0001A968
			public int countNewData
			{
				get
				{
					return this._countNewData;
				}
			}

			// Token: 0x06000489 RID: 1161 RVA: 0x0001C780 File Offset: 0x0001A980
			public bool hasNewData(int sID)
			{
				return this._sNewData[sID];
			}

			// Token: 0x0600048A RID: 1162 RVA: 0x0001C79C File Offset: 0x0001A99C
			public void setNewData(int sID)
			{
				bool flag = !this._sNewData[sID];
				checked
				{
					if (flag)
					{
						this._sNewData[sID] = true;
						this._countNewData++;
					}
				}
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x0001C7D4 File Offset: 0x0001A9D4
			public void setNotNewData(int sID)
			{
				bool flag = this._sNewData[sID];
				checked
				{
					if (flag)
					{
						this._sNewData[sID] = false;
						this._countNewData--;
					}
				}
			}

			// Token: 0x0600048C RID: 1164 RVA: 0x0001C808 File Offset: 0x0001AA08
			public void ClearAllToNotNewData()
			{
				int num = 1;
				checked
				{
					do
					{
						this._sNewData[num] = false;
						num++;
					}
					while (num <= 6);
					this._countNewData = 0;
				}
			}

			// Token: 0x04000217 RID: 535
			private int _countActive;

			// Token: 0x04000218 RID: 536
			private bool[] _sAct;

			// Token: 0x04000219 RID: 537
			private int _countNewData;

			// Token: 0x0400021A RID: 538
			private bool[] _sNewData;
		}

		/// <summary>
		/// Stores data For Each sensor, On a network(16000)
		/// </summary>
		// Token: 0x02000041 RID: 65
		public class SensorClass
		{
			// Token: 0x0600048D RID: 1165 RVA: 0x0001C830 File Offset: 0x0001AA30
			public SensorClass()
			{
				this._dataX = new double[4096];
				this._dataY = new double[4096];
				this._dataZ = new double[4096];
				this._dataXui = new uint[4096];
				this._dataYui = new uint[4096];
				this._dataZui = new uint[4096];
			}

			// Token: 0x0600048E RID: 1166 RVA: 0x0001C8A4 File Offset: 0x0001AAA4
			public void dataClearAll()
			{
				int num = 0;
				checked
				{
					do
					{
						this._dataX[num] = double.NaN;
						this._dataY[num] = double.NaN;
						this._dataZ[num] = double.NaN;
						num++;
					}
					while (num <= 4095);
				}
			}

			// Token: 0x17000184 RID: 388
			// (get) Token: 0x0600048F RID: 1167 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
			// (set) Token: 0x06000490 RID: 1168 RVA: 0x0001C90C File Offset: 0x0001AB0C
			public double[] dataX
			{
				get
				{
					return this._dataX;
				}
				set
				{
					this._dataX = value;
				}
			}

			// Token: 0x17000185 RID: 389
			// (get) Token: 0x06000491 RID: 1169 RVA: 0x0001C918 File Offset: 0x0001AB18
			// (set) Token: 0x06000492 RID: 1170 RVA: 0x0001C930 File Offset: 0x0001AB30
			public double[] dataY
			{
				get
				{
					return this._dataY;
				}
				set
				{
					this._dataY = value;
				}
			}

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x06000493 RID: 1171 RVA: 0x0001C93C File Offset: 0x0001AB3C
			// (set) Token: 0x06000494 RID: 1172 RVA: 0x0001C954 File Offset: 0x0001AB54
			public double[] dataZ
			{
				get
				{
					return this._dataZ;
				}
				set
				{
					this._dataZ = value;
				}
			}

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x06000495 RID: 1173 RVA: 0x0001C960 File Offset: 0x0001AB60
			// (set) Token: 0x06000496 RID: 1174 RVA: 0x0001C978 File Offset: 0x0001AB78
			public uint[] dataXui
			{
				get
				{
					return this._dataXui;
				}
				set
				{
					this._dataXui = value;
				}
			}

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x06000497 RID: 1175 RVA: 0x0001C984 File Offset: 0x0001AB84
			// (set) Token: 0x06000498 RID: 1176 RVA: 0x0001C99C File Offset: 0x0001AB9C
			public uint[] dataYui
			{
				get
				{
					return this._dataYui;
				}
				set
				{
					this._dataYui = value;
				}
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000499 RID: 1177 RVA: 0x0001C9A8 File Offset: 0x0001ABA8
			// (set) Token: 0x0600049A RID: 1178 RVA: 0x0001C9C0 File Offset: 0x0001ABC0
			public uint[] dataZui
			{
				get
				{
					return this._dataZui;
				}
				set
				{
					this._dataZui = value;
				}
			}

			// Token: 0x0400021B RID: 539
			private double[] _dataX;

			// Token: 0x0400021C RID: 540
			private double[] _dataY;

			// Token: 0x0400021D RID: 541
			private double[] _dataZ;

			// Token: 0x0400021E RID: 542
			private uint[] _dataXui;

			// Token: 0x0400021F RID: 543
			private uint[] _dataYui;

			// Token: 0x04000220 RID: 544
			private uint[] _dataZui;
		}
	}
}
