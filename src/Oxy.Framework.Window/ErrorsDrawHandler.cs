using System;
using System.Reflection;
using Oxy.Framework.Objects;

namespace Oxy.Framework
{
  public class ErrorsDrawHandler
  {
    private TextureObject _errorsBanner;
    private FontObject _font;
    private TextObject _text;
    public string FullError { get; private set; }

    public void LoadResources()
    {
      var assembly = typeof(Resources).GetTypeInfo().Assembly;

      // assembly.GetManifestResourceNames() - to check
      _errorsBanner =
        Resources.LoadTexture(assembly.GetManifestResourceStream("Oxy.Framework.resources.img.error.png"));
      _font = Resources.LoadFont(assembly.GetManifestResourceStream("Oxy.Framework.resources.font.roboto.ttf"));
    }

    public void Fire(Exception exception)
    {
      FullError = $"{exception.Message}\n\n{exception.StackTrace}";

      _text = Graphics.NewText(_font, FullError);
      Graphics.SetBackgroundColor(100, 100, 100);
    }

    public void DrawErrors()
    {
      Graphics.Draw(_errorsBanner, 70, 70);
      Graphics.Draw(_text, 70, 130);
    }
  }
}