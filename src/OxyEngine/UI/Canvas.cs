using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.UI.Renderers;
using OxyEngine.Dependency;
using OxyEngine.Graphics;

namespace OxyEngine.UI
{
  public class Canvas
  {
    public FreeGraphicsRenderer FreeGraphics { get; }

    private GraphicsManager _graphicsManager;
    private RenderTarget2D _renderTarget;
    private RootRenderer _rendrer;
    
    private Rectangle _canvasArea;
    
    public Canvas(int x, int y, int width, int height)
    {
      _canvasArea = new Rectangle(x, y, width, height);
      
      FreeGraphics = new FreeGraphicsRenderer();
      
      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
      _rendrer = new RootRenderer();
      
      Resize(_canvasArea.Width, _canvasArea.Height);
    }

    public void Resize(int width, int height)
    {
      _canvasArea = new Rectangle(_canvasArea.X, _canvasArea.Y, width, height);
    }
    
    public void Draw(Action<RootRenderer> action)
    {
      action(_rendrer);
    }
  }
}