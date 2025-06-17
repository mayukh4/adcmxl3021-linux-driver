// Decompiled with JetBrains decompiler
// Type: StreamDataLogger.My.Resources.Resources
// Assembly: StreamDataLogger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 328A96D1-45A7-47F9-A7ED-7DBD0C49147E
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.xml

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace StreamDataLogger.My.Resources;

/// <summary>A strongly-typed resource class, for looking up localized strings, etc.</summary>
[StandardModule]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
[HideModuleName]
internal sealed class Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  /// <summary>Returns the cached ResourceManager instance used by this class.</summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (object.ReferenceEquals((object) StreamDataLogger.My.Resources.Resources.resourceMan, (object) null))
        StreamDataLogger.My.Resources.Resources.resourceMan = new ResourceManager("StreamDataLogger.Resources", typeof (StreamDataLogger.My.Resources.Resources).Assembly);
      return StreamDataLogger.My.Resources.Resources.resourceMan;
    }
  }

  /// <summary>
  ///   Overrides the current thread's CurrentUICulture property for all
  ///   resource lookups using this strongly typed resource class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => StreamDataLogger.My.Resources.Resources.resourceCulture;
    set => StreamDataLogger.My.Resources.Resources.resourceCulture = value;
  }
}
