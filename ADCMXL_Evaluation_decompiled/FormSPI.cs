using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Vibration_Evaluation
{
	// Token: 0x0200001A RID: 26
	[DesignerGenerated]
	public partial class FormSPI : Form
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x000150FE File Offset: 0x000132FE
		public FormSPI()
		{
			base.Load += this.FormSPI_Load;
			this.InitializeComponent();
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00015974 File Offset: 0x00013B74
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0001597E File Offset: 0x00013B7E
		internal virtual TextBox TextBoxSclkFreq { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x00015987 File Offset: 0x00013B87
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00015994 File Offset: 0x00013B94
		internal virtual Button ButtonSclkUpdate
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonSclkUpdate;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonSclkUpdate_Click);
				Button buttonSclkUpdate = this._ButtonSclkUpdate;
				if (buttonSclkUpdate != null)
				{
					buttonSclkUpdate.Click -= value2;
				}
				this._ButtonSclkUpdate = value;
				buttonSclkUpdate = this._ButtonSclkUpdate;
				if (buttonSclkUpdate != null)
				{
					buttonSclkUpdate.Click += value2;
				}
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x000159D7 File Offset: 0x00013BD7
		// (set) Token: 0x060002AA RID: 682 RVA: 0x000159E1 File Offset: 0x00013BE1
		internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002AB RID: 683 RVA: 0x000159EA File Offset: 0x00013BEA
		// (set) Token: 0x060002AC RID: 684 RVA: 0x000159F4 File Offset: 0x00013BF4
		internal virtual Button ButtonLED { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002AD RID: 685 RVA: 0x000159FD File Offset: 0x00013BFD
		// (set) Token: 0x060002AE RID: 686 RVA: 0x00015A07 File Offset: 0x00013C07
		internal virtual Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060002AF RID: 687 RVA: 0x00015A10 File Offset: 0x00013C10
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x00015A1A File Offset: 0x00013C1A
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00015A23 File Offset: 0x00013C23
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00015A2D File Offset: 0x00013C2D
		internal virtual Label LabelCpol { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00015A36 File Offset: 0x00013C36
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x00015A40 File Offset: 0x00013C40
		internal virtual Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x00015A49 File Offset: 0x00013C49
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x00015A54 File Offset: 0x00013C54
		internal virtual Button ButtonStallCycleUpdate
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonStallCycleUpdate;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonStallCycleUpdate_Click);
				Button buttonStallCycleUpdate = this._ButtonStallCycleUpdate;
				if (buttonStallCycleUpdate != null)
				{
					buttonStallCycleUpdate.Click -= value2;
				}
				this._ButtonStallCycleUpdate = value;
				buttonStallCycleUpdate = this._ButtonStallCycleUpdate;
				if (buttonStallCycleUpdate != null)
				{
					buttonStallCycleUpdate.Click += value2;
				}
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x00015A97 File Offset: 0x00013C97
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x00015AA1 File Offset: 0x00013CA1
		internal virtual TextBox TextBoxStallCyles { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00015AAA File Offset: 0x00013CAA
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00015AB4 File Offset: 0x00013CB4
		internal virtual Button ButtonSpiReset
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonSpiReset;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonSpiReset_Click);
				Button buttonSpiReset = this._ButtonSpiReset;
				if (buttonSpiReset != null)
				{
					buttonSpiReset.Click -= value2;
				}
				this._ButtonSpiReset = value;
				buttonSpiReset = this._ButtonSpiReset;
				if (buttonSpiReset != null)
				{
					buttonSpiReset.Click += value2;
				}
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00015AF7 File Offset: 0x00013CF7
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00015B01 File Offset: 0x00013D01
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00015B0A File Offset: 0x00013D0A
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00015B14 File Offset: 0x00013D14
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060002BF RID: 703 RVA: 0x00015B20 File Offset: 0x00013D20
		private void FormSPI_Load(object sender, EventArgs e)
		{
			this.TextBoxSclkFreq.Text = GlobalDeclarations.FX3comm.SclkFrequency.ToString("N0");
			this.LabelCpol.Text = GlobalDeclarations.FX3comm.Cpol.ToString();
			this.Label1.Text = this.Label1.Text + "\r\nReducing the SPI clock can increase aquisition time from 0.5 to 15 sec.";
			this.Label1.Text = this.Label1.Text + "\r\nManual FTT and Manual Time mode captures cannot be canceled.";
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00015BB4 File Offset: 0x00013DB4
		private void ButtonSclkUpdate_Click(object sender, EventArgs e)
		{
			NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
			int num;
			bool flag = int.TryParse(this.TextBoxSclkFreq.Text.Trim(), style, CultureInfo.CurrentCulture, out num);
			if (flag)
			{
				bool flag2 = num < 200000 | num > 14000001;
				if (flag2)
				{
					Interaction.MsgBox(string.Format("Frequency must be within the range of {0:N0} and {1:N0} Hz.", 200000, 14000000), MsgBoxStyle.OkOnly, null);
				}
				else
				{
					GlobalDeclarations.FX3comm.SclkFrequency = num;
				}
			}
			else
			{
				Interaction.MsgBox(this.TextBoxSclkFreq.Text + " is not a vailid integer value.", MsgBoxStyle.OkOnly, null);
			}
			this.TextBoxSclkFreq.Text = GlobalDeclarations.FX3comm.SclkFrequency.ToString("N0");
			GlobalDeclarations.SPIsclkUser = num;
			this.writeLogFileinMain("SPI SCLK", num.ToString(), "Form SPI");
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00015CA0 File Offset: 0x00013EA0
		private void ButtonStallCycleUpdate_Click(object sender, EventArgs e)
		{
			NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowThousands | NumberStyles.AllowExponent;
			int num;
			bool flag = int.TryParse(this.TextBoxStallCyles.Text.Trim(), style, CultureInfo.CurrentCulture, out num);
			if (flag)
			{
				bool flag2 = num < 0 | num > 5000;
				if (flag2)
				{
					Interaction.MsgBox(string.Format("Stall Cycles must bin within the range of {0:N0} and {1:N0} clock cycles.", 0, 5000), MsgBoxStyle.OkOnly, null);
				}
			}
			else
			{
				Interaction.MsgBox(this.TextBoxStallCyles.Text + " is not a vailid integer value.", MsgBoxStyle.OkOnly, null);
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00015D2F File Offset: 0x00013F2F
		private void ButtonSpiReset_Click(object sender, EventArgs e)
		{
			GlobalDeclarations.FX3comm.Reset();
		}

		// Token: 0x04000137 RID: 311
		private const int minFreq = 200000;

		// Token: 0x04000138 RID: 312
		private const int maxFreq = 14000001;

		// Token: 0x04000139 RID: 313
		private const int minStallCycles = 0;

		// Token: 0x0400013A RID: 314
		private const int maxStallCycles = 5000;

		// Token: 0x0400013B RID: 315
		public Action<string, string, string> writeLogFileinMain;
	}
}
