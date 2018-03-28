using System;
using System.Collections.Generic;
using System.Linq;
using OxyEngine.ECS.Components;
using OxyEngine.ECS.Interfaces;

namespace OxyEngine.ECS
{
  public abstract class GameEntity : IUniqueObject, IConstructable
  {
    public Guid Id { get; }
    
    public GameEntity Parent { get; private set; }
    public TransformComponent Transform { get; }
    
    public IEnumerable<GameEntity> Children =>  _children;
    public IEnumerable<GameComponent> Components =>  _components;
    
    private readonly List<GameComponent> _components;
    private readonly List<GameEntity> _children;

    protected GameEntity()
    {
      Id = Guid.NewGuid();
      _components = new List<GameComponent>();
      Transform = new TransformComponent(this);
      _components.Add(Transform);
      _children = new List<GameEntity>();
    }

    protected GameEntity(GameEntity parent) : this()
    {
      SetParent(parent);
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
      
      component.SetEntity(this);
      _components.Add(component);
    }

    /// <summary>
    ///   Adds component to the entity
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    public T AddComponent<T>() where T : GameComponent
    {
      var component = (T) Activator.CreateInstance(typeof(T), this);
      _components.Add(component);
      
      return component;
    }

    /// <summary>
    ///   Returns first component of given type
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <returns>First component of given type</returns>
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
    /// <returns>Collection of all components of given type</returns>
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
      
      if (component.Entity != this)
        throw new Exception($"Given entity is not a direct child of this object: '{component}'");
      
      component.SetEntity(null);
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
          _components[i].SetEntity(null);
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
      var success = false;
      for (int i = 0; i < _components.Count; i++)
      {
        if (_components[i] is T)
        {
          _components[i].SetEntity(null);
          _components.RemoveAt(i);
          i--;
          success = true;
        }
      }

      return success;
    }
    
    /// <summary>
    ///   Set parent for this entity
    /// </summary>
    /// <param name="parent">Parent Entity</param>
    internal void SetParent(GameEntity parent)
    {
      Parent = parent;
    }

    /// <summary>
    ///   Adds child to this entity
    /// </summary>
    /// <param name="child">Child Entity</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddChild(GameEntity child)
    {
      if (child == null)
        throw new ArgumentNullException(nameof(child));
      
      child.SetParent(this);
      _children.Add(child);
    }

    /// <summary>
    ///   Adds child to this entity
    /// </summary>
    /// <typeparam name="T">Type of the Entity</typeparam>
    /// <returns>Newly created Entity</returns>
    public T AddChild<T>() where T : GameEntity
    {
      return (T) Activator.CreateInstance(typeof(T), this);
    }

    /// <summary>
    ///   Returns first entity of given type
    /// </summary>
    /// <typeparam name="T">Type of the Entity</typeparam>
    /// <returns>First entity of given type</returns>
    /// <exception cref="Exception">If not Entity was found</exception>
    public T GetChild<T>() where T : GameEntity
    {
      foreach (var component in _children)
      {
        if (component is T tComponent)
          return tComponent;
      }
      
      throw new Exception($"Child entity of type '{typeof(T).Name}' not found");
    }

    /// <summary>
    ///   Returns all entities of given type
    /// </summary>
    /// <typeparam name="T">Type of the Entity</typeparam>
    /// <returns>All entities of given type</returns>
    public IEnumerable<T> GetChildren<T>() where T : GameEntity
    {
      var result = new List<T>();
      
      foreach (var child in _children)
      {
        if (child is T tChild)
          result.Add(tChild);
      }

      return result;
    }

    /// <summary>
    ///   Removes child
    /// </summary>
    /// <param name="child">Entity object to remove</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public bool RemoveChild(GameEntity child)
    {
      if (child == null)
        throw new ArgumentNullException(nameof(child));
      
      if (child.Parent != this)
        throw new Exception($"Given entity is not a direct child of this object: '{child}'");
      
      child.SetParent(null);
      return _children.Remove(child);
    }

    /// <summary>
    ///   Remove first child of given type
    /// </summary>
    /// <typeparam name="T">Type of the Entity</typeparam>
    /// <returns>True if child was deleted</returns>
    public bool RemoveChild<T>() where T : GameEntity
    {
      for (int i = 0; i < _children.Count; i++)
      {
        if (_children[i] is T)
        {
          _children[i].SetParent(null);
          _children.RemoveAt(i);
          return true;
        }
      }

      return false;
    }

    /// <summary>
    ///   Remove all children of given type
    /// </summary>
    /// <typeparam name="T">Type of the Entity</typeparam>
    /// <returns></returns>
    public bool RemoveChildren<T>() where T : GameEntity
    {
      var success = false;
      for (int i = 0; i < _children.Count; i++)
      {
        if (_children[i] is T)
        {
          _components[i].SetEntity(null);
          _components.RemoveAt(i);
          i--;
          success = true;
        }
      }

      return success;
    }
  }
} 