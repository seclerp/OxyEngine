using System;
using System.Collections.Generic;
using OpenTK;

namespace Oxy.Framework.Objects
{
  public class BitmapFontObject : IDisposable
  {
    private TextureObject _texture;
    private string _characters;
    private Dictionary<char, RectObject> _glyphs;
    private bool _disposeTexture;
    
    internal BitmapFontObject(TextureObject texture, string characters, bool disposeTexture = false)
    {
      _texture = texture;
      _characters = characters;
      // To dispose only temporary textures
      _disposeTexture = disposeTexture;
    }

    private float ToNormalized(float x)
    {
      return x / _texture.Width;
    }
    
    private void GenerateGlyphs()
    {
      var glyphWidth = (float) _texture.Width / _characters.Length;
      
      for (var i = 0; i < _characters.Length; i++)
      {
        // Only one row supported at that time
        _glyphs[_characters[i]] = new RectObject(ToNormalized(i * glyphWidth), 0, ToNormalized(i * glyphWidth + glyphWidth), 1);
      }
    }
    
    public void Dispose()
    {
      if (_disposeTexture)
        _texture.Dispose();
    }
  }
}