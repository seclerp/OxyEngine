using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
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

    public TextObject(FontObject font, string text = "")
    {
      _brush = new SolidBrush(Color.White);
      _font = font;
      _text = text;

      // Listener for font size change
      _font.OnFontSizeChanged += (s, e) => RedrawRenderer();

      RedrawRenderer();
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
      GL.PushMatrix();

      GL.Translate(x, y, 0);
      GL.Rotate(r, Vector3.UnitZ);
      GL.Scale(sx, sy, 1);

      GL.BindTexture(TextureTarget.Texture2D, _textRenderer.Texture);
      GL.Begin(PrimitiveType.Quads);

      GL.TexCoord2(1.0f, 1.0f);
      GL.Vertex3(_size.Width, _size.Height, 0.1);
      GL.TexCoord2(1.0f, 0.0f);
      GL.Vertex3(_size.Width, 0, 0.1);
      GL.TexCoord2(0.0f, 0.0f);
      GL.Vertex3(0, 0, 0.1);
      GL.TexCoord2(0.0f, 1.0f);
      GL.Vertex3(0, _size.Height, 0.1);

      GL.End();
      GL.BindTexture(TextureTarget.Texture2D, 0);

      GL.PopMatrix();
    }

    private static SizeF MeasureString(string s, Font font)
    {
      if (s.Length == 0)
        return new SizeF(1, 1);

      SizeF result;
      using (var image = new Bitmap(1, 1))
      {
        using (var g = Graphics.FromImage(image))
        {
          result = g.MeasureString(s, font);
        }
      }

      return result;
    }

    private void RedrawRenderer()
    {
      _textRenderer?.Dispose();
      _size = MeasureString(_text, _font.Font);
      _textRenderer = new TextRenderer((int) _size.Width, (int) _size.Height);
      _textRenderer.Clear(Color.Transparent);
      _textRenderer.DrawString(_text, _font.Font, _brush, new PointF(0, 0));
    }


    /// <summary>
    ///   Set font for text
    /// </summary>
    /// <param name="newFont">Font object</param>
    public void SetFont(FontObject newFont)
    {
      _font = newFont;
      RedrawRenderer();
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
      RedrawRenderer();
    }

    /// <summary>
    ///   Set new text for drawing
    /// </summary>
    /// <param name="newText">New text to draw</param>
    public void SetText(string newText)
    {
      _text = newText;
      RedrawRenderer();
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