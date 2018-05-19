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
    private AreaStack _areaStack;
    
    public Canvas(int x, int y, int width, int height)
    {
      _canvasArea = new Rectangle(x, y, width, height);
      _areaStack = new AreaStack();
      
      // Renderers
      _freeGraphics = new FreeGraphicsRenderer(_areaStack);
      _renderer = new RootRenderer(_areaStack);

      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
      
      Resize(_canvasArea.Width, _canvasArea.Height);
    }

    public void Resize(int width, int height)
    {
      _canvasArea = new Rectangle(_canvasArea.X, _canvasArea.Y, width, height);
    }
    
    public void Draw(Action<RootRenderer> action)
    {
      _graphicsManager.SetColor(100, 100, 100);
      _graphicsManager.Rectangle("fill", _canvasArea);
      _graphicsManager.SetColor();
      
      _areaStack.Clear();
      _areaStack.Push(_canvasArea);
      
      action(_renderer);
    }
  }
}