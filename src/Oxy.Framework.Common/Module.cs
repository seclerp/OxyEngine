namespace Oxy.Framework
{
  /// <summary>
  ///   Base class for every non-lazy module
  /// </summary>
  /// <typeparam name="T">Singletone instance type (must have parameterless public constructor</typeparam>
  public class Module<T> where T : new()
  {
    private static T _instance;

    protected static T Instance
    {
      get
      {
        if (_instance == null)
          _instance = new T();
        return _instance;
      }
    }
  }
}