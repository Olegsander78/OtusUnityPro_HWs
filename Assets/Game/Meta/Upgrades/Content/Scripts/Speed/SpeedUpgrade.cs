using Services;
using GameElements;


public sealed class SpeedUpgrade : Upgrade,
    IGameInitElement
{
    //private HeroService _heroService;

    private readonly SpeedUpgradeConfig _config;

    public SpeedUpgrade(SpeedUpgradeConfig config) : base(config)
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
        var speed = _config.SpeedTable.GetSpeed(newLevel);
        HeroService.GetHero().Get<IComponent_SetMoveSpeed>().SetSpeed(speed);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        var speed = _config.SpeedTable.GetSpeed(Level);
        HeroService.GetHero().Get<IComponent_SetMoveSpeed>().SetSpeed(speed);
    }
}