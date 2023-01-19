using System;
using GameElements;
using Sirenix.OdinInspector;


public class EarnMoneyMission : Mission, IGameInitElement
{
    public override event Action<Mission> OnProgressChanged;

    [ReadOnly]
    [ShowInInspector]
    [PropertySpace(8)]
    public int EarnedMoney { get; private set; }

    [ReadOnly]
    [ShowInInspector]
    public int RequiredMoney
    {
        get { return this.config.RequiredMoney; }
    }

    public override float NormalizedProgress
    {
        get { return (float)this.EarnedMoney / this.RequiredMoney; }
    }

    public override string TextProgress
    {
        get { return $"{this.EarnedMoney}/{this.RequiredMoney}"; }
    }

    private readonly EarnMoneyMissionConfig config;

    private MoneyStorage moneyStorage;

    public EarnMoneyMission(EarnMoneyMissionConfig config) : base(config)
    {
        this.config = config;
        this.EarnedMoney = 0;
    }

    public void Setup(int currentResources)
    {
        this.EarnedMoney = Math.Min(currentResources, this.RequiredMoney);
    }

    protected override void OnStart()
    {
        this.moneyStorage.OnMoneyEarned += this.OnMoneyEarned;
    }

    protected override void OnStop()
    {
        this.moneyStorage.OnMoneyEarned -= this.OnMoneyEarned;
    }

    private void OnMoneyEarned(int income)
    {
        this.EarnedMoney = Math.Min(this.EarnedMoney + income, this.RequiredMoney);
        this.OnProgressChanged?.Invoke(this);
        this.TryComplete();
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        this.moneyStorage = context.GetService<MoneyStorage>();
    }
}