using Sirenix.OdinInspector;
using UnityEngine;
using GameElements;

public sealed class PlayerShower : MonoBehaviour, IGameInitElement
{
    private PopupManager _popupManager;

    private PlayerPresentationModelFactory _presenterFactory;

    [Button]
    public void ShowPartyMember(PartyMember partyMember)
    {
        var presentationModel = _presenterFactory.CreatePresenter(partyMember);
        _popupManager.ShowPopup(PopupName.PARTY_MEMBER, presentationModel);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _popupManager = context.GetService<PopupManager>();
        _presenterFactory = context.GetService<PlayerPresentationModelFactory>();
    }
}