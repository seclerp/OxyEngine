using OpenTK.Input;
using Oxy.Framework.Mappings;

namespace Oxy.Framework
{
  /// <summary>
  ///   Module for managing user input
  /// </summary>
  public class Input : LazyModule<Input>
  {
    private readonly InputMap _inputMap;

    public Input()
    {
      _inputMap = new InputMap();
    }

    /// <summary>
    ///   Return true if specified keyboard key is pressed, otherwise false
    /// </summary>
    /// <param name="key">Keyboard key to check</param>
    public static bool IsKeyPressed(string key)
    {
      return Keyboard.GetState()[Instance.Value._inputMap.KeyMap[key]];
    }

    /// <summary>
    ///   Return true if specified keyboard key is pressed, otherwise false
    /// </summary>
    /// <param name="button">Mouse button to check</param>
    public static bool IsMousePressed(string button)
    {
      return Mouse.GetState()[Instance.Value._inputMap.MouseMap[button]];
    }
  }
}