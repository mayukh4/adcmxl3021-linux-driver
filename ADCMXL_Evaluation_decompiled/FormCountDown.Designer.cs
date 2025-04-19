namespace Vibration_Evaluation
{
	// Token: 0x02000013 RID: 19
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormCountDown : global::System.Windows.Forms.Form
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00009BE4 File Offset: 0x00007DE4
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

		// Token: 0x0600012C RID: 300 RVA: 0x00009C34 File Offset: 0x00007E34
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.Timer1 = new global::System.Windows.Forms.Timer(this.components);
			base.SuspendLayout();
			this.Label1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.Label1.Location = new global::System.Drawing.Point(0, 0);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(272, 193);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Label1";
			this.Label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(272, 193);
			base.ControlBox = false;
			base.Controls.Add(this.Label1);
			base.Name = "FormCountDown";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormCountDown";
			base.ResumeLayout(false);
		}

		// Token: 0x04000078 RID: 120
		private global::System.ComponentModel.IContainer components;
	}
}
