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
    // " = () => { } " - this shit is needed to do not make 'if' to check listeners at runtime
    
    public Window(int width, int height, string title)
                : base(width, height, GraphicsMode.Default, title)
    {
      // TODO: By default let it be on
      VSync = VSyncMode.On;
    }
    
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      GL.ClearColor(0.1f, 0.2f, 0.5f, 0.0f);
      GL.Enable(EnableCap.DepthTest);
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      
      // TODO: Add normal handling
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
      base.OnUpdateFrame(e);
      
      // TODO: Add normal handling
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
      base.OnRenderFrame(e);

      // TODO: Add normal handling
      
      SwapBuffers();
    }
  }
}