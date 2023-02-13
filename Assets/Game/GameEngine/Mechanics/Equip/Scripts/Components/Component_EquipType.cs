using System;
using UnityEngine;


[Serializable]
public sealed class Component_EquipType : IComponent_GetEqupType
{
    public EquipType Type
    {
        get { return this.type; }
    }

    [SerializeField]
    private EquipType type;
}