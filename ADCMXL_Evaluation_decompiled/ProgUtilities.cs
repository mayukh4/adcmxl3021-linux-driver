using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using FX3Api;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Vibration_Evaluation.My;
using Vibration_Evaluation.My.Resources;

namespace Vibration_Evaluation
{
	// Token: 0x02000022 RID: 34
	public class ProgUtilities
	{
		// Token: 0x060003D2 RID: 978 RVA: 0x0001AE64 File Offset: 0x00019064
		public bool ScanForBlackFin()
		{
			bool flag = false;
			bool flag4;
			do
			{
				flag = false;
				try
				{
					GlobalDeclarations.BoardID = GlobalDeclarations.BoardIDtype.SDPEVAL;
				}
				catch (Exception ex)
				{
					string text = "Unable to connect to an EVAL-ADIS board.";
					text = text + "\r\n\r\nError Message: " + ex.Message;
					Interaction.MsgBox(text, MsgBoxStyle.OkOnly, Application.ProductName);
					GlobalDeclarations.BoardID = GlobalDeclarations.BoardIDtype.NONE;
					return false;
				}
				bool flag2 = GlobalDeclarations.BoardID == GlobalDeclarations.BoardIDtype.SDPEVAL;
				if (flag2)
				{
					MyProject.Forms.FormMessage.ShowMessage("Updating EVAL-ADIS Firmware", "USB utilities");
					try
					{
						byte[] eval_SDRAM = Resources.EVAL_SDRAM;
						Thread.Sleep(1000);
					}
					catch (Exception ex2)
					{
						string text2 = "Error encountered while updating EVAL-ADIS Firmware.";
						text2 = text2 + "\r\n\r\nError Message: " + ex2.Message;
						int num = (int)MessageBox.Show(text2, Application.ProductName, MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation);
						bool flag3 = num == 2;
						if (flag3)
						{
							flag = true;
						}
					}
					MyProject.Forms.FormMessage.Hide();
				}
				flag4 = (GlobalDeclarations.BoardID == GlobalDeclarations.BoardIDtype.SDPEVAL || flag);
			}
			while (!flag4);
			return GlobalDeclarations.BoardID == GlobalDeclarations.BoardIDtype.SDPEVAL;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001AFAC File Offset: 0x000191AC
		public bool FX3connect()
		{
			GlobalDeclarations.FX3devicetype = DeviceType.ADcmXL;
			bool result = false;
			string path = "";
			string path2 = "";
			string text = AppDomain.CurrentDomain.BaseDirectory + "Resources\\";
			try
			{
				GlobalDeclarations.FX3comm = new FX3Connection(text, text, text, DeviceType.ADcmXL);
				bool flag = GlobalDeclarations.FX3comm.WaitForBoard(2);
				bool flag2 = !flag & GlobalDeclarations.FX3comm.BusyFX3s.Count > 0;
				if (flag2)
				{
					GlobalDeclarations.FX3comm.ResetAllFX3s();
				}
				bool flag3 = !flag;
				if (flag3)
				{
					bool flag4 = !GlobalDeclarations.FX3comm.WaitForBoard(15);
					if (flag4)
					{
						throw new Exception("FX3 board search timed out");
					}
				}
				GlobalDeclarations.FX3comm.Connect(GlobalDeclarations.FX3comm.AvailableFX3s[0]);
				result = true;
				GlobalDeclarations.FX3comm.StallTime = 18;
				GlobalDeclarations.BoardID = GlobalDeclarations.BoardIDtype.FX3;
				GlobalDeclarations.FX3comm.Reset();
			}
			catch (Exception ex)
			{
				string text2 = "Unable to connect to an FX3 board.";
				text2 = text2 + "\r\n\r\nError Message: \r\n" + ex.Message;
				Interaction.MsgBox(text2, MsgBoxStyle.OkOnly, Application.ProductName);
				GlobalDeclarations.BoardID = GlobalDeclarations.BoardIDtype.NONE;
			}
			MyProject.Forms.FormMessage.Hide();
			bool flag5 = File.Exists(path);
			if (flag5)
			{
				File.Delete(path);
			}
			bool flag6 = File.Exists(path2);
			if (flag6)
			{
				File.Delete(path2);
			}
			return result;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001B12C File Offset: 0x0001932C
		public string WriteToTempFile(byte[] bytes)
		{
			string text = Path.GetTempFileName();
			int length = checked(text.Length - 4);
			text = Strings.Left(text, length);
			text += ".img";
			MyProject.Computer.FileSystem.WriteAllBytes(text, bytes, true);
			return text;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001B178 File Offset: 0x00019378
		public byte[] UShortToByteArray(ref IEnumerable<ushort> ShortArray)
		{
			checked
			{
				byte[] array = new byte[2 * ShortArray.Count<ushort>() - 1 + 1];
				int num = ShortArray.Count<ushort>() - 1;
				for (int i = 0; i <= num; i++)
				{
					array[2 * i + 1] = (byte)(ShortArray.ElementAtOrDefault(i) & 255);
					array[2 * i] = (byte)((ShortArray.ElementAtOrDefault(i) & 65280) >> 8);
				}
				return array;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001B1E0 File Offset: 0x000193E0
		public bool SearchWMI()
		{
			try
			{
				ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * From Win32_PnPEntity");
				try
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectSearcher.Get())
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						string text = Conversions.ToString(managementObject["Caption"]);
					}
				}
				finally
				{
					ManagementObjectCollection.ManagementObjectEnumerator enumerator;
					if (enumerator != null)
					{
						((IDisposable)enumerator).Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			bool result;
			return result;
		}

		/// <summary>
		/// parameters of the data log
		/// </summary>
		// Token: 0x02000049 RID: 73
		public class DatalogClass
		{
			// Token: 0x060004CA RID: 1226 RVA: 0x0001D05C File Offset: 0x0001B25C
			public DatalogClass()
			{
				this._path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				this._FileName = "Datalog.csv";
				this._selectedRegisters = new List<string>();
				this._nSamples = 1000UL;
				this._msecDelay = 0;
				this._fileHeader = true;
				this._scaledData = true;
				this._fileCount = 0UL;
				this._delimiter = ",";
				this._selectedRegisters.Add("1");
			}

			/// <summary>
			/// Calls upDate GUI which effects all controls registered with StatusClass
			/// </summary>
			// Token: 0x1700019E RID: 414
			// (get) Token: 0x060004CB RID: 1227 RVA: 0x0001D0D8 File Offset: 0x0001B2D8
			// (set) Token: 0x060004CC RID: 1228 RVA: 0x0001D0F0 File Offset: 0x0001B2F0
			public string Path
			{
				get
				{
					return this._path;
				}
				set
				{
					this._path = value;
				}
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x060004CD RID: 1229 RVA: 0x0001D0FC File Offset: 0x0001B2FC
			// (set) Token: 0x060004CE RID: 1230 RVA: 0x0001D114 File Offset: 0x0001B314
			public string FileName
			{
				get
				{
					return this._FileName;
				}
				set
				{
					this._FileName = value;
				}
			}

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x060004CF RID: 1231 RVA: 0x0001D120 File Offset: 0x0001B320
			// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0001D138 File Offset: 0x0001B338
			public List<string> SelectedRegisters
			{
				get
				{
					return this._selectedRegisters;
				}
				set
				{
					this._selectedRegisters = value;
				}
			}

			// Token: 0x170001A1 RID: 417
			// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001D144 File Offset: 0x0001B344
			// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0001D15C File Offset: 0x0001B35C
			public ulong nSamples
			{
				get
				{
					return this._nSamples;
				}
				set
				{
					this._nSamples = value;
				}
			}

			// Token: 0x170001A2 RID: 418
			// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0001D168 File Offset: 0x0001B368
			// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0001D180 File Offset: 0x0001B380
			public ulong FileCount
			{
				get
				{
					return this._fileCount;
				}
				set
				{
					this._fileCount = value;
				}
			}

			// Token: 0x170001A3 RID: 419
			// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0001D18C File Offset: 0x0001B38C
			// (set) Token: 0x060004D6 RID: 1238 RVA: 0x0001D1A4 File Offset: 0x0001B3A4
			public string delimiter
			{
				get
				{
					return this._delimiter;
				}
				set
				{
					this._delimiter = value;
				}
			}

			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0001D1B0 File Offset: 0x0001B3B0
			// (set) Token: 0x060004D8 RID: 1240 RVA: 0x0001D1C8 File Offset: 0x0001B3C8
			public int msecDelay
			{
				get
				{
					return this._msecDelay;
				}
				set
				{
					this._msecDelay = value;
				}
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0001D1D4 File Offset: 0x0001B3D4
			// (set) Token: 0x060004DA RID: 1242 RVA: 0x0001D1EC File Offset: 0x0001B3EC
			public bool fileHeader
			{
				get
				{
					return this._fileHeader;
				}
				set
				{
					this._fileHeader = value;
				}
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
			// (set) Token: 0x060004DC RID: 1244 RVA: 0x0001D210 File Offset: 0x0001B410
			public bool scaledData
			{
				get
				{
					return this._scaledData;
				}
				set
				{
					this._scaledData = value;
				}
			}

			// Token: 0x04000245 RID: 581
			private string _path;

			// Token: 0x04000246 RID: 582
			private string _FileName;

			// Token: 0x04000247 RID: 583
			private List<string> _selectedRegisters;

			// Token: 0x04000248 RID: 584
			private ulong _nSamples;

			// Token: 0x04000249 RID: 585
			private int _msecDelay;

			// Token: 0x0400024A RID: 586
			private bool _fileHeader;

			// Token: 0x0400024B RID: 587
			private bool _scaledData;

			// Token: 0x0400024C RID: 588
			private ulong _fileCount;

			// Token: 0x0400024D RID: 589
			private string _delimiter;
		}
	}
}
