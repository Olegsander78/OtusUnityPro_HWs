using GameElements;
using Services;
using System;
using UnityEngine;

public abstract class Upgrade:
    IGameConstructElement,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    public string Id
    {
        get { return _config.id; }
    }

    public int Level
    {
        get { return _currentLevel; }
        set { _currentLevel = value; }
    }

    public int CurrentMaxLevel
    {
        get { return _currentMaxLevel; }
        set { _currentMaxLevel = value; }
    }
    public bool IsCurrentMaxLevel
    {
        get { return _currentLevel == CurrentMaxLevel; }
    }

    public int MaxLevel
    {
        get { return _config.maxLevel; }
    }

    public bool IsMaxLevel
    {
        get { return _currentLevel == MaxLevel; }
    }

    public HeroService HeroService { get => _heroService; private set => _heroService = value; }

    private HeroService _heroService;

    private IComponent_GetLevel _component_GetLevel;
    
    private IComponent_OnLevelChanged _component_OnLevelChanged;

    private readonly UpgradeConfig _config;

    private int _currentLevel;

    private int _currentMaxLevel;

    public Upgrade(UpgradeConfig config)
    {
        _currentLevel = 1;
        _currentMaxLevel = 1;
        _config = config;
    }

    //[Inject]
    //public void Construct(HeroService heroService)
    //{
    //    _heroService = heroService;
    //}

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _heroService = context.GetService<HeroService>();
    }
    void IGameInitElement.InitGame(IGameContext context)
    {
        _component_OnLevelChanged = context.GetService<HeroService>().GetHero().Get<IComponent_OnLevelChanged>();
        _component_GetLevel = context.GetService<HeroService>().GetHero().Get<IComponent_GetLevel>();
        CurrentMaxLevel = _component_GetLevel.Level;
    }
    void IGameStartElement.StartGame(IGameContext context)
    {
        _component_OnLevelChanged.OnLevelChanged += UpdateCurrentMaxLevel;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _component_OnLevelChanged.OnLevelChanged -= UpdateCurrentMaxLevel;
    }

    private void UpdateCurrentMaxLevel(int currentLevel)
    {
        //currentLevel = _component_GetLevel.Level;
        Debug.Log($"Level {currentLevel}");
        _currentMaxLevel = currentLevel;
        Debug.Log($"Level {_currentMaxLevel}");
    }
    public void LevelUp()
    {
        if (IsMaxLevel || IsCurrentMaxLevel)
        {
            throw new Exception("Max level is reached!");
        }

        _currentLevel++;
        OnUpgrade(_currentLevel);
    }

    protected abstract void OnUpgrade(int newLevel);

}