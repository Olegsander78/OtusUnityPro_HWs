using System;
using UnityEngine;


[Serializable]
public sealed class Component_HealingPoints : IComponent_GetHealingPoints
{
    public int HealingPoints
    {
        get { return this.healingPoints; }
    }

    [SerializeField]
    private int healingPoints;
}