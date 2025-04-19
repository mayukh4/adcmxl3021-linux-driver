namespace Vibration_Evaluation
{
	// Token: 0x02000016 RID: 22
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormMain : global::System.Windows.Forms.Form
	{
		// Token: 0x0600020C RID: 524 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		[global::System.Diagnostics.DebuggerNonUserCode]
		protected override void Dispose(bool disposing)
		{
			try
			{
				bool flag = disposing && this.components != null;
				if (flag)
				{
					this.components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00011B28 File Offset: 0x0000FD28
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::Vibration_Evaluation.AlarmsClass.AxisClass alarmValues = new global::Vibration_Evaluation.AlarmsClass.AxisClass();
			global::Vibration_Evaluation.AlarmsClass.AxisClass alarmValues2 = new global::Vibration_Evaluation.AlarmsClass.AxisClass();
			global::Vibration_Evaluation.AlarmsClass.AxisClass alarmValues3 = new global::Vibration_Evaluation.AlarmsClass.AxisClass();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Vibration_Evaluation.FormMain));
			this.ButtonStartStop = new global::System.Windows.Forms.Button();
			this.MenuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.DeviceSelectToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.RegistersToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.AlarmsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.AlarmValuesToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.AlarmStatusFormToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.DataLogToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ViewToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.AllAxisToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.XAxisToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.YAxisToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ZAxisToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.ToolsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.USBToolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.SPIToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.HelpToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.AboutToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.StatusStrip1 = new global::System.Windows.Forms.StatusStrip();
			this.Label3 = new global::System.Windows.Forms.Label();
			this.cbxMode = new global::System.Windows.Forms.ComboBox();
			this.cbxAxis = new global::System.Windows.Forms.ComboBox();
			this.lblAxisSelect = new global::System.Windows.Forms.Label();
			this.ckDatalogEnabled = new global::System.Windows.Forms.CheckBox();
			this.lblFileCount = new global::System.Windows.Forms.Label();
			this.TableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.TimerFormLoaded = new global::System.Windows.Forms.Timer(this.components);
			this.sampleTimer = new global::System.Windows.Forms.Timer(this.components);
			this.CheckBoxExtTrigger = new global::System.Windows.Forms.CheckBox();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.LabelSystemMsg = new global::System.Windows.Forms.Label();
			this.CheckBoxNoScale = new global::System.Windows.Forms.CheckBox();
			this.Panel1 = new global::System.Windows.Forms.Panel();
			this.LabelTimer = new global::System.Windows.Forms.Label();
			this.TextBoxTimer = new global::System.Windows.Forms.TextBox();
			this.TimerReStart = new global::System.Windows.Forms.Timer(this.components);
			this.LabelFileLimit = new global::System.Windows.Forms.Label();
			this.TextBoxFileLimit = new global::System.Windows.Forms.TextBox();
			this.LabelDevice = new global::System.Windows.Forms.Label();
			this.LabelOrange = new global::System.Windows.Forms.Label();
			this.LabelCapTime = new global::System.Windows.Forms.Label();
			this.CheckBoxReStartTimer = new global::System.Windows.Forms.CheckBox();
			this.PlotUC3 = new global::Vibration_Evaluation.PlotUC();
			this.PlotUC1 = new global::Vibration_Evaluation.PlotUC();
			this.PlotUC2 = new global::Vibration_Evaluation.PlotUC();
			this.MenuStrip1.SuspendLayout();
			this.TableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.ButtonStartStop.Enabled = false;
			this.ButtonStartStop.Location = new global::System.Drawing.Point(210, 61);
			this.ButtonStartStop.Name = "ButtonStartStop";
			this.ButtonStartStop.Size = new global::System.Drawing.Size(51, 22);
			this.ButtonStartStop.TabIndex = 0;
			this.ButtonStartStop.Text = "Read";
			this.ButtonStartStop.UseVisualStyleBackColor = true;
			this.MenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.DeviceSelectToolStripMenuItem,
				this.RegistersToolStripMenuItem,
				this.AlarmsToolStripMenuItem,
				this.DataLogToolStripMenuItem,
				this.ViewToolStripMenuItem,
				this.ToolsToolStripMenuItem,
				this.HelpToolStripMenuItem,
				this.AboutToolStripMenuItem
			});
			this.MenuStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.MenuStrip1.Name = "MenuStrip1";
			this.MenuStrip1.Padding = new global::System.Windows.Forms.Padding(4, 2, 0, 2);
			this.MenuStrip1.Size = new global::System.Drawing.Size(785, 24);
			this.MenuStrip1.TabIndex = 2;
			this.MenuStrip1.Text = "MenuStrip1";
			this.DeviceSelectToolStripMenuItem.Name = "DeviceSelectToolStripMenuItem";
			this.DeviceSelectToolStripMenuItem.Size = new global::System.Drawing.Size(59, 20);
			this.DeviceSelectToolStripMenuItem.Text = "Devices";
			this.RegistersToolStripMenuItem.Name = "RegistersToolStripMenuItem";
			this.RegistersToolStripMenuItem.Size = new global::System.Drawing.Size(100, 20);
			this.RegistersToolStripMenuItem.Text = "Register Access";
			this.AlarmsToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.AlarmValuesToolStripMenuItem,
				this.AlarmStatusFormToolStripMenuItem
			});
			this.AlarmsToolStripMenuItem.Name = "AlarmsToolStripMenuItem";
			this.AlarmsToolStripMenuItem.Size = new global::System.Drawing.Size(56, 20);
			this.AlarmsToolStripMenuItem.Text = "Alarms";
			this.AlarmValuesToolStripMenuItem.Name = "AlarmValuesToolStripMenuItem";
			this.AlarmValuesToolStripMenuItem.Size = new global::System.Drawing.Size(172, 22);
			this.AlarmValuesToolStripMenuItem.Text = "Alarm Values";
			this.AlarmStatusFormToolStripMenuItem.Name = "AlarmStatusFormToolStripMenuItem";
			this.AlarmStatusFormToolStripMenuItem.Size = new global::System.Drawing.Size(172, 22);
			this.AlarmStatusFormToolStripMenuItem.Text = "Alarm Status Form";
			this.DataLogToolStripMenuItem.Name = "DataLogToolStripMenuItem";
			this.DataLogToolStripMenuItem.Size = new global::System.Drawing.Size(88, 20);
			this.DataLogToolStripMenuItem.Text = "Data Capture";
			this.ViewToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.AllAxisToolStripMenuItem,
				this.XAxisToolStripMenuItem,
				this.YAxisToolStripMenuItem,
				this.ZAxisToolStripMenuItem
			});
			this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
			this.ViewToolStripMenuItem.Size = new global::System.Drawing.Size(44, 20);
			this.ViewToolStripMenuItem.Text = "View";
			this.AllAxisToolStripMenuItem.Name = "AllAxisToolStripMenuItem";
			this.AllAxisToolStripMenuItem.Size = new global::System.Drawing.Size(110, 22);
			this.AllAxisToolStripMenuItem.Text = "All axis";
			this.XAxisToolStripMenuItem.Name = "XAxisToolStripMenuItem";
			this.XAxisToolStripMenuItem.Size = new global::System.Drawing.Size(110, 22);
			this.XAxisToolStripMenuItem.Text = "X axis";
			this.YAxisToolStripMenuItem.Name = "YAxisToolStripMenuItem";
			this.YAxisToolStripMenuItem.Size = new global::System.Drawing.Size(110, 22);
			this.YAxisToolStripMenuItem.Text = "Y axis";
			this.ZAxisToolStripMenuItem.Name = "ZAxisToolStripMenuItem";
			this.ZAxisToolStripMenuItem.Size = new global::System.Drawing.Size(110, 22);
			this.ZAxisToolStripMenuItem.Text = "Z axis";
			this.ToolsToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.USBToolStripMenuItem1,
				this.SPIToolStripMenuItem
			});
			this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
			this.ToolsToolStripMenuItem.Size = new global::System.Drawing.Size(56, 20);
			this.ToolsToolStripMenuItem.Text = "Comm";
			this.USBToolStripMenuItem1.Name = "USBToolStripMenuItem1";
			this.USBToolStripMenuItem1.Size = new global::System.Drawing.Size(104, 22);
			this.USBToolStripMenuItem1.Text = "USB...";
			this.SPIToolStripMenuItem.Name = "SPIToolStripMenuItem";
			this.SPIToolStripMenuItem.Size = new global::System.Drawing.Size(104, 22);
			this.SPIToolStripMenuItem.Text = "SPI";
			this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
			this.HelpToolStripMenuItem.Size = new global::System.Drawing.Size(44, 20);
			this.HelpToolStripMenuItem.Text = "Help";
			this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
			this.AboutToolStripMenuItem.Size = new global::System.Drawing.Size(52, 20);
			this.AboutToolStripMenuItem.Text = "About";
			this.StatusStrip1.Location = new global::System.Drawing.Point(0, 675);
			this.StatusStrip1.Name = "StatusStrip1";
			this.StatusStrip1.Size = new global::System.Drawing.Size(785, 22);
			this.StatusStrip1.TabIndex = 24;
			this.StatusStrip1.Text = "StatusStrip1";
			this.Label3.AutoSize = true;
			this.Label3.Location = new global::System.Drawing.Point(63, 45);
			this.Label3.Name = "Label3";
			this.Label3.Size = new global::System.Drawing.Size(81, 13);
			this.Label3.TabIndex = 31;
			this.Label3.Text = "Mode Selection";
			this.cbxMode.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxMode.FormattingEnabled = true;
			this.cbxMode.Location = new global::System.Drawing.Point(48, 61);
			this.cbxMode.Name = "cbxMode";
			this.cbxMode.Size = new global::System.Drawing.Size(128, 21);
			this.cbxMode.TabIndex = 32;
			this.cbxAxis.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxAxis.Enabled = false;
			this.cbxAxis.FormattingEnabled = true;
			this.cbxAxis.Location = new global::System.Drawing.Point(294, 62);
			this.cbxAxis.Name = "cbxAxis";
			this.cbxAxis.Size = new global::System.Drawing.Size(79, 21);
			this.cbxAxis.TabIndex = 33;
			this.cbxAxis.Visible = false;
			this.lblAxisSelect.AutoSize = true;
			this.lblAxisSelect.Enabled = false;
			this.lblAxisSelect.Location = new global::System.Drawing.Point(297, 47);
			this.lblAxisSelect.Name = "lblAxisSelect";
			this.lblAxisSelect.Size = new global::System.Drawing.Size(73, 13);
			this.lblAxisSelect.TabIndex = 34;
			this.lblAxisSelect.Text = "Axis Selection";
			this.lblAxisSelect.Visible = false;
			this.ckDatalogEnabled.AutoSize = true;
			this.ckDatalogEnabled.Location = new global::System.Drawing.Point(435, 56);
			this.ckDatalogEnabled.Name = "ckDatalogEnabled";
			this.ckDatalogEnabled.Size = new global::System.Drawing.Size(106, 17);
			this.ckDatalogEnabled.TabIndex = 36;
			this.ckDatalogEnabled.Text = "Enable Data Log";
			this.ckDatalogEnabled.UseVisualStyleBackColor = true;
			this.lblFileCount.AutoSize = true;
			this.lblFileCount.Location = new global::System.Drawing.Point(623, 57);
			this.lblFileCount.Name = "lblFileCount";
			this.lblFileCount.Size = new global::System.Drawing.Size(13, 13);
			this.lblFileCount.TabIndex = 37;
			this.lblFileCount.Text = "0";
			this.TableLayoutPanel1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.TableLayoutPanel1.BackColor = global::System.Drawing.SystemColors.Control;
			this.TableLayoutPanel1.ColumnCount = 1;
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.TableLayoutPanel1.Controls.Add(this.PlotUC3, 0, 2);
			this.TableLayoutPanel1.Controls.Add(this.PlotUC1, 0, 0);
			this.TableLayoutPanel1.Controls.Add(this.PlotUC2, 0, 1);
			this.TableLayoutPanel1.Location = new global::System.Drawing.Point(13, 93);
			this.TableLayoutPanel1.Name = "TableLayoutPanel1";
			this.TableLayoutPanel1.RowCount = 3;
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.TableLayoutPanel1.Size = new global::System.Drawing.Size(760, 576);
			this.TableLayoutPanel1.TabIndex = 40;
			this.CheckBoxExtTrigger.AutoSize = true;
			this.CheckBoxExtTrigger.Location = new global::System.Drawing.Point(546, 32);
			this.CheckBoxExtTrigger.Name = "CheckBoxExtTrigger";
			this.CheckBoxExtTrigger.Size = new global::System.Drawing.Size(113, 17);
			this.CheckBoxExtTrigger.TabIndex = 45;
			this.CheckBoxExtTrigger.Text = "Enable Ext Trigger";
			this.CheckBoxExtTrigger.UseVisualStyleBackColor = true;
			this.CheckBoxExtTrigger.Visible = false;
			this.Label1.AutoSize = true;
			this.Label1.Location = new global::System.Drawing.Point(563, 57);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(54, 13);
			this.Label1.TabIndex = 46;
			this.Label1.Text = "File Count";
			this.LabelSystemMsg.AutoSize = true;
			this.LabelSystemMsg.Location = new global::System.Drawing.Point(153, 28);
			this.LabelSystemMsg.Name = "LabelSystemMsg";
			this.LabelSystemMsg.Size = new global::System.Drawing.Size(87, 13);
			this.LabelSystemMsg.TabIndex = 47;
			this.LabelSystemMsg.Text = "LabelSystemMsg";
			this.LabelSystemMsg.Visible = false;
			this.CheckBoxNoScale.AutoSize = true;
			this.CheckBoxNoScale.Location = new global::System.Drawing.Point(435, 30);
			this.CheckBoxNoScale.Name = "CheckBoxNoScale";
			this.CheckBoxNoScale.Size = new global::System.Drawing.Size(74, 17);
			this.CheckBoxNoScale.TabIndex = 49;
			this.CheckBoxNoScale.Text = "no scaling";
			this.CheckBoxNoScale.UseVisualStyleBackColor = true;
			this.CheckBoxNoScale.Visible = false;
			this.Panel1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel1.Location = new global::System.Drawing.Point(122, 131);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new global::System.Drawing.Size(118, 98);
			this.Panel1.TabIndex = 50;
			this.Panel1.Visible = false;
			this.LabelTimer.AutoSize = true;
			this.LabelTimer.Location = new global::System.Drawing.Point(712, 47);
			this.LabelTimer.Name = "LabelTimer";
			this.LabelTimer.Size = new global::System.Drawing.Size(32, 13);
			this.LabelTimer.TabIndex = 52;
			this.LabelTimer.Text = "msec";
			this.LabelTimer.Visible = false;
			this.TextBoxTimer.Location = new global::System.Drawing.Point(693, 63);
			this.TextBoxTimer.Name = "TextBoxTimer";
			this.TextBoxTimer.Size = new global::System.Drawing.Size(68, 20);
			this.TextBoxTimer.TabIndex = 53;
			this.TextBoxTimer.Text = "1000";
			this.TextBoxTimer.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Right;
			this.TextBoxTimer.Visible = false;
			this.LabelFileLimit.AutoSize = true;
			this.LabelFileLimit.Location = new global::System.Drawing.Point(563, 75);
			this.LabelFileLimit.Name = "LabelFileLimit";
			this.LabelFileLimit.Size = new global::System.Drawing.Size(47, 13);
			this.LabelFileLimit.TabIndex = 54;
			this.LabelFileLimit.Text = "File Limit";
			this.LabelFileLimit.Visible = false;
			this.TextBoxFileLimit.Location = new global::System.Drawing.Point(617, 72);
			this.TextBoxFileLimit.Name = "TextBoxFileLimit";
			this.TextBoxFileLimit.Size = new global::System.Drawing.Size(46, 20);
			this.TextBoxFileLimit.TabIndex = 55;
			this.TextBoxFileLimit.Text = "100";
			this.TextBoxFileLimit.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Right;
			this.TextBoxFileLimit.Visible = false;
			this.LabelDevice.AutoSize = true;
			this.LabelDevice.Location = new global::System.Drawing.Point(10, 25);
			this.LabelDevice.Name = "LabelDevice";
			this.LabelDevice.Size = new global::System.Drawing.Size(56, 13);
			this.LabelDevice.TabIndex = 56;
			this.LabelDevice.Text = "No device";
			this.LabelOrange.AutoSize = true;
			this.LabelOrange.BackColor = global::System.Drawing.Color.Orange;
			this.LabelOrange.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.LabelOrange.Location = new global::System.Drawing.Point(518, 4);
			this.LabelOrange.Name = "LabelOrange";
			this.LabelOrange.Size = new global::System.Drawing.Size(114, 15);
			this.LabelOrange.TabIndex = 57;
			this.LabelOrange.Text = " ADI CONFIDENTIAL ";
			this.LabelOrange.Visible = false;
			this.LabelCapTime.AutoSize = true;
			this.LabelCapTime.Location = new global::System.Drawing.Point(276, 29);
			this.LabelCapTime.Name = "LabelCapTime";
			this.LabelCapTime.Size = new global::System.Drawing.Size(75, 13);
			this.LabelCapTime.TabIndex = 58;
			this.LabelCapTime.Text = "LabelCapTime";
			this.CheckBoxReStartTimer.AutoSize = true;
			this.CheckBoxReStartTimer.BackColor = global::System.Drawing.SystemColors.Control;
			this.CheckBoxReStartTimer.Location = new global::System.Drawing.Point(685, 29);
			this.CheckBoxReStartTimer.Name = "CheckBoxReStartTimer";
			this.CheckBoxReStartTimer.Size = new global::System.Drawing.Size(88, 17);
			this.CheckBoxReStartTimer.TabIndex = 51;
			this.CheckBoxReStartTimer.Text = "Enable Timer";
			this.CheckBoxReStartTimer.UseVisualStyleBackColor = false;
			this.CheckBoxReStartTimer.Visible = false;
			this.PlotUC3.AlarmsVisible = false;
			this.PlotUC3.AlarmValues = alarmValues;
			this.PlotUC3.AutoSize = true;
			this.PlotUC3.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.PlotUC3.barWidth = 2;
			this.PlotUC3.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.PlotUC3.chartType = global::Vibration_Evaluation.PlotUC.ChartTypeEnum.bar;
			this.PlotUC3.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.PlotUC3.Location = new global::System.Drawing.Point(3, 385);
			this.PlotUC3.Name = "PlotUC3";
			this.PlotUC3.ScaleLog10 = false;
			this.PlotUC3.ScaleType = global::Vibration_Evaluation.PlotUC.ScaleTypeEnum.Signed;
			this.PlotUC3.ScaleUnitsText = null;
			this.PlotUC3.Size = new global::System.Drawing.Size(754, 188);
			this.PlotUC3.TabIndex = 41;
			this.PlotUC3.Title = null;
			this.PlotUC3.Xmax = 700f;
			this.PlotUC3.Xmin = 0f;
			this.PlotUC3.Xoffset = 0f;
			this.PlotUC3.xScaleMax = 1.0;
			this.PlotUC3.Ymax = 60f;
			this.PlotUC3.Ymin = -60f;
			this.PlotUC3.Yoffset = 60f;
			this.PlotUC1.AlarmsVisible = false;
			this.PlotUC1.AlarmValues = alarmValues2;
			this.PlotUC1.AutoSize = true;
			this.PlotUC1.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.PlotUC1.barWidth = 2;
			this.PlotUC1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.PlotUC1.chartType = global::Vibration_Evaluation.PlotUC.ChartTypeEnum.bar;
			this.PlotUC1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.PlotUC1.Location = new global::System.Drawing.Point(3, 3);
			this.PlotUC1.Name = "PlotUC1";
			this.PlotUC1.ScaleLog10 = false;
			this.PlotUC1.ScaleType = global::Vibration_Evaluation.PlotUC.ScaleTypeEnum.Signed;
			this.PlotUC1.ScaleUnitsText = null;
			this.PlotUC1.Size = new global::System.Drawing.Size(754, 185);
			this.PlotUC1.TabIndex = 23;
			this.PlotUC1.Title = null;
			this.PlotUC1.Xmax = 700f;
			this.PlotUC1.Xmin = 0f;
			this.PlotUC1.Xoffset = 0f;
			this.PlotUC1.xScaleMax = 1.0;
			this.PlotUC1.Ymax = 60f;
			this.PlotUC1.Ymin = -60f;
			this.PlotUC1.Yoffset = 60f;
			this.PlotUC2.AlarmsVisible = false;
			this.PlotUC2.AlarmValues = alarmValues3;
			this.PlotUC2.AutoSize = true;
			this.PlotUC2.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.PlotUC2.barWidth = 2;
			this.PlotUC2.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.PlotUC2.chartType = global::Vibration_Evaluation.PlotUC.ChartTypeEnum.bar;
			this.PlotUC2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.PlotUC2.Location = new global::System.Drawing.Point(3, 194);
			this.PlotUC2.Name = "PlotUC2";
			this.PlotUC2.ScaleLog10 = false;
			this.PlotUC2.ScaleType = global::Vibration_Evaluation.PlotUC.ScaleTypeEnum.Signed;
			this.PlotUC2.ScaleUnitsText = null;
			this.PlotUC2.Size = new global::System.Drawing.Size(754, 185);
			this.PlotUC2.TabIndex = 40;
			this.PlotUC2.Title = null;
			this.PlotUC2.Xmax = 700f;
			this.PlotUC2.Xmin = 0f;
			this.PlotUC2.Xoffset = 0f;
			this.PlotUC2.xScaleMax = 1.0;
			this.PlotUC2.Ymax = 60f;
			this.PlotUC2.Ymin = -60f;
			this.PlotUC2.Yoffset = 60f;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(785, 697);
			base.Controls.Add(this.LabelCapTime);
			base.Controls.Add(this.LabelOrange);
			base.Controls.Add(this.LabelDevice);
			base.Controls.Add(this.TextBoxFileLimit);
			base.Controls.Add(this.LabelFileLimit);
			base.Controls.Add(this.TextBoxTimer);
			base.Controls.Add(this.LabelTimer);
			base.Controls.Add(this.CheckBoxReStartTimer);
			base.Controls.Add(this.Panel1);
			base.Controls.Add(this.CheckBoxNoScale);
			base.Controls.Add(this.LabelSystemMsg);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.CheckBoxExtTrigger);
			base.Controls.Add(this.TableLayoutPanel1);
			base.Controls.Add(this.lblFileCount);
			base.Controls.Add(this.ckDatalogEnabled);
			base.Controls.Add(this.lblAxisSelect);
			base.Controls.Add(this.cbxAxis);
			base.Controls.Add(this.Label3);
			base.Controls.Add(this.cbxMode);
			base.Controls.Add(this.StatusStrip1);
			base.Controls.Add(this.ButtonStartStop);
			base.Controls.Add(this.MenuStrip1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MainMenuStrip = this.MenuStrip1;
			this.MinimumSize = new global::System.Drawing.Size(40, 160);
			base.Name = "FormMain";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Analog Devices ADCMXL Evaluation Software";
			this.MenuStrip1.ResumeLayout(false);
			this.MenuStrip1.PerformLayout();
			this.TableLayoutPanel1.ResumeLayout(false);
			this.TableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000E0 RID: 224
		private global::System.ComponentModel.IContainer components;
	}
}
