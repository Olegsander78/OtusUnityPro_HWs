using Services;
using GameElements;


public sealed class HitPointsUpgrade : Upgrade,
    IGameInitElement
{
    private HeroService _heroService;

    private readonly HitPointsUpgradeConfig _config;

    public HitPointsUpgrade(HitPointsUpgradeConfig config) : base(config)
    {
        _config = config;
    }

    //[Inject]
    //public void Construct(HeroService heroService)
    //{
    //    _heroService = heroService;
    //}

    protected override void OnUpgrade(int newLevel)
    {
        var hitpoints = _config.HitPointsTable.GetHitPoints(newLevel);
        _heroService.GetHero().Get<IComponent_SetHitPoints>().SetHitPoints(hitpoints);
        _heroService.GetHero().Get<IComponent_SetMaxHitPoints>().SetMaxHitPoints(hitpoints);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        var hitpoints = _config.HitPointsTable.GetHitPoints(Level);
        _heroService.GetHero().Get<IComponent_SetHitPoints>().SetHitPoints(hitpoints);
        _heroService.GetHero().Get<IComponent_SetMaxHitPoints>().SetMaxHitPoints(hitpoints);
    }
}