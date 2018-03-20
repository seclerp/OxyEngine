using System;

namespace OxyEngine.EventManagers
{
  public class GameLifetimeEventManager
  {
    public event Action OnLoad;
    public event Action OnUnload;
    public event Action<double> OnUpdate;
    public event Action OnDraw;
    public event Action<int, int> OnResize;

    public void Load()
    {
      OnLoad?.Invoke();
    }
    
    public void Unload()
    {
      OnUnload?.Invoke();
    }
    
    public void Update(double dt)
    {
      OnUpdate?.Invoke(dt);
    }
    
    public void Draw()
    {
      OnDraw?.Invoke();
    }
    
    public void Resize()
    {
      OnDraw?.Invoke();
    }
  }
}