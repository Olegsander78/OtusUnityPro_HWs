using System;
using UnityEngine;


[CreateAssetMenu(
    fileName = "ItemInfoCatalog",
    menuName = "GameEngine/GameItems/New ItemInfoCatalog"
)]
public sealed class ItemInfoCatalog : ScriptableObject
{
    [SerializeField]
    private ItemFromTwoResourcesInfo[] configs;

    public ItemFromTwoResourcesInfo FindItems(ItemType type)
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

    public ItemFromTwoResourcesInfo[] GetAllItems()
    {
        return this.configs;
    }
}