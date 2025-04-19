namespace Vibration_Evaluation
{
	// Token: 0x02000010 RID: 16
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class DialogUserInteger : global::System.Windows.Forms.Form
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x000077B0 File Offset: 0x000059B0
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

		// Token: 0x060000E7 RID: 231 RVA: 0x00007800 File Offset: 0x00005A00
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Vibration_Evaluation.DialogUserInteger));
			this.TableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.OK_Button = new global::System.Windows.Forms.Button();
			this.Cancel_Button = new global::System.Windows.Forms.Button();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.TextBox1 = new global::System.Windows.Forms.TextBox();
			this.TableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.TableLayoutPanel1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.TableLayoutPanel1.ColumnCount = 2;
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.TableLayoutPanel1.Controls.Add(this.OK_Button, 0, 0);
			this.TableLayoutPanel1.Controls.Add(this.Cancel_Button, 1, 0);
			this.TableLayoutPanel1.Location = new global::System.Drawing.Point(141, 153);
			this.TableLayoutPanel1.Name = "TableLayoutPanel1";
			this.TableLayoutPanel1.RowCount = 1;
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.TableLayoutPanel1.Size = new global::System.Drawing.Size(146, 29);
			this.TableLayoutPanel1.TabIndex = 0;
			this.OK_Button.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.OK_Button.Location = new global::System.Drawing.Point(3, 3);
			this.OK_Button.Name = "OK_Button";
			this.OK_Button.Size = new global::System.Drawing.Size(67, 23);
			this.OK_Button.TabIndex = 0;
			this.OK_Button.Text = "OK";
			this.Cancel_Button.Anchor = global::System.Windows.Forms.AnchorStyles.None;
			this.Cancel_Button.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.Cancel_Button.Location = new global::System.Drawing.Point(76, 3);
			this.Cancel_Button.Name = "Cancel_Button";
			this.Cancel_Button.Size = new global::System.Drawing.Size(67, 23);
			this.Cancel_Button.TabIndex = 1;
			this.Cancel_Button.Text = "Cancel";
			this.Label1.AutoSize = true;
			this.Label1.Location = new global::System.Drawing.Point(50, 22);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(183, 13);
			this.Label1.TabIndex = 1;
			this.Label1.Text = "Enter the Update Interval in seconds.";
			this.Label2.AutoSize = true;
			this.Label2.Location = new global::System.Drawing.Point(50, 45);
			this.Label2.Name = "Label2";
			this.Label2.Size = new global::System.Drawing.Size(220, 13);
			this.Label2.TabIndex = 2;
			this.Label2.Text = "Valid values are integers between 10 and 60.";
			this.TextBox1.Location = new global::System.Drawing.Point(112, 78);
			this.TextBox1.Name = "TextBox1";
			this.TextBox1.Size = new global::System.Drawing.Size(58, 20);
			this.TextBox1.TabIndex = 3;
			base.AcceptButton = this.OK_Button;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.Cancel_Button;
			base.ClientSize = new global::System.Drawing.Size(299, 194);
			base.Controls.Add(this.TextBox1);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.TableLayoutPanel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogUserInteger";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Periodic Mode Set up";
			this.TableLayoutPanel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400005A RID: 90
		private global::System.ComponentModel.IContainer components;
	}
}
