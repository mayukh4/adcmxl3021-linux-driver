namespace Vibration_Evaluation
{
	// Token: 0x0200001A RID: 26
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormSPI : global::System.Windows.Forms.Form
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x00015120 File Offset: 0x00013320
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

		// Token: 0x060002A4 RID: 676 RVA: 0x00015170 File Offset: 0x00013370
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.TextBoxSclkFreq = new global::System.Windows.Forms.TextBox();
			this.ButtonSclkUpdate = new global::System.Windows.Forms.Button();
			this.TableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.ButtonLED = new global::System.Windows.Forms.Button();
			this.Label6 = new global::System.Windows.Forms.Label();
			this.Label3 = new global::System.Windows.Forms.Label();
			this.LabelCpol = new global::System.Windows.Forms.Label();
			this.Label4 = new global::System.Windows.Forms.Label();
			this.ButtonStallCycleUpdate = new global::System.Windows.Forms.Button();
			this.TextBoxStallCyles = new global::System.Windows.Forms.TextBox();
			this.ButtonSpiReset = new global::System.Windows.Forms.Button();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.TableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.TextBoxSclkFreq.Location = new global::System.Drawing.Point(168, 105);
			this.TextBoxSclkFreq.Name = "TextBoxSclkFreq";
			this.TextBoxSclkFreq.Size = new global::System.Drawing.Size(68, 20);
			this.TextBoxSclkFreq.TabIndex = 1;
			this.ButtonSclkUpdate.Location = new global::System.Drawing.Point(271, 105);
			this.ButtonSclkUpdate.Name = "ButtonSclkUpdate";
			this.ButtonSclkUpdate.Size = new global::System.Drawing.Size(58, 22);
			this.ButtonSclkUpdate.TabIndex = 3;
			this.ButtonSclkUpdate.Text = "Update";
			this.ButtonSclkUpdate.UseVisualStyleBackColor = true;
			this.TableLayoutPanel1.ColumnCount = 3;
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 33.33444f));
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 33.33444f));
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 33.33111f));
			this.TableLayoutPanel1.Controls.Add(this.ButtonLED, 2, 0);
			this.TableLayoutPanel1.Location = new global::System.Drawing.Point(0, 0);
			this.TableLayoutPanel1.Name = "TableLayoutPanel1";
			this.TableLayoutPanel1.RowCount = 1;
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.TableLayoutPanel1.Size = new global::System.Drawing.Size(200, 100);
			this.TableLayoutPanel1.TabIndex = 0;
			this.ButtonLED.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.ButtonLED.Location = new global::System.Drawing.Point(135, 39);
			this.ButtonLED.Name = "ButtonLED";
			this.ButtonLED.Size = new global::System.Drawing.Size(62, 22);
			this.ButtonLED.TabIndex = 4;
			this.ButtonLED.Text = "Blink LED";
			this.ButtonLED.UseVisualStyleBackColor = true;
			this.Label6.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.Label6.AutoSize = true;
			this.Label6.Location = new global::System.Drawing.Point(3, 138);
			this.Label6.Name = "Label6";
			this.Label6.Size = new global::System.Drawing.Size(73, 13);
			this.Label6.TabIndex = 17;
			this.Label6.Text = "Compile Time:";
			this.Label6.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.Label3.AutoSize = true;
			this.Label3.Location = new global::System.Drawing.Point(41, 105);
			this.Label3.Name = "Label3";
			this.Label3.Size = new global::System.Drawing.Size(109, 13);
			this.Label3.TabIndex = 1;
			this.Label3.Text = "SCLK Frequency (Hz)";
			this.LabelCpol.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.LabelCpol.AutoSize = true;
			this.LabelCpol.Location = new global::System.Drawing.Point(121, 123);
			this.LabelCpol.Name = "LabelCpol";
			this.LabelCpol.Size = new global::System.Drawing.Size(35, 13);
			this.LabelCpol.TabIndex = 8;
			this.LabelCpol.Text = "CPOL";
			this.Label4.AutoSize = true;
			this.Label4.Location = new global::System.Drawing.Point(35, 161);
			this.Label4.Name = "Label4";
			this.Label4.Size = new global::System.Drawing.Size(90, 13);
			this.Label4.TabIndex = 10;
			this.Label4.Text = "Stall Cycles (CLK)";
			this.Label4.Visible = false;
			this.ButtonStallCycleUpdate.Location = new global::System.Drawing.Point(265, 158);
			this.ButtonStallCycleUpdate.Name = "ButtonStallCycleUpdate";
			this.ButtonStallCycleUpdate.Size = new global::System.Drawing.Size(58, 22);
			this.ButtonStallCycleUpdate.TabIndex = 11;
			this.ButtonStallCycleUpdate.Text = "Update";
			this.ButtonStallCycleUpdate.UseVisualStyleBackColor = true;
			this.ButtonStallCycleUpdate.Visible = false;
			this.TextBoxStallCyles.Location = new global::System.Drawing.Point(162, 158);
			this.TextBoxStallCyles.Name = "TextBoxStallCyles";
			this.TextBoxStallCyles.Size = new global::System.Drawing.Size(68, 20);
			this.TextBoxStallCyles.TabIndex = 12;
			this.TextBoxStallCyles.Visible = false;
			this.ButtonSpiReset.Enabled = false;
			this.ButtonSpiReset.Location = new global::System.Drawing.Point(215, 173);
			this.ButtonSpiReset.Name = "ButtonSpiReset";
			this.ButtonSpiReset.Size = new global::System.Drawing.Size(138, 30);
			this.ButtonSpiReset.TabIndex = 4;
			this.ButtonSpiReset.Text = "Hardware Reset";
			this.ButtonSpiReset.UseVisualStyleBackColor = true;
			this.ButtonSpiReset.Visible = false;
			this.Label1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label1.Location = new global::System.Drawing.Point(12, 11);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(363, 76);
			this.Label1.TabIndex = 13;
			this.Label1.Text = "The user SPI SCLK value is only applied to Manual FFT and Manual Time capture modes. ";
			this.Label2.AutoSize = true;
			this.Label2.Location = new global::System.Drawing.Point(35, 142);
			this.Label2.Name = "Label2";
			this.Label2.Size = new global::System.Drawing.Size(261, 13);
			this.Label2.TabIndex = 14;
			this.Label2.Text = "This label and Stall Cycle controls  are set NOT visible";
			this.Label2.Visible = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(388, 156);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.TextBoxStallCyles);
			base.Controls.Add(this.Label4);
			base.Controls.Add(this.Label3);
			base.Controls.Add(this.TextBoxSclkFreq);
			base.Controls.Add(this.ButtonSpiReset);
			base.Controls.Add(this.ButtonStallCycleUpdate);
			base.Controls.Add(this.ButtonSclkUpdate);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "FormSPI";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SPI Utilities";
			this.TableLayoutPanel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000129 RID: 297
		private global::System.ComponentModel.IContainer components;
	}
}
