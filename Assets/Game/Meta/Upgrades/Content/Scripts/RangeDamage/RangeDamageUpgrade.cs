using GameElements;
using Entities;

public sealed class RangeDamageUpgrade : Upgrade,
    IGameInitElement
{
    private IEntity _hero;

    private readonly RangeDamageUpgradeConfig _config;

    public RangeDamageUpgrade(RangeDamageUpgradeConfig config) : base(config)
    {
        _config = config;
    }   
    protected override void OnUpgrade(int newLevel)
    {
        var damage = _config.RangeDamageTable.GetDamage(newLevel);
        _hero.Get<IComponent_ProjectileRangeAttack>().SetDamage(damage);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _hero = context.GetService<HeroService>().GetHero();

        var damage = _config.RangeDamageTable.GetDamage(UpgradeLevel);
        _hero.Get<IComponent_ProjectileRangeAttack>().SetDamage(damage);
    }
}