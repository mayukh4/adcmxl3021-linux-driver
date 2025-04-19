namespace Vibration_Evaluation
{
	// Token: 0x02000019 RID: 25
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormRichText : global::System.Windows.Forms.Form
	{
		// Token: 0x0600029B RID: 667 RVA: 0x00014E70 File Offset: 0x00013070
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

		// Token: 0x0600029C RID: 668 RVA: 0x00014EC0 File Offset: 0x000130C0
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Vibration_Evaluation.FormRichText));
			this.RTBox = new global::System.Windows.Forms.RichTextBox();
			base.SuspendLayout();
			this.RTBox.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.RTBox.Location = new global::System.Drawing.Point(0, 0);
			this.RTBox.Name = "RTBox";
			this.RTBox.Size = new global::System.Drawing.Size(284, 112);
			this.RTBox.TabIndex = 0;
			this.RTBox.Text = "RTbox docked fill";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(284, 112);
			base.Controls.Add(this.RTBox);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "FormRichText";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FormRichText";
			base.ResumeLayout(false);
		}

		// Token: 0x04000127 RID: 295
		private global::System.ComponentModel.IContainer components;
	}
}
