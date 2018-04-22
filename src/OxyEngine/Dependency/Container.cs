using System.Collections.Generic;
using System.Data;
using OxyEngine.Loggers;

namespace OxyEngine.Dependency
{
  public class Container
  {
    private static Container _instance;
    public static Container Instance
    {
      get
      {
        if (_instance == null)
          _instance = new Container();

        return _instance;
      }
    }

    private Dictionary<string, object> _registry;

    private Container()
    {
      _registry = new Dictionary<string, object>();
    }

    public void RegisterByName(string name, object value)
    {
      if (_registry.ContainsKey(name))
      {
        throw new DuplicateNameException($"This name already registered: '{name}'");
      }

      _registry[name] = value;
      LogManager.Log($"Registered named instance '{name}'");
    }

    public object ResolveByName(string name)
    {
      if (!_registry.ContainsKey(name))
      {
        throw new KeyNotFoundException($"Name not registered: '{name}'");
      }

      return _registry[name];
    }

    public T ResolveByName<T>(string name)
    {
      return (T) ResolveByName(name);
    }
  }
}