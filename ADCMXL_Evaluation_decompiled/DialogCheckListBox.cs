using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace Vibration_Evaluation
{
	// Token: 0x0200000F RID: 15
	[DesignerGenerated]
	public partial class DialogCheckListBox : Form
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00006F38 File Offset: 0x00005138
		public DialogCheckListBox()
		{
			base.Load += this.DialogCheckListBox_Load;
			this.CheckedItemList = new List<string>();
			this.InitializeComponent();
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00007496 File Offset: 0x00005696
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x000074A0 File Offset: 0x000056A0
		internal virtual TableLayoutPanel TableLayoutPanel1 { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000074A9 File Offset: 0x000056A9
		// (set) Token: 0x060000D4 RID: 212 RVA: 0x000074B4 File Offset: 0x000056B4
		internal virtual Button OK_Button
		{
			[CompilerGenerated]
			get
			{
				return this._OK_Button;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.OK_Button_Click);
				Button ok_Button = this._OK_Button;
				if (ok_Button != null)
				{
					ok_Button.Click -= value2;
				}
				this._OK_Button = value;
				ok_Button = this._OK_Button;
				if (ok_Button != null)
				{
					ok_Button.Click += value2;
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000074F7 File Offset: 0x000056F7
		// (set) Token: 0x060000D6 RID: 214 RVA: 0x00007504 File Offset: 0x00005704
		internal virtual Button Cancel_Button
		{
			[CompilerGenerated]
			get
			{
				return this._Cancel_Button;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.Cancel_Button_Click);
				Button cancel_Button = this._Cancel_Button;
				if (cancel_Button != null)
				{
					cancel_Button.Click -= value2;
				}
				this._Cancel_Button = value;
				cancel_Button = this._Cancel_Button;
				if (cancel_Button != null)
				{
					cancel_Button.Click += value2;
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x00007547 File Offset: 0x00005747
		// (set) Token: 0x060000D8 RID: 216 RVA: 0x00007551 File Offset: 0x00005751
		internal virtual Label LabelPrompt { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000755A File Offset: 0x0000575A
		// (set) Token: 0x060000DA RID: 218 RVA: 0x00007564 File Offset: 0x00005764
		internal virtual CheckedListBox CheckList { get; [MethodImpl(MethodImplOptions.Synchronized)] set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000756D File Offset: 0x0000576D
		// (set) Token: 0x060000DC RID: 220 RVA: 0x00007578 File Offset: 0x00005778
		internal virtual Button ButtonCheckAll
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonCheckAll;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonCheckAll_Click);
				Button buttonCheckAll = this._ButtonCheckAll;
				if (buttonCheckAll != null)
				{
					buttonCheckAll.Click -= value2;
				}
				this._ButtonCheckAll = value;
				buttonCheckAll = this._ButtonCheckAll;
				if (buttonCheckAll != null)
				{
					buttonCheckAll.Click += value2;
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000075BB File Offset: 0x000057BB
		// (set) Token: 0x060000DE RID: 222 RVA: 0x000075C8 File Offset: 0x000057C8
		internal virtual Button ButtonClearAll
		{
			[CompilerGenerated]
			get
			{
				return this._ButtonClearAll;
			}
			[CompilerGenerated]
			[MethodImpl(MethodImplOptions.Synchronized)]
			set
			{
				EventHandler value2 = new EventHandler(this.ButtonClearAll_Click);
				Button buttonClearAll = this._ButtonClearAll;
				if (buttonClearAll != null)
				{
					buttonClearAll.Click -= value2;
				}
				this._ButtonClearAll = value;
				buttonClearAll = this._ButtonClearAll;
				if (buttonClearAll != null)
				{
					buttonClearAll.Click += value2;
				}
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000760C File Offset: 0x0000580C
		public void AddRange(IEnumerable<string> items)
		{
			try
			{
				foreach (string item in items)
				{
					this.CheckList.Items.Add(item);
				}
			}
			finally
			{
				IEnumerator<string> enumerator;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007664 File Offset: 0x00005864
		private void DialogCheckListBox_Load(object sender, EventArgs e)
		{
			this.CheckedItemList.Clear();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00007674 File Offset: 0x00005874
		private void OK_Button_Click(object sender, EventArgs e)
		{
			this.CheckedItemList.AddRange(this.CheckList.CheckedItems.Cast<object>().Select((DialogCheckListBox._Closure$__.$I35-0 == null) ? (DialogCheckListBox._Closure$__.$I35-0 = ((object x) => x.ToString())) : DialogCheckListBox._Closure$__.$I35-0));
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000076D6 File Offset: 0x000058D6
		private void Cancel_Button_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.Cancel;
			base.Close();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000076E8 File Offset: 0x000058E8
		private void ButtonCheckAll_Click(object sender, EventArgs e)
		{
			checked
			{
				int num = this.CheckList.Items.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					this.CheckList.SetItemChecked(i, true);
				}
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00007724 File Offset: 0x00005924
		private void ButtonClearAll_Click(object sender, EventArgs e)
		{
			checked
			{
				int num = this.CheckList.Items.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					this.CheckList.SetItemChecked(i, false);
				}
			}
		}

		// Token: 0x04000059 RID: 89
		public List<string> CheckedItemList;
	}
}
