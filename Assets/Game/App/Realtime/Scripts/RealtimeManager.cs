using System;
using Sirenix.OdinInspector;
using SystemTime;
using UnityEngine;


public sealed class RealtimeManager : MonoBehaviour
{
    public delegate void SleepDelegate(long sleepSeconds);

    public event SleepDelegate OnStarted;

    public event Action OnPaused;

    public event SleepDelegate OnResumed;

    public event Action OnEnded;

    [PropertySpace]
    [ReadOnly]
    [ShowInInspector]
    public bool IsActive
    {
        get { return this.isActive; }
    }

    [ReadOnly]
    [ShowInInspector]
    public bool IsPaused
    {
        get { return this.isPaused; }
    }

    [ReadOnly]
    [ShowInInspector]
    public long RealtimeSeconds
    {
        get { return this.realtimeSeconds; }
    }

    private bool isActive;

    private bool isPaused;

    private long realtimeSeconds;

    private float realtimeSinceStartupCache;

    private float secondAcc;

    [Title("Methods")]
    [Button]
    public void Begin(long realtimeSeconds, long sleepSeconds = 0)
    {
        if (this.isActive)
        {
            throw new Exception("Realtime manager is already active!");
        }

        this.isActive = true;
        this.isPaused = false;
        this.realtimeSeconds = realtimeSeconds;

        sleepSeconds = Math.Max(sleepSeconds, 0);
        this.OnStarted?.Invoke(sleepSeconds);
    }

    [Button]
    public void End()
    {
        if (!this.isActive)
        {
            return;
        }

        this.isActive = false;
        this.isPaused = false;
        this.OnEnded?.Invoke();
    }

    private void Update()
    {
        if (this.isActive && !this.isPaused)
        {
            this.UpdateTime(Time.deltaTime);
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!this.isActive)
        {
            return;
        }

        if (pauseStatus)
        {
            this.Pause();
        }
        else
        {
            this.Resume();
        }
    }

    private void OnApplicationQuit()
    {
        this.End();
    }

    private void UpdateTime(float deltaTime)
    {
        this.secondAcc += deltaTime;
        if (this.secondAcc < 1)
        {
            return;
        }

        var seconds = (int)this.secondAcc;
        this.secondAcc -= seconds;
        this.realtimeSeconds += seconds;
    }

    [Button]
    private void Pause()
    {
        if (this.isPaused)
        {
            return;
        }

        this.realtimeSinceStartupCache = Time.realtimeSinceStartup;
        this.isPaused = true;
        this.OnPaused?.Invoke();
    }

    [Button]
    private void Resume()
    {
        if (!this.isPaused)
        {
            return;
        }

        var sleepSeconds = (long)(Time.realtimeSinceStartup - this.realtimeSinceStartupCache);
        this.realtimeSeconds += sleepSeconds;
        this.isPaused = false;
        this.OnResumed?.Invoke(sleepSeconds);
    }
}