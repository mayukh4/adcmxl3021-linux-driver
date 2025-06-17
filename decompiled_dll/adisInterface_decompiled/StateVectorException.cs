// Decompiled with JetBrains decompiler
// Type: adisInterface.StateVectorException
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

#nullable disable
namespace adisInterface;

/// <summary>
/// This exception is thrown when the state vector in the automotive SPI protocol indicates that an error has occurred.
/// </summary>
public class StateVectorException : SpiException
{
  /// <summary>
  /// Creates a new StateVectorException with a message and the invalid state vector field. The expected value will always be 1 (device OK)
  /// </summary>
  /// <param name="message">The message to pass up with the exception</param>
  /// <param name="stateVector">The state vector value which triggered the exception</param>
  public StateVectorException(string message, uint stateVector)
    : base(message, stateVector, 1U)
  {
  }

  public StateVectorException()
  {
  }
}
