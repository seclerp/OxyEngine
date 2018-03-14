using System;

namespace Oxy.Framework.Exceptions
{
  public class PyException : Exception
  {
    public PyException(string message, string stackTrace) : base(message)
    {
      PythonStackTrance = stackTrace;
    }

    public string PythonStackTrance { get; }
  }
}