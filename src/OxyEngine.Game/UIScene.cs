using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Entities;
using OxyEngine.Resources;
using OxyEngine.UI;
using OxyEngine.UI.Enums;
using OxyEngine.UI.Models;
using OxyEngine.Window;
using IDrawable = OxyEngine.Ecs.Behaviours.IDrawable;

namespace OxyEngine.Game
{
  public class UIScene : TransformEntity, IInitializable, IDrawable
  {
    private Canvas _canvas;
    private WindowManager _windowManager;
    private ResourceManager _resourceManager;
    private SpriteFont _font;
    
    private TextModel _textModel;
    
    public void Init()
    {
      _windowManager = Container.Instance.ResolveByName<WindowManager>(InstanceName.WindowManager);
      _resourceManager = Container.Instance.ResolveByName<ResourceManager>(InstanceName.ResourceManager);

      _textModel = new TextModel
      {
        Rect = new Rectangle(0, 0, 500, 40),
        Text = "нееш подумооооб.....",
        Font = _resourceManager.LoadFont("Roboto-Regular"),
        TextColor = Color.White,
        BackgroundColor = Color.DarkGray
      };

      _canvas = new Canvas(50, 50, 300, 300); //_windowManager.GetWidth(), _windowManager.GetHeight());
    }
    
    public void Draw()
    {
      _canvas.Draw(rootRederer =>
      {
        rootRederer.FreeLayout(new Rectangle(50, 50, 200, 200), graphicsRenderer =>
        {
          graphicsRenderer.Text(_textModel.Rect, _textModel.Font, _textModel.Text, _textModel.TextColor, _textModel.BackgroundColor, HorizontalAlignment.Left);
          graphicsRenderer.Text(
            new Rectangle(_textModel.Rect.X, _textModel.Rect.Y + _textModel.Rect.Height + 50, _textModel.Rect.Width, _textModel.Rect.Height),
            _textModel.Font, _textModel.Text, _textModel.TextColor, _textModel.BackgroundColor, HorizontalAlignment.Center);
          graphicsRenderer.Text(
            new Rectangle(_textModel.Rect.X, _textModel.Rect.Y + _textModel.Rect.Height * 2 + 100, _textModel.Rect.Width, _textModel.Rect.Height),
            _textModel.Font, _textModel.Text, _textModel.TextColor, _textModel.BackgroundColor, HorizontalAlignment.Right);
          graphicsRenderer.Text(
            new Rectangle(_textModel.Rect.X, _textModel.Rect.Y + _textModel.Rect.Height * 2 + 200, 50, _textModel.Rect.Height),
            _textModel.Font, _textModel.Text, _textModel.TextColor, _textModel.BackgroundColor, HorizontalAlignment.Left);
        });
      });
    }
  }
}