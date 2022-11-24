using Elementary;
using System;
using UnityEngine;

[AddComponentMenu("GameEngine/Mechanics/Component «Trigger Events»")]
public sealed class Component_TriggerEvents : MonoBehaviour, IComponent_TriggerEvents
{
    public event Action<Collider> OnEntered
    {
        add { _eventReceiver.OnTriggerEntered += value; }
        remove { _eventReceiver.OnTriggerExited -= value; }
    }

    public event Action<Collider> OnStaying
    {
        add { _eventReceiver.OnTriggerStaying += value; }
        remove { _eventReceiver.OnTriggerStaying -= value; }
    }

    public event Action<Collider> OnExited
    {
        add { _eventReceiver.OnTriggerExited += value; }
        remove { _eventReceiver.OnTriggerExited -= value; }
    }

    [SerializeField]
    private EventReceiver_Trigger _eventReceiver;
}
