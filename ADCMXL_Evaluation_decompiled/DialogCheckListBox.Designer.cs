namespace Vibration_Evaluation
{
	// Token: 0x0200000F RID: 15
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class DialogCheckListBox : global::System.Windows.Forms.Form
	{
		// Token: 0x060000CF RID: 207 RVA: 0x00006F68 File Offset: 0x00005168
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

		// Token: 0x060000D0 RID: 208 RVA: 0x00006FB8 File Offset: 0x000051B8
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.TableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.OK_Button = new global::System.Windows.Forms.Button();
			this.Cancel_Button = new global::System.Windows.Forms.Button();
			this.LabelPrompt = new global::System.Windows.Forms.Label();
			this.CheckList = new global::System.Windows.Forms.CheckedListBox();
			this.ButtonCheckAll = new global::System.Windows.Forms.Button();
			this.ButtonClearAll = new global::System.Windows.Forms.Button();
			this.TableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.TableLayoutPanel1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.TableLayoutPanel1.ColumnCount = 2;
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.TableLayoutPanel1.Controls.Add(this.OK_Button, 0, 0);
			this.TableLayoutPanel1.Controls.Add(this.Cancel_Button, 1, 0);
			this.TableLayoutPanel1.Location = new global::System.Drawing.Point(99, 213);
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
			this.LabelPrompt.AutoSize = true;
			this.LabelPrompt.Location = new global::System.Drawing.Point(24, 20);
			this.LabelPrompt.Name = "LabelPrompt";
			this.LabelPrompt.Size = new global::System.Drawing.Size(125, 13);
			this.LabelPrompt.TabIndex = 2;
			this.LabelPrompt.Text = "Check Applicable Boxes:";
			this.CheckList.FormattingEnabled = true;
			this.CheckList.Location = new global::System.Drawing.Point(27, 48);
			this.CheckList.Name = "CheckList";
			this.CheckList.Size = new global::System.Drawing.Size(140, 139);
			this.CheckList.TabIndex = 3;
			this.ButtonCheckAll.Location = new global::System.Drawing.Point(182, 48);
			this.ButtonCheckAll.Name = "ButtonCheckAll";
			this.ButtonCheckAll.Size = new global::System.Drawing.Size(63, 30);
			this.ButtonCheckAll.TabIndex = 4;
			this.ButtonCheckAll.Text = "Check All";
			this.ButtonCheckAll.UseVisualStyleBackColor = true;
			this.ButtonClearAll.Location = new global::System.Drawing.Point(182, 84);
			this.ButtonClearAll.Name = "ButtonClearAll";
			this.ButtonClearAll.Size = new global::System.Drawing.Size(63, 30);
			this.ButtonClearAll.TabIndex = 5;
			this.ButtonClearAll.Text = "Clear All";
			this.ButtonClearAll.UseVisualStyleBackColor = true;
			base.AcceptButton = this.OK_Button;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.Cancel_Button;
			base.ClientSize = new global::System.Drawing.Size(257, 254);
			base.Controls.Add(this.ButtonClearAll);
			base.Controls.Add(this.ButtonCheckAll);
			base.Controls.Add(this.CheckList);
			base.Controls.Add(this.LabelPrompt);
			base.Controls.Add(this.TableLayoutPanel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DialogCheckListBox";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Check List";
			this.TableLayoutPanel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000051 RID: 81
		private global::System.ComponentModel.IContainer components;
	}
}
