using System.Collections.Generic;
using Entities;
using Sirenix.OdinInspector;


public abstract class EntityDetectObserver : IEntityDetectObserver
{
    [ReadOnly]
    [ShowInInspector]
    private readonly List<IEntity> detectedEntites = new();

    protected abstract bool MatchesEntity(IEntity entity);

    protected abstract void OnEntitesChanged(List<IEntity> entities);

    public void OnEntitiesUpdated(List<IEntity> entities)
    {
        this.detectedEntites.Clear();
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