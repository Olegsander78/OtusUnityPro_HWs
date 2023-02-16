using UnityEngine;


public sealed class InventoryItemPopupPresenter : InventoryItemPopup.IPresenter
{
    public string Title
    {
        get { return this.item.Metadata.title; }
    }

    public string Description
    {
        get { return this.item.Metadata.decription; }
    }

    public Sprite Icon
    {
        get { return this.item.Metadata.icon; }
    }

    private readonly InventoryItem item;

    private InventoryItemConsumer consumeManager;

    private InventoryItemEquipper equipperManager;

    public InventoryItemPopupPresenter(InventoryItem item)
    {
        this.item = item;
    }

    public void Construct(InventoryItemConsumer consumeManager, InventoryItemEquipper equipperManager)
    {
        this.consumeManager = consumeManager;
        this.equipperManager = equipperManager;
    }

    public bool IsStackableItem()
    {
        return this.item.FlagsExists(InventoryItemFlags.STACKABLE);
    }

    public void GetStackInfo(out int current, out int size)
    {
        var component = this.item.GetComponent<IComponent_Stackable>();
        current = component.Value;
        size = component.Size;
    }

    public bool IsConsumableItem()
    {
        return this.item.FlagsExists(InventoryItemFlags.CONSUMABLE);
    }

    public bool CanConsumeItem()
    {
        return this.consumeManager.CanConsumeItem(this.item);
    }

    public void OnConsumeClicked()
    {
        if (this.consumeManager.CanConsumeItem(this.item))
        {
            this.consumeManager.ConsumeItem(this.item);
        }
    }

    public bool IsEquipableItem()
    {
        return this.item.FlagsExists(InventoryItemFlags.EQUIPPABLE);
    }

    public bool CanEquipableItem()
    {
        return this.equipperManager.CanEquipItem(this.item);
    }

    public void OnEquipClicked()
    {
        if (this.equipperManager.CanEquipItem(this.item))
        {
            this.equipperManager.EquipItem(this.item);
        }
    }
}