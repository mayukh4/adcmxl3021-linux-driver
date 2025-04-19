using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;
using Vibration_Evaluation.My;

namespace Vibration_Evaluation
{
	// Token: 0x02000017 RID: 23
	[DesignerGenerated]
	public partial class FormDataLog : Form
	{
		// Token: 0x06000268 RID: 616 RVA: 0x00013C54 File Offset: 0x00011E54
		public FormDataLog()
		{
			base.Load += this.FormDataLog_Load;
			base.FormClosing += this.FormDataLog_FormClosing;
			this.bgWorker = new BackgroundWorker
			{
				WorkerReportsProgress = true,
				WorkerSupportsCancellation = true
			};
			this.fileFilter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			this.defaultExt = ".csv";
			this.RegList = new List<RegClass>();
			this.SelList = new BindingList<FormDataLog.SelObject>();
			this.InitializeComponent();
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000144C8 File Offset: 0x000126C8
		// (set) Token: 0x0600026C RID: 620 RVA: 0x000144D2 File Offset: 0x000126D2
		internal virtual Panel Panel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000144DB File Offset: 0x000126DB
		// (set) Token: 0x0600026E RID: 622 RVA: 0x000144E5 File Offset: 0x000126E5
		internal virtual ToolTip ToolTipDirectory { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000144EE File Offset: 0x000126EE
		// (set) Token: 0x06000270 RID: 624 RVA: 0x000144F8 File Offset: 0x000126F8
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00014501 File Offset: 0x00012701
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0001450B File Offset: 0x0001270B
		internal virtual SaveFileDialog SaveFileDialog1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00014514 File Offset: 0x00012714
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0001451E File Offset: 0x0001271E
		internal virtual ToolTip ToolTipCapTime { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00014527 File Offset: 0x00012727
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00014534 File Offset: 0x00012734
		internal virtual Button ButtonFolderDialog
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonFolderDialog;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonFolderDialog_Click1);
				Button buttonFolderDialog = this._ButtonFolderDialog;
				if (buttonFolderDialog != null)
				{
					buttonFolderDialog.Click -= value2;
				}
				this._ButtonFolderDialog = value;
				buttonFolderDialog = this._ButtonFolderDialog;
				if (buttonFolderDialog != null)
				{
					buttonFolderDialog.Click += value2;
				}
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00014577 File Offset: 0x00012777
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00014581 File Offset: 0x00012781
		internal virtual TextBox txtDirectory { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0001458A File Offset: 0x0001278A
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00014594 File Offset: 0x00012794
		internal virtual Button ButtonOK
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonOK;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonOK_Click);
				Button buttonOK = this._ButtonOK;
				if (buttonOK != null)
				{
					buttonOK.Click -= value2;
				}
				this._ButtonOK = value;
				buttonOK = this._ButtonOK;
				if (buttonOK != null)
				{
					buttonOK.Click += value2;
				}
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600027B RID: 635 RVA: 0x000145D7 File Offset: 0x000127D7
		// (set) Token: 0x0600027C RID: 636 RVA: 0x000145E4 File Offset: 0x000127E4
		internal virtual Button ButtonResetFullPath
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonResetFullPath;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonResetFullPath_Click);
				Button buttonResetFullPath = this._ButtonResetFullPath;
				if (buttonResetFullPath != null)
				{
					buttonResetFullPath.Click -= value2;
				}
				this._ButtonResetFullPath = value;
				buttonResetFullPath = this._ButtonResetFullPath;
				if (buttonResetFullPath != null)
				{
					buttonResetFullPath.Click += value2;
				}
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00014627 File Offset: 0x00012827
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00014631 File Offset: 0x00012831
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0001463A File Offset: 0x0001283A
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00014644 File Offset: 0x00012844
		internal virtual Label lblFileCount { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0001464D File Offset: 0x0001284D
		// (set) Token: 0x06000282 RID: 642 RVA: 0x00014658 File Offset: 0x00012858
		internal virtual Button ButtonCountReSet
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonCountReSet;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonCountReSet_Click);
				Button buttonCountReSet = this._ButtonCountReSet;
				if (buttonCountReSet != null)
				{
					buttonCountReSet.Click -= value2;
				}
				this._ButtonCountReSet = value;
				buttonCountReSet = this._ButtonCountReSet;
				if (buttonCountReSet != null)
				{
					buttonCountReSet.Click += value2;
				}
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0001469B File Offset: 0x0001289B
		// (set) Token: 0x06000284 RID: 644 RVA: 0x000146A5 File Offset: 0x000128A5
		internal virtual Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000285 RID: 645 RVA: 0x000146AE File Offset: 0x000128AE
		// (set) Token: 0x06000286 RID: 646 RVA: 0x000146B8 File Offset: 0x000128B8
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000287 RID: 647 RVA: 0x000146C1 File Offset: 0x000128C1
		// (set) Token: 0x06000288 RID: 648 RVA: 0x000146CB File Offset: 0x000128CB
		internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000289 RID: 649 RVA: 0x000146D4 File Offset: 0x000128D4
		// (set) Token: 0x0600028A RID: 650 RVA: 0x000146DE File Offset: 0x000128DE
		private virtual BackgroundWorker bgWorker { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x0600028B RID: 651 RVA: 0x000146E8 File Offset: 0x000128E8
		private void FormDataLog_Load(object sender, EventArgs e)
		{
			checked
			{
				base.Left = MyProject.Forms.FormMain.Left + 200;
				base.Top = MyProject.Forms.FormMain.Top + 90;
				this.txtDirectory.Text = Path.Combine(GlobalDeclarations.DatalogUt.Path, GlobalDeclarations.DatalogUt.FileName);
				bool flag = Operators.CompareString(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".", false) == 0;
				if (flag)
				{
					this.defaultExt = ".csv";
					GlobalDeclarations.DatalogUt.delimiter = ",";
				}
				else
				{
					this.defaultExt = ".csv";
					GlobalDeclarations.DatalogUt.delimiter = ";";
				}
				this.ToolTipDirectory.SetToolTip(this.txtDirectory, this.txtDirectory.Text);
				this.ToolTipDirectory.InitialDelay = 0;
				this.ToolTipDirectory.ReshowDelay = 0;
				this.ToolTipDirectory.AutoPopDelay = 30000;
				this.lblFileCount.Text = GlobalDeclarations.DatalogUt.FileCount.ToString();
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00014810 File Offset: 0x00012A10
		private void ButtonFolderDialog_Click(object sender, EventArgs e)
		{
			this.SaveFileDialog1.Filter = this.fileFilter;
			this.SaveFileDialog1.DefaultExt = this.defaultExt;
			this.SaveFileDialog1.FileName = Path.GetFileName(this.txtDirectory.Text);
			this.SaveFileDialog1.InitialDirectory = Path.GetDirectoryName(this.txtDirectory.Text);
			bool flag = this.SaveFileDialog1.ShowDialog(this) == DialogResult.OK;
			if (flag)
			{
				GlobalDeclarations.DatalogUt.Path = Path.GetDirectoryName(this.SaveFileDialog1.FileName);
				GlobalDeclarations.DatalogUt.FileName = Path.GetFileName(this.SaveFileDialog1.FileName);
				this.txtDirectory.Text = this.SaveFileDialog1.FileName;
				this.ToolTipDirectory.SetToolTip(this.txtDirectory, this.SaveFileDialog1.FileName);
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x000148FC File Offset: 0x00012AFC
		private void ButtonCountReSet_Click(object sender, EventArgs e)
		{
			GlobalDeclarations.DatalogUt.FileCount = 0UL;
			this.lblFileCount.Text = GlobalDeclarations.DatalogUt.FileCount.ToString();
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00014935 File Offset: 0x00012B35
		private void TextBoxSampRate_Click(object sender, EventArgs e)
		{
			Interaction.MsgBox("Sample rate may be changed by setting the appropriate register in the Register Access window.", MsgBoxStyle.OkOnly, null);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00014948 File Offset: 0x00012B48
		private void ButtonFolderDialog_Click1(object sender, EventArgs e)
		{
			this.SaveFileDialog1.Filter = this.fileFilter;
			this.SaveFileDialog1.DefaultExt = this.defaultExt;
			this.SaveFileDialog1.FileName = Path.GetFileName(this.txtDirectory.Text);
			this.SaveFileDialog1.InitialDirectory = Path.GetDirectoryName(this.txtDirectory.Text);
			bool flag = this.SaveFileDialog1.ShowDialog(this) == DialogResult.OK;
			if (flag)
			{
				GlobalDeclarations.DatalogUt.Path = Path.GetDirectoryName(this.SaveFileDialog1.FileName);
				GlobalDeclarations.DatalogUt.FileName = Path.GetFileName(this.SaveFileDialog1.FileName);
				this.txtDirectory.Text = this.SaveFileDialog1.FileName;
				this.ToolTipDirectory.SetToolTip(this.txtDirectory, this.SaveFileDialog1.FileName);
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00014A34 File Offset: 0x00012C34
		private void FormDataLog_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool isBusy = this.bgWorker.IsBusy;
			if (isBusy)
			{
				string text = "Window may not be closed while a data log is in progress.\r\n\r\n";
				text += "You may click the STOP button to stop the data log process.\r\n";
				Interaction.MsgBox(text, MsgBoxStyle.OkOnly, Application.ProductName);
				e.Cancel = true;
			}
			try
			{
				FileInfo fileInfo = new FileInfo(this.txtDirectory.Text);
				string directoryName = Path.GetDirectoryName(this.txtDirectory.Text);
				bool flag = !Directory.Exists(directoryName);
				if (flag)
				{
					string text2 = directoryName + "\r\n";
					text2 += "Does not exist.\r\n";
					text2 += "Enter an existing path or click Reset to Default.\r\n";
					Interaction.MsgBox(text2, MsgBoxStyle.OkOnly, null);
					e.Cancel = true;
				}
				GlobalDeclarations.DatalogUt.FileName = Path.GetFileName(this.txtDirectory.Text);
				GlobalDeclarations.DatalogUt.Path = Path.GetDirectoryName(this.txtDirectory.Text);
			}
			catch (Exception ex)
			{
				string text2 = ex.Message + "\r\n";
				text2 += "Enter a valid path and file name or click Reset to Default.";
				Interaction.MsgBox(text2, MsgBoxStyle.OkOnly, Application.ProductName);
				e.Cancel = true;
			}
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00002FAA File Offset: 0x000011AA
		private void ButtonOK_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00014B78 File Offset: 0x00012D78
		private void ButtonResetFullPath_Click(object sender, EventArgs e)
		{
			string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string str = "Datalog.csv";
			this.txtDirectory.Text = folderPath + "\\" + str;
		}

		// Token: 0x0400011F RID: 287
		private string fileFilter;

		// Token: 0x04000120 RID: 288
		private string defaultExt;

		// Token: 0x04000121 RID: 289
		private List<RegClass> RegList;

		// Token: 0x04000122 RID: 290
		private BindingList<FormDataLog.SelObject> SelList;

		// Token: 0x04000123 RID: 291
		public Action dutDrSetupRoutine;

		// Token: 0x04000124 RID: 292
		public Func<double> dutSampleRateRoutine;

		// Token: 0x0200003A RID: 58
		private class SelObject
		{
			// Token: 0x17000179 RID: 377
			// (get) Token: 0x0600046D RID: 1133 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
			// (set) Token: 0x0600046E RID: 1134 RVA: 0x0001C510 File Offset: 0x0001A710
			public string Label
			{
				get
				{
					return this._Label;
				}
				set
				{
					this._Label = value;
				}
			}

			// Token: 0x1700017A RID: 378
			// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001C51C File Offset: 0x0001A71C
			// (set) Token: 0x06000470 RID: 1136 RVA: 0x0001C534 File Offset: 0x0001A734
			public string EvalLabel
			{
				get
				{
					return this._EvalLabel;
				}
				set
				{
					this._EvalLabel = value;
				}
			}

			// Token: 0x1700017B RID: 379
			// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001C540 File Offset: 0x0001A740
			// (set) Token: 0x06000472 RID: 1138 RVA: 0x0001C558 File Offset: 0x0001A758
			public bool Selected
			{
				get
				{
					return this._Selected;
				}
				set
				{
					this._Selected = value;
				}
			}

			// Token: 0x04000203 RID: 515
			private string _Label;

			// Token: 0x04000204 RID: 516
			private string _EvalLabel;

			// Token: 0x04000205 RID: 517
			private bool _Selected;
		}
	}
}
