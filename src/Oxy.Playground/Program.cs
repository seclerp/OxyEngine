using System;

namespace Oxy.Playground
{
  public static class Program
  {
    [STAThread]
    static void Main()
    {
      using (var playground = new PlaygroundInstance())
      {
        playground.Run();
      }
    }
  }
}
