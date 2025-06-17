// Decompiled with JetBrains decompiler
// Type: AdisApi.FormGPIO
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using sdpApi1;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

#nullable disable
namespace AdisApi;

/// <summary>
/// 
/// </summary>
public class FormGPIO : Form
{
  private Gpio gpio;
  private const NumberStyles HexNumber = NumberStyles.HexNumber;
  /// <summary>Required designer variable.</summary>
  private IContainer components = (IContainer) null;
  internal Label Label1;
  internal Button ButtonRead;
  internal Button ButtonWrite;
  internal Button ButtonToggle;
  internal Button ButtonClear;
  internal Button ButtonConfigOut;
  internal Button ButtonConfigIn;
  internal Button ButtonSet;
  internal TextBox TextBoxData;

  /// <summary>
  /// 
  /// 
  /// </summary>
  /// <param name="gpio"></param>
  public FormGPIO(Gpio gpio)
  {
    this.gpio = gpio;
    this.InitializeComponent();
  }

  private void ButtonConfigIn_Click(object sender, EventArgs e)
  {
    byte result;
    if (byte.TryParse(this.TextBoxData.Text, NumberStyles.HexNumber, (IFormatProvider) null, out result))
    {
      this.gpio.configInput(result);
    }
    else
    {
      int num = (int) MessageBox.Show("Invalid Data");
    }
  }

  private void ButtonConfigOut_Click(object sender, EventArgs e)
  {
    byte result;
    if (byte.TryParse(this.TextBoxData.Text, NumberStyles.HexNumber, (IFormatProvider) null, out result))
    {
      this.gpio.configOutput(result);
    }
    else
    {
      int num = (int) MessageBox.Show("Invalid Data");
    }
  }

  private void ButtonSet_Click(object sender, EventArgs e)
  {
    byte result;
    if (byte.TryParse(this.TextBoxData.Text, NumberStyles.HexNumber, (IFormatProvider) null, out result))
    {
      this.gpio.bitSet(result);
    }
    else
    {
      int num = (int) MessageBox.Show("Invalid Data");
    }
  }

  private void ButtonClear_Click(object sender, EventArgs e)
  {
    byte result;
    if (byte.TryParse(this.TextBoxData.Text, NumberStyles.HexNumber, (IFormatProvider) null, out result))
    {
      this.gpio.bitClear(result);
    }
    else
    {
      int num = (int) MessageBox.Show("Invalid Data");
    }
  }

  private void ButtonToggle_Click(object sender, EventArgs e)
  {
    byte result;
    if (byte.TryParse(this.TextBoxData.Text, NumberStyles.HexNumber, (IFormatProvider) null, out result))
    {
      this.gpio.bitToggle(result);
    }
    else
    {
      int num = (int) MessageBox.Show("Invalid Data");
    }
  }

  private void ButtonWrite_Click(object sender, EventArgs e)
  {
    byte result;
    if (byte.TryParse(this.TextBoxData.Text, NumberStyles.HexNumber, (IFormatProvider) null, out result))
    {
      this.gpio.dataWrite(result);
    }
    else
    {
      int num = (int) MessageBox.Show("Invalid Data");
    }
  }

