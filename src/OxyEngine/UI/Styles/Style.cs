using System.Collections.Generic;
using System.Linq;

namespace OxyEngine.UI.Styles
{
  public class Style
  {
    private Dictionary<string, object> _styles;
    
    public Style()
    {
      _styles = new Dictionary<string, object>();
    }

    public Style SetRule(string key, object value)
    {
      _styles[key] = value;
      
      return this;
    }

    public T GetRule<T>(string key)
    {
      return TryGet<T>(key, out var result) ? result : default;
    }

    public bool TryGet<T>(string key, out T value)
    {
      var result = _styles.TryGetValue(key, out var innerValue);
      value = (T) innerValue;
      
      return result;
    }
    
    public static Style Merge(Style major, Style minor)
    {
      var mergeResult = major._styles.Union(minor._styles).ToDictionary(pair => pair.Key, pair => pair.Value);

      return new Style
      {
        _styles = mergeResult
      };
    }
  }
}