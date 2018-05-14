using System;
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
    
    public Canvas(int width, int height)
    {
      Graphics = new GraphicsRenderer();
      
      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
      
      Resize(width, height);
    }

    public void Resize(int width, int height)
    {
      _renderTarget?.Dispose();
      _renderTarget = _graphicsManager.NewRenderTexture(width, height);
    }
    
    public void Draw(Action drawAction)
    {
      _graphicsManager.SetRenderTexture(_renderTarget);
      drawAction.Invoke();
      _graphicsManager.SetRenderTexture();
    }
  }
}