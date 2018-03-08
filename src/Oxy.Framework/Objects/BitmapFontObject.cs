using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Oxy.Framework.Objects
{
  public class BitmapFontObject : FontObject, IDisposable
  {
    private TextureObject _texture;
    private string _characters;
    private Dictionary<char, RectObject> _glyphs;
    private bool _disposeTexture;
    private SizeF _charSize;
    
    internal BitmapFontObject(TextureObject texture, string characters, bool disposeTexture = false)
    {
      _texture = texture;
      _characters = characters;
      // To dispose only temporary textures
      _disposeTexture = disposeTexture;

      GenerateGlyphs();
    }

    private float ToNormalized(float x)
    {
      return x / _texture.Width;
    }
    
    private void GenerateGlyphs()
    {
      _glyphs = new Dictionary<char, RectObject>();
      _charSize = new SizeF((float) _texture.Width / _characters.Length, _texture.Height);
      
      for (var i = 0; i < _characters.Length; i++)
      {
        // Only one row supported at that time
        _glyphs[_characters[i]] = new RectObject(ToNormalized(i * _charSize.Width), 0, ToNormalized(_charSize.Width), 1);
      }
    }
    
    public void Dispose()
    {
      if (_disposeTexture)
        _texture.Dispose();
    }

    internal override void Print(SolidBrush Brush, SizeF size, string text, float x = 0, float y = 0, float r = 0, float sx = 1,
      float sy = 1)
    {
      GL.Color4(Brush.Color);

      GL.Translate(x, y, 0);
      GL.Rotate(r, Vector3.UnitZ);
      GL.Scale(sx, sy, 1);

      GL.BindTexture(TextureTarget.Texture2D, _texture.Id);
      GL.Begin(PrimitiveType.Quads);

      float i = 0, j = 0;
      foreach (var ch in text)
      {
        RectObject rect;
        if (!_glyphs.TryGetValue(ch, out rect))
          throw new Exception($"Not supported character: '{ch}'");
        
        GL.TexCoord2(rect.X, rect.Y);                             GL.Vertex3(i, j, 0);
        GL.TexCoord2(rect.X, rect.Y + rect.Height);               GL.Vertex3(i, j + _charSize.Height, 0);
        GL.TexCoord2(rect.X + rect.Width, rect.Y + rect.Height);  GL.Vertex3(i + _charSize.Width, j + _charSize.Height, 0);
        GL.TexCoord2(rect.X + rect.Width, rect.Y);                GL.Vertex3(i + _charSize.Width, j, 0);

        if (ch != '\n')
          i += _charSize.Width;
        else
          j += _charSize.Height;
      }
      
      GL.End();
      GL.BindTexture(TextureTarget.Texture2D, 0);
    }

    // Size isn't used by Print, so return something
    internal override SizeF MeasureSize(string text)
    {
      return new SizeF(0, 0);
    }
  }
}