using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Entities;

namespace OxyEngine.Ecs.Systems
{
  public class LogicSystem : BaseGameSystem, IUpdateable
  {
    public LogicSystem(BaseGameEntity rootEntity) : base(rootEntity)
    {
    }
        
    public void Init()
    {
      InitRecursive(RootEntity);
    }
    
    public void Load()
    {
      LoadRecursive(RootEntity);
    }
    
    public void Update(float dt)
    {
      UpdateRecursive(RootEntity, dt);
    }

    private void InitRecursive(BaseGameEntity entity)
    {
      if (entity is IInitializable entityInitializable)
        entityInitializable.Init();
      
      foreach (var component in entity.Components)
      {
        if (component is IInitializable componentInitializable)
          componentInitializable.Init();
      }
      
      foreach (var child in entity.Children)
      {
        InitRecursive(child);
      }
    }
    
    private void LoadRecursive(BaseGameEntity entity)
    {
      if (entity is ILoadable entityLoadable)
        entityLoadable.Load();

      foreach (var component in entity.Components)
      {
        if (component is ILoadable componentLoadable)
          componentLoadable.Load();
      }
      
      foreach (var child in entity.Children)
      {
        LoadRecursive(child);
      }
    }

    private void UpdateRecursive(BaseGameEntity entity, float dt)
    {
      if (entity is IUpdateable entityUpdatable)
        entityUpdatable.Update(dt);

      foreach (var component in entity.Components)
      {
        if (component is IUpdateable componentUpdatable)
          componentUpdatable.Update(dt);
      }
      
      foreach (var child in entity.Children)
      {
        UpdateRecursive(child, dt);
      }
    }
  }
}