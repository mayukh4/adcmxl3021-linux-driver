using System;
using adisInterface;
using FX3Api;
using Microsoft.VisualBasic.CompilerServices;
using RegMapClasses;

namespace Vibration_Evaluation
{
	// Token: 0x02000021 RID: 33
	[StandardModule]
	internal sealed class GlobalDeclarations
	{
		// Token: 0x04000194 RID: 404
		public static CommandCollection Cmd = new CommandCollection();

		// Token: 0x04000195 RID: 405
		public static RegMapCollection Reg = new RegMapCollection();

		// Token: 0x04000196 RID: 406
		public static IDutInterface Dut;

		// Token: 0x04000197 RID: 407
		public static AdcmInterface3Axis Dutcmi3x;

		// Token: 0x04000198 RID: 408
		public static AdcmInterface2Axis Dutcmi2x;

		// Token: 0x04000199 RID: 409
		public static AdcmInterface1Axis Dutcmi1x;

		// Token: 0x0400019A RID: 410
		public static ProgUtilities progUt = new ProgUtilities();

		// Token: 0x0400019B RID: 411
		public static ProgUtilities.DatalogClass DatalogUt = new ProgUtilities.DatalogClass();

		// Token: 0x0400019C RID: 412
		public static FX3Connection FX3comm;

		// Token: 0x0400019D RID: 413
		public static FX3SPIConfig fX3spiConfig = new FX3SPIConfig(DeviceType.IMU, FX3BoardType.CypressFX3Board);

		// Token: 0x0400019E RID: 414
		public static DeviceType FX3devicetype = DeviceType.IMU;

		// Token: 0x0400019F RID: 415
		public static GlobalDeclarations.TFSMconfigSTRC TFSMconfig;

		// Token: 0x040001A0 RID: 416
		public static GlobalDeclarations.BoardIDtype BoardID;

		// Token: 0x040001A1 RID: 417
		public static int SPIsclkDefault;

		// Token: 0x040001A2 RID: 418
		public static int SPIsclkUser;

		// Token: 0x02000047 RID: 71
		public struct TFSMconfigSTRC
		{
			// Token: 0x0400023D RID: 573
			public int numSamples;

			// Token: 0x0400023E RID: 574
			public string FileNameBase;

			// Token: 0x0400023F RID: 575
			public string FilePath;

			// Token: 0x04000240 RID: 576
			public int LinesPerFile;
		}

		// Token: 0x02000048 RID: 72
		public enum BoardIDtype
		{
			// Token: 0x04000242 RID: 578
			NONE,
			// Token: 0x04000243 RID: 579
			SDPEVAL,
			// Token: 0x04000244 RID: 580
			FX3
		}
	}
}
