// Decompiled with JetBrains decompiler
// Type: StreamDataLogger.My.MySettingsProperty
// Assembly: StreamDataLogger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 328A96D1-45A7-47F9-A7ED-7DBD0C49147E
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.xml

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;

#nullable disable
namespace StreamDataLogger.My;

[StandardModule]
[HideModuleName]
[DebuggerNonUserCode]
[CompilerGenerated]
internal sealed class MySettingsProperty
{
  [HelpKeyword("My.Settings")]
  internal static MySettings Settings
  {
    get
    {
      MySettings settings = MySettings.Default;
      return settings;
    }
  }
}
