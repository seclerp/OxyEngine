using System;

namespace OxyEngine.Ecs.Systems
{
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
  public class GameSystemActionAttribute : Attribute
  {
    public readonly string Name;

    public GameSystemActionAttribute(string name)
    {
      Name = name;
    }
  }
}