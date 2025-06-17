// Decompiled with JetBrains decompiler
// Type: AdisApi.FormDaughter
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

/// <summary>
/// 
/// </summary>
public class FormDaughter : Form
{
  private AdisBase sdp;
  private SdpDaughter dbm;
  /// <summary>Required designer variable.</summary>
  private IContainer components = (IContainer) null;
  internal ComboBox comboBoxFlash;
  internal CheckBox CheckBoxReconn;
  internal CheckBox CheckBoxReboot;
  internal Button ButtonSpiFlash;
  internal Button ButtonWriteEEPROM;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="sdp"></param>
  public FormDaughter(AdisBase sdp)
  {
    this.sdp = sdp;
    this.InitializeComponent();
  }

  private void FormDaughter_Load(object sender, EventArgs e)
  {
    this.dbm = new SdpDaughter(this.sdp.Base);
    this.dbm.EepromAddress = (byte) 1;
    this.dbm.Connector = (SdpConnector) 0;
    this.comboBoxFlash.DataSource = (object) Enum.GetNames(typeof (FlashDev));
  }

  private void ButtonSpiFlash_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.Filter = "ldr files (*.ldr)|*.ldr|All files (*.*)|*.*";
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    try
    {
      bool reconnect = this.CheckBoxReboot.Checked;
      bool reboot = this.CheckBoxReconn.Checked;
      FlashDev flashDev = (FlashDev) Enum.Parse(typeof (FlashDev), this.comboBoxFlash.SelectedItem.ToString());
      this.dbm.ProgramSPIFlash(openFileDialog.FileName, reboot, reconnect, flashDev);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
    }
  }

  private void ButtonWriteEEPROM_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    try
    {
      this.dbm.WriteEEPROM(openFileDialog.FileName);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message);
    }
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
    this.comboBoxFlash = new ComboBox();
    this.CheckBoxReconn = new CheckBox();
    this.CheckBoxReboot = new CheckBox();
    this.ButtonSpiFlash = new Button();
    this.ButtonWriteEEPROM = new Button();
    this.SuspendLayout();
    this.comboBoxFlash.FormattingEnabled = true;
    this.comboBoxFlash.Location = new Point(37, 13);
    this.comboBoxFlash.Name = "comboBoxFlash";
    this.comboBoxFlash.Size = new Size(155, 21);
    this.comboBoxFlash.TabIndex = 8;
    this.CheckBoxReconn.AutoSize = true;
    this.CheckBoxReconn.Location = new Point(123, 40);
    this.CheckBoxReconn.Name = "CheckBoxReconn";
    this.CheckBoxReconn.Size = new Size(79, 17);
    this.CheckBoxReconn.TabIndex = 7;
    this.CheckBoxReconn.Text = "Reconnect";
    this.CheckBoxReconn.UseVisualStyleBackColor = true;
    this.CheckBoxReboot.AutoSize = true;
    this.CheckBoxReboot.Location = new Point(35, 40);
    this.CheckBoxReboot.Name = "CheckBoxReboot";
    this.CheckBoxReboot.Size = new Size(61, 17);
    this.CheckBoxReboot.TabIndex = 6;
    this.CheckBoxReboot.Text = "Reboot";
    this.CheckBoxReboot.UseVisualStyleBackColor = true;
    this.ButtonSpiFlash.Location = new Point(37, 63 /*0x3F*/);
    this.ButtonSpiFlash.Name = "ButtonSpiFlash";
    this.ButtonSpiFlash.Size = new Size(155, 34);
    this.ButtonSpiFlash.TabIndex = 5;
    this.ButtonSpiFlash.Text = "Program SPI Flash";
    this.ButtonSpiFlash.UseVisualStyleBackColor = true;
    this.ButtonSpiFlash.Click += new EventHandler(this.ButtonSpiFlash_Click);
    this.ButtonWriteEEPROM.Location = new Point(37, 117);
    this.ButtonWriteEEPROM.Name = "ButtonWriteEEPROM";
    this.ButtonWriteEEPROM.Size = new Size(155, 34);
    this.ButtonWriteEEPROM.TabIndex = 9;
    this.ButtonWriteEEPROM.Text = "Write I2C EEPROM";
    this.ButtonWriteEEPROM.UseVisualStyleBackColor = true;
    this.ButtonWriteEEPROM.Click += new EventHandler(this.ButtonWriteEEPROM_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(228, 169);
    this.Controls.Add((Control) this.ButtonWriteEEPROM);
    this.Controls.Add((Control) this.comboBoxFlash);
    this.Controls.Add((Control) this.CheckBoxReconn);
    this.Controls.Add((Control) this.CheckBoxReboot);
    this.Controls.Add((Control) this.ButtonSpiFlash);
    this.Name = nameof (FormDaughter);
    this.Text = "SDP Daughter Board";
    this.Load += new EventHandler(this.FormDaughter_Load);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
