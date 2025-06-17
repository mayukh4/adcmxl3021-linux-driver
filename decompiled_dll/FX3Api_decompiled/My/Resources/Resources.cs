// Decompiled with JetBrains decompiler
// Type: FX3Api.My.Resources.Resources
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace FX3Api.My.Resources;

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
      if (object.ReferenceEquals((object) FX3Api.My.Resources.Resources.resourceMan, (object) null))
        FX3Api.My.Resources.Resources.resourceMan = new ResourceManager("FX3Api.Resources", typeof (FX3Api.My.Resources.Resources).Assembly);
      return FX3Api.My.Resources.Resources.resourceMan;
    }
  }

  /// <summary>
  ///   Overrides the current thread's CurrentUICulture property for all
  ///   resource lookups using this strongly typed resource class.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => FX3Api.My.Resources.Resources.resourceCulture;
    set => FX3Api.My.Resources.Resources.resourceCulture = value;
  }

  /// <summary>
  ///   Looks up a localized string similar to Thu 06/27/2019 13:24:43.01
  /// .
  /// </summary>
  internal static string BuildDate
  {
    get => FX3Api.My.Resources.Resources.ResourceManager.GetString(nameof (BuildDate), FX3Api.My.Resources.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to generic_stream
  /// .
  /// </summary>
  internal static string CurrentBranch
  {
    get => FX3Api.My.Resources.Resources.ResourceManager.GetString(nameof (CurrentBranch), FX3Api.My.Resources.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to eee943f2cf38653a06efbb679c7477744f99a6ed
  /// .
  /// </summary>
  internal static string CurrentCommit
  {
    get => FX3Api.My.Resources.Resources.ResourceManager.GetString(nameof (CurrentCommit), FX3Api.My.Resources.Resources.resourceCulture);
  }

  /// <summary>
  ///   Looks up a localized string similar to https://github.com/juchong/iSensor-FX3-Interface.git
  /// .
  /// </summary>
  internal static string CurrentURL
  {
    get => FX3Api.My.Resources.Resources.ResourceManager.GetString(nameof (CurrentURL), FX3Api.My.Resources.Resources.resourceCulture);
  }
}
