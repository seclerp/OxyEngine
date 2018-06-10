using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.GUI.Enums;

namespace OxyEngine.Ecs.Components.UI
{
  public class TextComponent : UIComponent
  {
    private static Dictionary<(SpriteFont, string, int), string> _wrapCache;
    
    public string Text { get; set; }
    public SpriteFont Font { get; set; }
    public HorizontalAlignment HorizontalAlignment { get; set; }
    public VerticalAlignment VerticalAlignment { get; set; }
    public Color Color { get; set; }
    public bool UseWrap { get; set; }
    
    static TextComponent()
    {
      _wrapCache = new Dictionary<(SpriteFont, string, int), string>();
    }
    
    public override void Draw()
    {
      base.Draw();
      
      var finalText = UseWrap
        ? WrapText(Font, Text, Rectangle.Width) 
        : Text;
      
      var textSize = Font.MeasureString(finalText);
      var finalX = 0f;
      var finalY = 0f;
      
      switch (HorizontalAlignment)
      {
        case HorizontalAlignment.Left:
          finalX = 0;
          break;
        case HorizontalAlignment.Center:
        case HorizontalAlignment.Stretch:
          finalX = (Rectangle.Size.X - textSize.X) / 2;
          break;
        case HorizontalAlignment.Right:
          finalX = Rectangle.Size.X - textSize.X;
          break;
      }
      
      switch (VerticalAlignment)
      {
        case VerticalAlignment.Top:
          finalY = 0;
          break;
        case VerticalAlignment.Middle:
        case VerticalAlignment.Stretch:
          finalY = (Rectangle.Size.Y - textSize.Y) / 2;
          break;
        case VerticalAlignment.Bottom:
          finalY = Rectangle.Size.Y - textSize.Y;
          break;
      }
      
      var beforeFont = GraphicsManager.GetFont();
      GraphicsManager.SetFont(Font);
      var beforeColor = GraphicsManager.GetColor();
      GraphicsManager.SetColor(Color.R, Color.G, Color.B, Color.A);
      
      GraphicsManager.Print(finalText, Rectangle.X + finalX, Rectangle.Y + finalY);
      
      GraphicsManager.SetFont(beforeFont);
      GraphicsManager.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);
    }

    private string WrapText(SpriteFont font, string text, int maxLineWidth)
    {
      var cacheKey = (font, text, maxLineWidth);
      if (_wrapCache.ContainsKey(cacheKey))
      {
        return _wrapCache[cacheKey];
      }
      
      if (font.MeasureString(text).X < maxLineWidth)
      {
        return text;
      }

      var words = text.Split(' ');
      var wrappedText = new StringBuilder();
      var linewidth = 0f;
      var spaceWidth = font.MeasureString(" ").X;
      foreach (var word in words)
      {
        var size = font.MeasureString(word);
        if (linewidth + size.X < maxLineWidth)
        {
          linewidth += size.X + spaceWidth;
        }
        else
        {
          wrappedText.Append("\n");
          linewidth = size.X + spaceWidth;
        }

        wrappedText.Append(word);
        wrappedText.Append(" ");
      }

      _wrapCache[cacheKey] = wrappedText.ToString();
      
      return _wrapCache[cacheKey];
    }
  }
}