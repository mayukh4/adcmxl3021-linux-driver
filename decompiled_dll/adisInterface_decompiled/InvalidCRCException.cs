// Decompiled with JetBrains decompiler
// Type: adisInterface.InvalidCRCException
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

#nullable disable
namespace adisInterface;

/// <summary>
/// This exception type is thrown when the SPI data received from the DUT has an invalid CRC appended.
/// </summary>
public class InvalidCRCException : SpiException
{
  /// <summary>
  /// Creates a new InvalidCRCException with a message and data about the invalid CRC which was received.
  /// </summary>
  /// <param name="message">The message to pass up with the exception</param>
  /// <param name="invalidCRC">The CRC field value which triggered the exception</param>
  /// <param name="expectedCRC">The expected CRC field value</param>
  public InvalidCRCException(string message, uint invalidCRC, uint expectedCRC)
    : base(message, invalidCRC, expectedCRC)
  {
  }

  public InvalidCRCException()
  {
  }
}
