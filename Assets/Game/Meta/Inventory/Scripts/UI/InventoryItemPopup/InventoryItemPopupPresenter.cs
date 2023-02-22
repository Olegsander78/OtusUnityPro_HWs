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
            Debug.Log("Button clicked - Consume item");
        }
    }

    public bool IsEquipableItem()
    {
        return this.item.FlagsExists(InventoryItemFlags.EQUIPPABLE);
    }

    public bool CanEquipItem()
    {
        return this.equipperManager.CanEquipItem(this.item);
    }

    public void OnEquipClicked()
    {
        if (this.equipperManager.CanEquipItem(this.item))
        {
            this.equipperManager.EquipItem(this.item);
            Debug.Log("Button clicked - Equipped item");
        }
        else
        {
            Debug.Log("Button not clicked - Equipped item");
        }
    }

    public void OnUnequipClicked()
    {
        if (this.IsEquippedItem())
        {
            if(item.TryGetComponent(out IComponent_GetEqupType component))
            {
                this.equipperManager.UnequipItem(component.Type);
                //this.equipperManager.UnequipItem(this.item.GetComponent<IComponent_GetEqupType>().Type);

                Debug.Log("Button clicked - UnEquipped item");
            }
        }
        else
        {
            Debug.Log("Button not clicked - UnEquipped item");
        }
        //this.equipperManager.UnequipItem(this.item.GetComponent<IComponent_GetEqupType>().Type);
    }

    public bool IsEquippedItem()
    {
        if (item.TryGetComponent(out IComponent_GetEqupType component))
        {
            //return this.equipperManager.IsItemEquipped(this.item.GetComponent<IComponent_GetEqupType>().Type);

            //return this.equipperManager.GetEquippedItem(this.item.GetComponent<IComponent_GetEqupType>().Type) != null;
            return this.equipperManager.GetEquippedItem(component.Type) != null;

        }

        return false;
    }
}