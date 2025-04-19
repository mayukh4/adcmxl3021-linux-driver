using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Vibration_Evaluation
{
	/// <summary>
	/// Sets the Global Variable BoardID
	/// </summary>
	// Token: 0x0200001B RID: 27
	[DesignerGenerated]
	public partial class FormUSBConnect : Form
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00015D3D File Offset: 0x00013F3D
		public FormUSBConnect()
		{
			base.Activated += this.FormUSBConnect_Activated;
			base.FormClosing += this.FormUSBConnect_FormClosing;
			this.InitializeComponent();
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00016141 File Offset: 0x00014341
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x0001614B File Offset: 0x0001434B
		internal virtual GroupBox GroupBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00016154 File Offset: 0x00014354
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00016160 File Offset: 0x00014360
		internal virtual RadioButton RadioButtonFX3
		{
			[CompilerGenerated]
			get
			{
				return this._RadioButtonFX3;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.RadioFX3_CheckedChanged);
				RadioButton radioButtonFX = this._RadioButtonFX3;
				if (radioButtonFX != null)
				{
					radioButtonFX.CheckedChanged -= value2;
				}
				this._RadioButtonFX3 = value;
				radioButtonFX = this._RadioButtonFX3;
				if (radioButtonFX != null)
				{
					radioButtonFX.CheckedChanged += value2;
				}
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000161A3 File Offset: 0x000143A3
		// (set) Token: 0x060002CB RID: 715 RVA: 0x000161B0 File Offset: 0x000143B0
		internal virtual Button ButtonConnect
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonConnect;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonConnect_Click);
				Button buttonConnect = this._ButtonConnect;
				if (buttonConnect != null)
				{
					buttonConnect.Click -= value2;
				}
				this._ButtonConnect = value;
				buttonConnect = this._ButtonConnect;
				if (buttonConnect != null)
				{
					buttonConnect.Click += value2;
				}
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060002CC RID: 716 RVA: 0x000161F3 File Offset: 0x000143F3
		// (set) Token: 0x060002CD RID: 717 RVA: 0x000161FD File Offset: 0x000143FD
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00016206 File Offset: 0x00014406
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00016210 File Offset: 0x00014410
		internal virtual Label LabelClose { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060002D0 RID: 720 RVA: 0x00016219 File Offset: 0x00014419
		private void FormUSBConnect_Activated(object sender, EventArgs e)
		{
			this.RadioButtonFX3.Checked = true;
			this.showConnectionState();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00016230 File Offset: 0x00014430
		private void RadioFX3_CheckedChanged(object sender, EventArgs e)
		{
			bool @checked = this.RadioButtonFX3.Checked;
			if (@checked)
			{
				this.ButtonConnect.Enabled = true;
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00003198 File Offset: 0x00001398
		private void RadioSDP_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0001625C File Offset: 0x0001445C
		private void ButtonConnect_Click(object sender, EventArgs e)
		{
			this.ButtonConnect.Enabled = false;
			Application.DoEvents();
			bool flag = GlobalDeclarations.progUt.FX3connect();
			bool flag2 = flag;
			if (flag2)
			{
				GlobalDeclarations.BoardID = GlobalDeclarations.BoardIDtype.FX3;
			}
			else
			{
				GlobalDeclarations.BoardID = GlobalDeclarations.BoardIDtype.NONE;
			}
			this.showConnectionState();
		}

		/// <summary>
		/// Uses global var. BoardID
		/// </summary>
		// Token: 0x060002D4 RID: 724 RVA: 0x000162A8 File Offset: 0x000144A8
		public void showConnectionState()
		{
			this.RadioButtonFX3.Checked = true;
			this.RadioButtonFX3.Text = "FX3 board is Not connected.";
			this.RadioButtonFX3.BackColor = Control.DefaultBackColor;
			this.ButtonConnect.Enabled = true;
			this.LabelClose.Visible = false;
			switch (GlobalDeclarations.BoardID)
			{
			case GlobalDeclarations.BoardIDtype.SDPEVAL:
				this.RadioButtonFX3.Checked = false;
				this.RadioButtonFX3.Text = "FX3 board is not connected.";
				this.ButtonConnect.Enabled = false;
				this.LabelClose.Visible = true;
				break;
			case GlobalDeclarations.BoardIDtype.FX3:
				this.RadioButtonFX3.Checked = true;
				this.RadioButtonFX3.Text = "FX3 board is CONNECTED.";
				this.RadioButtonFX3.BackColor = Color.LightGreen;
				this.ButtonConnect.Enabled = false;
				this.LabelClose.Visible = true;
				break;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000163AC File Offset: 0x000145AC
		private void FormUSBConnect_FormClosing(object sender, FormClosingEventArgs e)
		{
			int num = 0;
			checked
			{
				num++;
			}
		}
	}
}
