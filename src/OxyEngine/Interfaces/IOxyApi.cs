using OxyEngine.Audio;
using OxyEngine.Events;
using OxyEngine.Graphics;
using OxyEngine.Input;
using OxyEngine.Resources;
using OxyEngine.Window;

namespace OxyEngine.Interfaces
{
  public interface IOxyApi
  {
    ResourceManager Resources { get; }
    GraphicsManager Graphics { get; }
    AudioManager Audio { get; }
    IScriptingManager Scripting { get; }
    GlobalEventsManager Events { get; }
    InputManager Input { get; }
    WindowManager Window { get; }
  }
}