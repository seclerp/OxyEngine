using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace OxyEngine.GUI
{
  /// <summary>
  ///   Special stack for area bounding rectangles
  ///   Used for croping UI child widget
  /// </summary>
  public class AreaStack
  {
    private Stack<Rectangle> _stack;

    public AreaStack()
    {
      _stack = new Stack<Rectangle>();
    }

    public void Push(Rectangle areaRectangle, bool crop = true)
    {
      if (_stack.Count == 0)
      {
        _stack.Push(areaRectangle);
        return;
      }

      var prev = _stack.Peek();
      
      if (!crop)
      {
        _stack.Push(new Rectangle(areaRectangle.X + prev.X, areaRectangle.Y + prev.Y, areaRectangle.Width, areaRectangle.Height));
        return;
      }

      var cropped = Merge(prev, areaRectangle);
      _stack.Push(cropped);
    }

    public Rectangle Peek() => _stack.Peek();
    public Rectangle Pop() => _stack.Pop();
    public void Clear() => _stack.Clear();
    
    private Rectangle Merge(Rectangle parent, Rectangle child)
    {
      var x1 = Math.Max(parent.X, parent.X + child.X);
      var y1 = Math.Max(parent.Y, parent.Y + child.Y);

      var x2 = Math.Min(parent.X + parent.Width, parent.X + child.X + child.Width);
      var y2 = Math.Min(parent.Y + parent.Height, parent.Y + child.Y + child.Height);
      
      var width = x2 - x1;
      var height = y2 - y1;

      if (width < 0 || height < 0)
      {
        return Rectangle.Empty;
      }
      
      return new Rectangle(x1, y1, width, height);
    }
  }
}