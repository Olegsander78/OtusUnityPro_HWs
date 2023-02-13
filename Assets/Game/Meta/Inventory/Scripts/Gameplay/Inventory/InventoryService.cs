using Sirenix.OdinInspector;
using UnityEngine;


public sealed class InventoryService
{
    [ReadOnly, ShowInInspector]
    private StackableInventory inventory;

    public void Setup(StackableInventory inventory)
    {
        this.inventory = inventory;
    }

    public StackableInventory GetInventory()
    {
        return this.inventory;
    }
}