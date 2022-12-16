using System;
using Elementary;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Collider Sensor»")]
public sealed class Component_ColliderSensor : MonoBehaviour
{
    public event Action OnCollisionsUpdated
    {
        add { this.sensor.OnCollidersUpdated += value; }
        remove { this.sensor.OnCollidersUpdated -= value; }
    }

    [SerializeField]
    private ColliderSensor sensor;

    public void GetCollidersNonAlloc(Collider[] buffer, out int size)
    {
        this.sensor.GetCollidersNonAlloc(buffer, out size);
    }

    public void GetCollidersUnsafe(out Collider[] buffer, out int size)
    {
        this.sensor.GetCollidersUnsafe(out buffer, out size);
    }
}