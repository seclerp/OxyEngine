using System;

namespace Oxy.Framework
{
  /// <summary>
  /// Base class for every lazy module
  /// </summary>
  /// <typeparam name="T">Singletone instance type (must have parameterless public constructor</typeparam>
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