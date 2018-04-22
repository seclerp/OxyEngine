using System;
using System.Linq;
using System.Text;

namespace OxyEngine.Helpers
{
  public static class DelegatesHelper
  {
    public static string GetSignature(this Delegate del)
    {
      var type = del.GetType();
      var method = type.GetMethod("Invoke");
      var parameters = string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
      return $"{method.ReturnType.Name} {type.Name}({parameters})";
    }
  }
}