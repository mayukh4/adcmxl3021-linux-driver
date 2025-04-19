namespace Vibration_Evaluation
{
	// Token: 0x02000018 RID: 24
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormMessage : global::System.Windows.Forms.Form
	{
		// Token: 0x06000294 RID: 660 RVA: 0x00014BBC File Offset: 0x00012DBC
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

		// Token: 0x06000295 RID: 661 RVA: 0x00014C0C File Offset: 0x00012E0C
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Vibration_Evaluation.FormMessage));
			this.Label1 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.Label1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.Label1.Location = new global::System.Drawing.Point(0, 0);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(297, 78);
			this.Label1.TabIndex = 0;
			this.Label1.Text = "Label1";
			this.Label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(297, 78);
			base.ControlBox = false;
			base.Controls.Add(this.Label1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormMessage";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Message";
			base.ResumeLayout(false);
		}

		// Token: 0x04000125 RID: 293
		private global::System.ComponentModel.IContainer components;
	}
}
