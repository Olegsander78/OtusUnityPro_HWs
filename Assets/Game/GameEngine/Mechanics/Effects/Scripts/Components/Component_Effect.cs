using System;
using UnityEngine;


[Serializable]
public sealed class Component_Effect : IComponent_GetEffect
{
    public IEffect Effect
    {
        get { return this.effect; }
    }

    [SerializeReference]
    private IEffect effect = default;
}