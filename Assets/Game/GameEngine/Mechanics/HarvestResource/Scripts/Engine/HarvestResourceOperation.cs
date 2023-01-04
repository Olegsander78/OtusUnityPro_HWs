using Entities;
using Sirenix.OdinInspector;
using UnityEngine;

public sealed class HarvestResourceOperation
{
    [ReadOnly]
    [ShowInInspector]
    public readonly IEntity TargetResource;

    [ReadOnly]
    [ShowInInspector]
    public readonly ResourceType ResourceType;

    [ReadOnly]
    [ShowInInspector]
    public readonly int ResourceCount;

    [Space]
    [ReadOnly]
    [ShowInInspector]
    public float Progress;

    [ReadOnly]
    [ShowInInspector]
    public bool IsCompleted;

    public HarvestResourceOperation(IEntity targetResource)
    {
        TargetResource = targetResource;
        ResourceType = targetResource.Get<IComponent_GetResourceType>().ResourceType;
        ResourceCount = targetResource.Get<IComponent_GetResourceCount>().ResourceCount;
    }
}