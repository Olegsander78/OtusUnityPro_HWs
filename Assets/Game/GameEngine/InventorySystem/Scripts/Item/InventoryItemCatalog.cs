using System;
using UnityEngine;


[CreateAssetMenu(
    fileName = "InventoryItemCatalog",
    menuName = "GameEngine/Inventory/New InventoryItemCatalog"
)]
public sealed class InventoryItemCatalog : ScriptableObject
{
    [SerializeField]
    private InventoryItemConfig[] items;

    public InventoryItemConfig FindItem(string name)
    {
        for (int i = 0, count = this.items.Length; i < count; i++)
        {
            var item = this.items[i];
            if (item.ItemName == name)
            {
                return item;
            }
        }

        throw new Exception($"Item {name} is not found!");
    }

    public InventoryItemConfig[] GetAllItems()
    {
        return this.items;
    }
}