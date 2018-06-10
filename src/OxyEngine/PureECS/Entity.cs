﻿using System;
using System.Collections.Generic;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Systems;

namespace OxyEngine.PureECS
{
  public class Entity : UniqueObject
  {
    /// <summary>
    ///   Name of an entity
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    ///   Parent for this entity
    ///   null if entity is root
    /// </summary>
    public Entity Parent { get; private set; }
    
    /// <summary>
    ///   Collection of entity children
    /// </summary>
    public IEnumerable<Entity> Children =>  _children;
    
    /// <summary>
    ///   Collection of entity components
    /// </summary>
    public IEnumerable<Component> Components =>  _components;

    internal GameSystemManager GameSystemManager { get; set; }

    private readonly List<Component> _components;
    private readonly List<Entity> _children;

    protected Entity() 
    {
      _components = new List<Component>();
      _children = new List<Entity>();
    }

    protected Entity(Entity parent) : this()
    {
      if (parent != null)
      {
        SetParent(parent);
      }
    }

    #region Components actions
    
    public void AddComponent(Component component)
    {
      if (component == null)
        throw new ArgumentNullException(nameof(component));
      
      component.SetEntity(this);
      _components.Add(component);
    }

    public T AddComponent<T>() where T : Component
    {
      var component = Activator.CreateInstance<T>();

      AddComponent(component);
      
      return component;
    }

    public T GetComponent<T>() where T : Component
    {
      foreach (var component in _components)
      {
        if (component is T tComponent)
          return tComponent;
      }
      
      throw new Exception($"Component of type '{typeof(T).Name}' not found");
    }

    public IEnumerable<T> GetComponents<T>() where T : Component
    {
      var result = new List<T>();
      
      foreach (var component in _components)
      {
        if (component is T tComponent)
          result.Add(tComponent);
      }

      return result;
    }

    public bool RemoveComponent(Component component)
    {
      if (component == null)
        throw new ArgumentNullException(nameof(component));
      
      if (component.Entity != this)
        throw new Exception($"Given entity is not a direct child of this object: '{component}'");
      
      component.SetEntity(null);
      return _components.Remove(component);
    }

    public bool RemoveComponent<T>() where T : Component
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

    public bool RemoveComponents<T>() where T : Component
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
    
    #endregion
    
    #region Children-parent actions
    
    /// <summary>
    ///   Set parent for this entity
    /// </summary>
    /// <param name="parent">Parent Entity</param>
    internal void SetParent(Entity parent)
    {
      Parent = parent;

      if (parent == null)
      {
        return;
      }
      
      if (this is IInitializable initializableEntity)
      {
        GenericSystem.InitializeQueue.Enqueue(initializableEntity);
      }
      
      if (this is ILoadable loadableEntity)
      {
        GenericSystem.LoadQueue.Enqueue(loadableEntity);
      }
    }

    /// <summary>
    ///   Adds child to this entity
    /// </summary>
    /// <param name="child">Child Entity</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddChild(Entity child)
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
    public T AddChild<T>() where T : Entity
    {
      var child = (T) Activator.CreateInstance(typeof(T), this);
      
      AddChild(child);
      return child;
    }

    /// <summary>
    ///   Returns first entity of given type
    /// </summary>
    /// <typeparam name="T">Type of the Entity</typeparam>
    /// <returns>First entity of given type</returns>
    public T GetChild<T>(bool searchRecursively = false) where T : Entity
    {
      foreach (var child in _children)
      {
        if (child is T tChild)
          return tChild;
      }

      if (searchRecursively)
      {
        foreach (var child in _children)
        {
          var result = child.GetChild<T>(true);
          if (result != null)
            return result;
        }
      }

      return null;
    }

    /// <summary>
    ///   Returns all entities of given type
    /// </summary>
    /// <typeparam name="T">Type of the Entity</typeparam>
    /// <returns>All entities of given type</returns>
    public IEnumerable<T> GetChildren<T>() where T : Entity
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
    public bool RemoveChild(Entity child)
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
    public bool RemoveChild<T>() where T : Entity
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
    public bool RemoveChildren<T>() where T : Entity
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

    #endregion
  }
}