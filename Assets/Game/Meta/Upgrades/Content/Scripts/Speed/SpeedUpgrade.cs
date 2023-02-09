using GameSystem;
using UnityEngine;
using Entities;

public sealed class SpeedUpgrade : Upgrade,
    IGameConstructElement
{
    private IEntity _hero;

    private readonly SpeedUpgradeConfig _config;

    public SpeedUpgrade(SpeedUpgradeConfig config) : base(config)
    {
        _config = config;
    }
    public override string CurrentStats
    {
        get { return _config.SpeedTable.GetSpeed(UpgradeLevel).ToString(); }
    }

    public override string NextImprovement
    {
        get { return _config.SpeedTable.SpeedStep.ToString(); }
    }

    protected override void UpgradeUp(int newLevel)
    {
        var speed = _config.SpeedTable.GetSpeed(newLevel);
        _hero.Get<IComponent_SetMoveSpeed>().SetSpeed(speed);        
    }

    void IGameConstructElement.ConstructGame(IGameContext context)
    {     
        _hero = context.GetService<HeroService>().GetHero();

        var speed = _config.SpeedTable.GetSpeed(UpgradeLevel);
         _hero.Get<IComponent_SetMoveSpeed>().SetSpeed(speed);
    }
}