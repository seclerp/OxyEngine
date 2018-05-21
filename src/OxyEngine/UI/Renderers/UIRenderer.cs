using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Models;

namespace OxyEngine.UI.Renderers
{
  public class UIRenderer : Renderer
  {
    private LayoutGraphicsRenderer _horizontalLayoutRenderer;
    private LayoutGraphicsRenderer _verticalLayoutRenderer;
    private TextRenderer _textRenderer;
    
    public UIRenderer(AreaStack areaStack) : base(areaStack)
    {
      _horizontalLayoutRenderer = new LayoutGraphicsRenderer(areaStack);
      _verticalLayoutRenderer = new LayoutGraphicsRenderer(areaStack);
      _textRenderer = new TextRenderer(areaStack);
    }
    
    public void HorizontalLayout(Action<LayoutGraphicsRenderer> action)
    {
      
    }
    
    public void VerticalLayout(Action<LayoutGraphicsRenderer> action)
    {
      
    }
    
    public void FreeLayout(Rectangle rectangle, Action<UIRenderer> action,
      Color? backColor = null, Color? borderColor = null, float? borderWidth = null)
    {
      backColor = backColor ?? Color.Transparent;
      borderColor = borderColor ?? Color.White;
      borderWidth = borderWidth ?? 0;
      
      AreaStack.Push(rectangle);
      rectangle = AreaStack.Peek();
      
      var beforeColor = GraphicsManager.GetColor();
      GraphicsManager.SetColor(backColor.Value.R, backColor.Value.G, backColor.Value.B, backColor.Value.A);
      GraphicsManager.Rectangle("fill", rectangle);

      var beforeWidth = GraphicsManager.GetLineWidth();
      GraphicsManager.SetColor(borderColor.Value.R, borderColor.Value.G, borderColor.Value.B, borderColor.Value.A);
      GraphicsManager.SetLineWidth(borderWidth.Value);
      GraphicsManager.Rectangle("line", rectangle);
      GraphicsManager.SetColor(beforeColor.R, beforeColor.G, beforeColor.B, beforeColor.A);
      GraphicsManager.SetLineWidth(beforeWidth);

      GraphicsManager.DrawCropped(AreaStack.Peek(), () => action(this));
      AreaStack.Pop();
    }
    
    public void Text(Rectangle rect, SpriteFont font, string text, Color? textColor = null, Color? backColor = null, 
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