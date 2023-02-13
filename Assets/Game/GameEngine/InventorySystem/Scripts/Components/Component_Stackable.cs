using System;
using UnityEngine;


[Serializable]
public sealed class Component_Stackable : IComponent_Stackable, ICloneable
{
    public event Action<int> OnValueChanged;

    public int Value
    {
        get { return this.value; }
        set { this.SetValue(value); }
    }

    public int Size
    {
        get { return this.size; }
        set { this.size = value; }
    }

    public bool IsFull
    {
        get { return this.value >= this.size; }
    }

    [SerializeField]
    private int size;

    private int value = 0;

    private void SetValue(int value)
    {
        value = Mathf.Clamp(value, 0, this.size);
        this.value = value;
        this.OnValueChanged?.Invoke(value);
    }

    public Component_Stackable()
    {
    }

    public Component_Stackable(int size)
    {
        this.size = size;
    }

    public Component_Stackable(int size, int value)
    {
        this.size = size;
        this.value = Mathf.Clamp(value, 0, size);
    }

    public object Clone()
    {
        return new Component_Stackable
        {
            value = this.value,
            size = this.size
        };
    }
}