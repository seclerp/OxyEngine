using System.Drawing;
using System.Net.Mime;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Bitmap = System.Drawing.Bitmap;
using Color = System.Drawing.Color;
using PointF = System.Drawing.PointF;
using SizeF = System.Drawing.SizeF;

namespace Oxy.Framework
{
  public class TextObject : IDrawable
  {
    private SolidBrush _brush;
    private Font _font;
    private TextRenderer _textRenderer;
    private string _text;
    private SizeF _size;

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

    private void RedrawRenderer()
    {
      _textRenderer?.Dispose();
      _size = MeasureString(_text, _font);
      _textRenderer = new TextRenderer((int)_size.Width, (int)_size.Height);
      _textRenderer.Clear(Color.Transparent);
      _textRenderer.DrawString(_text, _font, _brush, new PointF(0, 0));
    }
    
    public TextObject(Font font, string text = "")
    {
      _brush = new SolidBrush(Color.White);
      _font = font;
      _text = text;
      RedrawRenderer();
    }
    
    public void SetFont(Font newFont)
    {
      _font = newFont;
      RedrawRenderer();
    }

    public void SetColor(byte r, byte g, byte b, byte a)
    {
      _brush = new SolidBrush(Color.FromArgb(a, r, g, b));
      RedrawRenderer();
    }
    
    public void SetText(string newText)
    {
      _text = newText;
      RedrawRenderer();
    }

    public Font GetFont() =>
      _font;

    public (byte, byte, byte, byte) GetColor() => 
    (
      _brush.Color.R,
      _brush.Color.G,
      _brush.Color.B,
      _brush.Color.A
    );

    public string GetText() => 
      _text;
    
    public void Draw(float x = 0, float y = 0, float r = 0, float sx = 1, float sy = 1)
    {
      GL.PushMatrix();
      
      GL.Translate(x, y, 0);
      GL.Rotate(r, Vector3.UnitZ);
      GL.Scale(sx, sy, 1);
      
      GL.BindTexture(TextureTarget.Texture2D, _textRenderer.Texture);
      GL.Begin(PrimitiveType.Quads);

      GL.TexCoord2(1.0f, 1.0f); GL.Vertex3(_size.Width, _size.Height, 0.1);
      GL.TexCoord2(1.0f, 0.0f); GL.Vertex3(_size.Width, 0, 0.1);
      GL.TexCoord2(0.0f, 0.0f); GL.Vertex3(0, 0, 0.1);
      GL.TexCoord2(0.0f, 1.0f); GL.Vertex3(0, _size.Height, 0.1);
            
      GL.End();
      GL.BindTexture(TextureTarget.Texture2D, 0);
      
      GL.PopMatrix();
    }
  }
}