// Decompiled with JetBrains decompiler
// Type: RegMapClasses.CommandCollection
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

public class CommandCollection : KeyedCollection<string, CommandClass>
{
  private const StringComparison ignoreCase = StringComparison.InvariantCultureIgnoreCase;
  private string[] myColumns;
  private bool _ErrorFound;
  private string _ErrorText;

  public bool ErrorFound
  {
    get => this._ErrorFound;
    private set => this._ErrorFound = value;
  }

  public string ErrorText
  {
    get => this._ErrorText;
    private set => this._ErrorText = value;
  }

  public CommandCollection()
  {
    this.myColumns = new string[5]
    {
      "LABEL",
      "REGLABEL",
      "DELAY",
      "MASK",
      "VALUE"
    };
    this.Clear();
  }

  protected override string GetKeyForItem(CommandClass item) => item.Label;

  public new void Clear()
  {
    base.Clear();
    this.ErrorFound = false;
    this.ErrorText = "";
  }

  public int IndexOf(string key) => this.Contains(key) ? base.IndexOf(this[key]) : -1;

  public new int IndexOf(CommandClass cmd) => base.IndexOf(cmd);

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
    System.Collections.Generic.Dictionary<string, int> dictionary = new System.Collections.Generic.Dictionary<string, int>();
    this.Clear();
    this.ErrorFound = false;
    this.ErrorText = "";
    try
    {
      foreach (string line in (IEnumerable<string>) lines)
      {
        if (line.Trim().Length != 0 && line.Trim().First<char>() != '*')
        {
          string[] source = Microsoft.VisualBasic.Strings.Split(line, ",");
          Func<string, string> selector;
          // ISSUE: reference to a compiler-generated field
          if (CommandCollection._Closure\u0024__.\u0024I18\u002D0 != null)
          {
            // ISSUE: reference to a compiler-generated field
            selector = CommandCollection._Closure\u0024__.\u0024I18\u002D0;
          }
          else
          {
            // ISSUE: reference to a compiler-generated field
            CommandCollection._Closure\u0024__.\u0024I18\u002D0 = selector = (Func<string, string>) ([SpecialName] (x) => x.Trim());
          }
          List<string> list = ((IEnumerable<string>) source).Select<string, string>(selector).ToList<string>();
          if (dictionary.Count == 0)
            dictionary = this.ParseColTable((IList<string>) list);
          else if (list.Count < dictionary.Count)
          {
            this.ErrorFound = true;
            this.ErrorText = $"Line does not have enough columns: {line}\r\n";
          }
          else
            this.Add(new CommandClass()
            {
              Label = list[dictionary["LABEL"]],
              RegLabel = list[dictionary["REGLABEL"]],
              Delay = this.ParseInteger(list[dictionary["DELAY"]]),
              Mask = this.ParseUInteger(list[dictionary["MASK"]]),
              Value = this.ParseUInteger(list[dictionary["VALUE"]])
            });
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
    return !this.ErrorFound;
  }

  private int ParseInteger(string s)
  {
    int result;
    if (int.TryParse(s, NumberStyles.Integer, (IFormatProvider) CultureInfo.InvariantCulture, out result))
      return result;
    this.ErrorFound = ((this.ErrorFound ? 1 : 0) | 1) != 0;
    this.ErrorText = $"{this.ErrorText}Cannot convert {s} to Integer.\r\n";
    return 0;
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

  private System.Collections.Generic.Dictionary<string, int> ParseColTable(IList<string> Fields)
  {
    // ISSUE: variable of a compiler-generated type
    CommandCollection._Closure\u0024__21\u002D1 closure211_1;
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: variable of a compiler-generated type
    CommandCollection._Closure\u0024__21\u002D1 closure211_2 = new CommandCollection._Closure\u0024__21\u002D1(closure211_1);
    // ISSUE: reference to a compiler-generated field
    closure211_2.\u0024VB\u0024Local_colTable = new System.Collections.Generic.Dictionary<string, int>();
    int num = checked (Fields.Count - 1);
    int index = 0;
    while (index <= num)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      CommandCollection._Closure\u0024__21\u002D0 closure210 = new CommandCollection._Closure\u0024__21\u002D0(closure210);
      // ISSUE: reference to a compiler-generated field
      closure210.\u0024VB\u0024Local_title = Fields[index];
      // ISSUE: reference to a compiler-generated method
      string key = ((IEnumerable<string>) this.myColumns).Where<string>(new Func<string, bool>(closure210._Lambda\u0024__0)).FirstOrDefault<string>();
      if (!string.IsNullOrEmpty(key))
      {
        // ISSUE: reference to a compiler-generated field
        closure211_2.\u0024VB\u0024Local_colTable.Add(key, index);
      }
      checked { ++index; }
    }
    try
    {
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated method
      // ISSUE: reference to a compiler-generated field
      foreach (string str in ((IEnumerable<string>) this.myColumns).Where<string>(closure211_2.\u0024I1 == null ? (closure211_2.\u0024I1 = new Func<string, bool>(closure211_2._Lambda\u0024__1)) : closure211_2.\u0024I1))
      {
        this.ErrorFound = true;
        this.ErrorText = $"{this.ErrorText}Missing Required Column: {str}.\r\n";
      }
    }
    finally
    {
      IEnumerator<string> enumerator;
      enumerator?.Dispose();
    }
    // ISSUE: reference to a compiler-generated field
    return closure211_2.\u0024VB\u0024Local_colTable;
  }
}
