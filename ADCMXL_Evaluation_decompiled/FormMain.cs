using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using AdisApi;
using adisInterface;
using FX3Api;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;
using Vibration_Evaluation.My;
using Vibration_Evaluation.My.Resources;

namespace Vibration_Evaluation
{
	// Token: 0x02000016 RID: 22
	[DesignerGenerated]
	public partial class FormMain : Form
	{
		// Token: 0x060001CB RID: 459 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		public FormMain()
		{
			base.Load += this.FormMain_Load;
			base.Shown += this.FormMain_Shown;
			base.Resize += this.MainForm_Resize;
			base.FormClosing += this.FormMain_FormClosing;
			base.Closing += this.FormMain_Closing;
			this.DeviceSelected = null;
			this.DataLogForm = new FormDataLog();
			this.datArray = new double[0];
			this.displayList = new BindingList<FormMain.DisplayObject>();
			this.DeviceCollection = new DeviceCollection();
			this.Sampling = false;
			this.formLoaded = false;
			this.demoRunning = false;
			this.plotScaleIdxFreq1 = 6;
			this.plotScaleIdxTime1 = 4;
			this.plotScaleIdxFreq2 = 6;
			this.plotScaleIdxTime2 = 4;
			this.plotScaleIdxFreq3 = 6;
			this.plotScaleIdxTime3 = 4;
			this.updateIntervalSec = 10;
			this.LtBlueColor = Color.FromArgb(100, 100, 220);
			this.bgWorker = new BackgroundWorker
			{
				WorkerReportsProgress = true,
				WorkerSupportsCancellation = true
			};
			this.paintCount = 0;
			this.AutoModeAgain = false;
			this.tempera = 0.0;
			this.TimeDomainFirst = true;
			this.initialDevice = "";
			this.firstFileWrite = true;
			this.CalledFromFormReg = false;
			this.stopwatch = new Stopwatch();
			this.RecModeChangedByUser = true;
			this.UserActionLogFileEnabled = false;
			this._UserInputEnabled = true;
			this.InitializeComponent();
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0000ED6A File Offset: 0x0000CF6A
		// (set) Token: 0x060001CD RID: 461 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public virtual iVIBEcontrol DutControl { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001CE RID: 462 RVA: 0x0000ED7D File Offset: 0x0000CF7D
		// (set) Token: 0x060001CF RID: 463 RVA: 0x0000ED87 File Offset: 0x0000CF87
		private virtual BackgroundWorker bgWorker { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x060001D0 RID: 464 RVA: 0x0000ED90 File Offset: 0x0000CF90
		private void FormMain_Load(object sender, EventArgs e)
		{
			checked
			{
				int num;
				num++;
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00003198 File Offset: 0x00001398
		private void TimerFormLoaded_Tick(object sender, EventArgs e)
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000EDA4 File Offset: 0x0000CFA4
		private void FormMain_Shown(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					this.bgWorker.DoWork += this.bgWorker_DoWork;
					this.bgWorker.ProgressChanged += this.bgWorker_ProgressChanged;
					this.bgWorker.RunWorkerCompleted += this.bgWorker_RunWorkerCompleted;
					this.bgWorker.WorkerReportsProgress = true;
					this.bgWorker.WorkerSupportsCancellation = true;
					string text = Application.ProductVersion.ToString();
					text = text.Substring(0, Application.ProductVersion.ToString().Length);
					this.Text = "Analog Devices ADCMXL Evaluation Software  " + text;
					this.ButtonStartStop.Enabled = false;
					this.ButtonStartStop.Text = "Start";
					this.DeviceCollection.CreateCollection(Resources.DeviceCatalog);
					bool errorFound = this.DeviceCollection.ErrorFound;
					if (errorFound)
					{
						Interaction.MsgBox("Device resource parsing error.\r\n" + this.DeviceCollection.ErrorText, MsgBoxStyle.OkOnly, null);
						base.Close();
					}
					this.InitializeDeviceSelectionMenu();
					this.initialDevice = this.DeviceCollection.Last<DeviceClass>().Label;
					bool flag = !string.IsNullOrEmpty(MySettingsProperty.Settings.Device) && this.DeviceCollection.Contains(MySettingsProperty.Settings.Device);
					if (flag)
					{
						this.initialDevice = MySettingsProperty.Settings.Device;
					}
					this.PlotUC1.ckCursorLocation.Checked = true;
					this.PlotUC2.ckCursorLocation.Checked = true;
					this.PlotUC3.ckCursorLocation.Checked = true;
					this.PlotUC1.AddTrace("X_Axis", "X_ACCL_OUT", this.LtBlueColor, true);
					this.PlotUC2.AddTrace("Y_Axis", "Y_ACCL_OUT", this.LtBlueColor, true);
					this.PlotUC3.AddTrace("Z_Axis", "Z_ACCL_OUT", this.LtBlueColor, true);
					this.cbxAxis.Items.Add("X axis");
					this.cbxAxis.Items.Add("Y axis");
					this.cbxAxis.Items.Add("Z axis");
					this.cbxMode.Items.Add("Manual FFT");
					this.cbxMode.Items.Add("Automatic FFT");
					this.cbxMode.Items.Add("Manual Time Capture");
					this.cbxMode.Items.Add("Real Time");
					this.FillBindingList();
					int width = Screen.PrimaryScreen.Bounds.Width;
					int height = Screen.PrimaryScreen.Bounds.Height;
					base.Left = (int)Math.Round(unchecked((double)width / 2.0 - (double)base.Width / 2.0));
					base.Top = (int)Math.Round(unchecked((double)height / 2.0 - (double)base.Height / 2.0));
					this.TimerFormLoaded.Enabled = true;
				}
				catch (Exception ex)
				{
					Interaction.MsgBox(ex.ToString(), MsgBoxStyle.OkOnly, null);
				}
				this.LabelCapTime.Visible = false;
				this.TimerFormLoaded.Enabled = false;
				this.PlotsSetFrequencyDomain(110000f);
				this.formLoaded = true;
				this.PlotUC1.ResizeControlAndComponets();
				this.PlotUC2.ResizeControlAndComponets();
				this.PlotUC3.ResizeControlAndComponets();
				this.Panel1.Location = this.TableLayoutPanel1.Location;
				this.SystemMsg("No board sofware connection.");
				this.SystemMsgColor(Color.Pink);
				this.BoardInit();
				GlobalDeclarations.SPIsclkDefault = 14000000;
				GlobalDeclarations.SPIsclkUser = 14000000;
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		public void BoardInit()
		{
			base.Enabled = false;
			try
			{
				bool flag = Operators.CompareString(MySettingsProperty.Settings.BoardID, null, false) != 0 | Operators.CompareString(MySettingsProperty.Settings.BoardID, "NONE", false) != 0;
				if (flag)
				{
					try
					{
						FormMessage formMessage = new FormMessage();
						formMessage.ShowMessage("Connecting. Please wait", "");
						this.LabelSystemMsg.Text = "no board connection click Comm menu USB.";
						Application.DoEvents();
						bool flag2 = Operators.CompareString(MySettingsProperty.Settings.BoardID, GlobalDeclarations.BoardIDtype.SDPEVAL.ToString(), false) == 0;
						if (flag2)
						{
							bool flag3 = GlobalDeclarations.progUt.ScanForBlackFin();
							bool flag4 = flag3;
							if (flag4)
							{
								GlobalDeclarations.BoardID = GlobalDeclarations.BoardIDtype.SDPEVAL;
								this.LabelSystemMsg.Text = "SDP board connected";
								this.LabelSystemMsg.BackColor = Control.DefaultBackColor;
							}
						}
						else
						{
							bool flag3 = GlobalDeclarations.progUt.FX3connect();
							bool flag5 = flag3;
							if (flag5)
							{
								GlobalDeclarations.BoardID = GlobalDeclarations.BoardIDtype.FX3;
								this.LabelSystemMsg.Text = "FX3 board connected";
								this.LabelSystemMsg.BackColor = Control.DefaultBackColor;
							}
						}
						formMessage.Hide();
					}
					catch (Exception ex)
					{
					}
				}
				bool flag6 = GlobalDeclarations.BoardID == GlobalDeclarations.BoardIDtype.NONE;
				if (flag6)
				{
					this.LabelSystemMsg.Text = "no board connection click Comm menu USB.";
					this.LabelSystemMsg.BackColor = Color.LightPink;
				}
				else
				{
					this.LabelSystemMsg.Text = "";
					this.LabelSystemMsg.BackColor = Control.DefaultBackColor;
					this.SetDevice(this.initialDevice);
					bool flag7 = !string.IsNullOrEmpty(MySettingsProperty.Settings.SPIconfig);
					if (flag7)
					{
						this.DutControl.SpiConfig = MySettingsProperty.Settings.SPIconfig;
					}
					this.ButtonStartStop.Enabled = true;
					this.recordModeShow();
				}
			}
			catch (Exception ex2)
			{
				Interaction.MsgBox(ex2.Message, MsgBoxStyle.Exclamation, null);
			}
			base.Enabled = true;
			base.Activate();
		}

		/// <summary>
		/// Reads Dut RecCtrl1 register. Sets rec. mode combobox selection.
		/// </summary>
		// Token: 0x060001D4 RID: 468 RVA: 0x0000F3EC File Offset: 0x0000D5EC
		private void recordModeShow()
		{
			this.RecModeChangedByUser = false;
			try
			{
				this.DutControl.RecordControl_Read();
				this.cbxMode.SelectedIndex = (int)this.DutControl.RecordMode;
				string text = this.cbxMode.Text;
				if (Operators.CompareString(text, "Manual FFT", false) != 0)
				{
					if (Operators.CompareString(text, "Automatic FFT", false) != 0)
					{
						if (Operators.CompareString(text, "Manual Time Capture", false) != 0)
						{
							if (Operators.CompareString(text, "Real Time", false) != 0)
							{
							}
						}
					}
					else
					{
						this.ckDatalogEnabled.Enabled = true;
					}
				}
				else
				{
					this.ckDatalogEnabled.Enabled = true;
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000F4B8 File Offset: 0x0000D6B8
		private void FillBindingList()
		{
			this.displayList.Clear();
			this.displayList.Add(new FormMain.DisplayObject
			{
				regName = "X_BUF",
				evalLabel = "X axis"
			});
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00003198 File Offset: 0x00001398
		private void InitializeSDPSpiInterface()
		{
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		private void PlotsSetTimeDomain()
		{
			bool flag = !this.formLoaded;
			if (!flag)
			{
				int num = 4096;
				bool flag2 = Operators.CompareString(this.PlotUC1.Title, "Time Domain X axis", false) == 0;
				if (!flag2)
				{
					this.plotScaleIdxFreq1 = this.PlotUC1.YscaleIndex;
					this.PlotUC1.YscaleIndex = this.plotScaleIdxTime1;
					bool timeDomainFirst = this.TimeDomainFirst;
					if (timeDomainFirst)
					{
						this.PlotUC1.YscaleIndex = 4;
					}
				}
				this.PlotUC1.Title = "Time Domain X axis";
				this.PlotUC1.Xmax = (float)num;
				this.PlotUC1.xScaleMax = (double)num;
				this.PlotUC1.resetDisplay();
				this.PlotUC1.ScaleArrayY = new float[]
				{
					0.001f,
					0.01f,
					0.1f,
					0.5f,
					1f,
					2f,
					5f,
					10f,
					20f,
					40f,
					62.5f
				};
				this.PlotUC1.ScaleType = PlotUC.ScaleTypeEnum.Signed;
				this.PlotUC1.chartType = PlotUC.ChartTypeEnum.line;
				this.PlotUC1.lblXaxis.Text = "sample #";
				this.PlotUC1.lblYaxis.Text = "(g)";
				bool flag3 = Operators.CompareString(this.PlotUC2.Title, "Time Domain Y axis", false) == 0;
				if (!flag3)
				{
					this.plotScaleIdxFreq2 = this.PlotUC2.YscaleIndex;
					this.PlotUC2.YscaleIndex = this.plotScaleIdxTime2;
					bool timeDomainFirst2 = this.TimeDomainFirst;
					if (timeDomainFirst2)
					{
						this.PlotUC2.YscaleIndex = 4;
					}
				}
				this.PlotUC2.Title = "Time Domain Y axis";
				this.PlotUC2.Xmax = (float)num;
				this.PlotUC2.xScaleMax = (double)num;
				this.PlotUC2.resetDisplay();
				this.PlotUC2.ScaleArrayY = new float[]
				{
					0.001f,
					0.01f,
					0.1f,
					0.5f,
					1f,
					2f,
					5f,
					10f,
					20f,
					40f,
					62.5f
				};
				this.PlotUC2.ScaleType = PlotUC.ScaleTypeEnum.Signed;
				this.PlotUC2.chartType = PlotUC.ChartTypeEnum.line;
				this.PlotUC2.lblXaxis.Text = "sample #";
				this.PlotUC2.lblYaxis.Text = "(g)";
				bool flag4 = Operators.CompareString(this.PlotUC3.Title, "Time Domain Z axis", false) == 0;
				if (!flag4)
				{
					this.plotScaleIdxFreq3 = this.PlotUC3.YscaleIndex;
					this.PlotUC3.YscaleIndex = this.plotScaleIdxTime3;
					bool timeDomainFirst3 = this.TimeDomainFirst;
					if (timeDomainFirst3)
					{
						this.TimeDomainFirst = false;
						this.PlotUC3.YscaleIndex = 4;
					}
				}
				this.PlotUC3.Title = "Time Domain Z axis";
				this.PlotUC3.Xmax = (float)num;
				this.PlotUC3.xScaleMax = (double)num;
				this.PlotUC3.resetDisplay();
				this.PlotUC3.ScaleArrayY = new float[]
				{
					0.001f,
					0.01f,
					0.1f,
					0.5f,
					1f,
					2f,
					5f,
					10f,
					20f,
					40f,
					62.5f
				};
				this.PlotUC3.ScaleType = PlotUC.ScaleTypeEnum.Signed;
				this.PlotUC3.chartType = PlotUC.ChartTypeEnum.line;
				this.PlotUC3.lblXaxis.Text = "sample #";
				this.PlotUC3.lblYaxis.Text = "(g)";
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000F810 File Offset: 0x0000DA10
		private void PlotsSetFrequencyDomain(float MaxFreq = 110000f)
		{
			int num = 2048;
			bool flag = Operators.CompareString(this.PlotUC1.Title, "Frequency Domain X axis", false) == 0;
			if (!flag)
			{
				this.plotScaleIdxTime1 = this.PlotUC1.YscaleIndex;
				this.PlotUC1.YscaleIndex = this.plotScaleIdxFreq1;
			}
			this.PlotUC1.Title = "Frequency Domain X axis";
			this.PlotUC1.lblXaxis.Text = "(Hz)";
			this.PlotUC1.lblYaxis.Text = "";
			float[] scaleArrayY = new float[]
			{
				63f,
				255f,
				1023f,
				4095f,
				16383f,
				32767f,
				65535f
			};
			this.PlotUC1.ScaleArrayY = scaleArrayY;
			this.PlotUC1.Xmax = (float)num;
			this.PlotUC1.resetDisplay();
			this.PlotUC1.xScaleMax = (double)MaxFreq;
			this.PlotUC1.ScaleType = PlotUC.ScaleTypeEnum.Unsigned;
			this.PlotUC1.chartType = PlotUC.ChartTypeEnum.bar;
			this.PlotUC1.Ymin = 0f;
			bool flag2 = Operators.CompareString(this.PlotUC2.Title, "Frequency Domain Y axis", false) == 0;
			if (!flag2)
			{
				this.plotScaleIdxTime2 = this.PlotUC2.YscaleIndex;
				this.PlotUC2.YscaleIndex = this.plotScaleIdxFreq2;
			}
			this.PlotUC2.Title = "Frequency Domain Y axis";
			this.PlotUC2.lblXaxis.Text = "(Hz)";
			this.PlotUC2.lblYaxis.Text = "";
			this.PlotUC2.ScaleArrayY = scaleArrayY;
			this.PlotUC2.Xmax = (float)num;
			this.PlotUC2.resetDisplay();
			this.PlotUC2.xScaleMax = (double)MaxFreq;
			this.PlotUC2.ScaleType = PlotUC.ScaleTypeEnum.Unsigned;
			this.PlotUC2.chartType = PlotUC.ChartTypeEnum.bar;
			this.PlotUC2.Ymin = 0f;
			bool flag3 = Operators.CompareString(this.PlotUC3.Title, "Frequency Domain Z axis", false) == 0;
			if (!flag3)
			{
				this.plotScaleIdxTime3 = this.PlotUC3.YscaleIndex;
				this.PlotUC3.YscaleIndex = this.plotScaleIdxFreq3;
			}
			this.PlotUC3.Title = "Frequency Domain Z axis";
			this.PlotUC3.lblXaxis.Text = "(Hz)";
			this.PlotUC3.lblYaxis.Text = "";
			this.PlotUC3.ScaleArrayY = scaleArrayY;
			this.PlotUC3.Xmax = (float)num;
			this.PlotUC3.resetDisplay();
			this.PlotUC3.xScaleMax = (double)MaxFreq;
			this.PlotUC3.ScaleType = PlotUC.ScaleTypeEnum.Unsigned;
			this.PlotUC3.chartType = PlotUC.ChartTypeEnum.bar;
			this.PlotUC3.Ymin = 0f;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
		private void cbxMode_TextChanged(object sender, EventArgs e)
		{
			bool sampling = this.Sampling;
			if (sampling)
			{
				this.StopSampling();
			}
			this.ButtonStartStop.Text = "Start";
			this.ButtonStartStop.BackColor = Color.FromKnownColor(KnownColor.Control);
			this.setAxisSelectEnabled(false);
			this.cbxAxis.Enabled = false;
			checked
			{
				try
				{
					uint num = GlobalDeclarations.Dut.ReadUnsigned(GlobalDeclarations.Reg["REC_CTRL"]);
					uint num2 = (uint)(unchecked((ulong)num) & 65532UL);
					string text = this.cbxMode.Text;
					if (Operators.CompareString(text, "Manual FFT", false) != 0)
					{
						if (Operators.CompareString(text, "Automatic FFT", false) != 0)
						{
							if (Operators.CompareString(text, "Manual Time Capture", false) != 0)
							{
								if (Operators.CompareString(text, "Real Time", false) == 0)
								{
									bool recModeChangedByUser = this.RecModeChangedByUser;
									if (recModeChangedByUser)
									{
										GlobalDeclarations.Dut.WriteUnsigned(GlobalDeclarations.Reg["REC_CTRL"], (uint)(unchecked((ulong)num2) + 3UL));
									}
									this.DutControl.RecordMode = iVIBEcontrol.CapMode.realTimeDomain;
									bool flag = !this.CalledFromFormReg;
									if (flag)
									{
										this.writeLogFile("REC_CTRL", "Real Time", "cbxMode FormMain");
									}
									this.PlotsSetTimeDomain();
								}
							}
							else
							{
								bool recModeChangedByUser2 = this.RecModeChangedByUser;
								if (recModeChangedByUser2)
								{
									GlobalDeclarations.Dut.WriteUnsigned(GlobalDeclarations.Reg["REC_CTRL"], (uint)(unchecked((ulong)num2) + 2UL));
								}
								this.DutControl.RecordMode = iVIBEcontrol.CapMode.manualTimeDomain;
								this.PlotsSetTimeDomain();
								bool flag2 = !this.CalledFromFormReg;
								if (flag2)
								{
									this.writeLogFile("REC_CTRL", "Manual Time Capture", "cbxMode FormMain");
								}
							}
						}
						else
						{
							bool recModeChangedByUser3 = this.RecModeChangedByUser;
							if (recModeChangedByUser3)
							{
								GlobalDeclarations.Dut.WriteUnsigned(GlobalDeclarations.Reg["REC_CTRL"], (uint)(unchecked((ulong)num2) + 1UL));
							}
							this.DutControl.RecordMode = iVIBEcontrol.CapMode.AutomaticFFT;
							this.PlotsSetFrequencyDomain(110000f);
							bool flag3 = !this.CalledFromFormReg;
							if (flag3)
							{
								this.writeLogFile("REC_CTRL", "Automatic FFT", "cbxMode FormMain");
							}
						}
					}
					else
					{
						bool recModeChangedByUser4 = this.RecModeChangedByUser;
						if (recModeChangedByUser4)
						{
							GlobalDeclarations.Dut.WriteUnsigned(GlobalDeclarations.Reg["REC_CTRL"], num2);
						}
						this.DutControl.RecordMode = iVIBEcontrol.CapMode.manualFFT;
						this.PlotsSetFrequencyDomain(110000f);
						bool flag4 = !this.CalledFromFormReg;
						if (flag4)
						{
							this.writeLogFile("REC_CTRL", "Manual FFT", "cbxMode FormMain");
						}
					}
				}
				catch (Exception ex)
				{
					Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, null);
				}
				bool flag5 = !this.RecModeChangedByUser;
				if (flag5)
				{
					this.RecModeChangedByUser = true;
				}
				this.PlotUC1.ResizeControlAndComponets();
				this.PlotUC2.ResizeControlAndComponets();
				this.PlotUC3.ResizeControlAndComponets();
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		private void ButtonStartStop_Click(object sender, EventArgs e)
		{
			string text = this.ButtonStartStop.Text;
			if (Operators.CompareString(text, "Start", false) != 0)
			{
				if (Operators.CompareString(text, "Stop", false) == 0)
				{
					this.writeLogFile("GLOB_CMD", "stop", "ButtonStartStop FormMain");
					bool flag = Operators.CompareString(this.cbxMode.Text, "Manual FFT", false) == 0 | Operators.CompareString(this.cbxMode.Text, "Manual Time Capture", false) == 0;
					if (flag)
					{
						string prompt = "Manual FFT and Manual Time Capture Modes cannot be cancled.";
						Interaction.MsgBox(prompt, MsgBoxStyle.OkOnly, null);
						this.ButtonStartStop.Text = "Start";
						this.guiIdle = true;
						MyProject.Forms.FormRegAccess.Enabled = true;
					}
					else
					{
						this.DutControl.userCancel = true;
						this.bgWorker.CancelAsync();
						this.DutControl.StopCmd();
						this.ButtonStartStop.Text = "Start";
						this.recordModeShow();
						this.guiIdle = true;
						MyProject.Forms.FormRegAccess.Enabled = true;
					}
				}
			}
			else
			{
				this.LabelCapTime.Text = "0";
				this.stopwatch.Reset();
				this.stopwatch.Start();
				this.DutControl.userCancel = false;
				this.writeLogFile("GLOB_CMD", "start", "ButtonStartStop FormMain");
				bool flag2 = Operators.CompareString(this.cbxMode.Text, "Real Time", false) == 0;
				if (flag2)
				{
					bool flag3 = GlobalDeclarations.BoardID == GlobalDeclarations.BoardIDtype.SDPEVAL;
					if (flag3)
					{
						string text2 = "An SDP USB board cannot capture Real Time Data burst.\r\n\r\n";
						text2 += "A Cypress FX3 USB board is required.";
						MessageBox.Show(text2, "ADcmXL Evaluation");
					}
					else
					{
						FormDataLogBurst formDataLogBurst = new FormDataLogBurst(this.DeviceSelected.ProductID);
						formDataLogBurst.ShowDialog();
						this.guiIdle = true;
					}
				}
				else
				{
					bool flag4 = true;
					bool flag5 = this.DutControl.RecordMode == iVIBEcontrol.CapMode.AutomaticFFT;
					if (flag5)
					{
						flag4 = this.DutControl.REC_PRD_check();
					}
					bool flag6 = flag4;
					if (flag6)
					{
						this.ButtonStartStop.Text = "Stop";
						this.ButtonStartStop.BackColor = Color.Yellow;
						Application.DoEvents();
						this.guiIdle = false;
						this.bgWorker.RunWorkerAsync();
					}
				}
			}
		}

		/// <summary>
		/// Delegated, called from FormRegAccess when write reg = REC_CTRL.
		/// </summary>
		/// <param name="b"></param>
		// Token: 0x060001DB RID: 475 RVA: 0x00010033 File Offset: 0x0000E233
		public void RecModeShowFromRAF(bool b)
		{
			this.CalledFromFormReg = true;
			this.recordModeShow();
			this.CalledFromFormReg = false;
		}

		/// <summary>
		/// Delegated, called from FromRegAccess when CMD = GLOB_CMD Start/Stop.
		/// </summary>
		/// <param name="b"></param>
		// Token: 0x060001DC RID: 476 RVA: 0x0001004C File Offset: 0x0000E24C
		public void CMDStartStop(bool b)
		{
			EventArgs e = new EventArgs();
			this.ButtonStartStop_Click(this, e);
		}

		/// <summary>
		/// Writes log file rows.
		/// </summary>
		/// <param name="rg"></param>
		/// <param name="val"></param>
		/// <param name="src"></param>
		// Token: 0x060001DD RID: 477 RVA: 0x0001006C File Offset: 0x0000E26C
		private void writeLogFile(string rg, string val, string src)
		{
			bool flag = !this.UserActionLogFileEnabled;
			if (!flag)
			{
				string text = ",";
				string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				string str = "AdcmXLregisterLog.csv";
				string path = folderPath + "\\" + str;
				string text2 = DateTime.Now.ToString();
				try
				{
					bool flag2 = this.firstFileWrite;
					if (flag2)
					{
						this.firstFileWrite = false;
						bool flag3 = File.Exists(path);
						if (flag3)
						{
							File.Delete(path);
						}
					}
					bool append = true;
					TextWriter textWriter = new StreamWriter(path, append);
					string value = string.Concat(new string[]
					{
						rg,
						text,
						val,
						text,
						src,
						text,
						text2
					});
					textWriter.WriteLine(value);
					textWriter.Close();
				}
				catch (Exception ex)
				{
					string str2 = "Exception\r\nFormMain sub writeLogFile\r\n";
					Interaction.MsgBox(str2 + "\r\n" + ex.Message, MsgBoxStyle.OkOnly, null);
				}
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00010180 File Offset: 0x0000E380
		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			bool flag = this.AutoModeAgain & !this.bgWorker.CancellationPending;
			if (flag)
			{
				this.DutControl.waitForCapture(this.bgWorker, true);
			}
			else
			{
				bool flag2 = !this.bgWorker.CancellationPending;
				if (flag2)
				{
					this.DutControl.StartCmd(this.bgWorker);
				}
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00003198 File Offset: 0x00001398
		private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000101E4 File Offset: 0x0000E3E4
		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.stopwatch.Stop();
			this.LabelCapTime.Text = this.stopwatch.Elapsed.ToString();
			Application.DoEvents();
			bool flag = e.Cancelled | this.DutControl.userCancel;
			if (flag)
			{
				this.AutoModeAgain = false;
				string text = "Capture Canceled\r\n";
				text += "Data values for a canceled capture are not valid.\r\n";
				text += "Re-setting mode to Manual FFT.";
				Interaction.MsgBox(text, MsgBoxStyle.OkOnly, null);
			}
			else
			{
				bool flag2 = e.Error != null;
				if (flag2)
				{
					Interaction.MsgBox(e.Error.Message, MsgBoxStyle.OkOnly, null);
				}
				else
				{
					this.PlotData();
					bool flag3 = this.DutControl.RecordMode == iVIBEcontrol.CapMode.AutomaticFFT & !this.DutControl.userCancel;
					if (flag3)
					{
						this.AutoModeAgain = true;
						this.bgWorker.RunWorkerAsync();
					}
					else
					{
						this.AutoModeAgain = false;
						this.guiIdle = true;
					}
					bool flag4 = this.DutControl.RecordMode != iVIBEcontrol.CapMode.AutomaticFFT;
					if (flag4)
					{
						this.guiIdle = true;
					}
				}
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0001030C File Offset: 0x0000E50C
		public void PlotData()
		{
			bool flag = this.DutControl.RecordMode == iVIBEcontrol.CapMode.realTimeDomain;
			checked
			{
				if (flag)
				{
					this.DatalogWrite();
					this.guiIdle = true;
				}
				else
				{
					bool @checked = this.ckDatalogEnabled.Checked;
					if (@checked)
					{
						this.DatalogWrite();
						bool flag2 = this.DutControl.RecordMode != iVIBEcontrol.CapMode.AutomaticFFT;
						if (flag2)
						{
							this.guiIdle = true;
						}
					}
					bool visible = MyProject.Forms.FormAlarmStatus2.Visible;
					if (visible)
					{
						this.DutControl.Alarms.LsbValues[this.DutControl.recInfo.sampleRateOpt] = this.DutControl.AlarmsReadSRO(this.DutControl.recInfo.sampleRateOpt);
						int num = 32;
						int num2 = this.DutControl.recInfo.AvgCnt;
						bool flag3 = num2 == 0;
						if (flag3)
						{
							num2 = 1;
						}
						int num3 = (int)Math.Round(unchecked(Math.Pow(2.0, (double)num) / (double)num2 * 0.009535));
						this.DutControl.Alarms.CalcPlotAxisData(this.DutControl.recInfo.sampleRateOpt, (double)num3, (double)this.DutControl.recInfo.Fmax);
						this.PlotUC1.AlarmValues = this.DutControl.Alarms.AxisValues_X;
						this.PlotUC2.AlarmValues = this.DutControl.Alarms.AxisValues_Y;
						this.PlotUC3.AlarmValues = this.DutControl.Alarms.AxisValues_Z;
						List<uint> codeList = new List<uint>();
						codeList = this.DutControl.AlarmStatusRead();
						MyProject.Forms.FormAlarmStatus2.UpdateStatus(codeList, this.DutControl.sensorSelected);
					}
					this.PlotUC1.Traces[0].VertData = new double[this.DutControl.Sensor[this.DutControl.sensorSelected].dataX.Length + 1];
					Array.Copy(this.DutControl.Sensor[this.DutControl.sensorSelected].dataX, this.PlotUC1.Traces[0].VertData, this.DutControl.Sensor[this.DutControl.sensorSelected].dataX.Length);
					this.PlotUC1.Traces[0].Pen.Color = this.LtBlueColor;
					this.PlotUC2.Traces[0].VertData = new double[this.DutControl.Sensor[this.DutControl.sensorSelected].dataY.Length + 1];
					Array.Copy(this.DutControl.Sensor[this.DutControl.sensorSelected].dataY, this.PlotUC2.Traces[0].VertData, this.DutControl.Sensor[this.DutControl.sensorSelected].dataY.Length);
					this.PlotUC2.Traces[0].Pen.Color = this.LtBlueColor;
					this.PlotUC3.Traces[0].VertData = new double[this.DutControl.Sensor[this.DutControl.sensorSelected].dataZ.Length + 1];
					Array.Copy(this.DutControl.Sensor[this.DutControl.sensorSelected].dataZ, this.PlotUC3.Traces[0].VertData, this.DutControl.Sensor[this.DutControl.sensorSelected].dataZ.Length);
					this.PlotUC3.Traces[0].Pen.Color = this.LtBlueColor;
					double xScaleMax = 4096.0;
					bool flag4 = this.DutControl.RecordMode == iVIBEcontrol.CapMode.manualFFT | this.DutControl.RecordMode == iVIBEcontrol.CapMode.AutomaticFFT;
					if (flag4)
					{
						xScaleMax = (double)this.DutControl.recInfo.Fmax;
					}
					this.PlotUC1.xScaleMax = xScaleMax;
					this.PlotUC2.xScaleMax = xScaleMax;
					this.PlotUC3.xScaleMax = xScaleMax;
					this.PlotUC1.PlotTraces();
					this.PlotUC2.PlotTraces();
					this.PlotUC3.PlotTraces();
					bool flag5 = this.DutControl.RecordMode != iVIBEcontrol.CapMode.AutomaticFFT & this.CheckBoxReStartTimer.Checked;
					if (flag5)
					{
						this.TimerReStart.Interval = Conversions.ToInteger(this.TextBoxTimer.Text);
						this.TimerReStart.Enabled = true;
					}
					MyProject.Forms.FormRegAccess.Enabled = true;
				}
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x000107D7 File Offset: 0x0000E9D7
		private void StopSampling()
		{
			this.guiIdle = true;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000107E4 File Offset: 0x0000E9E4
		private bool TryReadRegListData()
		{
			RegMapCollection regMapCollection = new RegMapCollection();
			regMapCollection.Add(GlobalDeclarations.Reg[this.displayList[0].regName]);
			checked
			{
				try
				{
					this.datArray = this.DutControl.readSelectedAxis(regMapCollection, 1U);
					int num = 0;
					try
					{
						foreach (FormMain.DisplayObject displayObject in this.displayList)
						{
							displayObject.value = this.datArray[num];
							num++;
						}
					}
					finally
					{
						IEnumerator<FormMain.DisplayObject> enumerator;
						if (enumerator != null)
						{
							enumerator.Dispose();
						}
					}
				}
				catch (Exception ex)
				{
					this.StopSampling();
					Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, null);
				}
				return true;
			}
		}

		/// <summary>
		/// Returns the data from datArray from the register specifed by the label.
		/// </summary>
		/// <param name="label"></param>
		/// <returns></returns>
		/// <remarks></remarks>
		// Token: 0x060001E4 RID: 484 RVA: 0x000108C0 File Offset: 0x0000EAC0
		private double GetDataForLabel(string label)
		{
			double result = 0.0;
			try
			{
				foreach (FormMain.DisplayObject displayObject in this.displayList)
				{
					bool flag = displayObject.evalLabel.Contains(label);
					if (flag)
					{
						result = displayObject.value;
					}
				}
			}
			finally
			{
				IEnumerator<FormMain.DisplayObject> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			return result;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00010938 File Offset: 0x0000EB38
		private void RegistersToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FormRegAccess formRegAccess = MyProject.Forms.FormRegAccess;
			formRegAccess.WindowState = FormWindowState.Normal;
			formRegAccess.RecModeShowInMain = new Action<bool>(this.RecModeShowFromRAF);
			formRegAccess.CmdStartStopinMain = new Action<bool>(this.CMDStartStop);
			formRegAccess.writeLogFileinMain = new Action<string, string, string>(this.writeLogFile);
			formRegAccess.Show();
			formRegAccess.BringToFront();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0001099D File Offset: 0x0000EB9D
		private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MyProject.Forms.AboutBox1.Icon = base.Icon;
			MyProject.Forms.AboutBox1.ShowDialog();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00003198 File Offset: 0x00001398
		private void dgvOutPutRegs_MouseMove(object sender, MouseEventArgs e)
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000109C8 File Offset: 0x0000EBC8
		private void MainForm_Resize(object sender, EventArgs e)
		{
			bool flag = base.WindowState != FormWindowState.Minimized;
			if (flag)
			{
				this.ResizePlots();
			}
		}

		/// <summary>
		/// Redraws stored Y Data traces
		/// </summary>
		// Token: 0x060001E9 RID: 489 RVA: 0x000109F0 File Offset: 0x0000EBF0
		private void ResizePlots()
		{
			checked
			{
				this.TableLayoutPanel1.Height = base.Height - (this.TableLayoutPanel1.Top + 70);
				this.TableLayoutPanel1.Width = base.Width - 40;
				bool visible = this.Panel1.Visible;
				if (visible)
				{
					this.Panel1.Height = base.Height - (this.Panel1.Top + 70);
					this.Panel1.Width = base.Width - 40;
				}
				this.PlotUC1.PlotTraces();
				this.PlotUC2.PlotTraces();
				this.PlotUC3.PlotTraces();
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00010AA0 File Offset: 0x0000ECA0
		private void InitializeDeviceSelectionMenu()
		{
			try
			{
				foreach (DeviceClass deviceClass in this.DeviceCollection)
				{
					ToolStripMenuItem value = new ToolStripMenuItem(deviceClass.Label, null, new EventHandler(this.DeviceSelectionMenuItem_Click));
					this.DeviceSelectToolStripMenuItem.DropDownItems.Add(value);
				}
			}
			finally
			{
				IEnumerator<DeviceClass> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00010B18 File Offset: 0x0000ED18
		public void SystemMsg(string st)
		{
			bool flag = Operators.CompareString(st, "setvisfalse", false) == 0;
			if (flag)
			{
				this.LabelSystemMsg.Visible = false;
			}
			else
			{
				this.LabelSystemMsg.Visible = true;
				this.LabelSystemMsg.Text = st;
				Application.DoEvents();
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00010B6B File Offset: 0x0000ED6B
		public void SystemMsgColor(Color cl)
		{
			this.LabelSystemMsg.BackColor = cl;
			Application.DoEvents();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00010B84 File Offset: 0x0000ED84
		private void DeviceSelectionMenuItem_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)sender;
			this.StopSampling();
			this.SetDevice(toolStripMenuItem.Text);
			this.FillBindingList();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		private void CreateCmdMap()
		{
			string @string = Resources.ResourceManager.GetString(this.DeviceSelected.CmdMapName);
			bool flag = string.IsNullOrEmpty(@string);
			if (flag)
			{
				throw new Exception("No CmdDataFile found for device: " + this.DeviceSelected.Label + ".");
			}
			GlobalDeclarations.Cmd.CreateCollection(@string);
			bool errorFound = GlobalDeclarations.Cmd.ErrorFound;
			if (errorFound)
			{
				throw new Exception("Cmd File resource parsing error.\r\n" + GlobalDeclarations.Cmd.ErrorText);
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00010C38 File Offset: 0x0000EE38
		private void CreateRegMap()
		{
			string @string = Resources.ResourceManager.GetString(this.DeviceSelected.RegMapName);
			bool flag = string.IsNullOrEmpty(@string);
			if (flag)
			{
				throw new Exception("No RegDataFile found for device: " + this.DeviceSelected.Label + ".");
			}
			GlobalDeclarations.Reg.CreateCollection(@string);
			bool errorFound = GlobalDeclarations.Reg.ErrorFound;
			if (errorFound)
			{
				throw new Exception("Reg File resource parsing error.\r\n" + GlobalDeclarations.Reg.ErrorText);
			}
			try
			{
				foreach (RegClass regClass in GlobalDeclarations.Reg)
				{
					bool flag2 = string.IsNullOrEmpty(regClass.EvalLabel);
					if (flag2)
					{
						throw new Exception("Null or empty eval label found in reg config.");
					}
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
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00010D18 File Offset: 0x0000EF18
		private void SetDevice(string deviceLabel)
		{
			this.DeviceSelected = this.DeviceCollection[deviceLabel];
			GlobalDeclarations.BoardID = GlobalDeclarations.BoardIDtype.FX3;
			if (Operators.CompareString(deviceLabel, "ADcmXL3021", false) != 0)
			{
				if (Operators.CompareString(deviceLabel, "ADcmXL2021", false) != 0)
				{
					if (Operators.CompareString(deviceLabel, "ADcmXL1021", false) != 0)
					{
						Interaction.MsgBox("FormMain SetDevice() unknown device lable.", MsgBoxStyle.OkOnly, null);
					}
					else
					{
						GlobalDeclarations.Dutcmi1x = new AdcmInterface1Axis(GlobalDeclarations.FX3comm);
						GlobalDeclarations.FX3comm.PartType = DUTType.ADcmXL1021;
						GlobalDeclarations.Dut = new AdcmInterface1Axis(GlobalDeclarations.FX3comm);
					}
				}
				else
				{
					GlobalDeclarations.Dutcmi2x = new AdcmInterface2Axis(GlobalDeclarations.FX3comm);
					GlobalDeclarations.FX3comm.PartType = DUTType.ADcmXL2021;
					GlobalDeclarations.Dut = new AdcmInterface2Axis(GlobalDeclarations.FX3comm);
				}
			}
			else
			{
				GlobalDeclarations.Dutcmi3x = new AdcmInterface3Axis(GlobalDeclarations.FX3comm);
				GlobalDeclarations.FX3comm.PartType = DUTType.ADcmXL3021;
				GlobalDeclarations.Dut = new AdcmInterface3Axis(GlobalDeclarations.FX3comm);
			}
			GlobalDeclarations.fX3spiConfig.SCLKFrequency = 14000000;
			this.CreateRegMap();
			this.CreateCmdMap();
			ControlType dutControlType = this.DeviceSelected.DutControlType;
			if (dutControlType != ControlType.ADcmXL3021)
			{
				throw new Exception("Unknown Control Type");
			}
			this.DutControl = new ADcmXL021control(GlobalDeclarations.Reg, ref GlobalDeclarations.Dut, ref GlobalDeclarations.Cmd, ref GlobalDeclarations.FX3comm);
			this.DutControl.ProductSelected = this.DeviceSelected.Label;
			this.LabelDevice.Text = this.DeviceSelected.Label;
			this.DutControl.initializeDUT(this.DeviceSelected.ProductID, ref GlobalDeclarations.FX3comm);
			try
			{
				foreach (object obj in this.DeviceSelectToolStripMenuItem.DropDownItems)
				{
					ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)obj;
					toolStripMenuItem.Checked = false;
					bool flag = Operators.CompareString(toolStripMenuItem.Text, this.DeviceSelected.Label, false) == 0;
					if (flag)
					{
						toolStripMenuItem.Checked = true;
					}
				}
			}
			finally
			{
				IEnumerator enumerator;
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
			TableLayoutRowStyleCollection rowStyles = this.TableLayoutPanel1.RowStyles;
			rowStyles[0].Height = 0f;
			rowStyles[1].Height = 0f;
			rowStyles[2].Height = 0f;
			switch (this.DeviceSelected.AccelAxes)
			{
			case 1:
				rowStyles[2].Height = 100f;
				this.TableLayoutPanel1.Controls.Add(this.PlotUC3);
				this.PlotUC3.Dock = DockStyle.Fill;
				this.cbxAxis.Items.Add("Z axis");
				this.cbxAxis.SelectedItem = RuntimeHelpers.GetObjectValue(this.cbxAxis.Items[0]);
				this.DutControl.axisSelected = iVIBEcontrol.axis.z;
				base.Height = 514;
				break;
			case 2:
				rowStyles[1].Height = 50f;
				rowStyles[2].Height = 50f;
				this.cbxAxis.Items.Add("X axis");
				this.cbxAxis.Items.Add("Y axis");
				this.cbxAxis.SelectedItem = RuntimeHelpers.GetObjectValue(this.cbxAxis.Items[0]);
				this.DutControl.axisSelected = iVIBEcontrol.axis.x;
				base.Height = 735;
				break;
			case 3:
				rowStyles[0].Height = 33.3f;
				rowStyles[1].Height = 33.3f;
				rowStyles[2].Height = 33.3f;
				this.cbxAxis.Items.Add("X axis");
				this.cbxAxis.Items.Add("Y axis");
				this.cbxAxis.Items.Add("Z axis");
				this.cbxAxis.SelectedItem = RuntimeHelpers.GetObjectValue(this.cbxAxis.Items[0]);
				this.DutControl.axisSelected = iVIBEcontrol.axis.x;
				base.Height = 735;
				break;
			}
			this.DutControl.axisSelected = iVIBEcontrol.axis.x;
			this.ResizePlots();
			this.recordModeShow();
			this.PlotUC1.ClearTracesDrawGrid(true);
			this.DataLogForm.dutDrSetupRoutine = new Action(this.SetupDataCapture);
			FormDataLog dataLogForm = this.DataLogForm;
			FormMain._Closure$__R68-0 CS$<>8__locals1 = new FormMain._Closure$__R68-0(CS$<>8__locals1);
			CS$<>8__locals1.$VB$NonLocal_2 = this.DutControl;
			dataLogForm.dutSampleRateRoutine = (() => CS$<>8__locals1.$VB$NonLocal_2.SampleRateGet(0.0));
		}

		/// <summary>
		/// Displays a message box ix the product number read from the DUT does not match the expected number.
		/// </summary>
		/// <param name="prodNumber"></param>
		/// <remarks></remarks>
		// Token: 0x060001F1 RID: 497 RVA: 0x00003198 File Offset: 0x00001398
		private void VerifyProductNumber(int prodNumber)
		{
		}

		/// <summary>
		/// This routine can be called by data cap window to set DUT up for data capture.
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x060001F2 RID: 498 RVA: 0x000111E8 File Offset: 0x0000F3E8
		private void SetupDataCapture()
		{
			this.DutControl.SetupDataReady(1, true, true);
			GlobalDeclarations.FX3comm.DrActive = true;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00011206 File Offset: 0x0000F406
		private void USBToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.StopSampling();
			MyProject.Forms.FormUSBConnect.ShowDialog();
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x00011220 File Offset: 0x0000F420
		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			bool flag = this.DutControl != null;
			if (flag)
			{
				this.StopSampling();
				bool isBusy = this.bgWorker.IsBusy;
				if (isBusy)
				{
					this.bgWorker.CancelAsync();
					this.bgWorker = null;
				}
				this.DutControl.ExtTrigger = false;
				Application.DoEvents();
				Thread.Sleep(100);
				MySettingsProperty.Settings.Device = this.DeviceSelected.Label;
				MySettingsProperty.Settings.SPIconfig = this.DutControl.SpiConfig;
				MySettingsProperty.Settings.BoardID = GlobalDeclarations.BoardID.ToString();
				MySettingsProperty.Settings.Save();
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000112D9 File Offset: 0x0000F4D9
		private void DisplayGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000112E0 File Offset: 0x0000F4E0
		private void cbxAxis_TextChanged(object sender, EventArgs e)
		{
			string text = this.cbxAxis.Text;
			if (Operators.CompareString(text, "X axis", false) != 0)
			{
				if (Operators.CompareString(text, "Y axis", false) != 0)
				{
					if (Operators.CompareString(text, "Z axis", false) == 0)
					{
						this.DutControl.axisSelected = iVIBEcontrol.axis.z;
					}
				}
				else
				{
					this.DutControl.axisSelected = iVIBEcontrol.axis.y;
				}
			}
			else
			{
				this.DutControl.axisSelected = iVIBEcontrol.axis.x;
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00011370 File Offset: 0x0000F570
		private void setAxisSelectEnabled(bool tf)
		{
			this.lblAxisSelect.Enabled = tf;
			this.cbxAxis.Enabled = tf;
		}

		/// <summary>
		/// Increments the File count in the file name. Calls Dutcommand GetDatalogText. Writes file.
		/// </summary>
		// Token: 0x060001F8 RID: 504 RVA: 0x00011390 File Offset: 0x0000F590
		private void DatalogWrite()
		{
			string text = "";
			string text2 = "";
			string delimiter = GlobalDeclarations.DatalogUt.delimiter;
			bool append = false;
			try
			{
				text2 = Path.GetExtension(GlobalDeclarations.DatalogUt.FileName);
				text = Path.GetFileName(GlobalDeclarations.DatalogUt.FileName).Replace(text2, "");
			}
			catch (Exception ex)
			{
			}
			string path = string.Concat(new string[]
			{
				GlobalDeclarations.DatalogUt.Path,
				"\\",
				text,
				"_",
				decimal.Add(new decimal(GlobalDeclarations.DatalogUt.FileCount), 1m).ToString(),
				text2
			});
			try
			{
				TextWriter textWriter = new StreamWriter(path, append);
				List<string> list = new List<string>();
				list = this.DutControl.GetDataLogText(delimiter);
				try
				{
					foreach (string value in list)
					{
						textWriter.WriteLine(value);
					}
				}
				finally
				{
					List<string>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				textWriter.Close();
				GlobalDeclarations.DatalogUt.FileCount = checked(GlobalDeclarations.DatalogUt.FileCount + 1UL);
				this.lblFileCount.Text = GlobalDeclarations.DatalogUt.FileCount.ToString();
			}
			catch (Exception ex2)
			{
				string prompt = ex2.Message + "\r\n NO data file was written. ";
				Interaction.MsgBox(prompt, MsgBoxStyle.Exclamation, null);
			}
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00011548 File Offset: 0x0000F748
		private void DataLogToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.StopSampling();
			this.DataLogForm.ShowDialog();
			this.lblFileCount.Text = GlobalDeclarations.DatalogUt.FileCount.ToString();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00011587 File Offset: 0x0000F787
		private void AlarmValuesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MyProject.Forms.FormAlarmValues.axisCount = this.DeviceSelected.AccelAxes;
			MyProject.Forms.FormAlarmValues.Show();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000115B5 File Offset: 0x0000F7B5
		private void AlarmStatusFormToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MyProject.Forms.FormAlarmStatus2.Show(checked((uint)this.DeviceSelected.AccelAxes), this.DutControl.SensorsOnNetwork.countActive);
		}

		/// <summary>
		/// sets color and text of btnStartStop, Menu, TabControl1, ckDatalog 
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000115E4 File Offset: 0x0000F7E4
		// (set) Token: 0x060001FD RID: 509 RVA: 0x000115FC File Offset: 0x0000F7FC
		public bool guiIdle
		{
			get
			{
				return this._UserInputEnabled;
			}
			set
			{
				this._UserInputEnabled = value;
				if (value)
				{
					this.MenuStrip1.Enabled = true;
					this.cbxMode.Enabled = true;
					this.ButtonStartStop.BackColor = Color.FromKnownColor(KnownColor.Control);
					this.ButtonStartStop.Text = "Start";
					this.ckDatalogEnabled.Enabled = true;
				}
				else
				{
					this.ButtonStartStop.Text = "Stop";
					this.ButtonStartStop.BackColor = Color.Yellow;
					this.MenuStrip1.Enabled = false;
					this.cbxMode.Enabled = false;
					this.cbxAxis.Enabled = false;
					this.ckDatalogEnabled.Enabled = false;
				}
			}
		}

		/// <summary>
		/// Toggle Checked. True = Start Polling DIO3. False = usercancel.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		// Token: 0x060001FE RID: 510 RVA: 0x000116BC File Offset: 0x0000F8BC
		private void CheckBoxExtTrigger_Click(object sender, EventArgs e)
		{
			PinObject pin = new PinObject(PortType.H, 3U);
			bool flag = !this.CheckBoxExtTrigger.Checked;
			if (flag)
			{
				this.CheckBoxExtTrigger.Text = "Enable Ext Trigger";
				this.CheckBoxExtTrigger.BackColor = SystemColors.Control;
				this.ButtonStartStop.Enabled = true;
				this.cbxMode.Enabled = true;
				this.DutControl.userCancel = true;
				this.DutControl.ExtTrigger = false;
			}
			else
			{
				bool @checked = this.CheckBoxExtTrigger.Checked;
				if (@checked)
				{
					this.CheckBoxExtTrigger.Text = "Waiting on DIO3 High";
					this.CheckBoxExtTrigger.BackColor = Color.Yellow;
					this.ButtonStartStop.Enabled = false;
					this.cbxMode.Enabled = false;
					this.DutControl.ExtTrigger = true;
					while (this.CheckBoxExtTrigger.Checked)
					{
						this.DutControl.ExtTriggerWait();
						this.DatalogWrite();
						Application.DoEvents();
						bool flag2 = GlobalDeclarations.FX3comm.ReadPin(pin) > 0U;
						while (flag2 & this.DutControl.ExtTrigger)
						{
							flag2 = (GlobalDeclarations.FX3comm.ReadPin(pin) > 0U);
							Thread.Sleep(1);
							Application.DoEvents();
						}
					}
				}
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0001180C File Offset: 0x0000FA0C
		private void HelpToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MyProject.Forms.FormHelp.Show();
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00011820 File Offset: 0x0000FA20
		private void CheckBoxNoScale_CheckStateChanged(object sender, EventArgs e)
		{
			bool @checked = this.CheckBoxNoScale.Checked;
			if (@checked)
			{
				this.DutControl.ScaleData = false;
			}
			else
			{
				this.DutControl.ScaleData = true;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0001185C File Offset: 0x0000FA5C
		private void XAxisToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.LargePlotcfg("X");
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0001186B File Offset: 0x0000FA6B
		private void YAxisToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.LargePlotcfg("Y");
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0001187A File Offset: 0x0000FA7A
		private void ZAxisToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.LargePlotcfg("Z");
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00011889 File Offset: 0x0000FA89
		private void AllAxisToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.LargePlotcfg("");
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00011898 File Offset: 0x0000FA98
		private void LargePlotcfg(string st)
		{
			st = Strings.UCase(st);
			this.PlotUC1.Parent = this.TableLayoutPanel1;
			this.PlotUC2.Parent = this.TableLayoutPanel1;
			this.PlotUC3.Parent = this.TableLayoutPanel1;
			this.Panel1.Visible = false;
			string left = st;
			if (Operators.CompareString(left, "X", false) != 0)
			{
				if (Operators.CompareString(left, "Y", false) != 0)
				{
					if (Operators.CompareString(left, "Z", false) == 0)
					{
						this.PlotUC3.Parent = this.Panel1;
						this.Panel1.Visible = true;
					}
				}
				else
				{
					this.PlotUC2.Parent = this.Panel1;
					this.Panel1.Visible = true;
				}
			}
			else
			{
				this.PlotUC1.Parent = this.Panel1;
				this.Panel1.Visible = true;
			}
			this.ResizePlots();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00011990 File Offset: 0x0000FB90
		private void TimerReStart_Tick(object sender, EventArgs e)
		{
			this.TimerReStart.Enabled = false;
			object objectValue = RuntimeHelpers.GetObjectValue(new object());
			EventArgs e2 = new EventArgs();
			this.ButtonStartStop_Click(RuntimeHelpers.GetObjectValue(objectValue), e2);
			bool flag = Conversions.ToInteger(this.lblFileCount.Text) == checked(Conversions.ToInteger(this.TextBoxFileLimit.Text) - 1);
			if (flag)
			{
				this.CheckBoxReStartTimer.Checked = false;
				this.CheckBoxReStartTimer.BackColor = SystemColors.Control;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00011A14 File Offset: 0x0000FC14
		private void CheckBoxReStartTimer_Click(object sender, EventArgs e)
		{
			bool @checked = this.CheckBoxReStartTimer.Checked;
			if (@checked)
			{
				this.CheckBoxReStartTimer.BackColor = Color.Plum;
			}
			else
			{
				this.CheckBoxReStartTimer.BackColor = SystemColors.Control;
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00011A58 File Offset: 0x0000FC58
		private void SPIToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.StopSampling();
			new FormSPI
			{
				writeLogFileinMain = new Action<string, string, string>(this.writeLogFile)
			}.ShowDialog();
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00011A8C File Offset: 0x0000FC8C
		private void DeviceSelectToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
		{
			this.SetDevice(this.DeviceSelectToolStripMenuItem.Text);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00011AA4 File Offset: 0x0000FCA4
		private void FormMain_Closing(object sender, CancelEventArgs e)
		{
			bool flag = GlobalDeclarations.FX3comm != null;
			if (flag)
			{
				GlobalDeclarations.FX3comm.Disconnect();
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00011ACB File Offset: 0x0000FCCB
		protected override void Finalize()
		{
			base.Finalize();
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600020E RID: 526 RVA: 0x00013416 File Offset: 0x00011616
		// (set) Token: 0x0600020F RID: 527 RVA: 0x00013420 File Offset: 0x00011620
		internal virtual Button ButtonStartStop
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonStartStop;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonStartStop_Click);
				Button buttonStartStop = this._ButtonStartStop;
				if (buttonStartStop != null)
				{
					buttonStartStop.Click -= value2;
				}
				this._ButtonStartStop = value;
				buttonStartStop = this._ButtonStartStop;
				if (buttonStartStop != null)
				{
					buttonStartStop.Click += value2;
				}
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00013463 File Offset: 0x00011663
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0001346D File Offset: 0x0001166D
		internal virtual MenuStrip MenuStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00013476 File Offset: 0x00011676
		// (set) Token: 0x06000213 RID: 531 RVA: 0x00013480 File Offset: 0x00011680
		internal virtual ToolStripMenuItem DataLogToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._DataLogToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.DataLogToolStripMenuItem_Click);
				ToolStripMenuItem dataLogToolStripMenuItem = this._DataLogToolStripMenuItem;
				if (dataLogToolStripMenuItem != null)
				{
					dataLogToolStripMenuItem.Click -= value2;
				}
				this._DataLogToolStripMenuItem = value;
				dataLogToolStripMenuItem = this._DataLogToolStripMenuItem;
				if (dataLogToolStripMenuItem != null)
				{
					dataLogToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000134C3 File Offset: 0x000116C3
		// (set) Token: 0x06000215 RID: 533 RVA: 0x000134D0 File Offset: 0x000116D0
		internal virtual ToolStripMenuItem RegistersToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._RegistersToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.RegistersToolStripMenuItem_Click);
				ToolStripMenuItem registersToolStripMenuItem = this._RegistersToolStripMenuItem;
				if (registersToolStripMenuItem != null)
				{
					registersToolStripMenuItem.Click -= value2;
				}
				this._RegistersToolStripMenuItem = value;
				registersToolStripMenuItem = this._RegistersToolStripMenuItem;
				if (registersToolStripMenuItem != null)
				{
					registersToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00013513 File Offset: 0x00011713
		// (set) Token: 0x06000217 RID: 535 RVA: 0x00013520 File Offset: 0x00011720
		internal virtual ToolStripMenuItem AboutToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._AboutToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.AboutToolStripMenuItem_Click);
				ToolStripMenuItem aboutToolStripMenuItem = this._AboutToolStripMenuItem;
				if (aboutToolStripMenuItem != null)
				{
					aboutToolStripMenuItem.Click -= value2;
				}
				this._AboutToolStripMenuItem = value;
				aboutToolStripMenuItem = this._AboutToolStripMenuItem;
				if (aboutToolStripMenuItem != null)
				{
					aboutToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00013563 File Offset: 0x00011763
		// (set) Token: 0x06000219 RID: 537 RVA: 0x00013570 File Offset: 0x00011770
		internal virtual ToolStripMenuItem DeviceSelectToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._DeviceSelectToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.DeviceSelectToolStripMenuItem_CheckedChanged);
				ToolStripMenuItem deviceSelectToolStripMenuItem = this._DeviceSelectToolStripMenuItem;
				if (deviceSelectToolStripMenuItem != null)
				{
					deviceSelectToolStripMenuItem.CheckedChanged -= value2;
				}
				this._DeviceSelectToolStripMenuItem = value;
				deviceSelectToolStripMenuItem = this._DeviceSelectToolStripMenuItem;
				if (deviceSelectToolStripMenuItem != null)
				{
					deviceSelectToolStripMenuItem.CheckedChanged += value2;
				}
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000135B3 File Offset: 0x000117B3
		// (set) Token: 0x0600021B RID: 539 RVA: 0x000135BD File Offset: 0x000117BD
		internal virtual ToolStripMenuItem ToolsToolStripMenuItem { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600021C RID: 540 RVA: 0x000135C6 File Offset: 0x000117C6
		// (set) Token: 0x0600021D RID: 541 RVA: 0x000135D0 File Offset: 0x000117D0
		internal virtual StatusStrip StatusStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000135D9 File Offset: 0x000117D9
		// (set) Token: 0x0600021F RID: 543 RVA: 0x000135E4 File Offset: 0x000117E4
		internal virtual ToolStripMenuItem USBToolStripMenuItem1
		{
			[CompilerGenerated]
			get
			{
				return this._USBToolStripMenuItem1;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.USBToolStripMenuItem_Click);
				ToolStripMenuItem usbtoolStripMenuItem = this._USBToolStripMenuItem1;
				if (usbtoolStripMenuItem != null)
				{
					usbtoolStripMenuItem.Click -= value2;
				}
				this._USBToolStripMenuItem1 = value;
				usbtoolStripMenuItem = this._USBToolStripMenuItem1;
				if (usbtoolStripMenuItem != null)
				{
					usbtoolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00013627 File Offset: 0x00011827
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00013631 File Offset: 0x00011831
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0001363A File Offset: 0x0001183A
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00013644 File Offset: 0x00011844
		internal virtual ComboBox cbxMode
		{
			[CompilerGenerated]
			get
			{
				return this._cbxMode;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cbxMode_TextChanged);
				ComboBox cbxMode = this._cbxMode;
				if (cbxMode != null)
				{
					cbxMode.TextChanged -= value2;
				}
				this._cbxMode = value;
				cbxMode = this._cbxMode;
				if (cbxMode != null)
				{
					cbxMode.TextChanged += value2;
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00013687 File Offset: 0x00011887
		// (set) Token: 0x06000225 RID: 549 RVA: 0x00013694 File Offset: 0x00011894
		internal virtual ComboBox cbxAxis
		{
			[CompilerGenerated]
			get
			{
				return this._cbxAxis;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.cbxAxis_TextChanged);
				ComboBox cbxAxis = this._cbxAxis;
				if (cbxAxis != null)
				{
					cbxAxis.TextChanged -= value2;
				}
				this._cbxAxis = value;
				cbxAxis = this._cbxAxis;
				if (cbxAxis != null)
				{
					cbxAxis.TextChanged += value2;
				}
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000136D7 File Offset: 0x000118D7
		// (set) Token: 0x06000227 RID: 551 RVA: 0x000136E1 File Offset: 0x000118E1
		internal virtual Label lblAxisSelect { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000136EA File Offset: 0x000118EA
		// (set) Token: 0x06000229 RID: 553 RVA: 0x000136F4 File Offset: 0x000118F4
		internal virtual CheckBox ckDatalogEnabled { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000136FD File Offset: 0x000118FD
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00013707 File Offset: 0x00011907
		internal virtual Label lblFileCount { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00013710 File Offset: 0x00011910
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0001371A File Offset: 0x0001191A
		internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00013723 File Offset: 0x00011923
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0001372D File Offset: 0x0001192D
		internal virtual PlotUC PlotUC2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00013736 File Offset: 0x00011936
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00013740 File Offset: 0x00011940
		internal virtual PlotUC PlotUC1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00013749 File Offset: 0x00011949
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00013753 File Offset: 0x00011953
		internal virtual PlotUC PlotUC3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0001375C File Offset: 0x0001195C
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00013768 File Offset: 0x00011968
		internal virtual System.Windows.Forms.Timer TimerFormLoaded
		{
			[CompilerGenerated]
			get
			{
				return this._TimerFormLoaded;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.TimerFormLoaded_Tick);
				System.Windows.Forms.Timer timerFormLoaded = this._TimerFormLoaded;
				if (timerFormLoaded != null)
				{
					timerFormLoaded.Tick -= value2;
				}
				this._TimerFormLoaded = value;
				timerFormLoaded = this._TimerFormLoaded;
				if (timerFormLoaded != null)
				{
					timerFormLoaded.Tick += value2;
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000137AB File Offset: 0x000119AB
		// (set) Token: 0x06000237 RID: 567 RVA: 0x000137B5 File Offset: 0x000119B5
		internal virtual System.Windows.Forms.Timer sampleTimer { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000238 RID: 568 RVA: 0x000137BE File Offset: 0x000119BE
		// (set) Token: 0x06000239 RID: 569 RVA: 0x000137C8 File Offset: 0x000119C8
		internal virtual ToolStripMenuItem AlarmsToolStripMenuItem { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600023A RID: 570 RVA: 0x000137D1 File Offset: 0x000119D1
		// (set) Token: 0x0600023B RID: 571 RVA: 0x000137DC File Offset: 0x000119DC
		internal virtual ToolStripMenuItem AlarmValuesToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._AlarmValuesToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.AlarmValuesToolStripMenuItem_Click);
				ToolStripMenuItem alarmValuesToolStripMenuItem = this._AlarmValuesToolStripMenuItem;
				if (alarmValuesToolStripMenuItem != null)
				{
					alarmValuesToolStripMenuItem.Click -= value2;
				}
				this._AlarmValuesToolStripMenuItem = value;
				alarmValuesToolStripMenuItem = this._AlarmValuesToolStripMenuItem;
				if (alarmValuesToolStripMenuItem != null)
				{
					alarmValuesToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0001381F File Offset: 0x00011A1F
		// (set) Token: 0x0600023D RID: 573 RVA: 0x0001382C File Offset: 0x00011A2C
		internal virtual ToolStripMenuItem AlarmStatusFormToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._AlarmStatusFormToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.AlarmStatusFormToolStripMenuItem_Click);
				ToolStripMenuItem alarmStatusFormToolStripMenuItem = this._AlarmStatusFormToolStripMenuItem;
				if (alarmStatusFormToolStripMenuItem != null)
				{
					alarmStatusFormToolStripMenuItem.Click -= value2;
				}
				this._AlarmStatusFormToolStripMenuItem = value;
				alarmStatusFormToolStripMenuItem = this._AlarmStatusFormToolStripMenuItem;
				if (alarmStatusFormToolStripMenuItem != null)
				{
					alarmStatusFormToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0001386F File Offset: 0x00011A6F
		// (set) Token: 0x0600023F RID: 575 RVA: 0x0001387C File Offset: 0x00011A7C
		internal virtual CheckBox CheckBoxExtTrigger
		{
			[CompilerGenerated]
			get
			{
				return this._CheckBoxExtTrigger;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.CheckBoxExtTrigger_Click);
				CheckBox checkBoxExtTrigger = this._CheckBoxExtTrigger;
				if (checkBoxExtTrigger != null)
				{
					checkBoxExtTrigger.Click -= value2;
				}
				this._CheckBoxExtTrigger = value;
				checkBoxExtTrigger = this._CheckBoxExtTrigger;
				if (checkBoxExtTrigger != null)
				{
					checkBoxExtTrigger.Click += value2;
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000240 RID: 576 RVA: 0x000138BF File Offset: 0x00011ABF
		// (set) Token: 0x06000241 RID: 577 RVA: 0x000138C9 File Offset: 0x00011AC9
		internal virtual Label Label1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000138D2 File Offset: 0x00011AD2
		// (set) Token: 0x06000243 RID: 579 RVA: 0x000138DC File Offset: 0x00011ADC
		internal virtual ToolStripMenuItem HelpToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._HelpToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.HelpToolStripMenuItem_Click);
				ToolStripMenuItem helpToolStripMenuItem = this._HelpToolStripMenuItem;
				if (helpToolStripMenuItem != null)
				{
					helpToolStripMenuItem.Click -= value2;
				}
				this._HelpToolStripMenuItem = value;
				helpToolStripMenuItem = this._HelpToolStripMenuItem;
				if (helpToolStripMenuItem != null)
				{
					helpToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0001391F File Offset: 0x00011B1F
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00013929 File Offset: 0x00011B29
		internal virtual Label LabelSystemMsg { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00013932 File Offset: 0x00011B32
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0001393C File Offset: 0x00011B3C
		internal virtual CheckBox CheckBoxNoScale
		{
			[CompilerGenerated]
			get
			{
				return this._CheckBoxNoScale;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.CheckBoxNoScale_CheckStateChanged);
				CheckBox checkBoxNoScale = this._CheckBoxNoScale;
				if (checkBoxNoScale != null)
				{
					checkBoxNoScale.CheckStateChanged -= value2;
				}
				this._CheckBoxNoScale = value;
				checkBoxNoScale = this._CheckBoxNoScale;
				if (checkBoxNoScale != null)
				{
					checkBoxNoScale.CheckStateChanged += value2;
				}
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0001397F File Offset: 0x00011B7F
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00013989 File Offset: 0x00011B89
		internal virtual Panel Panel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00013992 File Offset: 0x00011B92
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0001399C File Offset: 0x00011B9C
		internal virtual ToolStripMenuItem ViewToolStripMenuItem { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600024C RID: 588 RVA: 0x000139A5 File Offset: 0x00011BA5
		// (set) Token: 0x0600024D RID: 589 RVA: 0x000139B0 File Offset: 0x00011BB0
		internal virtual ToolStripMenuItem AllAxisToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._AllAxisToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.AllAxisToolStripMenuItem_Click);
				ToolStripMenuItem allAxisToolStripMenuItem = this._AllAxisToolStripMenuItem;
				if (allAxisToolStripMenuItem != null)
				{
					allAxisToolStripMenuItem.Click -= value2;
				}
				this._AllAxisToolStripMenuItem = value;
				allAxisToolStripMenuItem = this._AllAxisToolStripMenuItem;
				if (allAxisToolStripMenuItem != null)
				{
					allAxisToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600024E RID: 590 RVA: 0x000139F3 File Offset: 0x00011BF3
		// (set) Token: 0x0600024F RID: 591 RVA: 0x00013A00 File Offset: 0x00011C00
		internal virtual ToolStripMenuItem XAxisToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._XAxisToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.XAxisToolStripMenuItem_Click);
				ToolStripMenuItem xaxisToolStripMenuItem = this._XAxisToolStripMenuItem;
				if (xaxisToolStripMenuItem != null)
				{
					xaxisToolStripMenuItem.Click -= value2;
				}
				this._XAxisToolStripMenuItem = value;
				xaxisToolStripMenuItem = this._XAxisToolStripMenuItem;
				if (xaxisToolStripMenuItem != null)
				{
					xaxisToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000250 RID: 592 RVA: 0x00013A43 File Offset: 0x00011C43
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00013A50 File Offset: 0x00011C50
		internal virtual ToolStripMenuItem YAxisToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._YAxisToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.YAxisToolStripMenuItem_Click);
				ToolStripMenuItem yaxisToolStripMenuItem = this._YAxisToolStripMenuItem;
				if (yaxisToolStripMenuItem != null)
				{
					yaxisToolStripMenuItem.Click -= value2;
				}
				this._YAxisToolStripMenuItem = value;
				yaxisToolStripMenuItem = this._YAxisToolStripMenuItem;
				if (yaxisToolStripMenuItem != null)
				{
					yaxisToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00013A93 File Offset: 0x00011C93
		// (set) Token: 0x06000253 RID: 595 RVA: 0x00013AA0 File Offset: 0x00011CA0
		internal virtual ToolStripMenuItem ZAxisToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._ZAxisToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ZAxisToolStripMenuItem_Click);
				ToolStripMenuItem zaxisToolStripMenuItem = this._ZAxisToolStripMenuItem;
				if (zaxisToolStripMenuItem != null)
				{
					zaxisToolStripMenuItem.Click -= value2;
				}
				this._ZAxisToolStripMenuItem = value;
				zaxisToolStripMenuItem = this._ZAxisToolStripMenuItem;
				if (zaxisToolStripMenuItem != null)
				{
					zaxisToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00013AE3 File Offset: 0x00011CE3
		// (set) Token: 0x06000255 RID: 597 RVA: 0x00013AED File Offset: 0x00011CED
		internal virtual Label LabelTimer { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00013AF6 File Offset: 0x00011CF6
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00013B00 File Offset: 0x00011D00
		internal virtual TextBox TextBoxTimer { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00013B09 File Offset: 0x00011D09
		// (set) Token: 0x06000259 RID: 601 RVA: 0x00013B14 File Offset: 0x00011D14
		internal virtual System.Windows.Forms.Timer TimerReStart
		{
			[CompilerGenerated]
			get
			{
				return this._TimerReStart;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.TimerReStart_Tick);
				System.Windows.Forms.Timer timerReStart = this._TimerReStart;
				if (timerReStart != null)
				{
					timerReStart.Tick -= value2;
				}
				this._TimerReStart = value;
				timerReStart = this._TimerReStart;
				if (timerReStart != null)
				{
					timerReStart.Tick += value2;
				}
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00013B57 File Offset: 0x00011D57
		// (set) Token: 0x0600025B RID: 603 RVA: 0x00013B61 File Offset: 0x00011D61
		internal virtual Label LabelFileLimit { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00013B6A File Offset: 0x00011D6A
		// (set) Token: 0x0600025D RID: 605 RVA: 0x00013B74 File Offset: 0x00011D74
		internal virtual TextBox TextBoxFileLimit { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00013B7D File Offset: 0x00011D7D
		// (set) Token: 0x0600025F RID: 607 RVA: 0x00013B87 File Offset: 0x00011D87
		internal virtual Label LabelDevice { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000260 RID: 608 RVA: 0x00013B90 File Offset: 0x00011D90
		// (set) Token: 0x06000261 RID: 609 RVA: 0x00013B9A File Offset: 0x00011D9A
		internal virtual Label LabelOrange { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00013BA3 File Offset: 0x00011DA3
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00013BAD File Offset: 0x00011DAD
		internal virtual Label LabelCapTime { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000264 RID: 612 RVA: 0x00013BB6 File Offset: 0x00011DB6
		// (set) Token: 0x06000265 RID: 613 RVA: 0x00013BC0 File Offset: 0x00011DC0
		internal virtual CheckBox CheckBoxReStartTimer
		{
			[CompilerGenerated]
			get
			{
				return this._CheckBoxReStartTimer;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.CheckBoxReStartTimer_Click);
				CheckBox checkBoxReStartTimer = this._CheckBoxReStartTimer;
				if (checkBoxReStartTimer != null)
				{
					checkBoxReStartTimer.Click -= value2;
				}
				this._CheckBoxReStartTimer = value;
				checkBoxReStartTimer = this._CheckBoxReStartTimer;
				if (checkBoxReStartTimer != null)
				{
					checkBoxReStartTimer.Click += value2;
				}
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00013C03 File Offset: 0x00011E03
		// (set) Token: 0x06000267 RID: 615 RVA: 0x00013C10 File Offset: 0x00011E10
		internal virtual ToolStripMenuItem SPIToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._SPIToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.SPIToolStripMenuItem_Click);
				ToolStripMenuItem spitoolStripMenuItem = this._SPIToolStripMenuItem;
				if (spitoolStripMenuItem != null)
				{
					spitoolStripMenuItem.Click -= value2;
				}
				this._SPIToolStripMenuItem = value;
				spitoolStripMenuItem = this._SPIToolStripMenuItem;
				if (spitoolStripMenuItem != null)
				{
					spitoolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x040000C2 RID: 194
		public DeviceClass DeviceSelected;

		// Token: 0x040000C3 RID: 195
		private FormDataLog DataLogForm;

		// Token: 0x040000C4 RID: 196
		private double[] datArray;

		// Token: 0x040000C5 RID: 197
		private BindingList<FormMain.DisplayObject> displayList;

		// Token: 0x040000C6 RID: 198
		private DeviceCollection DeviceCollection;

		// Token: 0x040000C7 RID: 199
		private bool Sampling;

		// Token: 0x040000C8 RID: 200
		private int valueColumnIndex;

		// Token: 0x040000C9 RID: 201
		private bool formLoaded;

		// Token: 0x040000CA RID: 202
		private bool demoRunning;

		// Token: 0x040000CB RID: 203
		private int plotScaleIdxFreq1;

		// Token: 0x040000CC RID: 204
		private int plotScaleIdxTime1;

		// Token: 0x040000CD RID: 205
		private int plotScaleIdxFreq2;

		// Token: 0x040000CE RID: 206
		private int plotScaleIdxTime2;

		// Token: 0x040000CF RID: 207
		private int plotScaleIdxFreq3;

		// Token: 0x040000D0 RID: 208
		private int plotScaleIdxTime3;

		// Token: 0x040000D1 RID: 209
		private int updateIntervalSec;

		// Token: 0x040000D3 RID: 211
		private Color LtBlueColor;

		// Token: 0x040000D5 RID: 213
		private int paintCount;

		// Token: 0x040000D6 RID: 214
		private bool AutoModeAgain;

		// Token: 0x040000D7 RID: 215
		public double tempera;

		// Token: 0x040000D8 RID: 216
		private bool TimeDomainFirst;

		// Token: 0x040000D9 RID: 217
		private string initialDevice;

		// Token: 0x040000DA RID: 218
		private bool firstFileWrite;

		// Token: 0x040000DB RID: 219
		private bool CalledFromFormReg;

		// Token: 0x040000DC RID: 220
		private Stopwatch stopwatch;

		// Token: 0x040000DD RID: 221
		private bool RecModeChangedByUser;

		// Token: 0x040000DE RID: 222
		private bool UserActionLogFileEnabled;

		// Token: 0x040000DF RID: 223
		private bool _UserInputEnabled;

		/// <summary>
		/// Object (row) added to a List for binding data to dataGridView.
		///  Properties of this object are the columns of a dataGridView
		/// </summary>
		// Token: 0x02000038 RID: 56
		private class DisplayObject
		{
			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000463 RID: 1123 RVA: 0x0001C438 File Offset: 0x0001A638
			// (set) Token: 0x06000464 RID: 1124 RVA: 0x0001C450 File Offset: 0x0001A650
			public string regName
			{
				get
				{
					return this._regName;
				}
				set
				{
					this._regName = value;
				}
			}

			// Token: 0x17000176 RID: 374
			// (get) Token: 0x06000465 RID: 1125 RVA: 0x0001C45C File Offset: 0x0001A65C
			// (set) Token: 0x06000466 RID: 1126 RVA: 0x0001C474 File Offset: 0x0001A674
			public string evalLabel
			{
				get
				{
					return this._evalLabel;
				}
				set
				{
					this._evalLabel = value;
				}
			}

			// Token: 0x17000177 RID: 375
			// (get) Token: 0x06000467 RID: 1127 RVA: 0x0001C480 File Offset: 0x0001A680
			// (set) Token: 0x06000468 RID: 1128 RVA: 0x0001C498 File Offset: 0x0001A698
			public double value
			{
				get
				{
					return this._value;
				}
				set
				{
					this._value = value;
				}
			}

			// Token: 0x17000178 RID: 376
			// (get) Token: 0x06000469 RID: 1129 RVA: 0x0001C4A4 File Offset: 0x0001A6A4
			public string valueString
			{
				get
				{
					return this.value.ToString("0.00");
				}
			}

			// Token: 0x040001FF RID: 511
			private string _regName;

			// Token: 0x04000200 RID: 512
			private string _evalLabel;

			// Token: 0x04000201 RID: 513
			private double _value;
		}
	}
}
