using System;
using GameElements;
using Sirenix.OdinInspector;

public class EarnMoneyDailyQuest : DailyQuest, IGameInitElement
{
    public override event Action<DailyQuest> OnProgressChanged;

    [ReadOnly]
    [ShowInInspector]
    [PropertySpace(8)]
    public int EarnedMoney { get; private set; }

    [ReadOnly]
    [ShowInInspector]
    public int RequiredMoney
    {
        get { return _config.RequiredMoney; }
    }

    public override float NormalizedProgress
    {
        get { return (float)EarnedMoney / RequiredMoney; }
    }

    public override string TextProgress
    {
        get { return $"{EarnedMoney}/{RequiredMoney}"; }
    }

    private readonly EarnMoneyDailyQuestConfig _config;

    private MoneyStorage _moneyStorage;

    public EarnMoneyDailyQuest(EarnMoneyDailyQuestConfig config) : base(config)
    {
        _config = config;
        EarnedMoney = 0;
    }

    public void Setup(int currentResources)
    {
        EarnedMoney = Math.Min(currentResources, RequiredMoney);
    }

    protected override void OnStart()
    {
        _moneyStorage.OnMoneyEarned += OnMoneyEarned;
    }

    protected override void OnStop()
    {
        _moneyStorage.OnMoneyEarned -= OnMoneyEarned;
    }

    private void OnMoneyEarned(int income)
    {
        EarnedMoney = Math.Min(EarnedMoney + income, RequiredMoney);
        OnProgressChanged?.Invoke(this);
        TryComplete();
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _moneyStorage = context.GetService<MoneyStorage>();
    }
}