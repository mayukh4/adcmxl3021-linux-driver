// Decompiled with JetBrains decompiler
// Type: FX3Api.FX3CommunicationException
// Assembly: FX3Api, Version=2.9.5.0, Culture=neutral, PublicKeyToken=null
// MVID: 12B0FED1-476B-4D9A-A704-DBE530C65588
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\FX3Api.xml

using System;

#nullable disable
namespace FX3Api;

/// <summary>
/// This exception is used when there is a communication failure with the FX3 board during
/// a data transfer.
/// </summary>
public class FX3CommunicationException : FX3Exception
{
  /// <summary>Create a new exception</summary>
  public FX3CommunicationException()
  {
  }

  /// <summary>Create a new exception with a specified message</summary>
  /// <param name="message">The message to pass with the exception</param>
  public FX3CommunicationException(string message)
    : base(message)
  {
  }

  /// <summary>
  /// Create a new exception with a specified message and the previous exception from down the stack
  /// </summary>
  /// <param name="message">The message to pass with the exception</param>
  /// <param name="innerException">The lower level exception to pass up</param>
  public FX3CommunicationException(string message, Exception innerException)
    : base(message, innerException)
  {
  }
}
