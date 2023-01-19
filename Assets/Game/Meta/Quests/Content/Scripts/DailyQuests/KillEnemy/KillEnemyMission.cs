using System;
using Entities;
using GameElements;
using Sirenix.OdinInspector;


public sealed class KillEnemyMission : Mission,
    IGameInitElement
{
    public override event Action<Mission> OnProgressChanged;

    [ReadOnly]
    [ShowInInspector]
    [PropertySpace(8)]
    public int CurrentKills { get; private set; }

    [ReadOnly]
    [ShowInInspector]
    public int RequiredKills
    {
        get { return this.config.RequiredKills; }
    }

    public override float NormalizedProgress
    {
        get { return (float)this.CurrentKills / this.RequiredKills; }
    }

    public override string TextProgress
    {
        get { return $"{this.CurrentKills}/{this.RequiredKills}"; }
    }

    private readonly KillEnemyMissionConfig config;

    private HeroService heroService;

    private IComponent_MeleeCombat heroComponent;

    public KillEnemyMission(KillEnemyMissionConfig config) : base(config)
    {
        this.config = config;
    }

    protected override void OnStart()
    {
        this.heroComponent.OnCombatStopped += this.OnCombatFinished;
    }

    protected override void OnStop()
    {
        this.heroComponent.OnCombatStopped -= this.OnCombatFinished;
    }

    private void OnCombatFinished(MeleeCombatOperation operation)
    {
        if (operation.targetDestroyed)
        {
            this.CurrentKills++;
            this.OnProgressChanged?.Invoke(this);
            this.TryComplete();
        }
    }

    public void Setup(int currentKills)
    {
        this.CurrentKills = currentKills;
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        this.heroService = context.GetService<HeroService>();
        this.heroComponent = this.heroService.GetHero().Get<IComponent_MeleeCombat>();
    }
}