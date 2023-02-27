


public sealed class EquipmentItemViewPresenter
{
    private readonly InventoryItemView view;

    private readonly InventoryItem item;

    private PopupManager popupManager;

    private InventoryItemEquipper equipperManager;

    private InventoryItemConsumer consumeManager;

    public EquipmentItemViewPresenter(InventoryItemView view, InventoryItem item)
    {
        this.view = view;
        this.item = item;
    }

    public void Construct(PopupManager popupManager, InventoryItemConsumer consumeManager, InventoryItemEquipper equipperManager)
    {
        this.popupManager = popupManager;
        this.consumeManager= consumeManager;
        this.equipperManager = equipperManager;
    }

    public void Start()
    {
        var metadata = this.item.Metadata;
        this.view.SetTitle(metadata.title);
        this.view.SetIcon(metadata.icon);

        this.view.AddClickListener(this.OnItemClicked);
    }

    public void Stop()
    {   

        this.view.RemoveClickListener(this.OnItemClicked);
    }

    private void OnItemClicked()
    {

        var presenter = new InventoryItemPopupPresenter(this.item);
        presenter.Construct(this.consumeManager, this.equipperManager);
        this.popupManager.ShowPopup(PopupName.INVENTORY_ITEM, presenter);

        //this.equipperManager.UnequipItem(item.GetComponent<IComponent_GetEqupType>().Type);
        
        //var presenter = new InventoryItemPopupPresenter(this.item);
        //presenter.Construct(this.consumeManager, this.equipperManager);
        //this.popupManager.ShowPopup(PopupName.INVENTORY_ITEM, presenter);
    }
}