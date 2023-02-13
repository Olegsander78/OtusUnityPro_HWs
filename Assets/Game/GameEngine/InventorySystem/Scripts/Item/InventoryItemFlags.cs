using System;


[Flags]
public enum InventoryItemFlags
{
    NONE = 0,
    STACKABLE = 1,
    CONSUMABLE = 2,
    EQUIPPABLE = 4,
    EFFECTIBLE = 8,
}