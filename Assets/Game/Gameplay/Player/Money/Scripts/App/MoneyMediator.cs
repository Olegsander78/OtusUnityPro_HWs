using GameElements;
using Services;

public sealed class MoneyMediator : IGameDataLoader, IGameDataSaver
{
    [Inject]
    private MoneyRepository _repository;

    void IGameDataLoader.LoadData(IGameContext context)
    {
        if (_repository.LoadMoney(out var money))
        {
            var moneyStorage = context.GetService<MoneyStorage>();
            moneyStorage.SetupMoney(money);
        }
    }

    void IGameDataSaver.SaveData(IGameContext context)
    {
        var moneyStorage = context.GetService<MoneyStorage>();
        _repository.SaveMoney(moneyStorage.Money);
    }
}