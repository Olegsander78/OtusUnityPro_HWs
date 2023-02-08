using Game.GameEngine;
using Services;


public sealed class RealtimeSynchronizer :
    IAppStartListener,
    IAppStopListener,
    IGameLoadDataListener
{
    [Inject]
    private RealtimeManager realtimeManager;
        
    private TimeShiftEmitter timeShiftEmitter;

    void IAppStartListener.Start()
    {
        this.realtimeManager.OnStarted += this.OnSessionStarted;
        this.realtimeManager.OnResumed += this.OnSessionResumed;
    }

    void IAppStopListener.Stop()
    {
        this.realtimeManager.OnStarted -= this.OnSessionStarted;
        this.realtimeManager.OnResumed -= this.OnSessionResumed;
    }

    void IGameLoadDataListener.OnLoadData(GameManager gameManager)
    {
        this.timeShiftEmitter = gameManager.GetService<TimeShiftEmitter>();
    }

    private void OnSessionStarted(long pauseSeconds)
    {
        if (pauseSeconds > 0)
        {
            this.timeShiftEmitter.EmitEvent(TimeShiftReason.START_GAME, pauseSeconds);
        }
    }

    private void OnSessionResumed(long pauseSeconds)
    {
        if (pauseSeconds > 0)
        {
            this.timeShiftEmitter.EmitEvent(TimeShiftReason.RESUME_GAME, pauseSeconds);
        }
    }
}