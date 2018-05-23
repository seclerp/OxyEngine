using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.UI.Renderers;
using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.UI.Styles;

namespace OxyEngine.UI
{
  public class Canvas
  {
    private readonly StyleDatabase _styles;
    private readonly GraphicsManager _graphicsManager;
    
    private readonly UIRenderer _renderer;
    
    private Rectangle _canvasArea;
    private AreaStack _areaStack;
    
    public Canvas(int x, int y, int width, int height, StyleDatabase styles)
    {
      _styles = styles;
      _canvasArea = new Rectangle(x, y, width, height);
      _areaStack = new AreaStack();
      
      // Renderers
      _renderer = new UIRenderer(_areaStack, styles);

      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
      
      Resize(_canvasArea.Width, _canvasArea.Height);
    }

    public void Resize(int width, int height)
    {
      _canvasArea = new Rectangle(_canvasArea.X, _canvasArea.Y, width, height);
    }
    
    public void Draw(Action<UIRenderer> action, string canvasSelector = "canvas")
    {
      var color = _styles.GetStyle(canvasSelector).GetRule<Color>("background-color");
      _graphicsManager.SetColor(color.R, color.G, color.B, color.A);
      _graphicsManager.Rectangle("fill", _canvasArea);
      _graphicsManager.SetColor();
      
      _areaStack.Clear();
      _areaStack.Push(_canvasArea);
      
      action(_renderer);
    }
  }
}