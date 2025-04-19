using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Vibration_Evaluation
{
	// Token: 0x0200001D RID: 29
	[DesignerGenerated]
	public partial class FormXX : Form
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x00016A51 File Offset: 0x00014C51
		public FormXX()
		{
			this.InitializeComponent();
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000176A1 File Offset: 0x000158A1
		// (set) Token: 0x060002EC RID: 748 RVA: 0x000176AB File Offset: 0x000158AB
		internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000176B4 File Offset: 0x000158B4
		// (set) Token: 0x060002EE RID: 750 RVA: 0x000176BE File Offset: 0x000158BE
		internal virtual PlotUC PlotUC1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000176C7 File Offset: 0x000158C7
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x000176D1 File Offset: 0x000158D1
		internal virtual PlotUC PlotUC2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000176DA File Offset: 0x000158DA
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x000176E4 File Offset: 0x000158E4
		internal virtual PlotUC PlotUC3 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x000176ED File Offset: 0x000158ED
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x000176F7 File Offset: 0x000158F7
		internal virtual PlotUC PlotUC4 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00017700 File Offset: 0x00015900
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0001770A File Offset: 0x0001590A
		internal virtual PlotUC PlotUC5 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00017713 File Offset: 0x00015913
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0001771D File Offset: 0x0001591D
		internal virtual PlotUC PlotUC6 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00017726 File Offset: 0x00015926
		// (set) Token: 0x060002FA RID: 762 RVA: 0x00017730 File Offset: 0x00015930
		internal virtual TableLayoutPanel TableLayoutPanel2 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00017739 File Offset: 0x00015939
		// (set) Token: 0x060002FC RID: 764 RVA: 0x00017743 File Offset: 0x00015943
		internal virtual MenuStrip MenuStrip1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0001774C File Offset: 0x0001594C
		// (set) Token: 0x060002FE RID: 766 RVA: 0x00017758 File Offset: 0x00015958
		internal virtual ToolStripMenuItem MEnuToolStripMenuItem
		{
			[CompilerGenerated]
			get
			{
				return this._MEnuToolStripMenuItem;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.MEnuToolStripMenuItem_Click);
				ToolStripMenuItem menuToolStripMenuItem = this._MEnuToolStripMenuItem;
				if (menuToolStripMenuItem != null)
				{
					menuToolStripMenuItem.Click -= value2;
				}
				this._MEnuToolStripMenuItem = value;
				menuToolStripMenuItem = this._MEnuToolStripMenuItem;
				if (menuToolStripMenuItem != null)
				{
					menuToolStripMenuItem.Click += value2;
				}
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00003198 File Offset: 0x00001398
		private void MEnuToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}
	}
}
