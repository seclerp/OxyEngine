using System;
using System.Collections.Generic;
using OxyEngine.EventHandlers;

namespace OxyEngine
{
  /// <summary>
  ///   Represents simple event system for games
  /// </summary>
  public class EventSystem
  {
    private Dictionary<string, EngineEventHandler> _registry;

    public void StartListening(string eventName, EngineEventHandler handler)
    {
      if (!_registry.ContainsKey(eventName))
      {
        _registry[eventName] = handler;
      }
      else
      {
        _registry[eventName] += handler;
      }
    }
    
    public void StopListening(string eventName, EngineEventHandler handler)
    {
      if (!_registry.ContainsKey(eventName))
        throw new Exception($"No handlers for event with name '{eventName}'");
      
      _registry[eventName] -= handler;
    }

    public void Invoke(string eventName, EngineEventArgs args, bool checkEventExists = false)
    {
      if (!_registry.ContainsKey(eventName))
      {
        if (checkEventExists)
          throw new Exception($"No handlers for event with name '{eventName}'");
        
        // If no listeners and checkEventExists == false, then ignore call
        return;
      }

      if (_registry[eventName] == null)
      {
        _registry.Remove(eventName);
        return;
      }

      _registry[eventName].Invoke(this, args);
    }
  }
}