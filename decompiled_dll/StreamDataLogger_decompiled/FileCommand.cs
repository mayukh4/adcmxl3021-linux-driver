// Decompiled with JetBrains decompiler
// Type: StreamDataLogger.FileCommand
// Assembly: StreamDataLogger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 328A96D1-45A7-47F9-A7ED-7DBD0C49147E
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.xml

#nullable disable
namespace StreamDataLogger;

internal enum FileCommand
{
  CreateFile,
  WriteData,
  FinalizeFile,
  WriteCompleteFile,
}
