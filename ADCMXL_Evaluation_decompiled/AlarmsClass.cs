using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;

namespace Vibration_Evaluation
{
	// Token: 0x0200000A RID: 10
	public class AlarmsClass
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00005DC4 File Offset: 0x00003FC4
		public AlarmsClass()
		{
			this.LsbValues = new AlarmsClass.LsbValuesArray[4];
			this.ScaledValues = new AlarmsClass.scaledValuesArray[4];
			this.scaleFactor = new AlarmsClass.scaleFactorType[4];
			this.AxisValues_X = new AlarmsClass.AxisClass();
			this.AxisValues_Y = new AlarmsClass.AxisClass();
			this.AxisValues_Z = new AlarmsClass.AxisClass();
			this.header = new string[11];
			this.LsbValues[0] = new AlarmsClass.LsbValuesArray();
			this.LsbValues[1] = new AlarmsClass.LsbValuesArray();
			this.LsbValues[2] = new AlarmsClass.LsbValuesArray();
			this.LsbValues[3] = new AlarmsClass.LsbValuesArray();
			this.ScaledValues[0] = new AlarmsClass.scaledValuesArray();
			this.ScaledValues[1] = new AlarmsClass.scaledValuesArray();
			this.ScaledValues[2] = new AlarmsClass.scaledValuesArray();
			this.ScaledValues[3] = new AlarmsClass.scaledValuesArray();
			this.scaleFactor[0] = new AlarmsClass.scaleFactorType();
			this.scaleFactor[1] = new AlarmsClass.scaleFactorType();
			this.scaleFactor[2] = new AlarmsClass.scaleFactorType();
			this.scaleFactor[3] = new AlarmsClass.scaleFactorType();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00005EC8 File Offset: 0x000040C8
		public void FileSelectOpen()
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			string filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			string defaultExt = "csv";
			openFileDialog.Filter = filter;
			openFileDialog.DefaultExt = defaultExt;
			openFileDialog.FileName = "Alarm_Spectrum";
			bool flag = openFileDialog.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				this.ReadFromFile(openFileDialog.FileName);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00005F20 File Offset: 0x00004120
		private StreamReader OpenFileForRead(string filename)
		{
			StreamReader result = null;
			bool flag;
			do
			{
				try
				{
					flag = true;
					result = new StreamReader(filename);
				}
				catch (Exception ex)
				{
					flag = false;
					bool flag2 = MessageBox.Show("Cannot Open Alarrm setting file" + filename + ".", "   Error", MessageBoxButtons.RetryCancel) != DialogResult.Retry;
					if (flag2)
					{
						Interaction.MsgBox("Alarms could not be loaded.", MsgBoxStyle.OkOnly, null);
						return null;
					}
				}
			}
			while (!flag);
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00005FB0 File Offset: 0x000041B0
		public void ReadFromResources(string resText)
		{
			string[] array = resText.Split(new char[]
			{
				'\n'
			});
			checked
			{
				bool flag = array[array.Count<string>() - 1].Length < 1;
				if (flag)
				{
					array = (string[])Utils.CopyArray(array, new string[array.Length - 2 + 1]);
				}
				this.ParseTextToOBJ(array);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006008 File Offset: 0x00004208
		public void ReadFromFile(string pathFileName)
		{
			checked
			{
				try
				{
					StreamReader streamReader = this.OpenFileForRead(pathFileName);
					string[] array = new string[1];
					int num = 0;
					for (;;)
					{
						bool endOfStream = streamReader.EndOfStream;
						if (endOfStream)
						{
							break;
						}
						array = (string[])Utils.CopyArray(array, new string[num + 1]);
						array[num] = streamReader.ReadLine();
						num++;
					}
					this.ParseTextToOBJ(array);
					streamReader.Close();
				}
				catch (Exception ex)
				{
					string prompt = "Error: AlarmsClass.ReadFromFile\r\n" + ex.Message;
					Interaction.MsgBox(prompt, MsgBoxStyle.OkOnly, null);
				}
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000060A8 File Offset: 0x000042A8
		private void ParseTextToOBJ(string[] lineCLL)
		{
			List<RegClass> list = new List<RegClass>();
			List<uint> list2 = new List<uint>();
			string text = lineCLL[0];
			string[] array = text.Split(new char[]
			{
				','
			});
			array.CopyTo(this.header, 0);
			int num = 1;
			int num2 = 0;
			checked
			{
				int num3 = lineCLL.Length - 1;
				for (int i = 1; i <= num3; i++)
				{
					text = lineCLL[i];
					array = text.Split(new char[]
					{
						','
					});
					int num4 = Conversions.ToInteger(array[0].Trim());
					this.LsbValues[num2].frequencyLow[num] = Conversions.ToUInteger(array[2].Trim());
					this.LsbValues[num2].frequencyHigh[num] = Conversions.ToUInteger(array[3].Trim());
					this.LsbValues[num2].X1[num] = Conversions.ToUInteger(array[4].Trim());
					this.LsbValues[num2].X2[num] = Conversions.ToUInteger(array[5].Trim());
					this.LsbValues[num2].Y1[num] = Conversions.ToUInteger(array[6].Trim());
					this.LsbValues[num2].Y2[num] = Conversions.ToUInteger(array[7].Trim());
					this.LsbValues[num2].Z1[num] = Conversions.ToUInteger(array[8].Trim());
					this.LsbValues[num2].Z2[num] = Conversions.ToUInteger(array[9].Trim());
					num++;
					bool flag = num == 7;
					if (flag)
					{
						num = 1;
						num2++;
					}
				}
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00006250 File Offset: 0x00004450
		private StreamWriter OpenFileForWrite(string filename)
		{
			StreamWriter result = null;
			bool flag3;
			do
			{
				bool flag;
				try
				{
					flag = true;
					result = new StreamWriter(filename);
				}
				catch (Exception ex)
				{
					flag = false;
					bool flag2 = MessageBox.Show("Can not Open register file" + filename + ".\r\n\r\n" + Conversions.ToString(5)) != DialogResult.Retry;
					if (flag2)
					{
						Interaction.MsgBox("Registers can not be saved.", MsgBoxStyle.OkOnly, null);
						return null;
					}
				}
				flag3 = flag;
			}
			while (!flag3);
			return result;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000062DC File Offset: 0x000044DC
		public void CalcScaledData()
		{
			int num = 0;
			checked
			{
				do
				{
					this.CalcScaledData(num);
					num++;
				}
				while (num <= 0);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000062FC File Offset: 0x000044FC
		public void CalcScaledData(int sro)
		{
			int num = 1;
			do
			{
				this.ScaledValues[sro].frequencyLow[num] = this.LsbValues[sro].frequencyLow[num] * (this.scaleFactor[sro].Fmax / 2047.0);
				this.ScaledValues[sro].frequencyHigh[num] = this.LsbValues[sro].frequencyHigh[num] * (this.scaleFactor[sro].Fmax / 2047.0);
				this.ScaledValues[sro].X1[num] = this.LsbValues[sro].X1[num];
				this.ScaledValues[sro].X2[num] = this.LsbValues[sro].X2[num];
				this.ScaledValues[sro].Y1[num] = this.LsbValues[sro].Y1[num];
				this.ScaledValues[sro].Y2[num] = this.LsbValues[sro].Y2[num];
				this.ScaledValues[sro].Z1[num] = this.LsbValues[sro].Z1[num];
				this.ScaledValues[sro].Z2[num] = this.LsbValues[sro].Z2[num];
				checked
				{
					num++;
				}
			}
			while (num <= 6);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006448 File Offset: 0x00004648
		public void CalcPlotAxisData(int sampleRateOpt, double Range, double Fmax)
		{
			this.scaleFactor[sampleRateOpt].magniFS = Range;
			this.scaleFactor[sampleRateOpt].Fmax = Fmax;
			this.CalcScaledData(sampleRateOpt);
			int num = 1;
			checked
			{
				do
				{
					this.AxisValues_X.level1[num] = this.ScaledValues[sampleRateOpt].X1[num];
					this.AxisValues_X.level2[num] = this.ScaledValues[sampleRateOpt].X2[num];
					this.AxisValues_X.frequencyLow[num] = this.ScaledValues[sampleRateOpt].frequencyLow[num];
					this.AxisValues_X.frequencyHigh[num] = this.ScaledValues[sampleRateOpt].frequencyHigh[num];
					this.AxisValues_X.range = this.scaleFactor[sampleRateOpt].magniFS;
					this.AxisValues_X.fMax = this.scaleFactor[sampleRateOpt].Fmax;
					this.AxisValues_Y.level1[num] = this.ScaledValues[sampleRateOpt].Y1[num];
					this.AxisValues_Y.level2[num] = this.ScaledValues[sampleRateOpt].Y2[num];
					this.AxisValues_Y.frequencyLow[num] = this.ScaledValues[sampleRateOpt].frequencyLow[num];
					this.AxisValues_Y.frequencyHigh[num] = this.ScaledValues[sampleRateOpt].frequencyHigh[num];
					this.AxisValues_Y.range = this.scaleFactor[sampleRateOpt].magniFS;
					this.AxisValues_Y.fMax = this.scaleFactor[sampleRateOpt].Fmax;
					this.AxisValues_Z.level1[num] = this.ScaledValues[sampleRateOpt].Z1[num];
					this.AxisValues_Z.level2[num] = this.ScaledValues[sampleRateOpt].Z2[num];
					this.AxisValues_Z.frequencyLow[num] = this.ScaledValues[sampleRateOpt].frequencyLow[num];
					this.AxisValues_Z.frequencyHigh[num] = this.ScaledValues[sampleRateOpt].frequencyHigh[num];
					this.AxisValues_Z.range = this.scaleFactor[sampleRateOpt].magniFS;
					this.AxisValues_Z.fMax = this.scaleFactor[sampleRateOpt].Fmax;
					num++;
				}
				while (num <= 6);
			}
		}

		// Token: 0x04000036 RID: 54
		public AlarmsClass.LsbValuesArray[] LsbValues;

		// Token: 0x04000037 RID: 55
		public AlarmsClass.scaledValuesArray[] ScaledValues;

		// Token: 0x04000038 RID: 56
		public AlarmsClass.scaleFactorType[] scaleFactor;

		// Token: 0x04000039 RID: 57
		public AlarmsClass.AxisClass AxisValues_X;

		// Token: 0x0400003A RID: 58
		public AlarmsClass.AxisClass AxisValues_Y;

		// Token: 0x0400003B RID: 59
		public AlarmsClass.AxisClass AxisValues_Z;

		// Token: 0x0400003C RID: 60
		public string[] header;

		// Token: 0x02000027 RID: 39
		public class scaleFactorType
		{
			// Token: 0x06000407 RID: 1031 RVA: 0x0001B982 File Offset: 0x00019B82
			public scaleFactorType()
			{
				this._Fmax = 0.0;
				this._magniFS = 0.0;
				this._fftAvg = 1;
			}

			// Token: 0x17000154 RID: 340
			// (get) Token: 0x06000408 RID: 1032 RVA: 0x0001B9B0 File Offset: 0x00019BB0
			// (set) Token: 0x06000409 RID: 1033 RVA: 0x0001B9C8 File Offset: 0x00019BC8
			public double Fmax
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

			// Token: 0x17000155 RID: 341
			// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001B9D4 File Offset: 0x00019BD4
			// (set) Token: 0x0600040B RID: 1035 RVA: 0x0001B9EC File Offset: 0x00019BEC
			public double magniFS
			{
				get
				{
					return this._magniFS;
				}
				set
				{
					this._magniFS = value;
				}
			}

			// Token: 0x17000156 RID: 342
			// (get) Token: 0x0600040C RID: 1036 RVA: 0x0001B9F8 File Offset: 0x00019BF8
			// (set) Token: 0x0600040D RID: 1037 RVA: 0x0001BA10 File Offset: 0x00019C10
			public int fftAvg
			{
				get
				{
					return this._fftAvg;
				}
				set
				{
					this._fftAvg = value;
				}
			}

			// Token: 0x040001B9 RID: 441
			protected double _Fmax;

			// Token: 0x040001BA RID: 442
			protected double _magniFS;

			// Token: 0x040001BB RID: 443
			protected int _fftAvg;
		}

		// Token: 0x02000028 RID: 40
		public class LsbValuesArray
		{
			// Token: 0x0600040E RID: 1038 RVA: 0x0001BA1C File Offset: 0x00019C1C
			public LsbValuesArray()
			{
				this.frequencyLow = new uint[8];
				this.frequencyHigh = new uint[8];
				this.X1 = new uint[8];
				this.X2 = new uint[8];
				this.Y1 = new uint[8];
				this.Y2 = new uint[8];
				this.Z1 = new uint[8];
				this.Z2 = new uint[8];
			}

			// Token: 0x040001BC RID: 444
			public uint[] frequencyLow;

			// Token: 0x040001BD RID: 445
			public uint[] frequencyHigh;

			// Token: 0x040001BE RID: 446
			public uint[] X1;

			// Token: 0x040001BF RID: 447
			public uint[] X2;

			// Token: 0x040001C0 RID: 448
			public uint[] Y1;

			// Token: 0x040001C1 RID: 449
			public uint[] Y2;

			// Token: 0x040001C2 RID: 450
			public uint[] Z1;

			// Token: 0x040001C3 RID: 451
			public uint[] Z2;
		}

		// Token: 0x02000029 RID: 41
		public class scaledValuesArray
		{
			// Token: 0x0600040F RID: 1039 RVA: 0x0001BA90 File Offset: 0x00019C90
			public scaledValuesArray()
			{
				this.frequencyLow = new double[8];
				this.frequencyHigh = new double[8];
				this.X1 = new double[8];
				this.X2 = new double[8];
				this.Y1 = new double[8];
				this.Y2 = new double[8];
				this.Z1 = new double[8];
				this.Z2 = new double[8];
			}

			// Token: 0x040001C4 RID: 452
			public double[] frequencyLow;

			// Token: 0x040001C5 RID: 453
			public double[] frequencyHigh;

			// Token: 0x040001C6 RID: 454
			public double[] X1;

			// Token: 0x040001C7 RID: 455
			public double[] X2;

			// Token: 0x040001C8 RID: 456
			public double[] Y1;

			// Token: 0x040001C9 RID: 457
			public double[] Y2;

			// Token: 0x040001CA RID: 458
			public double[] Z1;

			// Token: 0x040001CB RID: 459
			public double[] Z2;
		}

		// Token: 0x0200002A RID: 42
		public class AxisClass
		{
			// Token: 0x06000410 RID: 1040 RVA: 0x0001BB04 File Offset: 0x00019D04
			public AxisClass()
			{
				this.level1 = new double[7];
				this.level2 = new double[7];
				this.frequencyLow = new double[7];
				this.frequencyHigh = new double[7];
			}

			// Token: 0x040001CC RID: 460
			public int SamRateOpt;

			// Token: 0x040001CD RID: 461
			public double fMax;

			// Token: 0x040001CE RID: 462
			public double range;

			// Token: 0x040001CF RID: 463
			public double[] level1;

			// Token: 0x040001D0 RID: 464
			public double[] level2;

			// Token: 0x040001D1 RID: 465
			public double[] frequencyLow;

			// Token: 0x040001D2 RID: 466
			public double[] frequencyHigh;
		}
	}
}
