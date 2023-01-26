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
        get { return this._config.id; }
    }

    [ShowInInspector, ReadOnly]
    public bool IsActive
    {
        get { return this._countdown.IsPlaying; }
    }

    [ShowInInspector, ReadOnly]
    public float RemainingSeconds
    {
        get { return this._countdown.RemainingTime; }
    }

    [ShowInInspector, ReadOnly]
    public float DurationSeconds
    {
        get { return this._config.durationSeconds; }
    }

    [ShowInInspector, ReadOnly]
    public int MoneyPrice
    {
        get { return _config.chestRewards; }
    }

    private readonly ChestConfig _config;

    private readonly Countdown _countdown;

    public Chest(ChestConfig config, MonoBehaviour context)
    {
        this._config = config;
        this._countdown = new Countdown(context, config.durationSeconds);
    }

    public void Start()
    {
        if (this.IsActive)
        {
            throw new Exception("Already playing!");
        }

        this._countdown.OnTimeChanged += this.OnChangeTime;
        this._countdown.OnEnded += this.OnEndTime;

        this.OnStart();
        this.OnStarted?.Invoke(this);

        //this._countdown.ResetTime();
        this._countdown.Play();
    }

    protected abstract void OnStart();

    protected abstract void OnStop();

    private void OnChangeTime()
    {
        this.OnTimeChanged?.Invoke(this, this.RemainingSeconds);
    }

    private void OnEndTime()
    {
        this._countdown.OnEnded -= this.OnEndTime;
        this._countdown.OnTimeChanged -= this.OnChangeTime;

        this.OnStop();
        this.OnCompleted?.Invoke(this);
    }
}