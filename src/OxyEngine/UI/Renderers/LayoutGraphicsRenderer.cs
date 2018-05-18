using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Models;

namespace OxyEngine.UI.Renderers
{
  public class LayoutGraphicsRenderer : RootRenderer
  {
    private GraphicsManager _graphicsManager;
    private AreaWrapper _areaWrapper;
    private Point _pen;
    
    public LayoutGraphicsRenderer(AreaWrapper areaWrapper) : base(areaWrapper)
    {
      _areaWrapper = areaWrapper;
      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);

      Reset();
    }

    public void Reset()
    {
      _pen = Point.Zero;
    }
    
    public void Text(SpriteFont font, string text, Color textColor, Color backColor, 
      HorizontalAlignment hTextAlign = HorizontalAlignment.Left, VerticalAlignment vTextAlign = VerticalAlignment.Top)
    {
      var beforeColor = _graphicsManager.GetColor();
      _graphicsManager.SetColor(backColor.R, backColor.G, backColor.B, backColor.A);
      _graphicsManager.Rectangle("fill", _areaWrapper.Area.X + _pen.X, _areaWrapper.Area.Y + _pen.Y, rect.Width, rect.Height);
      
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
    
    public void Text(TextModel model)
    {
      Text(model.Rect, model.Font, model.Text, model.TextColor, model.BackgroundColor, model.HorizontalAlignment, model.VerticalAlignment);
    }
  }
}