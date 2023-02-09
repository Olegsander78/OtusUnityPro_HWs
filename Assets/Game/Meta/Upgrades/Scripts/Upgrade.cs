using GameSystem;
using Services;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public abstract class Upgrade
{
    public event Action<int> OnUpgradeUp;

    [ReadOnly]
    [ShowInInspector]
    public string Id
    {
        get { return _config.Id; }
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
        get { return _config.MaxLevel; }
    }
    [ReadOnly]
    [ShowInInspector]
    public bool IsMaxUpgradeLevel
    {
        get { return _currentUpgradeLevel == MaxUpgradeLevel; }
    }

    public float Progress
    {
        get { return (float)_currentUpgradeLevel / _config.MaxLevel; }
    }

    [ReadOnly]
    [ShowInInspector]
    public UpgradeMetadata Metadata
    {
        get { return _config.Metadata; }
    }

    [ReadOnly]
    [ShowInInspector]
    public abstract string CurrentStats { get; }

    [ReadOnly]
    [ShowInInspector]
    public abstract string NextImprovement { get; }

    [ReadOnly]
    [ShowInInspector]
    public int NextPrice
    {
        get { return _config.PriceTable.GetPrice(UpgradeLevel + 1); }
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

    public void SetupLevel(int level)
    {
        _currentUpgradeLevel = level;
    }

    public void LevelUp()
    {
        if (UpgradeLevel >= MaxUpgradeLevel)
        {
            throw new Exception("Max level is reached!");
        }

        var nextLevel = UpgradeLevel + 1;
        _currentUpgradeLevel = nextLevel;
        //_currentUpgradeLevel++;
        UpgradeUp(_currentUpgradeLevel);
        OnUpgradeUp?.Invoke(nextLevel);
    }

    protected abstract void UpgradeUp(int newLevel);

}