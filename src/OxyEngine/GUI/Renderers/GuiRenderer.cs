using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.GUI.Styles;

namespace OxyEngine.GUI.Renderers
{
  public class GuiRenderer : Renderer
  {
    private readonly TextRenderer _textRenderer;
    private readonly ImageRenderer _imageRenderer;
    
    public GuiRenderer(AreaStack areaStack, StyleDatabase styles) : base(areaStack, styles)
    {
      _textRenderer = new TextRenderer(areaStack, styles);
      _imageRenderer = new ImageRenderer(areaStack, styles);
    }
    
    public void FreeLayout(Rectangle rectangle, Action<GuiRenderer> action = null, Style style = null)
    {
      style = style ?? GetDefaultStyle();
      
      var backColor = style.GetRule<Color>("background-color");
      var borderColor = style.GetRule<Color>("border-color");
      var borderWidth = style.GetRule<float>("border-width");
      
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
      Action<GuiRenderer> action = null, Style headerStyle = null,  Style panelStyle = null)
    {
      Text(new Rectangle(rect.X, rect.Y, rect.Width, 25)
        , title
        , headerStyle
      );
      FreeLayout(new Rectangle(rect.X, rect.Y + 25, rect.Width, rect.Height)
        , action
        , panelStyle
      );

      return rect;
    }
    
    public void Text(Rectangle rect, string text, Style style = null)
    {
      AreaStack.Push(rect);
      _textRenderer.Render(AreaStack.Peek(), text, style);
      AreaStack.Pop();
    }
    
    public void Image(Texture2D texture, Rectangle rect, Rectangle sourceRect, Style style = null)
    {
      AreaStack.Push(rect);
      _imageRenderer.Render(texture, AreaStack.Peek(), sourceRect, style);
      AreaStack.Pop();
    }
  }
}