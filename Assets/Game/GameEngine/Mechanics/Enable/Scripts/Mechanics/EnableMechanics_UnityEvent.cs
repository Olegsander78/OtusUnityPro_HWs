using UnityEngine;
using UnityEngine.Events;


public sealed class EnableMechanics_UnityEvent : EnableMechanics
{
    [Space]
    [SerializeField]
    private UnityEvent onEnable;

    [SerializeField]
    private UnityEvent onDisable;

    protected override void SetEnable(bool isEnable)
    {
        if (isEnable)
        {
            this.onEnable?.Invoke();
        }
        else
        {
            this.onDisable?.Invoke();
        }
    }
}