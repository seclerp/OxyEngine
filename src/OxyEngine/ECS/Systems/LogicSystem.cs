using System.Security.Permissions;
using OxyEngine.ECS.Behaviours;
using OxyEngine.ECS.Entities;

namespace OxyEngine.ECS.Systems
{
  public class LogicSystem : BaseGameSystem, IUpdateable
  {
    public LogicSystem(BaseGameEntity rootEntity) : base(rootEntity)
    {
    }
        
    public void Load()
    {
      LoadRecursive(RootEntity);
    }
    
    public void Update(float dt)
    {
      UpdateRecursive(RootEntity, dt);
    }

    private void LoadRecursive(BaseGameEntity entity)
    {
      if (entity is ILoadable rootEntityUpdatable)
        rootEntityUpdatable.Load();

      foreach (var updatableChildren in RootEntity.Children)
      {
        LoadRecursive(updatableChildren);
      }
    }

    private void UpdateRecursive(BaseGameEntity entity, float dt)
    {
      if (entity is IUpdateable rootEntityUpdatable)
        rootEntityUpdatable.Update(dt);

      foreach (var updatableChildren in RootEntity.Children)
      {
        UpdateRecursive(updatableChildren, dt);
      }
    }
  }
}