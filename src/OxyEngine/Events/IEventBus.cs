using System;

namespace OxyEngine.Events
{
  public interface IEventBus
  {
    void Subscribe(string eventName, Action<object> handler);
    void Send(string eventName, object payload);
  }
}