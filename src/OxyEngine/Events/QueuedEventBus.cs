using System;
using System.Collections.Generic;

namespace OxyEngine.Events
{
  public class QueuedEventBus : IEventBus
  {
    private Dictionary<string, Action<object>> _handlers;
    private Queue<(string EventName, object Payload)> _sendQueue;
    
    public QueuedEventBus()
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
      _sendQueue.Enqueue((eventName, payload));
    }

    public void ProcessQueue()
    {
      while (_sendQueue.Count > 0)
      {
        var item = _sendQueue.Dequeue();
        
        if (!_handlers.ContainsKey(item.EventName))
        {
          continue;
        }
      
        _handlers[item.EventName].Invoke(item.Payload);
      }
    }
  }
}