using System;
using System.IO;

namespace Oxy.Framework.TestPlayer
{
  class Program
  {
    static void Main(string[] args)
    {
      Common.SetScriptsRoot(Path.Combine(Environment.CurrentDirectory, "scripts"));
      Common.SetLibraryRoot(Path.Combine(Environment.CurrentDirectory, "library"));

      var font = Resources.LoadFont("roboto.ttf");
      
      Graphics.SetFont(font);
      
      Window.OnDraw(() => Graphics.Print("Hello, World OxyEngine", 50, 50));

      Window.Show(60);
      
      //Common.ExecuteScript("entry.py");
      //Common.ExecuteScript("print-example.py");
    }
  }
}