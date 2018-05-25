using System;
using Microsoft.Xna.Framework;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Behaviours;
using OxyEngine.Ecs.Components;
using OxyEngine.Ecs.Entities;
using OxyEngine.Resources;
using IUpdateable = OxyEngine.Ecs.Behaviours.IUpdateable;

namespace OxyEngine.Game
{
  public class TransformScene : TransformEntity, IInitializable, IUpdateable
  {
    private float _timer;
    private TransformEntity _child;
    
    public void Init()
    {
      var resources = Container.Instance.ResolveByName<ResourceManager>(InstanceName.ResourceManager);
      
      Transform.Position = new Vector2(200, 200);
      _child = new TransformEntity();
      _child.Transform.Position = new Vector2(400, 300);
      _child.Transform.Scale = new Vector2(.5f, .5f);
      
      var sprite = _child.AddComponent<SpriteComponent>();
      
      sprite.Texture = resources.LoadTexture("example");
      sprite.SourceRectangle = new Rectangle(0, 0, sprite.Texture.Width, sprite.Texture.Height);
      sprite.Offset = new Vector2(sprite.Texture.Width / 2f, sprite.Texture.Height / 2f);
      
      AddChild(_child);
    }

    public void Update(float dt)
    {
      _timer += dt;

      _child.Transform.Rotation = (float) Math.Sin(_timer);
    }
  }
}