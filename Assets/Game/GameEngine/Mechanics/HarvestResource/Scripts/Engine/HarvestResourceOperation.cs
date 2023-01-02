using Entities;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class HarvestResourceOperation
{
    [ReadOnly]
    [ShowInInspector]
    public readonly IEntity targetResource;

    [ReadOnly]
    [ShowInInspector]
    public readonly ResourceType resourceType;

    [ReadOnly]
    [ShowInInspector]
    public readonly int resourceCount;

    [Space]
    [ReadOnly]
    [ShowInInspector]
    public float progress;

    [ReadOnly]
    [ShowInInspector]
    public bool isCompleted;

    public HarvestResourceOperation(IEntity targetResource)
    {
        this.targetResource = targetResource;
        this.resourceType = targetResource.Get<IComponent_GetResourceType>().ResourceType;
        this.resourceCount = targetResource.Get<IComponent_GetResourceCount>().ResourceCount;
    }
}