// Decompiled with JetBrains decompiler
// Type: AdisApi.ISpi32InterfaceException
// Assembly: adisApi, Version=1.2.0.999, Culture=neutral, PublicKeyToken=null
// MVID: 95D8AB16-C1DE-4618-B829-357EFC0B4F55
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisApi.xml

using System;

#nullable disable
namespace AdisApi;

/// <summary>
/// This exception is thrown by the Spi32Wrapper class whenever an invalid result is produced by an ISpi32Interface implementation class.
/// </summary>
/// <summary>
/// Constructor for ISpi32InterfaceException. Must be sent with a message.
/// </summary>
/// <param name="message">Details about </param>
public class ISpi32InterfaceException(string message) : Exception(message)
{
}
