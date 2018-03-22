using OxyEngine.EventHandlers;

namespace OxyEngine
{
  public class Events
  {
    #region Global game events

    public EventSystem Global { get; }
    
    #endregion

    public Events()
    {
      Global = new EventSystem();
    }
    
    #region Invokers

    public void Load()
    {
      Global.Invoke("load", null);
    }
    
    public void Unload()
    {
      Global.Invoke("unload", null);
    }
    
    public void Update(double dt)
    {
      Global.Invoke("update", new EngineUpdateEventArgs { DeltaTime = dt });
    }
    
    public void Draw()
    {
      Global.Invoke("draw", null);
    }
    
    public void Resize()
    {
      Global.Invoke("resize", null);
    }

    #endregion
  }
}