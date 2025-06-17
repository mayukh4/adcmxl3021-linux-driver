// Decompiled with JetBrains decompiler
// Type: RegMapClasses.RegMapCollection
// Assembly: RegMapClasses, Version=1.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A5F8C6F-7050-4AF9-8F46-4262EBB69E5D
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\RegMapClasses.dll

using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

#nullable disable
namespace RegMapClasses;

public class RegMapCollection : KeyedCollection<string, RegClass>
{
  public SortedDictionary<uint, RegClass> BurstReadDict;
  private System.Collections.Generic.Dictionary<string, string> PropertyDict;
  private string[][] validTitles;
  private RegClass _BurstReadTrig;
  private string _ErrorText;
  private bool _HasEmbeddedRegs;
  private bool _HasPages;

  public RegClass BurstReadTrig
  {
    get => this._BurstReadTrig;
    private set => this._BurstReadTrig = value;
  }

  public List<RegClass> BurstReadList => this.BurstReadDict.Values.ToList<RegClass>();

  public bool ErrorFound => Operators.CompareString(this.ErrorText, "", false) != 0;

  public string ErrorText
  {
    get => this._ErrorText.Trim();
    private set => this._ErrorText = value;
  }

  public bool HasEmbeddedRegs
  {
    get => this._HasEmbeddedRegs;
    private set => this._HasEmbeddedRegs = value;
  }

  public bool HasPages
  {
    get => this._HasPages;
    private set => this._HasPages = value;
  }

  public string GetPropertyValue(string key) => this.PropertyDict[key.ToUpperInvariant()];

  public bool HasPropertyValue(string key) => this.PropertyDict.ContainsKey(key.ToUpperInvariant());

  public bool TryGetPropertyValue(string key, ref string value)
  {
    return this.PropertyDict.TryGetValue(key.ToUpperInvariant(), out value);
  }

  public RegMapCollection()
  {
    this.BurstReadDict = new SortedDictionary<uint, RegClass>();
    this.PropertyDict = new System.Collections.Generic.Dictionary<string, string>();
    this.validTitles = new string[25][]
    {
      new string[1]{ "INDEX" },
      new string[1]{ "LABEL" },
      new string[1]{ "EVALLABEL" },
      new string[2]{ "PAGE", "AUXBASEADDR" },
      new string[3]{ "ADDRESS", "ADDR", "BASEADDR" },
      new string[2]{ "AUXADDRESS", "AUXADDR" },
      new string[1]{ "NUMBYTES" },
      new string[2]{ "OFFSET", "REGOFFSET" },
      new string[2]{ "SCALE", "REGSCALE" },
      new string[1]{ "READLEN" },
      new string[2]{ "READFLAG", "READABLE" },
      new string[2]{ "WRITEFLAG", "WRITEABLE" },
      new string[4]
      {
        "READPROTECTED",
        "READPROTECT",
        "HIDDEN",
        "PWPRTECT"
      },
      new string[2]{ "WRITEPROTECTED", "WRITEPROTECT" },
      new string[3]{ "TWOSCOMPFLAG", "TWOSCOMP", "TWOSCOMPL" },
      new string[1]{ "FLOAT" },
      new string[1]{ "CALREG" },
      new string[1]{ "DEFAULT" },
      new string[2]{ "TYPE", "REGTYPE" },
      new string[1]{ "EVALTYPE" },
      new string[1]{ "EMBMEMFLAG" },
      new string[1]{ "ECFLAG" },
      new string[1]{ "CALSCALE" },
      new string[1]{ "BURSTMODE" },
      new string[1]{ "PROPERTIES" }
    };
    this._HasEmbeddedRegs = false;
    this._HasPages = false;
    this.Clear();
  }

  protected override string GetKeyForItem(RegClass item) => item.Label;

  public new void Clear()
  {
    base.Clear();
    this.BurstReadDict.Clear();
    this.BurstReadList.Clear();
    this.BurstReadTrig = (RegClass) null;
    this.ErrorText = "";
    this.HasEmbeddedRegs = false;
    this.HasPages = false;
    this.PropertyDict.Clear();
  }

