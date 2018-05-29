using System.Collections.Generic;
using System.Linq;

namespace OxyEngine.GUI.Styles
{
  public class Style
  {
    private Dictionary<string, object> _styles;
    private Style _parentStyle;
    private Dictionary<string, Style> _states;
    
    public Style()
    {
      _styles = new Dictionary<string, object>();
      _states = new Dictionary<string, Style>();
    }

    public Style SetRule(string key, object value)
    {
      _styles[key] = value;
      
      return this;
    }

    public Style SetState(string stateName, Style stateStyle)
    {
      _states[stateName] = Merge(this, stateStyle);

      return this;
    }
    
    public Style GetState(string stateName)
    {
      if (!_states.ContainsKey(stateName))
      {
        // Return normal state - this
        return this;
      }

      return Merge(this, _states[stateName]);
    }

    public T GetRule<T>(string key)
    {
      var success = TryGetRule<T>(key, out var result);

      if (success)
      {
        return result;
      }

      if (!(_parentStyle is null))
      {
        return _parentStyle.GetRule<T>(key);
      }

      return default;
    }

    public bool TryGetRule<T>(string key, out T value)
    {
      var success = _styles.TryGetValue(key, out var innerValue);
      if (success)
      {
        value = (T) innerValue;
        return true;
      }
      if (_parentStyle is null)
      {
        value = default;
        return false;
      }
      
      success = _parentStyle.TryGetRule(key, out value);
      
      return success;
    }

    public void SetParent(Style parentStyle)
    {
      _parentStyle = parentStyle;
    }
    
    public static Style Merge(Style first, Style second)
    {
      var mergeResult = first._styles.Concat(second._styles)
        .GroupBy(kvp => kvp.Key, kvp => kvp.Value)
        .ToDictionary(g => g.Key, g => g.Last());

      return new Style
      {
        _styles = mergeResult,
        _parentStyle = second._parentStyle ?? first._parentStyle
      };
    }
  }
}