using Game.GameEngine;


public sealed class TimeShiftObserver_SyncBoosters : ITimeShiftListener
{
    private readonly BoostersManager boostersManager;

    public TimeShiftObserver_SyncBoosters(BoostersManager boostersManager)
    {
        this.boostersManager = boostersManager;
    }

    void ITimeShiftListener.OnTimeShifted(TimeShiftReason reason, float shiftSeconds)
    {
        this.SyncBoosters(shiftSeconds);
    }

    private void SyncBoosters(float shiftSeconds)
    {
        var boosters = this.boostersManager.GetActiveBoosters();
        for (int i = 0, count = boosters.Length; i < count; i++)
        {
            var booster = boosters[i];
            booster.RemainingTime -= shiftSeconds;
        }
    }
}