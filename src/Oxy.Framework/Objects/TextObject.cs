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
    private TtfFontObject _ttfFont;
    private SizeF _size;
    private string _text;
    private TextRenderer _textRenderer;

    public TextObject(TtfFontObject ttfFont, string text = "")
    {
      _brush = new SolidBrush(Color.White);
      _ttfFont = ttfFont;
      _text = text;
      _size = MeasureString(text, _ttfFont.Font);
    }

    public void Dispose()
    {
      _textRenderer?.Dispose();
    }

    /// <summary>
    ///   Draws this TextObject on screen with given position, rotation and scale
    /// </summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public void Draw(float x = 0, float y = 0, float r = 0, float sx = 1, float sy = 1)
    {
      _ttfFont.Print(_brush, _size, _text, x, y, r, sx, sy);
    }

    private static SizeF MeasureString(string s, Font font)
    {
      if (s.Length == 0)
        return new SizeF(1, 1);

      SizeF result;
      using (var image = new Bitmap(1, 1))
      {
        using (var g = System.Drawing.Graphics.FromImage(image))
        {
          result = g.MeasureString(s, font);
        }
      }

      return result;
    }

    /// <summary>
    ///   Set font for text
    /// </summary>
    /// <param name="newTtfFont">Font object</param>
    public void SetFont(TtfFontObject newTtfFont)
    {
      _ttfFont = newTtfFont;
      _size = MeasureString(_text, _ttfFont.Font);
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
      _size = MeasureString(_text, _ttfFont.Font);
    }

    /// <summary>
    ///   Returns font used to draw this text
    /// </summary>
    /// <returns></returns>
    public TtfFontObject GetFont()
    {
      return _ttfFont;
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