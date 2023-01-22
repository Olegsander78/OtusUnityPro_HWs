using System;
using GameElements;
using Sirenix.OdinInspector;


public sealed class KillEnemyDailyQuest : DailyQuest,
    IGameInitElement
{
    public override event Action<DailyQuest> OnProgressChanged;

    [ReadOnly]
    [ShowInInspector]
    [PropertySpace(8)]
    public int CurrentKills { get; private set; }

    [ReadOnly]
    [ShowInInspector]
    public int RequiredKills
    {
        get { return _config.RequiredKills; }
    }

    public override float NormalizedProgress
    {
        get { return (float)CurrentKills / RequiredKills; }
    }

    public override string TextProgress
    {
        get { return $"{CurrentKills}/{RequiredKills}"; }
    }

    private readonly KillEnemyDailyQuestConfig _config;

    private HeroService _heroService;

    private IComponent_MeleeCombat _heroMeleeCombatComponent;

    private IComponent_ProjectileRangeAttack _heroRangeCombatComponent;

    public KillEnemyDailyQuest(KillEnemyDailyQuestConfig config) : base(config)
    {
        _config = config;
    }

    protected override void OnStart()
    {
        _heroMeleeCombatComponent.OnCombatStopped += OnCombatFinished;
        //_heroRangeCombatComponent.TryGetCurrentProjectile().OnKilledEnemy += OnKilledEnemyOnDistance;
    }   

    protected override void OnStop()
    {
        _heroMeleeCombatComponent.OnCombatStopped -= OnCombatFinished;
        //_heroRangeCombatComponent.TryGetCurrentProjectile().OnKilledEnemy -= OnKilledEnemyOnDistance;
    }

    private void OnCombatFinished(MeleeCombatOperation operation)
    {
        if (operation.targetDestroyed)
        {
            CurrentKills++;
            OnProgressChanged?.Invoke(this);
            TryComplete();
        }
    }

    public void Setup(int currentKills)
    {
        CurrentKills = currentKills;
    }

    private void OnKilledEnemyOnDistance()
    {
        CurrentKills++;
        OnProgressChanged?.Invoke(this);
        TryComplete();
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _heroService = context.GetService<HeroService>();
        _heroMeleeCombatComponent = _heroService.GetHero().Get<IComponent_MeleeCombat>();
        _heroRangeCombatComponent = _heroService.GetHero().Get<IComponent_ProjectileRangeAttack>();
    }
}