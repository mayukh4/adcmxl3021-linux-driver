namespace Vibration_Evaluation
{
	// Token: 0x02000014 RID: 20
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormDataLogBurst : global::System.Windows.Forms.Form
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00009ED4 File Offset: 0x000080D4
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

		// Token: 0x06000135 RID: 309 RVA: 0x00009F24 File Offset: 0x00008124
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Vibration_Evaluation.FormDataLogBurst));
			this.ButtonStart = new global::System.Windows.Forms.Button();
			this.ButtonFolderDialog = new global::System.Windows.Forms.Button();
			this.txtFileNameAndPath = new global::System.Windows.Forms.TextBox();
			this.Panel2 = new global::System.Windows.Forms.Panel();
			this.ToolTipDirectory = new global::System.Windows.Forms.ToolTip(this.components);
			this.StatusStrip1 = new global::System.Windows.Forms.StatusStrip();
			this.ToolStripStatusLabel1 = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.ProgressBar1 = new global::System.Windows.Forms.ToolStripProgressBar();
			this.ToolStripStatusLabel3 = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.ButtonCancel = new global::System.Windows.Forms.Button();
			this.SaveFileDialog1 = new global::System.Windows.Forms.SaveFileDialog();
			this.Label4 = new global::System.Windows.Forms.Label();
			this.Label6 = new global::System.Windows.Forms.Label();
			this.TextBoxCapTime = new global::System.Windows.Forms.TextBox();
			this.Panel1 = new global::System.Windows.Forms.Panel();
			this.Label5 = new global::System.Windows.Forms.Label();
			this.TextBoxLinesPerFile = new global::System.Windows.Forms.TextBox();
			this.Label3 = new global::System.Windows.Forms.Label();
			this.TextBoxRecLength = new global::System.Windows.Forms.TextBox();
			this.ToolTipCapTime = new global::System.Windows.Forms.ToolTip(this.components);
			this.ToolTipRecordLength = new global::System.Windows.Forms.ToolTip(this.components);
			this.CheckBoxExtTrig = new global::System.Windows.Forms.CheckBox();
			this.LabelNOTE = new global::System.Windows.Forms.Label();
			this.StatusStrip1.SuspendLayout();
			this.Panel1.SuspendLayout();
			base.SuspendLayout();
			this.ButtonStart.Location = new global::System.Drawing.Point(149, 302);
			this.ButtonStart.Name = "ButtonStart";
			this.ButtonStart.Size = new global::System.Drawing.Size(70, 28);
			this.ButtonStart.TabIndex = 2;
			this.ButtonStart.Text = "START";
			this.ButtonStart.UseVisualStyleBackColor = true;
			this.ButtonFolderDialog.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.ButtonFolderDialog.Location = new global::System.Drawing.Point(454, 92);
			this.ButtonFolderDialog.Name = "ButtonFolderDialog";
			this.ButtonFolderDialog.Size = new global::System.Drawing.Size(54, 24);
			this.ButtonFolderDialog.TabIndex = 18;
			this.ButtonFolderDialog.Text = "Browse";
			this.ButtonFolderDialog.UseVisualStyleBackColor = true;
			this.txtFileNameAndPath.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFileNameAndPath.Location = new global::System.Drawing.Point(32, 96);
			this.txtFileNameAndPath.Name = "txtFileNameAndPath";
			this.txtFileNameAndPath.Size = new global::System.Drawing.Size(407, 20);
			this.txtFileNameAndPath.TabIndex = 20;
			this.Panel2.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel2.Location = new global::System.Drawing.Point(19, 80);
			this.Panel2.Name = "Panel2";
			this.Panel2.Size = new global::System.Drawing.Size(507, 55);
			this.Panel2.TabIndex = 10;
			this.StatusStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.ToolStripStatusLabel1,
				this.ProgressBar1,
				this.ToolStripStatusLabel3
			});
			this.StatusStrip1.Location = new global::System.Drawing.Point(0, 371);
			this.StatusStrip1.Name = "StatusStrip1";
			this.StatusStrip1.Size = new global::System.Drawing.Size(549, 22);
			this.StatusStrip1.TabIndex = 11;
			this.StatusStrip1.Text = "StatusStrip1";
			this.ToolStripStatusLabel1.AutoSize = false;
			this.ToolStripStatusLabel1.Margin = new global::System.Windows.Forms.Padding(5, 3, 0, 2);
			this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
			this.ToolStripStatusLabel1.Size = new global::System.Drawing.Size(80, 17);
			this.ToolStripStatusLabel1.Text = "Ready";
			this.ProgressBar1.AutoSize = false;
			this.ProgressBar1.BackColor = global::System.Drawing.SystemColors.Control;
			this.ProgressBar1.Margin = new global::System.Windows.Forms.Padding(10, 3, 1, 3);
			this.ProgressBar1.Name = "ProgressBar1";
			this.ProgressBar1.Size = new global::System.Drawing.Size(250, 16);
			this.ToolStripStatusLabel3.AutoSize = false;
			this.ToolStripStatusLabel3.Margin = new global::System.Windows.Forms.Padding(10, 3, 0, 2);
			this.ToolStripStatusLabel3.Name = "ToolStripStatusLabel3";
			this.ToolStripStatusLabel3.Size = new global::System.Drawing.Size(50, 17);
			this.ToolStripStatusLabel3.Text = "0 %";
			this.Label1.AutoSize = true;
			this.Label1.Location = new global::System.Drawing.Point(27, 64);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(117, 13);
			this.Label1.TabIndex = 12;
			this.Label1.Text = "Data Storage Location ";
			this.Label2.AutoSize = true;
			this.Label2.Location = new global::System.Drawing.Point(29, 156);
			this.Label2.Name = "Label2";
			this.Label2.Size = new global::System.Drawing.Size(107, 13);
			this.Label2.TabIndex = 13;
			this.Label2.Text = "Data Record Options";
			this.ButtonCancel.Location = new global::System.Drawing.Point(297, 302);
			this.ButtonCancel.Name = "ButtonCancel";
			this.ButtonCancel.Size = new global::System.Drawing.Size(70, 28);
			this.ButtonCancel.TabIndex = 15;
			this.ButtonCancel.Text = "STOP";
			this.ButtonCancel.UseVisualStyleBackColor = true;
			this.ButtonCancel.Visible = false;
			this.Label4.AutoSize = true;
			this.Label4.Location = new global::System.Drawing.Point(21, 30);
			this.Label4.Name = "Label4";
			this.Label4.Size = new global::System.Drawing.Size(93, 13);
			this.Label4.TabIndex = 7;
			this.Label4.Text = "Number of Frames";
			this.Label6.AutoSize = true;
			this.Label6.Location = new global::System.Drawing.Point(287, 33);
			this.Label6.Name = "Label6";
			this.Label6.Size = new global::System.Drawing.Size(70, 13);
			this.Label6.TabIndex = 14;
			this.Label6.Text = "Capture Time";
			this.TextBoxCapTime.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.TextBoxCapTime.Cursor = global::System.Windows.Forms.Cursors.Arrow;
			this.TextBoxCapTime.Location = new global::System.Drawing.Point(366, 31);
			this.TextBoxCapTime.Name = "TextBoxCapTime";
			this.TextBoxCapTime.ReadOnly = true;
			this.TextBoxCapTime.Size = new global::System.Drawing.Size(83, 20);
			this.TextBoxCapTime.TabIndex = 15;
			this.TextBoxCapTime.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Right;
			this.Panel1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel1.Controls.Add(this.Label5);
			this.Panel1.Controls.Add(this.TextBoxLinesPerFile);
			this.Panel1.Controls.Add(this.Label3);
			this.Panel1.Controls.Add(this.TextBoxRecLength);
			this.Panel1.Controls.Add(this.TextBoxCapTime);
			this.Panel1.Controls.Add(this.Label6);
			this.Panel1.Controls.Add(this.Label4);
			this.Panel1.Location = new global::System.Drawing.Point(19, 178);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new global::System.Drawing.Size(507, 99);
			this.Panel1.TabIndex = 5;
			this.Label5.AutoSize = true;
			this.Label5.Location = new global::System.Drawing.Point(21, 5);
			this.Label5.Name = "Label5";
			this.Label5.Size = new global::System.Drawing.Size(219, 13);
			this.Label5.TabIndex = 23;
			this.Label5.Text = "Each frame contains 32 values for each axis.";
			this.TextBoxLinesPerFile.Location = new global::System.Drawing.Point(122, 60);
			this.TextBoxLinesPerFile.Name = "TextBoxLinesPerFile";
			this.TextBoxLinesPerFile.Size = new global::System.Drawing.Size(117, 20);
			this.TextBoxLinesPerFile.TabIndex = 18;
			this.TextBoxLinesPerFile.Text = "1000000";
			this.Label3.AutoSize = true;
			this.Label3.Location = new global::System.Drawing.Point(21, 63);
			this.Label3.Name = "Label3";
			this.Label3.Size = new global::System.Drawing.Size(70, 13);
			this.Label3.TabIndex = 17;
			this.Label3.Text = "Lines Per File";
			this.TextBoxRecLength.Location = new global::System.Drawing.Point(121, 27);
			this.TextBoxRecLength.Name = "TextBoxRecLength";
			this.TextBoxRecLength.Size = new global::System.Drawing.Size(118, 20);
			this.TextBoxRecLength.TabIndex = 16;
			this.TextBoxRecLength.Text = "10000";
			this.CheckBoxExtTrig.AutoSize = true;
			this.CheckBoxExtTrig.Location = new global::System.Drawing.Point(396, 309);
			this.CheckBoxExtTrig.Name = "CheckBoxExtTrig";
			this.CheckBoxExtTrig.Size = new global::System.Drawing.Size(136, 17);
			this.CheckBoxExtTrig.TabIndex = 21;
			this.CheckBoxExtTrig.Text = "Enable External Trigger";
			this.CheckBoxExtTrig.UseVisualStyleBackColor = true;
			this.CheckBoxExtTrig.Visible = false;
			this.LabelNOTE.AutoSize = true;
			this.LabelNOTE.Location = new global::System.Drawing.Point(29, 9);
			this.LabelNOTE.Name = "LabelNOTE";
			this.LabelNOTE.Size = new global::System.Drawing.Size(63, 13);
			this.LabelNOTE.TabIndex = 22;
			this.LabelNOTE.Text = "LabelNOTE";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(549, 393);
			base.Controls.Add(this.LabelNOTE);
			base.Controls.Add(this.CheckBoxExtTrig);
			base.Controls.Add(this.ButtonFolderDialog);
			base.Controls.Add(this.txtFileNameAndPath);
			base.Controls.Add(this.ButtonCancel);
			base.Controls.Add(this.Label2);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.StatusStrip1);
			base.Controls.Add(this.Panel1);
			base.Controls.Add(this.Panel2);
			base.Controls.Add(this.ButtonStart);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "FormDataLogBurst";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Burst Data Capture";
			this.StatusStrip1.ResumeLayout(false);
			this.StatusStrip1.PerformLayout();
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400007D RID: 125
		private global::System.ComponentModel.IContainer components;
	}
}
