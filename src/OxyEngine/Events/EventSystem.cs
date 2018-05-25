using System;
using System.Collections.Generic;
using System.Reflection;
using OxyEngine.Events.Args;
using OxyEngine.Events.Handlers;
using OxyEngine.Loggers;

namespace OxyEngine.Events
{
  /// <summary>
  ///   Represents simple event system for games
  /// </summary>
  public class EventSystem : UniqueObject
  {
    private const BindingFlags BindingFlasgAll =  
      BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
      BindingFlags.Static | BindingFlags.FlattenHierarchy;
    
    private Dictionary<string, EngineEventHandler> _registry;

    public bool LogEventCalls { get; set; }
    public bool LogListenerRegistration { get; set; }
    
    public EventSystem()
    {
      _registry = new Dictionary<string, EngineEventHandler>();
    }
    
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

      if (LogListenerRegistration)
      {
        LogManager.Log($"Start listening event '{eventName}'',\n\t  event system '{Id}'");
      }
    }

    public void AddListenersUsingAttributes(object obj)
    {
      var type = obj.GetType();
      
      foreach (var methodInfo in type.GetMethods(BindingFlasgAll))
      {
        foreach (var attribute in methodInfo.GetCustomAttributes<ListenEventAttribute>(true))
        {
          var method = type.GetMethod(attribute.MethodName, BindingFlasgAll);

          if (method is null)
          {
            throw new NullReferenceException(nameof(method));
          }

          if (method.IsStatic)
          {
            _registry.Add(attribute.EventName, 
              (EngineEventHandler)Delegate.CreateDelegate(typeof(EngineEventHandler), method));
          }
          else
          {
            _registry.Add(attribute.EventName, 
              (EngineEventHandler)Delegate.CreateDelegate(typeof(EngineEventHandler), obj, method.Name));
          }
        }
      }
    }
    
    public void Invoke(string eventName, EngineEventArgs args, bool checkEventExists = false)
    {
      if (!_registry.ContainsKey(eventName))
      {
        if (checkEventExists)
          throw new Exception($"No handlers for event with name '{eventName}'',\n\t  event system '{Id}'");
        
        // If no listeners and checkEventExists == false, then log and ignore call
        LogManager.Warning($"No listeners for event with name '{eventName}'',\n\t  event system '{Id}'");
        return;
      }

      // If handler == null, remove it
      if (_registry[eventName] == null)
      {
        _registry.Remove(eventName);
        return;
      }

      _registry[eventName].Invoke(this, args);
    }
  }
}