using System;
using UnityEngine;


[Serializable]
public sealed class Component_ResourcePrice : IComponent_ResourcePrice
{
    [SerializeField]
    private ResourceData[] price;

    public ResourceData[] GetPrice()
    {
        return this.price;
    }
}