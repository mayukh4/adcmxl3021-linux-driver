// Decompiled with JetBrains decompiler
// Type: RegMapClasses.My.Resources.Resources
// Assembly: RegMapClasses, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A5F8C6F-7050-4AF9-8F46-4262EBB69E5D
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\RegMapClasses.dll

using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace RegMapClasses.My.Resources;

[StandardModule]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
[HideModuleName]
internal sealed class Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (object.ReferenceEquals((object) RegMapClasses.My.Resources.Resources.resourceMan, (object) null))
        RegMapClasses.My.Resources.Resources.resourceMan = new ResourceManager("RegMapClasses.Resources", typeof (RegMapClasses.My.Resources.Resources).Assembly);
      return RegMapClasses.My.Resources.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => RegMapClasses.My.Resources.Resources.resourceCulture;
    set => RegMapClasses.My.Resources.Resources.resourceCulture = value;
  }
}
