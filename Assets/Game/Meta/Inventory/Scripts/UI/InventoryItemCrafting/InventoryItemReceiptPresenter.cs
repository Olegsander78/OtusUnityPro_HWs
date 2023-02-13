
public sealed class InventoryItemReceiptPresenter
{
    private readonly InventoryItemReceiptView view;

    private readonly InventoryItemReceipt receipt;

    private InventoryItemCrafter craftManager;

    private StackableInventory inventory;

    public InventoryItemReceiptPresenter(InventoryItemReceiptView view, InventoryItemReceipt receipt)
    {
        this.view = view;
        this.receipt = receipt;
    }

    public void Construct(InventoryItemCrafter craftManager, StackableInventory inventory)
    {
        this.craftManager = craftManager;
        this.inventory = inventory;
    }

    public void Start()
    {
        this.SetupIngredients();
        this.SetupResult();
        this.SetupCraftButton();
        this.view.OnCraftButtonClicked += this.OnCraftButtonClicked;
    }

    private void SetupCraftButton()
    {
        var canCraft = this.craftManager.CanCraftItem(this.receipt);
        this.view.SetInteractibleButton(canCraft);
    }

    public void Stop()
    {
        this.view.OnCraftButtonClicked -= this.OnCraftButtonClicked;
    }

    private void OnCraftButtonClicked()
    {
        if (!this.craftManager.CanCraftItem(this.receipt))
        {
            return;
        }

        this.craftManager.CraftItem(this.receipt);

        //TODO: Show particles

        //Update state:
        this.SetupIngredients();
        this.SetupCraftButton();
    }

    private void SetupResult()
    {
        var resultItem = this.receipt.resultInfo;
        var metadata = resultItem.Metadata;
        this.view.SetTitle(metadata.title);
        this.view.SetDescription(metadata.decription);
        this.view.SetIcon(metadata.icon);
    }

    private void SetupIngredients()
    {
        var ingredients = this.receipt.ingredients;
        var ingredientCount = ingredients.Length;

        //Show used ingredient views:
        for (var i = 0; i < ingredientCount; i++)
        {
            var ingredient = ingredients[i];
            var ingredientView = this.view.GetIngredient(i);
            var ingredientItem = ingredient.itemInfo;

            var title = ingredientItem.Metadata.title;
            var requiredCount = ingredient.requiredCount;
            var actualCount = this.inventory.CountItems(ingredientItem.ItemName);
            ingredientView.Setup(title, requiredCount, actualCount);
            ingredientView.SetVisible(true);
        }

        //Hide unused ingredients views:
        for (int i = ingredientCount, end = this.view.IngredientCount; i < end; i++)
        {
            var ingredientView = this.view.GetIngredient(i);
            ingredientView.SetVisible(false);
        }
    }
}