using OxyEngine.Interfaces;

namespace OxyEngine
{
  public class OxyApi
  {
    public Resources Resources { get; internal set; }
    public Graphics Graphics { get; internal set; }
    public IScripting Scripting { get; internal set; }
    public Events Events { get; internal set; }
    public Input Input { get; internal set; }
  }
}