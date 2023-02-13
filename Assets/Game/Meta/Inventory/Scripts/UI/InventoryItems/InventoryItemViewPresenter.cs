


public sealed class InventoryItemViewPresenter
{
    private readonly InventoryItemView view;

    private readonly InventoryItem item;

    private IComponent_Stackable stackableComponent;

    private PopupManager popupManager;

    private InventoryItemConsumer consumeManager;

    public InventoryItemViewPresenter(InventoryItemView view, InventoryItem item)
    {
        this.view = view;
        this.item = item;
    }

    public void Construct(PopupManager popupManager, InventoryItemConsumer consumeManager)
    {
        this.popupManager = popupManager;
        this.consumeManager = consumeManager;
    }

    public void Start()
    {
        var metadata = this.item.Metadata;
        this.view.SetTitle(metadata.title);
        this.view.SetIcon(metadata.icon);

        var flagsExists = this.item.FlagsExists(InventoryItemFlags.STACKABLE);
        this.view.Stack.SetVisible(flagsExists);

        if (flagsExists)
        {
            this.stackableComponent = this.item.GetComponent<IComponent_Stackable>();
            this.stackableComponent.OnValueChanged += this.OnAmountChanged;

            this.view.Stack.SetAmount(this.stackableComponent.Value, this.stackableComponent.Size);
        }

        this.view.AddClickListener(this.OnItemClicked);
    }

    public void Stop()
    {
        if (this.item.FlagsExists(InventoryItemFlags.STACKABLE))
        {
            this.stackableComponent.OnValueChanged -= this.OnAmountChanged;
        }

        this.view.RemoveClickListener(this.OnItemClicked);
    }

    private void OnAmountChanged(int newCount)
    {
        this.view.Stack.SetAmount(newCount, this.stackableComponent.Size);
    }

    private void OnItemClicked()
    {
        var presenter = new InventoryItemPopupPresenter(this.item);
        presenter.Construct(this.consumeManager);
        this.popupManager.ShowPopup(PopupName.INVENTORY_ITEM, presenter);
    }
}