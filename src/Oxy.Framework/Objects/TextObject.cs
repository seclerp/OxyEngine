using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Oxy.Framework.Interfaces;
using Color = System.Drawing.Color;

namespace Oxy.Framework.Objects
{
  /// <summary>
  ///   Object for drawing text on screen
  /// </summary>
  public class TextObject : IDrawable, IDisposable
  {
    private TextureObject _texture;
    private Color _color;
    private IFontObject _font;
    private string _text;

    internal TextObject(IFontObject font, string text = "")
    {
      _color = Color.White;
      _font = font;
      _text = text;
      Redraw();
    }

    private void Redraw()
    {
      _texture?.Dispose();
      _texture = null;
      _texture = _font.Render(_text, _color);
    }

    public void Dispose()
    {
      _texture?.Dispose();
    }

    /// <summary>
    ///   Draws this TextObject on screen with given position, rotation and scale
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="ox"></param>
    /// <param name="oy"></param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public void Draw(float x, float y, float ox, float oy, float r, float sx, float sy)
    {
      _texture.Draw(x, y, ox, oy, r, sx, sy);
    }

    /// <summary>
    ///   Set font for text
    /// </summary>
    /// <param name="newTtfFont">Font object</param>
    public void SetFont(IFontObject newTtfFont)
    {
      _font = newTtfFont;
    }

    /// <summary>
    ///   Set color of the text
    /// </summary>
    /// <param name="r">Red color component</param>
    /// <param name="g">Green color component</param>
    /// <param name="b">Blue color component</param>
    /// <param name="a">Alpha color component</param>
    public void SetColor(byte r, byte g, byte b, byte a)
    {
      _color = Color.FromArgb(a, r, g, b);
    }

    /// <summary>
    ///   Set new text for drawing
    /// </summary>
    /// <param name="newText">New text to draw</param>
    public void SetText(string newText)
    {
      _text = newText;
      Redraw();
    }

    /// <summary>
    ///   Returns font used to draw this text
    /// </summary>
    /// <returns></returns>
    public IFontObject GetFont()
    {
      return _font;
    }

    /// <summary>
    ///   Returns color used to draw this text
    /// </summary>
    /// <returns>Tuple with 4 items - R, G, B, A color components</returns>
    public (byte, byte, byte, byte) GetColor()
    {
      return (
        _color.R,
        _color.G,
        _color.B,
        _color.A
        );
    }

    /// <summary>
    ///   Returns current text
    /// </summary>
    /// <returns>Current text</returns>
    public string GetText()
    {
      return _text;
    }
  }
}