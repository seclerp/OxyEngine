using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Models;

namespace OxyEngine.UI.Renderers
{
  public class FreeGraphicsRenderer : Renderer
  {
    private TextRenderer _textRenderer;
    
    public FreeGraphicsRenderer(AreaWrapper areaWrapper)
    {
      _textRenderer = new TextRenderer();
    }
    
    public void Text(Rectangle rect, SpriteFont font, string text, Color textColor, Color backColor, 
      HorizontalAlignment hTextAlign = HorizontalAlignment.Left, VerticalAlignment vTextAlign = VerticalAlignment.Top)
    {
      _textRenderer.Render(rect, font, text, textColor, backColor, hTextAlign, vTextAlign);
    }
    
    public void Text(TextModel model)
    {
      Text(model.Rect, model.Font, model.Text, model.TextColor, model.BackgroundColor, model.HorizontalAlignment, model.VerticalAlignment);
    }
  }
}