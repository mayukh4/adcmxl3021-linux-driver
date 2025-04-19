using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;
using Vibration_Evaluation.My;

namespace Vibration_Evaluation
{
	// Token: 0x02000015 RID: 21
	[DesignerGenerated]
	public partial class FormRegAccess : Form
	{
		// Token: 0x0600017E RID: 382 RVA: 0x0000BF9C File Offset: 0x0000A19C
		public FormRegAccess()
		{
			base.Load += this.FormRegAccess_Load;
			base.Shown += this.FormRegAccess_Shown;
			this.fileFilter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
			this.regSelectList = new BindingList<FormRegAccess.RegContents>();
			this.cmdSelectList = new BindingList<FormRegAccess.CmdContents>();
			this.regTypes = new Dictionary<string, RegClass.RegType>();
			this.InitializeComponent();
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000D0B5 File Offset: 0x0000B2B5
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		internal virtual ComboBox cbxRegType
		{
			[CompilerGenerated]
			get
			{
				return this._cbxRegType;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cbxRegType_SelectedIndexChanged);
				ComboBox cbxRegType = this._cbxRegType;
				if (cbxRegType != null)
				{
					cbxRegType.SelectedIndexChanged -= value2;
				}
				this._cbxRegType = value;
				cbxRegType = this._cbxRegType;
				if (cbxRegType != null)
				{
					cbxRegType.SelectedIndexChanged += value2;
				}
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000D103 File Offset: 0x0000B303
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000D10D File Offset: 0x0000B30D
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000D116 File Offset: 0x0000B316
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000D120 File Offset: 0x0000B320
		internal virtual Label Label2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000D129 File Offset: 0x0000B329
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000D134 File Offset: 0x0000B334
		internal virtual Button ButtonWriteReg
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonWriteReg;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonWrite_Click);
				Button buttonWriteReg = this._ButtonWriteReg;
				if (buttonWriteReg != null)
				{
					buttonWriteReg.Click -= value2;
				}
				this._ButtonWriteReg = value;
				buttonWriteReg = this._ButtonWriteReg;
				if (buttonWriteReg != null)
				{
					buttonWriteReg.Click += value2;
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000D177 File Offset: 0x0000B377
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000D184 File Offset: 0x0000B384
		internal virtual DataGridView dgvRegList
		{
			[CompilerGenerated]
			get
			{
				return this._dgvRegList;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				DataGridViewCellEventHandler value2 = new DataGridViewCellEventHandler(this.dgvRegList_CellEnter);
				DataGridView dgvRegList = this._dgvRegList;
				if (dgvRegList != null)
				{
					dgvRegList.CellEnter -= value2;
				}
				this._dgvRegList = value;
				dgvRegList = this._dgvRegList;
				if (dgvRegList != null)
				{
					dgvRegList.CellEnter += value2;
				}
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000D1C7 File Offset: 0x0000B3C7
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000D1D1 File Offset: 0x0000B3D1
		internal virtual TextBox txtHex { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000D1DA File Offset: 0x0000B3DA
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000D1ED File Offset: 0x0000B3ED
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000D1F7 File Offset: 0x0000B3F7
		internal virtual Label lblSelected { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000D200 File Offset: 0x0000B400
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000D20A File Offset: 0x0000B40A
		internal virtual Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000D213 File Offset: 0x0000B413
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000D21D File Offset: 0x0000B41D
		internal virtual Label lblCurrentValue { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000D226 File Offset: 0x0000B426
		// (set) Token: 0x06000196 RID: 406 RVA: 0x0000D230 File Offset: 0x0000B430
		internal virtual Button ButtonReadAll
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonReadAll;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonReadAll_Click);
				Button buttonReadAll = this._ButtonReadAll;
				if (buttonReadAll != null)
				{
					buttonReadAll.Click -= value2;
				}
				this._ButtonReadAll = value;
				buttonReadAll = this._ButtonReadAll;
				if (buttonReadAll != null)
				{
					buttonReadAll.Click += value2;
				}
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000D273 File Offset: 0x0000B473
		// (set) Token: 0x06000198 RID: 408 RVA: 0x0000D27D File Offset: 0x0000B47D
		internal virtual StatusStrip StatusStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000D286 File Offset: 0x0000B486
		// (set) Token: 0x0600019A RID: 410 RVA: 0x0000D290 File Offset: 0x0000B490
		internal virtual ToolStripStatusLabel ToolStripStatusLabel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600019B RID: 411 RVA: 0x0000D299 File Offset: 0x0000B499
		// (set) Token: 0x0600019C RID: 412 RVA: 0x0000D2A3 File Offset: 0x0000B4A3
		internal virtual ToolStripStatusLabel ToolStripStatusLabel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000D2AC File Offset: 0x0000B4AC
		// (set) Token: 0x0600019E RID: 414 RVA: 0x0000D2B8 File Offset: 0x0000B4B8
		internal virtual Button ButtonWriteFile
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonWriteFile;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonWriteFile_Click);
				Button buttonWriteFile = this._ButtonWriteFile;
				if (buttonWriteFile != null)
				{
					buttonWriteFile.Click -= value2;
				}
				this._ButtonWriteFile = value;
				buttonWriteFile = this._ButtonWriteFile;
				if (buttonWriteFile != null)
				{
					buttonWriteFile.Click += value2;
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600019F RID: 415 RVA: 0x0000D2FB File Offset: 0x0000B4FB
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x0000D308 File Offset: 0x0000B508
		internal virtual Button ButtonReadFile
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonReadFile;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonReadFile_Click);
				Button buttonReadFile = this._ButtonReadFile;
				if (buttonReadFile != null)
				{
					buttonReadFile.Click -= value2;
				}
				this._ButtonReadFile = value;
				buttonReadFile = this._ButtonReadFile;
				if (buttonReadFile != null)
				{
					buttonReadFile.Click += value2;
				}
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x0000D34B File Offset: 0x0000B54B
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x0000D355 File Offset: 0x0000B555
		internal virtual Label LabelCommandPanel { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x0000D35E File Offset: 0x0000B55E
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x0000D368 File Offset: 0x0000B568
		internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000D371 File Offset: 0x0000B571
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000D37B File Offset: 0x0000B57B
		internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000D384 File Offset: 0x0000B584
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x0000D38E File Offset: 0x0000B58E
		internal virtual Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0000D397 File Offset: 0x0000B597
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		internal virtual DataGridView dgvCommand
		{
			[CompilerGenerated]
			get
			{
				return this._dgvCommand;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				DataGridViewCellEventHandler value2 = new DataGridViewCellEventHandler(this.dgvCommand_CellClick);
				DataGridView dgvCommand = this._dgvCommand;
				if (dgvCommand != null)
				{
					dgvCommand.CellClick -= value2;
				}
				this._dgvCommand = value;
				dgvCommand = this._dgvCommand;
				if (dgvCommand != null)
				{
					dgvCommand.CellClick += value2;
				}
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000D3E7 File Offset: 0x0000B5E7
		// (set) Token: 0x060001AC RID: 428 RVA: 0x0000D3F4 File Offset: 0x0000B5F4
		internal virtual ComboBox cbxCmdReg
		{
			[CompilerGenerated]
			get
			{
				return this._cbxCmdReg;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cbxCmdReg_SelectedIndexChanged);
				ComboBox cbxCmdReg = this._cbxCmdReg;
				if (cbxCmdReg != null)
				{
					cbxCmdReg.SelectedIndexChanged -= value2;
				}
				this._cbxCmdReg = value;
				cbxCmdReg = this._cbxCmdReg;
				if (cbxCmdReg != null)
				{
					cbxCmdReg.SelectedIndexChanged += value2;
				}
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000D437 File Offset: 0x0000B637
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000D444 File Offset: 0x0000B644
		internal virtual Button ButtonReadOne
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonReadOne;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonReadOne_Click);
				Button buttonReadOne = this._ButtonReadOne;
				if (buttonReadOne != null)
				{
					buttonReadOne.Click -= value2;
				}
				this._ButtonReadOne = value;
				buttonReadOne = this._ButtonReadOne;
				if (buttonReadOne != null)
				{
					buttonReadOne.Click += value2;
				}
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000D488 File Offset: 0x0000B688
		private void FormRegAccess_Load(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					this.FormBusy = true;
					bool flag = Operators.CompareString(NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, ".", false) == 0;
					if (flag)
					{
						this.fileExt = ".csv";
						this.delim = ",";
					}
					else
					{
						this.fileExt = ".txt";
						this.delim = "\t";
					}
					base.Left = MyProject.Forms.FormMain.Left + 95;
					base.Top = MyProject.Forms.FormMain.Top + 65;
					bool flag2 = GlobalDeclarations.BoardID == GlobalDeclarations.BoardIDtype.NONE;
					if (flag2)
					{
						this.ButtonReadOne.Enabled = false;
						this.ButtonReadAll.Enabled = false;
						this.ButtonWriteReg.Enabled = false;
					}
					this.ToolStripStatusLabel1.Text = "";
					this.ToolStripStatusLabel2.Text = "";
					this.SetupCommandGrid();
					this.SetupCmdComboBox();
					this.SetupRegisterGrid();
					this.SetupRegComboBox();
					this.FormBusy = false;
				}
				catch (Exception ex)
				{
					Interaction.MsgBox(ex.ToString(), MsgBoxStyle.OkOnly, null);
				}
				this.ReadCategory();
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000D5DC File Offset: 0x0000B7DC
		private void SetupRegComboBox()
		{
			int selectedIndex = 0;
			this.regTypes.Clear();
			this.cbxRegType.Items.Clear();
			IEnumerable<RegClass.RegType> source = GlobalDeclarations.Reg.Select((FormRegAccess._Closure$__.$I107-0 == null) ? (FormRegAccess._Closure$__.$I107-0 = ((RegClass r) => r.Type)) : FormRegAccess._Closure$__.$I107-0).Distinct<RegClass.RegType>();
			bool flag = source.Contains(RegClass.RegType.UserOutputLow);
			if (flag)
			{
				this.regTypes.Add("OutputLow", RegClass.RegType.UserOutputLow);
			}
			bool flag2 = source.Contains(RegClass.RegType.UserOutput);
			if (flag2)
			{
				this.regTypes.Add("Output", RegClass.RegType.UserOutput);
			}
			bool flag3 = source.Contains(RegClass.RegType.Control);
			if (flag3)
			{
				this.regTypes.Add("Control/Status", RegClass.RegType.Control);
				selectedIndex = checked(this.regTypes.Count - 1);
			}
			bool flag4 = source.Contains(RegClass.RegType.UserCal);
			if (flag4)
			{
				this.regTypes.Add("Calibration", RegClass.RegType.UserCal);
			}
			bool flag5 = source.Contains(RegClass.RegType.FilterA);
			if (flag5)
			{
				this.regTypes.Add("Filter Bank A", RegClass.RegType.FilterA);
			}
			bool flag6 = source.Contains(RegClass.RegType.FilterB);
			if (flag6)
			{
				this.regTypes.Add("Filter Bank B", RegClass.RegType.FilterB);
			}
			bool flag7 = source.Contains(RegClass.RegType.FilterC);
			if (flag7)
			{
				this.regTypes.Add("Filter Bank C", RegClass.RegType.FilterC);
			}
			bool flag8 = source.Contains(RegClass.RegType.FilterD);
			if (flag8)
			{
				this.regTypes.Add("Filter Bank D", RegClass.RegType.FilterD);
			}
			bool flag9 = source.Contains(RegClass.RegType.FilterE);
			if (flag9)
			{
				this.regTypes.Add("Filter Bank E", RegClass.RegType.FilterE);
			}
			bool flag10 = source.Contains(RegClass.RegType.FilterF);
			if (flag10)
			{
				this.regTypes.Add("Filter Bank F", RegClass.RegType.FilterF);
			}
			this.cbxRegType.Items.AddRange(this.regTypes.Keys.ToArray<string>());
			bool flag11 = this.cbxRegType.Items.Count > 0;
			if (flag11)
			{
				this.cbxRegType.SelectedIndex = selectedIndex;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		private void SetupCmdComboBox()
		{
			this.cbxCmdReg.Items.Clear();
			this.cmdSelectList.Clear();
			bool flag = false;
			try
			{
				foreach (string text in GlobalDeclarations.Cmd.Select((FormRegAccess._Closure$__.$I108-0 == null) ? (FormRegAccess._Closure$__.$I108-0 = ((CommandClass x) => x.RegLabel)) : FormRegAccess._Closure$__.$I108-0).Distinct<string>())
				{
					bool flag2 = !GlobalDeclarations.Reg.Contains(text);
					if (flag2)
					{
						flag = true;
						Interaction.MsgBox("Can not find register " + text + " in Reg File.", MsgBoxStyle.OkOnly, null);
					}
				}
			}
			finally
			{
				IEnumerator<string> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			bool flag3 = flag;
			if (!flag3)
			{
				this.cbxCmdReg.Items.AddRange(GlobalDeclarations.Cmd.Select((FormRegAccess._Closure$__.$I108-1 == null) ? (FormRegAccess._Closure$__.$I108-1 = ((CommandClass x) => GlobalDeclarations.Reg[x.RegLabel].EvalLabel)) : FormRegAccess._Closure$__.$I108-1).Distinct<string>().ToArray<string>());
				bool flag4 = this.cbxCmdReg.Items.Count > 0;
				if (flag4)
				{
					bool flag5 = GlobalDeclarations.Reg.Contains("COMMAND") && this.cbxCmdReg.Items.Contains(GlobalDeclarations.Reg["COMMAND"].EvalLabel);
					if (flag5)
					{
						this.cbxCmdReg.SelectedItem = GlobalDeclarations.Reg["COMMAND"].EvalLabel;
					}
					else
					{
						this.cbxCmdReg.SelectedIndex = 0;
					}
				}
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000D98C File Offset: 0x0000BB8C
		private void SetupCommandGrid()
		{
			this.dgvCommand.SuspendLayout();
			this.dgvCommand.AutoGenerateColumns = false;
			this.dgvCommand.ReadOnly = true;
			this.dgvCommand.DataSource = this.cmdSelectList;
			this.dgvCommand.Columns.Clear();
			this.dgvCommand.Columns.Add(new DataGridViewTextBoxColumn
			{
				Width = 40,
				Name = "Value",
				DataPropertyName = "ValueString"
			});
			this.dgvCommand.Columns.Add(new DataGridViewTextBoxColumn
			{
				Width = 40,
				Name = "Mask",
				DataPropertyName = "MaskString"
			});
			this.dgvCommand.Columns.Add(new DataGridViewTextBoxColumn
			{
				Width = 90,
				Name = "Function",
				DataPropertyName = "Label"
			});
			this.dgvCommand.Columns.Add(new DataGridViewButtonColumn
			{
				Width = 50,
				Name = "Write",
				Text = "Write",
				UseColumnTextForButtonValue = true
			});
			this.dgvCommand.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			this.dgvCommand.ResumeLayout();
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000DAEC File Offset: 0x0000BCEC
		private void dgvCommand_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = e.ColumnIndex == this.dgvCommand.Columns["Write"].Index;
			if (flag)
			{
				CommandClass commandClass = GlobalDeclarations.Cmd[this.dgvCommand.Rows[e.RowIndex].Cells["Function"].Value.ToString()];
				RegClass regClass = GlobalDeclarations.Reg[commandClass.RegLabel];
				bool flag2 = commandClass.Delay > 0;
				base.Enabled = false;
				bool flag3 = this.FilterPreWrite(regClass.EvalLabel, this.dgvCommand.Rows[e.RowIndex].Cells["Function"].Value.ToString());
				bool flag4 = !flag3;
				if (flag4)
				{
					bool flag5 = flag2;
					if (flag5)
					{
						MyProject.Forms.FormMessage.ShowMessage("Waiting for " + commandClass.Label + " command delay.", "Regsiter Access");
					}
					GlobalDeclarations.Dut.WriteCommand(regClass, commandClass);
					this.FilterPostWrite(regClass, commandClass.Value);
					this.writeLogFile(regClass.EvalLabel, this.dgvCommand.Rows[e.RowIndex].Cells["Function"].Value.ToString(), "Cmd Write");
					bool flag6 = flag2;
					if (flag6)
					{
						MyProject.Forms.FormMessage.Hide();
					}
					base.Enabled = true;
				}
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000DC80 File Offset: 0x0000BE80
		private void SetupRegisterGrid()
		{
			this.dgvRegList.SuspendLayout();
			this.dgvRegList.AutoGenerateColumns = false;
			this.dgvRegList.ReadOnly = true;
			this.dgvRegList.DataSource = this.regSelectList;
			this.dgvRegList.Columns.Clear();
			DataGridViewColumn dataGridViewColumn = new DataGridViewTextBoxColumn();
			dataGridViewColumn.DataPropertyName = "EvalLabel";
			dataGridViewColumn.Name = "Register";
			dataGridViewColumn.Width = 120;
			this.dgvRegList.Columns.Add(dataGridViewColumn);
			dataGridViewColumn = new DataGridViewTextBoxColumn();
			dataGridViewColumn.DataPropertyName = "PageString";
			dataGridViewColumn.Name = "Page";
			dataGridViewColumn.Width = 35;
			dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvRegList.Columns.Add(dataGridViewColumn);
			dataGridViewColumn = new DataGridViewTextBoxColumn();
			dataGridViewColumn.DataPropertyName = "AddressString";
			dataGridViewColumn.Name = "Addr";
			dataGridViewColumn.Width = 35;
			dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			this.dgvRegList.Columns.Add(dataGridViewColumn);
			dataGridViewColumn = new DataGridViewTextBoxColumn();
			dataGridViewColumn.DataPropertyName = "HexString";
			dataGridViewColumn.Name = "Contents";
			dataGridViewColumn.Width = 80;
			dataGridViewColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
			this.dgvRegList.Columns.Add(dataGridViewColumn);
			this.dgvRegList.ResumeLayout();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000DDFC File Offset: 0x0000BFFC
		private void FillCmdSelectList()
		{
			FormRegAccess._Closure$__112-0 CS$<>8__locals1 = new FormRegAccess._Closure$__112-0(CS$<>8__locals1);
			this.FormBusy = true;
			CS$<>8__locals1.$VB$Local_s = (from x in GlobalDeclarations.Reg
			where Operators.CompareString(x.EvalLabel, this.cbxCmdReg.SelectedItem.ToString(), false) == 0
			select x).First<RegClass>().Label;
			this.cmdSelectList.Clear();
			try
			{
				foreach (CommandClass cmd in GlobalDeclarations.Cmd.Where((CS$<>8__locals1.$I1 == null) ? (CS$<>8__locals1.$I1 = ((CommandClass x) => Operators.CompareString(x.RegLabel, CS$<>8__locals1.$VB$Local_s, false) == 0)) : CS$<>8__locals1.$I1))
				{
					this.cmdSelectList.Add(new FormRegAccess.CmdContents(cmd));
				}
			}
			finally
			{
				IEnumerator<CommandClass> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			this.FormBusy = false;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000DECC File Offset: 0x0000C0CC
		private void FillRegSelectList()
		{
			FormRegAccess._Closure$__113-0 CS$<>8__locals1 = new FormRegAccess._Closure$__113-0(CS$<>8__locals1);
			this.FormBusy = true;
			CS$<>8__locals1.$VB$Local_rType = this.regTypes[this.cbxRegType.SelectedItem.ToString()];
			this.regSelectList.Clear();
			try
			{
				foreach (RegClass reg in GlobalDeclarations.Reg.Where((CS$<>8__locals1.$I0 == null) ? (CS$<>8__locals1.$I0 = ((RegClass x) => x.Type == CS$<>8__locals1.$VB$Local_rType)) : CS$<>8__locals1.$I0))
				{
					this.regSelectList.Add(new FormRegAccess.RegContents(reg));
				}
			}
			finally
			{
				IEnumerator<RegClass> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			this.FormBusy = false;
		}

		/// <summary>
		/// Reads the registers currently contained in the data grid view and updates their values
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x060001B7 RID: 439 RVA: 0x0000DF98 File Offset: 0x0000C198
		private void ReadCategory()
		{
			List<RegClass> list = new List<RegClass>();
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			uint[] array = new uint[2];
			bool flag = this.regSelectList.Count == 0;
			checked
			{
				if (!flag)
				{
					int num = this.regSelectList.Count - 1;
					for (int i = 0; i <= num; i++)
					{
						FormRegAccess.RegContents regContents = this.regSelectList[i];
						bool isReadable = regContents.Reg.IsReadable;
						if (isReadable)
						{
							list.Add(regContents.Reg);
							dictionary.Add(regContents.Label, i);
						}
					}
					bool flag2 = GlobalDeclarations.BoardID > GlobalDeclarations.BoardIDtype.NONE;
					if (flag2)
					{
						List<uint> list2 = new List<uint>();
						try
						{
							foreach (IGrouping<uint, RegClass> grouping in list.GroupBy((FormRegAccess._Closure$__.$I114-0 == null) ? (FormRegAccess._Closure$__.$I114-0 = ((RegClass r) => r.Page)) : FormRegAccess._Closure$__.$I114-0))
							{
								try
								{
									array = new uint[2];
									list2.Clear();
									try
									{
										foreach (RegClass reg in grouping)
										{
											list2.Add(GlobalDeclarations.Dut.ReadUnsigned(reg));
										}
									}
									finally
									{
										IEnumerator<RegClass> enumerator2;
										if (enumerator2 != null)
										{
											enumerator2.Dispose();
										}
									}
								}
								catch (Exception ex)
								{
									GlobalDeclarations.BoardID = GlobalDeclarations.BoardIDtype.NONE;
									return;
								}
								int num2 = grouping.Count<RegClass>() - 1;
								for (int j = 0; j <= num2; j++)
								{
									this.regSelectList[dictionary[grouping.ElementAtOrDefault(j).Label]].Content = list2[j];
								}
							}
						}
						finally
						{
							IEnumerator<IGrouping<uint, RegClass>> enumerator;
							if (enumerator != null)
							{
								enumerator.Dispose();
							}
						}
					}
					this.dgvRegList.InvalidateColumn(this.dgvRegList.Columns["Contents"].Index);
					this.dgvRegList.InvalidateColumn(this.dgvRegList.Columns["Page"].Index);
					this.dgvRegList.Update();
					this.UpDateSelected();
				}
			}
		}

		/// <summary>
		/// Updates the name and contents of the currenly selected register from the data grid
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x060001B8 RID: 440 RVA: 0x0000E210 File Offset: 0x0000C410
		private void UpDateSelected()
		{
			bool flag = this.dgvRegList.CurrentRow != null;
			if (flag)
			{
				int index = this.dgvRegList.CurrentRow.Index;
				this.lblSelected.Text = this.regSelectList[index].EvalLabel;
				this.lblCurrentValue.Text = this.regSelectList[index].HexString;
				bool flag2 = GlobalDeclarations.BoardID != GlobalDeclarations.BoardIDtype.NONE && this.regSelectList[index].Reg.IsWriteable;
				if (flag2)
				{
					this.ButtonWriteReg.Enabled = true;
					this.Label3.Enabled = true;
					this.txtHex.Enabled = true;
				}
				else
				{
					this.ButtonWriteReg.Enabled = false;
					this.Label3.Enabled = false;
					this.txtHex.Enabled = false;
				}
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000E2F8 File Offset: 0x0000C4F8
		private void dgvRegList_CellEnter(object sender, DataGridViewCellEventArgs e)
		{
			bool flag = !this.FormBusy;
			if (flag)
			{
				this.UpDateSelected();
			}
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000E31A File Offset: 0x0000C51A
		private void ButtonReadAll_Click(object sender, EventArgs e)
		{
			this.ReadCategory();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000E324 File Offset: 0x0000C524
		private void ButtonReadOne_Click(object sender, EventArgs e)
		{
			int index = this.dgvRegList.CurrentRow.Index;
			string label = this.regSelectList[index].Reg.Label;
			bool isReadable = GlobalDeclarations.Reg[label].IsReadable;
			if (isReadable)
			{
				uint content = GlobalDeclarations.Dut.ReadUnsigned(GlobalDeclarations.Reg[label]);
				this.regSelectList[index].Content = content;
				this.dgvRegList.InvalidateColumn(this.dgvRegList.Columns["Contents"].Index);
				this.dgvRegList.Update();
				this.lblCurrentValue.Text = this.regSelectList[index].HexString;
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000E3F0 File Offset: 0x0000C5F0
		private void ButtonWrite_Click(object sender, EventArgs e)
		{
			string rname = this.lblSelected.Text.Trim().ToUpper();
			RegClass regClass = (from x in GlobalDeclarations.Reg
			where Operators.CompareString(x.EvalLabel, rname, false) == 0
			select x).First<RegClass>();
			uint num;
			bool flag = uint.TryParse(this.txtHex.Text, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num);
			if (flag)
			{
				try
				{
					GlobalDeclarations.Dut.WriteUnsigned(regClass, num);
					this.FilterPostWrite(regClass, num);
					this.writeLogFile(regClass, num, "Manual Write");
				}
				catch (Exception ex)
				{
					Interaction.MsgBox(ex.Message, MsgBoxStyle.Information, null);
				}
			}
			else
			{
				Interaction.MsgBox("Error attempting to write register.\r\n\r\nPlease enter a valid Hexidecimal Value.", MsgBoxStyle.OkOnly, null);
			}
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		private void FilterPostWrite(RegClass Reg, uint val)
		{
			string label = Reg.Label;
			if (Operators.CompareString(label, "REC_CTRL", false) == 0)
			{
				this.RecModeShowInMain(true);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000E504 File Offset: 0x0000C704
		private bool FilterPreWrite(string RegLabel, string func)
		{
			bool result = false;
			if (Operators.CompareString(RegLabel, "GLOB_CMD", false) == 0)
			{
				bool flag = Operators.CompareString(func, "Record start/stop", false) == 0;
				if (flag)
				{
					this.CmdStartStopinMain(true);
					result = true;
				}
			}
			return result;
		}

		/// <summary>
		/// Writes log file rows.
		/// </summary>
		/// <param name="rg"></param>
		/// <param name="val"></param>
		/// <param name="src"></param>
		// Token: 0x060001BF RID: 447 RVA: 0x0000E55F File Offset: 0x0000C75F
		private void writeLogFile(string rg, string val, string src)
		{
			this.writeLogFileinMain(rg, val, src);
		}

		/// <summary>
		/// Overload converts RegMapClass and Uint32 types to strings. Calls lowest overload.
		/// </summary>
		/// <param name="rg"></param>
		/// <param name="val"></param>
		// Token: 0x060001C0 RID: 448 RVA: 0x0000E571 File Offset: 0x0000C771
		private void writeLogFile(RegClass rg, uint val, string src)
		{
			this.writeLogFile(rg.EvalLabel, "0x" + val.ToString(rg.HexFormat), src);
		}

		/// <summary>
		/// Overload parses list to individual elements. Calls writeLogFile(reg, value)
		/// </summary>
		/// <param name="rgL"></param>
		/// <param name="valL"></param>
		// Token: 0x060001C1 RID: 449 RVA: 0x0000E59C File Offset: 0x0000C79C
		private void writeLogFile(List<RegClass> rgL, List<uint> valL, string src)
		{
			checked
			{
				int num = rgL.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					this.writeLogFile(rgL[i], valL[i], src);
				}
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
		private void ButtonWriteFile_Click(object sender, EventArgs e)
		{
			FormRegAccess._Closure$__125-0 CS$<>8__locals1 = new FormRegAccess._Closure$__125-0(CS$<>8__locals1);
			DialogCheckListBox dialogCheckListBox = new DialogCheckListBox();
			dialogCheckListBox.Text = "Write Register File";
			dialogCheckListBox.LabelPrompt.Text = "Select Register Type(s) to Save:";
			dialogCheckListBox.AddRange(this.regTypes.Keys);
			dialogCheckListBox.ShowDialog();
			bool flag = dialogCheckListBox.DialogResult == DialogResult.Cancel | dialogCheckListBox.CheckedItemList.Count == 0;
			checked
			{
				if (!flag)
				{
					CS$<>8__locals1.$VB$Local_typeList = from x in dialogCheckListBox.CheckedItemList
					select this.regTypes[x];
					List<RegClass> source = (from x in GlobalDeclarations.Reg
					where x.IsReadable && CS$<>8__locals1.$VB$Local_typeList.Contains(x.Type)
					select x).ToList<RegClass>();
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.Filter = this.fileFilter;
					saveFileDialog.DefaultExt = this.fileExt;
					saveFileDialog.FileName = "RegDump";
					bool flag2 = saveFileDialog.ShowDialog(this) == DialogResult.OK;
					if (flag2)
					{
						StreamWriter streamWriter = this.OpenFileForWrite(saveFileDialog.FileName);
						bool flag3 = streamWriter != null;
						if (flag3)
						{
							uint[] array = null;
							try
							{
								foreach (IGrouping<uint, RegClass> source2 in source.GroupBy((FormRegAccess._Closure$__.$I125-2 == null) ? (FormRegAccess._Closure$__.$I125-2 = ((RegClass r) => r.Page)) : FormRegAccess._Closure$__.$I125-2))
								{
									bool flag4 = GlobalDeclarations.BoardID > GlobalDeclarations.BoardIDtype.NONE;
									if (flag4)
									{
										try
										{
											array = GlobalDeclarations.Dut.ReadUnsigned(source2.ToList<RegClass>(), 1U);
										}
										catch (Exception ex)
										{
											streamWriter.Close();
											return;
										}
										int num = source2.Count<RegClass>() - 1;
										for (int i = 0; i <= num; i++)
										{
											streamWriter.WriteLine(source2.ElementAtOrDefault(i).Label + this.delim + "0x" + array[i].ToString(source2.ElementAtOrDefault(i).HexFormat));
										}
									}
								}
							}
							finally
							{
								IEnumerator<IGrouping<uint, RegClass>> enumerator;
								if (enumerator != null)
								{
									enumerator.Dispose();
								}
							}
						}
						streamWriter.Close();
					}
				}
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000E80C File Offset: 0x0000CA0C
		private StreamWriter OpenFileForWrite(string filename)
		{
			StreamWriter result = null;
			bool flag3;
			do
			{
				bool flag;
				try
				{
					flag = true;
					result = new StreamWriter(filename);
				}
				catch (Exception ex)
				{
					flag = false;
					bool flag2 = MessageBox.Show("Can not Open register file" + filename + ".\r\n\r\n" + Conversions.ToString(5)) != DialogResult.Retry;
					if (flag2)
					{
						Interaction.MsgBox("Registers can not be saved.", MsgBoxStyle.OkOnly, null);
						return null;
					}
				}
				flag3 = flag;
			}
			while (!flag3);
			return result;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000E898 File Offset: 0x0000CA98
		private void ButtonReadFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = this.fileFilter;
			openFileDialog.DefaultExt = this.fileExt;
			openFileDialog.FileName = "RegDump";
			bool flag = openFileDialog.ShowDialog(this) == DialogResult.OK;
			if (flag)
			{
				StreamReader streamReader = this.OpenFileForRead(openFileDialog.FileName);
				bool flag2 = streamReader != null;
				if (flag2)
				{
					string str = "Error attempting to load registers from file.\r\n";
					List<RegClass> list = new List<RegClass>();
					List<uint> list2 = new List<uint>();
					string[] array = new string[2];
					for (string text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine())
					{
						array = text.Split(new char[]
						{
							','
						});
						string key = array[0].Trim().ToUpper();
						string text2 = array[1].Trim().ToUpper();
						bool flag3 = !GlobalDeclarations.Reg.Contains(key);
						if (flag3)
						{
							Interaction.MsgBox(str + "Unrecognized register label " + text2 + ".", MsgBoxStyle.OkOnly, null);
						}
						else
						{
							bool flag4 = text2.StartsWith("0X") | text2.StartsWith("&H");
							if (flag4)
							{
								text2 = text2.Substring(2);
							}
							uint item;
							bool flag5 = !uint.TryParse(text2, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out item);
							if (flag5)
							{
								Interaction.MsgBox(str + "Invalid hexidecimal value " + text2 + ".", MsgBoxStyle.OkOnly, null);
							}
							else
							{
								bool isWriteable = GlobalDeclarations.Reg[key].IsWriteable;
								if (isWriteable)
								{
									list.Add(GlobalDeclarations.Reg[key]);
									list2.Add(item);
								}
							}
						}
					}
					bool flag6 = list.Count == 0;
					if (flag6)
					{
						Interaction.MsgBox("No registers were written.", MsgBoxStyle.OkOnly, null);
					}
					else
					{
						try
						{
							GlobalDeclarations.Dut.WriteUnsigned(list, list2);
							this.writeLogFile(list, list2, "Load from file");
						}
						catch (Exception ex)
						{
							Interaction.MsgBox("FormRegAccess sub ButtonReadFile_Click\r\n" + ex.Message, MsgBoxStyle.OkOnly, null);
							streamReader.Close();
							return;
						}
						this.ReadCategory();
					}
					streamReader.Close();
				}
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000EADC File Offset: 0x0000CCDC
		private StreamReader OpenFileForRead(string filename)
		{
			StreamReader result = null;
			bool flag3;
			do
			{
				bool flag;
				try
				{
					flag = true;
					result = new StreamReader(filename);
				}
				catch (Exception ex)
				{
					flag = false;
					bool flag2 = MessageBox.Show("Cannot Open register setting file" + filename + ".", "Reg File Open Error", MessageBoxButtons.RetryCancel) != DialogResult.Retry;
					if (flag2)
					{
						Interaction.MsgBox("Registers could not be loaded.", MsgBoxStyle.OkOnly, null);
						return null;
					}
				}
				flag3 = flag;
			}
			while (!flag3);
			return result;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000EB68 File Offset: 0x0000CD68
		private void cbxRegType_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.FormBusy = true;
			this.FillRegSelectList();
			this.FormBusy = false;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000EB80 File Offset: 0x0000CD80
		private void cbxCmdReg_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.FillCmdSelectList();
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000EB8A File Offset: 0x0000CD8A
		private void FormRegAccess_Shown(object sender, EventArgs e)
		{
			base.BringToFront();
		}

		// Token: 0x040000B8 RID: 184
		private bool FormBusy;

		// Token: 0x040000B9 RID: 185
		private string fileFilter;

		// Token: 0x040000BA RID: 186
		private string fileExt;

		// Token: 0x040000BB RID: 187
		private string delim;

		// Token: 0x040000BC RID: 188
		private BindingList<FormRegAccess.RegContents> regSelectList;

		// Token: 0x040000BD RID: 189
		private BindingList<FormRegAccess.CmdContents> cmdSelectList;

		// Token: 0x040000BE RID: 190
		private Dictionary<string, RegClass.RegType> regTypes;

		// Token: 0x040000BF RID: 191
		public Action<bool> RecModeShowInMain;

		// Token: 0x040000C0 RID: 192
		public Action<bool> CmdStartStopinMain;

		// Token: 0x040000C1 RID: 193
		public Action<string, string, string> writeLogFileinMain;

		/// <summary>
		/// An object to facilitate binding a register and its contents to the dataGridView.
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x02000031 RID: 49
		[DebuggerDisplay("{Label} {HexString}")]
		private class RegContents
		{
			// Token: 0x06000441 RID: 1089 RVA: 0x0001C049 File Offset: 0x0001A249
			public RegContents(RegClass Reg)
			{
				this.Reg = Reg;
				this.Content = 0U;
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001C064 File Offset: 0x0001A264
			// (set) Token: 0x06000443 RID: 1091 RVA: 0x0001C07C File Offset: 0x0001A27C
			public RegClass Reg
			{
				get
				{
					return this._Reg;
				}
				private set
				{
					this._Reg = value;
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x06000444 RID: 1092 RVA: 0x0001C088 File Offset: 0x0001A288
			public string AddressString
			{
				get
				{
					return this._Reg.Address.ToString("X2");
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x06000445 RID: 1093 RVA: 0x0001C0B4 File Offset: 0x0001A2B4
			public string PageString
			{
				get
				{
					return this._Reg.Page.ToString();
				}
			}

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x06000446 RID: 1094 RVA: 0x0001C0DC File Offset: 0x0001A2DC
			public string Label
			{
				get
				{
					return this._Reg.Label;
				}
			}

			// Token: 0x1700016C RID: 364
			// (get) Token: 0x06000447 RID: 1095 RVA: 0x0001C0FC File Offset: 0x0001A2FC
			public string EvalLabel
			{
				get
				{
					return this._Reg.EvalLabel;
				}
			}

			// Token: 0x1700016D RID: 365
			// (get) Token: 0x06000448 RID: 1096 RVA: 0x0001C11C File Offset: 0x0001A31C
			// (set) Token: 0x06000449 RID: 1097 RVA: 0x0001C134 File Offset: 0x0001A334
			public uint Content
			{
				get
				{
					return this._Content;
				}
				set
				{
					this._Content = value;
				}
			}

			// Token: 0x1700016E RID: 366
			// (get) Token: 0x0600044A RID: 1098 RVA: 0x0001C140 File Offset: 0x0001A340
			private string ValString
			{
				get
				{
					return GlobalDeclarations.Dut.ScaleRegData(this.Reg, this.Content).ToString("F2");
				}
			}

			// Token: 0x1700016F RID: 367
			// (get) Token: 0x0600044B RID: 1099 RVA: 0x0001C178 File Offset: 0x0001A378
			public string HexString
			{
				get
				{
					bool isReadable = this.Reg.IsReadable;
					string result;
					if (isReadable)
					{
						result = this.Content.ToString(this.Reg.HexFormat);
					}
					else
					{
						result = new string('X', checked((int)((int)this.Reg.NumBytes << 1)));
					}
					return result;
				}
			}

			// Token: 0x040001EF RID: 495
			private RegClass _Reg;

			// Token: 0x040001F0 RID: 496
			private uint _Content;
		}

		/// <summary>
		/// An object to facilitate binding a register and its contents to the dataGridView.
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x02000032 RID: 50
		[DebuggerDisplay("{Label}")]
		private class CmdContents
		{
			// Token: 0x0600044C RID: 1100 RVA: 0x0001C1CB File Offset: 0x0001A3CB
			public CmdContents(CommandClass Cmd)
			{
				this.Cmd = Cmd;
				this.Reg = GlobalDeclarations.Reg[this.Cmd.RegLabel];
			}

			// Token: 0x17000170 RID: 368
			// (get) Token: 0x0600044D RID: 1101 RVA: 0x0001C1F8 File Offset: 0x0001A3F8
			// (set) Token: 0x0600044E RID: 1102 RVA: 0x0001C210 File Offset: 0x0001A410
			public CommandClass Cmd
			{
				get
				{
					return this._Cmd;
				}
				private set
				{
					this._Cmd = value;
				}
			}

			// Token: 0x17000171 RID: 369
			// (get) Token: 0x0600044F RID: 1103 RVA: 0x0001C21C File Offset: 0x0001A41C
			public string Label
			{
				get
				{
					return this.Cmd.Label;
				}
			}

			// Token: 0x17000172 RID: 370
			// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001C23C File Offset: 0x0001A43C
			public string RegLabel
			{
				get
				{
					return this.Cmd.RegLabel;
				}
			}

			// Token: 0x17000173 RID: 371
			// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001C25C File Offset: 0x0001A45C
			public string ValueString
			{
				get
				{
					return this.Cmd.Value.ToString(this.Reg.HexFormat);
				}
			}

			// Token: 0x17000174 RID: 372
			// (get) Token: 0x06000452 RID: 1106 RVA: 0x0001C28C File Offset: 0x0001A48C
			public string MaskString
			{
				get
				{
					return this.Cmd.Mask.ToString(this.Reg.HexFormat);
				}
			}

			// Token: 0x040001F1 RID: 497
			private RegClass Reg;

			// Token: 0x040001F2 RID: 498
			private CommandClass _Cmd;
		}
	}
}