  public int IndexOf(string key) => this.Contains(key) ? base.IndexOf(this[key]) : -1;

  public new int IndexOf(RegClass reg) => base.IndexOf(reg);

  public bool ReadFromCSV(string FileName)
  {
    FileStream fileStream = (FileStream) null;
    StreamReader reader = (StreamReader) null;
    try
    {
      fileStream = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
      reader = new StreamReader((Stream) fileStream);
      return this.CreateCollection(reader);
    }
    finally
    {
      fileStream?.Dispose();
      reader?.Dispose();
    }
  }

  public bool CreateCollection(StreamReader reader)
  {
    return this.CreateCollection((IList<string>) reader.ReadToEnd().Split('\n'));
  }

  public bool CreateCollection(string text)
  {
    return this.CreateCollection((IList<string>) text.Split('\n'));
  }

  public bool CreateCollection(IList<string> lines)
  {
    System.Collections.Generic.Dictionary<string, int> dictionary = (System.Collections.Generic.Dictionary<string, int>) null;
    this.Clear();
    try
    {
      IList<string> source1 = lines;
      Func<string, bool> predicate;
      // ISSUE: reference to a compiler-generated field
      if (RegMapCollection._Closure\u0024__.\u0024I34\u002D0 != null)
      {
        // ISSUE: reference to a compiler-generated field
        predicate = RegMapCollection._Closure\u0024__.\u0024I34\u002D0;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        RegMapCollection._Closure\u0024__.\u0024I34\u002D0 = predicate = (Func<string, bool>) ([SpecialName] (x) => x.Trim().Length > 0 & !x.Trim().StartsWith("*"));
      }
      foreach (string Expression in source1.Where<string>(predicate))
      {
        string[] source2 = Microsoft.VisualBasic.Strings.Split(Expression, ",");
        Func<string, string> selector;
        // ISSUE: reference to a compiler-generated field
        if (RegMapCollection._Closure\u0024__.\u0024I34\u002D1 != null)
        {
          // ISSUE: reference to a compiler-generated field
          selector = RegMapCollection._Closure\u0024__.\u0024I34\u002D1;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          RegMapCollection._Closure\u0024__.\u0024I34\u002D1 = selector = (Func<string, string>) ([SpecialName] (x) => x.Trim());
        }
        List<string> list = ((IEnumerable<string>) source2).Select<string, string>(selector).ToList<string>();
        int count;
        if (dictionary == null)
        {
          dictionary = this.ParseColTable((IList<string>) list);
          count = list.Count;
        }
        else if (list.Count > count)
        {
          this.ErrorText = $"{this.ErrorText}Line has too many columns: {Expression}\r\n";
        }
        else
        {
          while (list.Count < dictionary.Count)
            list.Add("");
          RegClass r = new RegClass();
          r.Label = list[dictionary["LABEL"]];
          r.NumBytes = this.ParseUInteger(list[dictionary["NUMBYTES"]]);
          r.Offset = this.ParseDouble(list[dictionary["OFFSET"]]);
          r.Scale = this.ParseDouble(list[dictionary["SCALE"]]);
          r.ReadLen = this.ParseUInteger(list[dictionary["READLEN"]]);
          r.IsReadable = this.ParseBoolean(list[dictionary["READFLAG"]]);
          r.IsWriteable = this.ParseBoolean(list[dictionary["WRITEFLAG"]]);
          r.IsTwosComp = this.ParseBoolean(list[dictionary["TWOSCOMPFLAG"]]);
          r.Address = this.ParseUInteger(list[dictionary["ADDRESS"]]);
          r.IsCalReg = this.ParseBoolean(list[dictionary["CALREG"]]);
          if (dictionary.ContainsKey("READPROTECTED"))
            r.IsReadProtected = this.ParseBoolean(list[dictionary["READPROTECTED"]]);
          if (dictionary.ContainsKey("WRITEPROTECTED"))
            r.IsWriteProtected = this.ParseBoolean(list[dictionary["WRITEPROTECTED"]]);
          if (dictionary.ContainsKey("TYPE"))
            r.Type = this.ParseRegType(list[dictionary["TYPE"]]);
          if (dictionary.ContainsKey("CALSCALE"))
            r.CalScale = this.ParseInteger(list[dictionary["CALSCALE"]]);
          if (dictionary.ContainsKey("FLOAT"))
            r.IsFloat = this.ParseBoolean(list[dictionary["FLOAT"]]);
          if (dictionary.ContainsKey("DEFAULT"))
            r.DefaultValue = this.ParseNullableUInteger(list[dictionary["DEFAULT"]]);
          if (dictionary.ContainsKey("BURSTMODE"))
            this.ParseBurstMode(list[dictionary["BURSTMODE"]], r);
          if (dictionary.ContainsKey("PROPERTIES"))
            this.ParseProperty(list[dictionary["PROPERTIES"]]);
          r.EvalLabel = dictionary.ContainsKey("EVALLABEL") ? list[dictionary["EVALLABEL"]] : r.Label;
          r.EvalType = dictionary.ContainsKey("EVALTYPE") ? this.ParseRegType(list[dictionary["EVALTYPE"]]) : r.Type;
          if (this.HasPages | this.HasEmbeddedRegs)
            r.Page = this.ParseUInteger(list[dictionary["PAGE"]]);
          if (this.HasEmbeddedRegs)
          {
            r.IsEmbedded = this.ParseBoolean(list[dictionary["EMBMEMFLAG"]]);
            r.AuxAddress = this.ParseUInteger(list[dictionary["AUXADDRESS"]]);
          }
          string label = r.Label;
          int num = 1;
          while (this.Contains(r.Label))
          {
            r.Label = $"{label}_{Conversions.ToString(num)}";
            checked { ++num; }
          }
          this.Add(r);
        }
        if (this.ErrorFound)
          break;
      }
    }
    finally
    {
      IEnumerator<string> enumerator;
      enumerator?.Dispose();
    }
    if (this.Count == 0)
      this.ErrorText += "No registers found in reg map file.";
    else if (dictionary.Keys.Contains<string>("BURSTMODE"))
      this.ValidateBurstModeInfo();
    return !this.ErrorFound;
  }

