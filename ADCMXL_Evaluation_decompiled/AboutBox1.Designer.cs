namespace Vibration_Evaluation
{
	// Token: 0x02000008 RID: 8
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public sealed partial class AboutBox1 : global::System.Windows.Forms.Form
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002718 File Offset: 0x00000918
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

		// Token: 0x0600003A RID: 58 RVA: 0x00002818 File Offset: 0x00000A18
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::Vibration_Evaluation.AboutBox1));
			this.TableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.LabelProductName = new global::System.Windows.Forms.Label();
			this.LabelVersion = new global::System.Windows.Forms.Label();
			this.LabelCopyright = new global::System.Windows.Forms.Label();
			this.LabelCompanyName = new global::System.Windows.Forms.Label();
			this.OKButton = new global::System.Windows.Forms.Button();
			this.TableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.TableLayoutPanel.ColumnCount = 2;
			this.TableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.TableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.TableLayoutPanel.Controls.Add(this.LabelProductName, 1, 1);
			this.TableLayoutPanel.Controls.Add(this.LabelVersion, 1, 2);
			this.TableLayoutPanel.Controls.Add(this.LabelCopyright, 1, 3);
			this.TableLayoutPanel.Controls.Add(this.LabelCompanyName, 1, 4);
			this.TableLayoutPanel.Controls.Add(this.OKButton, 1, 5);
			this.TableLayoutPanel.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.TableLayoutPanel.Location = new global::System.Drawing.Point(9, 9);
			this.TableLayoutPanel.Name = "TableLayoutPanel";
			this.TableLayoutPanel.RowCount = 6;
			this.TableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 16.66667f));
			this.TableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 16.66667f));
			this.TableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 16.66667f));
			this.TableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 16.66667f));
			this.TableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 16.66667f));
			this.TableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 16.66667f));
			this.TableLayoutPanel.Size = new global::System.Drawing.Size(310, 179);
			this.TableLayoutPanel.TabIndex = 0;
			this.LabelProductName.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.LabelProductName.Location = new global::System.Drawing.Point(26, 29);
			this.LabelProductName.Margin = new global::System.Windows.Forms.Padding(6, 0, 3, 0);
			this.LabelProductName.MaximumSize = new global::System.Drawing.Size(0, 17);
			this.LabelProductName.Name = "LabelProductName";
			this.LabelProductName.Size = new global::System.Drawing.Size(281, 17);
			this.LabelProductName.TabIndex = 0;
			this.LabelProductName.Text = "Product Name";
			this.LabelProductName.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.LabelVersion.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.LabelVersion.Location = new global::System.Drawing.Point(26, 58);
			this.LabelVersion.Margin = new global::System.Windows.Forms.Padding(6, 0, 3, 0);
			this.LabelVersion.MaximumSize = new global::System.Drawing.Size(0, 17);
			this.LabelVersion.Name = "LabelVersion";
			this.LabelVersion.Size = new global::System.Drawing.Size(281, 17);
			this.LabelVersion.TabIndex = 0;
			this.LabelVersion.Text = "Version";
			this.LabelVersion.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.LabelCopyright.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.LabelCopyright.Location = new global::System.Drawing.Point(26, 87);
			this.LabelCopyright.Margin = new global::System.Windows.Forms.Padding(6, 0, 3, 0);
			this.LabelCopyright.MaximumSize = new global::System.Drawing.Size(0, 17);
			this.LabelCopyright.Name = "LabelCopyright";
			this.LabelCopyright.Size = new global::System.Drawing.Size(281, 17);
			this.LabelCopyright.TabIndex = 0;
			this.LabelCopyright.Text = "Copyright";
			this.LabelCopyright.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.LabelCompanyName.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.LabelCompanyName.Location = new global::System.Drawing.Point(26, 116);
			this.LabelCompanyName.Margin = new global::System.Windows.Forms.Padding(6, 0, 3, 0);
			this.LabelCompanyName.MaximumSize = new global::System.Drawing.Size(0, 17);
			this.LabelCompanyName.Name = "LabelCompanyName";
			this.LabelCompanyName.Size = new global::System.Drawing.Size(281, 17);
			this.LabelCompanyName.TabIndex = 0;
			this.LabelCompanyName.Text = "Company Name";
			this.LabelCompanyName.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.OKButton.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.OKButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.OKButton.Location = new global::System.Drawing.Point(232, 153);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new global::System.Drawing.Size(75, 23);
			this.OKButton.TabIndex = 0;
			this.OKButton.Text = "&OK";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.OKButton;
			base.ClientSize = new global::System.Drawing.Size(328, 197);
			base.Controls.Add(this.TableLayoutPanel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "AboutBox1";
			base.Padding = new global::System.Windows.Forms.Padding(9);
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AboutBox1";
			this.TableLayoutPanel.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000011 RID: 17
		private global::System.ComponentModel.IContainer components;
	}
}
