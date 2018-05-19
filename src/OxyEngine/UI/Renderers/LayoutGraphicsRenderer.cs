using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Graphics;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Models;

namespace OxyEngine.UI.Renderers
{
  public class LayoutGraphicsRenderer : Renderer
  {
    // Current state info
    private Point _pen;
    private Color _background;

    private TextRenderer _textRenderer;
    
    public LayoutGraphicsRenderer(AreaStack areaStack) : base(areaStack)
    {
      _textRenderer = new TextRenderer(areaStack);
    }

    public void Reset(Color background)
    {
      _pen = Point.Zero;
      _background = background;

      var beforeColor = GraphicsManager.GetColor();
      //GraphicsManager.Rectangle("fill", _areaWrapper.Area);
    }
    
    public void Text(SpriteFont font, string text, Color textColor, Color backColor, 
      HorizontalAlignment hTextAlign = HorizontalAlignment.Left, VerticalAlignment vTextAlign = VerticalAlignment.Top)
    {
      var strSize = font.MeasureString(text);
      var rect = new Rectangle(_pen, new Point((int)strSize.X, (int)strSize.Y));
      
      _textRenderer.Render(rect, font, text, textColor, backColor, hTextAlign, vTextAlign);
      _pen += new Point((int)strSize.X, 0);
    }
    
    public void Text(TextModel model)
    {
      Text(model.Font, model.Text, model.TextColor, model.BackgroundColor, model.HorizontalAlignment, model.VerticalAlignment);
    }
  }
}