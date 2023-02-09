using Services;
using Sirenix.OdinInspector;
using UnityEngine;
using GameSystem;


public class ChestTimeSinchronizer : MonoBehaviour,
    IGameConstructElement,
    ITimeShiftListener,
    //IGameInitElement,
    IGameReadyElement,
    IGameFinishElement
{    
    private ChestsManager _chestsManager;

    private TimeShifter _timeShifter;


    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _chestsManager = context.GetService<ChestsManager>();
        _timeShifter = context.GetService<TimeShifter>();
    }

    //void IGameInitElement.InitGame()
    //{
    
    //}

    //void ITimeShiftListener.OnTimeShifted(float secondOffset)
    //{
    //    foreach (var chest in _chestsManager.GetActiveChests())
    //    {
    //        chest.RemainingSeconds -= secondOffset;
    //        Debug.Log($"{chest.Id} shifted ");
    //    }
    //}

    void IGameReadyElement.ReadyGame()
    {
        _timeShifter.AddListener(this);
    }

    void IGameFinishElement.FinishGame()
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
