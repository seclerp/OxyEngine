using OpenTK.Input;
using Oxy.Framework.Mappings;

namespace Oxy.Framework
{
  /// <summary>
  ///   Module for managing user input
  /// </summary>
  public class Input
  {
    private static readonly InputMap _inputMap;

    static Input()
    {
      _inputMap = new InputMap();
    }

    /// <summary>
    ///   Return true if specified keyboard key is pressed, otherwise false
    /// </summary>
    /// <param name="key">Keyboard key to check</param>
    public static bool IsKeyPressed(string key)
    {
      return Keyboard.GetState()[_inputMap.KeyMap[key]];
    }

    /// <summary>
    ///   Return true if specified keyboard key is pressed, otherwise false
    /// </summary>
    /// <param name="button">Mouse button to check</param>
    public static bool IsMousePressed(string button)
    {
      return Mouse.GetState()[_inputMap.MouseMap[button]];
    }
  }
}