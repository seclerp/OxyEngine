using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.UI.Enums;

namespace OxyEngine.UI.Renderers
{
  public class TextRenderer : Renderer
  {
    public TextRenderer(AreaStack areaStack) : base(areaStack)
    {
    }
    
    public void Render(Rectangle rect, SpriteFont font, string text, Color textColor, Color backColor,
      HorizontalAlignment hTextAlign = HorizontalAlignment.Left, VerticalAlignment vTextAlign = VerticalAlignment.Top)
    {
      var beforeColor = GraphicsManager.GetColor();
      GraphicsManager.SetColor(backColor.R, backColor.G, backColor.B, backColor.A);
      GraphicsManager.Rectangle("fill", rect.X, rect.Y, rect.Width, rect.Height);
      
      var beforeFont = GraphicsManager.GetFont();
      GraphicsManager.SetFont(font);
      GraphicsManager.SetColor(textColor.R, textColor.G, textColor.B, textColor.A);

      var textSize = font.MeasureString(text);
      var finalX = 0f;
      var finalY = 0f;
      
      switch (hTextAlign)
      {
        case HorizontalAlignment.Left:
          finalX = 0;
          break;
        case HorizontalAlignment.Center:
        case HorizontalAlignment.FullWidth:
          finalX = (rect.Size.X - textSize.X) / 2;
          break;
        case HorizontalAlignment.Right:
          finalX = rect.Size.X - textSize.X;
          break;
      }
      
      switch (vTextAlign)
      {
        case VerticalAlignment.Top:
          finalY = 0;
          break;
        case VerticalAlignment.Middle:
        case VerticalAlignment.FullHeight:
          finalY = (rect.Size.Y - textSize.Y) / 2;
          break;
        case VerticalAlignment.Bottom:
          finalY = rect.Size.Y - textSize.Y;
          break;
      }
      
      GraphicsManager.DrawCropped(rect, () =>
      {
        GraphicsManager.Print(text, rect.X + finalX, rect.Y + finalY);
      });
      
      GraphicsManager.SetFont(beforeFont);
      GraphicsManager.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);
    }
  }
}