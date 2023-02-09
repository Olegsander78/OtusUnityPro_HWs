using Services;

public sealed class MoneyMediator :
    IGameSaveDataListener,
    IGameLoadDataListener
{
    [ServiceInject]
    private MoneyRepository _repository;

    private MoneyStorage _storage;

    [ServiceInject]
    public void Construct(MoneyRepository repository)
    {
        _repository = repository;
    }


    void IGameSaveDataListener.OnSaveData(GameSaveReason reason)
    {
        _repository.SaveMoney(_storage.Money);
    }

    private void LoadMoney()
    {
        if (_repository.LoadMoney(out var money))
            _storage.SetupMoney(money);
    }

    void IGameLoadDataListener.OnLoadData(GameManager gameManager)
    {
        _storage = gameManager.GetService<MoneyStorage>();
        LoadMoney();
    }
}