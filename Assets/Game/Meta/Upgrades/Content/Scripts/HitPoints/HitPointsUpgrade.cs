using Services;
using GameElements;
using UnityEngine;
using System;
using Entities;

public sealed class HitPointsUpgrade : Upgrade,
    IGameInitElement
{
    private IEntity _hero;

    private readonly HitPointsUpgradeConfig _config;

    public HitPointsUpgrade(HitPointsUpgradeConfig config) : base(config)
    {
        _config = config;
    }

    protected override void OnUpgrade(int newLevel)
    {
        var hitpoints = _config.HitPointsTable.GetHitPoints(newLevel);
        //_hero.Get<IComponent_SetHitPoints>().SetHitPoints(hitpoints);
        _hero.Get<IComponent_SetMaxHitPoints>().SetMaxHitPoints(hitpoints);
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _hero = context.GetService<HeroService>().GetHero();

        var hitpoints = _config.HitPointsTable.GetHitPoints(UpgradeLevel);
        //_hero.Get<IComponent_SetHitPoints>().SetHitPoints(hitpoints);
        _hero.Get<IComponent_SetMaxHitPoints>().SetMaxHitPoints(hitpoints);
    }
}