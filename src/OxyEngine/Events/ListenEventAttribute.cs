using System;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace OxyEngine.Events
{
  [AttributeUsage(AttributeTargets.Method)]
  public class ListenEventAttribute : Attribute
  {
    public string EventName { get; }
    internal string MethodName { get; }
    
    public ListenEventAttribute(string eventName, [CallerMemberName] string callerMemberName = null)
    {
      EventName = eventName;
      MethodName = callerMemberName;
    }
  }
}