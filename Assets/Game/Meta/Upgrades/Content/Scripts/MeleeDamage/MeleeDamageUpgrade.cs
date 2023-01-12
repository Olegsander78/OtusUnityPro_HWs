using GameElements;
using Entities;

public sealed class MeleeDamageUpgrade : Upgrade,   
    IGameInitElement
{
    private IEntity _hero;

    private readonly MeleeDamageUpgradeConfig _config;
    public MeleeDamageUpgrade(MeleeDamageUpgradeConfig config) : base(config)
    {
        _config = config;
    }

    protected override void OnUpgrade(int newLevel)
    {
        var damage = _config.MeleeDamageTable.GetDamage(newLevel);
        _hero.Get<IComponent_SetMeleeDamage>().SetDamage(damage);        
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _hero = context.GetService<HeroService>().GetHero();

        var damage = _config.MeleeDamageTable.GetDamage(UpgradeLevel);
        _hero.Get<IComponent_SetMeleeDamage>().SetDamage(damage);
    }    
}