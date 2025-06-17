// Decompiled with JetBrains decompiler
// Type: adisInterface.SpiException
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using System;

#nullable disable
namespace adisInterface;

public class SpiException : Exception
{
  private uint m_ExpectedFieldValue;
  private uint m_InvalidFieldValue;

  /// <summary>The expected value for that field.</summary>
  /// <returns></returns>
  public uint ExpectedFieldValue => this.m_ExpectedFieldValue;

  /// <summary>
  /// The invalid field value (SV, TC, CRC, Address) in the SPI word which triggered the exception.
  /// </summary>
  /// <returns></returns>
  public uint InvalidFieldValue => this.m_InvalidFieldValue;

  /// <summary>
  /// Create a new exception with a specified message, the invalid field, and the expected field for a given SPI exception.
  /// </summary>
  /// <param name="message">The message to pass up with the exception</param>
  /// <param name="invalidFieldValue">The field value (SV, TC, etc) in the SPI word which triggered the exception</param>
  /// <param name="expectedFieldValue">The expected value for that field</param>
  public SpiException(string message, uint invalidFieldValue, uint expectedFieldValue)
    : base(message)
  {
    this.m_ExpectedFieldValue = expectedFieldValue;
    this.m_InvalidFieldValue = invalidFieldValue;
  }

  public SpiException()
    : base("SPI Exception error message not set!")
  {
    this.m_ExpectedFieldValue = 0U;
    this.m_InvalidFieldValue = 0U;
  }
}
