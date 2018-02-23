using System;

namespace Oxy.Framework.Events
{
  public class FontSizeChangedEventArgs : EventArgs
  {
    public float NewSize { get; set; }
  }
}