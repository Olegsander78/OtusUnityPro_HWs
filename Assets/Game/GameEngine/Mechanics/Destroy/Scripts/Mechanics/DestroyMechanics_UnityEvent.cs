using UnityEngine;
using UnityEngine.Events;


public sealed class DestroyMechanics_UnityEvent : DestroyMechanics
{
    [SerializeField]
    private UnityEvent<DestroyEvent> unityEvent;

    protected override void OnDestroyEvent(DestroyEvent destroyEvent)
    {
        this.unityEvent.Invoke(destroyEvent);
    }
}