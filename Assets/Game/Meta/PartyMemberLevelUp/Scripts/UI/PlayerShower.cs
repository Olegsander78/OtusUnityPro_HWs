using Sirenix.OdinInspector;
using UnityEngine;

public sealed class PlayerShower : MonoBehaviour, IConstructListener
{
    private PopupManager _popupManager;

    private PlayerPresentationModelFactory _presenterFactory;

    [Button]
    public void ShowPartyMember(PartyMember partyMember)
    {
        var presentationModel = _presenterFactory.CreatePresenter(partyMember);
        _popupManager.ShowPopup(PopupName.PARTY_MEMBER, presentationModel);
    }

    void IConstructListener.Construct(GameContext context)
    {
        _popupManager = context.GetService<PopupManager>();
        _presenterFactory = context.GetService<PlayerPresentationModelFactory>();
    }
}