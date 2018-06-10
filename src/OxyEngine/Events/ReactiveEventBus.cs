using System;
using System.Collections.Generic;

namespace OxyEngine.Events
{
  public class ReactiveEventBus : IEventBus
  {
    private Dictionary<string, Action<object>> _handlers;

    public ReactiveEventBus()
    {
      _handlers = new Dictionary<string, Action<object>>();
    }
    
    public void Subscribe(string eventName, Action<object> handler)
    {
      if (!_handlers.ContainsKey(eventName))
      {
        _handlers[eventName] = handler;
        return;
      }

      _handlers[eventName] += handler;
    }

    public void Send(string eventName, object payload = null)
    {
      if (!_handlers.ContainsKey(eventName))
      {
        return;
      }
      
      _handlers[eventName].Invoke(payload);
    }
  }
}