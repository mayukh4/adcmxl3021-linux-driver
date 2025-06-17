// Decompiled with JetBrains decompiler
// Type: adisInterface.RunAsyncCompletedEventArgs
// Assembly: adisInterface, Version=1.1.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 6AA9F5AC-9E64-4D1A-9369-198895DCE53B
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\adisInterface.xml

using System;

#nullable disable
namespace adisInterface;

/// <summary>
/// The object passes to the RunAsynCompleted event handler when stream is complete.
/// </summary>
/// <remarks></remarks>
public class RunAsyncCompletedEventArgs
{
  private bool m_Cancelled;
  private Exception m_EnqueueError;
  private Exception m_DequeueError;

  public RunAsyncCompletedEventArgs()
  {
    this.m_Cancelled = false;
    this.m_EnqueueError = (Exception) null;
    this.m_DequeueError = (Exception) null;
  }

  /// <summary>
  /// True if the streaming operation was cancelled using the CancelAsync() method.
  /// </summary>
  /// <value></value>
  /// <returns></returns>
  /// <remarks></remarks>
  public bool Cancelled
  {
    get => this.m_Cancelled;
    internal set => this.m_Cancelled = value;
  }

  /// <summary>
  /// Returns any exception that occurred in the ennqueing thread, including stream packet time out exceptions.
  /// </summary>
  /// <value></value>
  /// <returns>Returns exception or null if no exceptions were thrown.</returns>
  /// <remarks></remarks>
  public Exception EnqueueError
  {
    get => this.m_EnqueueError;
    internal set => this.m_EnqueueError = value;
  }

  /// <summary>
  /// Returns any exception that occurred in the dequeueing thread, including file access exceptions.
  /// </summary>
  /// <value></value>
  /// <returns>Returns exception or null if no exceptions were thrown.</returns>
  /// <remarks></remarks>
  public Exception DequeueError
  {
    get => this.m_DequeueError;
    internal set => this.m_DequeueError = value;
  }
}
