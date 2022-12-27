using System;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Destroy»")]
public sealed class Component_Destroy : MonoBehaviour,
    IComponent_Destroy,
    IComponent_OnDestroyed
{
    public event Action<DestroyEvent> OnDestroyed
    {
        add { this.eventReceiver.OnDestroy += value; }
        remove { this.eventReceiver.OnDestroy -= value; }
    }

    [SerializeField]
    private DestroyReceiver eventReceiver;

    public void Destroy(DestroyEvent destroyEvent)
    {
        this.eventReceiver.Invoke(destroyEvent);
    }
}