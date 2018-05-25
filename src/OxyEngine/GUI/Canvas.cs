using System;
using Microsoft.Xna.Framework;
using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.GUI.Renderers;
using OxyEngine.GUI.Styles;

namespace OxyEngine.GUI
{
  public class Canvas
  {
    private readonly StyleDatabase _styles;
    private readonly GraphicsManager _graphicsManager;
    
    private readonly GuiRenderer _renderer;
    
    private Rectangle _canvasArea;
    private AreaStack _areaStack;
    
    public Canvas(int x, int y, int width, int height, StyleDatabase styles)
    {
      _styles = styles;
      _canvasArea = new Rectangle(x, y, width, height);
      _areaStack = new AreaStack();
      
      // Renderers
      _renderer = new GuiRenderer(_areaStack, styles);

      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
      
      Resize(_canvasArea.Width, _canvasArea.Height);
    }

    public void Resize(int width, int height)
    {
      _canvasArea = new Rectangle(_canvasArea.X, _canvasArea.Y, width, height);
    }
    
    public void Draw(Action<GuiRenderer> action, string canvasSelector = "canvas")
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