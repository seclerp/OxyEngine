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
    public GraphicsRenderer Graphics { get; }

    private GraphicsManager _graphicsManager;
    private RenderTarget2D _renderTarget;

    private Rectangle _rect;
    
    public Canvas(int x, int y, int width, int height)
    {
      _rect = new Rectangle(x, y, width, height);
      
      Graphics = new GraphicsRenderer();
      
      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
      
      Resize(_rect.Width, _rect.Height);
    }

    public void Resize(int width, int height)
    {
      _rect = new Rectangle(_rect.X, _rect.Y, width, height);
    }
    
    public void Draw(Action drawAction)
    {
      drawAction();
    }
  }
}