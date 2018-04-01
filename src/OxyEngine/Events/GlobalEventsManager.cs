using OxyEngine.Events.Args;
using OxyEngine.Interfaces;

namespace OxyEngine.Events
{
  public class GlobalEventsManager : IModule
  {
    #region Global game events

    public EventSystem Global { get; }
    
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

    internal void Load()
    {
      Global.Invoke("load", null);
    }
    
    internal void Unload()
    {
      Global.Invoke("unload", null);
    }
    
    internal void Update(double dt)
    {
      Global.Invoke("update", new EngineUpdateEventArgs { DeltaTime = dt });
    }
    
    internal void Draw()
    {
      Global.Invoke("draw", null);
    }
    
    internal void Resize()
    {
      Global.Invoke("resize", null);
    }

    #endregion
  }
}