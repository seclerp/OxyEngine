using System;
using Microsoft.Xna.Framework;

namespace OxyEngine.Graphics
{
  public class Transformation : ICloneable
  {
    public Vector2 Position { get; private set; }
    public float Rotation { get; private set; }
    public Vector2 Scale { get; private set; }

    public Matrix Matrix { get; private set; }

    public Transformation()
    {
      Matrix = Matrix.Identity;
    }

    public Transformation(Vector2 position, float rotation, Vector2 scale)
    {
      Position = position;
      Rotation = rotation;
      Scale = scale;

      Matrix = Matrix.CreateTranslation(new Vector3(position, 0))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateScale(new Vector3(scale, 0));
    }

    public void Translate(Vector2 value)
    {
      Matrix *= Matrix.CreateTranslation(new Vector3(value, 0));
      Position += value;
    }

    public void Rotate(float value)
    {
      Matrix *= Matrix.CreateRotationZ(value);
      Rotation += value;
    }

    public void Zoom(Vector2 value)
    {
      Matrix *= Matrix.CreateScale(new Vector3(value, 0));
      Scale += value;
    }
    
    public object Clone()
    {
      return new Transformation(Position, Rotation, Scale);
    }
  }
}