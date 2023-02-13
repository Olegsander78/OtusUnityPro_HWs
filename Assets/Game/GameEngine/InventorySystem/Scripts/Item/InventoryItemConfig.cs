using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;


[CreateAssetMenu(
    fileName = "InventoryItemConfig",
    menuName = "GameEngine/Inventory/New InventoryItem"
)]
public sealed class InventoryItemConfig : SerializedScriptableObject
{
    public string ItemName
    {
        get { return this.origin.Name; }
    }

    public InventoryItemMetadata Metadata
    {
        get { return this.origin.Metadata; }
    }

    public InventoryItemFlags Flags
    {
        get { return this.origin.Flags; }
    }

    public InventoryItem Prototype
    {
        get { return this.origin; }
    }

    [OdinSerialize]
    private InventoryItem origin = new();
}