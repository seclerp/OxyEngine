namespace OxyEngine.Ecs.Behaviours
{
  /// <summary>
  ///   Behaviour interface for every object that can be called before update
  /// </summary>
  public interface IBeforeUpdateable
  {
    /// <summary>
    ///   BeforeUpdate handler for IBeforeUpdatable
    /// </summary>
    void BeforeUpdate();
  }
}