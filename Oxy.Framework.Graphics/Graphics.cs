using System;

namespace Oxy.Framework
{
  /// <summary>
  /// Class that used for rendering all things in engine
  /// </summary>
  public class Graphics
  {
    private static Lazy<Graphics> _instance;
    
    // Closed constructor
    private Graphics()
    {}

    static Graphics()
    {
      _instance = new Lazy<Graphics>(() => new Graphics());
    }
    
    #region Static methods
    
    // Methods goes here
    
    #endregion
  }
}