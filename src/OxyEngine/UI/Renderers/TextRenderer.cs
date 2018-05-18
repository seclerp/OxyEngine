using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.UI.Enums;

namespace OxyEngine.UI.Renderers
{
  public class TextRenderer
  {
    private GraphicsManager _graphicsManager;
    
    public TextRenderer()
    {
      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
    }
    
    public void Render(Rectangle rect, SpriteFont font, string text, Color textColor, Color backColor,
      HorizontalAlignment hTextAlign = HorizontalAlignment.Left, VerticalAlignment vTextAlign = VerticalAlignment.Top)
    {
      var beforeColor = _graphicsManager.GetColor();
      _graphicsManager.SetColor(backColor.R, backColor.G, backColor.B, backColor.A);
      _graphicsManager.Rectangle("fill", rect.X, rect.Y, rect.Width, rect.Height);
      
      var beforeFont = _graphicsManager.GetFont();
      _graphicsManager.SetFont(font);
      _graphicsManager.SetColor(textColor.R, textColor.G, textColor.B, textColor.A);

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
      
      _graphicsManager.Print(text, rect.X + finalX, rect.Y + finalY);

      _graphicsManager.SetFont(beforeFont);
      _graphicsManager.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);
    }
  }
}