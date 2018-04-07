using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Entities;
using OxyEngine.Interfaces;

namespace OxyEngine.Ecs.Systems
{
  public class LogicSystem : BaseGameSystem, IUpdateable, IApiUser
  {
    private GameInstance _gameInstance;

    public LogicSystem(GameInstance gameInstance, BaseGameEntity rootEntity) : base(rootEntity)
    {
      _gameInstance = gameInstance;
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

    public OxyApi GetApi()
    {
      return _gameInstance.GetApi();
    }
  }
}