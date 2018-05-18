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

    private GraphicsManager _graphicsManager;
    private RenderTarget2D _renderTarget;
    
    private RootRenderer _renderer;
    private FreeGraphicsRenderer _freeGraphics { get; }
    
    private Rectangle _canvasArea;
    private AreaWrapper _clientArea;
    
    public Canvas(int x, int y, int width, int height)
    {
      _canvasArea = new Rectangle(x, y, width, height);
      _clientArea = new AreaWrapper { Area = _canvasArea };
      
      _freeGraphics = new FreeGraphicsRenderer(_clientArea);
      
      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
      _renderer = new RootRenderer(_clientArea);
      
      Resize(_canvasArea.Width, _canvasArea.Height);
    }

    public void Resize(int width, int height)
    {
      _canvasArea = new Rectangle(_canvasArea.X, _canvasArea.Y, width, height);
    }
    
    public void Draw(Action<RootRenderer> action)
    {
      _clientArea.Area = _canvasArea;
      action(_renderer);
    }
  }
}