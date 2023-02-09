using System.Collections.Generic;
using Entities;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;


public abstract class AbstractDetector : SerializedMonoBehaviour,
    IGameConstructElement,
    //IGameInitElement,
    IGameReadyElement,
    IGameFinishElement
{
    private EntitySensor sensor;

    [ReadOnly]
    [ShowInInspector]
    private readonly List<IEntity> detectedEntites;

    public AbstractDetector()
    {
        this.detectedEntites = new List<IEntity>();
    }

    public virtual void ConstructGame(IGameContext context)
    {
        this.sensor = context.GetService<EntitySensor>();
    }

    //public virtual void InitGame()
    //{
        
    //}

    public virtual void ReadyGame()
    {
        this.sensor.OnEntitiesUpdated += this.OnEntitiesUpdated;
    }

    public virtual void FinishGame()
    {
        this.sensor.OnEntitiesUpdated -= this.OnEntitiesUpdated;
    }

    protected abstract bool MatchesEntity(IEntity entity);

    protected abstract void OnEntitesChanged(List<IEntity> entities);

    private void OnEntitiesUpdated()
    {
        this.detectedEntites.Clear();

        var entities = this.sensor.GetDetectedEntitesUnsafe();
        for (int i = 0, count = entities.Count; i < count; i++)
        {
            var entity = entities[i];
            if (this.MatchesEntity(entity))
            {
                this.detectedEntites.Add(entity);
            }
        }

        this.OnEntitesChanged(this.detectedEntites);
    }


}