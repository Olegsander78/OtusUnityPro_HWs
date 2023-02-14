using System;
using Elementary;
using Sirenix.OdinInspector;
using UnityEngine;


public abstract class Booster
{
    public event Action<Booster> OnStarted;

    public event Action<Booster> OnTimeChanged;

    public event Action<Booster> OnStopped;

    public event Action<Booster> OnEnded;

    [ReadOnly]
    [ShowInInspector]
    public string Id
    {
        get { return this.config.id; }
    }

    [ReadOnly]
    [ShowInInspector]
    public bool IsActive
    {
        get { return this.timer.IsPlaying; }
    }

    [ReadOnly]
    [ShowInInspector]
    public float RemainingTime
    {
        get { return this.timer.RemainingTime; }
        set { this.timer.RemainingTime = value; }
    }

    [ReadOnly]
    [ShowInInspector]
    public float Duration
    {
        get { return this.timer.Duration; }
    }

    [ReadOnly]
    [ShowInInspector]
    [ProgressBar(0, 1)]
    public float Progress
    {
        get { return this.timer.Progress; }
    }

    [ReadOnly]
    [ShowInInspector]
    public BoosterMetadata Metadata
    {
        get { return this.config.metadata; }
    }

    private readonly BoosterConfig config;

    private readonly Countdown timer;

    public Booster(BoosterConfig config, MonoBehaviour context)
    {
        this.config = config;
        this.timer = new Countdown(context, config.duration);
    }

    public void Start()
    {
        if (this.timer.IsPlaying)
        {
            throw new Exception("Timer is already started!");
        }

        this.timer.OnTimeChanged += this.OnChangeTime;
        this.timer.OnEnded += this.OnEnd;

        this.OnStart();
        this.OnStarted?.Invoke(this);

        this.timer.Play();
    }

    public void Stop()
    {
        if (!this.timer.IsPlaying)
        {
            return;
        }

        this.timer.OnEnded -= this.OnEnd;
        this.timer.OnTimeChanged -= this.OnChangeTime;
        this.timer.Stop();

        this.OnStop();
        this.OnStopped?.Invoke(this);
    }

    protected abstract void OnStart();

    protected abstract void OnStop();

    private void OnEnd()
    {
        this.timer.OnEnded -= this.OnEnd;
        this.timer.OnTimeChanged -= this.OnChangeTime;

        this.OnStop();
        this.OnEnded?.Invoke(this);
    }

    private void OnChangeTime()
    {
        this.OnTimeChanged?.Invoke(this);
    }
}