using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Entities;
using OxyEngine.Resources;
using OxyEngine.UI;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Models;
using OxyEngine.UI.Renderers;
using OxyEngine.Window;
using IDrawable = OxyEngine.Ecs.Behaviours.IDrawable;

namespace OxyEngine.Game
{
  public class UIScene : TransformEntity, IInitializable, ILoadable, IDrawable
  {
    private Canvas _canvas;
    private WindowManager _windowManager;
    private ResourceManager _resourceManager;
    private SpriteFont _font;

    private Color _canvasBackColor;
    private Color _panelBackColor;
    private Color _panelHeaderColor;
    
    public void Init()
    {
      _windowManager = Container.Instance.ResolveByName<WindowManager>(InstanceName.WindowManager);
      _resourceManager = Container.Instance.ResolveByName<ResourceManager>(InstanceName.ResourceManager);

      _canvas = new Canvas(0, 0, _windowManager.GetWidth(), _windowManager.GetHeight());
      
      _canvasBackColor = new Color(6, 82, 80);
      _panelBackColor = new Color(44, 142, 123);
      _panelHeaderColor = new Color(76, 194, 159);
    }
    
    public void Load()
    {
      _font = _resourceManager.LoadFont("Roboto-Regular");
    }
    
    public void Draw()
    {
      _canvas.Draw(rootRederer =>
      {
        // Root panel with background
        rootRederer.FreeLayout(new Rectangle(0, 0, _windowManager.GetWidth(), _windowManager.GetHeight()), 
          rootLayoutRenderer =>
          {
            Panel(rootLayoutRenderer, new Rectangle(50, 50, 200, 200), "Text", renderer =>
            {
              renderer.Text(new Rectangle(0, 10, 200, 25), _font, "Left aligned"
                , hTextAlign: HorizontalAlignment.Left);
              renderer.Text(new Rectangle(0, 35, 200, 25), _font, "Center aligned"
                , hTextAlign: HorizontalAlignment.Center);
              renderer.Text(new Rectangle(0, 60, 200, 25), _font, "Right aligned"
                , hTextAlign: HorizontalAlignment.Right);
            });
          }
          , backColor: _canvasBackColor);
      });
    }

    private void Panel(UIRenderer renderer, Rectangle rect, string title, Action<UIRenderer> action)
    {
      renderer.Text(new Rectangle(rect.X, rect.Y, rect.Width, 25), _font, title
        , hTextAlign: HorizontalAlignment.Center, vTextAlign: VerticalAlignment.Middle, backColor: _panelHeaderColor);
      renderer.FreeLayout(new Rectangle(rect.X, rect.Y + 25, rect.Width, rect.Height), action, _panelBackColor);
    }
  }
}