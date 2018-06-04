namespace OxyEngine.Ecs.Behaviours
{
  /// <summary>
  ///   Behaviour interface for every object that can be called after update
  /// </summary>
  public interface IAfterUpdateable
  {
    /// <summary>
    ///   AfterUpdate handler for IAfterUpdatable
    /// </summary>
    void AfterUpdate();
  }
}