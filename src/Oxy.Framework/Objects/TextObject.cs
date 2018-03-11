using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Oxy.Framework.Interfaces;
using Oxy.Framework.Rendering;
using Bitmap = System.Drawing.Bitmap;
using Color = System.Drawing.Color;
using PointF = System.Drawing.PointF;
using SizeF = System.Drawing.SizeF;

namespace Oxy.Framework.Objects
{
  /// <summary>
  ///   Object for drawing text on screen
  /// </summary>
  public class TextObject : IDrawable, IDisposable
  {
    private SolidBrush _brush;
    private FontObject _font;
    private SizeF _size;
    private string _text;
    private TextRenderer _textRenderer;

    internal TextObject(FontObject font, string text = "")
    {
      _brush = new SolidBrush(Color.White);
      _font = font;
      _text = text;
      _size = _font.MeasureSize(text);
    }

    public void Dispose()
    {
      _textRenderer?.Dispose();
    }

    /// <summary>
    ///   Draws this TextObject on screen with given position, rotation and scale
    /// </summary>
    /// <param name="sourceRect"></param>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="ox"></param>
    /// <param name="oy"></param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public void Draw(RectObject sourceRect, float x, float y, float ox, float oy, float r, float sx, float sy)
    {
      _font.Print(_brush, _size, _text, x, y, r, sx, sy);
    }

    /// <summary>
    ///   Set font for text
    /// </summary>
    /// <param name="newTtfFont">Font object</param>
    public void SetFont(FontObject newTtfFont)
    {
      _font = newTtfFont;
      _size = _font.MeasureSize(_text);
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
      _brush = new SolidBrush(Color.FromArgb(a, r, g, b));
    }

    /// <summary>
    ///   Set new text for drawing
    /// </summary>
    /// <param name="newText">New text to draw</param>
    public void SetText(string newText)
    {
      _text = newText;
      _size = _font.MeasureSize(_text);
    }

    /// <summary>
    ///   Returns font used to draw this text
    /// </summary>
    /// <returns></returns>
    public FontObject GetFont()
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
        _brush.Color.R,
        _brush.Color.G,
        _brush.Color.B,
        _brush.Color.A
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