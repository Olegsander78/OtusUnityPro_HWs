using System;
using UnityEngine;


[Serializable]
public sealed class Component_BoosterInfo : IComponent_BoosterInfo
{
    public BoosterConfig BoosterInfo
    {
        get { return this.config; }
    }

    [SerializeField]
    private BoosterConfig config;
}