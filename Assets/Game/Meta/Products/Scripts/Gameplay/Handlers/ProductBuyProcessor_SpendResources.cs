


public sealed class ProductBuyProcessor_SpendResources : IProductBuyProcessor
{
    private readonly ResourceStorage resourceStorage;

    public ProductBuyProcessor_SpendResources(ResourceStorage resourceStorage)
    {
        this.resourceStorage = resourceStorage;
    }

    public void ProcessBuy(Product product)
    {
        if (product.TryGetComponent(out IComponent_ResourcePrice component))
        {
            var requiredResources = component.GetPrice();
            this.SpendResources(requiredResources);
        }
    }

    private void SpendResources(ResourceData[] requiredResources)
    {
        for (int i = 0, count = requiredResources.Length; i < count; i++)
        {
            var resourceData = requiredResources[i];
            this.resourceStorage.ExtractResource(resourceData.type, resourceData.amount);
        }
    }
}