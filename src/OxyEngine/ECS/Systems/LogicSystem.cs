using OxyEngine.ECS.Behaviours;
using OxyEngine.ECS.Entities;

namespace OxyEngine.ECS.Systems
{
  public class LogicSystem : BaseGameSystem, IUpdateable
  {
    public LogicSystem(BaseGameEntity rootEntity) : base(rootEntity)
    {
    }
    
    public void Update(float dt)
    {
      
    }
  }
}