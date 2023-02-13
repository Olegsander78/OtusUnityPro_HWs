


public sealed class ProductBuyProcessor_SpendMoney : IProductBuyProcessor
{
    private readonly MoneyStorage moneyStorage;

    public ProductBuyProcessor_SpendMoney(MoneyStorage moneyStorage)
    {
        this.moneyStorage = moneyStorage;
    }

    public void ProcessBuy(Product product)
    {
        if (product.TryGetComponent(out IComponent_MoneyPrice component))
        {
            this.moneyStorage.SpendMoney(component.Price);
        }
    }
}