using Entities;
using UnityEngine;
using GameSystem;

public sealed class PlayerPresentationModelFactory : MonoBehaviour,
    IGameConstructElement
{
    [SerializeField]
    private PartyMember _partyMember;

    private IEntity _character;    

    private PlayerLevelUpper _playerLevelUpper;

    public PlayerPresentationModel CreatePresenter()
    {
        return new PlayerPresentationModel(_partyMember, _character, _playerLevelUpper);
    }

    void IGameConstructElement.ConstructGame(GameSystem.IGameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
        _playerLevelUpper = context.GetService<PlayerLevelUpper>();
    }
}