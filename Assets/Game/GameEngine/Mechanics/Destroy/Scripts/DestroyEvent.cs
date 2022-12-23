using System;
using UnityEngine;


[Serializable]
public struct DestroyEvent
{
    [SerializeField]
    public DestroyReason reason;

    [SerializeField]
    public object source;

    public DestroyEvent(DestroyReason reason, object source = null)
    {
        this.reason = reason;
        this.source = source;
    }
}