using System;
using Entities;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(Collider))]
public sealed class ConveyorTrigger : MonoBehaviour
{
    public IEntity Conveyor
    {
        get { return this.conveyor; }
    }

    public ZoneType Zone
    {
        get { return this.zone; }
    }

    [SerializeField]
    private UnityEntity conveyor;

    [SerializeField]
    private ZoneType zone;

    [Serializable]
    public enum ZoneType
    {
        LOAD = 0,
        UNLOAD = 1
    }
}