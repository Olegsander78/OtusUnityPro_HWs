using Services;
using GameElements;


public sealed class MeleeDamageUpgrade : Upgrade,
    IGameInitElement
{
    private HeroService _heroService;

    private readonly MeleeDamageUpgradeConfig _config;

    public MeleeDamageUpgrade(MeleeDamageUpgradeConfig config) : base(config)
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
        var damage = _config.MeleeDamageTable.GetDamage(newLevel);
        _heroService.GetHero().Get<IComponent_SetMeleeDamage>().SetDamage(damage);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        var damage = _config.MeleeDamageTable.GetDamage(Level);
        _heroService.GetHero().Get<IComponent_SetMeleeDamage>().SetDamage(damage);
    }
}