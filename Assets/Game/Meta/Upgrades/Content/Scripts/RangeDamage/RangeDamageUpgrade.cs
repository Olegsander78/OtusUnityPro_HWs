using GameSystem;
using Entities;

public sealed class RangeDamageUpgrade : Upgrade,
    IGameConstructElement
{
    private IEntity _hero;

    private readonly RangeDamageUpgradeConfig _config;

    public RangeDamageUpgrade(RangeDamageUpgradeConfig config) : base(config)
    {
        _config = config;
    }

    public override string CurrentStats
    {
        get { return _config.RangeDamageTable.GetDamage(UpgradeLevel).ToString(); }
    }

    public override string NextImprovement
    {
        get { return _config.RangeDamageTable.DamageStep.ToString(); }
    }

    protected override void UpgradeUp(int newLevel)
    {
        var damage = _config.RangeDamageTable.GetDamage(newLevel);
        _hero.Get<IComponent_ProjectileRangeAttack>().SetDamage(damage);
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _hero = context.GetService<HeroService>().GetHero();

        var damage = _config.RangeDamageTable.GetDamage(UpgradeLevel);
        _hero.Get<IComponent_ProjectileRangeAttack>().SetDamage(damage);
    }
}