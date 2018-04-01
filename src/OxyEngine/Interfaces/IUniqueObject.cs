using System;

namespace OxyEngine.Interfaces
{
  public interface IUniqueObject
  {
    /// <summary>
    ///   Unique ID used by internal engine logic
    /// </summary>
    Guid Id { get; }
  }
}