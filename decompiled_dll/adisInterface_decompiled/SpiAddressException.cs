// Decompiled with JetBrains decompiler
// Type: adisInterface.SpiAddressException
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

#nullable disable
namespace adisInterface;

/// <summary>
/// This exception is thrown when the address received back from the DUT does not match the expected address.
/// </summary>
public class SpiAddressException : SpiException
{
  /// <summary>Creates a new SpiAddressException</summary>
  /// <param name="message">The message to pass up with the exception</param>
  /// <param name="receivedAddress">The address received back from the DUT</param>
  /// <param name="expectedAddress">The expected address to receive (from the previous SPI word)</param>
  public SpiAddressException(string message, uint receivedAddress, uint expectedAddress)
    : base(message, receivedAddress, expectedAddress)
  {
  }

  public SpiAddressException()
  {
  }
}
