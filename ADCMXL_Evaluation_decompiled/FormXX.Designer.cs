namespace Vibration_Evaluation
{
	// Token: 0x0200001D RID: 29
	[global::Microsoft.VisualBasic.CompilerServices.DesignerGenerated]
	public partial class FormXX : global::System.Windows.Forms.Form
	{
		// Token: 0x060002E9 RID: 745 RVA: 0x00016A60 File Offset: 0x00014C60
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

		// Token: 0x060002EA RID: 746 RVA: 0x00016AB0 File Offset: 0x00014CB0
		[global::System.Diagnostics.DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.TableLayoutPanel1 = new global::System.Windows.Forms.TableLayoutPanel();
			this.PlotUC1 = new global::Vibration_Evaluation.PlotUC();
			this.PlotUC2 = new global::Vibration_Evaluation.PlotUC();
			this.PlotUC3 = new global::Vibration_Evaluation.PlotUC();
			this.PlotUC4 = new global::Vibration_Evaluation.PlotUC();
			this.PlotUC5 = new global::Vibration_Evaluation.PlotUC();
			this.PlotUC6 = new global::Vibration_Evaluation.PlotUC();
			this.TableLayoutPanel2 = new global::System.Windows.Forms.TableLayoutPanel();
			this.MenuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.MEnuToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.TableLayoutPanel1.SuspendLayout();
			this.TableLayoutPanel2.SuspendLayout();
			this.MenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.TableLayoutPanel1.ColumnCount = 2;
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 49.8062f));
			this.TableLayoutPanel1.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50.1938f));
			this.TableLayoutPanel1.Controls.Add(this.PlotUC1, 0, 0);
			this.TableLayoutPanel1.Controls.Add(this.PlotUC2, 1, 0);
			this.TableLayoutPanel1.Controls.Add(this.PlotUC3, 0, 1);
			this.TableLayoutPanel1.Controls.Add(this.PlotUC4, 1, 1);
			this.TableLayoutPanel1.Controls.Add(this.PlotUC5, 0, 2);
			this.TableLayoutPanel1.Controls.Add(this.PlotUC6, 1, 2);
			this.TableLayoutPanel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.TableLayoutPanel1.Location = new global::System.Drawing.Point(3, 30);
			this.TableLayoutPanel1.Margin = new global::System.Windows.Forms.Padding(3, 30, 3, 3);
			this.TableLayoutPanel1.Name = "TableLayoutPanel1";
			this.TableLayoutPanel1.RowCount = 3;
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.TableLayoutPanel1.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.TableLayoutPanel1.Size = new global::System.Drawing.Size(817, 534);
			this.TableLayoutPanel1.TabIndex = 0;
			this.PlotUC1.barWidth = 2;
			this.PlotUC1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.PlotUC1.chartType = global::Vibration_Evaluation.PlotUC.ChartTypeEnum.bar;
			this.PlotUC1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.PlotUC1.Location = new global::System.Drawing.Point(10, 10);
			this.PlotUC1.Margin = new global::System.Windows.Forms.Padding(10);
			this.PlotUC1.Name = "PlotUC1";
			this.PlotUC1.ScaleType = global::Vibration_Evaluation.PlotUC.ScaleTypeEnum.Signed;
			this.PlotUC1.ScaleUnitsText = null;
			this.PlotUC1.Size = new global::System.Drawing.Size(386, 158);
			this.PlotUC1.TabIndex = 0;
			this.PlotUC1.Title = null;
			this.PlotUC1.Xmax = 700f;
			this.PlotUC1.Xmin = 0f;
			this.PlotUC1.Xoffset = 0f;
			this.PlotUC1.xScaleMax = 1.0;
			this.PlotUC1.Ymax = 60f;
			this.PlotUC1.Ymin = -60f;
			this.PlotUC1.Yoffset = 60f;
			this.PlotUC2.barWidth = 2;
			this.PlotUC2.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.PlotUC2.chartType = global::Vibration_Evaluation.PlotUC.ChartTypeEnum.bar;
			this.PlotUC2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.PlotUC2.Location = new global::System.Drawing.Point(416, 10);
			this.PlotUC2.Margin = new global::System.Windows.Forms.Padding(10);
			this.PlotUC2.Name = "PlotUC2";
			this.PlotUC2.ScaleType = global::Vibration_Evaluation.PlotUC.ScaleTypeEnum.Signed;
			this.PlotUC2.ScaleUnitsText = null;
			this.PlotUC2.Size = new global::System.Drawing.Size(391, 158);
			this.PlotUC2.TabIndex = 1;
			this.PlotUC2.Title = null;
			this.PlotUC2.Xmax = 700f;
			this.PlotUC2.Xmin = 0f;
			this.PlotUC2.Xoffset = 0f;
			this.PlotUC2.xScaleMax = 1.0;
			this.PlotUC2.Ymax = 60f;
			this.PlotUC2.Ymin = -60f;
			this.PlotUC2.Yoffset = 60f;
			this.PlotUC3.barWidth = 2;
			this.PlotUC3.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.PlotUC3.chartType = global::Vibration_Evaluation.PlotUC.ChartTypeEnum.bar;
			this.PlotUC3.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.PlotUC3.Location = new global::System.Drawing.Point(10, 188);
			this.PlotUC3.Margin = new global::System.Windows.Forms.Padding(10);
			this.PlotUC3.Name = "PlotUC3";
			this.PlotUC3.ScaleType = global::Vibration_Evaluation.PlotUC.ScaleTypeEnum.Signed;
			this.PlotUC3.ScaleUnitsText = null;
			this.PlotUC3.Size = new global::System.Drawing.Size(386, 158);
			this.PlotUC3.TabIndex = 2;
			this.PlotUC3.Title = null;
			this.PlotUC3.Xmax = 700f;
			this.PlotUC3.Xmin = 0f;
			this.PlotUC3.Xoffset = 0f;
			this.PlotUC3.xScaleMax = 1.0;
			this.PlotUC3.Ymax = 60f;
			this.PlotUC3.Ymin = -60f;
			this.PlotUC3.Yoffset = 60f;
			this.PlotUC4.barWidth = 2;
			this.PlotUC4.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.PlotUC4.chartType = global::Vibration_Evaluation.PlotUC.ChartTypeEnum.bar;
			this.PlotUC4.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.PlotUC4.Location = new global::System.Drawing.Point(416, 188);
			this.PlotUC4.Margin = new global::System.Windows.Forms.Padding(10);
			this.PlotUC4.Name = "PlotUC4";
			this.PlotUC4.ScaleType = global::Vibration_Evaluation.PlotUC.ScaleTypeEnum.Signed;
			this.PlotUC4.ScaleUnitsText = null;
			this.PlotUC4.Size = new global::System.Drawing.Size(391, 158);
			this.PlotUC4.TabIndex = 3;
			this.PlotUC4.Title = null;
			this.PlotUC4.Xmax = 700f;
			this.PlotUC4.Xmin = 0f;
			this.PlotUC4.Xoffset = 0f;
			this.PlotUC4.xScaleMax = 1.0;
			this.PlotUC4.Ymax = 60f;
			this.PlotUC4.Ymin = -60f;
			this.PlotUC4.Yoffset = 60f;
			this.PlotUC5.barWidth = 2;
			this.PlotUC5.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.PlotUC5.chartType = global::Vibration_Evaluation.PlotUC.ChartTypeEnum.bar;
			this.PlotUC5.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.PlotUC5.Location = new global::System.Drawing.Point(10, 366);
			this.PlotUC5.Margin = new global::System.Windows.Forms.Padding(10);
			this.PlotUC5.Name = "PlotUC5";
			this.PlotUC5.ScaleType = global::Vibration_Evaluation.PlotUC.ScaleTypeEnum.Signed;
			this.PlotUC5.ScaleUnitsText = null;
			this.PlotUC5.Size = new global::System.Drawing.Size(386, 158);
			this.PlotUC5.TabIndex = 4;
			this.PlotUC5.Title = null;
			this.PlotUC5.Xmax = 700f;
			this.PlotUC5.Xmin = 0f;
			this.PlotUC5.Xoffset = 0f;
			this.PlotUC5.xScaleMax = 1.0;
			this.PlotUC5.Ymax = 60f;
			this.PlotUC5.Ymin = -60f;
			this.PlotUC5.Yoffset = 60f;
			this.PlotUC6.barWidth = 2;
			this.PlotUC6.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.PlotUC6.chartType = global::Vibration_Evaluation.PlotUC.ChartTypeEnum.bar;
			this.PlotUC6.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.PlotUC6.Location = new global::System.Drawing.Point(416, 366);
			this.PlotUC6.Margin = new global::System.Windows.Forms.Padding(10);
			this.PlotUC6.Name = "PlotUC6";
			this.PlotUC6.ScaleType = global::Vibration_Evaluation.PlotUC.ScaleTypeEnum.Signed;
			this.PlotUC6.ScaleUnitsText = null;
			this.PlotUC6.Size = new global::System.Drawing.Size(391, 158);
			this.PlotUC6.TabIndex = 5;
			this.PlotUC6.Title = null;
			this.PlotUC6.Xmax = 700f;
			this.PlotUC6.Xmin = 0f;
			this.PlotUC6.Xoffset = 0f;
			this.PlotUC6.xScaleMax = 1.0;
			this.PlotUC6.Ymax = 60f;
			this.PlotUC6.Ymin = -60f;
			this.PlotUC6.Yoffset = 60f;
			this.TableLayoutPanel2.ColumnCount = 1;
			this.TableLayoutPanel2.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.TableLayoutPanel2.Controls.Add(this.TableLayoutPanel1, 0, 0);
			this.TableLayoutPanel2.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.TableLayoutPanel2.Location = new global::System.Drawing.Point(0, 24);
			this.TableLayoutPanel2.Name = "TableLayoutPanel2";
			this.TableLayoutPanel2.RowCount = 1;
			this.TableLayoutPanel2.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.TableLayoutPanel2.Size = new global::System.Drawing.Size(823, 567);
			this.TableLayoutPanel2.TabIndex = 1;
			this.MenuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.MEnuToolStripMenuItem
			});
			this.MenuStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.MenuStrip1.Name = "MenuStrip1";
			this.MenuStrip1.Size = new global::System.Drawing.Size(823, 24);
			this.MenuStrip1.TabIndex = 2;
			this.MenuStrip1.Text = "MenuStrip1";
			this.MEnuToolStripMenuItem.Name = "MEnuToolStripMenuItem";
			this.MEnuToolStripMenuItem.Size = new global::System.Drawing.Size(50, 20);
			this.MEnuToolStripMenuItem.Text = "Menu";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(823, 591);
			base.Controls.Add(this.TableLayoutPanel2);
			base.Controls.Add(this.MenuStrip1);
			base.MainMenuStrip = this.MenuStrip1;
			base.Name = "FormXX";
			this.Text = "FormXX";
			this.TableLayoutPanel1.ResumeLayout(false);
			this.TableLayoutPanel2.ResumeLayout(false);
			this.MenuStrip1.ResumeLayout(false);
			this.MenuStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400014B RID: 331
		private global::System.ComponentModel.IContainer components;
	}
}
