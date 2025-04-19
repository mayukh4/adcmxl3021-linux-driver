using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Vibration_Evaluation.My;

namespace Vibration_Evaluation
{
	// Token: 0x02000012 RID: 18
	[DesignerGenerated]
	public partial class FormAlarmStatus2 : Form
	{
		// Token: 0x0600011B RID: 283 RVA: 0x000090D8 File Offset: 0x000072D8
		public FormAlarmStatus2()
		{
			base.Load += this.FormAlarmStatus2_Load;
			base.FormClosing += this.FormAlarmStatus2_FormClosing;
			base.MouseMove += this.FormAlarmStatus2_MouseMove;
			this.alarmsPerAxis = new BindingList<FormAlarmStatus2.AlarmRow>[7];
			this._axisCount = 3;
			this._sensorCount = 1;
			this.dgList = new List<DataGridView>();
			this.InitializeComponent();
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000093AF File Offset: 0x000075AF
		// (set) Token: 0x0600011F RID: 287 RVA: 0x000093B9 File Offset: 0x000075B9
		internal virtual DataGridView DataGridView1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000093C4 File Offset: 0x000075C4
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000093DC File Offset: 0x000075DC
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

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000093E8 File Offset: 0x000075E8
		// (set) Token: 0x06000123 RID: 291 RVA: 0x00009400 File Offset: 0x00007600
		public int sensorCount
		{
			get
			{
				return this._sensorCount;
			}
			set
			{
				this._sensorCount = value;
			}
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000940C File Offset: 0x0000760C
		private void FormAlarmStatus2_Load(object sender, EventArgs e)
		{
			this.Text = "Alarm Status";
			this.DataGridView1.ColumnHeadersVisible = false;
			this.alarmsPerAxis[1] = new BindingList<FormAlarmStatus2.AlarmRow>();
			this.DataGridView1.DataSource = this.alarmsPerAxis[1];
			this.dgList.Add(this.DataGridView1);
			this.dgList.Add(this.DataGridView1);
			this.initializeAlarmList(1);
			int width = 20;
			this.DataGridView1.Columns[0].Width = 60;
			this.DataGridView1.Columns[1].Width = width;
			this.DataGridView1.Columns[2].Width = width;
			this.DataGridView1.Columns[3].Width = width;
			this.DataGridView1.Columns[4].Width = width;
			this.DataGridView1.Columns[5].Width = width;
			this.DataGridView1.Columns[6].Width = width;
			this.DataGridView1.Width = 183;
			int num = 0;
			checked
			{
				try
				{
					foreach (object obj in ((IEnumerable)this.DataGridView1.Rows))
					{
						DataGridViewRow dataGridViewRow = (DataGridViewRow)obj;
						num += dataGridViewRow.Height;
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
					this.DataGridView1.Rows[1].Visible = false;
					this.DataGridView1.Rows[2].Visible = false;
				}
				this.DataGridView1.Height = num + 3;
				this.DataGridView1.ClearSelection();
				MyProject.Forms.FormMain.PlotUC1.AlarmsVisible = true;
				MyProject.Forms.FormMain.PlotUC2.AlarmsVisible = true;
				MyProject.Forms.FormMain.PlotUC3.AlarmsVisible = true;
				bool flag2 = MyProject.Forms.FormMain.DeviceSelected.ProductID == 16000;
				if (flag2)
				{
					int num2 = 2;
					do
					{
						DataGridView dataGridView = new DataGridView();
						this.dgList.Add(dataGridView);
						base.Controls.Add(dataGridView);
						this.alarmsPerAxis[num2] = new BindingList<FormAlarmStatus2.AlarmRow>();
						this.initializeAlarmList(num2);
						dataGridView.DataSource = this.alarmsPerAxis[num2];
						dataGridView.DefaultCellStyle = new DataGridViewCellStyle
						{
							Alignment = DataGridViewContentAlignment.MiddleCenter
						};
						base.Height += 82;
						dataGridView.Top = this.DataGridView1.Top + (69 * (num2 - 1) + (num2 - 1) * 18);
						dataGridView.Left = this.DataGridView1.Left;
						dataGridView.Columns[0].Width = 60;
						dataGridView.Columns[1].Width = width;
						dataGridView.Columns[2].Width = width;
						dataGridView.Columns[3].Width = width;
						dataGridView.Columns[4].Width = width;
						dataGridView.Columns[5].Width = width;
						dataGridView.Columns[6].Width = width;
						dataGridView.ColumnHeadersVisible = false;
						dataGridView.ColumnHeadersVisible = false;
						dataGridView.RowHeadersVisible = false;
						dataGridView.AllowUserToAddRows = false;
						dataGridView.AllowUserToDeleteRows = false;
						dataGridView.AllowUserToResizeRows = false;
						dataGridView.Width = 183;
						num = 0;
						try
						{
							foreach (object obj2 in ((IEnumerable)dataGridView.Rows))
							{
								DataGridViewRow dataGridViewRow = (DataGridViewRow)obj2;
								num += dataGridViewRow.Height;
							}
						}
						finally
						{
							IEnumerator enumerator2;
							if (enumerator2 is IDisposable)
							{
								(enumerator2 as IDisposable).Dispose();
							}
						}
						dataGridView.Height = num + 3;
						dataGridView.ClearSelection();
						num2++;
					}
					while (num2 <= 6);
				}
				base.Top = (int)Math.Round(unchecked((double)Screen.PrimaryScreen.Bounds.Height / 2.0 - (double)MyProject.Forms.FormMain.Height / 2.0 + 90.0));
				base.Left = (int)Math.Round(unchecked((double)Screen.PrimaryScreen.Bounds.Width / 2.0 - (double)MyProject.Forms.FormMain.Width / 2.0 - 260.0));
				this.Refresh();
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00009904 File Offset: 0x00007B04
		public void Show(uint axisCount, int SensorCount)
		{
			this.sensorCount = SensorCount;
			this.axisCount = checked((int)axisCount);
			base.Show();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00009920 File Offset: 0x00007B20
		private void initializeAlarmList(int dev)
		{
			this.alarmsPerAxis[dev].Clear();
			int num = 0;
			checked
			{
				do
				{
					this.alarmsPerAxis[dev].Add(new FormAlarmStatus2.AlarmRow());
					num++;
				}
				while (num <= 3);
				this.alarmsPerAxis[dev][0].s0 = "Sens " + dev.ToString();
				this.alarmsPerAxis[dev][3].s0 = "Z_axis";
				this.alarmsPerAxis[dev][2].s0 = "Y_axis";
				this.alarmsPerAxis[dev][1].s0 = "X_axis";
				int num2 = 1;
				do
				{
					this.alarmsPerAxis[dev][0].setValue(num2, num2.ToString());
					num2++;
				}
				while (num2 <= 6);
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000099F0 File Offset: 0x00007BF0
		public void UpdateStatus(List<uint> codeList, int sensorID)
		{
			int num = 1;
			checked
			{
				do
				{
					int num2 = 1;
					do
					{
						this.alarmsPerAxis[sensorID][num].setValue(num2, "0");
						num2++;
					}
					while (num2 <= 6);
					num++;
				}
				while (num <= 3);
				int num3 = 1;
				do
				{
					int num2 = 1;
					uint num4 = codeList[num3 - 1];
					int num5 = 4;
					do
					{
						long num6 = (long)Math.Round(Math.Pow(2.0, (double)num5));
						int num7 = (int)Math.Round((double)(unchecked((ulong)num4) & (ulong)num6) / (double)num6);
						bool flag = num7 == 1;
						if (flag)
						{
							this.alarmsPerAxis[sensorID][num3].setValue(num2, "1");
						}
						num6 = (long)Math.Round(Math.Pow(2.0, (double)(num5 + 1)));
						num7 = (int)Math.Round((double)(unchecked((ulong)num4) & (ulong)num6) / (double)num6);
						bool flag2 = num7 == 1;
						if (flag2)
						{
							this.alarmsPerAxis[sensorID][num3].setValue(num2, "2");
						}
						num2++;
						num5 += 2;
					}
					while (num5 <= 14);
					num3++;
				}
				while (num3 <= 3);
				int num8 = 1;
				do
				{
					int num9 = 1;
					do
					{
						this.dgList[sensorID][num9, num8].Style.BackColor = this.alarmsPerAxis[sensorID][num8].backcolor[num9];
						num9++;
					}
					while (num9 <= 6);
					num8++;
				}
				while (num8 <= 3);
				this.Refresh();
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00009B68 File Offset: 0x00007D68
		private void FormAlarmStatus2_FormClosing(object sender, FormClosingEventArgs e)
		{
			MyProject.Forms.FormMain.PlotUC1.AlarmsVisible = false;
			MyProject.Forms.FormMain.PlotUC2.AlarmsVisible = false;
			MyProject.Forms.FormMain.PlotUC3.AlarmsVisible = false;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00003198 File Offset: 0x00001398
		private void FormAlarmStatus2_MouseMove(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x04000074 RID: 116
		private BindingList<FormAlarmStatus2.AlarmRow>[] alarmsPerAxis;

		// Token: 0x04000075 RID: 117
		private int _axisCount;

		// Token: 0x04000076 RID: 118
		private int _sensorCount;

		// Token: 0x04000077 RID: 119
		private List<DataGridView> dgList;

		// Token: 0x02000030 RID: 48
		private class AlarmRow
		{
			// Token: 0x06000430 RID: 1072 RVA: 0x0001BD64 File Offset: 0x00019F64
			public AlarmRow()
			{
				this.backcolor = new Color[7];
				this.color0 = Color.FromArgb(10, 215, 10);
				this.color1 = Color.FromArgb(245, 245, 40);
				this.color2 = Color.FromArgb(235, 90, 90);
				this._s0 = "-";
				this._s1 = "-";
				this._s2 = "-";
				this._s3 = "-";
				this._s4 = "-";
				this._s5 = "-";
				this._s6 = "-";
			}

			// Token: 0x06000431 RID: 1073 RVA: 0x0001BE10 File Offset: 0x0001A010
			private Color setcolor(string value)
			{
				Color result = this.color0;
				bool flag = Operators.CompareString(value, "1", false) == 0;
				if (flag)
				{
					result = this.color1;
				}
				bool flag2 = Operators.CompareString(value, "2", false) == 0;
				if (flag2)
				{
					result = this.color2;
				}
				return result;
			}

			// Token: 0x06000432 RID: 1074 RVA: 0x0001BE60 File Offset: 0x0001A060
			public void setValue(int band, string value)
			{
				switch (band)
				{
				case 1:
					this.s1 = value;
					break;
				case 2:
					this.s2 = value;
					break;
				case 3:
					this.s3 = value;
					break;
				case 4:
					this.s4 = value;
					break;
				case 5:
					this.s5 = value;
					break;
				case 6:
					this.s6 = value;
					break;
				}
			}

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x06000433 RID: 1075 RVA: 0x0001BED8 File Offset: 0x0001A0D8
			// (set) Token: 0x06000434 RID: 1076 RVA: 0x0001BEF0 File Offset: 0x0001A0F0
			public string s0
			{
				get
				{
					return this._s0;
				}
				set
				{
					this._s0 = value;
				}
			}

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x06000435 RID: 1077 RVA: 0x0001BEFC File Offset: 0x0001A0FC
			// (set) Token: 0x06000436 RID: 1078 RVA: 0x0001BF14 File Offset: 0x0001A114
			public string s1
			{
				get
				{
					return this._s1;
				}
				set
				{
					this._s1 = value;
					this.backcolor[1] = this.setcolor(value);
				}
			}

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x06000437 RID: 1079 RVA: 0x0001BF34 File Offset: 0x0001A134
			// (set) Token: 0x06000438 RID: 1080 RVA: 0x0001BF4C File Offset: 0x0001A14C
			public string s2
			{
				get
				{
					return this._s2;
				}
				set
				{
					this._s2 = value;
					this.backcolor[2] = this.setcolor(value);
				}
			}

			// Token: 0x17000164 RID: 356
			// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001BF6C File Offset: 0x0001A16C
			// (set) Token: 0x0600043A RID: 1082 RVA: 0x0001BF84 File Offset: 0x0001A184
			public string s3
			{
				get
				{
					return this._s3;
				}
				set
				{
					this._s3 = value;
					this.backcolor[3] = this.setcolor(value);
				}
			}

			// Token: 0x17000165 RID: 357
			// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001BFA4 File Offset: 0x0001A1A4
			// (set) Token: 0x0600043C RID: 1084 RVA: 0x0001BFBC File Offset: 0x0001A1BC
			public string s4
			{
				get
				{
					return this._s4;
				}
				set
				{
					this._s4 = value;
					this.backcolor[4] = this.setcolor(value);
				}
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x0600043D RID: 1085 RVA: 0x0001BFDC File Offset: 0x0001A1DC
			// (set) Token: 0x0600043E RID: 1086 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
			public string s5
			{
				get
				{
					return this._s5;
				}
				set
				{
					this._s5 = value;
					this.backcolor[5] = this.setcolor(value);
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x0600043F RID: 1087 RVA: 0x0001C014 File Offset: 0x0001A214
			// (set) Token: 0x06000440 RID: 1088 RVA: 0x0001C02C File Offset: 0x0001A22C
			public string s6
			{
				get
				{
					return this._s6;
				}
				set
				{
					this._s6 = value;
					this.backcolor[6] = this.setcolor(value);
				}
			}

			// Token: 0x040001E4 RID: 484
			public Color[] backcolor;

			// Token: 0x040001E5 RID: 485
			private Color color0;

			// Token: 0x040001E6 RID: 486
			private Color color1;

			// Token: 0x040001E7 RID: 487
			private Color color2;

			// Token: 0x040001E8 RID: 488
			private string _s0;

			// Token: 0x040001E9 RID: 489
			private string _s1;

			// Token: 0x040001EA RID: 490
			private string _s2;

			// Token: 0x040001EB RID: 491
			private string _s3;

			// Token: 0x040001EC RID: 492
			private string _s4;

			// Token: 0x040001ED RID: 493
			private string _s5;

			// Token: 0x040001EE RID: 494
			private string _s6;
		}
	}
}
