using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;
using Vibration_Evaluation.My.Resources;

namespace Vibration_Evaluation
{
	// Token: 0x02000019 RID: 25
	[DesignerGenerated]
	public partial class FormRichText : Form
	{
		// Token: 0x0600029A RID: 666 RVA: 0x00014E62 File Offset: 0x00013062
		public FormRichText()
		{
			this.InitializeComponent();
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00014FEA File Offset: 0x000131EA
		// (set) Token: 0x0600029E RID: 670 RVA: 0x00014FF4 File Offset: 0x000131F4
		internal virtual RichTextBox RTBox { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x0600029F RID: 671 RVA: 0x00015000 File Offset: 0x00013200
		public void ShowRevisionsRTF()
		{
			string revisions = Resources.Revisions;
			Stream data = FormRichText.StringToStream(revisions, Encoding.ASCII);
			this.RTBox.LoadFile(data, RichTextBoxStreamType.RichText);
			base.Width = 600;
			base.Height = 600;
			this.Text = Application.ProductName + "   Revision History";
			base.Show();
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00015064 File Offset: 0x00013264
		public static Stream StringToStream(string input, Encoding enc)
		{
			MemoryStream memoryStream = new MemoryStream();
			StreamWriter streamWriter = new StreamWriter(memoryStream, enc);
			streamWriter.Write(input);
			streamWriter.Flush();
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000150A0 File Offset: 0x000132A0
		public void ShowMessage(string Text, string subTitle = "")
		{
			base.Width = 300;
			base.Height = 150;
			this.Text = Application.ProductName + "   " + subTitle;
			this.RTBox.Text = Text;
			base.Show();
			base.Activate();
			Application.DoEvents();
		}
	}
}
