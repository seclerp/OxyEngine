using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Oxy.Framework.Mappings
{
  /// <summary>
  ///   Class for mapping string to OpenTK Key enum
  /// </summary>
  public class InputMap
  {
    #region Key map

    private Dictionary<string, Keys> _keyMap => new Dictionary<string, Keys>
    {
      {"lshift", Keys.LeftShift},
      {"rshift", Keys.RightShift},
      {"lcontrol", Keys.LeftControl},
      {"rcontrol", Keys.RightControl},
      {"lalt", Keys.LeftAlt},
      {"ralt", Keys.RightAlt},
      {"lwin", Keys.LeftWindows},
      {"rwin", Keys.RightWindows},

      {"f1", Keys.F1},
      {"f2", Keys.F2},
      {"f3", Keys.F3},
      {"f4", Keys.F4},
      {"f5", Keys.F5},
      {"f6", Keys.F6},
      {"f7", Keys.F7},
      {"f8", Keys.F8},
      {"f9", Keys.F9},
      {"f10", Keys.F10},
      {"f11", Keys.F11},
      {"f12", Keys.F12},
      {"f13", Keys.F13},
      {"f14", Keys.F14},
      {"f15", Keys.F15},
      {"f16", Keys.F16},
      {"f17", Keys.F17},
      {"f18", Keys.F18},
      {"f19", Keys.F19},
      {"f20", Keys.F20},
      {"f21", Keys.F21},
      {"f22", Keys.F22},
      {"f23", Keys.F23},
      {"f24", Keys.F24},

      {"up", Keys.Up},
      {"down", Keys.Down},
      {"left", Keys.Left},
      {"right", Keys.Right},
      {"enter", Keys.Enter},
      {"escape", Keys.Escape},
      {"space", Keys.Space},
      {"tab", Keys.Tab},
      {"backspace", Keys.Back},
      {"insert", Keys.Insert},
      {"pageup", Keys.PageUp},
      {"pagedown", Keys.PageDown},
      {"home", Keys.Home},
      {"end", Keys.End},
      {"capslock", Keys.CapsLock},
      {"scrolllock", Keys.Scroll},
      {"printscreen", Keys.PrintScreen},
      {"pause", Keys.CapsLock},
      {"numlock", Keys.CapsLock},
      {"clear", Keys.CapsLock},
      {"sleep", Keys.CapsLock},
      {"a", Keys.A},
      {"b", Keys.B},
      {"c", Keys.C},
      {"d", Keys.D},
      {"e", Keys.E},
      {"f", Keys.F},
      {"g", Keys.G},
      {"h", Keys.H},
      {"i", Keys.I},
      {"j", Keys.J},
      {"k", Keys.K},
      {"l", Keys.L},
      {"m", Keys.M},
      {"n", Keys.N},
      {"o", Keys.O},
      {"p", Keys.P},
      {"q", Keys.Q},
      {"r", Keys.R},
      {"s", Keys.S},
      {"t", Keys.T},
      {"u", Keys.U},
      {"v", Keys.V},
      {"w", Keys.W},
      {"x", Keys.X},
      {"y", Keys.Y},
      {"z", Keys.Z},
      {"0", Keys.NumPad0},
      {"1", Keys.NumPad1},
      {"2", Keys.NumPad2},
      {"3", Keys.NumPad3},
      {"4", Keys.NumPad4},
      {"5", Keys.NumPad5},
      {"6", Keys.NumPad6},
      {"7", Keys.NumPad7},
      {"8", Keys.NumPad8},
      {"9", Keys.NumPad9},
      {"~", Keys.OemTilde},
      {"-", Keys.OemMinus},
      {"+", Keys.OemPlus},
      {"[", Keys.OemOpenBrackets},
      {"]", Keys.OemCloseBrackets},
      {";", Keys.OemSemicolon},
      {"'", Keys.OemQuotes},
      {",", Keys.OemComma},
      {".", Keys.OemPeriod},
      {"/", Keys.Divide},
      {@"\", Keys.OemBackslash}
    };

    public Dictionary<string, Keys> KeyMap => _keyMap;

    #endregion
    
    public bool CheckMouseButton(MouseState mouseState, string mouseButton)
    {
      switch (mouseButton)
      {
        case "left":
          return mouseState.LeftButton == ButtonState.Pressed;
        case "right":
          return mouseState.RightButton == ButtonState.Pressed;
        case "middle":
          return mouseState.MiddleButton == ButtonState.Pressed;
        case "xbutton1":
          return mouseState.XButton1 == ButtonState.Pressed;
        case "xbutton2":
          return mouseState.XButton2 == ButtonState.Pressed;
        default:
          throw new Exception($"Unknown mouse button: '{mouseButton}'");
      }
    }
    
    public bool CheckGamePadButton(GamePadState gamePadState, string gamePadButton)
    {
      switch (gamePadButton)
      {
        case "a":
          return gamePadState.Buttons.A == ButtonState.Pressed;
        case "x":
          return gamePadState.Buttons.X == ButtonState.Pressed;
        case "y":
          return gamePadState.Buttons.Y == ButtonState.Pressed;
        case "b":
          return gamePadState.Buttons.B == ButtonState.Pressed;
        case "leftstick":
          return gamePadState.Buttons.LeftStick == ButtonState.Pressed;
        case "rightstick":
          return gamePadState.Buttons.RightStick == ButtonState.Pressed;
        case "leftshoulder":
          return gamePadState.Buttons.LeftShoulder == ButtonState.Pressed;
        case "rightshoulder":
          return gamePadState.Buttons.RightShoulder == ButtonState.Pressed;
        case "dpadleft":
          return gamePadState.DPad.Left == ButtonState.Pressed;
        case "dpadup":
          return gamePadState.DPad.Up == ButtonState.Pressed;
        case "dpadright":
          return gamePadState.DPad.Right == ButtonState.Pressed;
        case "dpaddown":
          return gamePadState.DPad.Down == ButtonState.Pressed;
        default:
          throw new Exception($"Unknown gamepad button: '{gamePadButton}'");
      }
    }

    public float CheckGamePadTrigger(GamePadState gamePadState, string trigger)
    {
      switch (trigger)
      {
        case "left":
          return gamePadState.Triggers.Left;
        case "right":
          return gamePadState.Triggers.Right;
        default:
          throw new Exception($"Unknown gamepad trigger: '{trigger}'");
      }
    }
  }
}