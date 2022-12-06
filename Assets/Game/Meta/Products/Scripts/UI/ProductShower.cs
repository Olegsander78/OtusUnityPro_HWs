using Sirenix.OdinInspector;
using UnityEngine;
using GameElements;

public sealed class ProductShower : MonoBehaviour, IGameInitElement
{
    private PopupManager popupManager;

    private ProductPresentationModelFactory presenterFactory;

    [Button]
    public void ShowProduct(Product product)
    {
        var presentationModel = this.presenterFactory.CreatePresenter(product);
        this.popupManager.ShowPopup(PopupName.PRODUCT, presentationModel);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        this.popupManager = context.GetService<PopupManager>();
        this.presenterFactory = context.GetService<ProductPresentationModelFactory>();
    }
}