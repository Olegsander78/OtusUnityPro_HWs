using System;
using Elementary;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Enable»")]
public sealed class Component_Enable : MonoBehaviour, IComponent_Enable
{
    public event Action<bool> OnEnabled
    {
        add { this.isEnable.OnValueChanged += value; }
        remove { this.isEnable.OnValueChanged -= value; }
    }

    public bool IsEnable
    {
        get { return this.isEnable.Value; }
    }

    public void SetEnable(bool isEnable)
    {
        this.isEnable.Assign(isEnable);
    }

    [SerializeField]
    private BoolBehaviour isEnable;
}