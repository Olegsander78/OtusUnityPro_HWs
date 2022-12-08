using System;
using Elementary;
using UnityEngine;


public sealed class WorkComponent : MonoBehaviour, IWorkComponent
{
    public event Action OnStartWork
    {
        add { this.timer.OnStarted += value; }
        remove { this.timer.OnStarted -= value; }
    }

    public event Action<float> OnProgress;

    public event Action OnFinishWork
    {
        add { this.timer.OnFinished += value; }
        remove { this.timer.OnFinished -= value; }
    }

    public bool IsWorking
    {
        get { return this.timer.IsPlaying; }
    }

    public float Progress
    {
        get { return this.timer.Progress; }
    }

    [SerializeField]
    private TimerBehaviour timer;

    private void OnEnable()
    {
        this.timer.OnTimeChanged += this.OnProgressChanged;
    }

    private void OnDisable()
    {
        this.timer.OnTimeChanged -= this.OnProgressChanged;
    }

    private void OnProgressChanged()
    {
        this.OnProgress?.Invoke(this.Progress);
    }
}