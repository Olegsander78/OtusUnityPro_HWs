using Sirenix.OdinInspector;
using UnityEngine;
using GameSystem;

public sealed class PlayerShower : MonoBehaviour, 
    IGameConstructElement
{
    private PopupManager_ _popupManager;

    private PlayerPresentationModelFactory _presenterFactory;

    [Button]
    public void ShowHero()
    {
        var presentationModel = _presenterFactory.CreatePresenter();
        _popupManager.ShowPopup(PopupName_.HERO_LEVELUP, presentationModel);
    }

    void IGameConstructElement.ConstructGame(GameSystem.IGameContext context)
    {
        _popupManager = context.GetService<PopupManager_>();
        _presenterFactory = context.GetService<PlayerPresentationModelFactory>();
    }
}