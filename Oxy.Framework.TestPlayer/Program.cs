using System;

namespace Oxy.Framework.TestPlayer
{
  class Program
  {
    static void Main(string[] args)
    {
      Window.SetTitle("Hello World from OxyEngine!");

      Window.OnUpdate((dt) => 
      {
        if (Input.IsMousePressed("middle") || Input.IsKeyPressed("escape"))
          Window.Exit();
      });

      Window.Show(60);
    }
  }
}