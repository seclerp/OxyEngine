using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.GUI.Enums;

namespace OxyEngine.GUI.Models
{
  public struct TextModel
  {
    public Color BackgroundColor;
    public Color TextColor;
    public SpriteFont Font;
    public string Text;
    public Rectangle Rect;
    public VerticalAlignment VerticalAlignment;
    public HorizontalAlignment HorizontalAlignment;
  }
}