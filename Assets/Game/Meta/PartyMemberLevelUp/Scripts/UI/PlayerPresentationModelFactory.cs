using Entities;
using UnityEngine;
using GameElements;

public sealed class PlayerPresentationModelFactory : MonoBehaviour, IGameInitElement
{
    private IEntity _character;

    private PlayerLevelUpper _playerLevelUpper;

    public PlayerPresentationModel CreatePresenter(PartyMember player)
    {
        return new PlayerPresentationModel(player,_character, _playerLevelUpper);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
        _playerLevelUpper = context.GetService<PlayerLevelUpper>();
    }
}