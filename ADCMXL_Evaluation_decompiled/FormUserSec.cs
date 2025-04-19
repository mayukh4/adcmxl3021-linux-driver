using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Vibration_Evaluation.My;

namespace Vibration_Evaluation
{
	// Token: 0x0200001C RID: 28
	[DesignerGenerated]
	public partial class FormUserSec : Form
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x000163C0 File Offset: 0x000145C0
		public FormUserSec()
		{
			this.userResult = true;
			this.InitializeComponent();
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000167B8 File Offset: 0x000149B8
		// (set) Token: 0x060002DA RID: 730 RVA: 0x000167C2 File Offset: 0x000149C2
		internal virtual TextBox TextBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002DB RID: 731 RVA: 0x000167CB File Offset: 0x000149CB
		// (set) Token: 0x060002DC RID: 732 RVA: 0x000167D8 File Offset: 0x000149D8
		internal virtual Button ButtonStart
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonStart;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonOK_Click);
				Button buttonStart = this._ButtonStart;
				if (buttonStart != null)
				{
					buttonStart.Click -= value2;
				}
				this._ButtonStart = value;
				buttonStart = this._ButtonStart;
				if (buttonStart != null)
				{
					buttonStart.Click += value2;
				}
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0001681B File Offset: 0x00014A1B
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00016825 File Offset: 0x00014A25
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0001682E File Offset: 0x00014A2E
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00016838 File Offset: 0x00014A38
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00016841 File Offset: 0x00014A41
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0001684B File Offset: 0x00014A4B
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00016854 File Offset: 0x00014A54
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00016860 File Offset: 0x00014A60
		internal virtual Button ButtonCancel
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonCancel;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonCancel_Click);
				Button buttonCancel = this._ButtonCancel;
				if (buttonCancel != null)
				{
					buttonCancel.Click -= value2;
				}
				this._ButtonCancel = value;
				buttonCancel = this._ButtonCancel;
				if (buttonCancel != null)
				{
					buttonCancel.Click += value2;
				}
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000168A4 File Offset: 0x00014AA4
		public bool ShowDialogFunc(iVIBEcontrol dutCommand)
		{
			this.dutComm = dutCommand;
			checked
			{
				int num = dutCommand.SensorsOnNetwork.countActive * 2;
				this.TextBox1.Text = num.ToString();
				base.Left = MyProject.Forms.FormMain.Left + 500;
				base.Top = MyProject.Forms.FormMain.Top + 100;
				base.ShowDialog();
				return this.userResult;
			}
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00016920 File Offset: 0x00014B20
		private void ButtonOK_Click(object sender, EventArgs e)
		{
			int num = 0;
			checked
			{
				int num2 = this.dutComm.SensorsOnNetwork.countActive * 2 - 1;
				int num3 = 31;
				try
				{
					num = Conversions.ToInteger(this.TextBox1.Text);
				}
				catch (Exception ex)
				{
					Interaction.MsgBox(ex.Message, MsgBoxStyle.OkOnly, null);
					this.TextBox1.Text = num2.ToString();
				}
				bool flag = num > num2;
				bool flag3;
				if (flag)
				{
					bool flag2 = num < num3;
					if (flag2)
					{
						flag3 = true;
					}
					else
					{
						flag3 = false;
						Interaction.MsgBox("The update interval for this demonstration must be less than " + num3.ToString(), MsgBoxStyle.OkOnly, null);
						this.TextBox1.Text = (num3 - 1).ToString();
					}
				}
				else
				{
					flag3 = false;
					Interaction.MsgBox("The update interval for this demonstration must be greater than " + num2.ToString(), MsgBoxStyle.OkOnly, null);
					this.TextBox1.Text = (num2 + 1).ToString();
				}
				bool flag4 = flag3;
				if (flag4)
				{
					this.userResult = true;
					base.Close();
				}
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00016A40 File Offset: 0x00014C40
		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			this.userResult = false;
			base.Close();
		}

		// Token: 0x04000149 RID: 329
		private iVIBEcontrol dutComm;

		// Token: 0x0400014A RID: 330
		private bool userResult;
	}
}
