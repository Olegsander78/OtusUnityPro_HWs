using UnityEngine;


public sealed class PlayerPresentationModelFactory : MonoBehaviour, IConstructListener
{
    private PlayerLevelUpper _playerLevelUpper;

    private ExperienceStorage _experienceStorage;

    public PlayerPresentationModel CreatePresenter(PartyMember player)
    {
        return new PlayerPresentationModel(player, _playerLevelUpper, _experienceStorage);
    }

    void IConstructListener.Construct(GameContext context)
    {
        _playerLevelUpper = context.GetService<PlayerLevelUpper>();
        _experienceStorage = context.GetService<ExperienceStorage>();
    }
}