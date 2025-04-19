namespace Vibration_Evaluation
{
	/// <summary>
	/// Sets the Global Variable BoardID
	/// </summary>
	// Token: 0x0200001B RID: 27
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormUSBConnect : global::System.Windows.Forms.Form
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x00015D70 File Offset: 0x00013F70
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

		// Token: 0x060002C5 RID: 709 RVA: 0x00015DC0 File Offset: 0x00013FC0
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Vibration_Evaluation.FormUSBConnect));
			this.GroupBox1 = new global::System.Windows.Forms.GroupBox();
			this.RadioButtonFX3 = new global::System.Windows.Forms.RadioButton();
			this.ButtonConnect = new global::System.Windows.Forms.Button();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.LabelClose = new global::System.Windows.Forms.Label();
			this.GroupBox1.SuspendLayout();
			base.SuspendLayout();
			this.GroupBox1.Controls.Add(this.RadioButtonFX3);
			this.GroupBox1.Location = new global::System.Drawing.Point(12, 26);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new global::System.Drawing.Size(266, 74);
			this.GroupBox1.TabIndex = 0;
			this.GroupBox1.TabStop = false;
			this.RadioButtonFX3.AutoSize = true;
			this.RadioButtonFX3.Checked = true;
			this.RadioButtonFX3.Location = new global::System.Drawing.Point(69, 19);
			this.RadioButtonFX3.Name = "RadioButtonFX3";
			this.RadioButtonFX3.Size = new global::System.Drawing.Size(155, 17);
			this.RadioButtonFX3.TabIndex = 0;
			this.RadioButtonFX3.TabStop = true;
			this.RadioButtonFX3.Text = "FX3 Board NOT connected";
			this.RadioButtonFX3.UseVisualStyleBackColor = true;
			this.ButtonConnect.Location = new global::System.Drawing.Point(105, 108);
			this.ButtonConnect.Name = "ButtonConnect";
			this.ButtonConnect.Size = new global::System.Drawing.Size(103, 26);
			this.ButtonConnect.TabIndex = 1;
			this.ButtonConnect.Text = "Connect";
			this.ButtonConnect.UseVisualStyleBackColor = true;
			this.Label1.AutoSize = true;
			this.Label1.Location = new global::System.Drawing.Point(54, 5);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(212, 13);
			this.Label1.TabIndex = 2;
			this.Label1.Text = "Please select the USB board you are using.";
			this.LabelClose.AutoSize = true;
			this.LabelClose.Location = new global::System.Drawing.Point(79, 149);
			this.LabelClose.Name = "LabelClose";
			this.LabelClose.Size = new global::System.Drawing.Size(155, 13);
			this.LabelClose.TabIndex = 3;
			this.LabelClose.Text = "Close this windows to continue.";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(318, 178);
			base.Controls.Add(this.LabelClose);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.ButtonConnect);
			base.Controls.Add(this.GroupBox1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "FormUSBConnect";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "USB Connection";
			this.GroupBox1.ResumeLayout(false);
			this.GroupBox1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400013C RID: 316
		private global::System.ComponentModel.IContainer components;
	}
}
