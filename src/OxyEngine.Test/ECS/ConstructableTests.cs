using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using NUnit.Framework;
using OxyEngine.Dependency;
using OxyEngine.Ecs.Components;
using OxyEngine.Ecs.Entities;
using OxyEngine.Interfaces;

namespace OxyEngine.Test.ECS
{
  [TestFixture]
  public class ConstructableTests
  {
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
//    [Ignore("Need to be fixed explicitly")]
    public void Entity_AddGenericGetComponent()
    {
      var entity = new TransformEntity();
      var component = entity.AddComponent<SpriteComponent>();
      
      var componentFromGet = entity.GetComponent<SpriteComponent>();
      
      Assert.AreEqual(component, componentFromGet);
    }
    
    [Test]
//    [Ignore("Need to be fixed explicitly")]
    public void Entity_AddGetComponent()
    {
      var entity = new TransformEntity();
      var component = new SpriteComponent();
      
      entity.AddComponent(component);
      var componentFromGet = entity.GetComponent<SpriteComponent>();
      
      Assert.AreEqual(component, componentFromGet);
    }
    
    [Test]
//    [Ignore("Need to be fixed explicitly")]
    public void Entity_AddGenericRemoveCompoent()
    {
      var entity = new TransformEntity();
      
      var component = entity.AddComponent<SpriteComponent>();
      var componentEntity = component.Entity;
      
      var oldCount = entity.Components.Count();
      var removeSuccess = entity.RemoveComponent<SpriteComponent>();
      var newCount = entity.Components.Count();
      
      Assert.That(removeSuccess);
      Assert.That(oldCount == 2);
      Assert.That(newCount == 1);
      Assert.AreEqual(componentEntity, entity);
      Assert.IsNull(component.Entity);
    }
    
    [Test]
//    [Ignore("Need to be fixed explicitly")]
    public void Entity_AddRemoveComponent()
    {
      var entity = new TransformEntity();
      
      var component = new SpriteComponent();
      entity.AddComponent(component);
      var componentEntity = component.Entity;
      
      var oldCount = entity.Components.Count();
      var removeSuccess = entity.RemoveComponent<SpriteComponent>();
      var newCount = entity.Components.Count();
      
      Assert.That(removeSuccess);
      Assert.That(oldCount == 2);
      Assert.That(newCount == 1);
      Assert.AreEqual(componentEntity, entity);
      Assert.IsNull(component.Entity);
    }
    
    [Test]
//    [Ignore("Need to be fixed explicitly")]
    public void Entity_GetComponents()
    {
      var entity = new TransformEntity();
      var listOfComponents = new List<GameComponent> { new SpriteComponent(), new SpriteComponent() };
      
      entity.AddComponent(listOfComponents[0]);
      entity.AddComponent(listOfComponents[1]);

      var getListOfComponents = entity.GetComponents<SpriteComponent>();
      
      CollectionAssert.AreEqual(listOfComponents, getListOfComponents);
    }
  }
}