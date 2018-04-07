using OxyEngine.Ecs.Entities;

namespace OxyEngine.Ecs.Systems
{
  public class BaseGameSystem
  {
    protected BaseGameEntity RootEntity;
    
    public BaseGameSystem(BaseGameEntity rootEntity)
    {
      RootEntity = rootEntity;
    }
  }
}