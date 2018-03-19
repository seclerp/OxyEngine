﻿using System;
using System.Drawing;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Oxy.Framework.Exceptions;
using Oxy.Framework.Rendering;

namespace Oxy.Framework
{
  /// <summary>
  ///   Class that represents OxyEngine game window
  /// </summary>
  public class Window : IDisposable
  {
    internal static GameWindow _instance;

    private static Action _loadEvent;
    private static Action<float> _updateEvent;
    private static Action _drawEvent;

    private static readonly ErrorsDrawHandler _errorsDrawHandler;
    private static bool _isDebug;
    private static bool _updateCalled = false;
    
    private static AudioContext _context;

    private static float _updateMs;
    private static float _updateCounter;
    private static float _updateTimer;
    private static float _fps;

    static Window()
    {
      InitWindow("OxyEngine Game");
      _errorsDrawHandler = new ErrorsDrawHandler();
    }

    public void Dispose()
    {
      _context.Dispose();
    }

    private static void InitWindow(string title)
    {
      _instance = new GameWindow(800, 600,
        new GraphicsMode(new ColorFormat(8, 8, 8, 0),
          24, // Depth bits
          8, // Stencil bits
          0 // FSAA samples
        ), title);

      _instance.WindowBorder = WindowBorder.Fixed;

      // Setup default window properties
      SetVSyncEnabled(true);
    }

    #region Error window

    private static void SwitchToErrorScreen(Exception exception)
    {
      _errorsDrawHandler.Fire(exception);
      _drawEvent = DrawErrors;
    }


    public static void Error(Exception e, bool isPython = false)
    {
      Exit();
      InitWindow("Error");
      SwitchToErrorScreen(e);
      _updateCalled = true;
      _loadEvent = null;
      _instance.Load += Load;
      _instance.RenderFrame += Draw;
      _instance.Resize += Resize;
      _instance.Run(60);
      _instance.Exit();
    }

    #endregion

    /// <summary>
    ///   Shows window. Take no effect if window is already shown
    /// </summary>
    /// <param name="maxFps">Max update rate</param>
    public static void Show(float maxFps = 60)
    {
      try
      {
        // Register listeners
        _instance.Load += Load;
        _instance.UpdateFrame += Update;
        _instance.RenderFrame += Draw;
        _instance.KeyUp += KeyUp;
        _instance.Resize += Resize;

        _context = new AudioContext();
        _context.MakeCurrent();

        _instance.Run(60, 60);

        _instance.Exit();
      }
      catch (PyException e)
      {
        if (_isDebug)
          throw e;

        Error(e, true);
      }
      catch (Exception e)
      {
        if (_isDebug)
          throw e;

        Error(e);
      }
    }

    /// <summary>
    ///   Closes window and exit game
    /// </summary>
    public static void Exit()
    {
      _instance.Exit();
    }


    #region Events

    /// <summary>
    ///   Calls 'handler' when window is initialized
    /// </summary>
    /// <param name="handler">Function to call</param>
    public static void OnLoad(Action handler)
    {
      _loadEvent += handler;
    }

    /// <summary>
    ///   Calls 'handler' function every frame and pass delta time as float
    /// </summary>
    /// <param name="handler">Function to call</param>
    public static void OnUpdate(Action<float> handler)
    {
      _updateEvent += handler;
    }

    /// <summary>
    ///   Calls 'handler' function after update (when draw requested)
    /// </summary>
    /// <param name="handler">Function to call</param>
    public static void OnDraw(Action handler)
    {
      _drawEvent += handler;
    }

    #endregion

    #region Window's event handlers

    private static void Load(object sender, EventArgs e)
    {
      GL.Enable(EnableCap.Texture2D);
      GL.Enable(EnableCap.Blend);
      GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
      GL.Enable(EnableCap.DepthTest);
      GL.DepthMask(true);
      GL.DepthFunc(DepthFunction.Lequal);

      ResetViewport();

      _errorsDrawHandler.LoadResources();

      _loadEvent?.Invoke();
    }

    private static void Update(object sender, FrameEventArgs args)
    {
      _updateTimer += (float)args.Time;
      _updateCounter++;

      if (_updateTimer >= 1.0)
      {
        _updateMs = 1000f / _updateCounter;
        _fps = _updateCounter;
        _updateCounter = 0;
        _updateTimer = 0;
      }

      _updateEvent?.Invoke((float) args.Time);
      _updateCalled = true;
    }

