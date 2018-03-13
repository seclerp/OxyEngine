using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Oxy.Framework.Interfaces;
using Oxy.Framework.Rendering;
using QuickFont;
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
    private QFontDrawing _drawing;
    private Color _color;
    private FontObject _font;
    private string _text;
    private TextRenderer _textRenderer;

    internal TextObject(FontObject font, string text = "")
    {
      _color = Color.White;
      _font = font;
      _text = text;
    }

    private void RedrawDrawing()
    {
      _drawing.DrawingPrimitives.Clear();
      _font.Print(_drawing, _color, _text);

      _drawing.RefreshBuffers();
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
    /// <param name="ox"></param>
    /// <param name="oy"></param>
    /// <param name="r">Rotation</param>
    /// <param name="sx">X scale factor</param>
    /// <param name="sy">Y scale factor</param>
    public void Draw(float x, float y, float ox, float oy, float r, float sx, float sy)
    {
      GL.PushMatrix();

      GL.Translate(x, y, 0);
      GL.Rotate(r, Vector3.UnitZ);
      GL.Scale(sx, sy, 1);

      _drawing.Draw();

      GL.PopMatrix();
    }

    /// <summary>
    ///   Set font for text
    /// </summary>
    /// <param name="newTtfFont">Font object</param>
    public void SetFont(FontObject newTtfFont)
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
      RedrawDrawing();
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