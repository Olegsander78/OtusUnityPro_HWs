using System.Collections.Generic;
using Entities;


public sealed class EntitiesProvider_ComponentsInChildren : EntitiesProvider
{
    public override IEnumerable<IEntity> ProvideEntities()
    {
        return this.GetComponentsInChildren<IEntity>();
    }
}