using System;
using UnityEngine;

public interface IComponent_TriggerEvents
{
    event Action<Collider> OnEntered;

    event Action<Collider> OnStaying;

    event Action<Collider> OnExited;
}
