using System;
using UnityEngine;


[CreateAssetMenu(
    fileName = "ResourceInfoCatalog",
    menuName = "GameEngine/GameResources/New ResourceInfoCatalog"
)]
public sealed class ResourceInfoCatalog : ScriptableObject
{
    [SerializeField]
    private ResourceInfo[] configs;

    public ResourceInfo FindResource(ResourceType type)
    {
        for (int i = 0, count = this.configs.Length; i < count; i++)
        {
            var info = this.configs[i];
            if (info.type == type)
            {
                return info;
            }
        }

        throw new Exception($"Resource {type} is not found!");
    }

    public ResourceInfo[] GetAllResources()
    {
        return this.configs;
    }
}