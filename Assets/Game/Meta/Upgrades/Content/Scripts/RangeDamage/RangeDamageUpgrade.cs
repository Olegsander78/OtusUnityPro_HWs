using Services;
using GameElements;


public sealed class RangeDamageUpgrade : Upgrade,
    IGameInitElement
{
    //private HeroService _heroService;

    private readonly RangeDamageUpgradeConfig _config;

    public RangeDamageUpgrade(RangeDamageUpgradeConfig config) : base(config)
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
        var damage = _config.RangeDamageTable.GetDamage(newLevel);
        HeroService.GetHero().Get<IComponent_ProjectileRangeAttack>().SetDamage(damage);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        var damage = _config.RangeDamageTable.GetDamage(Level);
        HeroService.GetHero().Get<IComponent_ProjectileRangeAttack>().SetDamage(damage);
    }
}