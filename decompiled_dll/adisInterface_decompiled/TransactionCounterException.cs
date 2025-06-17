// Decompiled with JetBrains decompiler
// Type: adisInterface.TransactionCounterException
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

#nullable disable
namespace adisInterface;

/// <summary>
/// This exception is thrown when the transaction counter value received in the last SPI transaction does not match the expected transaction counter.
/// </summary>
public class TransactionCounterException : SpiException
{
  /// <summary>Creates a new TransactionCounterException</summary>
  /// <param name="message">The message to pass up with the exception</param>
  /// <param name="receivedTransactionCounter">The transaction counter value received</param>
  /// <param name="expectedTransactionCounter">The expected transaction counter value</param>
  public TransactionCounterException(
    string message,
    uint receivedTransactionCounter,
    uint expectedTransactionCounter)
    : base(message, receivedTransactionCounter, expectedTransactionCounter)
  {
  }

  public TransactionCounterException()
  {
  }
}
