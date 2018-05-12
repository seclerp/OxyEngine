using System;
using System.Collections.Generic;
using OxyEngine.Ecs.Components;

namespace OxyEngine.Ecs.Interfaces
{
  /// <summary>
  ///   Interface for entities that may have components
  /// </summary>
  public interface IConstructable
  {
    /// <summary>
    ///   Adds component to the entity
    /// </summary>
    /// <param name="component">Component object</param>
    /// <exception cref="ArgumentNullException">If component is null</exception>
    void AddComponent(GameComponent component);
    
    /// <summary>
    ///   Adds component to the entity
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    T AddComponent<T>() where T : GameComponent;
    
    /// <summary>
    ///   Returns first component of given type
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <returns>First component of given type</returns>
    /// <exception cref="Exception">If component with that type not found</exception>
    T GetComponent<T>() where T : GameComponent;
    
    /// <summary>
    ///   Returns all components of given type
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <returns>Collection of all components of given type</returns>
    IEnumerable<T> GetComponents<T>() where T : GameComponent;
    
    /// <summary>
    ///   Removes component from entity
    /// </summary>
    /// <param name="component">Component object</param>
    /// <returns>True if deleted more than 0</returns>
    /// <exception cref="ArgumentNullException"></exception>
    bool RemoveComponent(GameComponent component);
    
    /// <summary>
    ///   Removes component with given type from entity
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <returns>True if deleted more than 0</returns>
    bool RemoveComponent<T>() where T : GameComponent;
    
    /// <summary>
    ///   Removes all components with given type from entity
    /// </summary>
    /// <typeparam name="T">Component type</typeparam>
    /// <returns>True if deleted more than 0</returns>
    bool RemoveComponents<T>() where T : GameComponent;
  }
}