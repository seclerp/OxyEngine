using System;
using System.Collections.Generic;
using OxyEngine.Events;

namespace OxyEngine.PureECS
{
  public class SystemManager
  {
    private readonly List<Entity> _entities;
    private readonly List<Component> _components;
    private readonly List<System> _systems;
    private readonly IEventBus _managerEмentBus;
    private readonly IEventBus _gameLoopEventBus;

    public SystemManager(IEventBus gameLoopEventBus)
    {
      _gameLoopEventBus = gameLoopEventBus;
      
      _entities = new List<Entity>();
      _components = new List<Component>();
      _systems = new List<System>();
      _managerEмentBus = new QueuedEventBus();

      SubscribeOnGameloopEvents();
    }

    private void SubscribeOnGameloopEvents()
    {
      _gameLoopEventBus.Subscribe(EventNames.Initialization.OnInit, OnInit);
      _gameLoopEventBus.Subscribe(EventNames.Initialization.OnLoad, OnLoad);
      _gameLoopEventBus.Subscribe(EventNames.Initialization.OnUnload, OnUnload);
      _gameLoopEventBus.Subscribe(EventNames.Gameloop.Update.OnUpdate, OnUpdate);
      _gameLoopEventBus.Subscribe(EventNames.Gameloop.Draw.OnDraw, OnDraw);
    }

    private void OnInit(object payload) => _managerEмentBus.Send(EventNames.Initialization.OnInit, payload);
    private void OnLoad(object payload) => _managerEмentBus.Send(EventNames.Initialization.OnInit, payload);
    private void OnUnload(object payload) => _managerEмentBus.Send(EventNames.Initialization.OnInit, payload);
    private void OnUpdate(object payload) => _managerEмentBus.Send(EventNames.Initialization.OnInit, payload);
    private void OnDraw(object payload) => _managerEмentBus.Send(EventNames.Initialization.OnInit, payload);
    
    public void AddSystem(System system)
    {
      if (system is null)
      {
        throw new NullReferenceException(nameof(system));
      }
      
      _systems.Add(system);
    }
  }
}