using System;
using Microsoft.Xna.Framework;

namespace Oxy.Framework
{
  internal class Transformation : ICloneable
  {
    public Vector2 Position { get; private set; } = Vector2.Zero;
    public Vector2 Scale { get; private set; } = Vector2.One;
    public float Rotation { get; private set; } = 0;

    private Matrix _matrix;

    public Transformation(Vector2 position, float rotation, Vector2 scale)
    {
      TranslateMatrix(position);
      RotateMatrix(rotation);
      ScaleMatrix(scale);
    }
    
    public void TranslateMatrix(Vector2 position)
    {
      _matrix *= Matrix.CreateTranslation(new Vector3(position, 0));
      Position += position;
    }

    public void RotateMatrix(float rotation)
    {
      _matrix *= Matrix.CreateRotationZ(rotation);
      Rotation += rotation;
    }
    
    public void ScaleMatrix(Vector2 scale)
    {
      _matrix *= Matrix.CreateScale(new Vector3(scale, 0));
      Position += scale;
    }
    
    public Vector2 TransformVector(Vector2 point)
    {
      return Vector2.Transform(point, _matrix);
    }
    
    public static Matrix Compose(Matrix a, Matrix b)
    {
      return Matrix.Add(a, b);
    }

    public object Clone()
    {
      return new Transformation(Position, Rotation, Scale);
    }
  }
}