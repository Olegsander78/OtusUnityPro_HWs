using Elementary;
using UnityEngine;


public sealed class InfoWidgetAdapter : MonoBehaviour
{
    [SerializeField]
    private TimerBehaviour workTimer;

    [SerializeField]
    private InfoWidget view;

    private void Awake()
    {
        this.view.SetVisible(true);
        this.view.ProgressBar.SetVisible(this.workTimer.IsPlaying);
    }

    private void OnEnable()
    {
        this.workTimer.OnStarted += this.OnWorkStarted;
        this.workTimer.OnTimeChanged += this.OnWorkProgressChanged;
        this.workTimer.OnFinished += this.OnWorkFinished;
    }

    private void OnDisable()
    {
        this.workTimer.OnStarted -= this.OnWorkStarted;
        this.workTimer.OnTimeChanged -= this.OnWorkProgressChanged;
        this.workTimer.OnFinished -= this.OnWorkFinished;
    }

    private void OnWorkStarted()
    {
        this.view.ProgressBar.SetVisible(true);
    }

    private void OnWorkProgressChanged()
    {
        this.view.ProgressBar.SetProgress(this.workTimer.Progress);
    }

    private void OnWorkFinished()
    {
        this.view.ProgressBar.SetVisible(false);
    }
}