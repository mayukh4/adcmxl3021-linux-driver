using System;
using System.Diagnostics;

namespace Vibration_Evaluation
{
	// Token: 0x0200000D RID: 13
	[DebuggerDisplay("{Label}")]
	public class DeviceClass
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00006674 File Offset: 0x00004874
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x0000668C File Offset: 0x0000488C
		public bool HasBarometer
		{
			get
			{
				return this._HasBarometer;
			}
			set
			{
				this._HasBarometer = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00006698 File Offset: 0x00004898
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000066B0 File Offset: 0x000048B0
		public bool HasMagnetometer
		{
			get
			{
				return this._HasMagnetometer;
			}
			set
			{
				this._HasMagnetometer = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000066BC File Offset: 0x000048BC
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000066D4 File Offset: 0x000048D4
		public bool HasOrientation
		{
			get
			{
				return this._HasOrientation;
			}
			set
			{
				this._HasOrientation = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000066E0 File Offset: 0x000048E0
		// (set) Token: 0x060000AF RID: 175 RVA: 0x000066F8 File Offset: 0x000048F8
		public int AccelAxes
		{
			get
			{
				return this._AccelAxes;
			}
			set
			{
				this._AccelAxes = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00006704 File Offset: 0x00004904
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x0000671C File Offset: 0x0000491C
		public string CmdMapName
		{
			get
			{
				return this._CmdMapName;
			}
			set
			{
				this._CmdMapName = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00006728 File Offset: 0x00004928
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00006740 File Offset: 0x00004940
		public ControlType DutControlType
		{
			get
			{
				return this._DutControlType;
			}
			set
			{
				this._DutControlType = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000674C File Offset: 0x0000494C
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00006764 File Offset: 0x00004964
		public InterfaceType DutInterfaceType
		{
			get
			{
				return this._DutInterfaceType;
			}
			set
			{
				this._DutInterfaceType = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00006770 File Offset: 0x00004970
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00006788 File Offset: 0x00004988
		public int GyroAxes
		{
			get
			{
				return this._GyroAxes;
			}
			set
			{
				this._GyroAxes = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00006794 File Offset: 0x00004994
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x000067AC File Offset: 0x000049AC
		public string Label
		{
			get
			{
				return this._Label;
			}
			set
			{
				this._Label = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000067B8 File Offset: 0x000049B8
		// (set) Token: 0x060000BB RID: 187 RVA: 0x000067D0 File Offset: 0x000049D0
		public int ProductID
		{
			get
			{
				return this._ProductID;
			}
			set
			{
				this._ProductID = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000067DC File Offset: 0x000049DC
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000067F4 File Offset: 0x000049F4
		public string RegMapName
		{
			get
			{
				return this._RegMapName;
			}
			set
			{
				this._RegMapName = value;
			}
		}

		// Token: 0x04000042 RID: 66
		private bool _HasBarometer;

		// Token: 0x04000043 RID: 67
		private bool _HasMagnetometer;

		// Token: 0x04000044 RID: 68
		private bool _HasOrientation;

		// Token: 0x04000045 RID: 69
		private int _AccelAxes;

		// Token: 0x04000046 RID: 70
		private string _CmdMapName;

		// Token: 0x04000047 RID: 71
		private ControlType _DutControlType;

		// Token: 0x04000048 RID: 72
		private InterfaceType _DutInterfaceType;

		// Token: 0x04000049 RID: 73
		private int _GyroAxes;

		// Token: 0x0400004A RID: 74
		private string _Label;

		// Token: 0x0400004B RID: 75
		private int _ProductID;

		// Token: 0x0400004C RID: 76
		private string _RegMapName;
	}
}
