using UnityEngine;
using GameSystem;

public sealed class ProductPresentationModelFactory : MonoBehaviour, 
    IGameConstructElement
{
    private ProductBuyer productBuyer;

    private MoneyStorage moneyStorage;

    public ProductPresentationModel CreatePresenter(Product product)
    {
        return new ProductPresentationModel(product, this.productBuyer, this.moneyStorage);
    }

    void IGameConstructElement.ConstructGame(GameSystem.IGameContext context)
    {
        this.productBuyer = context.GetService<ProductBuyer>();
        this.moneyStorage = context.GetService<MoneyStorage>();
    }
}