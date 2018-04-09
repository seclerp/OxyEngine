using OxyEngine.Events.Args;
using OxyEngine.Interfaces;

namespace OxyEngine.Events
{
  public class GlobalEventsManager : IModule
  {
    #region Global game events

    // Public because used by scripting
    public readonly EventSystem Global;
    
    #endregion

    public GlobalEventsManager()
    {
      Global = new EventSystem();
    }

    #region Fabrics

    /// <summary>
    ///   Return new event system
    /// </summary>
    /// <returns>New event system</returns>
    public EventSystem NewEventSystem()
    {
      return new EventSystem();
    }

    #endregion
    
    #region Global invokers

    internal void Init()
    {
      Global.Invoke(EventNames.Initialization.OnInit, null);
    }
    
    internal void Load()
    {
      Global.Invoke(EventNames.Initialization.OnLoad, null);
    }
    
    internal void Unload()
    {
      Global.Invoke(EventNames.Initialization.OnUnload, null);
    }
    
    internal void Update(double dt)
    {
      Global.Invoke(EventNames.Gameloop.OnUpdate, new EngineUpdateEventArgs { DeltaTime = dt });
    }
    
    internal void Draw()
    {
      Global.Invoke(EventNames.Graphics.OnDraw, null);
    }
    
    internal void Resize()
    {
      Global.Invoke(EventNames.Window.OnResize, null);
    }

    #endregion
  }
}