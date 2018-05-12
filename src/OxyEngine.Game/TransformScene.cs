using System;
using Microsoft.Xna.Framework;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Components;
using OxyEngine.Ecs.Entities;
using IUpdateable = OxyEngine.Ecs.Behaviours.IUpdateable;

namespace OxyEngine.Game
{
  public class TransformScene : TransformEntity, IInitializable, IUpdateable
  {
    private Vector2 _scale;
    private float Timer;
    private TransformEntity _child;
    
    public void Init()
    {
      Transform.Position = new Vector2(200, 200);
      _child = new TransformEntity();
      _child.Transform.Position = new Vector2(400, 300);
      _child.Transform.Scale = new Vector2(0.5f, 0.5f);
      
      var sprite = _child.AddComponent<SpriteComponent>();
      
      sprite.Texture = GetApiManager().Resources.LoadTexture("example");
      sprite.SourceRectangle = new Rectangle(0, 0, sprite.Texture.Width, sprite.Texture.Height);
      sprite.Offset = new Vector2(sprite.Texture.Width / 2, sprite.Texture.Height / 2);
      
      AddChild(_child);
    }

    public void Update(float dt)
    {
      Timer += dt;

      _child.Transform.Rotation = (float) Math.Sin(Timer);
    }
  }
}