using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Oxy.Framework
{
  /// <summary>
  /// Class that represents OxyEngine game window
  /// </summary>
  public class Window : GameWindow
  {
    public Window(int width, int height, string title)
                : base(width, height, GraphicsMode.Default, title)
    {
      // TODO: By default let it be on
      VSync = VSyncMode.On;
    }
    
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      GL.ClearColor(Color.CornflowerBlue);
            
      GL.Enable(EnableCap.Texture2D);
      GL.Enable(EnableCap.Blend);
      GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
      GL.Enable(EnableCap.DepthTest);
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      
      GL.Viewport(ClientRectangle);

      GL.MatrixMode(MatrixMode.Projection);
      GL.LoadIdentity();
      GL.Ortho(0, 1, 1, 0, -4.0, 4.0);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
      #region Camera setup

      GL.MatrixMode(MatrixMode.Modelview);
      GL.LoadIdentity();
            
      #endregion
      
      // Render code goes here
      
      SwapBuffers();
    }
  }
}