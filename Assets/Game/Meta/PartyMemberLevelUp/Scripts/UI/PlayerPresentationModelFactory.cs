using Entities;
using UnityEngine;
using GameElements;

public sealed class PlayerPresentationModelFactory : MonoBehaviour, IGameInitElement
{
    [SerializeField]
    private PartyMember _partyMember;

    private IEntity _character;    

    private PlayerLevelUpper _playerLevelUpper;

    public PlayerPresentationModel CreatePresenter()
    {
        return new PlayerPresentationModel(_partyMember, _character, _playerLevelUpper);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
        _playerLevelUpper = context.GetService<PlayerLevelUpper>();
    }
}