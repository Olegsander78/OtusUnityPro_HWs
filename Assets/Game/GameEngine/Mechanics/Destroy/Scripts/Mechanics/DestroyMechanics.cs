using UnityEngine;


public abstract class DestroyMechanics : MonoBehaviour
{
    [SerializeField]
    private DestroyReceiver eventReceiver;

    protected virtual void OnEnable()
    {
        this.eventReceiver.OnDestroy += this.OnDestroyEvent;
    }

    protected virtual void OnDisable()
    {
        this.eventReceiver.OnDestroy -= this.OnDestroyEvent;
    }

    protected abstract void OnDestroyEvent(DestroyEvent destroyEvent);
}