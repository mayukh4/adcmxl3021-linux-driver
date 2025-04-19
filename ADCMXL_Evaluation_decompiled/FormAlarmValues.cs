using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Vibration_Evaluation.My;
using Vibration_Evaluation.My.Resources;

namespace Vibration_Evaluation
{
	// Token: 0x02000011 RID: 17
	[DesignerGenerated]
	public partial class FormAlarmValues : Form
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00007EF5 File Offset: 0x000060F5
		public FormAlarmValues()
		{
			base.Load += this.FormAlarms_Load;
			this.AlarmsObj = new AlarmsClass();
			this.alarmList = new BindingList<FormAlarmValues.AlarmRow>();
			this._axisCount = 2;
			this.InitializeComponent();
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00008691 File Offset: 0x00006891
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000869C File Offset: 0x0000689C
		internal virtual DataGridView DataGridView1
		{
			[CompilerGenerated]
			get
			{
				return this._DataGridView1;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				DataGridViewDataErrorEventHandler value2 = new DataGridViewDataErrorEventHandler(this.DataGridView1_DataError);
				DataGridView dataGridView = this._DataGridView1;
				if (dataGridView != null)
				{
					dataGridView.DataError -= value2;
				}
				this._DataGridView1 = value;
				dataGridView = this._DataGridView1;
				if (dataGridView != null)
				{
					dataGridView.DataError += value2;
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000086DF File Offset: 0x000068DF
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000086EC File Offset: 0x000068EC
		internal virtual Button ButtonReadDut
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonReadDut;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonReadDut_Click);
				Button buttonReadDut = this._ButtonReadDut;
				if (buttonReadDut != null)
				{
					buttonReadDut.Click -= value2;
				}
				this._ButtonReadDut = value;
				buttonReadDut = this._ButtonReadDut;
				if (buttonReadDut != null)
				{
					buttonReadDut.Click += value2;
				}
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000872F File Offset: 0x0000692F
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000873C File Offset: 0x0000693C
		internal virtual Button ButtonWriteDUT
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonWriteDUT;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonWriteDUT_Click);
				Button buttonWriteDUT = this._ButtonWriteDUT;
				if (buttonWriteDUT != null)
				{
					buttonWriteDUT.Click -= value2;
				}
				this._ButtonWriteDUT = value;
				buttonWriteDUT = this._ButtonWriteDUT;
				if (buttonWriteDUT != null)
				{
					buttonWriteDUT.Click += value2;
				}
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000877F File Offset: 0x0000697F
		// (set) Token: 0x06000102 RID: 258 RVA: 0x0000878C File Offset: 0x0000698C
		internal virtual Button ButtonReadFile
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonReadFile;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonReadFile_Click);
				Button buttonReadFile = this._ButtonReadFile;
				if (buttonReadFile != null)
				{
					buttonReadFile.Click -= value2;
				}
				this._ButtonReadFile = value;
				buttonReadFile = this._ButtonReadFile;
				if (buttonReadFile != null)
				{
					buttonReadFile.Click += value2;
				}
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000087CF File Offset: 0x000069CF
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000087DC File Offset: 0x000069DC
		internal virtual Button ButtonWriteFile
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonWriteFile;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonWriteFile_Click);
				Button buttonWriteFile = this._ButtonWriteFile;
				if (buttonWriteFile != null)
				{
					buttonWriteFile.Click -= value2;
				}
				this._ButtonWriteFile = value;
				buttonWriteFile = this._ButtonWriteFile;
				if (buttonWriteFile != null)
				{
					buttonWriteFile.Click += value2;
				}
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000881F File Offset: 0x00006A1F
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00008829 File Offset: 0x00006A29
		internal virtual Label lblBusy { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00008832 File Offset: 0x00006A32
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000883C File Offset: 0x00006A3C
		internal virtual Button ButtonReadDefault
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonReadDefault;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonReadDefault_Click);
				Button buttonReadDefault = this._ButtonReadDefault;
				if (buttonReadDefault != null)
				{
					buttonReadDefault.Click -= value2;
				}
				this._ButtonReadDefault = value;
				buttonReadDefault = this._ButtonReadDefault;
				if (buttonReadDefault != null)
				{
					buttonReadDefault.Click += value2;
				}
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000887F File Offset: 0x00006A7F
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00008889 File Offset: 0x00006A89
		internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00008892 File Offset: 0x00006A92
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000889C File Offset: 0x00006A9C
		internal virtual Panel Panel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000088A5 File Offset: 0x00006AA5
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000088AF File Offset: 0x00006AAF
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x0600010F RID: 271 RVA: 0x000088B8 File Offset: 0x00006AB8
		private void FormAlarms_Load(object sender, EventArgs e)
		{
			this.DataGridView1.DataSource = this.alarmList;
			this.AlarmListRefresh();
			this.DataGridView1.Columns[0].Width = 50;
			this.DataGridView1.Columns[1].Width = 40;
			this.DataGridView1.Columns[2].Width = 60;
			this.DataGridView1.Columns[3].Width = 60;
			this.DataGridView1.Columns[4].Width = 60;
			this.DataGridView1.Columns[5].Width = 60;
			this.DataGridView1.Columns[6].Width = 60;
			this.DataGridView1.Columns[7].Width = 60;
			this.DataGridView1.Columns[8].Width = 60;
			this.DataGridView1.Columns[9].Width = 60;
			this.DataGridView1.Width = 575;
			this.DataGridView1.ClearSelection();
			this.lblBusy.Visible = false;
			int num = this.DataGridView1.ColumnHeadersHeight;
			checked
			{
				try
				{
					foreach (object obj in ((IEnumerable)this.DataGridView1.Rows))
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
						num += dataGridViewRow.Height;
						this.DataGridView1.Height = num + 5;
					}
				}
				finally
				{
					IEnumerator enumerator;
					if (enumerator is IDisposable)
					{
						(enumerator as IDisposable).Dispose();
					}
				}
				bool flag = this.axisCount == 1;
				if (flag)
				{
					this.DataGridView1.Columns[4].Visible = false;
					this.DataGridView1.Columns[5].Visible = false;
					this.DataGridView1.Columns[6].Visible = false;
					this.DataGridView1.Columns[7].Visible = false;
				}
				this.DataGridView1.Columns[0].ReadOnly = true;
				this.DataGridView1.Columns[1].ReadOnly = true;
				string text = "Values displayed in this Form must be written to the DUT";
				text += " by clicking the Write to DUT button.\r\n";
				text += "Alarms must be enabled by writting to the ALM_CTRL register. ALM_CTRL is accessible in the Register Access Form.\r\n";
				text += "Alarm values are only plotted when the Alarm Status Form is visible.";
				this.Label1.Text = text;
				this.Refresh();
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00008B5C File Offset: 0x00006D5C
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00008B74 File Offset: 0x00006D74
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

		// Token: 0x06000112 RID: 274 RVA: 0x00008B7E File Offset: 0x00006D7E
		private void ButtonReadDefault_Click(object sender, EventArgs e)
		{
			this.AlarmsObj.ReadFromResources(Resources.AlarmSpectrum_Default);
			this.AlarmListRefresh();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008B9C File Offset: 0x00006D9C
		private void ButtonReadDut_Click(object sender, EventArgs e)
		{
			this.lblBusy.Visible = true;
			this.Refresh();
			this.AlarmsObj.LsbValues = MyProject.Forms.FormMain.DutControl.AlarmsReadAll();
			this.AlarmListRefresh();
			this.lblBusy.Visible = false;
			this.Refresh();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008BF8 File Offset: 0x00006DF8
		private void ButtonWriteDUT_Click(object sender, EventArgs e)
		{
			this.lblBusy.Visible = true;
			this.Refresh();
			int num = -1;
			int num2 = 0;
			checked
			{
				do
				{
					int num3 = 1;
					do
					{
						num++;
						this.AlarmsObj.LsbValues[num2].frequencyLow[num3] = this.alarmList[num].F_LOW;
						this.AlarmsObj.LsbValues[num2].frequencyHigh[num3] = this.alarmList[num].F_HIGH;
						this.AlarmsObj.LsbValues[num2].X1[num3] = this.alarmList[num].X_MAG1;
						this.AlarmsObj.LsbValues[num2].X2[num3] = this.alarmList[num].X_MAG2;
						this.AlarmsObj.LsbValues[num2].Y1[num3] = this.alarmList[num].Y_MAG1;
						this.AlarmsObj.LsbValues[num2].Y2[num3] = this.alarmList[num].Y_MAG2;
						this.AlarmsObj.LsbValues[num2].Z1[num3] = this.alarmList[num].Z_MAG1;
						this.AlarmsObj.LsbValues[num2].Z2[num3] = this.alarmList[num].Z_MAG2;
						num3++;
					}
					while (num3 <= 6);
					num2++;
				}
				while (num2 <= 3);
				MyProject.Forms.FormMain.DutControl.AlarmsWrite(this.AlarmsObj.LsbValues);
				this.lblBusy.Visible = false;
				this.Refresh();
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00008D96 File Offset: 0x00006F96
		private void ButtonReadFile_Click(object sender, EventArgs e)
		{
			this.AlarmsObj.FileSelectOpen();
			this.AlarmListRefresh();
			this.DataGridView1.ClearSelection();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00008DB8 File Offset: 0x00006FB8
		private void ButtonWriteFile_Click(object sender, EventArgs e)
		{
			this.FileSelectSave();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00008DC4 File Offset: 0x00006FC4
		private void AlarmListRefresh()
		{
			this.alarmList.Clear();
			int num = 0;
			checked
			{
				do
				{
					int num2 = 1;
					do
					{
						this.alarmList.Add(new FormAlarmValues.AlarmRow
						{
							SR_opt = (uint)num,
							Band = (uint)num2,
							F_LOW = this.AlarmsObj.LsbValues[num].frequencyLow[num2],
							F_HIGH = this.AlarmsObj.LsbValues[num].frequencyHigh[num2],
							X_MAG1 = this.AlarmsObj.LsbValues[num].X1[num2],
							X_MAG2 = this.AlarmsObj.LsbValues[num].X2[num2],
							Y_MAG1 = this.AlarmsObj.LsbValues[num].Y1[num2],
							Y_MAG2 = this.AlarmsObj.LsbValues[num].Y2[num2],
							Z_MAG1 = this.AlarmsObj.LsbValues[num].Z1[num2],
							Z_MAG2 = this.AlarmsObj.LsbValues[num].Z2[num2]
						});
						num2++;
					}
					while (num2 <= 6);
					num++;
				}
				while (num <= 3);
				this.DataGridView1.ClearSelection();
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008F04 File Offset: 0x00007104
		private void DataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			string prompt = "Invalid Data\r\nEntries must be a positive integer.\r\n\r\nPress the Esc key to undo the edit.";
			Interaction.MsgBox(prompt, MsgBoxStyle.OkOnly, null);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00008F24 File Offset: 0x00007124
		public void FileSelectSave()
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			string filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			string defaultExt = "csv";
			saveFileDialog.Filter = filter;
			saveFileDialog.DefaultExt = defaultExt;
			saveFileDialog.FileName = "Alarm_Spectrum";
			bool flag = saveFileDialog.ShowDialog() == DialogResult.OK;
			if (flag)
			{
				this.saveToFile(saveFileDialog.FileName);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008F7C File Offset: 0x0000717C
		public void saveToFile(string fileName)
		{
			StreamWriter streamWriter = new StreamWriter(fileName);
			string[] array = new string[25];
			checked
			{
				int num = array.Count<string>() - 1;
				int i;
				for (i = 0; i <= num; i++)
				{
					array[i] = "";
				}
				int num2 = this.DataGridView1.ColumnCount - 1;
				for (int j = 0; j <= num2; j++)
				{
					bool flag = array[0].Length > 0;
					if (flag)
					{
						array[0] = array[0] + ",";
					}
					array[0] = array[0] + this.DataGridView1.Columns[j].Name;
				}
				i = 1;
				int num3 = this.DataGridView1.RowCount - 1;
				for (int k = 0; k <= num3; k++)
				{
					int num4 = this.DataGridView1.ColumnCount - 1;
					for (int l = 0; l <= num4; l++)
					{
						bool flag2 = array[i].Length > 0;
						if (flag2)
						{
							array[i] += ",";
						}
						array[i] += this.DataGridView1[l, k].Value.ToString();
					}
					i++;
				}
				int num5 = array.Count<string>() - 1;
				for (i = 0; i <= num5; i++)
				{
					streamWriter.WriteLine(array[i]);
				}
				streamWriter.Close();
			}
		}

		// Token: 0x0400006F RID: 111
		private AlarmsClass AlarmsObj;

		// Token: 0x04000070 RID: 112
		private BindingList<FormAlarmValues.AlarmRow> alarmList;

		// Token: 0x04000071 RID: 113
		private int _axisCount;

		/// <summary>
		/// Object (row) added to a List for binding data to dataGridView.
		///  Properties of this object are the columns of a dataGridView
		/// </summary>
		// Token: 0x0200002F RID: 47
		private class AlarmRow
		{
			// Token: 0x17000157 RID: 343
			// (get) Token: 0x0600041C RID: 1052 RVA: 0x0001BBFC File Offset: 0x00019DFC
			// (set) Token: 0x0600041D RID: 1053 RVA: 0x0001BC14 File Offset: 0x00019E14
			public uint SR_opt
			{
				get
				{
					return this._SampleRateOption;
				}
				set
				{
					this._SampleRateOption = value;
				}
			}

			// Token: 0x17000158 RID: 344
			// (get) Token: 0x0600041E RID: 1054 RVA: 0x0001BC20 File Offset: 0x00019E20
			// (set) Token: 0x0600041F RID: 1055 RVA: 0x0001BC38 File Offset: 0x00019E38
			public uint Band
			{
				get
				{
					return this._Band;
				}
				set
				{
					this._Band = value;
				}
			}

			// Token: 0x17000159 RID: 345
			// (get) Token: 0x06000420 RID: 1056 RVA: 0x0001BC44 File Offset: 0x00019E44
			// (set) Token: 0x06000421 RID: 1057 RVA: 0x0001BC5C File Offset: 0x00019E5C
			public uint F_LOW
			{
				get
				{
					return this._F_LOW;
				}
				set
				{
					this._F_LOW = value;
				}
			}

			// Token: 0x1700015A RID: 346
			// (get) Token: 0x06000422 RID: 1058 RVA: 0x0001BC68 File Offset: 0x00019E68
			// (set) Token: 0x06000423 RID: 1059 RVA: 0x0001BC80 File Offset: 0x00019E80
			public uint F_HIGH
			{
				get
				{
					return this._F_HIGH;
				}
				set
				{
					this._F_HIGH = value;
				}
			}

			// Token: 0x1700015B RID: 347
			// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001BC8C File Offset: 0x00019E8C
			// (set) Token: 0x06000425 RID: 1061 RVA: 0x0001BCA4 File Offset: 0x00019EA4
			public uint X_MAG1
			{
				get
				{
					return this._X_MAG1;
				}
				set
				{
					this._X_MAG1 = value;
				}
			}

			// Token: 0x1700015C RID: 348
			// (get) Token: 0x06000426 RID: 1062 RVA: 0x0001BCB0 File Offset: 0x00019EB0
			// (set) Token: 0x06000427 RID: 1063 RVA: 0x0001BCC8 File Offset: 0x00019EC8
			public uint X_MAG2
			{
				get
				{
					return this._X_MAG2;
				}
				set
				{
					this._X_MAG2 = value;
				}
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x06000428 RID: 1064 RVA: 0x0001BCD4 File Offset: 0x00019ED4
			// (set) Token: 0x06000429 RID: 1065 RVA: 0x0001BCEC File Offset: 0x00019EEC
			public uint Y_MAG1
			{
				get
				{
					return this._Y_MAG1;
				}
				set
				{
					this._Y_MAG1 = value;
				}
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x0600042A RID: 1066 RVA: 0x0001BCF8 File Offset: 0x00019EF8
			// (set) Token: 0x0600042B RID: 1067 RVA: 0x0001BD10 File Offset: 0x00019F10
			public uint Y_MAG2
			{
				get
				{
					return this._Y_MAG2;
				}
				set
				{
					this._Y_MAG2 = value;
				}
			}

			// Token: 0x1700015F RID: 351
			// (get) Token: 0x0600042C RID: 1068 RVA: 0x0001BD1C File Offset: 0x00019F1C
			// (set) Token: 0x0600042D RID: 1069 RVA: 0x0001BD34 File Offset: 0x00019F34
			public uint Z_MAG1
			{
				get
				{
					return this._Z_MAG1;
				}
				set
				{
					this._Z_MAG1 = value;
				}
			}

			// Token: 0x17000160 RID: 352
			// (get) Token: 0x0600042E RID: 1070 RVA: 0x0001BD40 File Offset: 0x00019F40
			// (set) Token: 0x0600042F RID: 1071 RVA: 0x0001BD58 File Offset: 0x00019F58
			public uint Z_MAG2
			{
				get
				{
					return this._Z_MAG2;
				}
				set
				{
					this._Z_MAG2 = value;
				}
			}

			// Token: 0x040001DA RID: 474
			private uint _SampleRateOption;

			// Token: 0x040001DB RID: 475
			private uint _Band;

			// Token: 0x040001DC RID: 476
			private uint _F_LOW;

			// Token: 0x040001DD RID: 477
			private uint _F_HIGH;

			// Token: 0x040001DE RID: 478
			private uint _X_MAG1;

			// Token: 0x040001DF RID: 479
			private uint _X_MAG2;

			// Token: 0x040001E0 RID: 480
			private uint _Y_MAG1;

			// Token: 0x040001E1 RID: 481
			private uint _Y_MAG2;

			// Token: 0x040001E2 RID: 482
			private uint _Z_MAG1;

			// Token: 0x040001E3 RID: 483
			private uint _Z_MAG2;
		}
	}
}
