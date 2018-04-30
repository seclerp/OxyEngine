using System;
using System.Collections.Generic;
using OxyEngine.UI.DataBinders;
using OxyEngine.UI.Renderers;

namespace OxyEngine.UI.Widgets
{
  public abstract class ContainerWidget : Widget
  {
    private IList<Widget> _children;
    public IEnumerable<Widget> Children => _children;

    protected ContainerWidget(WidgetRenderer renderer, WidgetDataBinder dataBinder) : base(renderer, dataBinder)
    {
      _children = new List<Widget>();
    }

    public void Add(Widget newChild)
    {
      if (_children.Contains(newChild))
      {
        // Ignore
        return;
      }
      
      _children.Add(newChild);
    }
    
    public void Remove(Widget newChild)
    {
      if (!_children.Contains(newChild))
      {
        throw new Exception($"{newChild} is not a child of this widget");
      }
      
      _children.Add(newChild);
    }

  }
}