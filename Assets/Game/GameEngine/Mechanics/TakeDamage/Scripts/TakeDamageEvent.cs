using System;
using UnityEngine;


[Serializable]
public struct TakeDamageEvent
{
    [SerializeField]
    public int damage;

    [SerializeField]
    public TakeDamageReason reason;

    [SerializeField]
    public object source;

    public TakeDamageEvent(int damage, TakeDamageReason reason, object source = null)
    {
        this.damage = damage;
        this.reason = reason;
        this.source = source;
    }
}