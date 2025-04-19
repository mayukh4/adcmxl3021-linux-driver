namespace Vibration_Evaluation
{
	// Token: 0x0200001C RID: 28
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormUserSec : global::System.Windows.Forms.Form
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x000163D8 File Offset: 0x000145D8
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

		// Token: 0x060002D8 RID: 728 RVA: 0x00016428 File Offset: 0x00014628
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.TextBox1 = new global::System.Windows.Forms.TextBox();
			this.ButtonStart = new global::System.Windows.Forms.Button();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.Label3 = new global::System.Windows.Forms.Label();
			this.ButtonCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.TextBox1.Location = new global::System.Drawing.Point(109, 90);
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.Size = new global::System.Drawing.Size(50, 20);
			this.TextBox1.TabIndex = 0;
			this.ButtonStart.Location = new global::System.Drawing.Point(61, 134);
			this.ButtonStart.Name = "ButtonStart";
			this.ButtonStart.Size = new global::System.Drawing.Size(46, 22);
			this.ButtonStart.TabIndex = 1;
			this.ButtonStart.Text = "Start";
			this.ButtonStart.UseVisualStyleBackColor = true;
			this.Label1.AutoSize = true;
			this.Label1.Location = new global::System.Drawing.Point(24, 7);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(159, 13);
			this.Label1.TabIndex = 2;
			this.Label1.Text = "Press the escape key two times ";
			this.Label2.AutoSize = true;
			this.Label2.Location = new global::System.Drawing.Point(27, 24);
			this.Label2.Name = "Label2";
			this.Label2.Size = new global::System.Drawing.Size(89, 13);
			this.Label2.TabIndex = 3;
			this.Label2.Text = "to stop the demo.";
			this.Label3.AutoSize = true;
			this.Label3.Location = new global::System.Drawing.Point(49, 61);
			this.Label3.Name = "Label3";
			this.Label3.Size = new global::System.Drawing.Size(177, 13);
			this.Label3.TabIndex = 4;
			this.Label3.Text = "Enter an update interval in seconds.";
			this.ButtonCancel.Location = new global::System.Drawing.Point(162, 134);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new global::System.Drawing.Size(64, 22);
			this.ButtonCancel.TabIndex = 5;
			this.ButtonCancel.Text = "Cancel";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(267, 168);
			base.ControlBox = false;
			base.Controls.Add(this.ButtonCancel);
			base.Controls.Add(this.Label3);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.ButtonStart);
			base.Controls.Add(this.TextBox1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormUserSec";
			this.Text = "Periodic Mode Demo";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000142 RID: 322
		private global::System.ComponentModel.IContainer components;
	}
}
