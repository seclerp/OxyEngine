using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Entities;
using OxyEngine.Resources;
using OxyEngine.UI;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Renderers;
using OxyEngine.UI.Styles;
using OxyEngine.Window;
using IDrawable = OxyEngine.Ecs.Behaviours.IDrawable;

namespace OxyEngine.Game
{
  public class UIScene : TransformEntity, IInitializable, ILoadable, IDrawable
  {
    private Canvas _canvas;
    private WindowManager _windowManager;
    private ResourceManager _resourceManager;

    private StyleDatabase _styles;
    private SpriteFont _font;

    private Color _canvasBackColor;
    private Color _panelBackColor;
    private Color _innerpanelBackColor;
    private Color _panelHeaderColor;
    
    public void Init()
    {
      _windowManager = Container.Instance.ResolveByName<WindowManager>(InstanceName.WindowManager);
      _resourceManager = Container.Instance.ResolveByName<ResourceManager>(InstanceName.ResourceManager);
      
      _canvasBackColor = new Color(6, 82, 80);
      _panelBackColor = new Color(44, 142, 123);
      _innerpanelBackColor = new Color(9, 119, 116);
      _panelHeaderColor = new Color(76, 194, 159);
      
      _styles = new StyleDatabase();

      _styles
        .AddStyle("canvas", // Canvas styles
          new Style()
            .SetRule("background-color", new Color(6, 82, 80))
            .SetRule("color", Color.White)
        )
        .AddStyle("panel", // Panel styles
          new Style()
            .SetRule("background-color", new Color(44, 142, 123))
        )
        .AddStyle("panel-header", // Panel header styles
          new Style()
            .SetRule("background-color", new Color(76, 194, 159))
        )
        .AddStyle("inner-panel", // Panel styles
          new Style()
            .SetRule("background-color", new Color(44, 142, 123))
            .SetRule("title-background-color", new Color(76, 194, 159))
        )
        .SetChildRelation("panel", "canvas");
      
      _canvas = new Canvas(0, 0, _windowManager.GetWidth(), _windowManager.GetHeight(), _styles);
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
          freeLayoutRenderer =>
          {
            // Text examples panel
            freeLayoutRenderer.Panel(new Rectangle(50, 50, 230, 400), "Text", renderer =>
              {
                //TextAlignment(freeLayoutRenderer);
                //TextWrapping(freeLayoutRenderer);
              }
              , "panel"
              , "panel-header"
            );
            
            // Image examples panel
            freeLayoutRenderer.Panel(new Rectangle(330, 50, 230, 400), "Images", renderer =>
              {
              }
              , "panel"
              , "panel-header"
            );
          }
          , "canvas"
        );
      });
    }

//    private void TextAlignment(UIRenderer renderer)
//    {
//      renderer.Text(new Rectangle(5, 5, 220, 25), _font, "Alignment:"
//        , hTextAlign: HorizontalAlignment.Center
//      );
//      
//      renderer.Text(new Rectangle(5, 30, 70, 70), _font, "Text"
//        , hTextAlign: HorizontalAlignment.Left
//        , vTextAlign: VerticalAlignment.Top
//        , backColor: _innerpanelBackColor
//      );
//      renderer.Text(new Rectangle(80, 30, 70, 70), _font, "Text"
//        , hTextAlign: HorizontalAlignment.Center
//        , vTextAlign: VerticalAlignment.Top
//        , backColor: _innerpanelBackColor
//      );
//      renderer.Text(new Rectangle(155, 30, 70, 70), _font, "Text"
//        , hTextAlign: HorizontalAlignment.Right
//        , vTextAlign: VerticalAlignment.Top
//        , backColor: _innerpanelBackColor
//      );
//      
//      renderer.Text(new Rectangle(5, 110, 70, 70), _font, "Text"
//        , hTextAlign: HorizontalAlignment.Left
//        , vTextAlign: VerticalAlignment.Middle
//        , backColor: _innerpanelBackColor
//      );
//      renderer.Text(new Rectangle(80, 110, 70, 70), _font, "Text"
//        , hTextAlign: HorizontalAlignment.Center
//        , vTextAlign: VerticalAlignment.Middle
//        , backColor: _innerpanelBackColor
//      );
//      renderer.Text(new Rectangle(155, 110, 70, 70), _font, "Text"
//        , hTextAlign: HorizontalAlignment.Right
//        , vTextAlign: VerticalAlignment.Middle
//        , backColor: _innerpanelBackColor
//      );
//      
//      renderer.Text(new Rectangle(5, 185, 70, 70), _font, "Text"
//        , hTextAlign: HorizontalAlignment.Left
//        , vTextAlign: VerticalAlignment.Bottom
//        , backColor: _innerpanelBackColor
//      );
//      renderer.Text(new Rectangle(80, 185, 70, 70), _font, "Text"
//        , hTextAlign: HorizontalAlignment.Center
//        , vTextAlign: VerticalAlignment.Bottom
//        , backColor: _innerpanelBackColor
//      );
//      renderer.Text(new Rectangle(155, 185, 70, 70), _font, "Text"
//        , hTextAlign: HorizontalAlignment.Right
//        , vTextAlign: VerticalAlignment.Bottom
//        , backColor: _innerpanelBackColor
//      );
//    }
//    
//    private void TextWrapping(UIRenderer renderer)
//    {
//      renderer.Text(new Rectangle(5, 260, 220, 25), _font, "Wrapping:"
//        , hTextAlign: HorizontalAlignment.Center
//      );
//      
//      renderer.Text(new Rectangle(5, 285, 107, 107), _font, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
//        , backColor: _innerpanelBackColor
//      );
//      renderer.Text(new Rectangle(117, 285, 107, 107), _font, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
//        , backColor: _innerpanelBackColor
//        , textWrap: true
//      );
//    }
  }
}