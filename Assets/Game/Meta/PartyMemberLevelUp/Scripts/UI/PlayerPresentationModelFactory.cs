using Entities;
using UnityEngine;

public sealed class PlayerPresentationModelFactory : MonoBehaviour, IConstructListener
{
    private IEntity _character;

    private PlayerLevelUpper _playerLevelUpper;

    public PlayerPresentationModel CreatePresenter(PartyMember player)
    {
        return new PlayerPresentationModel(player,_character, _playerLevelUpper);
    }

    void IConstructListener.Construct(GameContext context)
    {
        _character = context.GetService<HeroService>().GetHero();
        _playerLevelUpper = context.GetService<PlayerLevelUpper>();
    }
}