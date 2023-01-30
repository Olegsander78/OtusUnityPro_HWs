using System;
using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;


public class Chest
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
        get { return _config.PriceOpen; }
    }

    [ShowInInspector, ReadOnly]
    public ChestConfig Config
    {
        get { return _config; }
    }

    [ShowInInspector, ReadOnly]
    public ChestRewardConfig Reward
    {
        get { return _reward; }
        set { _reward = value; }
    }

    public float NormalizedProgress
    {
        get {return _countdown.RemainingTime / _config.DurationSeconds; }
    }

    private readonly ChestConfig _config;

    private readonly Countdown _countdown;

    private ChestRewardConfig _reward;

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

    protected virtual void OnStart() { 
    }

    protected virtual void OnStop()
    {

    }

    private void OnChangeTime()
    {
        OnTimeChanged?.Invoke(this, RemainingSeconds);
    }

    private void OnEndTime()
    {
        _countdown.OnEnded -= OnEndTime;
        _countdown.OnTimeChanged -= OnChangeTime;

        OnCompleted?.Invoke(this);
    }
}