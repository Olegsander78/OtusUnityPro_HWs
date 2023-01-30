using Sirenix.OdinInspector;
using System;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Chest Reward - Resources",
    menuName = "Meta/Chests/New Reward (Resources)"
)]
public class ChestRewardConfig_Resource : ChestRewardConfig
{
    public ResourceType Resource { get; private set; }
       

    public  ResourceType GenerateResourceType()
    {
        var rnd = new System.Random();
        Resource = (ResourceType)rnd.Next(Enum.GetNames(typeof(ResourceType)).Length);
        return Resource;
    }
}