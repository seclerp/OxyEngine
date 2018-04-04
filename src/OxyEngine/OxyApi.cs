using OxyEngine.Events;
using OxyEngine.Graphics;
using OxyEngine.Input;
using OxyEngine.Interfaces;
using OxyEngine.Resources;

namespace OxyEngine
{
  public class OxyApi
  {
    public ResourceManager Resources { get; internal set; }
    public GraphicsManager Graphics { get; internal set; }
    public IScriptingManager Scripting { get; internal set; }
    public GlobalEventsManager Events { get; internal set; }
    public InputManager Input { get; internal set; }
  }
}