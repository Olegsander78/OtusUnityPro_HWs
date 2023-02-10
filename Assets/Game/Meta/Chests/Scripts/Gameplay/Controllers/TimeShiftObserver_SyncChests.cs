

using UnityEngine;

public sealed class TimeShiftObserver_SyncChests : ITimeShiftListener
{
    private readonly ChestsManager _chestsManager;

    public TimeShiftObserver_SyncChests(ChestsManager chestsManager)
    {
        _chestsManager = chestsManager;
    }

    void ITimeShiftListener.OnTimeShifted(TimeShiftReason reason, float shiftSeconds)
    {
        SyncChests(shiftSeconds);
    }

    private void SyncChests(float shiftSeconds)
    {
        var chests = _chestsManager.GetActiveChests();
        for (int i = 0, count = chests.Length; i < count; i++)
        {
            var chest = chests[i];

            if (chest.RemainingSeconds > shiftSeconds)
            {
                chest.RemainingSeconds -= shiftSeconds;
                Debug.Log($"{chest.Config.ChestType}: {chest.RemainingSeconds}, {shiftSeconds} - Remaining > shifted time");
            }
            else
            {
                chest.RemainingSeconds = 0f;
                Debug.Log($"{chest.Config.ChestType}: {shiftSeconds} - Remaining < shifted time");
            }                
        }
    }
}