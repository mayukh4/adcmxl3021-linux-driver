using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Vibration_Evaluation.My.Resources
{
	/// <summary>
	///   A strongly-typed resource class, for looking up localized strings, etc.
	/// </summary>
	// Token: 0x02000005 RID: 5
	[StandardModule]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	[HideModuleName]
	internal sealed class Resources
	{
		/// <summary>
		///   Returns the cached ResourceManager instance used by this class.
		/// </summary>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002198 File Offset: 0x00000398
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = object.ReferenceEquals(Resources.resourceMan, null);
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Vibration_Evaluation.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		/// <summary>
		///   Overrides the current thread's CurrentUICulture property for all
		///   resource lookups using this strongly typed resource class.
		/// </summary>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021E0 File Offset: 0x000003E0
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021F7 File Offset: 0x000003F7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		/// <summary>
		///   Looks up a localized string similar to SR_opt,Band,F_LOW,F_HIGH,X_MAG1,X_MAG2,Y_MAG1,Y_MAG2,Z_MAG1,Z_MAG2
		/// 0,1,40,230,7000,10000,7000,10000,7000,10000
		/// 0,2,230,330,6100,8100,6100,8100,6100,8100
		/// 0,3,330,450,5000,7000,5000,7000,5000,7000
		/// 0,4,450,580,9000,11000,11000,14000,11000,14000
		/// 0,5,580,1000,8000,10000,8000,10000,8000,1000
		/// 0,6,1000,2032,6000,9000,6000,9000,6000,9000
		/// 1,1,41,231,7001,10001,7001,10001,7001,10001
		/// 1,2,231,331,6101,8101,6101,8101,6101,8101
		/// 1,3,331,451,5001,7001,5001,7001,5001,7001
		/// 1,4,451,581,9001,11001,11001,14001,11001,14 [rest of string was truncated]";.
		/// </summary>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002200 File Offset: 0x00000400
		internal static string AlarmSpectrum_Default
		{
			get
			{
				return Resources.ResourceManager.GetString("AlarmSpectrum_Default", Resources.resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
		/// </summary>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002228 File Offset: 0x00000428
		internal static Icon Analog
		{
			get
			{
				object objectValue = RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("Analog", Resources.resourceCulture));
				return (Icon)objectValue;
			}
		}

		/// <summary>
		///   Looks up a localized resource of type System.Byte[].
		/// </summary>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000225C File Offset: 0x0000045C
		internal static byte[] boot_fw
		{
			get
			{
				object objectValue = RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("boot_fw", Resources.resourceCulture));
				return (byte[])objectValue;
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Label,RegLabel,Delay,Mask,Value
		/// Auto-Null,COMMAND,100,0xFFFF,0x0001
		/// Power-Down (wake with /CS toggle). ,COMMAND,500,0xFFFF,0x0002
		/// Self-Test,COMMAND,1000,0xFFFF,0x0004
		/// Factory Reset,COMMAND,1000,0xFFFF,0x0008
		/// Clear Status ,COMMAND,100,0xFFFF,0x0010
		/// Flash Test,COMMAND,500,0xFFFF,0x0020
		/// Flash Update,COMMAND,500,0xFFFF,0x0040
		/// Software Reset,COMMAND,1000,0xFFFF,0x0080
		/// Clear Records,COMMAND,500,0xFFFF,0x0100
		/// Clear spectral alarm bands,COMMAND,500,0xFFFF,0x0200
		/// Reset buffer pointer,COMMAND,100,0xFFFF,0x [rest of string was truncated]";.
		/// </summary>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002290 File Offset: 0x00000490
		internal static string CmdDataFile_1021
		{
			get
			{
				return Resources.ResourceManager.GetString("CmdDataFile_1021", Resources.resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Label,RegLabel,Delay,Mask,Value
		/// Auto-Null,COMMAND,1500,0xFFFF,0x0001
		/// Power-Down (wake with /CS toggle). ,COMMAND,500,0xFFFF,0x0002
		/// Self-Test,COMMAND,1000,0xFFFF,0x0004
		/// Factory Reset,COMMAND,1000,0xFFFF,0x0008
		/// Clear Status ,COMMAND,100,0xFFFF,0x0010
		/// Flash Test,COMMAND,500,0xFFFF,0x0020
		/// Flash Update,COMMAND,500,0xFFFF,0x0040
		/// Software Reset,COMMAND,1000,0xFFFF,0x0080
		/// Clear Records,COMMAND,500,0xFFFF,0x0100
		/// Clear spectral alarm bands,COMMAND,500,0xFFFF,0x0200
		/// Reset buffer pointer,COMMAND,100,0xFFFF,0 [rest of string was truncated]";.
		/// </summary>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000022B8 File Offset: 0x000004B8
		internal static string CmdDataFile_16227
		{
			get
			{
				return Resources.ResourceManager.GetString("CmdDataFile_16227", Resources.resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Label,RegLabel,Delay,Mask,Value
		/// Auto-Null,COMMAND,1500,0xFFFF,0x0001
		/// Power-Down (wake with /CS toggle). ,COMMAND,500,0xFFFF,0x0002
		/// Self-Test,COMMAND,1000,0xFFFF,0x0004
		/// Factory Reset,COMMAND,1000,0xFFFF,0x0008
		/// Clear Status ,COMMAND,100,0xFFFF,0x0010
		/// Flash Test,COMMAND,500,0xFFFF,0x0020
		/// Flash Update,COMMAND,500,0xFFFF,0x0040
		/// Software Reset,COMMAND,1000,0xFFFF,0x0080
		/// Clear Records,COMMAND,500,0xFFFF,0x0100
		/// Clear spectral alarm bands,COMMAND,500,0xFFFF,0x0200
		/// Reset buffer pointer,COMMAND,100,0xFFFF,0 [rest of string was truncated]";.
		/// </summary>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000022E0 File Offset: 0x000004E0
		internal static string CmdDataFile_16228
		{
			get
			{
				return Resources.ResourceManager.GetString("CmdDataFile_16228", Resources.resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Label,RegLabel,Delay,Mask,Value
		/// Auto-Null,COMMAND,100,0xFFFF,0x0001
		/// Power-Down (wake with /CS toggle). ,COMMAND,500,0xFFFF,0x0002
		/// Self-Test,COMMAND,1000,0xFFFF,0x0004
		/// Factory Reset,COMMAND,1000,0xFFFF,0x0008
		/// Clear Status ,COMMAND,100,0xFFFF,0x0010
		/// Flash Test,COMMAND,500,0xFFFF,0x0020
		/// Flash Update,COMMAND,500,0xFFFF,0x0040
		/// Software Reset,COMMAND,1000,0xFFFF,0x0080
		/// Clear Records,COMMAND,500,0xFFFF,0x0100
		/// Clear spectral alarm bands,COMMAND,500,0xFFFF,0x0200
		/// Reset buffer pointer,COMMAND,100,0xFFFF,0x [rest of string was truncated]";.
		/// </summary>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002308 File Offset: 0x00000508
		internal static string CmdDataFile_3021
		{
			get
			{
				return Resources.ResourceManager.GetString("CmdDataFile_3021", Resources.resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized resource of type System.Byte[].
		/// </summary>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002330 File Offset: 0x00000530
		internal static byte[] CyUSB
		{
			get
			{
				object objectValue = RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("CyUSB", Resources.resourceCulture));
				return (byte[])objectValue;
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Label,ProductID,RegMapName,CmdMapName,DutInterface,DutControl,AccelAxes,GyroAxes,HasMagnetometer,HasBarometer,HasOrientation
		/// ADcmXL1021,1021,RegDataFile_3021,CmdDataFile_1021,adbf,ADcmXL3021,1,0,FALSE,FALSE,FALSE
		/// *ADcmXL2021,2021,RegDataFile_3021,CmdDataFile_2021,adbf,ADcmXL3021,2,0,FALSE,FALSE,FALSE
		/// ADcmXL3021,3021,RegDataFile_3021,CmdDataFile_3021,adbf,ADcmXL3021,3,0,FALSE,FALSE,FALSE
		/// .
		/// </summary>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002364 File Offset: 0x00000564
		internal static string DeviceCatalog
		{
			get
			{
				return Resources.ResourceManager.GetString("DeviceCatalog", Resources.resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized resource of type System.Byte[].
		/// </summary>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000238C File Offset: 0x0000058C
		internal static byte[] EVAL_SDRAM
		{
			get
			{
				object objectValue = RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("EVAL_SDRAM", Resources.resourceCulture));
				return (byte[])objectValue;
			}
		}

		/// <summary>
		///   Looks up a localized resource of type System.Byte[].
		/// </summary>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000023C0 File Offset: 0x000005C0
		internal static byte[] FX3_Firmware
		{
			get
			{
				object objectValue = RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("FX3_Firmware", Resources.resourceCulture));
				return (byte[])objectValue;
			}
		}

		/// <summary>
		///   Looks up a localized resource of type System.Byte[].
		/// </summary>
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000023F4 File Offset: 0x000005F4
		internal static byte[] FX3Api
		{
			get
			{
				object objectValue = RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("FX3Api", Resources.resourceCulture));
				return (byte[])objectValue;
			}
		}

		/// <summary>
		///   Looks up a localized string similar to 
		/// The ADIS16375 and ADIS164xx provide 32-bits of output data resolution for gyroscopes, accelerometers and the barometer (48x only).
		/// Each 32-bit sensor has two registers, that have 16 bits each. 
		/// The register that ends in “OUT” represents the most significant 16-bits.  For example: X_GYRO_OUT
		/// The register that ends in “LOW” represents the least significant 16-bits. For example: X_GYRO_LOW
		/// For the Data Capture, select the “OUT” register to record the 16-bit version of the output data OR select the “LOW”  [rest of string was truncated]";.
		/// </summary>
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002428 File Offset: 0x00000628
		internal static string Help_DataLog
		{
			get
			{
				return Resources.ResourceManager.GetString("Help_DataLog", Resources.resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to {\rtf1\adeflang1037\ansi\ansicpg1252\uc1\adeff1\deff0\stshfdbch31505\stshfloch31506\stshfhich31506\stshfbi0\deflang1033\deflangfe1028\themelang1033\themelangfe1028\themelangcs1037{\fonttbl{\f0\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\f1\fbidi \fswiss\fcharset0\fprq2{\*\panose 020b0604020202020204}Arial;}
		/// {\f14\fbidi \froman\fcharset136\fprq2{\*\panose 02020500000000000000}PMingLiU{\*\falt !Ps2OcuAe};}{\f34\fbidi \froman\fcharset0\fprq2{\*\panose 02040503050406030204}C [rest of string was truncated]";.
		/// </summary>
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002450 File Offset: 0x00000650
		internal static string HelpText
		{
			get
			{
				return Resources.ResourceManager.GetString("HelpText", Resources.resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Label,EvalLabel,Type,Page,Addr,NumBytes,RegOffset,RegScale,ReadLen,ReadFlag,WriteFlag,TwosCompl,Hidden,Default,CalReg
		/// ENDURANCE,FLASH_CNT,FactoryCal,0,0,2,0,1,16,TRUE,FALSE,FALSE,FALSE,none,FALSE
		/// X_NULL,X_NULL,UserCal,0,2,2,0,0.0023842,16,TRUE,TRUE,TRUE,FALSE,none,FALSE
		/// Y_NULL,Y_NULL,UserCal,0,4,2,0,0.0023842,16,TRUE,TRUE,TRUE,FALSE,none,FALSE
		/// Z_NULL,Z_NULL,UserCal,0,6,2,0,0.0023842,16,TRUE,TRUE,TRUE,FALSE,none,FALSE
		/// REC_FLSH_CNT,REC_FLSH_CNT,UserCal,0,8,2,0,1,16,TRUE,TRUE,TRUE,FALSE,none,FALSE
		/// SUPPLY [rest of string was truncated]";.
		/// </summary>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002478 File Offset: 0x00000678
		internal static string RegDataFile_16227
		{
			get
			{
				return Resources.ResourceManager.GetString("RegDataFile_16227", Resources.resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Label,EvalLabel,Type,Page,Addr,NumBytes,RegOffset,RegScale,ReadLen,ReadFlag,WriteFlag,TwosCompl,Hidden,Default,CalReg
		/// ENDURANCE,FLASH_CNT,FactoryCal,0,0,2,0,1,16,TRUE,FALSE,FALSE,FALSE,none,FALSE
		/// X_NULL,X_SENS,UserCal,0,2,2,0,0.0006104,16,TRUE,TRUE,TRUE,FALSE,none,FALSE
		/// Y_NULL,Y_SENS,UserCal,0,4,2,0,0.0006104,16,TRUE,TRUE,TRUE,FALSE,none,FALSE
		/// Z_NULL,Z_SENS,UserCal,0,6,2,0,0.0006104,16,TRUE,TRUE,TRUE,FALSE,none,FALSE
		/// TEMP_OUT,TEMP_OUT,UserOutput,0,8,2,625,-0.47,16,TRUE,FALSE,TRUE,FALSE,none,FALSE
		/// SUPP [rest of string was truncated]";.
		/// </summary>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024A0 File Offset: 0x000006A0
		internal static string RegDataFile_16228
		{
			get
			{
				return Resources.ResourceManager.GetString("RegDataFile_16228", Resources.resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized string similar to Label,EvalLabel,Type,Page,HexAddr,Addr,NumBytes,RegOffset,RegScale,ReadLen,ReadFlag,WriteFlag,TwosCompl,Hidden,Default,CalReg
		/// PAGE_IDENTITY,PAGE_ID,Control,0,0,0,2,0,1,16,TRUE,TRUE,FALSE,FALSE,none,FALSE
		/// TEMP_OUT,TEMP_OUT,UserOutput,0,2,2,2,273,0.25,16,TRUE,TRUE,TRUE,FALSE,none,FALSE
		/// SUPPLY_OUT,SUPPLY_OUT,UserOutput,0,4,4,2,0,0.003223,16,TRUE,TRUE,TRUE,FALSE,none,FALSE
		/// FFT_AVG1,FFT_AVG1,Control,0,6,6,2,0,1,16,TRUE,TRUE,FALSE,FALSE,264,FALSE
		/// FFT_AVG2,FFT_AVG2,Control,0,8,8,2,625,1,16,TRUE,TRUE,FALSE,FAL [rest of string was truncated]";.
		/// </summary>
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000024C8 File Offset: 0x000006C8
		internal static string RegDataFile_3021
		{
			get
			{
				return Resources.ResourceManager.GetString("RegDataFile_3021", Resources.resourceCulture);
			}
		}

		/// <summary>
		///   Looks up a localized resource of type System.Byte[].
		/// </summary>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024F0 File Offset: 0x000006F0
		internal static byte[] RegMapClasses
		{
			get
			{
				object objectValue = RuntimeHelpers.GetObjectValue(Resources.ResourceManager.GetObject("RegMapClasses", Resources.resourceCulture));
				return (byte[])objectValue;
			}
		}

		/// <summary>
		///   Looks up a localized string similar to {\rtf1\adeflang1037\ansi\ansicpg1252\uc1\adeff1\deff0\stshfdbch31505\stshfloch31506\stshfhich31506\stshfbi0\deflang1033\deflangfe1028\themelang1033\themelangfe1028\themelangcs1037{\fonttbl{\f0\fbidi \froman\fcharset0\fprq2{\*\panose 02020603050405020304}Times New Roman;}{\f1\fbidi \fswiss\fcharset0\fprq2{\*\panose 020b0604020202020204}Arial;}
		/// {\f14\fbidi \froman\fcharset136\fprq2{\*\panose 02020500000000000000}PMingLiU{\*\falt !Ps2OcuAe};}{\f34\fbidi \froman\fcharset0\fprq2{\*\panose 02040503050406030204}C [rest of string was truncated]";.
		/// </summary>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002524 File Offset: 0x00000724
		internal static string Revisions
		{
			get
			{
				return Resources.ResourceManager.GetString("Revisions", Resources.resourceCulture);
			}
		}

		// Token: 0x04000006 RID: 6
		private static ResourceManager resourceMan;

		// Token: 0x04000007 RID: 7
		private static CultureInfo resourceCulture;
	}
}
