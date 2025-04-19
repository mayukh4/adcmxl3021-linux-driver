using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Vibration_Evaluation.My.Resources;

namespace Vibration_Evaluation
{
	// Token: 0x0200001E RID: 30
	[DesignerGenerated]
	public partial class FormHelp : Form
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0001779B File Offset: 0x0001599B
		public FormHelp()
		{
			base.Load += this.HelpForm_Load;
			this.InitializeComponent();
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0001792C File Offset: 0x00015B2C
		// (set) Token: 0x06000304 RID: 772 RVA: 0x00017936 File Offset: 0x00015B36
		internal virtual RichTextBox RichTextBox1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x06000305 RID: 773 RVA: 0x0001793F File Offset: 0x00015B3F
		private void HelpForm_Load(object sender, EventArgs e)
		{
			this.RichTextBox1.Rtf = Resources.HelpText;
		}
	}
}
