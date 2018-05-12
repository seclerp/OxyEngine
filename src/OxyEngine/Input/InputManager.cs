using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using OxyEngine.Events;
using OxyEngine.Events.Args;
using OxyEngine.Interfaces;
using OxyEngine.Mapping;

namespace OxyEngine.Input
{
  /// <summary>
  ///   Module for managing user input
  /// </summary>
  public class InputManager : IModule
  {
    private readonly InputMap _map;
    
    private KeyboardState _keyboardState;
    private MouseState _mouseState;
    private GamePadState[] _gamePadStates;

    private GameInstance _gameInstance;
    
    private bool _anyGamePadConnected;

    public InputManager(GameInstance gameInstance)
    {
      _map = new InputMap();
      _anyGamePadConnected = false;
      _gameInstance = gameInstance;
      
      // TODO: Make listeners binding using attributes (gameInstance.Events.Global.AddListenersFromAttributes();)
      _gameInstance.Events.Global.StartListening(EventNames.Gameloop.Update.OnBeginUpdate, OnBeginUpdate);
    }
    
    private void OnBeginUpdate(object sender, EngineEventArgs args)
    {
      UpdateAllGamepadStates();
      UpdateInputState(Keyboard.GetState(), Mouse.GetState(), _gamePadStates);
      
      // TODO: Make configurable
      // Handle Alt+F4
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
          Keyboard.GetState().IsKeyDown(Keys.Escape))
      {
        _gameInstance.Exit();
      }
    }

    private void UpdateAllGamepadStates()
    {
      var count = GamePad.MaximumGamePadCount;

      if (_gamePadStates == null)
        _gamePadStates = new GamePadState[count];
      
      for (int i = 0; i < count; i++)
      {
        _gamePadStates[i] = GamePad.GetState(i);
      }
    }
    
    public void UpdateInputState(KeyboardState keyboardState, MouseState mouseState, GamePadState[] gamePadStates)
    {
      _keyboardState = keyboardState;
      _mouseState = mouseState;
      _gamePadStates = gamePadStates;
      _anyGamePadConnected = gamePadStates != null && gamePadStates.Length > 0;
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
    /// <param name="gamePad">Gamepad ID</param>
    /// <param name="button">Gamepad button to check</param>
    public bool IsGamePadButtonPressed(int gamePad, string button)
    {
      CheckGamepadConnected(gamePad);
      return _map.CheckGamePadButton(_gamePadStates[gamePad], button);
    }

    /// <summary>
    ///   Return value of specified gamepad trigger
    /// </summary>
    /// <param name="gamePad">Gamepad ID</param>
    /// <param name="trigger">Gamepad trigger to check</param>
    public float GetGamePadTrigger(int gamePad, string trigger)
    {
      CheckGamepadConnected(gamePad);
      return _map.CheckGamePadTrigger(_gamePadStates[gamePad], trigger);
    }
    
    /// <summary>
    ///   Return value of specified gamepad stick
    /// </summary>
    /// <param name="gamePad">Gamepad ID</param>
    /// <param name="stick">Gamepad stick to check</param>
    public Vector2 GetGamePadStick(int gamePad, string stick)
    {
      CheckGamepadConnected(gamePad);
      return _map.CheckGamePadStick(_gamePadStates[gamePad], stick);
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

    private void CheckGamepadConnected(int gamePad)
    {
      if (!_anyGamePadConnected)
      {
        throw new Exception("Attempt to get input from gamepad, but no gamepads connected");
      }

      if (gamePad >= _gamePadStates.Length)
      {
        throw new Exception($"Attempt to get input from gamepad #{gamePad}, but there are only {_gamePadStates.Length} connected");
      }
    }
  }
}