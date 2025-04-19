using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Vibration_Evaluation.My;

namespace Vibration_Evaluation
{
	// Token: 0x02000008 RID: 8
	[DesignerGenerated]
	public sealed partial class AboutBox1 : Form
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000026E3 File Offset: 0x000008E3
		public AboutBox1()
		{
			base.KeyPress += this.AboutBox1_KeyPress;
			base.Load += this.AboutBox1_Load;
			this.InitializeComponent();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002768 File Offset: 0x00000968
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002772 File Offset: 0x00000972
		internal TableLayoutPanel TableLayoutPanel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000277B File Offset: 0x0000097B
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002785 File Offset: 0x00000985
		internal Label LabelProductName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000278E File Offset: 0x0000098E
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002798 File Offset: 0x00000998
		internal Label LabelVersion { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000027A1 File Offset: 0x000009A1
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000027AB File Offset: 0x000009AB
		internal Label LabelCompanyName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000027B4 File Offset: 0x000009B4
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000027C0 File Offset: 0x000009C0
		internal Button OKButton
		{
			[CompilerGenerated]
			get
			{
				return this._OKButton;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.OKButton_Click);
				Button okbutton = this._OKButton;
				if (okbutton != null)
				{
					okbutton.Click -= value2;
				}
				this._OKButton = value;
				okbutton = this._OKButton;
				if (okbutton != null)
				{
					okbutton.Click += value2;
				}
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002803 File Offset: 0x00000A03
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000280D File Offset: 0x00000A0D
		internal Label LabelCopyright { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x0600003B RID: 59 RVA: 0x00002E34 File Offset: 0x00001034
		private void AboutBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			bool flag = Operators.CompareString(Conversions.ToString(e.KeyChar), "r", false) == 0;
			if (flag)
			{
				FormRichText formRichText = new FormRichText();
				formRichText.ShowRevisionsRTF();
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002E70 File Offset: 0x00001070
		private void AboutBox1_Load(object sender, EventArgs e)
		{
			bool flag = Operators.CompareString(MyProject.Application.Info.Title, "", false) != 0;
			string arg;
			if (flag)
			{
				arg = MyProject.Application.Info.Title;
			}
			else
			{
				arg = Path.GetFileNameWithoutExtension(MyProject.Application.Info.AssemblyName);
			}
			this.Text = string.Format("About {0}", arg);
			this.LabelProductName.Text = MyProject.Application.Info.ProductName;
			this.LabelVersion.Text = string.Format("Version {0}", Application.ProductVersion);
			this.LabelCopyright.Text = MyProject.Application.Info.Copyright;
			this.LabelCompanyName.Text = MyProject.Application.Info.CompanyName;
			checked
			{
				base.Top = MyProject.Forms.FormMain.Top + 70;
				base.Left = (int)Math.Round(unchecked((double)MyProject.Forms.FormMain.Left + ((double)MyProject.Forms.FormMain.Width / 2.0 - (double)base.Width / 2.0)));
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002FAA File Offset: 0x000011AA
		private void OKButton_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}
