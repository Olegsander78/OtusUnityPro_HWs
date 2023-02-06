using GameElements;
using UnityEngine;


public abstract class TimeShiftObserver : MonoBehaviour,
    IGameConstructElement,
    IGameReadyElement,
    IGameFinishElement
{
    private TimeShiftEmitter emitter;

    public virtual void ConstructGame(IGameContext context)
    {
        this.emitter = context.GetService<TimeShiftEmitter>();
    }

    public virtual void ReadyGame(IGameContext context)
    {
        this.emitter.OnTimeShifted += this.OnTimeShifted;
    }

    public virtual void FinishGame(IGameContext context)
    {
        this.emitter.OnTimeShifted -= this.OnTimeShifted;
    }

    protected abstract void OnTimeShifted(TimeShiftReason reason, float shiftSeconds);
}