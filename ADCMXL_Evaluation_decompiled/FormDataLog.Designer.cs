namespace Vibration_Evaluation
{
	// Token: 0x02000017 RID: 23
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormDataLog : global::System.Windows.Forms.Form
	{
		// Token: 0x06000269 RID: 617 RVA: 0x00013CE0 File Offset: 0x00011EE0
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

		// Token: 0x0600026A RID: 618 RVA: 0x00013D30 File Offset: 0x00011F30
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Vibration_Evaluation.FormDataLog));
			this.Panel2 = new global::System.Windows.Forms.Panel();
			this.ButtonResetFullPath = new global::System.Windows.Forms.Button();
			this.ButtonFolderDialog = new global::System.Windows.Forms.Button();
			this.txtDirectory = new global::System.Windows.Forms.TextBox();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.ToolTipDirectory = new global::System.Windows.Forms.ToolTip(this.components);
			this.SaveFileDialog1 = new global::System.Windows.Forms.SaveFileDialog();
			this.ToolTipCapTime = new global::System.Windows.Forms.ToolTip(this.components);
			this.ButtonOK = new global::System.Windows.Forms.Button();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.lblFileCount = new global::System.Windows.Forms.Label();
			this.ButtonCountReSet = new global::System.Windows.Forms.Button();
			this.Label4 = new global::System.Windows.Forms.Label();
			this.Label3 = new global::System.Windows.Forms.Label();
			this.Panel1 = new global::System.Windows.Forms.Panel();
			this.Panel2.SuspendLayout();
			this.Panel1.SuspendLayout();
			base.SuspendLayout();
			this.Panel2.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel2.Controls.Add(this.ButtonResetFullPath);
			this.Panel2.Controls.Add(this.ButtonFolderDialog);
			this.Panel2.Controls.Add(this.txtDirectory);
			this.Panel2.Controls.Add(this.Label1);
			this.Panel2.Location = new global::System.Drawing.Point(19, 26);
			this.Panel2.Name = "Panel2";
			this.Panel2.Size = new global::System.Drawing.Size(507, 91);
			this.Panel2.TabIndex = 10;
			this.ButtonResetFullPath.Location = new global::System.Drawing.Point(202, 53);
			this.ButtonResetFullPath.Name = "ButtonResetFullPath";
			this.ButtonResetFullPath.Size = new global::System.Drawing.Size(110, 20);
			this.ButtonResetFullPath.TabIndex = 23;
			this.ButtonResetFullPath.Text = "Reset to Default";
			this.ButtonResetFullPath.UseVisualStyleBackColor = true;
			this.ButtonFolderDialog.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.ButtonFolderDialog.Location = new global::System.Drawing.Point(439, 23);
			this.ButtonFolderDialog.Name = "ButtonFolderDialog";
			this.ButtonFolderDialog.Size = new global::System.Drawing.Size(54, 24);
			this.ButtonFolderDialog.TabIndex = 21;
			this.ButtonFolderDialog.Text = "Browse";
			this.ButtonFolderDialog.UseVisualStyleBackColor = true;
			this.txtDirectory.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtDirectory.Location = new global::System.Drawing.Point(17, 27);
			this.txtDirectory.Name = "txtDirectory";
			this.txtDirectory.Size = new global::System.Drawing.Size(407, 20);
			this.txtDirectory.TabIndex = 22;
			this.Label1.AutoSize = true;
			this.Label1.Location = new global::System.Drawing.Point(23, 11);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(114, 13);
			this.Label1.TabIndex = 12;
			this.Label1.Text = "Data Storage Location";
			this.ButtonOK.Location = new global::System.Drawing.Point(241, 269);
			this.ButtonOK.Name = "ButtonOK";
			this.ButtonOK.Size = new global::System.Drawing.Size(71, 25);
			this.ButtonOK.TabIndex = 17;
			this.ButtonOK.Text = "OK";
			this.ButtonOK.UseVisualStyleBackColor = true;
			this.Label2.AutoSize = true;
			this.Label2.Location = new global::System.Drawing.Point(143, 73);
			this.Label2.Name = "Label2";
			this.Label2.Size = new global::System.Drawing.Size(63, 13);
			this.Label2.TabIndex = 13;
			this.Label2.Text = "File Count =";
			this.lblFileCount.AutoSize = true;
			this.lblFileCount.Location = new global::System.Drawing.Point(229, 73);
			this.lblFileCount.Name = "lblFileCount";
			this.lblFileCount.Size = new global::System.Drawing.Size(13, 13);
			this.lblFileCount.TabIndex = 14;
			this.lblFileCount.Text = "0";
			this.ButtonCountReSet.Location = new global::System.Drawing.Point(314, 68);
			this.ButtonCountReSet.Name = "ButtonCountReSet";
			this.ButtonCountReSet.Size = new global::System.Drawing.Size(85, 22);
			this.ButtonCountReSet.TabIndex = 15;
			this.ButtonCountReSet.Text = "Reset Count";
			this.ButtonCountReSet.UseVisualStyleBackColor = true;
			this.Label4.AutoSize = true;
			this.Label4.Location = new global::System.Drawing.Point(66, 42);
			this.Label4.Name = "Label4";
			this.Label4.Size = new global::System.Drawing.Size(332, 13);
			this.Label4.TabIndex = 16;
			this.Label4.Text = "File Count will be automatically incremented each time a file is written.";
			this.Label3.AutoSize = true;
			this.Label3.Location = new global::System.Drawing.Point(23, 12);
			this.Label3.Name = "Label3";
			this.Label3.Size = new global::System.Drawing.Size(375, 13);
			this.Label3.TabIndex = 18;
			this.Label3.Text = "File Count will be appended to the specified file name before the file extension.";
			this.Panel1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel1.Controls.Add(this.Label4);
			this.Panel1.Controls.Add(this.lblFileCount);
			this.Panel1.Controls.Add(this.Label2);
			this.Panel1.Controls.Add(this.Label3);
			this.Panel1.Controls.Add(this.ButtonCountReSet);
			this.Panel1.Location = new global::System.Drawing.Point(19, 135);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new global::System.Drawing.Size(507, 103);
			this.Panel1.TabIndex = 19;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(545, 315);
			base.Controls.Add(this.Panel1);
			base.Controls.Add(this.ButtonOK);
			base.Controls.Add(this.Panel2);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MaximizeBox = false;
			base.Name = "FormDataLog";
			this.Text = "Data Capture Configuration";
			this.Panel2.ResumeLayout(false);
			this.Panel2.PerformLayout();
			this.Panel1.ResumeLayout(false);
			this.Panel1.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0400010E RID: 270
		private global::System.ComponentModel.IContainer components;
	}
}
