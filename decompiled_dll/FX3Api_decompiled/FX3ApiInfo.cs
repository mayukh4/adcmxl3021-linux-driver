// Decompiled with JetBrains decompiler
// Type: FX3Api.FX3ApiInfo
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

using Microsoft.VisualBasic.CompilerServices;
using System;

#nullable disable
namespace FX3Api;

/// <summary>
/// This class provides a collection of information about the FX3 API. All the fields are hard-coded into the DLL at compile time.
/// To retrieve the FX3ApiInfo set during compile time, use the GetFX3ApiInfo call within FX3 connection.
/// </summary>
public class FX3ApiInfo
{
  /// <summary>The project name (should be FX3Api)</summary>
  public string Name;
  /// <summary>The project description</summary>
  public string Description;
  /// <summary>The date and time of the current FX3Api build in use.</summary>
  public string BuildDateTime;
  /// <summary>
  /// The build version of this FX3Api instance. Should match application firmware.
  /// </summary>
  public string VersionNumber;
  private string m_GitURL;
  private string m_GitCommitURL;
  private string m_GitBranch;
  private string m_GitCommitSHA1;

  /// <summary>Constructor which initializes values to "Error: Not Set"</summary>
  public FX3ApiInfo()
  {
    this.Name = "Error: Not Set";
    this.Description = "Error: Not Set";
    this.BuildDateTime = "Error: Not Set";
    this.VersionNumber = "Error: Not Set";
    this.m_GitBranch = "Error: Not Set";
    this.m_GitCommitSHA1 = "Error: Not Set";
    this.m_GitCommitURL = "Error: Not Set";
    this.m_GitURL = "Error: Not Set";
  }

  /// <summary>The base git remote URL which this version of the FX3Api was build on.</summary>
  /// <returns></returns>
  public string GitURL
  {
    get => this.m_GitURL;
    set
    {
      value.Replace(Environment.NewLine, "");
      this.m_GitURL = value;
    }
  }

  /// <summary>The branch which this version of the FX3Api was built on.</summary>
  /// <returns></returns>
  public string GitBranch
  {
    get => this.m_GitBranch;
    set
    {
      value.Replace(Environment.NewLine, "");
      this.m_GitBranch = value;
    }
  }

  /// <summary>The hast for the git commit which this version of the FX3Api was built on.</summary>
  /// <returns></returns>
  public string GitCommitSHA1
  {
    get => this.m_GitCommitSHA1;
    set
    {
      value.Replace(Environment.NewLine, "");
      this.m_GitCommitSHA1 = value;
    }
  }

  /// <summary>The URL of the git commit which this version of the FX3Api was built on.</summary>
  /// <returns></returns>
  public string GitCommitURL
  {
    get
    {
      try
      {
        this.m_GitCommitURL = $"{this.m_GitURL.Replace(".git", "")}/tree/{this.m_GitCommitSHA1}";
      }
      catch (Exception ex)
      {
        ProjectData.SetProjectError(ex);
        this.m_GitCommitURL = "Error building link";
        ProjectData.ClearProjectError();
      }
      return this.m_GitCommitURL;
    }
  }

  /// <summary>Overload of the toString function to allow for better formatting.</summary>
  /// <returns>A string representing all available FX3 API information.</returns>
  public override string ToString()
  {
    return $"{$"{$"{$"{$"{$"{$"{$"Project Name: {this.Name}{Environment.NewLine}"}Description: {this.Description}{Environment.NewLine}"}Version Number: {this.VersionNumber}{Environment.NewLine}"}Build Date and Time: {this.BuildDateTime}{Environment.NewLine}"}Base Git URL: {this.m_GitURL}"}Current Branch: {this.m_GitBranch}{Environment.NewLine}"}Most Recent Commit Hash: {this.m_GitCommitSHA1}{Environment.NewLine}"}Link to the commit: {this.GitCommitURL}";
  }
}
