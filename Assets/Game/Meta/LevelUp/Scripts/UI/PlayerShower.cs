using Sirenix.OdinInspector;
using UnityEngine;


public sealed class PlayerShower : MonoBehaviour, IConstructListener
{
    private PopupManager popupManager;

    private ProductPresentationModelFactory presenterFactory;

    [Button]
    public void ShowProduct(Product product)
    {
        var presentationModel = this.presenterFactory.CreatePresenter(product);
        this.popupManager.ShowPopup(PopupName.PRODUCT, presentationModel);
    }

    void IConstructListener.Construct(GameContext context)
    {
        this.popupManager = context.GetService<PopupManager>();
        this.presenterFactory = context.GetService<ProductPresentationModelFactory>();
    }
}