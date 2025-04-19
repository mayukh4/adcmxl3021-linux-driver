using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Vibration_Evaluation
{
	// Token: 0x0200000E RID: 14
	public class DeviceCollection : KeyedCollection<string, DeviceClass>
	{
		/// <summary>
		/// Returns true if a parsing error was encountered during the previous reg map file read operation.
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00006800 File Offset: 0x00004A00
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00006818 File Offset: 0x00004A18
		public bool ErrorFound
		{
			get
			{
				return this._ErrorFound;
			}
			private set
			{
				this._ErrorFound = value;
			}
		}

		/// <summary>
		/// Returns a multiline string containing descriptions of reg map file parsing errors.
		/// </summary>
		/// <value></value>
		/// <returns></returns>
		/// <remarks></remarks>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00006824 File Offset: 0x00004A24
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x0000683C File Offset: 0x00004A3C
		public string ErrorText
		{
			get
			{
				return this._ErrorText;
			}
			private set
			{
				this._ErrorText = value;
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006848 File Offset: 0x00004A48
		public DeviceCollection()
		{
			this.myColumns = new string[]
			{
				"Label",
				"ProductID",
				"DutControl",
				"DutInterface",
				"RegMapName",
				"CmdMapName",
				"AccelAxes",
				"GyroAxes",
				"HasMagnetometer",
				"HasBarometer",
				"HasOrientation"
			};
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000068C4 File Offset: 0x00004AC4
		protected override string GetKeyForItem(DeviceClass item)
		{
			return item.Label;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000068DC File Offset: 0x00004ADC
		public int IndexOf(string key)
		{
			return base.IndexOf(base[key]);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000068FC File Offset: 0x00004AFC
		public new int IndexOf(DeviceClass dev)
		{
			return base.IndexOf(dev);
		}

		/// <summary>
		/// Creates a Reg Collection Object based on a multiline string formatted as a RegFile.
		/// </summary>
		/// <param name="text"></param>
		/// <returns>Returns true if parses sucessfully, False if an error was emcountered.</returns>
		/// <remarks></remarks>
		// Token: 0x060000C6 RID: 198 RVA: 0x00006918 File Offset: 0x00004B18
		public bool CreateCollection(string text)
		{
			return this.CreateCollection(text.Split(new char[]
			{
				'\n'
			}));
		}

		/// <summary>
		/// Creates a Reg Collection Object based on a multiline string formatted as a RegFile.
		/// </summary>
		/// <param name="lines"></param>
		/// <returns>Returns true if parses sucessfully, False if an error was emcountered.</returns>
		/// <remarks></remarks>
		// Token: 0x060000C7 RID: 199 RVA: 0x00006944 File Offset: 0x00004B44
		public bool CreateCollection(IList<string> lines)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			base.Clear();
			this.ErrorFound = false;
			this.ErrorText = "";
			try
			{
				foreach (string text in lines)
				{
					bool flag = text.Trim().Length != 0 && text.Trim().First<char>() != '*';
					if (flag)
					{
						List<string> list = Strings.Split(text, ",", -1, CompareMethod.Binary).Select((DeviceCollection._Closure$__.$I15-0 == null) ? (DeviceCollection._Closure$__.$I15-0 = ((string x) => x.Trim())) : DeviceCollection._Closure$__.$I15-0).ToList<string>();
						bool flag2 = dictionary.Count == 0;
						if (flag2)
						{
							dictionary = this.ParseColTable(list);
						}
						else
						{
							bool flag3 = list.Count < dictionary.Count;
							if (flag3)
							{
								this.ErrorFound = true;
								this.ErrorText = "Line does not have enough columns: " + text + "\r\n";
							}
							else
							{
								base.Add(new DeviceClass
								{
									Label = this.ParseString(list[dictionary["Label"]]),
									CmdMapName = this.ParseString(list[dictionary["CmdMapName"]]),
									RegMapName = this.ParseString(list[dictionary["RegMapName"]]),
									HasBarometer = this.ParseBoolean(list[dictionary["HasBarometer"]]),
									HasMagnetometer = this.ParseBoolean(list[dictionary["HasMagnetometer"]]),
									HasOrientation = this.ParseBoolean(list[dictionary["HasOrientation"]]),
									DutInterfaceType = this.ParseDutInterface(list[dictionary["DutInterface"]]),
									DutControlType = this.ParseDutControl(list[dictionary["DutControl"]]),
									ProductID = this.ParseInteger(list[dictionary["ProductID"]]),
									AccelAxes = this.ParseInteger(list[dictionary["AccelAxes"]]),
									GyroAxes = this.ParseInteger(list[dictionary["GyroAxes"]])
								});
							}
						}
					}
					bool errorFound = this.ErrorFound;
					if (errorFound)
					{
						break;
					}
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
			return !this.ErrorFound;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00006C10 File Offset: 0x00004E10
		private bool ParseBoolean(string s)
		{
			string left = s.ToUpperInvariant();
			bool flag = Operators.CompareString(left, "1", false) == 0 || Operators.CompareString(left, "TRUE", false) == 0 || Operators.CompareString(left, "T", false) == 0;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = Operators.CompareString(left, "0", false) == 0 || Operators.CompareString(left, "FALSE", false) == 0 || Operators.CompareString(left, "F", false) == 0;
				if (flag2)
				{
					result = false;
				}
				else
				{
					this.ErrorFound = true;
					this.ErrorText = this.ErrorText + "Cannot convert " + s + " to Boolean.\r\n";
					result = false;
				}
			}
			return result;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00006CBC File Offset: 0x00004EBC
		private ControlType ParseDutControl(string s)
		{
			bool flag = string.Equals(s, "ADcmXL3021", StringComparison.InvariantCultureIgnoreCase);
			ControlType result;
			if (flag)
			{
				result = ControlType.ADcmXL3021;
			}
			else
			{
				this.ErrorFound = true;
				this.ErrorText = this.ErrorText + "Cannot convert " + s + " to ControlType.\r\n";
			}
			return result;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006D08 File Offset: 0x00004F08
		private InterfaceType ParseDutInterface(string s)
		{
			bool flag = string.Equals(s, "aduc", StringComparison.InvariantCultureIgnoreCase);
			InterfaceType result;
			if (flag)
			{
				result = InterfaceType.aduc;
			}
			else
			{
				bool flag2 = string.Equals(s, "adbf", StringComparison.InvariantCultureIgnoreCase);
				if (flag2)
				{
					result = InterfaceType.adbf;
				}
				else
				{
					this.ErrorFound = true;
					this.ErrorText = this.ErrorText + "Cannot convert " + s + " to InterfaceType.\r\n";
				}
			}
			return result;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00006D68 File Offset: 0x00004F68
		private int ParseInteger(string s)
		{
			int num;
			bool flag = int.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out num);
			int result;
			if (flag)
			{
				result = num;
			}
			else
			{
				this.ErrorFound = true;
				this.ErrorText = this.ErrorText + "Cannot convert " + s + " to Integer.\r\n";
				result = 0;
			}
			return result;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006DB8 File Offset: 0x00004FB8
		private string ParseString(string s)
		{
			bool flag = string.IsNullOrEmpty(s);
			string result;
			if (flag)
			{
				this.ErrorFound |= true;
				this.ErrorText += "String fields may not be blank.\r\n";
				result = "";
			}
			else
			{
				result = s;
			}
			return result;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006E04 File Offset: 0x00005004
		private Dictionary<string, int> ParseColTable(IList<string> Fields)
		{
			DeviceCollection._Closure$__21-1 CS$<>8__locals1 = new DeviceCollection._Closure$__21-1(CS$<>8__locals1);
			CS$<>8__locals1.$VB$Local_colTable = new Dictionary<string, int>();
			checked
			{
				int num = Fields.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					DeviceCollection._Closure$__21-0 CS$<>8__locals2 = new DeviceCollection._Closure$__21-0(CS$<>8__locals2);
					CS$<>8__locals2.$VB$Local_title = Fields[i].Trim();
					string text = (from x in this.myColumns
					where string.Equals(x, CS$<>8__locals2.$VB$Local_title, StringComparison.InvariantCultureIgnoreCase)
					select x).FirstOrDefault<string>();
					bool flag = !string.IsNullOrEmpty(text);
					if (flag)
					{
						CS$<>8__locals1.$VB$Local_colTable.Add(text, i);
					}
				}
				try
				{
					foreach (string str in this.myColumns.Where((CS$<>8__locals1.$I1 == null) ? (CS$<>8__locals1.$I1 = ((string x) => !CS$<>8__locals1.$VB$Local_colTable.ContainsKey(x))) : CS$<>8__locals1.$I1))
					{
						this.ErrorFound = true;
						this.ErrorText = this.ErrorText + "Missing Required Column: " + str + ".\r\n";
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
				return CS$<>8__locals1.$VB$Local_colTable;
			}
		}

		// Token: 0x0400004D RID: 77
		private const StringComparison ignoreCase = StringComparison.InvariantCultureIgnoreCase;

		// Token: 0x0400004E RID: 78
		private string[] myColumns;

		// Token: 0x0400004F RID: 79
		private bool _ErrorFound;

		// Token: 0x04000050 RID: 80
		private string _ErrorText;
	}
}