  public void WriteDefaultsToFile(string fileName) => this.WriteDefaultsToFile(fileName, "\t");

  public void WriteDefaultsToFile(string fileName, string delim)
  {
    using (StreamWriter streamWriter = new StreamWriter(fileName))
    {
      try
      {
        Func<RegClass, bool> predicate;
        // ISSUE: reference to a compiler-generated field
        if (RegMapCollection._Closure\u0024__.\u0024I36\u002D0 != null)
        {
          // ISSUE: reference to a compiler-generated field
          predicate = RegMapCollection._Closure\u0024__.\u0024I36\u002D0;
        }
        else
        {
          // ISSUE: reference to a compiler-generated field
          RegMapCollection._Closure\u0024__.\u0024I36\u002D0 = predicate = (Func<RegClass, bool>) ([SpecialName] (r) => r.DefaultValue.HasValue);
        }
        foreach (RegClass regClass in this.Where<RegClass>(predicate))
          streamWriter.WriteLine(regClass.Label + delim + $"{regClass.DefaultValue:X4}");
      }
      finally
      {
        IEnumerator<RegClass> enumerator;
        enumerator?.Dispose();
      }
    }
  }

  private void ValidateBurstModeInfo()
  {
    if (this.BurstReadTrig == null)
      this.ErrorText += "No Burst Mode Trigger Register Found";
    SortedDictionary<uint, RegClass> burstReadDict = this.BurstReadDict;
    Func<KeyValuePair<uint, RegClass>, int, bool> selector;
    // ISSUE: reference to a compiler-generated field
    if (RegMapCollection._Closure\u0024__.\u0024I37\u002D0 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector = RegMapCollection._Closure\u0024__.\u0024I37\u002D0;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      RegMapCollection._Closure\u0024__.\u0024I37\u002D0 = selector = (Func<KeyValuePair<uint, RegClass>, int, bool>) ([SpecialName] (kvp, idx) => (long) kvp.Key != (long) idx);
    }
    IEnumerable<bool> source = burstReadDict.Select<KeyValuePair<uint, RegClass>, bool>(selector);
    Func<bool, bool> predicate;
    // ISSUE: reference to a compiler-generated field
    if (RegMapCollection._Closure\u0024__.\u0024I37\u002D1 != null)
    {
      // ISSUE: reference to a compiler-generated field
      predicate = RegMapCollection._Closure\u0024__.\u0024I37\u002D1;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      RegMapCollection._Closure\u0024__.\u0024I37\u002D1 = predicate = (Func<bool, bool>) ([SpecialName] (b) => b);
    }
    if (!source.Any<bool>(predicate))
      return;
    this.ErrorText += "Burst Mode Read Register List Incorrect";
  }

