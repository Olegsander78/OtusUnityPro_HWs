using System;
using UnityEngine;


[Serializable]
public sealed class Component_MoneyPrice : IComponent_MoneyPrice
{
    public int Price
    {
        get { return this.price; }
    }

    [SerializeField]
    private int price;
}