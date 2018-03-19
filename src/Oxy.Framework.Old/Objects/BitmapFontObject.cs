using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Oxy.Framework.Objects
{
  public class BitmapFontObject : IFontObject, IDisposable
  {
    private TextureObject _charSheet;
    private string _characters;
    private Dictionary<char, RectObject> _glyphs;
    private bool _disposeTexture;
    private SizeF _charSize;

    internal BitmapFontObject(TextureObject charSheet, string characters, bool disposeTexture = false)
    {
      _charSheet = charSheet;
      _characters = characters;
      // To dispose only temporary textures
      _disposeTexture = disposeTexture;

      GenerateGlyphs();
    }

    private float ToNormalized(float x)
    {
      return x / _charSheet.Width;
    }

    private void GenerateGlyphs()
    {
      _glyphs = new Dictionary<char, RectObject>();
      _charSize = new SizeF((float)_charSheet.Width / _characters.Length, _charSheet.Height);

      for (var i = 0; i < _characters.Length; i++)
      {
        // Only one row supported at that time
        _glyphs[_characters[i]] = new RectObject(ToNormalized(i * _charSize.Width), 0, ToNormalized(_charSize.Width), 1);
      }
    }

    public void Dispose()
    {
      if (_disposeTexture)
        _charSheet.Dispose();
    }

    public TextureObject Render(string text, Color color)
    {
      var size = MeasureText(text);
      var texture = Graphics.NewTexture(size.Width, size.Height);


      Graphics.SetRenderTexture(texture);


      GL.Color4(color);

      GL.BindTexture(TextureTarget.Texture2D, _charSheet.Id);
      GL.Begin(PrimitiveType.Quads);

      float i = 0, j = 0;
      foreach (var ch in text)
      {
        if (ch != '\n')
        {
          RectObject rect;
          if (!_glyphs.TryGetValue(ch, out rect))
            throw new Exception($"Not supported character: '{ch}'");

          GL.TexCoord2(rect.X, rect.Y); GL.Vertex3(i, j, 0);
          GL.TexCoord2(rect.X, rect.Y + rect.Height); GL.Vertex3(i, j + _charSize.Height, 0);
          GL.TexCoord2(rect.X + rect.Width, rect.Y + rect.Height); GL.Vertex3(i + _charSize.Width, j + _charSize.Height, 0);
          GL.TexCoord2(rect.X + rect.Width, rect.Y); GL.Vertex3(i + _charSize.Width, j, 0);

          i += _charSize.Width;
        }
        else
        {
          j += _charSize.Height;
          i = 0;
        }
      }

      GL.End();
      GL.BindTexture(TextureTarget.Texture2D, 0);


      Graphics.SetRenderTexture();

      return texture;
    }

    private Size MeasureText(string text)
    {
      int sizeX = 0;
      int sizeY = (text.Count(x => x == '\n') + 1) * (int)_charSize.Height;

      foreach (var s in text.Split('\n'))
      {
        sizeX = Math.Max(sizeX, s.Length * (int)_charSize.Width);
      }

      return new Size(sizeX, sizeY);
    }
  }
}