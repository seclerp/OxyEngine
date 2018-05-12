using Microsoft.Xna.Framework;

namespace OxyEngine.Ecs.Interfaces
{
  public interface ITransformable
  {
    /// <summary>
    ///   Local position of an object
    /// </summary>
    Vector2 Position { get; set; }
    
    /// <summary>
    ///   Local rotation of an object
    /// </summary>
    float Rotation { get; set; }
    
    /// <summary>
    ///   Local scaling of an object
    /// </summary>
    Vector2 Scale { get; set; }
    
    /// <summary>
    ///   Local transformation matrix of an object 
    /// </summary>
    Matrix Matrix { get; }
    
    
    /// <summary>
    ///   Global position of an object
    /// </summary>
    Vector2 GlobalPosition { get; set; }
    
    /// <summary>
    ///   Global rotation of an object
    /// </summary>
    float GlobalRotation { get; set; }
    
    /// <summary>
    ///   Global scaling of an object
    /// </summary>
    Vector2 GlobalScale { get; set; }
    
    /// <summary>
    ///   Global transformation matrix of an object 
    /// </summary>
    Matrix GlobalMatrix { get; }
    
    
    /// <summary>
    ///   Moves object by given coordinates 
    /// </summary>
    void Translate(float x, float y);
    
    /// <summary>
    ///   Moves object by given vector 
    /// </summary>
    void Translate(Vector2 vector);
    
    
    /// <summary>
    ///   Rotates object by given angle 
    /// </summary>
    void Rotate(float angle);
    
    
    /// <summary>
    ///   Scales object by given scale factors 
    /// </summary>
    void Zoom(float x, float y);
    
    /// <summary>
    ///   Scales object by given scale vector  
    /// </summary>
    void Zoom(Vector2 vector);
  }
}