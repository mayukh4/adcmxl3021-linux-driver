// Decompiled with JetBrains decompiler
// Type: AdisApi.FormAdisBase
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using sdpApi1;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace AdisApi;

public class FormAdisBase : Form
{
  private AdisBase sdp;
  private FormDaughter daughterForm;
  private FormGPIO gpioForm;
  private Gpio gpio;
  /// <summary>Required designer variable.</summary>
  private IContainer components = (IContainer) null;
  private Button buttonConnect;
  private Button buttonFlashLed;
  private TextBox textBoxMessage;
  private Label label1;
  private TextBox textBoxInfo;
  private TextBox textBoxBoot;
  private TextBox textBoxUserGuid;
  private TextBox textBoxSessionGuid;
  private Label label2;
  private Label labelSessionGuid;
  private Button buttonDisconnect;
  private Button buttonProgramSdram;
  private Button buttonRefresh;
  private Button buttonResetDisconnect;
  private Button buttonProgramDaughter;
  private Button buttonGPIO;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="adisBase"></param>
  public FormAdisBase(AdisBase adisBase)
  {
    this.sdp = adisBase;
    this.gpio = new Gpio(this.sdp.Base, (SdpConnector) 0);
    this.InitializeComponent();
  }

  private void setFormConnected(bool connected)
  {
    this.buttonConnect.Enabled = !connected;
    this.buttonDisconnect.Enabled = connected;
    this.buttonFlashLed.Enabled = connected;
    this.buttonProgramSdram.Enabled = connected;
    this.buttonResetDisconnect.Enabled = connected;
    this.buttonProgramDaughter.Enabled = connected;
    this.buttonGPIO.Enabled = connected;
    if (connected)
    {
      this.textBoxMessage.Clear();
      this.textBoxInfo.Text = this.sdp.GetInfo();
      this.buttonFlashLed.Focus();
    }
    else
    {
      this.textBoxInfo.Clear();
      this.textBoxBoot.Clear();
      this.textBoxSessionGuid.Clear();
      this.textBoxUserGuid.Clear();
      this.buttonConnect.Focus();
    }
  }

  private void connect()
  {
    try
    {
      this.sdp.Connect();
    }
    catch (Exception ex)
    {
      this.textBoxMessage.Text = ex.Message;
      return;
    }
    this.setFormConnected(true);
  }

  private void buttonConnect_Click(object sender, EventArgs e) => this.connect();

  private void buttonFlashLed_Click(object sender, EventArgs e) => this.sdp.FlashLed();

  private void FormAdisBase_Load(object sender, EventArgs e)
  {
    this.setFormConnected(this.sdp.IsConncted());
  }

  private void buttonDisconnect_Click(object sender, EventArgs e)
  {
    this.sdp.Disconnect();
    this.setFormConnected(false);
  }

