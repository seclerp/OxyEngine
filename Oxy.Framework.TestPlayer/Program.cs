using System;
using System.IO;

namespace Oxy.Framework.TestPlayer
{
  class Program
  {
    static void Main(string[] args)
    {
      Common.SetScriptsRoot(Path.Combine(Environment.CurrentDirectory, "scripts"));
      Common.ExecuteScript("entry.py");

      /*Window.SetTitle("Hello World from OxyEngine!");

      Window.OnUpdate((dt) => 
      {
        if (Input.IsMousePressed("middle") || Input.IsKeyPressed("escape"))
          Window.Exit();
      });

      Window.Show(60);*/
    }
  }
}