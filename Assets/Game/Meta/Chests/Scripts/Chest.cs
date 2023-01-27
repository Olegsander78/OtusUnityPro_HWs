using System;
using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;


public abstract class Chest
{
    public event Action<Chest> OnStarted;

    public event Action<Chest, float> OnTimeChanged;

    public event Action<Chest> OnCompleted;

    [ShowInInspector, ReadOnly]
    public string Id
    {
        get { return _config.Id; }
    }

    [ShowInInspector, ReadOnly]
    public bool IsActive
    {
        get { return _countdown.IsPlaying; }
    }

    [ShowInInspector, ReadOnly]
    public float RemainingSeconds
    {
        get { return _countdown.RemainingTime; }
    }

    [ShowInInspector, ReadOnly]
    public float DurationSeconds
    {
        get { return _config.DurationSeconds; }
    }

    [ShowInInspector, ReadOnly]
    public int EarlyOpeningPrice
    {
        get { return _config.ChestMetadata.PriceOpen; }
    }

    [ShowInInspector, ReadOnly]
    public ChestConfig Config
    {
        get { return _config; }
    }

    private readonly ChestConfig _config;

    private readonly Countdown _countdown;

    public Chest(ChestConfig config, MonoBehaviour context)
    {
        _config = config;
        _countdown = new Countdown(context, config.DurationSeconds);
    }

    public void Start()
    {
        if (IsActive)
        {
            throw new Exception("It's not time yet!");
        }

        _countdown.OnTimeChanged += OnChangeTime;
        _countdown.OnEnded += OnEndTime;

        OnStart();
        OnStarted?.Invoke(this);

        _countdown.Reset();
        _countdown.Play();
    }

    protected abstract void OnStart();

    protected abstract void OnStop();
    
    [Button]
    protected virtual ChestRewardConfig GetReward()
    {
        int totalDropWeight = 0;

        foreach (var dropWeigt in _config.ChestRewardConfigs)
        {
            totalDropWeight += dropWeigt.DropChance;
        }

        int randomPoint = (int)UnityEngine.Random.value * totalDropWeight;

        for (int i = 0; i < _config.ChestRewardConfigs.Length; i++)
        {
            if (randomPoint < _config.ChestRewardConfigs[i].DropChance)
            {
                return _config.ChestRewardConfigs[i].RewardConfig;
            }
            else
            {
                randomPoint -= _config.ChestRewardConfigs[i].DropChance;
            }
        }

        return _config.ChestRewardConfigs[_config.ChestRewardConfigs.Length - 1].RewardConfig;
    }

    private void OnChangeTime()
    {
        OnTimeChanged?.Invoke(this, RemainingSeconds);
    }

    private void OnEndTime()
    {
        _countdown.OnEnded -= OnEndTime;
        _countdown.OnTimeChanged -= OnChangeTime;

        OnStop();
        OnCompleted?.Invoke(this);
    }

    internal void Stop()
    {
        throw new NotImplementedException();
    }
}