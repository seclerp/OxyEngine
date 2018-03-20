using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Oxy.Framework.Mappings;

namespace Oxy.Framework
{
  /// <summary>
  ///   Module for managing user input
  /// </summary>
  public class Input
  {
    private KeyboardState _keyboardState;
    private MouseState _mouseState;
    private GamePadState _gamePadState;
    private InputMap _map;

    public Input()
    {
      _map = new InputMap();
    }
    
    public void UpdateInputState(KeyboardState keyboardState, MouseState mouseState, GamePadState gamePadState)
    {
      _keyboardState = keyboardState;
      _mouseState = mouseState;
      _gamePadState = gamePadState;
    }
    
    /// <summary>
    ///   Return true if specified keyboard key is pressed, otherwise false
    /// </summary>
    /// <param name="key">Keyboard key to check</param>
    public bool IsKeyPressed(string key)
    {
      if (!_map.KeyMap.ContainsKey(key))
        throw new Exception($"Unknown keyboard key '{key}'");
      
      return _keyboardState.IsKeyDown(_map.KeyMap[key]);
    }
    
    /// <summary>
    ///   Return true if specified gamepad button is pressed, otherwise false
    /// </summary>
    /// <param name="button">Gamepad button to check</param>
    public bool IsGamePadButtonPressed(string button)
    {
      return _map.CheckGamePadButton(_gamePadState, button);
    }
    
    /// <summary>
    ///   Return value of specified gamepad trigger
    /// </summary>
    /// <param name="trigger">Gamepad trigger to check</param>
    public float GetGamePadTrigger(string trigger)
    {
      return _map.CheckGamePadTrigger(_gamePadState, trigger);
    }

    /// <summary>
    ///   Return true if specified keyboard key is pressed, otherwise false
    /// </summary>
    /// <param name="button">Mouse button to check</param>
    public bool IsMousePressed(string button)
    {
      return _map.CheckMouseButton(_mouseState, button);
    }

    /// <summary>
    ///   Return cursor position relative to window
    /// </summary>
    /// <returns>Vector2 with cursor position relative to window</returns>
    public Vector2 GetCursorPosition()
    {
      return new Vector2(_mouseState.X, _mouseState.Y);
    }

    /// <summary>
    ///   Return mouse wheel state 
    /// </summary>
    /// <returns>Mouse wheel state</returns>
    public int GetMouseWheel()
    {
      return _mouseState.ScrollWheelValue;
    }
  }
}