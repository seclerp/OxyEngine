namespace Oxy.Framework.TestPlayer
{
  class Program
  {
    static void Main(string[] args)
    {
      // Create window
      using (var window = new Window(800, 600, "Hello world from OpenTK window!"))
      {
        // Run at 60 FPS
        window.Run(60);
      }
    }
  }
}