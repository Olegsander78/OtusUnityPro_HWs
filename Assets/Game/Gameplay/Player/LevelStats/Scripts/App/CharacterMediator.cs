using Entities;
using Services;

public sealed class CharacterMediator :
    IGameSetupListener,
    IGameSaveListener
    
{
    private CharacterRepository _repository;
        
    private IEntity _character;

    [Inject]
    public void Construct(CharacterRepository repository)
    {
        _repository = repository;
    }
    void IGameSetupListener.OnSetupGame(GameManager gameManager)
    {
        _character = gameManager.GetService<HeroService>().GetHero();

        if (_repository.LoadCharacter(out CharacterData data))
        {
            _character = gameManager.GetService<HeroService>().GetHero();
            CharacterConverter.SetupStats(_character, data);
        }
    }

    void IGameSaveListener.OnSaveGame(GameSaveReason reason)
    {
        var data = CharacterConverter.ConvertToData(_character);
        _repository.SaveCharacter(data);
    }
}