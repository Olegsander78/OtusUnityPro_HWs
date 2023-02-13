using System;
using UnityEngine;


[Serializable]
public sealed class Component_InventoryItem : IComponent_InventoryItem
{
    public InventoryItem Item
    {
        get { return this.config.Prototype; }
    }

    [SerializeField]
    private InventoryItemConfig config;
}