using GameSystem;


public abstract class InventoryObserver :
    IGameReadyElement,
    IGameFinishElement
{
    protected StackableInventory inventory;

    public void SetInventory(StackableInventory inventory)
    {
        this.inventory = inventory;
    }

    public virtual void ReadyGame()
    {
        this.inventory.OnItemAdded += this.OnItemAdded;
        this.inventory.OnItemRemoved += this.OnItemRemoved;
    }

    public virtual void FinishGame()
    {
        this.inventory.OnItemAdded -= this.OnItemAdded;
        this.inventory.OnItemRemoved -= this.OnItemRemoved;
    }

    protected abstract void OnItemAdded(InventoryItem item);

    protected abstract void OnItemRemoved(InventoryItem item);
}