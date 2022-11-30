using UnityEngine;


public sealed class ProductPresentationModelFactory : MonoBehaviour, IConstructListener
{
    private ProductBuyer productBuyer;

    private MoneyStorage moneyStorage;

    public ProductPresentationModel CreatePresenter(Product product)
    {
        return new ProductPresentationModel(product, this.productBuyer, this.moneyStorage);
    }

    void IConstructListener.Construct(GameContext context)
    {
        this.productBuyer = context.GetService<ProductBuyer>();
        this.moneyStorage = context.GetService<MoneyStorage>();
    }
}