  private bool ParseBoolean(string s)
  {
    string upperInvariant = s.ToUpperInvariant();
    if (Operators.CompareString(upperInvariant, "1", false) == 0 || Operators.CompareString(upperInvariant, "TRUE", false) == 0 || Operators.CompareString(upperInvariant, "T", false) == 0)
      return true;
    if (Operators.CompareString(upperInvariant, "0", false) == 0 || Operators.CompareString(upperInvariant, "FALSE", false) == 0 || Operators.CompareString(upperInvariant, "F", false) == 0)
      return false;
    this.ErrorText = $"{this.ErrorText}Cannot convert {s} to Boolean.\r\n";
    return false;
  }

  private double ParseDouble(string s)
  {
    double result;
    if (double.TryParse(s, NumberStyles.Float, (IFormatProvider) CultureInfo.InvariantCulture, out result))
      return result;
    this.ErrorText = $"{this.ErrorText}Cannot convert {s} to Double.\r\n";
    return 0.0;
  }

  private int ParseInteger(string s)
  {
    int result;
    if (int.TryParse(s, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
      return result;
    this.ErrorText = $"{this.ErrorText}Cannot convert \"{s}\" to Integer.\r\n";
    return 0;
  }

  private void ParseBurstMode(string s, RegClass r)
  {
    string upperInvariant = s.Trim().ToUpperInvariant();
    if (Operators.CompareString(upperInvariant, "", false) == 0)
      return;
    if (Operators.CompareString(upperInvariant, "T", false) == 0)
    {
      if (this.BurstReadTrig == null)
        this.BurstReadTrig = r;
      else
        this.ErrorText += "BurstMode column has multipe triggers specified.\r\n";
    }
    else
    {
      uint result;
      if (uint.TryParse(s, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
        this.BurstReadDict.Add(result, r);
      else
        this.ErrorText = $"{this.ErrorText}{s} is an invalid value in the BurstModeColumn.\r\n";
    }
  }

  private void ParseProperty(string s)
  {
    if (Operators.CompareString(s.Trim(), "", false) == 0)
      return;
    string[] source = s.Split('=');
    if (((IEnumerable<string>) source).Count<string>() == 2)
      this.PropertyDict.Add(source[0].Trim().ToUpperInvariant(), source[1].Trim());
    else
      this.ErrorText += "Properties must be in key = value format.\r\n";
  }

  private uint ParseUInteger(string s)
  {
    string str = s.Trim();
    uint uinteger;
    if (Operators.CompareString(str, "", false) == 0)
      this.ErrorText += "Cannot convert whitespace to UInteger.\r\n";
    else if (str.StartsWith("0x"))
    {
      uint result;
      if (uint.TryParse(str.Substring(2), NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result))
        uinteger = result;
    }
    else
    {
      uint result;
      if (uint.TryParse(str, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
      {
        uinteger = result;
      }
      else
      {
        this.ErrorText = $"{this.ErrorText}Cannot convert \"{s}\" to UInteger.\r\n";
        uinteger = 0U;
      }
    }
    return uinteger;
  }

  private uint? ParseNullableUInteger(string s)
  {
    return Operators.CompareString(s.Trim(), "", false) != 0 && Operators.CompareString(s.ToLowerInvariant(), "none", false) != 0 ? new uint?(this.ParseUInteger(s)) : new uint?();
  }

  private System.Collections.Generic.Dictionary<string, int> ParseColTable(IList<string> Fields)
  {
    // ISSUE: variable of a compiler-generated type
    RegMapCollection._Closure\u0024__45\u002D0 closure450_1;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    RegMapCollection._Closure\u0024__45\u002D0 closure450_2 = new RegMapCollection._Closure\u0024__45\u002D0(closure450_1);
    // ISSUE: reference to a compiler-generated field
    closure450_2.\u0024VB\u0024Local_table = new System.Collections.Generic.Dictionary<string, int>();
    IList<string> source1 = Fields;
    Func<string, string> selector;
    // ISSUE: reference to a compiler-generated field
    if (RegMapCollection._Closure\u0024__.\u0024I45\u002D0 != null)
    {
      // ISSUE: reference to a compiler-generated field
      selector = RegMapCollection._Closure\u0024__.\u0024I45\u002D0;
    }
    else
    {
      // ISSUE: reference to a compiler-generated field
      RegMapCollection._Closure\u0024__.\u0024I45\u002D0 = selector = (Func<string, string>) ([SpecialName] (s) => s.ToUpperInvariant().Replace("(I)", "").Trim());
    }
    string[] array = source1.Select<string, string>(selector).ToArray<string>();
    int num = checked (((IEnumerable<string>) array).Count<string>() - 1);
    int index1 = 0;
    while (index1 <= num)
    {
      try
      {
        string[][] validTitles = this.validTitles;
        int index2 = 0;
        while (index2 < validTitles.Length)
        {
          string[] source2 = validTitles[index2];
          if (((IEnumerable<string>) source2).Contains<string>(array[index1]))
          {
            // ISSUE: reference to a compiler-generated field
            closure450_2.\u0024VB\u0024Local_table.Add(source2[0], index1);
            break;
          }
          checked { ++index2; }
        }
      }
      catch (ArgumentException ex)
      {
        ProjectData.SetProjectError((Exception) ex);
        this.ErrorText = $"{this.ErrorText}Duplicate or incompatible column header found.{array[index1]}.\r\n";
        ProjectData.ClearProjectError();
      }
      checked { ++index1; }
    }
    string[] source3 = new string[11]
    {
      "LABEL",
      "NUMBYTES",
      "OFFSET",
      "SCALE",
      "READLEN",
      "READFLAG",
      "WRITEFLAG",
      "TWOSCOMPFLAG",
      "READPROTECTED",
      "ADDRESS",
      "CALREG"
    };
    try
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated field
      foreach (string Right in ((IEnumerable<string>) source3).Where<string>(closure450_2.\u0024I1 == null ? (closure450_2.\u0024I1 = new Func<string, bool>(closure450_2._Lambda\u0024__1)) : closure450_2.\u0024I1))
      {
        string str = (string) null;
        string[][] validTitles = this.validTitles;
        int index3 = 0;
        while (index3 < validTitles.Length)
        {
          string[] strArray = validTitles[index3];
          if (Operators.CompareString(strArray[0], Right, false) == 0)
          {
            str = string.Join("/", strArray);
            break;
          }
          checked { ++index3; }
        }
        this.ErrorText = $"{this.ErrorText}Missing Required Column: {str}.\r\n";
      }
    }
    finally
    {
      IEnumerator<string> enumerator;
      enumerator?.Dispose();
    }
    if (((IEnumerable<string>) array).Contains<string>("PAGE"))
    {
      if (((IEnumerable<string>) array).Contains<string>("EMBMEMFLAG") | ((IEnumerable<string>) array).Contains<string>("AUXADDR") | ((IEnumerable<string>) array).Contains<string>("AUXBASEADDR"))
        this.ErrorText += "Reg map may not contian Pages and Embedded Registers.\r\n";
      else
        this.HasPages = true;
    }
    else if (((IEnumerable<string>) array).Contains<string>("EMBMEMFLAG") | ((IEnumerable<string>) array).Contains<string>("AUXADDR") | ((IEnumerable<string>) array).Contains<string>("AUXBASEADDR"))
    {
      if (((IEnumerable<string>) array).Contains<string>("EMBMEMFLAG") & ((IEnumerable<string>) array).Contains<string>("AUXADDR") & ((IEnumerable<string>) array).Contains<string>("AUXBASEADDR"))
        this.HasEmbeddedRegs = true;
      else
        this.ErrorText += "Embedded maps must include EMBMEMFLAG, AUXADDR, and AUXBASEADDR columns.\r\n";
    }
    // ISSUE: reference to a compiler-generated field
    return closure450_2.\u0024VB\u0024Local_table;
  }

  private RegClass.RegType ParseRegType(string s)
  {
    string upperInvariant = s.Trim().ToUpperInvariant();
    RegClass.RegType regType;
    // ISSUE: reference to a compiler-generated method
    switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(upperInvariant))
    {
      case 16026925:
        if (Operators.CompareString(upperInvariant, "FIR_D", false) == 0)
        {
          regType = RegClass.RegType.FilterD;
          break;
        }
        goto default;
      case 377410699:
        if (Operators.CompareString(upperInvariant, "PAGEID", false) == 0)
        {
          regType = RegClass.RegType.PageId;
          break;
        }
        goto default;
      case 468430365:
        if (Operators.CompareString(upperInvariant, "USEROUTPUTFLAG", false) == 0)
        {
          regType = RegClass.RegType.UserOutputFlag;
          break;
        }
        goto default;
      case 569533575:
        if (Operators.CompareString(upperInvariant, "FACTORYCAL", false) == 0)
        {
          regType = RegClass.RegType.FactoryCal;
          break;
        }
        goto default;
      case 1592615862:
        if (Operators.CompareString(upperInvariant, "FACTORYCONTROL", false) == 0)
        {
          regType = RegClass.RegType.FactoryControl;
          break;
        }
        goto default;
      case 2549462383:
        if (Operators.CompareString(upperInvariant, "STATUS", false) == 0)
        {
          regType = RegClass.RegType.Status;
          break;
        }
        goto default;
      case 2693550206:
        if (Operators.CompareString(upperInvariant, "USERCAL", false) == 0)
        {
          regType = RegClass.RegType.UserCal;
          break;
        }
        goto default;
      case 3261547267:
        if (Operators.CompareString(upperInvariant, "USEROUTPUTLOW", false) == 0)
        {
          regType = RegClass.RegType.UserOutputLow;
          break;
        }
        goto default;
      case 3377733591:
        if (Operators.CompareString(upperInvariant, "FACTORYSTATUS", false) == 0)
        {
          regType = RegClass.RegType.FactoryStatus;
          break;
        }
        goto default;
      case 3725453694:
        if (Operators.CompareString(upperInvariant, "CONTROL", false) == 0)
        {
          regType = RegClass.RegType.Control;
          break;
        }
        goto default;
      case 4125030067:
        if (Operators.CompareString(upperInvariant, "USEROUTPUT", false) == 0)
        {
          regType = RegClass.RegType.UserOutput;
          break;
        }
        goto default;
      case 4167748188:
        if (Operators.CompareString(upperInvariant, "FACTORYOUTPUT", false) == 0)
        {
          regType = RegClass.RegType.FactoryOuput;
          break;
        }
        goto default;
      case 4193550888:
        if (Operators.CompareString(upperInvariant, "FIR_C", false) == 0)
        {
          regType = RegClass.RegType.FilterC;
          break;
        }
        goto default;
      case 4210328507:
        if (Operators.CompareString(upperInvariant, "FIR_B", false) == 0)
        {
          regType = RegClass.RegType.FilterB;
          break;
        }
        goto default;
      case 4227106126:
        if (Operators.CompareString(upperInvariant, "FIR_A", false) == 0)
        {
          regType = RegClass.RegType.FilterA;
          break;
        }
        goto default;
      case 4277438983:
        if (Operators.CompareString(upperInvariant, "FIR_F", false) == 0)
        {
          regType = RegClass.RegType.FilterF;
          break;
        }
        goto default;
      case 4294216602:
        if (Operators.CompareString(upperInvariant, "FIR_E", false) == 0)
        {
          regType = RegClass.RegType.FilterE;
          break;
        }
        goto default;
      default:
        regType = RegClass.RegType.Undefined;
        break;
    }
    return regType;
  }
}
