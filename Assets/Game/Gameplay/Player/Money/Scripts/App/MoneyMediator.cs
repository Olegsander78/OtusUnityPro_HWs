using Services;

public sealed class MoneyMediator :
    IGameSaveDataListener,
    IGameLoadDataListener
{
    
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

    void IGameLoadDataListener.OnLoadData(GameManager gameManager)
    {
        _storage = gameManager.GetService<MoneyStorage>();
        LoadMoney();
    }

    private void LoadMoney()
    {
        if (!_repository.LoadMoney(out var money))
        {
            var config = MoneyStorageConfig.LoadAsset();
            money = config.InitialMoney;
        }
        _storage.SetupMoney(money);
    }
}