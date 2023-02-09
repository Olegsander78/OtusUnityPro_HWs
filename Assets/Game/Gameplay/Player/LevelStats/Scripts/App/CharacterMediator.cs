using Entities;
using Services;

public sealed class CharacterMediator :
    IGameSaveDataListener,
    IGameLoadDataListener
    
{
    private CharacterRepository _repository;
        
    private IEntity _character;

    [ServiceInject]
    public void Construct(CharacterRepository repository)
    {
        _repository = repository;
    }
    //void IGameSetupListener.OnSetupGame(GameManager gameManager)
    //{
    //    _character = gameManager.GetService<HeroService>().GetHero();

    //    if (_repository.LoadCharacter(out CharacterData data))
    //    {
    //        _character = gameManager.GetService<HeroService>().GetHero();
    //        CharacterConverter.SetupStats(_character, data);
    //    }
    //}

    void IGameSaveDataListener.OnSaveData(GameSaveReason reason)
    {
        var data = CharacterConverter.ConvertToData(_character);
        _repository.SaveCharacter(data);
    }

    void IGameLoadDataListener.OnLoadData(GameManager gameManager)
    {
        _character = gameManager.GetService<HeroService>().GetHero();

        if (_repository.LoadCharacter(out CharacterData data))
        {
            _character = gameManager.GetService<HeroService>().GetHero();
            CharacterConverter.SetupStats(_character, data);
        }
    }
}