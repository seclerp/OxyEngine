using System;
using System.Collections.Generic;
using OxyEngine.ECS.Iterfaces;

namespace OxyEngine.ECS.Entities
{
  public class SimpleEntity : GameEntity, IConstructable
  {
    private readonly List<GameComponent> _components;

    public SimpleEntity()
    {
      _components = new List<GameComponent>();
    }
    
    /// <summary>
    ///   Adds component to the entity
    /// </summary>
    /// <param name="component">Component object</param>
    /// <exception cref="ArgumentNullException">If component is null</exception>
    public void AddComponent(GameComponent component)
    {
      if (component == null)
        throw new ArgumentNullException(nameof(component));
      
      _components.Add(component);
    }

    /// <summary>
    ///   Adds component to the entity
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    public void AddComponent<T>() where T : GameComponent
    {
      _components.Add((T) Activator.CreateInstance(typeof(T), this));
    }

    /// <summary>
    ///   Returns first component of given type
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <returns></returns>
    /// <exception cref="Exception">If component with that type not found</exception>
    public T GetComponent<T>() where T : GameComponent
    {
      foreach (var component in _components)
      {
        if (component is T tComponent)
          return tComponent;
      }
      
      throw new Exception($"Component of type '{typeof(T).Name}' not found");
    }

    /// <summary>
    ///   Returns all components of given type
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <returns></returns>
    public IEnumerable<T> GetComponents<T>() where T : GameComponent
    {
      var result = new List<T>();
      
      foreach (var component in _components)
      {
        if (component is T tComponent)
          result.Add(tComponent);
      }

      return result;
    }

    /// <summary>
    ///   Removes component from entity
    /// </summary>
    /// <param name="component">Component object</param>
    /// <returns>True if deleted more than 0</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public bool RemoveComponent(GameComponent component)
    {
      if (component == null)
        throw new ArgumentNullException(nameof(component));
      
      return _components.Remove(component);
    }

    /// <summary>
    ///   Removes component with given type from entity
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <returns>True if deleted more than 0</returns>
    public bool RemoveComponent<T>() where T : GameComponent
    {
      for (int i = 0; i < _components.Count; i++)
      {
        if (_components[i] is T)
        {
          _components.RemoveAt(i);
          return true;
        }
      }

      return false;
    }

    /// <summary>
    ///   Removes all components with given type from entity
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <returns>True if deleted more than 0</returns>
    public bool RemoveComponents<T>() where T : GameComponent
    {
      return _components.RemoveAll(c => c is T) != 0;
    }
  }
}