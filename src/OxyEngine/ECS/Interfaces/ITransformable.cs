using Microsoft.Xna.Framework;

namespace OxyEngine.ECS.Iterfaces
{
  public interface ITransformable
  {
    Vector2 Position { get; }
    float Rotation { get; }
    Vector2 Scale { get; }

    void Translate(float x, float y);
    void Translate(Vector2 vector);
    
    void Rotate(float angle);
    
    void Zoom(float x, float y);
    void Zoom(Vector2 vector);
  }
}