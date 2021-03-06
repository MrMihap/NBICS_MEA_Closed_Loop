﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MEAClosedLoop
{
  #region Definitions
  using TData = System.Double;
  using TTime = System.UInt64;
  using TStimIndex = System.Int16;
  using TAbsStimIndex = System.UInt64;
  using TRawData = UInt16;
  using TRawDataPacket = Dictionary<int, ushort[]>;
  using TFltDataPacket = Dictionary<int, System.Double[]>;
  using TFltData = System.Double;
  #endregion

  public class CDataFlowController
  {
    private List<IRecieveBusrt> BurstRecievers = new List<IRecieveBusrt>();
    private List<IRecieveFltData> FltDataRecievers = new List<IRecieveFltData>();
    private List<IRecieveStim> StimRecievers = new List<IRecieveStim>();
    private List<IRecieveEvokedBurst> EvokedBurstRecievers = new List<IRecieveEvokedBurst>();

    private object LockBurstRecievers = new object();
    private object LockFltDataRecievers = new object();
    private object LockStimRecievers = new object();
    private object LockEvBurstRecievers = new object();

    //одписывание интерфейсов на раздачу потоков данных
    public void AddConsumer<ObjType>(ObjType Obj)
    {
      if (Obj is IRecieveBusrt && !BurstRecievers.Contains(Obj as IRecieveBusrt))
      {
        lock (LockBurstRecievers) BurstRecievers.Add(Obj as IRecieveBusrt);
      }
      if (Obj is IRecieveFltData && !FltDataRecievers.Contains(Obj as IRecieveFltData))
      {
        lock (LockFltDataRecievers) FltDataRecievers.Add(Obj as IRecieveFltData);
      }
      if (Obj is IRecieveStim && !StimRecievers.Contains(Obj as IRecieveStim))
      {
        lock (LockStimRecievers) StimRecievers.Add(Obj as IRecieveStim);
      }
      if (Obj is IRecieveEvokedBurst && !EvokedBurstRecievers.Contains(Obj as IRecieveEvokedBurst))
      {
        lock (LockEvBurstRecievers) EvokedBurstRecievers.Add(Obj as IRecieveEvokedBurst);
      }
    }

    //одписывание интерфейсов на раздачу потоков данных
    public void RemoveConsumer<ObjType>(ObjType Obj)
    {
      if (Obj is IRecieveBusrt)
      {
        throw new NotImplementedException("in develop:");
      }
      if (Obj is IRecieveFltData)
      {
        throw new NotImplementedException("in develop:");
      }
      if (Obj is IRecieveStim)
      {
        throw new NotImplementedException("in develop:");
      }
    }

    #region Recieve&Send Data Methods
    public void RecieveBurstData(CPack Pack)
    {
      List<IRecieveBusrt> RecieversToRemove = new List<IRecieveBusrt>();
      lock (LockBurstRecievers)
        foreach (IRecieveBusrt reciever in BurstRecievers)
        {
          try
          {
            reciever.RecieveBurst(Pack);
          }
          catch(Exception e)
          {
            RecieversToRemove.Add(reciever);
          }
        }
      lock (LockBurstRecievers)
      {
        BurstRecievers.RemoveAll(s => RecieversToRemove.Contains(s));
      }
    }
    public void RecieveFltData(TFltDataPacket DataPacket)
    {
      List<IRecieveFltData> RecieversToRemove = new List<IRecieveFltData>();
      lock (LockFltDataRecievers)
        foreach (IRecieveFltData reciever in FltDataRecievers)
        {
          try
          {
            reciever.RecieveFltData(DataPacket);
          }
          catch (Exception e)
          {
            RecieversToRemove.Add(reciever);
          }
        }
      lock(LockFltDataRecievers)
      {
        FltDataRecievers.RemoveAll(s => RecieversToRemove.Contains(s));
      }
    }
    public void RecieveStim(List<TAbsStimIndex> stims)
    {
      List<IRecieveStim> RecieversToRemove = new List<IRecieveStim>();
      lock (LockStimRecievers)
        foreach (IRecieveStim reciever in StimRecievers)
        {
          try
          {
            reciever.RecieveStim(stims);
          }
          catch (Exception e)
          {
            RecieversToRemove.Add(reciever);
          }
        }
      lock (LockStimRecievers) StimRecievers.RemoveAll(s => RecieversToRemove.Contains(s));
    }
    public void RecieveEvPack(SEvokedPack EvBurst)
    {
      List<IRecieveEvokedBurst> RecieversToRemove = new List<IRecieveEvokedBurst>();
      lock (LockEvBurstRecievers)
      {
        foreach (IRecieveEvokedBurst reciever in EvokedBurstRecievers)
        {
          try
          {
            reciever.RecieveEvokedBurst(EvBurst);
          }
          catch (Exception e)
          {
            RecieversToRemove.Add(reciever);
          }
        }
      }
      lock (LockEvBurstRecievers) EvokedBurstRecievers.RemoveAll(s => RecieversToRemove.Contains(s));
    }
    #endregion


  }
}
