using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Vibration_Evaluation
{
	// Token: 0x02000020 RID: 32
	[DesignerGenerated]
	public class PlotUC : UserControl
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00017954 File Offset: 0x00015B54
		[DebuggerNonUserCode]
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

		// Token: 0x06000356 RID: 854 RVA: 0x000179A4 File Offset: 0x00015BA4
		[DebuggerStepThrough]
		private void InitializeComponent()
		{
			this.PictureBox1 = new PictureBox();
			this.TLPstats = new TableLayoutPanel();
			this.LBLStatName = new Label();
			this.Label5 = new Label();
			this.Label6 = new Label();
			this.lblName = new Label();
			this.lblPkPk = new Label();
			this.Label4 = new Label();
			this.LbLRMS = new Label();
			this.lblAverage = new Label();
			this.PanelOptions = new Panel();
			this.ckCursorLocation = new CheckBox();
			this.ckStats = new CheckBox();
			this.ButtonClear = new Button();
			this.TableLayoutPanel1 = new TableLayoutPanel();
			this.ScaleDown = new Label();
			this.ScaleUp = new Label();
			this.lblclose = new Label();
			this.lblYfullScale = new Label();
			this.Label3 = new Label();
			this.Label7 = new Label();
			this.lblCursorY = new Label();
			this.lblCursorX = new Label();
			this.lblYmax = new Label();
			this.lblYmid = new Label();
			this.lblYmin = new Label();
			this.lblXmax = new Label();
			this.lblTitle = new Label();
			this.lblYaxis = new Label();
			this.lblXaxis = new Label();
			((ISupportInitialize)this.PictureBox1).BeginInit();
			this.TLPstats.SuspendLayout();
			this.PanelOptions.SuspendLayout();
			this.TableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.PictureBox1.BackColor = Color.WhiteSmoke;
			this.PictureBox1.BackgroundImageLayout = ImageLayout.None;
			this.PictureBox1.BorderStyle = BorderStyle.FixedSingle;
			this.PictureBox1.Cursor = Cursors.Cross;
			this.PictureBox1.Location = new Point(46, 4);
			this.PictureBox1.Margin = new Padding(3, 2, 3, 3);
			this.PictureBox1.MinimumSize = new Size(10, 10);
			this.PictureBox1.Name = "PictureBox1";
			this.PictureBox1.Size = new Size(540, 293);
			this.PictureBox1.TabIndex = 10;
			this.PictureBox1.TabStop = false;
			this.TLPstats.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.TLPstats.BackColor = Color.GhostWhite;
			this.TLPstats.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
			this.TLPstats.ColumnCount = 2;
			this.TLPstats.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45f));
			this.TLPstats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			this.TLPstats.Controls.Add(this.LBLStatName, 0, 0);
			this.TLPstats.Controls.Add(this.lblName, 1, 0);
			this.TLPstats.Controls.Add(this.Label5, 0, 1);
			this.TLPstats.Controls.Add(this.lblAverage, 1, 1);
			this.TLPstats.Controls.Add(this.Label6, 0, 2);
			this.TLPstats.Controls.Add(this.lblPkPk, 1, 2);
			this.TLPstats.Controls.Add(this.Label4, 0, 3);
			this.TLPstats.Controls.Add(this.LbLRMS, 1, 3);
			this.TLPstats.Location = new Point(464, 14);
			this.TLPstats.Name = "TLPstats";
			this.TLPstats.RowCount = 4;
			this.TLPstats.RowStyles.Add(new RowStyle(SizeType.Percent, 20.40608f));
			this.TLPstats.RowStyles.Add(new RowStyle(SizeType.Percent, 20.40608f));
			this.TLPstats.RowStyles.Add(new RowStyle(SizeType.Percent, 20.40608f));
			this.TLPstats.RowStyles.Add(new RowStyle(SizeType.Percent, 20.40608f));
			this.TLPstats.Size = new Size(97, 71);
			this.TLPstats.TabIndex = 11;
			this.TLPstats.Visible = false;
			this.LBLStatName.AutoSize = true;
			this.LBLStatName.BackColor = Color.Gainsboro;
			this.LBLStatName.BorderStyle = BorderStyle.FixedSingle;
			this.LBLStatName.FlatStyle = FlatStyle.Flat;
			this.LBLStatName.Location = new Point(4, 1);
			this.LBLStatName.Name = "LBLStatName";
			this.LBLStatName.Size = new Size(37, 15);
			this.LBLStatName.TabIndex = 0;
			this.LBLStatName.Text = "Name";
			this.Label5.AutoSize = true;
			this.Label5.Location = new Point(4, 18);
			this.Label5.Name = "Label5";
			this.Label5.Size = new Size(26, 13);
			this.Label5.TabIndex = 4;
			this.Label5.Text = "Avg";
			this.Label6.AutoSize = true;
			this.Label6.Location = new Point(4, 35);
			this.Label6.Name = "Label6";
			this.Label6.Size = new Size(33, 13);
			this.Label6.TabIndex = 5;
			this.Label6.Text = "PkPk";
			this.lblName.AutoSize = true;
			this.lblName.Location = new Point(50, 1);
			this.lblName.Name = "lblName";
			this.lblName.Size = new Size(43, 16);
			this.lblName.TabIndex = 6;
			this.lblName.Text = "TraceName";
			this.lblPkPk.AutoSize = true;
			this.lblPkPk.Location = new Point(50, 35);
			this.lblPkPk.Name = "lblPkPk";
			this.lblPkPk.Size = new Size(33, 13);
			this.lblPkPk.TabIndex = 11;
			this.lblPkPk.Text = "PkPk";
			this.Label4.AutoSize = true;
			this.Label4.Location = new Point(4, 52);
			this.Label4.Name = "Label4";
			this.Label4.Size = new Size(35, 13);
			this.Label4.TabIndex = 12;
			this.Label4.Text = "stDev";
			this.LbLRMS.AutoSize = true;
			this.LbLRMS.Location = new Point(50, 52);
			this.LbLRMS.Name = "LbLRMS";
			this.LbLRMS.Size = new Size(35, 13);
			this.LbLRMS.TabIndex = 13;
			this.LbLRMS.Text = "stDev";
			this.lblAverage.AutoSize = true;
			this.lblAverage.Location = new Point(50, 18);
			this.lblAverage.Name = "lblAverage";
			this.lblAverage.Size = new Size(41, 16);
			this.lblAverage.TabIndex = 10;
			this.lblAverage.Text = "Average";
			this.PanelOptions.BackColor = Color.Ivory;
			this.PanelOptions.BorderStyle = BorderStyle.FixedSingle;
			this.PanelOptions.Controls.Add(this.ckCursorLocation);
			this.PanelOptions.Controls.Add(this.ckStats);
			this.PanelOptions.Controls.Add(this.ButtonClear);
			this.PanelOptions.Controls.Add(this.TableLayoutPanel1);
			this.PanelOptions.Controls.Add(this.lblclose);
			this.PanelOptions.Controls.Add(this.lblYfullScale);
			this.PanelOptions.Controls.Add(this.Label3);
			this.PanelOptions.Controls.Add(this.Label7);
			this.PanelOptions.Location = new Point(89, 20);
			this.PanelOptions.Name = "PanelOptions";
			this.PanelOptions.Size = new Size(241, 107);
			this.PanelOptions.TabIndex = 12;
			this.PanelOptions.Visible = false;
			this.ckCursorLocation.AutoSize = true;
			this.ckCursorLocation.Location = new Point(9, 86);
			this.ckCursorLocation.Name = "ckCursorLocation";
			this.ckCursorLocation.Size = new Size(100, 17);
			this.ckCursorLocation.TabIndex = 13;
			this.ckCursorLocation.Text = "Cursor Location";
			this.ckCursorLocation.UseVisualStyleBackColor = true;
			this.ckStats.AutoSize = true;
			this.ckStats.Location = new Point(128, 85);
			this.ckStats.Name = "ckStats";
			this.ckStats.Size = new Size(98, 17);
			this.ckStats.TabIndex = 12;
			this.ckStats.Text = "Show Statistics";
			this.ckStats.UseVisualStyleBackColor = true;
			this.ButtonClear.Location = new Point(152, 41);
			this.ButtonClear.Name = "ButtonClear";
			this.ButtonClear.Size = new Size(39, 22);
			this.ButtonClear.TabIndex = 11;
			this.ButtonClear.Text = "Clear";
			this.ButtonClear.UseVisualStyleBackColor = true;
			this.TableLayoutPanel1.ColumnCount = 1;
			this.TableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.TableLayoutPanel1.Controls.Add(this.ScaleDown, 0, 1);
			this.TableLayoutPanel1.Controls.Add(this.ScaleUp, 0, 0);
			this.TableLayoutPanel1.Location = new Point(50, 37);
			this.TableLayoutPanel1.Margin = new Padding(0);
			this.TableLayoutPanel1.Name = "TableLayoutPanel1";
			this.TableLayoutPanel1.RowCount = 2;
			this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.TableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.TableLayoutPanel1.Size = new Size(20, 31);
			this.TableLayoutPanel1.TabIndex = 9;
			this.ScaleDown.AutoSize = true;
			this.ScaleDown.BackColor = SystemColors.ControlLight;
			this.ScaleDown.BorderStyle = BorderStyle.FixedSingle;
			this.ScaleDown.Dock = DockStyle.Fill;
			this.ScaleDown.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.ScaleDown.Location = new Point(3, 15);
			this.ScaleDown.Name = "ScaleDown";
			this.ScaleDown.Size = new Size(14, 16);
			this.ScaleDown.TabIndex = 1;
			this.ScaleDown.Text = "<";
			this.ScaleUp.AutoSize = true;
			this.ScaleUp.BackColor = SystemColors.ControlLight;
			this.ScaleUp.BorderStyle = BorderStyle.FixedSingle;
			this.ScaleUp.Dock = DockStyle.Fill;
			this.ScaleUp.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.ScaleUp.Location = new Point(3, 0);
			this.ScaleUp.Name = "ScaleUp";
			this.ScaleUp.Size = new Size(14, 15);
			this.ScaleUp.TabIndex = 0;
			this.ScaleUp.Text = ">";
			this.lblclose.AutoSize = true;
			this.lblclose.BackColor = Color.Gainsboro;
			this.lblclose.BorderStyle = BorderStyle.FixedSingle;
			this.lblclose.Location = new Point(152, 8);
			this.lblclose.Margin = new Padding(2);
			this.lblclose.Name = "lblclose";
			this.lblclose.Size = new Size(35, 15);
			this.lblclose.TabIndex = 0;
			this.lblclose.Text = "Close";
			this.lblYfullScale.AutoSize = true;
			this.lblYfullScale.Location = new Point(7, 48);
			this.lblYfullScale.Margin = new Padding(2, 0, 2, 0);
			this.lblYfullScale.Name = "lblYfullScale";
			this.lblYfullScale.Size = new Size(13, 13);
			this.lblYfullScale.TabIndex = 5;
			this.lblYfullScale.Text = "1";
			this.Label3.AutoSize = true;
			this.Label3.Location = new Point(17, 22);
			this.Label3.Margin = new Padding(2, 0, 2, 0);
			this.Label3.Name = "Label3";
			this.Label3.Size = new Size(53, 13);
			this.Label3.TabIndex = 3;
			this.Label3.Text = "Full Scale";
			this.Label7.AutoSize = true;
			this.Label7.Location = new Point(7, 4);
			this.Label7.Margin = new Padding(2, 0, 2, 0);
			this.Label7.Name = "Label7";
			this.Label7.Size = new Size(94, 13);
			this.Label7.TabIndex = 2;
			this.Label7.Text = "Plot Options Menu";
			this.lblCursorY.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.lblCursorY.AutoSize = true;
			this.lblCursorY.BackColor = Color.WhiteSmoke;
			this.lblCursorY.Location = new Point(164, 300);
			this.lblCursorY.Margin = new Padding(2, 0, 2, 0);
			this.lblCursorY.Name = "lblCursorY";
			this.lblCursorY.Size = new Size(32, 13);
			this.lblCursorY.TabIndex = 15;
			this.lblCursorY.Text = "Y = 0";
			this.lblCursorY.Visible = false;
			this.lblCursorX.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.lblCursorX.AutoSize = true;
			this.lblCursorX.BackColor = Color.WhiteSmoke;
			this.lblCursorX.Location = new Point(76, 300);
			this.lblCursorX.Margin = new Padding(2, 0, 2, 0);
			this.lblCursorX.Name = "lblCursorX";
			this.lblCursorX.Size = new Size(32, 13);
			this.lblCursorX.TabIndex = 16;
			this.lblCursorX.Text = "X = 0";
			this.lblCursorX.Visible = false;
			this.lblYmax.AutoSize = true;
			this.lblYmax.Location = new Point(3, 4);
			this.lblYmax.Name = "lblYmax";
			this.lblYmax.Size = new Size(13, 13);
			this.lblYmax.TabIndex = 17;
			this.lblYmax.Text = "1";
			this.lblYmid.Anchor = AnchorStyles.Left;
			this.lblYmid.AutoSize = true;
			this.lblYmid.Location = new Point(3, 142);
			this.lblYmid.Name = "lblYmid";
			this.lblYmid.Size = new Size(13, 13);
			this.lblYmid.TabIndex = 18;
			this.lblYmid.Text = "0";
			this.lblYmin.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.lblYmin.AutoSize = true;
			this.lblYmin.Location = new Point(3, 284);
			this.lblYmin.Name = "lblYmin";
			this.lblYmin.Size = new Size(16, 13);
			this.lblYmin.TabIndex = 19;
			this.lblYmin.Text = "-1";
			this.lblXmax.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.lblXmax.AutoSize = true;
			this.lblXmax.Location = new Point(538, 300);
			this.lblXmax.Name = "lblXmax";
			this.lblXmax.Size = new Size(25, 13);
			this.lblXmax.TabIndex = 20;
			this.lblXmax.Text = "512";
			this.lblTitle.AutoSize = true;
			this.lblTitle.BackColor = Color.WhiteSmoke;
			this.lblTitle.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.lblTitle.Location = new Point(293, 4);
			this.lblTitle.Margin = new Padding(2, 0, 2, 0);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new Size(27, 13);
			this.lblTitle.TabIndex = 21;
			this.lblTitle.Text = "Title";
			this.lblYaxis.AutoSize = true;
			this.lblYaxis.Location = new Point(3, 20);
			this.lblYaxis.Name = "lblYaxis";
			this.lblYaxis.Size = new Size(37, 13);
			this.lblYaxis.TabIndex = 22;
			this.lblYaxis.Text = "Log2g";
			this.lblXaxis.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.lblXaxis.AutoSize = true;
			this.lblXaxis.Location = new Point(255, 300);
			this.lblXaxis.Name = "lblXaxis";
			this.lblXaxis.Size = new Size(40, 13);
			this.lblXaxis.TabIndex = 23;
			this.lblXaxis.Text = "sample";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.Controls.Add(this.lblXaxis);
			base.Controls.Add(this.lblYaxis);
			base.Controls.Add(this.lblTitle);
			base.Controls.Add(this.lblXmax);
			base.Controls.Add(this.lblYmin);
			base.Controls.Add(this.lblYmid);
			base.Controls.Add(this.lblYmax);
			base.Controls.Add(this.PanelOptions);
			base.Controls.Add(this.TLPstats);
			base.Controls.Add(this.lblCursorX);
			base.Controls.Add(this.lblCursorY);
			base.Controls.Add(this.PictureBox1);
			base.Name = "PlotUC";
			base.Size = new Size(593, 319);
			((ISupportInitialize)this.PictureBox1).EndInit();
			this.TLPstats.ResumeLayout(false);
			this.TLPstats.PerformLayout();
			this.PanelOptions.ResumeLayout(false);
			this.PanelOptions.PerformLayout();
			this.TableLayoutPanel1.ResumeLayout(false);
			this.TableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00018E98 File Offset: 0x00017098
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00018EA4 File Offset: 0x000170A4
		internal virtual PictureBox PictureBox1
		{
			[CompilerGenerated]
			get
			{
				return this._PictureBox1;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				MouseEventHandler value2 = new MouseEventHandler(this.PictureBox1_MouseClick);
				MouseEventHandler value3 = new MouseEventHandler(this.PictureBox1_MouseMove);
				PictureBox pictureBox = this._PictureBox1;
				if (pictureBox != null)
				{
					pictureBox.MouseClick -= value2;
					pictureBox.MouseMove -= value3;
				}
				this._PictureBox1 = value;
				pictureBox = this._PictureBox1;
				if (pictureBox != null)
				{
					pictureBox.MouseClick += value2;
					pictureBox.MouseMove += value3;
				}
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00018F02 File Offset: 0x00017102
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00018F0C File Offset: 0x0001710C
		internal virtual TableLayoutPanel TLPstats { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00018F15 File Offset: 0x00017115
		// (set) Token: 0x0600035C RID: 860 RVA: 0x00018F20 File Offset: 0x00017120
		internal virtual Label LBLStatName
		{
			[CompilerGenerated]
			get
			{
				return this._LBLStatName;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.LBLStatName_Click);
				Label lblstatName = this._LBLStatName;
				if (lblstatName != null)
				{
					lblstatName.Click -= value2;
				}
				this._LBLStatName = value;
				lblstatName = this._LBLStatName;
				if (lblstatName != null)
				{
					lblstatName.Click += value2;
				}
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00018F63 File Offset: 0x00017163
		// (set) Token: 0x0600035E RID: 862 RVA: 0x00018F6D File Offset: 0x0001716D
		internal virtual Label Label5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600035F RID: 863 RVA: 0x00018F76 File Offset: 0x00017176
		// (set) Token: 0x06000360 RID: 864 RVA: 0x00018F80 File Offset: 0x00017180
		internal virtual Label Label6 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000361 RID: 865 RVA: 0x00018F89 File Offset: 0x00017189
		// (set) Token: 0x06000362 RID: 866 RVA: 0x00018F93 File Offset: 0x00017193
		internal virtual Label lblName { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00018F9C File Offset: 0x0001719C
		// (set) Token: 0x06000364 RID: 868 RVA: 0x00018FA6 File Offset: 0x000171A6
		internal virtual Label lblAverage { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00018FAF File Offset: 0x000171AF
		// (set) Token: 0x06000366 RID: 870 RVA: 0x00018FB9 File Offset: 0x000171B9
		internal virtual Label lblPkPk { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00018FC2 File Offset: 0x000171C2
		// (set) Token: 0x06000368 RID: 872 RVA: 0x00018FCC File Offset: 0x000171CC
		internal virtual Panel PanelOptions { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00018FD5 File Offset: 0x000171D5
		// (set) Token: 0x0600036A RID: 874 RVA: 0x00018FDF File Offset: 0x000171DF
		internal virtual Label lblCursorY { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00018FE8 File Offset: 0x000171E8
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00018FF2 File Offset: 0x000171F2
		internal virtual Label lblCursorX { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00018FFB File Offset: 0x000171FB
		// (set) Token: 0x0600036E RID: 878 RVA: 0x00019008 File Offset: 0x00017208
		internal virtual Label lblclose
		{
			[CompilerGenerated]
			get
			{
				return this._lblclose;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.lblclose_Click);
				Label lblclose = this._lblclose;
				if (lblclose != null)
				{
					lblclose.Click -= value2;
				}
				this._lblclose = value;
				lblclose = this._lblclose;
				if (lblclose != null)
				{
					lblclose.Click += value2;
				}
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0001904B File Offset: 0x0001724B
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00019055 File Offset: 0x00017255
		internal virtual Label Label7 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0001905E File Offset: 0x0001725E
		// (set) Token: 0x06000372 RID: 882 RVA: 0x00019068 File Offset: 0x00017268
		internal virtual Label Label3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00019071 File Offset: 0x00017271
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0001907B File Offset: 0x0001727B
		internal virtual Label lblYfullScale { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00019084 File Offset: 0x00017284
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0001908E File Offset: 0x0001728E
		internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00019097 File Offset: 0x00017297
		// (set) Token: 0x06000378 RID: 888 RVA: 0x000190A4 File Offset: 0x000172A4
		internal virtual Label ScaleDown
		{
			[CompilerGenerated]
			get
			{
				return this._ScaleDown;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ScaleDown_Click);
				Label scaleDown = this._ScaleDown;
				if (scaleDown != null)
				{
					scaleDown.Click -= value2;
				}
				this._ScaleDown = value;
				scaleDown = this._ScaleDown;
				if (scaleDown != null)
				{
					scaleDown.Click += value2;
				}
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000379 RID: 889 RVA: 0x000190E7 File Offset: 0x000172E7
		// (set) Token: 0x0600037A RID: 890 RVA: 0x000190F4 File Offset: 0x000172F4
		internal virtual Label ScaleUp
		{
			[CompilerGenerated]
			get
			{
				return this._ScaleUp;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ScaleUp_Click);
				Label scaleUp = this._ScaleUp;
				if (scaleUp != null)
				{
					scaleUp.Click -= value2;
				}
				this._ScaleUp = value;
				scaleUp = this._ScaleUp;
				if (scaleUp != null)
				{
					scaleUp.Click += value2;
				}
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00019137 File Offset: 0x00017337
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00019144 File Offset: 0x00017344
		internal virtual CheckBox ckStats
		{
			[CompilerGenerated]
			get
			{
				return this._ckStats;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ckStats_Click);
				CheckBox ckStats = this._ckStats;
				if (ckStats != null)
				{
					ckStats.Click -= value2;
				}
				this._ckStats = value;
				ckStats = this._ckStats;
				if (ckStats != null)
				{
					ckStats.Click += value2;
				}
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00019187 File Offset: 0x00017387
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00019194 File Offset: 0x00017394
		internal virtual Button ButtonClear
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonClear;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonClear_Click);
				Button buttonClear = this._ButtonClear;
				if (buttonClear != null)
				{
					buttonClear.Click -= value2;
				}
				this._ButtonClear = value;
				buttonClear = this._ButtonClear;
				if (buttonClear != null)
				{
					buttonClear.Click += value2;
				}
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600037F RID: 895 RVA: 0x000191D7 File Offset: 0x000173D7
		// (set) Token: 0x06000380 RID: 896 RVA: 0x000191E4 File Offset: 0x000173E4
		internal virtual CheckBox ckCursorLocation
		{
			[CompilerGenerated]
			get
			{
				return this._ckCursorLocation;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ckCursorLocation_CheckStateChanged);
				CheckBox ckCursorLocation = this._ckCursorLocation;
				if (ckCursorLocation != null)
				{
					ckCursorLocation.CheckStateChanged -= value2;
				}
				this._ckCursorLocation = value;
				ckCursorLocation = this._ckCursorLocation;
				if (ckCursorLocation != null)
				{
					ckCursorLocation.CheckStateChanged += value2;
				}
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00019227 File Offset: 0x00017427
		// (set) Token: 0x06000382 RID: 898 RVA: 0x00019231 File Offset: 0x00017431
		internal virtual Label Label4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0001923A File Offset: 0x0001743A
		// (set) Token: 0x06000384 RID: 900 RVA: 0x00019244 File Offset: 0x00017444
		internal virtual Label LbLRMS { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0001924D File Offset: 0x0001744D
		// (set) Token: 0x06000386 RID: 902 RVA: 0x00019257 File Offset: 0x00017457
		internal virtual Label lblYmax { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00019260 File Offset: 0x00017460
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0001926A File Offset: 0x0001746A
		internal virtual Label lblYmid { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00019273 File Offset: 0x00017473
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0001927D File Offset: 0x0001747D
		internal virtual Label lblYmin { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00019286 File Offset: 0x00017486
		// (set) Token: 0x0600038C RID: 908 RVA: 0x00019290 File Offset: 0x00017490
		internal virtual Label lblXmax { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00019299 File Offset: 0x00017499
		// (set) Token: 0x0600038E RID: 910 RVA: 0x000192A4 File Offset: 0x000174A4
		internal virtual Label lblTitle
		{
			[CompilerGenerated]
			get
			{
				return this._lblTitle;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.lblTitle_SizeChanged);
				Label lblTitle = this._lblTitle;
				if (lblTitle != null)
				{
					lblTitle.SizeChanged -= value2;
				}
				this._lblTitle = value;
				lblTitle = this._lblTitle;
				if (lblTitle != null)
				{
					lblTitle.SizeChanged += value2;
				}
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600038F RID: 911 RVA: 0x000192E7 File Offset: 0x000174E7
		// (set) Token: 0x06000390 RID: 912 RVA: 0x000192F1 File Offset: 0x000174F1
		internal virtual Label lblYaxis { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000391 RID: 913 RVA: 0x000192FA File Offset: 0x000174FA
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00019304 File Offset: 0x00017504
		internal virtual Label lblXaxis { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		/// <summary>
		/// Currently implemented in Draw bars only.
		/// </summary>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00019310 File Offset: 0x00017510
		// (set) Token: 0x06000394 RID: 916 RVA: 0x00019328 File Offset: 0x00017528
		public bool ScaleLog10
		{
			get
			{
				return this._ScaleLog10;
			}
			set
			{
				this._ScaleLog10 = value;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000395 RID: 917 RVA: 0x00019334 File Offset: 0x00017534
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0001934C File Offset: 0x0001754C
		public bool AlarmsVisible
		{
			get
			{
				return this._alarmsVisible;
			}
			set
			{
				this._alarmsVisible = value;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000397 RID: 919 RVA: 0x00019358 File Offset: 0x00017558
		// (set) Token: 0x06000398 RID: 920 RVA: 0x00019370 File Offset: 0x00017570
		public AlarmsClass.AxisClass AlarmValues
		{
			get
			{
				return this._AlarmValues;
			}
			set
			{
				this._AlarmValues = value;
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001937C File Offset: 0x0001757C
		public PlotUC()
		{
			base.Load += this.PlotUC_Load;
			base.Resize += this.PlotUC_Resize;
			this._ScaleType = PlotUC.ScaleTypeEnum.Signed;
			this._Ymax = 60f;
			this._Ymin = -this._Ymax;
			this._Yoffset = (this._Ymax - this._Ymin) / 2f;
			this._Xmax = 700f;
			this._Xmin = 0f;
			this._Xoffset = 0f;
			this.Grid = new PlotUC.GridClass();
			this.Traces = new PlotUC.TraceCollection();
			this._alarmsVisible = false;
			this._ScaleLog10 = false;
			this._AlarmValues = new AlarmsClass.AxisClass();
			this._chartType = PlotUC.ChartTypeEnum.bar;
			this._barWidth = 2;
			this.ContinuousStats = false;
			this.BM = new Bitmap(300, 200);
			this.BMG = Graphics.FromImage(this.BM);
			this.LBLaLow = new Label[7];
			this.LBLaHigh = new Label[7];
			this._xScaleMax = 1.0;
			this.InitializeComponent();
			this.ScaleArrayY = new float[]
			{
				1f,
				10f,
				30f,
				60f,
				120f
			};
			this.GridGapArray = new float[]
			{
				0.001f,
				0.02f,
				1f,
				0.1f,
				1f
			};
			this.GridGapIndex = 0;
			this.PlotTraces();
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x0600039A RID: 922 RVA: 0x000194E4 File Offset: 0x000176E4
		// (set) Token: 0x0600039B RID: 923 RVA: 0x000194FC File Offset: 0x000176FC
		public string Title
		{
			get
			{
				return this._Title;
			}
			set
			{
				this._Title = value;
				this.lblTitle.Text = value;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600039C RID: 924 RVA: 0x00019514 File Offset: 0x00017714
		// (set) Token: 0x0600039D RID: 925 RVA: 0x0001952C File Offset: 0x0001772C
		public string ScaleUnitsText
		{
			get
			{
				return this._ScaleUnitsText;
			}
			set
			{
				this._ScaleUnitsText = value;
				this.lblTitle.Text = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600039E RID: 926 RVA: 0x00019544 File Offset: 0x00017744
		// (set) Token: 0x0600039F RID: 927 RVA: 0x0001955C File Offset: 0x0001775C
		public double xScaleMax
		{
			get
			{
				return this._xScaleMax;
			}
			set
			{
				bool flag = value != 0.0;
				if (flag)
				{
					this._xScaleMax = value;
					this.lblXmax.Text = value.ToString("0");
				}
			}
		}

		/// <summary>
		/// Setting ScaleType will implicitly set Ymin if ScaleType is not FreeForm
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x000195A0 File Offset: 0x000177A0
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x000195B8 File Offset: 0x000177B8
		public PlotUC.ScaleTypeEnum ScaleType
		{
			get
			{
				return this._ScaleType;
			}
			set
			{
				this._ScaleType = value;
				bool flag = this.ScaleType == PlotUC.ScaleTypeEnum.Signed;
				if (flag)
				{
					this.Ymin = -this.Ymax;
				}
				else
				{
					bool flag2 = this.ScaleType == PlotUC.ScaleTypeEnum.Unsigned;
					if (flag2)
					{
					}
				}
				this.ChangeYscale(0);
			}
		}

		/// <summary>
		/// Setting YMax will implicitly set Ymin if ScaleType is not FreeForm
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00019604 File Offset: 0x00017804
		// (set) Token: 0x060003A3 RID: 931 RVA: 0x0001961C File Offset: 0x0001781C
		public float Ymax
		{
			get
			{
				return this._Ymax;
			}
			set
			{
				this._Ymax = value;
				this.lblYfullScale.Text = value.ToString();
				this.lblYmax.Text = value.ToString();
				this.GridCalcYscaleRedraw();
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00019654 File Offset: 0x00017854
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x0001970B File Offset: 0x0001790B
		public float Ymin
		{
			get
			{
				switch (this.ScaleType)
				{
				case PlotUC.ScaleTypeEnum.Unsigned:
					this.Ycenter = this._Ymax / 2f;
					this.lblYmid.Text = Conversion.Int(this.Ycenter).ToString();
					break;
				case PlotUC.ScaleTypeEnum.Signed:
					this._Ymin = -this._Ymax;
					this.Ycenter = 0f;
					this.lblYmid.Text = this.Ycenter.ToString();
					break;
				}
				this.lblYmin.Text = this._Ymin.ToString();
				return this._Ymin;
			}
			set
			{
				this._Ymin = value;
				this.lblYmin.Text = value.ToString();
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00019728 File Offset: 0x00017928
		public float Yscale
		{
			get
			{
				this._Yscale = (float)this.PictureBox1.Height / (this.Ymax - this.Ymin);
				return this._Yscale;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00019760 File Offset: 0x00017960
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x000197C4 File Offset: 0x000179C4
		public float Yoffset
		{
			get
			{
				switch (this.ScaleType)
				{
				case PlotUC.ScaleTypeEnum.Unsigned:
					this._Yoffset = 0f;
					break;
				case PlotUC.ScaleTypeEnum.Signed:
					this._Yoffset = (this.Ymax - this.Ymin) / 2f;
					break;
				}
				return this._Yoffset;
			}
			set
			{
				this._Yoffset = value;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x000197D0 File Offset: 0x000179D0
		// (set) Token: 0x060003AA RID: 938 RVA: 0x000197E8 File Offset: 0x000179E8
		public float Xmax
		{
			get
			{
				return this._Xmax;
			}
			set
			{
				this._Xmax = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003AB RID: 939 RVA: 0x000197F4 File Offset: 0x000179F4
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0001980C File Offset: 0x00017A0C
		public float Xmin
		{
			get
			{
				return this._Xmin;
			}
			set
			{
				this._Xmin = value;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003AD RID: 941 RVA: 0x00019818 File Offset: 0x00017A18
		// (set) Token: 0x060003AE RID: 942 RVA: 0x00019830 File Offset: 0x00017A30
		public float Xoffset
		{
			get
			{
				return this._Xoffset;
			}
			set
			{
				this._Xoffset = value;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0001983C File Offset: 0x00017A3C
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x00019854 File Offset: 0x00017A54
		public PlotUC.ChartTypeEnum chartType
		{
			get
			{
				return this._chartType;
			}
			set
			{
				this._chartType = value;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00019860 File Offset: 0x00017A60
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x00019878 File Offset: 0x00017A78
		public int barWidth
		{
			get
			{
				return this._barWidth;
			}
			set
			{
				this._barWidth = value;
			}
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00019882 File Offset: 0x00017A82
		private void PlotUC_Load(object sender, EventArgs e)
		{
			this.lblYfullScale.Text = Conversions.ToString(this.Ymax);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001989C File Offset: 0x00017A9C
		private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			bool flag = e.Button == MouseButtons.Right;
			if (flag)
			{
				this.PanelOptions.Left = 110;
				this.PanelOptions.Visible = true;
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000198D8 File Offset: 0x00017AD8
		private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			double num = (double)((float)e.X / ((float)this.PictureBox1.Width / (this.Xmax - this.Xmin)));
			num *= this._xScaleMax / (double)this._Xmax;
			this.lblCursorX.Text = "X = " + num.ToString("0");
			double num3;
			switch (this.ScaleType)
			{
			case PlotUC.ScaleTypeEnum.Unsigned:
			{
				double num2 = (double)(checked(this.PictureBox1.Height - e.Y)) / (double)this.PictureBox1.Height;
				num3 = num2 * (double)this.Ymax;
				break;
			}
			case PlotUC.ScaleTypeEnum.Signed:
				num3 = ((double)this.PictureBox1.Height / 2.0 - (double)e.Y) / (double)this.Yscale;
				break;
			case PlotUC.ScaleTypeEnum.log10:
			{
				double num2 = (double)(checked(this.PictureBox1.Height - e.Y)) / (double)this.PictureBox1.Height;
				double num4 = Math.Pow(10.0, num2 * 2.0) / 100.0 * (double)this.Ymax;
				num3 = num4;
				break;
			}
			}
			this.lblCursorY.Text = "Y = " + num3.ToString("0.####");
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00019A34 File Offset: 0x00017C34
		private void lblclose_Click(object sender, EventArgs e)
		{
			this.PanelOptions.Visible = false;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00019A44 File Offset: 0x00017C44
		private void ckStats_Click(object sender, EventArgs e)
		{
			bool @checked = this.ckStats.Checked;
			if (@checked)
			{
				this.TLPstats.Left = checked(base.Width - (this.TLPstats.Width + 5));
				this.TLPstats.Visible = true;
				this.lblName.Text = this.Traces[0].Label;
				string statType = "frequency";
				bool flag = this.chartType == PlotUC.ChartTypeEnum.line;
				if (flag)
				{
					statType = "timeDomain";
				}
				this.StatsUpdate(statType);
			}
			else
			{
				this.TLPstats.Visible = false;
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00019AE0 File Offset: 0x00017CE0
		public void ChangeYscale(int increment)
		{
			bool flag = increment < -1 | increment > 1;
			if (flag)
			{
				throw new Exception("Increment out of range");
			}
			checked
			{
				this.YscaleIndex += increment;
				bool flag2 = this.YscaleIndex > this.ScaleArrayY.Count<float>() - 1;
				if (flag2)
				{
					this.YscaleIndex = 0;
				}
				bool flag3 = this.YscaleIndex < 0;
				if (flag3)
				{
					this.YscaleIndex = this.ScaleArrayY.Count<float>() - 1;
				}
				this.Ymax = this.ScaleArrayY[this.YscaleIndex];
				this.GridCalcYscaleRedraw();
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00019B74 File Offset: 0x00017D74
		private void ChangeGridGap(int increment)
		{
			bool flag = increment < -1 | increment > 1;
			if (flag)
			{
				throw new Exception("Increment out of range");
			}
			checked
			{
				this.GridGapIndex += increment;
				bool flag2 = this.GridGapIndex > this.GridGapArray.Count<float>() - 1;
				if (flag2)
				{
					this.GridGapIndex = 0;
				}
				bool flag3 = this.GridGapIndex < 0;
				if (flag3)
				{
					this.GridGapIndex = this.GridGapArray.Count<float>() - 1;
				}
				this.GridCalcYscaleRedraw();
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00019BF4 File Offset: 0x00017DF4
		public void GridCalcYscaleRedraw()
		{
			bool flag = this.ScaleType == PlotUC.ScaleTypeEnum.Unsigned;
			checked
			{
				if (flag)
				{
					this.GridGapArray[3] = 0f;
					int num = this.GridGapArray.Count<float>() - 1;
					for (int i = 0; i <= num; i++)
					{
						this.GridGapArray[i] = this._Ymax / (float)(2 + i * 2);
					}
				}
				else
				{
					this.GridGapArray[3] = 0f;
					int num2 = this.GridGapArray.Count<float>() - 1;
					for (int j = 0; j <= num2; j++)
					{
						this.GridGapArray[j] = this._Ymax / (float)(1 + j * 1);
					}
				}
				this.Grid.YaxisSpacingUnits = this.GridGapArray[this.GridGapIndex];
			}
			this.Grid.YaxisSpacingPixels = this.Grid.YaxisSpacingUnits * this.Yscale;
			string text = this.Grid.YaxisSpacingUnits.ToString("0.0###");
			this.lblYfullScale.Text = this.Ymax.ToString();
			this.lblYmax.Text = this.Ymax.ToString();
			this.lblYmid.Text = Conversion.Int(this.Ycenter).ToString("0");
			bool flag2 = this.ScaleType == PlotUC.ScaleTypeEnum.log10;
			if (flag2)
			{
				this.lblYmid.Text = (this.Ymax / 10f).ToString("0");
			}
			this.lblYmin.Text = this.Ymin.ToString();
			this.PlotTraces();
			Application.DoEvents();
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00019DAC File Offset: 0x00017FAC
		private void LBLStatName_Click(object sender, EventArgs e)
		{
			int index = checked(this.Traces[this.lblName.Text].Index + 1) % this.Traces.Count;
			this.lblName.Text = this.Traces[index].Label;
			string statType = "frequency";
			bool flag = this.chartType == PlotUC.ChartTypeEnum.line;
			if (flag)
			{
				statType = "timeDomain";
			}
			this.StatsUpdate(statType);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00019E22 File Offset: 0x00018022
		private void ScaleUp_Click(object sender, EventArgs e)
		{
			this.ChangeYscale(1);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00019E2D File Offset: 0x0001802D
		private void ScaleDown_Click(object sender, EventArgs e)
		{
			this.ChangeYscale(-1);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00019E38 File Offset: 0x00018038
		private void GridUp_Click(object sender, EventArgs e)
		{
			this.ChangeGridGap(1);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00019E43 File Offset: 0x00018043
		private void GridDown_Click(object sender, EventArgs e)
		{
			this.ChangeGridGap(-1);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00019E4E File Offset: 0x0001804E
		private void ButtonClear_Click(object sender, EventArgs e)
		{
			this.ClearTracesDrawGrid(false);
		}

		/// <summary>
		/// Instantiates and initializes a trace and adds it to the trace collection.
		/// </summary>
		/// <param name="Label"></param>
		/// <param name="RegName"></param>
		/// <param name="visible"></param>
		/// <remarks></remarks>
		// Token: 0x060003C1 RID: 961 RVA: 0x00019E5C File Offset: 0x0001805C
		public void AddTrace(string Label, string RegName, Color traceColor, bool visible = true)
		{
			Pen pen = new Pen(traceColor, 1f);
			this.Traces.AddTrace(new PlotUC.TraceClass(Label, RegName, pen, this.Traces.Count, visible));
		}

		/// <summary>
		/// Set object in the Stats table on the Plot Form.
		/// </summary>
		/// <param name="statType"></param>
		// Token: 0x060003C2 RID: 962 RVA: 0x00019E98 File Offset: 0x00018098
		private void StatsUpdate(string statType)
		{
			bool visible = this.TLPstats.Visible;
			if (visible)
			{
				string text = this.lblName.Text;
				this.Traces[text].StatsCalc(statType);
				if (Operators.CompareString(statType, "frequency", false) != 0)
				{
					if (Operators.CompareString(statType, "timeDomain", false) != 0)
					{
						Interaction.MsgBox("PlotUC sub StatsUpdate, Unknown SatsType string.", MsgBoxStyle.OkOnly, null);
					}
					else
					{
						this.Label5.Text = "Avg";
						this.Label6.Text = "PkPk";
						this.Label4.Text = "stDev";
						this.lblAverage.Text = this.Traces[text].Average.ToString("0.000");
						this.lblPkPk.Text = this.Traces[text].PKPK.ToString("0.000");
						this.LbLRMS.Text = this.Traces[text].RMS.ToString("0.000");
					}
				}
				else
				{
					this.Label5.Text = "Pk 1";
					this.Label6.Text = "Pk 2";
					this.Label4.Text = "Pk 3";
					this.lblAverage.Text = this.Traces[text].pk1.ToString("0.000");
					this.lblPkPk.Text = this.Traces[text].pk2.ToString("0.000");
					this.LbLRMS.Text = this.Traces[text].pk3.ToString("0.000");
				}
			}
		}

		/// <summary>
		/// Sets xPostion to zero, erases the traces, draws the grid.
		/// </summary>
		/// <remarks></remarks>
		// Token: 0x060003C3 RID: 963 RVA: 0x0001A07C File Offset: 0x0001827C
		public void resetDisplay()
		{
			this.TracesClearYflyBackX();
			this.ClearTracesDrawGrid(false);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001A090 File Offset: 0x00018290
		public void TracesClearYflyBackX()
		{
			checked
			{
				try
				{
					foreach (PlotUC.TraceClass traceClass in this.Traces)
					{
						int num = traceClass.VertData.Count<double>() - 1;
						for (int i = 0; i <= num; i++)
						{
							traceClass.VertData[i] = double.NaN;
						}
					}
				}
				finally
				{
					IEnumerator<PlotUC.TraceClass> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
				this.xCounter = 0;
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001A110 File Offset: 0x00018310
		private void ckCursorLocation_CheckStateChanged(object sender, EventArgs e)
		{
			bool @checked = this.ckCursorLocation.Checked;
			if (@checked)
			{
				this.lblCursorX.Visible = true;
				this.lblCursorY.Visible = true;
			}
			else
			{
				this.lblCursorX.Visible = false;
				this.lblCursorY.Visible = false;
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001A168 File Offset: 0x00018368
		public void AddTracePoints(IList<double> y)
		{
			try
			{
				foreach (PlotUC.TraceClass traceClass in this.Traces)
				{
					traceClass.VertData[this.xCounter] = y[traceClass.Index];
				}
			}
			finally
			{
				IEnumerator<PlotUC.TraceClass> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			checked
			{
				this.xCounter++;
				bool flag = (float)this.xCounter > this.Xmax;
				if (flag)
				{
					this.xCounter = 0;
				}
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0001A1F4 File Offset: 0x000183F4
		public void ClearTracesDrawGrid(bool SetTraceXtoZero = false)
		{
			this.lblYfullScale.Text = this.Ymax.ToString();
			this.BM = new Bitmap(this.PictureBox1.Width, this.PictureBox1.Height);
			this.BMG = Graphics.FromImage(this.BM);
			this.BMG.SmoothingMode = SmoothingMode.AntiAlias;
			float num = (float)((double)(checked(this.PictureBox1.Height - 2)) / 2.0);
			this.BMG.TranslateTransform(0f, num * 2f);
			this.BMG.ScaleTransform(1f, -1f);
			bool visible = this.Grid.Visible;
			if (visible)
			{
				float num2 = 0f;
				float x = (float)this.PictureBox1.Width;
				float num3 = 0f;
				int num4 = 0;
				this.Grid.YaxisSpacingPixels = (float)((double)this.PictureBox1.Height / 6.0);
				bool flag = this.ScaleType == PlotUC.ScaleTypeEnum.log10;
				if (flag)
				{
					int num5 = 1;
					do
					{
						double num6 = Math.Log10((double)num5);
						num3 = (float)((double)this.PictureBox1.Height / 2.0 * num6);
						this.BMG.DrawLine(this.Grid.Pen, num2, num3, x, num3);
						checked
						{
							num5++;
						}
					}
					while (num5 <= 10);
					int num7 = 1;
					do
					{
						double num6 = Math.Log10((double)num7);
						num3 = (float)((double)this.PictureBox1.Height / 2.0 * num6);
						num3 = (float)((double)num3 + (double)this.PictureBox1.Height / 2.0);
						this.BMG.DrawLine(this.Grid.Pen, num2, num3, x, num3);
						checked
						{
							num7++;
						}
					}
					while (num7 <= 10);
				}
				else
				{
					while (num3 < (float)this.PictureBox1.Height)
					{
						this.BMG.DrawLine(this.Grid.Pen, num2, num3, x, num3);
						checked
						{
							num4++;
						}
						num3 = this.Grid.YaxisSpacingPixels * (float)num4;
					}
				}
				num2 = 0f;
				num3 = 0f;
				float y = (float)this.PictureBox1.Height;
				float num8 = (float)this.PictureBox1.Width / (this.Xmax - this.Xmin);
				do
				{
					this.BMG.DrawLine(this.Grid.Pen, num2, num3, num2, y);
					num2 += (float)((double)this.PictureBox1.Width / 14.0);
				}
				while (num2 < (float)this.PictureBox1.Width);
			}
			this.PictureBox1.Image = this.BM;
			if (SetTraceXtoZero)
			{
				this.TracesClearYflyBackX();
			}
		}

		/// <summary>
		/// Resizes The bit map. 
		/// Redraws and re-scales stored Y Data for each trace.
		/// Does not reset the x ordinate of the plot.
		/// Multiple plot windows remian in sync on x if they are the same width.
		/// </summary>
		// Token: 0x060003C8 RID: 968 RVA: 0x0001A4D4 File Offset: 0x000186D4
		public void PlotTraces()
		{
			bool flag = this.chartType == PlotUC.ChartTypeEnum.bar;
			if (flag)
			{
				this.drawBars();
			}
			else
			{
				bool flag2 = this.chartType == PlotUC.ChartTypeEnum.line;
				if (flag2)
				{
					this.drawlines();
				}
				else
				{
					Interaction.MsgBox("unknown chartType; bar or line", MsgBoxStyle.OkOnly, "PlotUC module");
				}
			}
			bool @checked = this.ckStats.Checked;
			if (@checked)
			{
				string statType = "frequency";
				bool flag3 = this.chartType == PlotUC.ChartTypeEnum.line;
				if (flag3)
				{
					statType = "timeDomain";
				}
				this.StatsUpdate(statType);
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001A558 File Offset: 0x00018758
		private void drawlines()
		{
			this.lblYfullScale.Text = this.Ymax.ToString();
			this.ClearTracesDrawGrid(false);
			float num = (float)this.PictureBox1.Width / (this.Xmax - this.Xmin);
			checked
			{
				try
				{
					foreach (PlotUC.TraceClass traceClass in this.Traces)
					{
						bool visible = traceClass.Visible;
						if (visible)
						{
							int num2 = traceClass.VertData.Count<double>() - 1;
							for (int i = 0; i <= num2; i++)
							{
								bool flag = double.IsNaN(traceClass.VertData[i]);
								if (flag)
								{
									break;
								}
								unchecked
								{
									float num3 = (float)((traceClass.VertData[i] + (double)this.Yoffset) * (double)this.Yscale);
									bool flag2 = i > 0;
									float y;
									if (flag2)
									{
										y = (float)((traceClass.VertData[checked(i - 1)] + (double)this.Yoffset) * (double)this.Yscale);
									}
									else
									{
										y = num3;
									}
									float num4 = (float)i * num;
									float x = num4 + num;
									this.BMG.DrawLine(traceClass.Pen, num4, y, x, num3);
								}
							}
						}
					}
				}
				finally
				{
					IEnumerator<PlotUC.TraceClass> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
				this.PictureBox1.Image = this.BM;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0001A6D8 File Offset: 0x000188D8
		private void drawBars()
		{
			this.lblYfullScale.Text = this.Ymax.ToString();
			this.ClearTracesDrawGrid(false);
			float num = (float)this.PictureBox1.Width / (this.Xmax - this.Xmin);
			checked
			{
				try
				{
					foreach (PlotUC.TraceClass traceClass in this.Traces)
					{
						bool visible = traceClass.Visible;
						if (visible)
						{
							int num2 = traceClass.VertData.Count<double>() - 1;
							for (int i = 0; i <= num2; i++)
							{
								bool flag = double.IsNaN(traceClass.VertData[i]);
								if (flag)
								{
									break;
								}
								unchecked
								{
									float num3 = (float)((traceClass.VertData[i] + (double)this.Yoffset) * (double)this.Yscale);
									bool flag2 = this.ScaleType == PlotUC.ScaleTypeEnum.log10;
									if (flag2)
									{
										double num4 = traceClass.VertData[i];
										bool flag3 = num4 == 0.0;
										if (flag3)
										{
											num4 = 1E-06;
										}
										double d = 100.0 * (num4 / (double)this.Ymax);
										double num5 = Math.Log10(d) / 2.0;
										num3 = (float)(num5 * (double)(this.Ymax * this.Yscale));
									}
									float y = num3;
									float num6 = (float)i * num;
									float num7 = num6 + num;
									traceClass.Pen.Width = 1f;
									this.BMG.DrawLine(traceClass.Pen, num6, this.Ymin, num6, y);
								}
							}
						}
						traceClass.Pen.Width = 1f;
					}
				}
				finally
				{
					IEnumerator<PlotUC.TraceClass> enumerator;
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
				bool alarmsVisible = this._alarmsVisible;
				if (alarmsVisible)
				{
					this.drawAlarms();
				}
				this.PictureBox1.Image = this.BM;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001A8F0 File Offset: 0x00018AF0
		private void drawAlarms()
		{
			bool flag = this.AlarmValues.fMax == 0.0;
			if (!flag)
			{
				float num = (float)((double)this.PictureBox1.Width / this.AlarmValues.fMax);
				Pen pen = new Pen(Color.Black, 1f);
				Color color = Color.FromArgb(255, 230, 60);
				Pen pen2 = new Pen(color, 3f);
				Color color2 = Color.FromArgb(255, 80, 40);
				Pen pen3 = new Pen(color2, 3f);
				int num2 = 1;
				do
				{
					float num3 = (float)(this.AlarmValues.frequencyLow[num2] * (double)num);
					float num4 = (float)this.PictureBox1.Height;
					this.BMG.DrawLine(pen, num3, 0f, num3, num4);
					num3 = (float)(this.AlarmValues.frequencyHigh[num2] * (double)num);
					this.BMG.DrawLine(pen, num3, 0f, num3, num4);
					num3 = (float)(this.AlarmValues.frequencyLow[num2] * (double)num);
					float num5 = (float)(this.AlarmValues.frequencyHigh[num2] * (double)num);
					num4 = (float)(this.AlarmValues.level1[num2] * (double)this.Yscale);
					bool flag2 = this.ScaleType == PlotUC.ScaleTypeEnum.log10;
					if (flag2)
					{
						double num6 = this.AlarmValues.level1[num2];
						bool flag3 = num6 == 0.0;
						if (flag3)
						{
							num6 = 1E-06;
						}
						double d = 100.0 * (num6 / (double)this.Ymax);
						double num7 = Math.Log10(d) / 2.0;
						num4 = (float)(num7 * (double)(this.Ymax * this.Yscale));
					}
					this.BMG.DrawLine(pen2, num3, num4, num5, num4);
					num4 = (float)(this.AlarmValues.level2[num2] * (double)this.Yscale);
					bool flag4 = this.ScaleType == PlotUC.ScaleTypeEnum.log10;
					if (flag4)
					{
						double num6 = this.AlarmValues.level2[num2];
						bool flag5 = num6 == 0.0;
						if (flag5)
						{
							num6 = 1E-06;
						}
						double d = 100.0 * (num6 / (double)this.Ymax);
						double num7 = Math.Log10(d) / 2.0;
						num4 = (float)(num7 * (double)(this.Ymax * this.Yscale));
					}
					this.BMG.DrawLine(pen3, num3, num4, num5, num4);
					Brush black = Brushes.Black;
					Font font = new Font("Microsoft Sans Serif", 11f, FontStyle.Regular);
					this.BMG.ScaleTransform(1f, -1f);
					this.BMG.DrawString(num2.ToString(), font, black, num3 + 3f, -num4);
					this.BMG.DrawString(num2.ToString(), font, black, num5 - 12f, -num4);
					this.BMG.ScaleTransform(1f, -1f);
					checked
					{
						num2++;
					}
				}
				while (num2 <= 6);
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001AC0D File Offset: 0x00018E0D
		private void PlotUC_Resize(object sender, EventArgs e)
		{
			this.ResizeControlAndComponets();
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001AC18 File Offset: 0x00018E18
		public void ResizeControlAndComponets()
		{
			this.centerTitle();
			checked
			{
				this.PictureBox1.Left = this.lblYmid.Left + this.lblYmid.Width + 1;
				bool flag = this.PictureBox1.Left < 25;
				if (flag)
				{
					this.PictureBox1.Left = 25;
				}
				this.PictureBox1.Top = 4;
				int num = 5;
				int num2 = 25;
				this.PictureBox1.Width = base.Width - this.PictureBox1.Left - num;
				this.PictureBox1.Height = base.Height - this.PictureBox1.Top - num2;
				int top = this.PictureBox1.Top + this.PictureBox1.Height + 5;
				this.lblCursorX.Top = top;
				this.lblCursorX.Left = 76;
				this.lblCursorY.Top = top;
				this.lblCursorY.Left = 164;
				this.lblXaxis.Top = top;
				this.lblXaxis.Left = (int)Math.Round(unchecked((double)this.PictureBox1.Left + (double)this.PictureBox1.Width / 2.0 - (double)this.lblXaxis.Width / 2.0));
				this.lblXmax.Top = top;
				this.lblXmax.Left = this.PictureBox1.Left + this.PictureBox1.Width - this.lblXmax.Width - 5;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001ADAF File Offset: 0x00018FAF
		private void lblTitle_SizeChanged(object sender, EventArgs e)
		{
			this.centerTitle();
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001ADBC File Offset: 0x00018FBC
		private void centerTitle()
		{
			this.lblTitle.Left = checked((int)Math.Round(unchecked((double)this.PictureBox1.Width / 2.0 - (double)this.lblTitle.Width / 2.0 + (double)this.PictureBox1.Left)));
		}

		// Token: 0x04000158 RID: 344
		private IContainer components;

		// Token: 0x04000177 RID: 375
		public float[] ScaleArrayY;

		// Token: 0x04000178 RID: 376
		public float[] GridGapArray;

		// Token: 0x04000179 RID: 377
		private PlotUC.ScaleTypeEnum _ScaleType;

		// Token: 0x0400017A RID: 378
		private float _Ymax;

		// Token: 0x0400017B RID: 379
		private float _Ymin;

		// Token: 0x0400017C RID: 380
		private float _Yoffset;

		// Token: 0x0400017D RID: 381
		private float _Yscale;

		// Token: 0x0400017E RID: 382
		private float _Xmax;

		// Token: 0x0400017F RID: 383
		private float _Xmin;

		// Token: 0x04000180 RID: 384
		private float _Xoffset;

		// Token: 0x04000181 RID: 385
		public PlotUC.GridClass Grid;

		// Token: 0x04000182 RID: 386
		public PlotUC.TraceCollection Traces;

		// Token: 0x04000183 RID: 387
		private string _Title;

		// Token: 0x04000184 RID: 388
		private string _ScaleUnitsText;

		// Token: 0x04000185 RID: 389
		private bool _alarmsVisible;

		// Token: 0x04000186 RID: 390
		protected bool _ScaleLog10;

		// Token: 0x04000187 RID: 391
		protected AlarmsClass.AxisClass _AlarmValues;

		// Token: 0x04000188 RID: 392
		private PlotUC.ChartTypeEnum _chartType;

		// Token: 0x04000189 RID: 393
		private int _barWidth;

		// Token: 0x0400018A RID: 394
		private int xCounter;

		// Token: 0x0400018B RID: 395
		private bool ContinuousStats;

		// Token: 0x0400018C RID: 396
		private Bitmap BM;

		// Token: 0x0400018D RID: 397
		private Graphics BMG;

		// Token: 0x0400018E RID: 398
		private Label[] LBLaLow;

		// Token: 0x0400018F RID: 399
		private Label[] LBLaHigh;

		// Token: 0x04000190 RID: 400
		private double _xScaleMax;

		// Token: 0x04000191 RID: 401
		private float Ycenter;

		// Token: 0x04000192 RID: 402
		public int YscaleIndex;

		// Token: 0x04000193 RID: 403
		public int GridGapIndex;

		// Token: 0x02000042 RID: 66
		public enum ScaleTypeEnum
		{
			// Token: 0x04000222 RID: 546
			Unsigned = 1,
			// Token: 0x04000223 RID: 547
			Signed,
			// Token: 0x04000224 RID: 548
			FreeForm,
			// Token: 0x04000225 RID: 549
			log10
		}

		// Token: 0x02000043 RID: 67
		public enum ChartTypeEnum
		{
			// Token: 0x04000227 RID: 551
			line = 1,
			// Token: 0x04000228 RID: 552
			bar
		}

		// Token: 0x02000044 RID: 68
		public class GridClass
		{
			// Token: 0x0600049B RID: 1179 RVA: 0x0001C9CC File Offset: 0x0001ABCC
			public GridClass()
			{
				this._XaxisSpacingPixels = 50f;
				this._YaxisSpacingUnits = 10f;
				this._Visible = true;
				this.Color = Color.LightBlue;
				this._Pen = new Pen(this.Color);
				this.LineWidth = 1f;
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x0600049C RID: 1180 RVA: 0x0001CA28 File Offset: 0x0001AC28
			// (set) Token: 0x0600049D RID: 1181 RVA: 0x0001CA40 File Offset: 0x0001AC40
			public Pen Pen
			{
				get
				{
					return this._Pen;
				}
				set
				{
					this._Pen = value;
				}
			}

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x0600049E RID: 1182 RVA: 0x0001CA4C File Offset: 0x0001AC4C
			// (set) Token: 0x0600049F RID: 1183 RVA: 0x0001CA64 File Offset: 0x0001AC64
			public Color Color
			{
				get
				{
					return this._Color;
				}
				set
				{
					this._Color = value;
					this.Pen = new Pen(this._Color);
				}
			}

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0001CA80 File Offset: 0x0001AC80
			// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0001CA98 File Offset: 0x0001AC98
			public float Counter
			{
				get
				{
					return this._Counter;
				}
				set
				{
					this._Counter = value;
				}
			}

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001CAA4 File Offset: 0x0001ACA4
			// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0001CABC File Offset: 0x0001ACBC
			public float LineWidth
			{
				get
				{
					return this._LineWidth;
				}
				set
				{
					this._LineWidth = value;
				}
			}

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001CAC8 File Offset: 0x0001ACC8
			// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0001CAE0 File Offset: 0x0001ACE0
			public bool Visible
			{
				get
				{
					return this._Visible;
				}
				set
				{
					this._Visible = value;
				}
			}

			// Token: 0x1700018F RID: 399
			// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001CAEC File Offset: 0x0001ACEC
			// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0001CB04 File Offset: 0x0001AD04
			public float XaxisSpacingPixels
			{
				get
				{
					return this._XaxisSpacingPixels;
				}
				set
				{
					this._XaxisSpacingPixels = value;
				}
			}

			// Token: 0x17000190 RID: 400
			// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0001CB10 File Offset: 0x0001AD10
			// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0001CB28 File Offset: 0x0001AD28
			public float YaxisSpacingPixels
			{
				get
				{
					return this._YaxisSpacingPixels;
				}
				set
				{
					this._YaxisSpacingPixels = value;
				}
			}

			// Token: 0x17000191 RID: 401
			// (get) Token: 0x060004AA RID: 1194 RVA: 0x0001CB34 File Offset: 0x0001AD34
			// (set) Token: 0x060004AB RID: 1195 RVA: 0x0001CB4C File Offset: 0x0001AD4C
			public float YaxisSpacingUnits
			{
				get
				{
					return this._YaxisSpacingUnits;
				}
				set
				{
					this._YaxisSpacingUnits = value;
				}
			}

			// Token: 0x04000229 RID: 553
			private float _XaxisSpacingPixels;

			// Token: 0x0400022A RID: 554
			private float _YaxisSpacingPixels;

			// Token: 0x0400022B RID: 555
			private float _YaxisSpacingUnits;

			// Token: 0x0400022C RID: 556
			private float _LineWidth;

			// Token: 0x0400022D RID: 557
			private Color _Color;

			// Token: 0x0400022E RID: 558
			private Pen _Pen;

			// Token: 0x0400022F RID: 559
			private float _Counter;

			// Token: 0x04000230 RID: 560
			private bool _Visible;
		}

		// Token: 0x02000045 RID: 69
		[DebuggerDisplay("{Label}")]
		public class TraceClass
		{
			// Token: 0x060004AC RID: 1196 RVA: 0x0001CB58 File Offset: 0x0001AD58
			public TraceClass(string Title, string RegName, Pen Pen, int Index, bool Visible = true)
			{
				this.VertData = new double[512];
				this._Visible = Visible;
				this._Title = Title;
				this._RegName = RegName;
				this._Pen = Pen;
				this._Index = Index;
				checked
				{
					int num = this.VertData.Count<double>() - 1;
					for (int i = 0; i <= num; i++)
					{
						this.VertData[i] = double.NaN;
					}
				}
			}

			// Token: 0x17000192 RID: 402
			// (get) Token: 0x060004AD RID: 1197 RVA: 0x0001CBD0 File Offset: 0x0001ADD0
			// (set) Token: 0x060004AE RID: 1198 RVA: 0x0001CBE8 File Offset: 0x0001ADE8
			public string RegName
			{
				get
				{
					return this._RegName;
				}
				set
				{
					this._RegName = value;
				}
			}

			// Token: 0x17000193 RID: 403
			// (get) Token: 0x060004AF RID: 1199 RVA: 0x0001CBF4 File Offset: 0x0001ADF4
			// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0001CC0C File Offset: 0x0001AE0C
			public string Label
			{
				get
				{
					return this._Title;
				}
				set
				{
					this._Title = value;
				}
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0001CC18 File Offset: 0x0001AE18
			// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0001CC30 File Offset: 0x0001AE30
			public Pen Pen
			{
				get
				{
					return this._Pen;
				}
				set
				{
					this._Pen = value;
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0001CC3C File Offset: 0x0001AE3C
			// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0001CC54 File Offset: 0x0001AE54
			public int Index
			{
				get
				{
					return this._Index;
				}
				set
				{
					this._Index = value;
				}
			}

			// Token: 0x17000196 RID: 406
			// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0001CC60 File Offset: 0x0001AE60
			// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0001CC78 File Offset: 0x0001AE78
			public bool Visible
			{
				get
				{
					return this._Visible;
				}
				set
				{
					this._Visible = value;
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0001CC84 File Offset: 0x0001AE84
			public string ColorString
			{
				get
				{
					return this.Pen.Color.Name;
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0001CCAC File Offset: 0x0001AEAC
			// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0001CCC4 File Offset: 0x0001AEC4
			public double RMS
			{
				get
				{
					return this._RMS;
				}
				set
				{
					this._RMS = value;
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x060004BA RID: 1210 RVA: 0x0001CCD0 File Offset: 0x0001AED0
			// (set) Token: 0x060004BB RID: 1211 RVA: 0x0001CCE8 File Offset: 0x0001AEE8
			public double Average
			{
				get
				{
					return this._Average;
				}
				set
				{
					this._Average = value;
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x060004BC RID: 1212 RVA: 0x0001CCF4 File Offset: 0x0001AEF4
			// (set) Token: 0x060004BD RID: 1213 RVA: 0x0001CD0C File Offset: 0x0001AF0C
			public double PKPK
			{
				get
				{
					return this._PKPK;
				}
				set
				{
					this._PKPK = value;
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x060004BE RID: 1214 RVA: 0x0001CD18 File Offset: 0x0001AF18
			// (set) Token: 0x060004BF RID: 1215 RVA: 0x0001CD30 File Offset: 0x0001AF30
			public double pk1
			{
				get
				{
					return this._pk1;
				}
				set
				{
					this._pk1 = value;
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x060004C0 RID: 1216 RVA: 0x0001CD3C File Offset: 0x0001AF3C
			// (set) Token: 0x060004C1 RID: 1217 RVA: 0x0001CD54 File Offset: 0x0001AF54
			public double pk2
			{
				get
				{
					return this._pk2;
				}
				set
				{
					this._pk2 = value;
				}
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001CD60 File Offset: 0x0001AF60
			// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0001CD78 File Offset: 0x0001AF78
			public double pk3
			{
				get
				{
					return this._pk3;
				}
				set
				{
					this._pk3 = value;
				}
			}

			// Token: 0x060004C4 RID: 1220 RVA: 0x0001CD84 File Offset: 0x0001AF84
			public void StatsCalc(string statType)
			{
				double num = 0.0;
				double num2 = this.VertData[0];
				double num3 = this.VertData[0];
				checked
				{
					int num4 = this.VertData.Count<double>() - 1;
					bool flag = num4 > 0;
					if (flag)
					{
						int num5 = num4;
						for (int i = 0; i <= num5; i++)
						{
							double num6 = this.VertData[i];
							unchecked
							{
								num += num6;
								bool flag2 = num2 < num6;
								if (flag2)
								{
									num2 = num6;
								}
								bool flag3 = num3 > num6;
								if (flag3)
								{
									num3 = num6;
								}
							}
						}
						double num7 = num / (double)num4;
						double pkpk = unchecked(num2 - num3);
						num = 0.0;
						int num8 = num4;
						for (int i = 0; i <= num8; i++)
						{
							unchecked
							{
								num += Math.Abs(this.VertData[i] - num7);
							}
						}
						double rms = num / (double)num4;
						this.Average = num7;
						this.PKPK = pkpk;
						this.RMS = rms;
					}
					double[] array = new double[this.VertData.Count<double>() + 1];
					this.VertData.CopyTo(array, 0);
					this.pk1 = this.VertData.Max();
					this.pk2 = this.VertData.Max();
					this.pk3 = this.VertData.Max();
					double num9 = 0.0;
					int num10 = 0;
					int num11 = array.Count<double>() - 1;
					for (int j = 0; j <= num11; j++)
					{
						bool flag4 = array[j] > num9;
						if (flag4)
						{
							num9 = array[j];
							num10 = j;
						}
					}
					this.pk1 = num9;
					array[num10] = 0.0;
					num9 = 0.0;
					int num12 = array.Count<double>() - 1;
					for (int k = 0; k <= num12; k++)
					{
						bool flag5 = array[k] > num9;
						if (flag5)
						{
							num9 = array[k];
							num10 = k;
						}
					}
					this.pk2 = num9;
					array[num10] = 0.0;
					num9 = 0.0;
					int num13 = array.Count<double>() - 1;
					for (int l = 0; l <= num13; l++)
					{
						bool flag6 = array[l] > num9;
						if (flag6)
						{
							num9 = array[l];
							num10 = l;
						}
					}
					this.pk3 = num9;
					array[num10] = 0.0;
				}
			}

			// Token: 0x04000231 RID: 561
			private string _Title;

			// Token: 0x04000232 RID: 562
			private string _RegName;

			// Token: 0x04000233 RID: 563
			private int _Index;

			// Token: 0x04000234 RID: 564
			public double[] VertData;

			// Token: 0x04000235 RID: 565
			private bool _Visible;

			// Token: 0x04000236 RID: 566
			private double _RMS;

			// Token: 0x04000237 RID: 567
			private double _Average;

			// Token: 0x04000238 RID: 568
			private double _PKPK;

			// Token: 0x04000239 RID: 569
			private Pen _Pen;

			// Token: 0x0400023A RID: 570
			private double _pk1;

			// Token: 0x0400023B RID: 571
			private double _pk2;

			// Token: 0x0400023C RID: 572
			private double _pk3;

			// Token: 0x0200004E RID: 78
			public enum statTypeEnum
			{
				// Token: 0x04000250 RID: 592
				frequency,
				// Token: 0x04000251 RID: 593
				timeDomain
			}
		}

		// Token: 0x02000046 RID: 70
		public class TraceCollection : KeyedCollection<string, PlotUC.TraceClass>
		{
			// Token: 0x060004C6 RID: 1222 RVA: 0x0001CFFB File Offset: 0x0001B1FB
			public void AddTrace(PlotUC.TraceClass NewTrace)
			{
				base.Add(NewTrace);
			}

			// Token: 0x060004C7 RID: 1223 RVA: 0x0001D008 File Offset: 0x0001B208
			protected override string GetKeyForItem(PlotUC.TraceClass item)
			{
				return item.Label;
			}

			/// <summary>
			/// Return index of a given register in the map.
			/// </summary>
			/// <param name="key">Key string of register to process.</param>
			/// <returns></returns>
			/// <remarks></remarks>
			// Token: 0x060004C8 RID: 1224 RVA: 0x0001D020 File Offset: 0x0001B220
			public int IndexOf(string key)
			{
				return base.IndexOf(base[key]);
			}

			// Token: 0x060004C9 RID: 1225 RVA: 0x0001D040 File Offset: 0x0001B240
			public new int IndexOf(PlotUC.TraceClass Trace)
			{
				return base.IndexOf(Trace);
			}
		}
	}
}
