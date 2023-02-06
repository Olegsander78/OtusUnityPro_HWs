using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;


public sealed class TimeShifter: MonoBehaviour
{
    private List<ITimeShiftListener> _listeners = new();

    [Button]
    public void ShiftTime(TimeShiftReason reason, float secondsOffset)
    {
        foreach (var shiftTimer in _listeners)
        {
            shiftTimer.OnTimeShifted(reason, secondsOffset);

            Debug.Log($"{secondsOffset} - Time shifted ");
        }
    }

    public void AddListener(ITimeShiftListener shiftListener)
    {
        _listeners.Add(shiftListener);
    }

    public void RemoveListener(ITimeShiftListener shiftListener)
    {
        _listeners.Remove(shiftListener);
    }
}

