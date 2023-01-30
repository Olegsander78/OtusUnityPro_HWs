using Sirenix.OdinInspector;
using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Chest Reward - Resources",
    menuName = "Meta/Chests/New Reward (Resources)"
)]
public class ChestRewardConfig_Resource : ChestRewardConfig
{
    [ShowInInspector, ReadOnly]
    public ResourceType ResourceType => _resourceType;

    private ResourceType _resourceType;

    public ResourceType GenerateResourceType()
    {
        var rnd = new System.Random();
        _resourceType = (ResourceType)rnd.Next(Enum.GetNames(typeof(ResourceType)).Length);
        return _resourceType;
    }
}