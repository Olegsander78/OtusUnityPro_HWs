using GameElements;
using UnityEngine;
using Entities;

public sealed class SpeedUpgrade : Upgrade,
    IGameInitElement
{
    private IEntity _hero;

    private readonly SpeedUpgradeConfig _config;

    public SpeedUpgrade(SpeedUpgradeConfig config) : base(config)
    {
        _config = config;
    }

    protected override void OnUpgrade(int newLevel)
    {
        var speed = _config.SpeedTable.GetSpeed(newLevel);
        _hero.Get<IComponent_SetMoveSpeed>().SetSpeed(speed);        
    }

    void IGameInitElement.InitGame(IGameContext context)
    {     
        _hero = context.GetService<HeroService>().GetHero();

        var speed = _config.SpeedTable.GetSpeed(UpgradeLevel);
         _hero.Get<IComponent_SetMoveSpeed>().SetSpeed(speed);

        Debug.Log("Init speedUpgrade components");
    }
}