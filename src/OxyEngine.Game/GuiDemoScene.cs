using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Entities;
using OxyEngine.GUI;
using OxyEngine.GUI.Enums;
using OxyEngine.GUI.Renderers;
using OxyEngine.GUI.Styles;
using OxyEngine.Input;
using OxyEngine.Resources;
using OxyEngine.Window;
using IDrawable = OxyEngine.Ecs.Behaviours.IDrawable;
using IUpdateable = OxyEngine.Ecs.Behaviours.IUpdateable;

namespace OxyEngine.Game
{
  public class GuiDemoScene : TransformEntity, IInitializable, IUpdateable, IDrawable
  {
    private Canvas _canvas;
    private WindowManager _windowManager;
    private ResourceManager _resourceManager;
    private InputManager _inputManager;

    private Texture2D _exampleImage;
    private Texture2D _exampleImage2;
    
    private StyleDatabase _styles;

    private Action<GuiRenderer> _currentWindowRenderer;
    private int _currentWindowRendererId;
    private Action<GuiRenderer>[] _windowRenderers;
    
    public void Init()
    {
      _windowManager = Container.Instance.ResolveByName<WindowManager>(InstanceName.WindowManager);
      _resourceManager = Container.Instance.ResolveByName<ResourceManager>(InstanceName.ResourceManager);
      _inputManager = Container.Instance.ResolveByName<InputManager>(InstanceName.InputManager);

      _exampleImage = _resourceManager.LoadTexture("planet");
      _exampleImage2 = _resourceManager.LoadTexture("mario");
      
      _styles = new StyleDatabase();

      _styles
        .AddStyle("canvas", // Canvas styles
          new Style()
            .SetRule("background-color", new Color(6, 82, 80))
            .SetRule("color", Color.White)
            .SetRule("font", _resourceManager.LoadFont("Roboto-Regular"))
        )
        .AddStyle("panel", // Panel styles
          new Style()
            .SetRule("background-color", new Color(44, 142, 123))
        )
        .AddStyle("panel-header", // Panel header styles
          new Style()
            .SetRule("background-color", new Color(76, 194, 159))
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
        .AddStyle("inner-panel", // Inner panel styles
          new Style()
            .SetRule("background-color", new Color(9, 119, 116))
        )
        .SetChildRelation("panel", "canvas");
      
      _canvas = new Canvas(0, 0, _windowManager.GetWidth(), _windowManager.GetHeight(), _styles);

      InitializeExampleWindowRenderers();
    }
    
    public void Update(float dt)
    {
      if (_inputManager.IsKeyPressed("right"))
      {
        _currentWindowRendererId++;
        
        if (_currentWindowRendererId >= _windowRenderers.Length)
        {
          _currentWindowRendererId = 0;
        }

        _currentWindowRenderer = _windowRenderers[_currentWindowRendererId];
      }
      
      if (_inputManager.IsKeyPressed("left"))
      {
        _currentWindowRendererId--;
        
        if (_currentWindowRendererId < 0)
        {
          _currentWindowRendererId = _windowRenderers.Length - 1;
        }

        _currentWindowRenderer = _windowRenderers[_currentWindowRendererId];
      }
    }
    
    public void Draw()
    {
      _canvas.Draw(rootRederer =>
      {
        // Root panel with background
        rootRederer.FreeLayout(new Rectangle(0, 0, _windowManager.GetWidth(), _windowManager.GetHeight())
          , _currentWindowRenderer
          , _styles.GetStyle("canvas")
        );
      });
    }

    private void InitializeExampleWindowRenderers()
    {
      _windowRenderers = new Action<GuiRenderer>[]
      {
        Example1_TextAndImages,
        Example2_Buttons
      };
      
      _currentWindowRenderer = _windowRenderers[0];
    }
    
    #region Example 1

    private void Example1_TextAndImages(GuiRenderer renderer)
    {
      // Header
      renderer.Text(new Rectangle(0, 0, _windowManager.GetWidth(), 25)
        , "1. Text and Images"
        , Style.Merge(_styles.GetStyle("panel panel-header")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      
      // Text examples panel
      renderer.Panel(new Rectangle(50, 50, 230, 400), "Text", 
        panelRenderer =>
        {
          TextAlignment(panelRenderer);
          TextWrapping(panelRenderer);
        }
        , _styles.GetStyle("panel panel-header")
        , _styles.GetStyle("panel")
      );
            
      // Image examples panel
      renderer.Panel(new Rectangle(330, 50, 230, 400), "Images", 
        panelRenderer =>
        {
          ImageAlignment(panelRenderer);
          ImageSize(panelRenderer);
        }
        , _styles.GetStyle("panel panel-header")
        , _styles.GetStyle("panel")
      );
    }
    
    private void TextAlignment(GuiRenderer renderer)
    {
      renderer.Text(new Rectangle(5, 5, 220, 25), "Alignment:"
        , Style.Merge(_styles.GetStyle("panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      
      renderer.Text(new Rectangle(5, 30, 70, 70), "Text"
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Left)
            .SetRule("v-align", VerticalAlignment.Top)
        )
      );
      
      renderer.Text(new Rectangle(80, 30, 70, 70), "Text"
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Top)
        )
      );
      renderer.Text(new Rectangle(155, 30, 70, 70), "Text"
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Right)
            .SetRule("v-align", VerticalAlignment.Top)
        )
      );
      
      renderer.Text(new Rectangle(5, 110, 70, 70), "Text"
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Left)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      renderer.Text(new Rectangle(80, 110, 70, 70), "Text"
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      renderer.Text(new Rectangle(155, 110, 70, 70), "Text"
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Right)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      
      renderer.Text(new Rectangle(5, 185, 70, 70), "Text"
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Left)
            .SetRule("v-align", VerticalAlignment.Bottom)
        )
      );
      renderer.Text(new Rectangle(80, 185, 70, 70), "Text"
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Bottom)
        )
      );
      renderer.Text(new Rectangle(155, 185, 70, 70), "Text"
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Right)
            .SetRule("v-align", VerticalAlignment.Bottom)
        )
      );
    }
    
    private void TextWrapping(GuiRenderer renderer)
    {
      renderer.Text(new Rectangle(5, 260, 220, 25), "Wrapping:"
        , Style.Merge(_styles.GetStyle("panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      
      renderer.Text(new Rectangle(5, 285, 107, 107), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style().SetRule("text-wrap", false)
        )
      );
      renderer.Text(new Rectangle(117, 285, 107, 107), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua."
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style().SetRule("text-wrap", true)
        )
      );
    }
    
    private void ImageAlignment(GuiRenderer renderer)
    {
      renderer.Text(new Rectangle(5, 5, 220, 25), "Alignment:"
        , Style.Merge(_styles.GetStyle("panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      
      renderer.Image(_exampleImage
        ,new Rectangle(5, 30, 70, 70)
        , new Rectangle(0, 0, _exampleImage.Width, _exampleImage.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Left)
            .SetRule("v-align", VerticalAlignment.Top)
        )
      );
      
      renderer.Image(_exampleImage
        , new Rectangle(80, 30, 70, 70)
        , new Rectangle(0, 0, _exampleImage.Width, _exampleImage.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Top)
        )
      );
      renderer.Image(_exampleImage
        , new Rectangle(155, 30, 70, 70)
        , new Rectangle(0, 0, _exampleImage.Width, _exampleImage.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Right)
            .SetRule("v-align", VerticalAlignment.Top)
        )
      );
      
      renderer.Image(_exampleImage
        , new Rectangle(5, 110, 70, 70)
        , new Rectangle(0, 0, _exampleImage.Width, _exampleImage.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Left)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      renderer.Image(_exampleImage
        , new Rectangle(80, 110, 70, 70)
        , new Rectangle(0, 0, _exampleImage.Width, _exampleImage.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      renderer.Image(_exampleImage
        , new Rectangle(155, 110, 70, 70)
        , new Rectangle(0, 0, _exampleImage.Width, _exampleImage.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Right)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      
      renderer.Image(_exampleImage
        , new Rectangle(5, 185, 70, 70)
        , new Rectangle(0, 0, _exampleImage.Width, _exampleImage.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Left)
            .SetRule("v-align", VerticalAlignment.Bottom)
        )
      );
      renderer.Image(_exampleImage
        , new Rectangle(80, 185, 70, 70)
        , new Rectangle(0, 0, _exampleImage.Width, _exampleImage.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Bottom)
        )
      );
      renderer.Image(_exampleImage
        , new Rectangle(155, 185, 70, 70)
        , new Rectangle(0, 0, _exampleImage.Width, _exampleImage.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Right)
            .SetRule("v-align", VerticalAlignment.Bottom)
        )
      );
    }
    
    private void ImageSize(GuiRenderer renderer)
    {
      renderer.Text(new Rectangle(5, 260, 220, 25), "Image size mode:"
        , Style.Merge(_styles.GetStyle("panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      
      renderer.Image(_exampleImage2
        , new Rectangle(5, 285, 70, 70)
        , new Rectangle(0, 0, _exampleImage2.Width, _exampleImage2.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
            .SetRule("image-size-mode", ImageSizeMode.Stretch)
        )
      );
      renderer.Image(_exampleImage2
        , new Rectangle(80, 285, 70, 70)
        , new Rectangle(0, 0, _exampleImage2.Width, _exampleImage2.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
            .SetRule("image-size-mode", ImageSizeMode.Cover)
        )
      );
      renderer.Image(_exampleImage2
        , new Rectangle(155, 285, 70, 70)
        , new Rectangle(0, 0, _exampleImage2.Width, _exampleImage2.Height)
        , Style.Merge(_styles.GetStyle("panel inner-panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
            .SetRule("image-size-mode", ImageSizeMode.Contain)
        )
      );
      
      
      renderer.Text(new Rectangle(5, 285 + 70, 70, 25), "Stretch"
        , Style.Merge(_styles.GetStyle("panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      
      renderer.Text(new Rectangle(80, 285 + 70, 70, 25), "Cover"
        , Style.Merge(_styles.GetStyle("panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      
      renderer.Text(new Rectangle(155, 285 + 70, 70, 25), "Contain"
        , Style.Merge(_styles.GetStyle("panel")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
    }

    #endregion
    
    private void Example2_Buttons(GuiRenderer renderer)
    {
      // Header
      renderer.Text(new Rectangle(0, 0, _windowManager.GetWidth(), 25)
        , "2. Buttons"
        , Style.Merge(_styles.GetStyle("panel panel-header")
          , new Style()
            .SetRule("h-align", HorizontalAlignment.Center)
            .SetRule("v-align", VerticalAlignment.Middle)
        )
      );
      
      // Text examples panel
//      renderer.Panel(new Rectangle(50, 50, 230, 400), "Text", 
//        panelRenderer =>
//        {
//          TextAlignment(panelRenderer);
//          TextWrapping(panelRenderer);
//                
//        }
//        , _styles.GetStyle("panel panel-header")
//        , _styles.GetStyle("panel")
//      );
//            
//      // Image examples panel
//      renderer.Panel(new Rectangle(330, 50, 230, 400), "Images", 
//        panelRenderer =>
//        {
//          ImageAlignment(panelRenderer);
//          ImageSize(panelRenderer);
//        }
//        , _styles.GetStyle("panel panel-header")
//        , _styles.GetStyle("panel")
//      );
    }
  }
}