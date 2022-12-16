using System.Collections.Generic;
using Entities;
using UnityEngine;


public sealed class EntitiesProvider_Base : EntitiesProvider
{
    [SerializeField]
    private UnityEntity[] entities;

    public override IEnumerable<IEntity> ProvideEntities()
    {
        return this.entities;
    }
}