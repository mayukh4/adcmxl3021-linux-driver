using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Vibration_Evaluation.My;

namespace Vibration_Evaluation
{
	// Token: 0x02000013 RID: 19
	[DesignerGenerated]
	public partial class FormCountDown : Form
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00009BB8 File Offset: 0x00007DB8
		public FormCountDown()
		{
			this.remainSec = 0.0;
			this.msg2 = "";
			this.InitializeComponent();
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00009D60 File Offset: 0x00007F60
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00009D6A File Offset: 0x00007F6A
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00009D73 File Offset: 0x00007F73
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00009D80 File Offset: 0x00007F80
		internal virtual Timer Timer1
		{
			[CompilerGenerated]
			get
			{
				return this._Timer1;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Timer1_Tick);
				Timer timer = this._Timer1;
				if (timer != null)
				{
					timer.Tick -= value2;
				}
				this._Timer1 = value;
				timer = this._Timer1;
				if (timer != null)
				{
					timer.Tick += value2;
				}
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009DC4 File Offset: 0x00007FC4
		public void ShowCountDown(string msg, double seconds)
		{
			this.Text = Application.ProductName;
			this.Label1.Text = msg + seconds.ToString();
			MyProject.Forms.FormMessage.Refresh();
			this.remainSec = seconds;
			this.msg2 = msg;
			this.Timer1.Enabled = true;
			base.ShowDialog();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009E2C File Offset: 0x0000802C
		private void Timer1_Tick(object sender, EventArgs e)
		{
			double num = this.remainSec;
			string str = num.ToString("0.0");
			this.Label1.Text = this.msg2 + str;
			this.Label1.Refresh();
			this.Refresh();
			Application.DoEvents();
			this.remainSec -= 0.1;
			bool flag = this.remainSec < 0.0;
			if (flag)
			{
				this.Timer1.Enabled = false;
				base.Hide();
			}
		}

		// Token: 0x0400007B RID: 123
		private double remainSec;

		// Token: 0x0400007C RID: 124
		private string msg2;
	}
}
