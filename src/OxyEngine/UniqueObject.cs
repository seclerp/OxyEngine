using System;
using Newtonsoft.Json;

namespace OxyEngine
{
  public class UniqueObject
  {
    /// <summary>
    ///   Unique ID used by internal engine logic
    /// </summary>
    [JsonIgnore]
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

    [JsonProperty]
    private Guid _id;
  }
}