using OxyEngine.ECS.Entities;

namespace OxyEngine.ECS.Systems
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