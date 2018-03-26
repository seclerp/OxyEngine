using System;
using System.Collections.Generic;

namespace OxyEngine
{
  public static class CollectionsHelper
  {
    public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> func)
    {
      foreach (var item in enumerable)
        func(item);
    }
  }
}