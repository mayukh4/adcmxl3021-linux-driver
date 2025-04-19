using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace Vibration_Evaluation.My
{
	// Token: 0x02000006 RID: 6
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal sealed partial class MySettings : ApplicationSettingsBase
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00002578 File Offset: 0x00000778
		[DebuggerNonUserCode]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		private static void AutoSaveSettings(object sender, EventArgs e)
		{
			bool saveMySettingsOnExit = MyProject.Application.SaveMySettingsOnExit;
			if (saveMySettingsOnExit)
			{
				MySettingsProperty.Settings.Save();
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000025A4 File Offset: 0x000007A4
		public static MySettings Default
		{
			get
			{
				bool flag = !MySettings.addedHandler;
				if (flag)
				{
					object obj = MySettings.addedHandlerLockObject;
					ObjectFlowControl.CheckForSyncLockOnValueType(obj);
					lock (obj)
					{
						bool flag3 = !MySettings.addedHandler;
						if (flag3)
						{
							MyProject.Application.Shutdown += MySettings.AutoSaveSettings;
							MySettings.addedHandler = true;
						}
					}
				}
				return MySettings.defaultInstance;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002630 File Offset: 0x00000830
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002652 File Offset: 0x00000852
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string Device
		{
			get
			{
				return Conversions.ToString(this["Device"]);
			}
			set
			{
				this["Device"] = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002664 File Offset: 0x00000864
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002686 File Offset: 0x00000886
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string SPIconfig
		{
			get
			{
				return Conversions.ToString(this["SPIconfig"]);
			}
			set
			{
				this["SPIconfig"] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002698 File Offset: 0x00000898
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000026BA File Offset: 0x000008BA
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string BoardID
		{
			get
			{
				return Conversions.ToString(this["BoardID"]);
			}
			set
			{
				this["BoardID"] = value;
			}
		}

		// Token: 0x04000008 RID: 8
		private static MySettings defaultInstance = (MySettings)SettingsBase.Synchronized(new MySettings());

		// Token: 0x04000009 RID: 9
		private static bool addedHandler;

		// Token: 0x0400000A RID: 10
		private static object addedHandlerLockObject = RuntimeHelpers.GetObjectValue(new object());
	}
}
