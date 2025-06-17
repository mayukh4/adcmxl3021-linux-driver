// Decompiled with JetBrains decompiler
// Type: AdisApi.TimeoutException
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System;
using System.Runtime.Serialization;

#nullable disable
namespace AdisApi;

/// <summary>Exception for time out.</summary>
[Serializable]
public class TimeoutException : Exception
{
  private const string defaultMessage = "adisApi Transfer Timed Out";

  /// <summary>
  /// 
  /// </summary>
  public TimeoutException()
    : base("adisApi Transfer Timed Out")
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="inner"></param>
  public TimeoutException(Exception inner)
    : base("adisApi Transfer Timed Out", inner)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  public TimeoutException(string message)
    : base(message)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="message"></param>
  /// <param name="inner"></param>
  public TimeoutException(string message, Exception inner)
    : base(message, inner)
  {
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="info"></param>
  /// <param name="context"></param>
  protected TimeoutException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }
}
