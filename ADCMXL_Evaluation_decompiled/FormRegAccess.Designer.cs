namespace Vibration_Evaluation
{
	// Token: 0x02000015 RID: 21
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormRegAccess : global::System.Windows.Forms.Form
	{
		// Token: 0x0600017F RID: 383 RVA: 0x0000C00C File Offset: 0x0000A20C
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

		// Token: 0x06000180 RID: 384 RVA: 0x0000C05C File Offset: 0x0000A25C
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Vibration_Evaluation.FormRegAccess));
			this.cbxRegType = new global::System.Windows.Forms.ComboBox();
			this.Label1 = new global::System.Windows.Forms.Label();
			this.Label2 = new global::System.Windows.Forms.Label();
			this.ButtonWriteReg = new global::System.Windows.Forms.Button();
			this.dgvRegList = new global::System.Windows.Forms.DataGridView();
			this.txtHex = new global::System.Windows.Forms.TextBox();
			this.Label3 = new global::System.Windows.Forms.Label();
			this.lblSelected = new global::System.Windows.Forms.Label();
			this.Label5 = new global::System.Windows.Forms.Label();
			this.lblCurrentValue = new global::System.Windows.Forms.Label();
			this.ButtonReadAll = new global::System.Windows.Forms.Button();
			this.StatusStrip1 = new global::System.Windows.Forms.StatusStrip();
			this.ToolStripStatusLabel1 = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.ToolStripStatusLabel2 = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.ButtonWriteFile = new global::System.Windows.Forms.Button();
			this.ButtonReadFile = new global::System.Windows.Forms.Button();
			this.LabelCommandPanel = new global::System.Windows.Forms.Label();
			this.TableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.Panel1 = new global::System.Windows.Forms.Panel();
			this.Label4 = new global::System.Windows.Forms.Label();
			this.dgvCommand = new global::System.Windows.Forms.DataGridView();
			this.cbxCmdReg = new global::System.Windows.Forms.ComboBox();
			this.ButtonReadOne = new global::System.Windows.Forms.Button();
			((global::System.ComponentModel.ISupportInitialize)this.dgvRegList).BeginInit();
			this.StatusStrip1.SuspendLayout();
			this.TableLayoutPanel1.SuspendLayout();
			this.Panel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.dgvCommand).BeginInit();
			base.SuspendLayout();
			this.cbxRegType.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxRegType.FormattingEnabled = true;
			this.cbxRegType.Location = new global::System.Drawing.Point(137, 39);
			this.cbxRegType.Margin = new global::System.Windows.Forms.Padding(2);
			this.cbxRegType.Name = "cbxRegType";
			this.cbxRegType.Size = new global::System.Drawing.Size(121, 21);
			this.cbxRegType.TabIndex = 2;
			this.Label1.AutoSize = true;
			this.Label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label1.Location = new global::System.Drawing.Point(13, 42);
			this.Label1.Name = "Label1";
			this.Label1.Size = new global::System.Drawing.Size(108, 13);
			this.Label1.TabIndex = 3;
			this.Label1.Text = "Select a Category";
			this.Label2.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.Label2.AutoSize = true;
			this.Label2.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label2.Location = new global::System.Drawing.Point(3, 11);
			this.Label2.Name = "Label2";
			this.Label2.Size = new global::System.Drawing.Size(91, 13);
			this.Label2.TabIndex = 4;
			this.Label2.Text = "Selected Register";
			this.ButtonWriteReg.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.ButtonWriteReg.Location = new global::System.Drawing.Point(100, 120);
			this.ButtonWriteReg.Name = "ButtonWriteReg";
			this.ButtonWriteReg.Size = new global::System.Drawing.Size(71, 21);
			this.ButtonWriteReg.TabIndex = 7;
			this.ButtonWriteReg.Text = "Write Register";
			this.ButtonWriteReg.UseVisualStyleBackColor = true;
			this.dgvRegList.AllowUserToAddRows = false;
			this.dgvRegList.AllowUserToDeleteRows = false;
			this.dgvRegList.BackgroundColor = global::System.Drawing.Color.White;
			this.dgvRegList.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvRegList.Location = new global::System.Drawing.Point(11, 70);
			this.dgvRegList.Margin = new global::System.Windows.Forms.Padding(2);
			this.dgvRegList.MultiSelect = false;
			this.dgvRegList.Name = "dgvRegList";
			this.dgvRegList.ReadOnly = true;
			this.dgvRegList.RowHeadersVisible = false;
			this.dgvRegList.RowHeadersWidthSizeMode = global::System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dgvRegList.RowTemplate.Height = 24;
			this.dgvRegList.RowTemplate.Resizable = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dgvRegList.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvRegList.Size = new global::System.Drawing.Size(267, 342);
			this.dgvRegList.TabIndex = 2;
			this.txtHex.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.txtHex.Location = new global::System.Drawing.Point(100, 82);
			this.txtHex.Name = "txtHex";
			this.txtHex.Size = new global::System.Drawing.Size(71, 20);
			this.txtHex.TabIndex = 10;
			this.txtHex.TextAlign = global::System.Windows.Forms.HorizontalAlignment.Right;
			this.Label3.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.Label3.AutoSize = true;
			this.Label3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label3.Location = new global::System.Drawing.Point(3, 86);
			this.Label3.Name = "Label3";
			this.Label3.Size = new global::System.Drawing.Size(81, 13);
			this.Label3.TabIndex = 11;
			this.Label3.Text = "New Hex Value";
			this.lblSelected.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.lblSelected.BackColor = global::System.Drawing.Color.White;
			this.lblSelected.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblSelected.Location = new global::System.Drawing.Point(100, 11);
			this.lblSelected.Name = "lblSelected";
			this.lblSelected.Size = new global::System.Drawing.Size(71, 14);
			this.lblSelected.TabIndex = 14;
			this.lblSelected.Text = "lblSelectedReg";
			this.lblSelected.TextAlign = global::System.Drawing.ContentAlignment.TopCenter;
			this.Label5.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.Label5.AutoSize = true;
			this.Label5.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label5.Location = new global::System.Drawing.Point(3, 42);
			this.Label5.Name = "Label5";
			this.Label5.Size = new global::System.Drawing.Size(66, 26);
			this.Label5.TabIndex = 15;
			this.Label5.Text = "Current Hex Value";
			this.lblCurrentValue.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.lblCurrentValue.BackColor = global::System.Drawing.Color.White;
			this.lblCurrentValue.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblCurrentValue.Location = new global::System.Drawing.Point(100, 48);
			this.lblCurrentValue.Name = "lblCurrentValue";
			this.lblCurrentValue.Size = new global::System.Drawing.Size(71, 14);
			this.lblCurrentValue.TabIndex = 16;
			this.lblCurrentValue.Text = "lblCurrentValue";
			this.lblCurrentValue.TextAlign = global::System.Drawing.ContentAlignment.TopRight;
			this.ButtonReadAll.Location = new global::System.Drawing.Point(306, 299);
			this.ButtonReadAll.Name = "ButtonReadAll";
			this.ButtonReadAll.Size = new global::System.Drawing.Size(195, 21);
			this.ButtonReadAll.TabIndex = 17;
			this.ButtonReadAll.Text = "Update Registers in Category";
			this.ButtonReadAll.UseVisualStyleBackColor = true;
			this.StatusStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.ToolStripStatusLabel1,
				this.ToolStripStatusLabel2
			});
			this.StatusStrip1.Location = new global::System.Drawing.Point(0, 444);
			this.StatusStrip1.Name = "StatusStrip1";
			this.StatusStrip1.Size = new global::System.Drawing.Size(842, 22);
			this.StatusStrip1.TabIndex = 19;
			this.StatusStrip1.Text = "StatusStrip1";
			this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
			this.ToolStripStatusLabel1.Size = new global::System.Drawing.Size(121, 17);
			this.ToolStripStatusLabel1.Text = "ToolStripStatusLabel1";
			this.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2";
			this.ToolStripStatusLabel2.Size = new global::System.Drawing.Size(121, 17);
			this.ToolStripStatusLabel2.Text = "ToolStripStatusLabel2";
			this.ButtonWriteFile.Location = new global::System.Drawing.Point(306, 354);
			this.ButtonWriteFile.Name = "ButtonWriteFile";
			this.ButtonWriteFile.Size = new global::System.Drawing.Size(195, 21);
			this.ButtonWriteFile.TabIndex = 20;
			this.ButtonWriteFile.Text = "Save Reg Settings to File...";
			this.ButtonWriteFile.UseVisualStyleBackColor = true;
			this.ButtonReadFile.Location = new global::System.Drawing.Point(306, 391);
			this.ButtonReadFile.Name = "ButtonReadFile";
			this.ButtonReadFile.Size = new global::System.Drawing.Size(195, 21);
			this.ButtonReadFile.TabIndex = 21;
			this.ButtonReadFile.Text = "Load Reg Settings from File...";
			this.ButtonReadFile.UseVisualStyleBackColor = true;
			this.LabelCommandPanel.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.LabelCommandPanel.Location = new global::System.Drawing.Point(549, 42);
			this.LabelCommandPanel.Name = "LabelCommandPanel";
			this.LabelCommandPanel.Size = new global::System.Drawing.Size(103, 18);
			this.LabelCommandPanel.TabIndex = 23;
			this.LabelCommandPanel.Text = "Select Register";
			this.TableLayoutPanel1.Anchor = global::System.Windows.Forms.AnchorStyles.Left;
			this.TableLayoutPanel1.ColumnCount = 2;
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 48.3871f));
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 51.6129f));
			this.TableLayoutPanel1.Controls.Add(this.Label2, 0, 0);
			this.TableLayoutPanel1.Controls.Add(this.lblSelected, 1, 0);
			this.TableLayoutPanel1.Controls.Add(this.Label5, 0, 1);
			this.TableLayoutPanel1.Controls.Add(this.txtHex, 1, 2);
			this.TableLayoutPanel1.Controls.Add(this.lblCurrentValue, 1, 1);
			this.TableLayoutPanel1.Controls.Add(this.ButtonWriteReg, 1, 3);
			this.TableLayoutPanel1.Controls.Add(this.Label3, 0, 2);
			this.TableLayoutPanel1.Location = new global::System.Drawing.Point(6, -1);
			this.TableLayoutPanel1.Name = "TableLayoutPanel1";
			this.TableLayoutPanel1.RowCount = 4;
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 48.75f));
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 51.25f));
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 37f));
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 38f));
			this.TableLayoutPanel1.Size = new global::System.Drawing.Size(201, 150);
			this.TableLayoutPanel1.TabIndex = 24;
			this.Panel1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.Panel1.Controls.Add(this.TableLayoutPanel1);
			this.Panel1.Location = new global::System.Drawing.Point(296, 92);
			this.Panel1.Name = "Panel1";
			this.Panel1.Size = new global::System.Drawing.Size(208, 149);
			this.Panel1.TabIndex = 25;
			this.Label4.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.Label4.Location = new global::System.Drawing.Point(314, 70);
			this.Label4.Name = "Label4";
			this.Label4.Size = new global::System.Drawing.Size(179, 19);
			this.Label4.TabIndex = 26;
			this.Label4.Text = "Single Register Write";
			this.Label4.TextAlign = global::System.Drawing.ContentAlignment.TopCenter;
			this.dgvCommand.AllowUserToAddRows = false;
			this.dgvCommand.AllowUserToDeleteRows = false;
			this.dgvCommand.BackgroundColor = global::System.Drawing.Color.White;
			this.dgvCommand.ColumnHeadersHeightSizeMode = global::System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvCommand.Location = new global::System.Drawing.Point(522, 70);
			this.dgvCommand.Margin = new global::System.Windows.Forms.Padding(2);
			this.dgvCommand.MultiSelect = false;
			this.dgvCommand.Name = "dgvCommand";
			this.dgvCommand.ReadOnly = true;
			this.dgvCommand.RowHeadersVisible = false;
			this.dgvCommand.RowHeadersWidthSizeMode = global::System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
			this.dgvCommand.RowTemplate.Height = 24;
			this.dgvCommand.RowTemplate.Resizable = global::System.Windows.Forms.DataGridViewTriState.False;
			this.dgvCommand.SelectionMode = global::System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvCommand.Size = new global::System.Drawing.Size(306, 342);
			this.dgvCommand.TabIndex = 27;
			this.cbxCmdReg.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxCmdReg.FormattingEnabled = true;
			this.cbxCmdReg.Location = new global::System.Drawing.Point(671, 39);
			this.cbxCmdReg.Margin = new global::System.Windows.Forms.Padding(2);
			this.cbxCmdReg.Name = "cbxCmdReg";
			this.cbxCmdReg.Size = new global::System.Drawing.Size(122, 21);
			this.cbxCmdReg.TabIndex = 28;
			this.ButtonReadOne.Location = new global::System.Drawing.Point(306, 260);
			this.ButtonReadOne.Name = "ButtonReadOne";
			this.ButtonReadOne.Size = new global::System.Drawing.Size(195, 21);
			this.ButtonReadOne.TabIndex = 29;
			this.ButtonReadOne.Text = "Read Selected Register";
			this.ButtonReadOne.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(842, 466);
			base.Controls.Add(this.ButtonReadOne);
			base.Controls.Add(this.cbxCmdReg);
			base.Controls.Add(this.dgvCommand);
			base.Controls.Add(this.Label4);
			base.Controls.Add(this.LabelCommandPanel);
			base.Controls.Add(this.ButtonReadFile);
			base.Controls.Add(this.ButtonWriteFile);
			base.Controls.Add(this.StatusStrip1);
			base.Controls.Add(this.ButtonReadAll);
			base.Controls.Add(this.dgvRegList);
			base.Controls.Add(this.Label1);
			base.Controls.Add(this.cbxRegType);
			base.Controls.Add(this.Panel1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new global::System.Windows.Forms.Padding(2);
			base.Name = "FormRegAccess";
			this.Text = "Register Access";
			((global::System.ComponentModel.ISupportInitialize)this.dgvRegList).EndInit();
			this.StatusStrip1.ResumeLayout(false);
			this.StatusStrip1.PerformLayout();
			this.TableLayoutPanel1.ResumeLayout(false);
			this.TableLayoutPanel1.PerformLayout();
			this.Panel1.ResumeLayout(false);
			((global::System.ComponentModel.ISupportInitialize)this.dgvCommand).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000A0 RID: 160
		private global::System.ComponentModel.IContainer components;
	}
}
