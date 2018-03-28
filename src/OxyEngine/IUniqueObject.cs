using System;

namespace OxyEngine
{
  public interface IUniqueObject
  {
    /// <summary>
    ///   Unique ID used by internal engine logic
    /// </summary>
    Guid Id { get; }
  }
}