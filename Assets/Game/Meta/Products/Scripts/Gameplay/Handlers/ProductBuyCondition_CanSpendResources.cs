

public sealed class ProductBuyCondition_CanSpendResources : IProductBuyCondition
{
    private readonly ResourceStorage resourceStorage;

    public ProductBuyCondition_CanSpendResources(ResourceStorage resourceStorage)
    {
        this.resourceStorage = resourceStorage;
    }

    public bool CanBuy(Product product)
    {
        if (product.TryGetComponent(out IComponent_ResourcePrice component))
        {
            return this.IsResourcesEnough(component);
        }

        return true;
    }

    private bool IsResourcesEnough(IComponent_ResourcePrice component)
    {
        var requiredResources = component.GetPrice();
        for (int i = 0, count = requiredResources.Length; i < count; i++)
        {
            var resourceData = requiredResources[i];
            var amountInStorage = this.resourceStorage.GetResource(resourceData.type);
            if (amountInStorage < resourceData.amount)
            {
                return false;
            }
        }

        return true;
    }
}