using System.Collections.Generic;
using Entities;


public interface IEntityDetectObserver
{
    void OnEntitiesUpdated(List<IEntity> entities);
}