using System.Collections.Generic;
using System.Reflection;
using OxyEngine.UI.Widgets;

namespace OxyEngine.UI.DataBinders
{
  // TODO: Implement
  public class WidgetDataBinder
  {
    private Dictionary<string, PropertyInfo> _properties;
    
    private Widget _widget;
    
    public WidgetDataBinder(Widget widget)
    {
      _widget = widget;
      _properties = new Dictionary<string, PropertyInfo>();
      InitializePropertiesCache();
    }

    public virtual void BindRead()
    {
    }
    
    public virtual void BindWrite()
    {
    }
    
    public virtual void BindReadWrite()
    {
    }

    private void InitializePropertiesCache()
    {
      // Cache all properties to quick access
      foreach (var propertyInfo in _widget.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
      {
        _properties[propertyInfo.Name] = propertyInfo;
      }
    }
  }
}