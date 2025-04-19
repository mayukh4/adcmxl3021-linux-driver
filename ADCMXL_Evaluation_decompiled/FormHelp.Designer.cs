namespace Vibration_Evaluation
{
	// Token: 0x0200001E RID: 30
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormHelp : global::System.Windows.Forms.Form
	{
		// Token: 0x06000301 RID: 769 RVA: 0x000177BC File Offset: 0x000159BC
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

		// Token: 0x06000302 RID: 770 RVA: 0x0001780C File Offset: 0x00015A0C
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Vibration_Evaluation.FormHelp));
			this.RichTextBox1 = new global::System.Windows.Forms.RichTextBox();
			base.SuspendLayout();
			this.RichTextBox1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.RichTextBox1.Location = new global::System.Drawing.Point(0, 0);
			this.RichTextBox1.Name = "RichTextBox1";
			this.RichTextBox1.Size = new global::System.Drawing.Size(903, 403);
			this.RichTextBox1.TabIndex = 0;
			this.RichTextBox1.Text = "";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(903, 403);
			base.Controls.Add(this.RichTextBox1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "FormHelp";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HelpForm";
			base.ResumeLayout(false);
		}

		// Token: 0x04000156 RID: 342
		private global::System.ComponentModel.IContainer components;
	}
}
