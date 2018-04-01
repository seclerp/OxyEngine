using System;
using System.Security.Policy;

namespace OxyEngine.ECS.Systems
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public class UseGameSystemAttribute : Attribute
  {
    public string SystemName { get; set; }

    public UseGameSystemAttribute(string systemName)
    {
      SystemName = systemName;
    }
  }
}