  private void buttonProgramSdram_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.Filter = "ldr files (*.ldr)|*.ldr|All files (*.*)|*.*";
    if (openFileDialog.ShowDialog() == DialogResult.OK)
    {
      try
      {
        this.sdp.ProgramSDRAM(openFileDialog.FileName, true);
      }
      catch (Exception ex)
      {
        this.textBoxMessage.Text = ex.Message;
        return;
      }
    }
    this.setFormConnected(false);
  }

  private void buttonRefresh_Click(object sender, EventArgs e)
  {
    this.setFormConnected(this.sdp.IsConncted());
  }

  private void buttonResetDisconnect_Click(object sender, EventArgs e)
  {
    this.sdp.ResetAndDisconnect();
    this.setFormConnected(false);
  }

  private void buttonProgramDaughter_Click(object sender, EventArgs e)
  {
    if (this.daughterForm == null || this.daughterForm.IsDisposed)
      this.daughterForm = new FormDaughter(this.sdp);
    this.daughterForm.Show();
  }

  private void buttonGPIO_Click(object sender, EventArgs e)
  {
    if (this.gpioForm == null || this.gpioForm.IsDisposed)
      this.gpioForm = new FormGPIO(this.gpio);
    this.gpioForm.Show();
  }

  private void textBoxBoot_TextChanged(object sender, EventArgs e)
  {
  }

  private void textBoxInfo_TextChanged(object sender, EventArgs e)
  {
  }

  /// <summary>Clean up any resources being used.</summary>
  /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  /// <summary>
  /// Required method for Designer support - do not modify
  /// the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    this.buttonConnect = new Button();
    this.buttonFlashLed = new Button();
    this.textBoxMessage = new TextBox();
    this.label1 = new Label();
    this.textBoxInfo = new TextBox();
    this.textBoxBoot = new TextBox();
    this.textBoxUserGuid = new TextBox();
    this.textBoxSessionGuid = new TextBox();
    this.label2 = new Label();
    this.labelSessionGuid = new Label();
    this.buttonDisconnect = new Button();
    this.buttonProgramSdram = new Button();
    this.buttonRefresh = new Button();
    this.buttonResetDisconnect = new Button();
    this.buttonProgramDaughter = new Button();
    this.buttonGPIO = new Button();
    this.SuspendLayout();
    this.buttonConnect.Location = new Point(16 /*0x10*/, 21);
    this.buttonConnect.Margin = new Padding(4, 4, 4, 4);
    this.buttonConnect.Name = "buttonConnect";
    this.buttonConnect.Size = new Size(183, 31 /*0x1F*/);
    this.buttonConnect.TabIndex = 0;
    this.buttonConnect.Text = "Connect";
    this.buttonConnect.UseVisualStyleBackColor = true;
    this.buttonConnect.Click += new EventHandler(this.buttonConnect_Click);
    this.buttonFlashLed.Location = new Point(16 /*0x10*/, 59);
    this.buttonFlashLed.Margin = new Padding(4, 4, 4, 4);
    this.buttonFlashLed.Name = "buttonFlashLed";
    this.buttonFlashLed.Size = new Size(183, 31 /*0x1F*/);
    this.buttonFlashLed.TabIndex = 1;
    this.buttonFlashLed.Text = "Flash LED";
    this.buttonFlashLed.UseVisualStyleBackColor = true;
    this.buttonFlashLed.Click += new EventHandler(this.buttonFlashLed_Click);
    this.textBoxMessage.Location = new Point(116, 367);
    this.textBoxMessage.Margin = new Padding(4, 4, 4, 4);
    this.textBoxMessage.Name = "textBoxMessage";
    this.textBoxMessage.ReadOnly = true;
    this.textBoxMessage.Size = new Size(339, 22);
    this.textBoxMessage.TabIndex = 2;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(41, 370);
    this.label1.Margin = new Padding(4, 0, 4, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(65, 17);
    this.label1.TabIndex = 3;
    this.label1.Text = "Message";
    this.textBoxInfo.Location = new Point(223, 25);
    this.textBoxInfo.Margin = new Padding(4, 4, 4, 4);
    this.textBoxInfo.Multiline = true;
    this.textBoxInfo.Name = "textBoxInfo";
    this.textBoxInfo.ReadOnly = true;
    this.textBoxInfo.Size = new Size(265, 128 /*0x80*/);
    this.textBoxInfo.TabIndex = 4;
    this.textBoxInfo.TextChanged += new EventHandler(this.textBoxInfo_TextChanged);
    this.textBoxBoot.Location = new Point(223, 167);
    this.textBoxBoot.Margin = new Padding(4, 4, 4, 4);
    this.textBoxBoot.Multiline = true;
    this.textBoxBoot.Name = "textBoxBoot";
    this.textBoxBoot.ReadOnly = true;
    this.textBoxBoot.Size = new Size(265, 78);
    this.textBoxBoot.TabIndex = 5;
    this.textBoxBoot.TextChanged += new EventHandler(this.textBoxBoot_TextChanged);
    this.textBoxUserGuid.Location = new Point(116, 399);
    this.textBoxUserGuid.Margin = new Padding(4, 4, 4, 4);
    this.textBoxUserGuid.Name = "textBoxUserGuid";
    this.textBoxUserGuid.ReadOnly = true;
    this.textBoxUserGuid.Size = new Size(339, 22);
    this.textBoxUserGuid.TabIndex = 6;
    this.textBoxSessionGuid.Location = new Point(116, 431);
    this.textBoxSessionGuid.Margin = new Padding(4, 4, 4, 4);
    this.textBoxSessionGuid.Name = "textBoxSessionGuid";
    this.textBoxSessionGuid.ReadOnly = true;
    this.textBoxSessionGuid.Size = new Size(339, 22);
    this.textBoxSessionGuid.TabIndex = 7;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(29, 402);
    this.label2.Margin = new Padding(4, 0, 4, 0);
    this.label2.Name = "label2";
    this.label2.Size = new Size(76, 17);
    this.label2.TabIndex = 8;
    this.label2.Text = "User GUID";
    this.labelSessionGuid.AutoSize = true;
    this.labelSessionGuid.Location = new Point(9, 434);
    this.labelSessionGuid.Margin = new Padding(4, 0, 4, 0);
    this.labelSessionGuid.Name = "labelSessionGuid";
    this.labelSessionGuid.Size = new Size(96 /*0x60*/, 17);
    this.labelSessionGuid.TabIndex = 9;
    this.labelSessionGuid.Text = "Session GUID";
    this.buttonDisconnect.Location = new Point(16 /*0x10*/, 212);
    this.buttonDisconnect.Margin = new Padding(4, 4, 4, 4);
    this.buttonDisconnect.Name = "buttonDisconnect";
    this.buttonDisconnect.Size = new Size(183, 31 /*0x1F*/);
    this.buttonDisconnect.TabIndex = 10;
    this.buttonDisconnect.Text = "Disconnect";
    this.buttonDisconnect.UseVisualStyleBackColor = true;
    this.buttonDisconnect.Click += new EventHandler(this.buttonDisconnect_Click);
    this.buttonProgramSdram.Location = new Point(16 /*0x10*/, 174);
    this.buttonProgramSdram.Margin = new Padding(4, 4, 4, 4);
    this.buttonProgramSdram.Name = "buttonProgramSdram";
    this.buttonProgramSdram.Size = new Size(183, 31 /*0x1F*/);
    this.buttonProgramSdram.TabIndex = 11;
    this.buttonProgramSdram.Text = "Program SDRAM";
    this.buttonProgramSdram.UseVisualStyleBackColor = true;
    this.buttonProgramSdram.Click += new EventHandler(this.buttonProgramSdram_Click);
    this.buttonRefresh.Location = new Point(311, 502);
    this.buttonRefresh.Margin = new Padding(4, 4, 4, 4);
    this.buttonRefresh.Name = "buttonRefresh";
    this.buttonRefresh.Size = new Size(145, 31 /*0x1F*/);
    this.buttonRefresh.TabIndex = 12;
    this.buttonRefresh.Text = "Refesh";
    this.buttonRefresh.UseVisualStyleBackColor = true;
    this.buttonRefresh.Click += new EventHandler(this.buttonRefresh_Click);
    this.buttonResetDisconnect.Location = new Point(16 /*0x10*/, 250);
    this.buttonResetDisconnect.Margin = new Padding(4, 4, 4, 4);
    this.buttonResetDisconnect.Name = "buttonResetDisconnect";
    this.buttonResetDisconnect.Size = new Size(183, 31 /*0x1F*/);
    this.buttonResetDisconnect.TabIndex = 13;
    this.buttonResetDisconnect.Text = "Reset and Disconnect";
    this.buttonResetDisconnect.UseVisualStyleBackColor = true;
    this.buttonResetDisconnect.Click += new EventHandler(this.buttonResetDisconnect_Click);
    this.buttonProgramDaughter.Location = new Point(13, 135);
    this.buttonProgramDaughter.Margin = new Padding(4, 4, 4, 4);
    this.buttonProgramDaughter.Name = "buttonProgramDaughter";
    this.buttonProgramDaughter.Size = new Size(185, 31 /*0x1F*/);
    this.buttonProgramDaughter.TabIndex = 14;
    this.buttonProgramDaughter.Text = "Program Daughterboard";
    this.buttonProgramDaughter.UseVisualStyleBackColor = true;
    this.buttonProgramDaughter.Click += new EventHandler(this.buttonProgramDaughter_Click);
    this.buttonGPIO.Location = new Point(16 /*0x10*/, 97);
    this.buttonGPIO.Margin = new Padding(4, 4, 4, 4);
    this.buttonGPIO.Name = "buttonGPIO";
    this.buttonGPIO.Size = new Size(183, 31 /*0x1F*/);
    this.buttonGPIO.TabIndex = 15;
    this.buttonGPIO.Text = "GPIO";
    this.buttonGPIO.UseVisualStyleBackColor = true;
    this.buttonGPIO.Click += new EventHandler(this.buttonGPIO_Click);
    this.AutoScaleDimensions = new SizeF(8f, 16f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(505, 548);
    this.Controls.Add((Control) this.buttonGPIO);
    this.Controls.Add((Control) this.buttonProgramDaughter);
    this.Controls.Add((Control) this.buttonResetDisconnect);
    this.Controls.Add((Control) this.buttonRefresh);
    this.Controls.Add((Control) this.buttonProgramSdram);
    this.Controls.Add((Control) this.buttonDisconnect);
    this.Controls.Add((Control) this.labelSessionGuid);
    this.Controls.Add((Control) this.label2);
    this.Controls.Add((Control) this.textBoxSessionGuid);
    this.Controls.Add((Control) this.textBoxUserGuid);
    this.Controls.Add((Control) this.textBoxBoot);
    this.Controls.Add((Control) this.textBoxInfo);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.textBoxMessage);
    this.Controls.Add((Control) this.buttonFlashLed);
    this.Controls.Add((Control) this.buttonConnect);
    this.Margin = new Padding(4, 4, 4, 4);
    this.Name = nameof (FormAdisBase);
    this.Text = "SDP Control";
    this.Load += new EventHandler(this.FormAdisBase_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
