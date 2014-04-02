﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using MEAClosedLoop;
using System.Windows;
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
  public class CDataRender : Microsoft.Xna.Framework.Game
  {
    #region стандартные значения
    private int DEFAULT_WINDOW_HEIGHT = 600;
    private int DEFAULT_WINDOW_WIDTH = 800;
    #endregion
    #region внутренние данные
    TFltDataPacket DataPacket; //Данные для отрисовки фильтрованных данных
    List<TFltDataPacket> DataPacketHistory; // история данных
    object DataPacketLock = new object(); // блокировка данных
    TTime summary_time_stamp = 0;

    GraphicsDeviceManager graphics;
    // эффект BasicEffect для кривой
    BasicEffect basicEffect;
    // массив массивов вершин нашей кривой
    VertexPositionColor[][] vertices;
    #endregion

    public CDataRender()
    {
      graphics = new GraphicsDeviceManager(this);

      graphics.PreferredBackBufferWidth = DEFAULT_WINDOW_WIDTH; // ширина приложения
      graphics.PreferredBackBufferHeight = DEFAULT_WINDOW_HEIGHT; // высота приложения
      graphics.IsFullScreen = false; // флаг полноэкранного приложения
      graphics.ApplyChanges(); // применяем параметры


      DataPacketHistory = new List<TFltDataPacket>();
    }
    protected override void Initialize()
    {
      (System.Windows.Forms.Control.FromHandle(this.Window.Handle)).Location = new System.Drawing.Point(0, 0);
      basicEffect = new BasicEffect(graphics.GraphicsDevice);
      basicEffect.VertexColorEnabled = true;
      basicEffect.Projection = Matrix.CreateOrthographicOffCenter
         (0, graphics.GraphicsDevice.Viewport.Width,     // left, right
          graphics.GraphicsDevice.Viewport.Height, 0,    // bottom, top
          0, 1);                                         // near, far plane

      base.Initialize();
    }
    protected override void LoadContent()
    {

    }
    protected override void UnloadContent()
    {
    }
    protected override void Update(GameTime gameTime)
    {
      // Allows the game to exit
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        this.Exit();

      // TODO: Add your update logic here

      base.Update(gameTime);
    }
    public void RecivieFltData(TFltDataPacket data)
    {
      lock (DataPacketLock)
      {
        DataPacket = new TFltDataPacket(data);
        DataPacketHistory.Add(DataPacket);
        int AnyExistsKey = DataPacket.Keys.First();
        summary_time_stamp += (TTime)DataPacket[AnyExistsKey].Length;
      }

    }
    protected override void Draw(GameTime gameTime)
    {
      if (DataPacket == null) return;

      //Получаем размеры окна;
      int WindowHeight = graphics.PreferredBackBufferHeight;
      int WindowWidth = graphics.PreferredBackBufferWidth;


      GraphicsDevice.Clear(Color.CornflowerBlue);
      TFltData[] data_to_display;

      //создание массива векторов для каналов
      List<Vector3> ChannelVectors = new List<Vector3>();
      Vector3[] ChannelvectorsArray = new Vector3[DataPacket.Keys.Count()];
      foreach (int key in DataPacket.Keys)
      {
        ChannelVectors.Add(new Vector3());
      }

      // сетка 8 x 8 клеток
      // длина и ширина пропорциональны размеру главного окна
      int CellWidth = WindowWidth / 8;
      int CellHeght = WindowHeight / 8;

      for (int i = 0; i < 8; i++)
      {
        for (int j = 0; j < 8 && i * 8 + j < ChannelvectorsArray.Length; j++)
        {
          ChannelvectorsArray[i * 8 + j] = new Vector3(i * CellWidth, j * CellHeght, 0);
        }
      }

      int RealChannelIndx = 0;
      vertices = new VertexPositionColor[DataPacket.Keys.Count][];

      basicEffect.CurrentTechnique.Passes[0].Apply();
      foreach (int key in DataPacket.Keys)
      {
        //подготовка массива массивов точек
        lock (DataPacketLock)
        {
          int length = DataPacket[RealChannelIndx].Length;
          data_to_display = new TFltData[length];
        }
        vertices[RealChannelIndx] = new VertexPositionColor[data_to_display.Length];
      
        for (int i = 0; i < data_to_display.Length; i++)
        {
          vertices[RealChannelIndx][i].Position.X = ChannelvectorsArray[RealChannelIndx].X + ((float)i * CellWidth) / data_to_display.Length;
          vertices[RealChannelIndx][i].Position.Y = ChannelvectorsArray[RealChannelIndx].Y + CellHeght / 2 - (float)data_to_display[i];
          vertices[RealChannelIndx][i].Position.Z = 0;
        }
        graphics.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineStrip, vertices[RealChannelIndx], 0, vertices.Count() - 2);
        RealChannelIndx++;
      }

      base.Draw(gameTime);
    }
  }
}


