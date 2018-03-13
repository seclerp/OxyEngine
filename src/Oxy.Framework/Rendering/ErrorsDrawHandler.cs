using System;
using System.Reflection;
using System.Text;
using Oxy.Framework.Exceptions;
using Oxy.Framework.Objects;

namespace Oxy.Framework.Rendering
{
  public class ErrorsDrawHandler
  {
    private TextureObject _errorsBanner;
    private TtfFontObject _ttfFont;
    private TextObject _text;
    public string FullError { get; private set; }

    public void LoadResources()
    {
    }

    private static string WordWrap(string text, int width)
    {
      int pos, next;
      var sb = new StringBuilder();

      // Lucidity check
      if (width < 1)
        return text;

      // Parse each line of text
      for (pos = 0; pos < text.Length; pos = next)
      {
        // Find end of line
        int eol = text.IndexOf(Environment.NewLine, pos);
        if (eol == -1)
          next = eol = text.Length;
        else
          next = eol + Environment.NewLine.Length;

        // Copy this line of text, breaking into smaller lines as needed
        if (eol > pos)
          do
          {
            int len = eol - pos;
            if (len > width)
              len = BreakLine(text, pos, width);
            sb.Append(text, pos, len);
            sb.Append(Environment.NewLine);

            // Trim whitespace following break
            pos += len;
            while (pos < eol && char.IsWhiteSpace(text[pos]))
              pos++;
          } while (eol > pos);
        else sb.Append(Environment.NewLine); // Empty line
      }

      return sb.ToString();
    }

    private static int BreakLine(string text, int pos, int max)
    {
      // Find last whitespace in line
      int i = max;
      while (i >= 0 && !char.IsWhiteSpace(text[pos + i]))
        i--;

      // If no whitespace found, break at maximum length
      if (i < 0)
        return max;

      // Find start of whitespace
      while (i >= 0 && char.IsWhiteSpace(text[pos + i]))
        i--;

      // Return length of text before whitespace
      return i + 1;
    }

    public void Fire(Exception exception)
    {
      var assembly = typeof(Resources).GetTypeInfo().Assembly;

      if (exception is PyException pythonException)
        FullError = $"[Python]: \n{pythonException.PythonStackTrance}\n\n";
      else
        FullError = $"[C#]: \n{exception.GetType().Name}: {exception.Message}\n\n{exception.StackTrace}";

      // assembly.GetManifestResourceNames() - to check
      _errorsBanner = Resources.LoadTexture(assembly.GetManifestResourceStream("Oxy.Framework.builtin.img.error.png"));
      _ttfFont = Resources.LoadFont(assembly.GetManifestResourceStream("Oxy.Framework.builtin.font.monospace.ttf"));

      FullError = WordWrap(FullError, 50);
      FullError = FullError.Replace("\nat", "\n\n    at");

      _text = Graphics.NewText(_ttfFont, FullError);
      Graphics.SetBackgroundColor(100, 100, 100);
    }

    public void DrawErrors()
    {
      Graphics.Draw(_errorsBanner, 60, 60);
      Graphics.Draw(_text, 60, 120);
    }
  }
}