namespace Oxy.Framework
{
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