using Microsoft.Xna.Framework;

namespace OxyEngine.Ecs.Interfaces
{
  public interface ITransformable
  {
    Vector2 Position { get; set; }
    float Rotation { get; set; }
    Vector2 Scale { get; set; }
    Matrix Matrix { get; }
    
    Vector2 GlobalPosition { get; set; }
    float GlobalRotation { get; set; }
    Vector2 GlobalScale { get; set; }
    Matrix GlobalMatrix { get; }
    
    void Translate(float x, float y);
    void Translate(Vector2 vector);
    
    void Rotate(float angle);
    
    void Zoom(float x, float y);
    void Zoom(Vector2 vector);
  }
}