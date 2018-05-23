using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Models;
using OxyEngine.UI.Styles;

namespace OxyEngine.UI.Renderers
{
  public class UIRenderer : Renderer
  {
    private readonly TextRenderer _textRenderer;
    
    public UIRenderer(AreaStack areaStack, StyleDatabase styles) : base(areaStack, styles)
    {
      _textRenderer = new TextRenderer(areaStack, styles);
    }
    
    public void FreeLayout(Rectangle rectangle, Action<UIRenderer> action = null, string styleSelector = "")
    {
      var backColor = Styles.GetStyle(styleSelector).GetRule<Color>("background-color");
      var borderColor = Styles.GetStyle(styleSelector).GetRule<Color>("border-color");
      var borderWidth = Styles.GetStyle(styleSelector).GetRule<float>("border-width");
      
      AreaStack.Push(rectangle);
      rectangle = AreaStack.Peek();
      
      var beforeColor = GraphicsManager.GetColor();
      GraphicsManager.SetColor(backColor.R, backColor.G, backColor.B, backColor.A);
      GraphicsManager.Rectangle("fill", rectangle);

      var beforeWidth = GraphicsManager.GetLineWidth();
      GraphicsManager.SetColor(borderColor.R, borderColor.G, borderColor.B, borderColor.A);
      GraphicsManager.SetLineWidth(borderWidth);
      GraphicsManager.Rectangle("line", rectangle);
      GraphicsManager.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);
      GraphicsManager.SetLineWidth(beforeWidth);

      GraphicsManager.DrawCropped(AreaStack.Peek(), () => action?.Invoke(this));
      AreaStack.Pop();
    }
    
    public Rectangle Panel(Rectangle rect, string title = "Panel", 
      Action<UIRenderer> action = null, string panelStyleSelector = "", string headerStyleSelector = "")
    {
      var panelStyles = Styles.GetStyle(panelStyleSelector);
      
      Text(new Rectangle(rect.X, rect.Y, rect.Width, 25)
        , title
        , headerStyleSelector
      );
      FreeLayout(new Rectangle(rect.X, rect.Y + 25, rect.Width, rect.Height)
        , action
        , panelStyleSelector
      );

      return rect;
    }
    
    public void Text(Rectangle rect, string text, string styleSelector = "")
    {
      AreaStack.Push(rect);
      _textRenderer.Render(AreaStack.Peek(), text, styleSelector);
      AreaStack.Pop();
    }
  }
}