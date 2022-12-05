using Entities;
using GameElements;
using Services;

public sealed class CharacterMediator : IGameDataLoader, IGameDataSaver
{
    private CharacterRepository _repository;

    [Inject]
    public void Construct(CharacterRepository repository)
    {
        _repository = repository;
    }

    void IGameDataLoader.LoadData(IGameContext context)
    {
        if (_repository.LoadStats(out CharacterData data))
        {
            IEntity character = context.GetService<HeroService>().GetHero();
            CharacterConverter.SetupStats(character, data);
        }
    }

    void IGameDataSaver.SaveData(IGameContext context)
    {
        var character = context.GetService<HeroService>().GetHero();
        var data = CharacterConverter.ConvertToData(character);
        _repository.SaveStats(data);
    }
}