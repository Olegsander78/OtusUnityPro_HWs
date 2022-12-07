using Services;

public sealed class MoneyMediator :
    IGameSetupListener,
    IGameSaveListener
{
    [Inject]
    private MoneyRepository _repository;

    private MoneyStorage _storage;

    [Inject]
    public void Construct(MoneyRepository repository)
    {
        _repository = repository;
    }
    void IGameSetupListener.OnSetupGame(GameManager gameManager)
    {
        _storage = gameManager.GetService<MoneyStorage>();
        LoadMoney();
    }

    void IGameSaveListener.OnSaveGame(GameSaveReason reason)
    {
        _repository.SaveMoney(_storage.Money);
    }

    private void LoadMoney()
    {
        if (_repository.LoadMoney(out var money))
            _storage.SetupMoney(money);
    }
}