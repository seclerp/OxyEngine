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
    
    public FreeGraphicsRenderer(AreaStack areaStack) : base(areaStack)
    {
      _textRenderer = new TextRenderer(areaStack);
    }
    
    public void Text(Rectangle rect, SpriteFont font, string text, Color textColor, Color backColor, 
      HorizontalAlignment hTextAlign = HorizontalAlignment.Left, VerticalAlignment vTextAlign = VerticalAlignment.Top)
    {
      AreaStack.Push(rect);
      _textRenderer.Render(AreaStack.Peek(), font, text, textColor, backColor, hTextAlign, vTextAlign);
      AreaStack.Pop();
    }
    
    public void Text(TextModel model)
    {
      Text(model.Rect, model.Font, model.Text, model.TextColor, model.BackgroundColor, model.HorizontalAlignment, model.VerticalAlignment);
    }
  }
}