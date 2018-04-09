using System;

namespace OxyEngine
{
  public class UniqueObject
  {
    /// <summary>
    ///   Unique ID used by internal engine logic
    /// </summary>
    public Guid Id
    {
      get
      {
        if (_id == default(Guid))
        {
          _id = Guid.NewGuid();
        }

        return _id;
      }
    }

    private Guid _id;
  }
}