using System;
using System.Collections.Generic;
using System.Reflection;
using OxyEngine.UI.Models;
using OxyEngine.UI.Nodes;

namespace OxyEngine.UI.DataBinders
{
  // DataBinder - reflect changes in model into view and vice-versa
  public class WidgetDataBinder : WidgetPart
  {
    private Dictionary<string, PropertyInfo> _properties;
    
    private WidgetModel _widgetModel;
    
    public WidgetDataBinder(WidgetNode node)
    {
      Node = node;
      
      _widgetModel = Node.Model;
      _properties = new Dictionary<string, PropertyInfo>();
      InitializePropertiesCache();
    }

    public virtual void BindRead()
    {
      throw new NotImplementedException();
    }
    
    public virtual void BindWrite()
    {
      throw new NotImplementedException();
    }
    
    public virtual void BindReadWrite()
    {
      throw new NotImplementedException();
    }

    private void InitializePropertiesCache()
    {
      // Cache all properties to quick access
      foreach (var propertyInfo in _widgetModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
      {
        _properties[propertyInfo.Name] = propertyInfo;
      }
    }
  }
}