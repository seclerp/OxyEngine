using OxyEngine.Events.Args;
using OxyEngine.Interfaces;

namespace OxyEngine.Events
{
  public class GlobalEventsManager : IModule
  {
    #region Global game events

    private readonly EventSystem _global;
    
    #endregion

    public GlobalEventsManager()
    {
      _global = new EventSystem();
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

    internal void Load()
    {
      _global.Invoke(EventNames.Initialization.OnLoad, null);
    }
    
    internal void Unload()
    {
      _global.Invoke(EventNames.Initialization.OnUnload, null);
    }
    
    internal void Update(double dt)
    {
      _global.Invoke(EventNames.Gameloop.OnUpdate, new EngineUpdateEventArgs { DeltaTime = dt });
    }
    
    internal void Draw()
    {
      _global.Invoke(EventNames.Graphics.OnDraw, null);
    }
    
    internal void Resize()
    {
      _global.Invoke(EventNames.Window.OnResize, null);
    }

    #endregion
  }
}