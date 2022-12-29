using Elementary;
using System;
using UnityEngine;

[AddComponentMenu("GameEngine/Mechanics/Component «Collision Events»")]
public sealed class Component_CollisionEvents : MonoBehaviour,
    IComponent_CollisionEvents
{
    public event Action<Collision> OnCollisionEntered
    {
        add { _eventReceiver.OnCollisionEntered += value; }
        remove { _eventReceiver.OnCollisionEntered -= value; }
    }

    public event Action<Collision> OnCollisionStaying
    {
        add { _eventReceiver.OnCollisionStaying += value; }
        remove { _eventReceiver.OnCollisionStaying -= value; }
    }

    public event Action<Collision> OnCollisionExited
    {
        add { _eventReceiver.OnCollisionExited += value; }
        remove { _eventReceiver.OnCollisionExited -= value; }
    }

    [SerializeField]
    private EventReceiver_Collision _eventReceiver;
}