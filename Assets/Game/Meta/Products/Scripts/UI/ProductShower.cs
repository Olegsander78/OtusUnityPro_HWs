using Sirenix.OdinInspector;
using UnityEngine;
using GameSystem;

public sealed class ProductShower : MonoBehaviour,
    IGameConstructElement
{
    private PopupManager_ popupManager;

    private ProductPresentationModelFactory presenterFactory;

    [Button]
    public void ShowProduct(Product product)
    {
        var presentationModel = this.presenterFactory.CreatePresenter(product);
        this.popupManager.ShowPopup(PopupName_.PRODUCT, presentationModel);
    }

    void IGameConstructElement.ConstructGame(GameSystem.IGameContext context)
    {
        this.popupManager = context.GetService<PopupManager_>();
        this.presenterFactory = context.GetService<ProductPresentationModelFactory>();
    }
}