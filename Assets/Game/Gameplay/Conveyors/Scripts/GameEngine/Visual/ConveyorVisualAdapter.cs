using Elementary;
using UnityEngine;


public sealed class ConveyorVisualAdapter : MonoBehaviour
{
    [SerializeField]
    private TimerBehaviour workTimer;

    [SerializeField]
    private ConveyorVisual conveyor;

    private void OnEnable()
    {
        this.workTimer.OnStarted += this.OnStartWork;
        this.workTimer.OnFinished += this.OnFinishWork;
    }

    private void OnDisable()
    {
        this.workTimer.OnStarted -= this.OnStartWork;
        this.workTimer.OnFinished -= this.OnFinishWork;
    }

    private void OnStartWork()
    {
        this.conveyor.Play();
    }

    private void OnFinishWork()
    {
        this.conveyor.Stop();
    }
}