using System;
using System.IO;

namespace Oxy.Framework.TestPlayer
{
  class Program
  {
    static void Main(string[] args)
    {
      // Set scripts root folder. All script paths will be relative to this folder
      Common.SetScriptsRoot(Path.Combine(Environment.CurrentDirectory, "scripts"));
      // Set library root folder. All asset paths will be relative to this folder
      Common.SetLibraryRoot(Path.Combine(Environment.CurrentDirectory, "library"));

      // Text object for "Hello, World!" text
      TextObject textObj = null;
      
      // Callback for OnLoad event to load and initialize all things
      Window.OnLoad(() =>
      {
        var font = Resources.LoadFont("roboto.ttf");
        textObj = Graphics.NewText(font, "Hello, World!");
      });      
      
      // On draw calls every frame to draw graphics on screen
      Window.OnDraw(() => Graphics.Draw(textObj, 50, 50));
      
      // Start gameloop (at 60 FPS by default)
      Window.Show();
      
      //Common.ExecuteScript("entry.py");
      //Common.ExecuteScript("print-example.py");
    }
  }
}