  private void ButtonRead_Click(object sender, EventArgs e)
  {
    byte num;
    this.gpio.dataRead(ref num);
    this.TextBoxData.Text = num.ToString("X2");
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
    this.Label1 = new Label();
    this.ButtonRead = new Button();
    this.ButtonWrite = new Button();
    this.ButtonToggle = new Button();
    this.ButtonClear = new Button();
    this.ButtonConfigOut = new Button();
    this.ButtonConfigIn = new Button();
    this.ButtonSet = new Button();
    this.TextBoxData = new TextBox();
    this.SuspendLayout();
    this.Label1.AutoSize = true;
    this.Label1.Location = new Point(12, 9);
    this.Label1.Name = "Label1";
    this.Label1.Size = new Size(87, 13);
    this.Label1.TabIndex = 17;
    this.Label1.Text = "GPIO mask/data";
    this.ButtonRead.Location = new Point(13, 245);
    this.ButtonRead.Name = "ButtonRead";
    this.ButtonRead.Size = new Size(115, 29);
    this.ButtonRead.TabIndex = 16 /*0x10*/;
    this.ButtonRead.Text = "Read Data";
    this.ButtonRead.UseVisualStyleBackColor = true;
    this.ButtonRead.Click += new EventHandler(this.ButtonRead_Click);
    this.ButtonWrite.Location = new Point(13, 210);
    this.ButtonWrite.Name = "ButtonWrite";
    this.ButtonWrite.Size = new Size(115, 29);
    this.ButtonWrite.TabIndex = 15;
    this.ButtonWrite.Text = "Write Data";
    this.ButtonWrite.UseVisualStyleBackColor = true;
    this.ButtonWrite.Click += new EventHandler(this.ButtonWrite_Click);
    this.ButtonToggle.Location = new Point(12, 175);
    this.ButtonToggle.Name = "ButtonToggle";
    this.ButtonToggle.Size = new Size(115, 29);
    this.ButtonToggle.TabIndex = 14;
    this.ButtonToggle.Text = "Toggle Bits";
    this.ButtonToggle.UseVisualStyleBackColor = true;
    this.ButtonToggle.Click += new EventHandler(this.ButtonToggle_Click);
    this.ButtonClear.Location = new Point(12, 140);
    this.ButtonClear.Name = "ButtonClear";
    this.ButtonClear.Size = new Size(115, 29);
    this.ButtonClear.TabIndex = 13;
    this.ButtonClear.Text = "Clear Bits";
    this.ButtonClear.UseVisualStyleBackColor = true;
    this.ButtonClear.Click += new EventHandler(this.ButtonClear_Click);
    this.ButtonConfigOut.Location = new Point(12, 70);
    this.ButtonConfigOut.Name = "ButtonConfigOut";
    this.ButtonConfigOut.Size = new Size(116, 29);
    this.ButtonConfigOut.TabIndex = 12;
    this.ButtonConfigOut.Text = "Config Output";
    this.ButtonConfigOut.UseVisualStyleBackColor = true;
    this.ButtonConfigOut.Click += new EventHandler(this.ButtonConfigOut_Click);
    this.ButtonConfigIn.Location = new Point(12, 35);
    this.ButtonConfigIn.Name = "ButtonConfigIn";
    this.ButtonConfigIn.Size = new Size(116, 29);
    this.ButtonConfigIn.TabIndex = 11;
    this.ButtonConfigIn.Text = "Config Input";
    this.ButtonConfigIn.UseVisualStyleBackColor = true;
    this.ButtonConfigIn.Click += new EventHandler(this.ButtonConfigIn_Click);
    this.ButtonSet.Location = new Point(12, 105);
    this.ButtonSet.Name = "ButtonSet";
    this.ButtonSet.Size = new Size(115, 29);
    this.ButtonSet.TabIndex = 10;
    this.ButtonSet.Text = "Set Bits";
    this.ButtonSet.UseVisualStyleBackColor = true;
    this.ButtonSet.Click += new EventHandler(this.ButtonSet_Click);
    this.TextBoxData.Location = new Point(102, 9);
    this.TextBoxData.Name = "TextBoxData";
    this.TextBoxData.Size = new Size(26, 20);
    this.TextBoxData.TabIndex = 9;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(145, 282);
    this.Controls.Add((Control) this.Label1);
    this.Controls.Add((Control) this.ButtonRead);
    this.Controls.Add((Control) this.ButtonWrite);
    this.Controls.Add((Control) this.ButtonToggle);
    this.Controls.Add((Control) this.ButtonClear);
    this.Controls.Add((Control) this.ButtonConfigOut);
    this.Controls.Add((Control) this.ButtonConfigIn);
    this.Controls.Add((Control) this.ButtonSet);
    this.Controls.Add((Control) this.TextBoxData);
    this.Name = nameof (FormGPIO);
    this.Text = "GPIO";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
