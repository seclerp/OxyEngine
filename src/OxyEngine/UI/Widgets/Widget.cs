using OxyEngine.UI.DataBinders;
using OxyEngine.UI.Renderers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using OxyEngine.Annotations;

namespace OxyEngine.UI.Widgets
{
  public abstract class Widget : INotifyPropertyChanged
  {
    private WidgetRenderer _renderer;
    private WidgetDataBinder _dataBinder;

    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }
    // Size + Margin
    public Vector2 FullSize => new Vector2(Size.X + MarginLeft + MarginRight, Size.Y + MarginTop + MarginBottom);

    public float X => Position.X;
    public float Y => Position.Y;

    public float Width => Size.X;
    public float Height => Size.Y;
    
    public float MarginLeft { get; set; } = 0;
    public float MarginRight { get; set; } = 0;
    public float MarginTop { get; set; } = 0;
    public float MarginBottom { get; set; } = 0;

    public float Margin
    {
      set => MarginLeft = MarginRight = MarginTop = MarginBottom = value;
    }
    
    public float PaddingLeft { get; set; } = 0;
    public float PaddingRight { get; set; } = 0;
    public float PaddingTop { get; set; } = 0;
    public float PaddingBottom { get; set; } = 0;

    // Padding is inclided in Width and Height
    public float Padding
    {
      set => PaddingLeft = PaddingRight = PaddingTop = PaddingBottom = value;
    }

    public Rectangle ClientRectangle => new Rectangle(
      (int)(X + PaddingLeft + MarginLeft), 
      (int)(Y + PaddingTop + MarginTop), 
      (int)(X + Width - PaddingRight - MarginRight), 
      (int)(X + Height - PaddingBottom - MarginBottom)
    );

    public bool IsVisible { get; set; } = true;

    protected Widget(WidgetRenderer renderer, WidgetDataBinder dataBinder)
    {
      _renderer = renderer;
      _dataBinder = dataBinder;
    }

    public void Render()
    {
      if (IsVisible)
      {
        _renderer.Render();
      }
    }
    
    public void BindRead()
    {
      _dataBinder.BindRead();
    }
    
    public void BindWrite()
    {
      _dataBinder.BindWrite();
    }
    
    public void BindReadWrite()
    {
      _dataBinder.BindReadWrite();
    }

    #region INotifyPropertyChanged Implementation

    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    #endregion
  }
}