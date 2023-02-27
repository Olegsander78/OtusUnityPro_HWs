using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public sealed class InventoryItem
{
    public string Name
    {
        get { return this.name; }
    }

    public InventoryItemFlags Flags
    {
        get { return this.flags; }
    }

    public InventoryItemMetadata Metadata
    {
        get { return this.metadata; }
    }

    [PropertyOrder(-10)]
    [SerializeField]
    private string name;

    [PropertyOrder(-9)]
    [SerializeField]
    private InventoryItemFlags flags;

    [PropertyOrder(-8)]
    [SerializeField]
    private InventoryItemMetadata metadata;

    [Space]
    [SerializeField]
    private object[] components;

    public InventoryItem()
    {
        this.name = string.Empty;
        this.components = new object[0];
    }

    public InventoryItem(
        string name,
        InventoryItemFlags flags,
        InventoryItemMetadata metadata,
        params object[] components
    )
    {
        this.name = name;
        this.flags = flags;
        this.metadata = metadata;
        this.components = components;
    }

    public T GetComponent<T>()
    {
        for (int i = 0, count = this.components.Length; i < count; i++)
        {
            var component = this.components[i];
            if (component is T result)
            {
                return result;
            }
        }
        
        throw new Exception($"Component {typeof(T).Name} is not found!");
    }

    public T[] GetComponents<T>()
    {
        var result = new List<T>();
        for (int i = 0, count = this.components.Length; i < count; i++)
        {
            var component = this.components[i];
            if (component is T tComponent)
            {
                result.Add(tComponent);
            }
        }

        return result.ToArray();
    }

    public object[] GetAllComponents()
    {
        return this.components;
    }

    public bool TryGetComponent<T>(out T result)
    {
        for (int i = 0, count = this.components.Length; i < count; i++)
        {
            var component = this.components[i];
            if (component is T tComponent)
            {
                result = tComponent;
                return true;
            }
        }

        result = default;
        return false;
    }

    public InventoryItem Clone()
    {
        return new InventoryItem(
            this.name,
            this.flags,
            this.metadata,
            this.CloneComponents()
        );
    }

    private object[] CloneComponents()
    {
        var count = this.components.Length;
        var result = new object[count];

        for (var i = 0; i < count; i++)
        {
            var component = this.components[i];
            if (component is ICloneable cloneable)
            {
                component = cloneable.Clone();
            }

            result[i] = component;
        }

        return result;
    }
}