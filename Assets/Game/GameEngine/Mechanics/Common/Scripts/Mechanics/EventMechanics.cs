using Elementary;
using UnityEngine;


public abstract class EventMechanics : MonoBehaviour
{
    [SerializeField]
    private EventBehaviour receiver;

    protected virtual void OnEnable()
    {
        this.receiver.OnEvent += this.OnEvent;
    }

    protected virtual void OnDisable()
    {
        this.receiver.OnEvent -= this.OnEvent;
    }

    protected abstract void OnEvent();
}