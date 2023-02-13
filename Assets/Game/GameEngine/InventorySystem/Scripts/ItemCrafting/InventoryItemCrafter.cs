using System;
using Sirenix.OdinInspector;


public class InventoryItemCrafter
{
    public event Action<InventoryItemReceipt> OnCraftFinished;

    private StackableInventory inventory;

    public InventoryItemCrafter()
    {
    }

    public InventoryItemCrafter(StackableInventory inventory)
    {
        this.inventory = inventory;
    }

    public void SetInventory(StackableInventory inventory)
    {
        this.inventory = inventory;
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public bool CanCraftItem(InventoryItemReceipt receipt)
    {
        var ingredients = receipt.ingredients;
        for (int i = 0, count = ingredients.Length; i < count; i++)
        {
            var ingredient = ingredients[i];
            if (!this.IngredientExists(ingredient))
            {
                return false;
            }
        }

        return true;
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void CraftItem(InventoryItemReceipt receipt)
    {
        var ingredients = receipt.ingredients;
        for (int i = 0, count = ingredients.Length; i < count; i++)
        {
            var ingredient = ingredients[i];
            this.ConsumeIngredient(ingredient);
        }

        this.ProduceResult(receipt);
        this.OnCraftFinished?.Invoke(receipt);
    }

    private bool IngredientExists(InventoryItemIngredient ingredient)
    {
        var count = ingredient.requiredCount;
        var itemName = ingredient.itemInfo.ItemName;
        return this.inventory.CountItems(itemName) >= count;
    }

    private void ConsumeIngredient(InventoryItemIngredient ingredient)
    {
        var itemName = ingredient.itemInfo.ItemName;
        var count = ingredient.requiredCount;
        this.inventory.RemoveItems(itemName, count);
    }

    private void ProduceResult(InventoryItemReceipt receipt)
    {
        var resultItem = receipt.resultInfo.Prototype;
        this.inventory.AddItemsByPrototype(resultItem, 1);
    }
}