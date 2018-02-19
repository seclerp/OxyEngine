using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Oxy.Framework
{
  /// <summary>
  /// Class that represents OxyEngine game window
  /// </summary>
  public class Window : Module<GameWindow>
  {
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
      GL.ClearColor(Color.CornflowerBlue);
            
      GL.Enable(EnableCap.Texture2D);
      GL.Enable(EnableCap.Blend);
      GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
      GL.Enable(EnableCap.DepthTest);
    }

    private static void Update(object sender, FrameEventArgs args)
    {
      // TODO
    }

    private static void Resize(object sender, EventArgs e)
    {
      GL.Viewport(Instance.ClientRectangle);

      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(0, 1, 1, 0, -4.0, 4.0);
    }

    private static void Draw(object sender, FrameEventArgs e)
    {
      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
      #region Camera setup

      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadIdentity();
            
      #endregion
      
      #region Rendering

      // ....

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
      Instance.VSync == VSyncMode.On ? true : false;

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

      Instance.Run(maxFps);
    }

    /// <summary>
    /// Closes window and exit game
    /// </summary>
    public static void Exit()
    {
      Instance.Exit();
    }

    /// <summary>
    /// Calls 'handler' function every frame and pass delta time as float
    /// </summary>
    /// <param name="handler">Function to call</param>
    public static void OnUpdate(Action<float> handler) 
    {
      Instance.UpdateFrame += (s, e) => handler((float) e.Time);
    }
  }
}