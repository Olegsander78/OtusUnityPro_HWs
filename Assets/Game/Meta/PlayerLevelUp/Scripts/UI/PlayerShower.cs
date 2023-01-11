using Sirenix.OdinInspector;
using UnityEngine;
using GameElements;

public sealed class PlayerShower : MonoBehaviour, IGameInitElement
{
    private PopupManager _popupManager;

    private PlayerPresentationModelFactory _presenterFactory;

    [Button]
    public void ShowHero()
    {
        var presentationModel = _presenterFactory.CreatePresenter();
        _popupManager.ShowPopup(PopupName.HERO, presentationModel);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _popupManager = context.GetService<PopupManager>();
        _presenterFactory = context.GetService<PlayerPresentationModelFactory>();
    }
}