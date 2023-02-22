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

    public Component_EquipType()
    {
            
    }

    public Component_EquipType(EquipType type)
    {
        this.type = type;   
    }
}