    internal static void ResetViewport()
    {
      GL.Viewport(_instance.ClientRectangle);

      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(0, _instance.ClientRectangle.Width, _instance.ClientRectangle.Height, 0, -1.0, 1.0);
    }

    private static void Resize(object sender, EventArgs e)
    {
      ResetViewport();
    }

    private static void KeyUp(object sender, KeyboardKeyEventArgs args)
    {
      if (args.Alt && args.Key == Key.F4)
        Exit();
    }

    // Handler that replaces _drawEvent to draw errors
    private static void DrawErrors()
    {
      _errorsDrawHandler.DrawErrors();
    }

    private static void Draw(object sender, FrameEventArgs e)
    {
      (byte, byte, byte, byte) bgColor = Graphics.GetBackgroundColor();
      GL.ClearColor(Color.FromArgb(bgColor.Item4, bgColor.Item1, bgColor.Item2, bgColor.Item3));

      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

      #region Camera setup

      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadIdentity();

      #endregion

      #region Rendering

      if (_updateCalled)
        _drawEvent?.Invoke();

      #endregion

      _instance.SwapBuffers();
    }

    #endregion

    #region Get

    /// <summary>
    ///   Returns window width
    /// </summary>
    public static int GetWidth()
    {
      return _instance.Width;
    }

    /// <summary>
    ///   Returns window height
    /// </summary>
    public static int GetHeight()
    {
      return _instance.Height;
    }

    /// <summary>
    ///   Returns window title
    /// </summary>
    public static string GetTitle()
    {
      return _instance.Title;
    }

    /// <summary>
    ///   Returns true if vertical sync enabled, otherwise false
    /// </summary>
    public static bool GetVSyncEnabled()
    {
      return _instance.VSync == VSyncMode.On;
    }

    /// <summary>
    ///   Returns fullscreen mode
    /// </summary>
    public static bool GetFullscreen()
    {
      return _instance.WindowState == WindowState.Fullscreen;
    }
    
    /// <summary>
    ///   Returns debug mode
    /// </summary>
    public static bool GetDebugMode()
    {
      return _isDebug;
    }

    public static float GetCursorX()
    {
      return _instance.Mouse.X;
    }

    public static float GetCursorY()
    {
      return _instance.Mouse.Y;
    }

    public static int GetMouseWheel()
    {
      return _instance.Mouse.Wheel;
    }

    public static float GetRenderTime()
    {
      return _updateMs;
    }

    public static float GetFPS()
    {
      return _fps;
    }

    public static float GetMouseWheelPrecised()
    {
      return _instance.Mouse.WheelPrecise;
    }

    #endregion

    #region Set

    /// <summary>
    ///   Sets window width
    /// </summary>
    /// <param name="width">New width</param>
    public static void SetWidth(int width)
    {
      _instance.Width = width;
    }

    /// <summary>
    ///   Sets window height
    /// </summary>YtpzНе
    /// <param name="height">New height</param>
    public static void SetHeight(int height)
    {
      _instance.Height = height;
    }

    /// <summary>
    ///   Sets window title
    /// </summary>
    /// <param name="title">New title</param>
    public static void SetTitle(string title)
    {
      _instance.Title = title;
    }

    /// <summary>
    ///   Enables or disables vertical sync
    /// </summary>
    /// <param name="enabled">true to enable VSync, false to disable</param>
    public static void SetVSyncEnabled(bool enabled)
    {
      _instance.VSync = enabled ? VSyncMode.On : VSyncMode.Off;
    }

    /// <summary>
    ///   Sets the fullscreen mode
    /// </summary>
    /// <param name="fullscreen">true to enable, false to disable</param>
    public static void SetFullscreen(bool fullscreen)
    {
      _instance.WindowState = fullscreen ? WindowState.Fullscreen : WindowState.Normal;
    }

    /// <summary>
    ///   True for pass exceptions without error window
    /// </summary>
    /// <param name="debugMode"></param>
    public static void SetDebugMode(bool debugMode = true)
    {
      _isDebug = debugMode;
    }

    #endregion
  }
}