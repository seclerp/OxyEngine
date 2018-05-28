using System.Collections.Generic;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Entities;

namespace OxyEngine.Ecs.Systems
{
  public class GenericSystem : GameSystem, IUpdateable
  {
    private static Dictionary<IInitializable, bool> _isInitialized = new Dictionary<IInitializable, bool>();
    private static Dictionary<ILoadable, bool> _isLoaded = new Dictionary<ILoadable, bool>();
    
    internal static Queue<IInitializable> InitializeQueue = new Queue<IInitializable>();
    internal static Queue<ILoadable> LoadQueue = new Queue<ILoadable>();
    
    public GenericSystem(GameEntity rootEntity) : base(rootEntity)
    {
      if (rootEntity is IInitializable initializableRootEntity)
      {
        InitializeQueue.Enqueue(initializableRootEntity);
      }
      
      if (rootEntity is ILoadable loadableRootEntity)
      {
        LoadQueue.Enqueue(loadableRootEntity);
      }
    }
        
    public void Init()
    {
      ProcessInitQueue();
    }
    
    public void Load()
    {
      ProcessLoadQueue();
    }
    
    public void Update(float dt)
    {
      UpdateRecursive(RootEntity, dt);
    }

    private void UpdateRecursive(GameEntity entity, float dt)
    {
      ProcessInitQueue();
      ProcessLoadQueue();
      
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

    private void ProcessInitQueue()
    {
      while (InitializeQueue.Count > 0)
      {
        var result = InitializeQueue.Dequeue();

        if (!_isInitialized.ContainsKey(result))
        {
          result.Init();
          _isInitialized[result] = true;
        }
      }
    }
    
    private void ProcessLoadQueue()
    {
      while (LoadQueue.Count > 0)
      {
        var result = LoadQueue.Dequeue();

        if (!_isLoaded.ContainsKey(result))
        {
          result.Load();
          _isLoaded[result] = true;
        }
      }
    }
  }
}