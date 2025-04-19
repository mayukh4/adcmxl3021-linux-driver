using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Vibration_Evaluation
{
	// Token: 0x02000018 RID: 24
	[DesignerGenerated]
	public partial class FormMessage : Form
	{
		// Token: 0x06000293 RID: 659 RVA: 0x00014BAB File Offset: 0x00012DAB
		public FormMessage()
		{
			this.InitializeComponent();
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00014D4C File Offset: 0x00012F4C
		// (set) Token: 0x06000297 RID: 663 RVA: 0x00014D56 File Offset: 0x00012F56
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x06000298 RID: 664 RVA: 0x00014D60 File Offset: 0x00012F60
		public void ShowMessage(string Text, string subTitle = "")
		{
			base.Width = 300;
			base.Height = 150;
			this.Text = Application.ProductName + "   " + subTitle;
			this.Label1.Text = Text;
			base.Show();
			base.Activate();
			Application.DoEvents();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00014DC0 File Offset: 0x00012FC0
		public void ShowMessage(string mText, int sec)
		{
			base.Show();
			base.Activate();
			this.Label1.Text = mText + "\r\r" + sec.ToString("0") + "  seconds.";
			Application.DoEvents();
			int num = sec;
			checked
			{
				for (int i = 1; i <= num; i++)
				{
					Thread.Sleep(1000);
					double num2 = (double)(sec - 1 * i);
					this.Label1.Text = mText + "\r\r" + num2.ToString("0") + "  seconds.";
					Application.DoEvents();
				}
			}
		}
	}
}
