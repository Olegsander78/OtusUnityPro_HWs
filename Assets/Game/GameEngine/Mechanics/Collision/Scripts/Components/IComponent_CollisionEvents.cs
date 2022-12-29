using System;
using UnityEngine;


public interface IComponent_CollisionEvents
{
    event Action<Collision> OnCollisionEntered;

    event Action<Collision> OnCollisionStaying;

    event Action<Collision> OnCollisionExited;
}