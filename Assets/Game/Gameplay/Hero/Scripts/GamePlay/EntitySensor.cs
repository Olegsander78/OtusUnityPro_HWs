using System;
using System.Collections.Generic;
using Entities;
using Game.GameEngine.Mechanics;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("Gameplay/Hero/Hero Entity Sensor")]
public sealed class EntitySensor : MonoBehaviour,
    IGameConstructElement,
    //IGameInitElement,
    IGameReadyElement,
    IGameFinishElement
{
    public event Action OnEntitiesUpdated;

    [ReadOnly]
    [ShowInInspector]
    private readonly List<IEntity> detectedEntities;

    private Component_ColliderSensor heroComponent;

    public EntitySensor()
    {
        this.detectedEntities = new List<IEntity>();
    }

    public List<IEntity> GetDetectedEntitesUnsafe()
    {
        return this.detectedEntities;
    }

    //void IGameInitElement.InitGame()
    //{
        
    //}

    void IGameReadyElement.ReadyGame()
    {
        this.heroComponent.OnCollisionsUpdated += this.UpdateEntities;
    }

    void IGameFinishElement.FinishGame()
    {
        this.heroComponent.OnCollisionsUpdated -= this.UpdateEntities;
    }

    private void UpdateEntities()
    {
        this.detectedEntities.Clear();
        this.heroComponent.GetCollidersUnsafe(out var buffer, out var size);

        for (var i = 0; i < size; i++)
        {
            var collider = buffer[i];
            if (collider.TryGetComponent(out IEntity entity))
            {
                this.detectedEntities.Add(entity);
            }
        }

        this.OnEntitiesUpdated?.Invoke();
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        this.heroComponent = context
            .GetService<HeroService>()
            .GetHero()
            .Get<Component_ColliderSensor>();
    }
}