using System;

namespace Oxy.Framework.Exceptions
{
  public class PyException : Exception
  {
    public string PythonStackTrance { get; }
    
    public PyException(string message, string stackTrace, Exception inner) : base(message, inner)
    {
      PythonStackTrance = stackTrace;
    }
  }
}