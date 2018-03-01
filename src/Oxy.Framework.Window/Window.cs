using System;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Oxy.Framework
{
  /// <summary>
  ///   Class that represents OxyEngine game window
  /// </summary>
  public class Window : IDisposable
  {
    private static readonly GameWindow _instance;

    private static Action _loadEvent;
    private static Action<float> _updateEvent;
    private static Action _drawEvent;

    private static readonly ErrorsDrawHandler _errorsDrawHandler;

    private static AudioContext _context;

    static Window()
    {
      _instance = new GameWindow(800, 600,
        new GraphicsMode(new ColorFormat(8, 8, 8, 0),
          24, // Depth bits
          8, // Stencil bits
          4 // FSAA samples
        ), "OxyEngine Game");
      _instance.WindowBorder = WindowBorder.Fixed;
      _errorsDrawHandler = new ErrorsDrawHandler();
      // Setup default window properties
      SetVSyncEnabled(true);

      SetupGlobalHandling();
    }

    public void Dispose()
    {
      _context.Dispose();
    }

    private static void SetupGlobalHandling()
    {
      AppDomain.CurrentDomain.UnhandledException += (s, a) =>
      {
        Console.WriteLine("=========================\n Working \n=========================");
        _errorsDrawHandler.Fire((Exception) a.ExceptionObject);
        _drawEvent = DrawErrors;
      };
    }

    /// <summary>
    ///   Shows window. Take no effect if window is already shown
    /// </summary>
    /// <param name="maxFps">Max update rate</param>
    public static void Show(float maxFps = 60)
    {
      // Register listeners
      _instance.Load += Load;
      _instance.UpdateFrame += Update;
      _instance.RenderFrame += Draw;
      _instance.Resize += Resize;

      _context = new AudioContext();
      _context.MakeCurrent();

      _instance.Run(maxFps);
      _instance.Exit();
    }

    /// <summary>
    ///   Closes window and exit game
    /// </summary>
    public static void Exit()
    {
      _instance.Exit();
      _instance.Dispose();
    }

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

    #region Window's event handlers

    private static void Load(object sender, EventArgs e)
    {
      GL.Enable(EnableCap.Texture2D);
      GL.Enable(EnableCap.Blend);
      GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
      GL.Enable(EnableCap.DepthTest);
      GL.DepthMask(true);
      GL.DepthFunc(DepthFunction.Lequal);

      _errorsDrawHandler.LoadResources();

      _loadEvent?.Invoke();
    }

    private static void Update(object sender, FrameEventArgs args)
    {
      _updateEvent?.Invoke((float) args.Time);
    }

    private static void Resize(object sender, EventArgs e)
    {
      GL.Viewport(_instance.ClientRectangle);

      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(0, _instance.ClientRectangle.Width, _instance.ClientRectangle.Height, 0, -1.0, 1.0);
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
    /// </summary>
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

    #endregion
  }
}