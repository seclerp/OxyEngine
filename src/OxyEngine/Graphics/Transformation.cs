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

    public Transformation() : this(Vector2.Zero, 0, Vector2.One)
    {
    }

    public Transformation(Vector2 position, float rotation, Vector2 scale)
    {
      Position = position;
      Rotation = rotation;
      Scale = scale;

      RecreateMatrix();
    }

    private void RecreateMatrix()
    {
      Matrix =
        Matrix.CreateScale(new Vector3(Scale, 0))
        * Matrix.CreateRotationZ(Rotation)
        * Matrix.CreateTranslation(new Vector3(Position, 0));
    }
    
    public void Translate(Vector2 value)
    {
      Position += value;
      RecreateMatrix();
    }

    public void Rotate(float value)
    {
      Rotation += value;
      RecreateMatrix();
    }

    public void Zoom(Vector2 value)
    {
      Scale *= value;
      RecreateMatrix();
    }
    
    public object Clone()
    {
      return new Transformation(Position, Rotation, Scale);
    }
  }
}