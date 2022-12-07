using Entities;
using GameElements;
using Services;
using UnityEngine;

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
    //void IGameDataLoader.LoadData(IGameContext context)
    //{
    //    if (_repository.LoadStats(out CharacterData data))
    //    {
    //        IEntity character = context.GetService<HeroService>().GetHero();
    //        CharacterConverter.SetupStats(character, data);
    //    }
    //}

    void IGameSaveListener.OnSaveGame(GameSaveReason reason)
    {
        //_character = ServiceLocator.GetService<HeroService>().GetHero();
        var data = CharacterConverter.ConvertToData(_character);
        _repository.SaveCharacter(data);
    }



    //void IGameDataSaver.SaveData(IGameContext context)
    //{
    //    _character = context.GetService<HeroService>().GetHero();
    //    var data = CharacterConverter.ConvertToData(character);
    //    _repository.SaveStats(data);
    //}

    //private void LoadCharactear()
    //{
    //    if (_repository.LoadCharacter(out var character))
    //        _ .SetupMoney(character);
    //}

}