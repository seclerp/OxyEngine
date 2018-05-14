using System;
using System.Collections.Generic;
using OxyEngine.UI.Nodes;

namespace OxyEngine.UI.Models
{
  public abstract class ContainerWidgetModel : WidgetModel
  {
    private readonly IList<WidgetModel> _children;
    public IEnumerable<WidgetModel> Children => _children;

    protected ContainerWidgetModel(WidgetNode node) : base(node)
    {
      _children = new List<WidgetModel>();
    }

    public void Add(WidgetModel newChild)
    {
      if (_children.Contains(newChild))
      {
        // Ignore
        return;
      }
      
      _children.Add(newChild);
    }
    
    public void Remove(WidgetModel newChild)
    {
      if (!_children.Contains(newChild))
      {
        throw new Exception($"{newChild} is not a child of this widget");
      }
      
      _children.Add(newChild);
    }

  }
}