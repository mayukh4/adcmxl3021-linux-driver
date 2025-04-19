namespace Vibration_Evaluation
{
	// Token: 0x02000011 RID: 17
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormAlarmValues : global::System.Windows.Forms.Form
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00007F38 File Offset: 0x00006138
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

		// Token: 0x060000FA RID: 250 RVA: 0x00007F88 File Offset: 0x00006188
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new global::System.Windows.Forms.DataGridViewCellStyle();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Vibration_Evaluation.FormAlarmValues));
			this.DataGridView1 = new global::System.Windows.Forms.DataGridView();
			this.ButtonReadDut = new global::System.Windows.Forms.Button();
			this.ButtonWriteDUT = new global::System.Windows.Forms.Button();
			this.ButtonReadFile = new global::System.Windows.Forms.Button();
			this.ButtonWriteFile = new global::System.Windows.Forms.Button();
			this.lblBusy = new global::System.Windows.Forms.Label();
			this.ButtonReadDefault = new global::System.Windows.Forms.Button();
			this.Panel1 = new global::System.Windows.Forms.Panel();
			this.Panel2 = new global::System.Windows.Forms.Panel();
			this.Label1 = new global::System.Windows.Forms.Label();
			((global::System.ComponentModel.ISupportInitialize)this.DataGridView1).BeginInit();
			this.Panel1.SuspendLayout();
			this.Panel2.SuspendLayout();
			base.SuspendLayout();
			this.DataGridView1.AllowUserToAddRows = false;
			this.DataGridView1.AllowUserToDeleteRows = false;
			this.DataGridView1.AllowUserToResizeColumns = false;
			this.DataGridView1.AllowUserToResizeRows = false;
			this.DataGridView1.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dataGridViewCellStyle.Alignment = global::System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle.BackColor = global::System.Drawing.SystemColors.Window;
			dataGridViewCellStyle.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			dataGridViewCellStyle.ForeColor = global::System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle.SelectionBackColor = global::System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle.SelectionForeColor = global::System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle.WrapMode = global::System.Windows.Forms.DataGridViewTriState.False;
			this.DataGridView1.DefaultCellStyle = dataGridViewCellStyle;
			this.DataGridView1.Location = new global::System.Drawing.Point(23, 22);
			this.DataGridView1.Name = "DataGridView1";
			this.DataGridView1.RowHeadersVisible = false;
			dataGridViewCellStyle2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
			this.DataGridView1.Size = new global::System.Drawing.Size(571, 607);
			this.DataGridView1.TabIndex = 0;
			this.ButtonReadDut.Location = new global::System.Drawing.Point(9, 17);
			this.ButtonReadDut.Name = "ButtonReadDut";
			this.ButtonReadDut.Size = new global::System.Drawing.Size(118, 24);
			this.ButtonReadDut.TabIndex = 1;
			this.ButtonReadDut.Text = "Read From DUT";
			this.ButtonReadDut.UseVisualStyleBackColor = true;
			this.ButtonWriteDUT.Location = new global::System.Drawing.Point(9, 58);
			this.ButtonWriteDUT.Name = "ButtonWriteDUT";
			this.ButtonWriteDUT.Size = new global::System.Drawing.Size(118, 24);
			this.ButtonWriteDUT.TabIndex = 2;
			this.ButtonWriteDUT.Text = "Write to DUT";
			this.ButtonWriteDUT.UseVisualStyleBackColor = true;
			this.ButtonReadFile.Location = new global::System.Drawing.Point(7, 17);
			this.ButtonReadFile.Name = "ButtonReadFile";
			this.ButtonReadFile.Size = new global::System.Drawing.Size(118, 23);
			this.ButtonReadFile.TabIndex = 3;
			this.ButtonReadFile.Text = "Read From File";
			this.ButtonReadFile.UseVisualStyleBackColor = true;
			this.ButtonWriteFile.Location = new global::System.Drawing.Point(7, 58);
			this.ButtonWriteFile.Name = "ButtonWriteFile";
			this.ButtonWriteFile.Size = new global::System.Drawing.Size(118, 23);
			this.ButtonWriteFile.TabIndex = 4;
			this.ButtonWriteFile.Text = "Write to File";
			this.ButtonWriteFile.UseVisualStyleBackColor = true;
			this.lblBusy.AutoSize = true;
			this.lblBusy.BackColor = global::System.Drawing.Color.Yellow;
			this.lblBusy.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblBusy.Location = new global::System.Drawing.Point(266, 4);
			this.lblBusy.Name = "lblBusy";
			this.lblBusy.Size = new global::System.Drawing.Size(64, 15);
			this.lblBusy.TabIndex = 6;
			this.lblBusy.Text = "Comm Busy";
			this.ButtonReadDefault.Location = new global::System.Drawing.Point(631, 26);
			this.ButtonReadDefault.Name = "ButtonReadDefault";
			this.ButtonReadDefault.Size = new global::System.Drawing.Size(118, 23);
			this.ButtonReadDefault.TabIndex = 7;
			this.ButtonReadDefault.Text = "Read Default Values";
			this.ButtonReadDefault.UseVisualStyleBackColor = true;
			this.Panel1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel1.Controls.Add(this.ButtonWriteFile);
			this.Panel1.Controls.Add(this.ButtonReadFile);
			this.Panel1.Location = new global::System.Drawing.Point(622, 216);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new global::System.Drawing.Size(136, 102);
			this.Panel1.TabIndex = 9;
			this.Panel2.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel2.Controls.Add(this.ButtonReadDut);
			this.Panel2.Controls.Add(this.ButtonWriteDUT);
			this.Panel2.Location = new global::System.Drawing.Point(622, 79);
			this.Panel2.Name = "Panel2";
			this.Panel2.Size = new global::System.Drawing.Size(136, 102);
			this.Panel2.TabIndex = 10;
			this.Label1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.Label1.Location = new global::System.Drawing.Point(622, 364);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(136, 186);
			this.Label1.TabIndex = 11;
			this.Label1.Text = "Label1";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(801, 653);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.Panel2);
			base.Controls.Add(this.Panel1);
			base.Controls.Add(this.ButtonReadDefault);
			base.Controls.Add(this.lblBusy);
			base.Controls.Add(this.DataGridView1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "FormAlarmValues";
			this.Text = "Alarm Values";
			((global::System.ComponentModel.ISupportInitialize)this.DataGridView1).EndInit();
			this.Panel1.ResumeLayout(false);
			this.Panel2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000064 RID: 100
		private global::System.ComponentModel.IContainer components;
	}
}
