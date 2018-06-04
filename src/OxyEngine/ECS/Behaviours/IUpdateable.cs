namespace OxyEngine.Ecs.Behaviours
{
  /// <summary>
  ///   Behaviour interface for every object that can be updated
  /// </summary>
  public interface IUpdateable
  {
    /// <summary>
    ///   Update handler for IUpdatable
    /// </summary>
    void Update(float dt);
  }
}