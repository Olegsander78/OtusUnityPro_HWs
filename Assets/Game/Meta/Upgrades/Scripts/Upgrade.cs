using System;

public abstract class Upgrade
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

    private readonly UpgradeConfig _config;

    private int _currentLevel;

    private int _currentMaxLevel;

    public Upgrade(UpgradeConfig config)
    {
        _currentLevel = 1;
        _currentMaxLevel = 1;
        _config = config;
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