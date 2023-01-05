using Elementary;
using UnityEngine;


public sealed class HarvestResourceMechanics_ByTimer : MonoBehaviour
{
    [SerializeField]
    private HarvestResourceEngineLS engine;

    [SerializeField]
    private TimerBehaviour countdown;

    // private float speed;

    private void OnEnable()
    {
        this.engine.OnStarted += this.OnHarvestStarted;
        this.engine.OnStopped += this.OnHarvestFinished;
        this.countdown.OnFinished += this.OnTimerFinished;
    }

    private void OnDisable()
    {
        this.engine.OnStarted -= this.OnHarvestStarted;
        this.engine.OnStopped -= this.OnHarvestFinished;
        this.countdown.OnFinished -= this.OnTimerFinished;
    }

    private void OnHarvestStarted(HarvestResourceOperation operation)
    {
        this.countdown.ResetTime();
        this.countdown.Play();
    }

    private void OnHarvestFinished(HarvestResourceOperation operation)
    {
        this.countdown.Stop();
    }

    private void OnTimerFinished()
    {
        this.engine.CurrentOperation.IsCompleted = true;
        this.engine.StopHarvest();
    }
}