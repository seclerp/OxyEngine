using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OxyEngine.ECS;
using OxyEngine.ECS.Components;
using OxyEngine.ECS.Entities;

namespace OxyEngine.Test.ECS
{
  [TestFixture]
  public class Constructable
  {
    [Test]
    [Ignore("Need to be fixed explicitly")]
    public void Entity_AddGenericGetComponent()
    {
      var entity = new GameEntity();
      var component = entity.AddComponent<TransformComponent>();
      
      var componentFromGet = entity.GetComponent<TransformComponent>();
      
      Assert.AreEqual(component, componentFromGet);
    }
    
    [Test]
    [Ignore("Need to be fixed explicitly")]
    public void Entity_AddGetComponent()
    {
      var entity = new GameEntity();
      var component = new TransformComponent(null);
      
      entity.AddComponent(component);
      var componentFromGet = entity.GetComponent<TransformComponent>();
      
      Assert.AreEqual(component, componentFromGet);
    }
    
    [Test]
    [Ignore("Need to be fixed explicitly")]
    public void Entity_AddGenericRemoveCompoent()
    {
      var entity = new GameEntity();
      
      var component = entity.AddComponent<TransformComponent>();
      var componentEntity = component.Entity;
      
      var oldCount = entity.Components.Count();
      var removeSuccess = entity.RemoveComponent<TransformComponent>();
      var newCount = entity.Components.Count();
      
      Assert.That(removeSuccess);
      Assert.That(oldCount == 1);
      Assert.That(newCount == 0);
      Assert.AreEqual(componentEntity, entity);
      Assert.IsNull(component.Entity);
    }
    
    [Test]
    [Ignore("Need to be fixed explicitly")]
    public void Entity_AddRemoveComponent()
    {
      var entity = new GameEntity();
      
      var component = new TransformComponent(null);
      entity.AddComponent(component);
      var componentEntity = component.Entity;
      
      var oldCount = entity.Components.Count();
      var removeSuccess = entity.RemoveComponent<TransformComponent>();
      var newCount = entity.Components.Count();
      
      Assert.That(removeSuccess);
      Assert.That(oldCount == 1);
      Assert.That(newCount == 0);
      Assert.AreEqual(componentEntity, entity);
      Assert.IsNull(component.Entity);
    }
    
    [Test]
    [Ignore("Need to be fixed explicitly")]
    public void Entity_GetComponents()
    {
      var entity = new GameEntity();
      var listOfComponents = new List<BaseGameComponent> { new TransformComponent(null), new TransformComponent(null) };
      
      entity.AddComponent(listOfComponents[0]);
      entity.AddComponent(listOfComponents[1]);

      var getListOfComponents = entity.GetComponents<TransformComponent>();
      
      CollectionAssert.AreEqual(listOfComponents, getListOfComponents);
    }
  }
}