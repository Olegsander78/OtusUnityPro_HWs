using UnityEngine;
using GameElements;

public sealed class ProductPresentationModelFactory : MonoBehaviour, IGameInitElement
{
    private ProductBuyer productBuyer;

    private MoneyStorage moneyStorage;

    public ProductPresentationModel CreatePresenter(Product product)
    {
        return new ProductPresentationModel(product, this.productBuyer, this.moneyStorage);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        this.productBuyer = context.GetService<ProductBuyer>();
        this.moneyStorage = context.GetService<MoneyStorage>();
    }
}