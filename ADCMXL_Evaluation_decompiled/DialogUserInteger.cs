using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Vibration_Evaluation
{
	// Token: 0x02000010 RID: 16
	[DesignerGenerated]
	public partial class DialogUserInteger : Form
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00007760 File Offset: 0x00005960
		public DialogUserInteger()
		{
			base.Load += this.DialogUserInteger_Load;
			base.Activated += this.DialogUserInteger_Activated;
			this.min = 5;
			this.max = 60;
			this.InitializeComponent();
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00007C71 File Offset: 0x00005E71
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00007C7B File Offset: 0x00005E7B
		internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00007C84 File Offset: 0x00005E84
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00007C90 File Offset: 0x00005E90
		internal virtual Button OK_Button
		{
			[CompilerGenerated]
			get
			{
				return this._OK_Button;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.OK_Button_Click);
				Button ok_Button = this._OK_Button;
				if (ok_Button != null)
				{
					ok_Button.Click -= value2;
				}
				this._OK_Button = value;
				ok_Button = this._OK_Button;
				if (ok_Button != null)
				{
					ok_Button.Click += value2;
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00007CD3 File Offset: 0x00005ED3
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00007CE0 File Offset: 0x00005EE0
		internal virtual Button Cancel_Button
		{
			[CompilerGenerated]
			get
			{
				return this._Cancel_Button;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Cancel_Button_Click);
				Button cancel_Button = this._Cancel_Button;
				if (cancel_Button != null)
				{
					cancel_Button.Click -= value2;
				}
				this._Cancel_Button = value;
				cancel_Button = this._Cancel_Button;
				if (cancel_Button != null)
				{
					cancel_Button.Click += value2;
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00007D23 File Offset: 0x00005F23
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00007D2D File Offset: 0x00005F2D
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00007D36 File Offset: 0x00005F36
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00007D40 File Offset: 0x00005F40
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00007D49 File Offset: 0x00005F49
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00007D53 File Offset: 0x00005F53
		internal virtual TextBox TextBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060000F4 RID: 244 RVA: 0x00007D5C File Offset: 0x00005F5C
		private void DialogUserInteger_Load(object sender, EventArgs e)
		{
			this.Label1.Text = "Enter the Update Interval in seconds.";
			this.Label2.Text = string.Concat(new string[]
			{
				"Valid values are integers between ",
				this.min.ToString(),
				" and ",
				this.max.ToString(),
				"."
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00007DC6 File Offset: 0x00005FC6
		private void DialogUserInteger_Activated(object sender, EventArgs e)
		{
			this.TextBox1.Focus();
			this.TextBox1.SelectionStart = 2;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007DE4 File Offset: 0x00005FE4
		private void OK_Button_Click(object sender, EventArgs e)
		{
			bool flag = Versioned.IsNumeric(this.TextBox1.Text);
			bool flag2 = flag;
			bool flag6;
			if (flag2)
			{
				bool flag3 = int.TryParse(this.TextBox1.Text, out DialogUserInteger.userValue);
				bool flag4 = flag3;
				if (flag4)
				{
					bool flag5 = DialogUserInteger.userValue >= this.min & DialogUserInteger.userValue <= this.max;
					flag6 = flag5;
				}
				else
				{
					flag6 = false;
				}
			}
			else
			{
				flag6 = false;
			}
			bool flag7 = flag6;
			if (flag7)
			{
				base.DialogResult = DialogResult.OK;
				base.Close();
			}
			else
			{
				string text = "Your input = " + this.TextBox1.Text + "   is not valid.\r\n\r\n";
				text = string.Concat(new string[]
				{
					text,
					"You must enter an integer between  ",
					this.min.ToString(),
					" and ",
					this.max.ToString(),
					"  ,or click Cancel."
				});
				Interaction.MsgBox(text, MsgBoxStyle.Exclamation, null);
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000076D6 File Offset: 0x000058D6
		private void Cancel_Button_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x04000061 RID: 97
		private static int userValue;

		// Token: 0x04000062 RID: 98
		private int min;

		// Token: 0x04000063 RID: 99
		private int max;
	}
}
