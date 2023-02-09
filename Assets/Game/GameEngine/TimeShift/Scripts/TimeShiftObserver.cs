using GameSystem;
using UnityEngine;


public abstract class TimeShiftObserver : MonoBehaviour,
    IGameConstructElement,
    IGameReadyElement,
    IGameFinishElement
{
    private TimeShiftEmitter _emitter;

    public virtual void ConstructGame(IGameContext context)
    {
        _emitter = context.GetService<TimeShiftEmitter>();
    }

    public virtual void ReadyGame()
    {
        _emitter.OnTimeShifted += this.OnTimeShifted;
    }

    public virtual void FinishGame()
    {
        _emitter.OnTimeShifted -= this.OnTimeShifted;
    }

    protected abstract void OnTimeShifted(TimeShiftReason reason, float shiftSeconds);
}