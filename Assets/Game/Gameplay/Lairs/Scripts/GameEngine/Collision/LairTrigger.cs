using System;
using Entities;
using UnityEngine;
using UnityEngine.Serialization;


[RequireComponent(typeof(Collider))]
public sealed class LairTrigger : MonoBehaviour
{
    public IEntity Lair
    {
        get { return _lair; }
    }

    public ZoneType Zone
    {
        get { return _zone; }
    }

    [SerializeField]
    private UnityEntity _lair;

    [SerializeField]
    private ZoneType _zone;

    [Serializable]
    public enum ZoneType
    {
        LOAD = 0,
        UNLOAD = 1
    }
}