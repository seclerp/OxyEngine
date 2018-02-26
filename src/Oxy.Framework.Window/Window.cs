using System;
using OpenTK;
using OpenTK.Audio;
using OpenTK.Graphics.OpenGL;

namespace Oxy.Framework
{
  /// <summary>
  /// Class that represents OxyEngine game window
  /// </summary>
  public class Window : Module<GameWindow>, IDisposable
  {
    private static Action _loadEvent;
    private static Action<float> _updateEvent;
    private static Action _drawEvent;

    private static AudioContext _context;
    
    static Window()
    {
      // Setup default window properties
      SetWidth(800);
      SetHeight(600);
      SetTitle("OxyEngine Game");
      SetVSyncEnabled(true);
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
      
      _loadEvent?.Invoke();
    }

    private static void Update(object sender, FrameEventArgs args)
    {
      _updateEvent?.Invoke((float)args.Time);
    }

    private static void Resize(object sender, EventArgs e)
    {
      GL.Viewport(Instance.ClientRectangle);

      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(0, Instance.ClientRectangle.Width, Instance.ClientRectangle.Height, 0, -1.0, 1.0);
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
      
      Instance.SwapBuffers();
    }

    #endregion

    #region Get

    /// <summary>
    /// Returns window width
    /// </summary>
    public static int GetWidth() =>
      Instance.Width;

    /// <summary>
    /// Returns window height
    /// </summary>
    public static int GetHeight() =>
      Instance.Height;

    /// <summary>
    /// Returns window title
    /// </summary>
    public static string GetTitle() =>
      Instance.Title;

    /// <summary>
    /// Returns true if vertical sync enabled, otherwise false
    /// </summary>
    public static bool GetVSyncEnabled() =>
      Instance.VSync == VSyncMode.On;

    #endregion

    #region Set

    /// <summary>
    /// Sets window width
    /// </summary>
    /// <param name="width">New width</param>
    public static void SetWidth(int width) =>
      Instance.Width = width;

    /// <summary>
    /// Sets window height
    /// </summary>
    /// <param name="height">New height</param>
    public static void SetHeight(int height) =>
      Instance.Height = height;

    /// <summary>
    /// Sets window title
    /// </summary>
    /// <param name="title">New title</param>
    public static void SetTitle(string title) =>
      Instance.Title = title;

    /// <summary>
    /// Enables or disables vertical sync
    /// </summary>
    /// <param name="enabled">true to enable VSync, false to disable</param>
    public static void SetVSyncEnabled(bool enabled) =>
      Instance.VSync = enabled ? VSyncMode.On : VSyncMode.Off;

    #endregion

    /// <summary>
    /// Shows window. Take no effect if window is already shown
    /// </summary>
    /// <param name="maxFps">Max update rate</param>
    public static void Show(float maxFps = 60)
    {
      // Register listeners
      Instance.Load += Load;
      Instance.UpdateFrame += Update;
      Instance.RenderFrame += Draw;
      Instance.Resize += Resize;

      _context = new AudioContext();
      _context.MakeCurrent();
      
      Instance.Run(maxFps);
      Instance.Exit();
    }

    /// <summary>
    /// Closes window and exit game
    /// </summary>
    public static void Exit()
    {
      Instance.Exit();
      Instance.Dispose();
    }
    
    /// <summary>
    /// Calls 'handler' when window is initialized
    /// </summary>
    /// <param name="handler">Function to call</param>
    public static void OnLoad(Action handler)
    {
      _loadEvent += handler;
    }

    /// <summary>
    /// Calls 'handler' function every frame and pass delta time as float
    /// </summary>
    /// <param name="handler">Function to call</param>
    public static void OnUpdate(Action<float> handler)
    {
      _updateEvent += handler;
    }
    
    /// <summary>
    /// Calls 'handler' function after update (when draw requested)
    /// </summary>
    /// <param name="handler">Function to call</param>
    public static void OnDraw(Action handler)
    {
      _drawEvent += handler;
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}