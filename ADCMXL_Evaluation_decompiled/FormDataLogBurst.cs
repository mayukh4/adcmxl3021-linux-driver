using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using AdisApi;
using adisInterface;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;

namespace Vibration_Evaluation
{
	// Token: 0x02000014 RID: 20
	[DesignerGenerated]
	public partial class FormDataLogBurst : Form
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000AB7B File Offset: 0x00008D7B
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000AB88 File Offset: 0x00008D88
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
				EventHandler value2 = new EventHandler(this.ButtonStart_Click);
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000ABCB File Offset: 0x00008DCB
		// (set) Token: 0x06000139 RID: 313 RVA: 0x0000ABD8 File Offset: 0x00008DD8
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
				EventHandler value2 = new EventHandler(this.ButtonFolderDialog_Click);
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000AC1B File Offset: 0x00008E1B
		// (set) Token: 0x0600013B RID: 315 RVA: 0x0000AC25 File Offset: 0x00008E25
		internal virtual TextBox txtFileNameAndPath { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000AC2E File Offset: 0x00008E2E
		// (set) Token: 0x0600013D RID: 317 RVA: 0x0000AC38 File Offset: 0x00008E38
		internal virtual Panel Panel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000AC41 File Offset: 0x00008E41
		// (set) Token: 0x0600013F RID: 319 RVA: 0x0000AC4B File Offset: 0x00008E4B
		internal virtual ToolTip ToolTipDirectory { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000AC54 File Offset: 0x00008E54
		// (set) Token: 0x06000141 RID: 321 RVA: 0x0000AC5E File Offset: 0x00008E5E
		internal virtual StatusStrip StatusStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000AC67 File Offset: 0x00008E67
		// (set) Token: 0x06000143 RID: 323 RVA: 0x0000AC71 File Offset: 0x00008E71
		internal virtual ToolStripProgressBar ProgressBar1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000AC7A File Offset: 0x00008E7A
		// (set) Token: 0x06000145 RID: 325 RVA: 0x0000AC84 File Offset: 0x00008E84
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000AC8D File Offset: 0x00008E8D
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000AC97 File Offset: 0x00008E97
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000ACA0 File Offset: 0x00008EA0
		// (set) Token: 0x06000149 RID: 329 RVA: 0x0000ACAC File Offset: 0x00008EAC
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000ACEF File Offset: 0x00008EEF
		// (set) Token: 0x0600014B RID: 331 RVA: 0x0000ACF9 File Offset: 0x00008EF9
		internal virtual SaveFileDialog SaveFileDialog1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000AD02 File Offset: 0x00008F02
		// (set) Token: 0x0600014D RID: 333 RVA: 0x0000AD0C File Offset: 0x00008F0C
		internal virtual Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000AD15 File Offset: 0x00008F15
		// (set) Token: 0x0600014F RID: 335 RVA: 0x0000AD1F File Offset: 0x00008F1F
		internal virtual Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000AD28 File Offset: 0x00008F28
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000AD32 File Offset: 0x00008F32
		internal virtual TextBox TextBoxCapTime { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000AD3B File Offset: 0x00008F3B
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000AD45 File Offset: 0x00008F45
		internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000AD4E File Offset: 0x00008F4E
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000AD58 File Offset: 0x00008F58
		internal virtual ToolTip ToolTipCapTime { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000AD61 File Offset: 0x00008F61
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000AD6B File Offset: 0x00008F6B
		internal virtual ToolTip ToolTipRecordLength { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000AD74 File Offset: 0x00008F74
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000AD7E File Offset: 0x00008F7E
		internal virtual ToolStripStatusLabel ToolStripStatusLabel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000AD87 File Offset: 0x00008F87
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000AD91 File Offset: 0x00008F91
		internal virtual ToolStripStatusLabel ToolStripStatusLabel3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000AD9A File Offset: 0x00008F9A
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000ADA4 File Offset: 0x00008FA4
		internal virtual TextBox TextBoxRecLength
		{
			[CompilerGenerated]
			get
			{
				return this._TextBoxRecLength;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.TextBoxRecLength_TextChanged);
				TextBox textBoxRecLength = this._TextBoxRecLength;
				if (textBoxRecLength != null)
				{
					textBoxRecLength.TextChanged -= value2;
				}
				this._TextBoxRecLength = value;
				textBoxRecLength = this._TextBoxRecLength;
				if (textBoxRecLength != null)
				{
					textBoxRecLength.TextChanged += value2;
				}
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000ADE7 File Offset: 0x00008FE7
		// (set) Token: 0x0600015F RID: 351 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		internal virtual TextBox TextBoxLinesPerFile
		{
			[CompilerGenerated]
			get
			{
				return this._TextBoxLinesPerFile;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.TextBoxLinesPerFile_TextChanged);
				TextBox textBoxLinesPerFile = this._TextBoxLinesPerFile;
				if (textBoxLinesPerFile != null)
				{
					textBoxLinesPerFile.TextChanged -= value2;
				}
				this._TextBoxLinesPerFile = value;
				textBoxLinesPerFile = this._TextBoxLinesPerFile;
				if (textBoxLinesPerFile != null)
				{
					textBoxLinesPerFile.TextChanged += value2;
				}
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000AE37 File Offset: 0x00009037
		// (set) Token: 0x06000161 RID: 353 RVA: 0x0000AE41 File Offset: 0x00009041
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000162 RID: 354 RVA: 0x0000AE4A File Offset: 0x0000904A
		// (set) Token: 0x06000163 RID: 355 RVA: 0x0000AE54 File Offset: 0x00009054
		internal virtual CheckBox CheckBoxExtTrig
		{
			[CompilerGenerated]
			get
			{
				return this._CheckBoxExtTrig;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.CheckBoxExtTrig_CheckedChanged);
				CheckBox checkBoxExtTrig = this._CheckBoxExtTrig;
				if (checkBoxExtTrig != null)
				{
					checkBoxExtTrig.CheckedChanged -= value2;
				}
				this._CheckBoxExtTrig = value;
				checkBoxExtTrig = this._CheckBoxExtTrig;
				if (checkBoxExtTrig != null)
				{
					checkBoxExtTrig.CheckedChanged += value2;
				}
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000AE97 File Offset: 0x00009097
		// (set) Token: 0x06000165 RID: 357 RVA: 0x0000AEA1 File Offset: 0x000090A1
		internal virtual Label LabelNOTE { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000AEAA File Offset: 0x000090AA
		// (set) Token: 0x06000167 RID: 359 RVA: 0x0000AEB4 File Offset: 0x000090B4
		internal virtual Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000AEBD File Offset: 0x000090BD
		// (set) Token: 0x06000169 RID: 361 RVA: 0x0000AEC8 File Offset: 0x000090C8
		private virtual TextFileStreamManager TFSM
		{
			[CompilerGenerated]
			get
			{
				return this._TFSM;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				ProgressChangedEventHandler obj = new ProgressChangedEventHandler(this.TFSM_ProgressChanged);
				RunAsyncCompletedEventHandler obj2 = new RunAsyncCompletedEventHandler(this.TFSM_RunWorkerCompleted);
				TextFileStreamManager tfsm = this._TFSM;
				if (tfsm != null)
				{
					tfsm.ProgressChanged -= obj;
					tfsm.RunAsyncCompleted -= obj2;
				}
				this._TFSM = value;
				tfsm = this._TFSM;
				if (tfsm != null)
				{
					tfsm.ProgressChanged += obj;
					tfsm.RunAsyncCompleted += obj2;
				}
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000AF28 File Offset: 0x00009128
		public FormDataLogBurst(int prodID)
		{
			base.Load += this.FormDataLog_Load;
			base.Shown += this.FormDataLogBurst_Shown;
			base.FormClosing += this.FormDataLog_FormClosing;
			base.Closing += this.FormDataLogBurst_Closing;
			this.fileFilter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
			this.Samples = 6987;
			this.ScaleData = true;
			this.TFSM = new TextFileStreamManager();
			this.RunAsyncCompletedEventArgs = new RunAsyncCompletedEventArgs();
			this.InitializeComponent();
			this.productID = prodID;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000AFCC File Offset: 0x000091CC
		private void FormDataLog_Load(object sender, EventArgs e)
		{
			this.LabelNOTE.Text = "NOTE : The ADcmXL data log produces 8 MB of data per second = 220,704 csv file lines.\r\n";
			this.LabelNOTE.Text = this.LabelNOTE.Text + "On some PC's a 1 second capture can require 2 seconds to write files.\r\n";
			this.LabelNOTE.Text = this.LabelNOTE.Text + "File writes queued to the Windows system can only be canceled by closing this application.";
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B030 File Offset: 0x00009230
		private void FormDataLogBurst_Shown(object sender, EventArgs e)
		{
			bool flag = GlobalDeclarations.BoardID == GlobalDeclarations.BoardIDtype.SDPEVAL;
			if (flag)
			{
				MsgBoxResult msgBoxResult = Interaction.MsgBox("An SDP type board cannot capture real time data.", MsgBoxStyle.OkOnly, null);
				base.Close();
			}
			try
			{
				bool flag2 = this.TFSM.RegList == null;
				if (flag2)
				{
					this.TFSM.RegList = new List<RegClass>();
				}
				bool flag3 = Operators.CompareString(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".", false) == 0;
				if (flag3)
				{
					this.TFSM.DataSeperator = ",";
				}
				else
				{
					this.TFSM.DataSeperator = ";";
				}
				bool flag4 = Operators.CompareString(GlobalDeclarations.TFSMconfig.FileNameBase, "", false) == 0;
				if (flag4)
				{
					this.TFSM.FilePath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
					this.TFSM.FileBaseName = "Datalog";
					this.TFSM.FileExtension = "csv";
					this.TextBoxRecLength.Text = "6897";
					this.TextBoxLinesPerFile.Text = "1000000";
				}
				else
				{
					this.TFSM.FilePath = GlobalDeclarations.TFSMconfig.FilePath;
					this.TFSM.FileBaseName = GlobalDeclarations.TFSMconfig.FileNameBase;
					this.TextBoxRecLength.Text = GlobalDeclarations.TFSMconfig.numSamples.ToString();
					this.TextBoxLinesPerFile.Text = GlobalDeclarations.TFSMconfig.LinesPerFile.ToString();
				}
				this.txtFileNameAndPath.Text = string.Concat(new string[]
				{
					this.TFSM.FilePath,
					"\\",
					this.TFSM.FileBaseName,
					".",
					this.TFSM.FileExtension
				});
				this.ToolTipCapTime.SetToolTip(this.TextBoxCapTime, "Hours:Minutes:Seconds.Tenths");
				this.ToolTipCapTime.InitialDelay = 0;
				this.ToolTipCapTime.ReshowDelay = 0;
				this.ToolTipCapTime.AutoPopDelay = 30000;
				this.ToolTipRecordLength.SetToolTip(this.TextBoxRecLength, "Due to USB buffering, the number of recorded samples may be greater.");
				this.ToolTipRecordLength.InitialDelay = 0;
				this.ToolTipRecordLength.ReshowDelay = 0;
				this.ToolTipRecordLength.AutoPopDelay = 3000;
				this.UpdateTextBoxCapTime();
				this.ProgressBar1.Visible = true;
				this.ToolStripStatusLabel3.Visible = true;
			}
			catch (Exception ex)
			{
				Interaction.MsgBox("FormDataLogBurst.vb event Form_Shown" + ex.Message, MsgBoxStyle.OkOnly, null);
			}
			Application.DoEvents();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B2E8 File Offset: 0x000094E8
		private void ButtonStart_Click(object sender, EventArgs e)
		{
			this.ButtonCancel.Visible = true;
			this.ButtonCancel.Enabled = true;
			this.ButtonStart.Enabled = false;
			GlobalDeclarations.FX3comm.SclkFrequency = GlobalDeclarations.SPIsclkDefault;
			Thread.Sleep(100);
			this.RunDatalog();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B33C File Offset: 0x0000953C
		private void CheckBoxExtTrig_CheckedChanged(object sender, EventArgs e)
		{
			string text = string.Concat(new string[]
			{
				this.TFSM.FilePath,
				"\\",
				this.TFSM.FileBaseName,
				"_1.",
				this.TFSM.FileExtension
			});
			bool @checked = this.CheckBoxExtTrig.Checked;
			if (@checked)
			{
				this.ButtonStart.Enabled = false;
				this.ExtTrigLoop();
			}
			else
			{
				this.ButtonStart.Enabled = true;
			}
			Application.DoEvents();
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000B3CC File Offset: 0x000095CC
		public void ExtTrigLoop()
		{
			string fileBaseName = this.TFSM.FileBaseName;
			int num = 1;
			checked
			{
				int length = this.txtFileNameAndPath.Text.Length - 4;
				string str = Strings.Left(this.txtFileNameAndPath.Text, length);
				while (this.CheckBoxExtTrig.Checked)
				{
					this.waitForExtTrig();
					bool @checked = this.CheckBoxExtTrig.Checked;
					if (@checked)
					{
						this.txtFileNameAndPath.Text = str + "_" + num.ToString() + ".csv";
						this.RunDatalog();
						while (this.TFSM.IsBusy)
						{
							Application.DoEvents();
						}
						num++;
					}
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B484 File Offset: 0x00009684
		public void waitForExtTrig()
		{
			int num = 0;
			IPinObject dio = GlobalDeclarations.FX3comm.DIO4;
			uint num2 = 0U;
			this.CheckBoxExtTrig.Text = "Polling DIO 4";
			Application.DoEvents();
			num2 = GlobalDeclarations.FX3comm.ReadPin(dio);
			try
			{
				while ((ulong)num2 == 0UL & this.CheckBoxExtTrig.Checked)
				{
					Thread.Sleep(100);
					num2 = GlobalDeclarations.FX3comm.ReadPin(dio);
					checked
					{
						num++;
						Application.DoEvents();
					}
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
			}
			this.CheckBoxExtTrig.Text = "Enable External Trigger";
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B540 File Offset: 0x00009740
		private void ButtonCancel_Click(object sender, EventArgs e)
		{
			bool isBusy = this.TFSM.IsBusy;
			if (isBusy)
			{
				this.TFSM.CancelAsync();
			}
			this.ButtonCancel.Enabled = false;
			this.UIshowIdle();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000B580 File Offset: 0x00009780
		private void RunDatalog()
		{
			try
			{
				this.TFSM.DutInterface = (IBufferedStreamProducer)GlobalDeclarations.Dut;
			}
			catch (Exception ex)
			{
				Interaction.MsgBox("FormDataLogBurst sub RunDatalog\r" + ex.Message, MsgBoxStyle.OkOnly, null);
			}
			this.TFSM.FileBaseName = Path.GetFileNameWithoutExtension(this.txtFileNameAndPath.Text.Trim());
			this.TFSM.FilePath = Path.GetDirectoryName(this.txtFileNameAndPath.Text.Trim());
			this.TFSM.FileExtension = Path.GetExtension(this.txtFileNameAndPath.Text.Trim());
			string text = string.Concat(new string[]
			{
				this.TFSM.FilePath,
				"\\",
				this.TFSM.FileBaseName,
				"_0000.",
				this.TFSM.FileExtension
			});
			try
			{
				FileInfo fileInfo = new FileInfo(text);
				bool flag = Directory.Exists(this.TFSM.FilePath);
				bool flag2 = !flag;
				if (flag2)
				{
					SystemException ex2 = new SystemException("Directory path does not exist.");
					throw ex2;
				}
			}
			catch (Exception ex3)
			{
				Interaction.MsgBox("FormDataLogBurst sub RunDataLog()" + ex3.Message, MsgBoxStyle.Exclamation, null);
				this.UIshowIdle();
				return;
			}
			bool flag3 = File.Exists(text);
			if (flag3)
			{
				string text2 = "The file\r\n";
				text2 = text2 + text + "\r\n";
				text2 += "already exists.\r\n\r\n";
				text2 += "All files with the base name\r\n";
				text2 = text2 + this.TFSM.FileBaseName + "\r\n";
				text2 += "must be moved, or a different path must be selected.";
				Interaction.MsgBox("FormDataLogBurst sub RunDataLog() file.exist" + text2, MsgBoxStyle.Exclamation, null);
				this.UIshowIdle();
			}
			else
			{
				try
				{
					this.Samples = Conversions.ToInteger(this.TextBoxRecLength.Text);
					this.TFSM.Captures = 1U;
					this.TFSM.Buffers = checked((uint)this.Samples);
					this.TFSM.BuffersPerWrite = 6897U;
					uint buffersPerWrite = this.TFSM.BuffersPerWrite;
					this.TFSM.FileMaxDataRows = Conversions.ToUInteger(this.TextBoxLinesPerFile.Text);
				}
				catch (Exception ex4)
				{
					Interaction.MsgBox("FormDataLogBurst sub runDatalog call runAsync\r\n" + ex4.Message, MsgBoxStyle.OkOnly, null);
					return;
				}
				this.UIshowRunning();
				this.ProgressBar1.Value = 0;
				this.TFSM.BufferTimeout = 5;
				int num = this.productID;
				if (num != 1021)
				{
					if (num != 2021)
					{
						if (num != 3021)
						{
							string prompt = "FormDataLogBurst Error\r\n" + Conversions.ToString(this.productID) + "  is an Unknown Device.";
							Interaction.MsgBox(prompt, MsgBoxStyle.OkOnly, null);
						}
						else
						{
							this.TFSM.RegList = GlobalDeclarations.Dutcmi3x.RealTimeSamplingRegList;
						}
					}
					else
					{
						this.TFSM.RegList = GlobalDeclarations.Dutcmi2x.RealTimeSamplingRegList;
					}
				}
				else
				{
					this.TFSM.RegList = GlobalDeclarations.Dutcmi1x.RealTimeSamplingRegList;
					this.TFSM.DutInterface = GlobalDeclarations.Dutcmi1x;
				}
				GlobalDeclarations.FX3comm.PinExit = true;
				GlobalDeclarations.Dut.WriteUnsigned(GlobalDeclarations.Reg["REC_CTRL"], 259U);
				Thread.Sleep(100);
				try
				{
					this.TFSM.RunAsync();
				}
				catch (Exception ex5)
				{
					Interaction.MsgBox("FormDataLogBurst sub runDatalog call runAsync\r\n" + ex5.Message, MsgBoxStyle.OkOnly, null);
				}
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000B97C File Offset: 0x00009B7C
		private void TFSM_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			this.ProgressBar1.Value = e.ProgressPercentage;
			this.ToolStripStatusLabel3.Text = string.Format("{0:D}%", e.ProgressPercentage).PadLeft(5);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		private void TFSM_RunWorkerCompleted(object sender, RunAsyncCompletedEventArgs e)
		{
			this.UIshowIdle();
			bool cancelled = e.Cancelled;
			if (cancelled)
			{
				Interaction.MsgBox("Data capture was cancelled.", MsgBoxStyle.OkOnly, null);
			}
			bool flag = e.DequeueError != null;
			if (flag)
			{
				Interaction.MsgBox("FormDataLogBurst sub RunWorkerCompleted\r\n" + e.DequeueError.Message, MsgBoxStyle.Exclamation, null);
			}
			bool flag2 = e.EnqueueError != null;
			if (flag2)
			{
				Interaction.MsgBox("FormDataLogBurst sub RunWorkerCompleted\r\n" + e.EnqueueError.Message, MsgBoxStyle.Exclamation, null);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000BA40 File Offset: 0x00009C40
		public void UIshowRunning()
		{
			this.ProgressBar1.Visible = true;
			this.ToolStripStatusLabel1.Text = "Recording Data";
			this.ToolStripStatusLabel1.BackColor = Color.FromArgb(255, 255, 180);
			this.ToolStripStatusLabel3.Text = "";
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		public void UIshowIdle()
		{
			this.ButtonStart.Enabled = true;
			this.ButtonCancel.Enabled = false;
			this.ButtonCancel.Visible = false;
			this.ProgressBar1.Visible = false;
			this.ToolStripStatusLabel1.Text = "Ready";
			this.ToolStripStatusLabel1.BackColor = Color.FromKnownColor(KnownColor.Control);
			this.ToolStripStatusLabel3.Text = "";
			this.ToolStripStatusLabel3.Visible = false;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0000BB24 File Offset: 0x00009D24
		private void ButtonFolderDialog_Click(object sender, EventArgs e)
		{
			this.SaveFileDialog1.Filter = this.fileFilter;
			this.SaveFileDialog1.DefaultExt = this.TFSM.FileExtension;
			this.SaveFileDialog1.FileName = Path.GetFileName(this.txtFileNameAndPath.Text);
			this.SaveFileDialog1.InitialDirectory = Path.GetDirectoryName(this.txtFileNameAndPath.Text);
			bool flag = this.SaveFileDialog1.ShowDialog(this) == DialogResult.OK;
			if (flag)
			{
				this.DirPath = Path.GetDirectoryName(this.SaveFileDialog1.FileName);
				this.TFSM.FileBaseName = Path.GetFileName(this.SaveFileDialog1.FileName);
				this.TFSM.FilePath = Path.GetDirectoryName(this.SaveFileDialog1.FileName);
				this.txtFileNameAndPath.Text = this.SaveFileDialog1.FileName;
				this.ToolTipDirectory.SetToolTip(this.txtFileNameAndPath, this.SaveFileDialog1.FileName);
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0000BC30 File Offset: 0x00009E30
		private void FormDataLog_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool isBusy = this.TFSM.IsBusy;
			if (isBusy)
			{
				MsgBoxResult msgBoxResult = Interaction.MsgBox("Cancel data collection ?", MsgBoxStyle.YesNo, null);
				bool flag = msgBoxResult == MsgBoxResult.No;
				if (flag)
				{
					e.Cancel = true;
				}
				else
				{
					this.TFSM.CancelAsync();
					Thread.Sleep(1000);
				}
			}
			else
			{
				uint num;
				bool flag2 = uint.TryParse(this.TextBoxRecLength.Text, out num);
				if (flag2)
				{
					GlobalDeclarations.TFSMconfig.numSamples = checked((int)num);
				}
				GlobalDeclarations.TFSMconfig.FileNameBase = Path.GetFileNameWithoutExtension(this.txtFileNameAndPath.Text.Trim());
				GlobalDeclarations.TFSMconfig.FilePath = Path.GetDirectoryName(this.txtFileNameAndPath.Text.Trim());
				this.TFSM.FileExtension = Path.GetExtension(this.txtFileNameAndPath.Text.Trim());
				GlobalDeclarations.TFSMconfig.LinesPerFile = Conversions.ToInteger(this.TextBoxLinesPerFile.Text);
			}
			GlobalDeclarations.FX3comm.DrActive = false;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000BD3C File Offset: 0x00009F3C
		private void UpdateTextBoxCapTime()
		{
			uint num;
			bool flag = uint.TryParse(this.TextBoxRecLength.Text, out num) && (ulong)num > 0UL;
			if (flag)
			{
				double num2 = 6896.551724137931;
				TimeSpan timeSpan = TimeSpan.FromMilliseconds(1000.0 * (num / num2));
				bool flag2 = timeSpan.TotalMilliseconds < 100.0;
				if (flag2)
				{
					timeSpan = TimeSpan.FromMilliseconds(100.0);
				}
				this.TextBoxCapTime.Text = string.Format("{0:00}:{1:00}:{2:00}.{3:0}", new object[]
				{
					Math.Truncate(timeSpan.TotalHours),
					timeSpan.Minutes,
					timeSpan.Seconds,
					Math.Truncate((double)timeSpan.Milliseconds / 100.0)
				});
				string caption = string.Concat(new string[]
				{
					timeSpan.Hours.ToString(),
					" hrs ",
					timeSpan.Minutes.ToString(),
					" min ",
					timeSpan.Seconds.ToString(),
					".",
					Math.Truncate((double)timeSpan.Milliseconds / 100.0).ToString(),
					" sec"
				});
				this.ToolTipCapTime.SetToolTip(this.TextBoxCapTime, caption);
			}
			else
			{
				this.TextBoxCapTime.Text = "not valid";
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		private void TextBoxRecLength_Leave(object sender, EventArgs e)
		{
			uint num;
			bool flag = uint.TryParse(this.TextBoxRecLength.Text, out num) && (ulong)num > 0UL;
			if (!flag)
			{
				Interaction.MsgBox("Please enter a positive integer value for Record length.", MsgBoxStyle.OkOnly, null);
				this.TextBoxRecLength.Select();
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000BF23 File Offset: 0x0000A123
		private void TextBoxRecLength_TextChanged(object sender, EventArgs e)
		{
			this.UpdateTextBoxCapTime();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000BF30 File Offset: 0x0000A130
		private void FormDataLogBurst_Closing(object sender, CancelEventArgs e)
		{
			bool isBusy = this.TFSM.IsBusy;
			if (isBusy)
			{
				this.TFSM.CancelAsync();
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x0000BF5C File Offset: 0x0000A15C
		private void TextBoxLinesPerFile_TextChanged(object sender, EventArgs e)
		{
			bool flag = Conversions.ToDouble(this.TextBoxLinesPerFile.Text) < 30000.0;
			if (flag)
			{
				this.TextBoxLinesPerFile.Text = "30000";
			}
		}

		// Token: 0x04000097 RID: 151
		private string fileFilter;

		// Token: 0x04000098 RID: 152
		private string DirPath;

		// Token: 0x04000099 RID: 153
		private double SampleRate;

		// Token: 0x0400009A RID: 154
		private int Samples;

		// Token: 0x0400009B RID: 155
		private bool ScaleData;

		// Token: 0x0400009C RID: 156
		public static uint FileMaxDataRowsAdvOp = 0U;

		// Token: 0x0400009E RID: 158
		private RunAsyncCompletedEventArgs RunAsyncCompletedEventArgs;

		// Token: 0x0400009F RID: 159
		private int productID;
	}
}
