using GameElements;
using Services;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public abstract class Upgrade
{
    [ReadOnly]
    [ShowInInspector]
    public string Id
    {
        get { return _config.id; }
    }

    [ReadOnly]
    [ShowInInspector]
    public int UpgradeLevel
    {
        get { return _currentUpgradeLevel; }
        set { _currentUpgradeLevel = value; }
    }
    [ReadOnly]
    [ShowInInspector]
    public int MaxUpgradeLevel
    {
        get { return _config.maxLevel; }
    }
    [ReadOnly]
    [ShowInInspector]
    public bool IsMaxUpgradeLevel
    {
        get { return _currentUpgradeLevel == MaxUpgradeLevel; }
    }

    [ReadOnly]
    [ShowInInspector]
    public int NextPrice
    {
        get { return _config.priceTable.GetPrice(UpgradeLevel + 1); }
    }

    [ReadOnly]
    [ShowInInspector]
    private readonly UpgradeConfig _config;

    [ReadOnly]
    [ShowInInspector]
    private int _currentUpgradeLevel;

    public Upgrade(UpgradeConfig config)
    {
        _currentUpgradeLevel = 1;
        _config = config;
    }

    //[Inject]
    //public void Construct(HeroService heroService)
    //{
    //    _heroService = heroService;
    //}

    //void IGameConstructElement.ConstructGame(IGameContext context)
    //{
    //    _heroService = context.GetService<HeroService>();
    //}
    //void IGameInitElement.InitGame(IGameContext context)
    //{
    //    _component_OnLevelChanged = context.GetService<HeroService>().GetHero().Get<IComponent_OnLevelChanged>();
    //    _component_GetLevel = context.GetService<HeroService>().GetHero().Get<IComponent_GetLevel>();
    //    CurrentMaxLevel = _component_GetLevel.Level;
    //}
    //void IGameStartElement.StartGame(IGameContext context)
    //{
    //    _component_OnLevelChanged.OnLevelChanged += UpdateCurrentMaxLevel;
    //}

    //void IGameFinishElement.FinishGame(IGameContext context)
    //{
    //    _component_OnLevelChanged.OnLevelChanged -= UpdateCurrentMaxLevel;
    //}

    //private void UpdateCurrentMaxLevel(int currentLevel)
    //{
    //    //currentLevel = _component_GetLevel.Level;
    //    Debug.Log($"Level {currentLevel}");
    //    _currentMaxLevel = currentLevel;
    //    Debug.Log($"Level {_currentMaxLevel}");
    //}
    public void LevelUp()
    {
        if (IsMaxUpgradeLevel)
        {
            throw new Exception("Max level is reached!");
        }

        _currentUpgradeLevel++;
        OnUpgrade(_currentUpgradeLevel);
    }

    protected abstract void OnUpgrade(int newLevel);

}