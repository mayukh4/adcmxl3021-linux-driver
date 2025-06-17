// Decompiled with JetBrains decompiler
// Type: StreamDataLogger.FakeStreamProducer
// Assembly: StreamDataLogger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 328A96D1-45A7-47F9-A7ED-7DBD0C49147E
// Assembly location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.dll
// XML documentation location: C:\Users\bagch\OneDrive\Documents\Mayukh_misc\adcmxl_evaluation_rev_2192\StreamDataLogger.xml

using adisInterface;
using RegMapClasses;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;

#nullable disable
namespace StreamDataLogger;

public class FakeStreamProducer : IBufferedStreamProducer, IStreamEventProducer
{
  private ConcurrentQueue<ushort[]> m_Data;
  private Thread m_StreamThread;
  private int m_NumBuffers;
  private int m_BufSize;
  private bool m_Cancel;
  public int BufferDelayMs;

  public event IStreamEventProducer.NewBufferAvailableEventHandler NewBufferAvailable;

  public event IStreamEventProducer.StreamFinishedEventHandler StreamFinished;

  public FakeStreamProducer() => this.BufferDelayMs = 0;

  private void StreamWorker()
  {
    uint num1 = 0;
    List<ushort> ushortList = new List<ushort>();
    while ((long) num1 < (long) this.m_NumBuffers & !this.m_Cancel)
    {
      ushort num2 = checked ((ushort) unchecked ((long) num1 % (long) checked ((int) ushort.MaxValue - this.m_BufSize - 1)));
      int num3 = checked (this.m_BufSize - 1);
      int num4 = 0;
      while (num4 <= num3)
      {
        ushortList.Add(checked ((ushort) (num4 + (int) num2)));
        checked { ++num4; }
      }
      this.m_Data.Enqueue(ushortList.ToArray());
      ushortList.Clear();
      num1 = checked ((uint) ((long) num1 + 1L));
      // ISSUE: reference to a compiler-generated field
      IStreamEventProducer.NewBufferAvailableEventHandler bufferAvailableEvent = this.NewBufferAvailableEvent;
      if (bufferAvailableEvent != null)
        bufferAvailableEvent(this.m_Data.Count);
      if (this.BufferDelayMs > 0)
        Thread.Sleep(this.BufferDelayMs);
    }
    // ISSUE: reference to a compiler-generated field
    IStreamEventProducer.StreamFinishedEventHandler streamFinishedEvent = this.StreamFinishedEvent;
    if (streamFinishedEvent == null)
      return;
    streamFinishedEvent();
  }

  public void CancelStreamAsync() => this.m_Cancel = true;

  public void StartBufferedStream(
    IEnumerable<RegClass> regList,
    uint numCaptures,
    uint numBuffers,
    int timeoutSeconds,
    BackgroundWorker worker)
  {
    this.m_BufSize = 0;
    try
    {
      foreach (RegClass reg in regList)
      {
        // ISSUE: variable of a reference type
        int& local;
        // ISSUE: explicit reference operation
        int num = checked (^(local = ref this.m_BufSize) + Math.Max((int) Math.Round(unchecked ((double) reg.NumBytes / 2.0)), 1));
        local = num;
      }
    }
    finally
    {
      IEnumerator<RegClass> enumerator;
      enumerator?.Dispose();
    }
    this.m_BufSize = checked ((int) ((long) this.m_BufSize * (long) numCaptures));
    this.m_NumBuffers = checked ((int) numBuffers);
    this.m_Data = new ConcurrentQueue<ushort[]>();
    this.m_Cancel = false;
    this.m_StreamThread = new Thread(new ThreadStart(this.StreamWorker));
    this.m_StreamThread.Start();
  }

  public uint[] ConvertReadDataToU32(IEnumerable<RegClass> regList, IEnumerable<ushort> u16data)
  {
    throw new NotImplementedException();
  }

  public ushort[] GetBufferedStreamDataPacket()
  {
    ushort[] result = (ushort[]) null;
    this.m_Data.TryDequeue(out result);
    return result;
  }

  public double[] ScaleRegData(IEnumerable<RegClass> regList, IEnumerable<uint> uintData)
  {
    throw new NotImplementedException();
  }
}
