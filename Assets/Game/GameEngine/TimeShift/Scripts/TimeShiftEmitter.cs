using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class TimeShiftEmitter : MonoBehaviour
{
    public event TimeShiftDelegate OnTimeShifted;

    [SerializeField]
    private bool isEnable = true;

    private readonly List<ITimeShiftListener> listeners = new();

    [Button]
    [ShowIf("isEnable")]
    [GUIColor(0, 1, 0)]
    public void EmitEvent(TimeShiftReason reason, float shiftSeconds)
    {
        if (!this.isEnable)
        {
            return;
        }

        for (int i = 0, count = this.listeners.Count; i < count; i++)
        {
            var listener = this.listeners[i];
            listener.OnTimeShifted(reason, shiftSeconds);
        }

        this.OnTimeShifted?.Invoke(reason, shiftSeconds);
    }

    public void AddListener(ITimeShiftListener listener)
    {
        this.listeners.Add(listener);
    }

    public void RemoveListener(ITimeShiftListener listener)
    {
        this.listeners.Remove(listener);
    }
}