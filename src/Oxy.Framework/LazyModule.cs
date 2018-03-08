using System;

namespace Oxy.Framework
{
  public class LazyModule<T> where T : new()
  {
    private static Lazy<T> _instance;

    protected static Lazy<T> Instance
    {
      get
      {
        if (_instance == null)
          _instance = new Lazy<T>(() => new T());
        return _instance;
      }
    }
  }
}