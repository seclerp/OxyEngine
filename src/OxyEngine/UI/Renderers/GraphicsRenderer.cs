using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Models;

namespace OxyEngine.UI.Renderers
{
  public class GraphicsRenderer
  {
    private GraphicsManager _graphicsManager;
    
    public GraphicsRenderer()
    {
      _graphicsManager = Container.Instance.ResolveByName<GraphicsManager>(InstanceName.GraphicsManager);
    }
    
    public void Text(SpriteFont font, string text, Color textColor, Color backColor, 
      HorizontalAlignment hTextAlign = HorizontalAlignment.Left, VerticalAlignment vTextAlign = VerticalAlignment.Top)
    {
      var beforeColor = _graphicsManager.GetColor();
      _graphicsManager.SetColor(_widget.BackgroundColor.R, _widget.BackgroundColor.G, _widget.BackgroundColor.B, _widget.BackgroundColor.A);
      _graphicsManager.Rectangle("fill", (int) _widget.X, (int) _widget.Y, (int) _widget.Width, (int) _widget.Height);
      
      var beforeFont = _graphicsManager.GetFont();
      _graphicsManager.SetFont(_widget.Font);
      _graphicsManager.SetColor(_widget.ForegroundColor.R, _widget.ForegroundColor.G, _widget.ForegroundColor.B, _widget.ForegroundColor.A);

      var textSize = _widget.Font.MeasureString(_widget.Value);
      var finalX = 0f;
      var finalY = 0f;
      
      switch (_widget.HTextAlign)
      {
        case HorizontalAlignment.Left:
          finalX = _widget.PaddingLeft;
          break;
        case HorizontalAlignment.Center:
          finalX = (_widget.Size.X - _widget.PaddingLeft - _widget.PaddingRight - textSize.X) / 2;
          break;
        case HorizontalAlignment.Right:
          finalX = _widget.Size.X - _widget.PaddingRight - textSize.X;
          break;
      }
      
      switch (_widget.VTextAlign)
      {
        case VerticalAlignment.Top:
          finalY = _widget.PaddingTop;
          break;
        case VerticalAlignment.Middle:
          finalY = (_widget.Size.Y - _widget.PaddingTop - _widget.PaddingBottom - textSize.Y) / 2;
          break;
        case VerticalAlignment.Bottom:
          finalY = _widget.Size.Y - _widget.PaddingBottom - textSize.Y;
          break;
      }
      
      GraphicsApi.Print(_widget.Value, finalX, finalY);

      GraphicsApi.SetFont(beforeFont);
      GraphicsApi.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);
    }
    
    public void Text(TextModel model)
    {
      Text(model.Font, model.Text, model.TextColor, model.TextColor, model.HorizontalAlignment, model.VerticalAlignment);
    }
  }
}