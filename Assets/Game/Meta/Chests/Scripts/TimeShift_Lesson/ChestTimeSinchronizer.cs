using Services;
using Sirenix.OdinInspector;
using UnityEngine;
using GameElements;


public class ChestTimeSinchronizer : MonoBehaviour, 
    ITimeShiftListener,
    IGameInitElement,
    IGameReadyElement,
    IGameFinishElement
{    
    private ChestsManager _chestsManager;

    private TimeShifter _timeShifter;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _chestsManager = context.GetService<ChestsManager>();
        _timeShifter = context.GetService<TimeShifter>();
    }

    //void ITimeShiftListener.OnTimeShifted(float secondOffset)
    //{
    //    foreach (var chest in _chestsManager.GetActiveChests())
    //    {
    //        chest.RemainingSeconds -= secondOffset;
    //        Debug.Log($"{chest.Id} shifted ");
    //    }
    //}

    void IGameReadyElement.ReadyGame(IGameContext context)
    {
        _timeShifter.AddListener(this);
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _timeShifter.RemoveListener(this);
    }

    void ITimeShiftListener.OnTimeShifted(TimeShiftReason reason, float shiftSeconds)
    {
        foreach (var chest in _chestsManager.GetActiveChests())
        {
            chest.RemainingSeconds -= shiftSeconds;
            Debug.Log($"{chest.Id} shifted ");
        }
    }
}
