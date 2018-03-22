using OxyEngine.Interfaces;

namespace OxyEngine
{
  public class Oxy
  {
    public static Resources Resources { get; internal set; }
    public static Graphics Graphics { get; internal set; }
    public static IScripting Scripting { get; internal set; }
    public static Events Events { get; internal set; }
    public static Input Input { get; internal set; }
  }
}