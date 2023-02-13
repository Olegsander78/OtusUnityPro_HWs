using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;


[Serializable]
public sealed class Product
{
    public string Id
    {
        get { return this.id; }
    }

    public IProductMetadata Metadata
    {
        get { return this.metadata; }
    }

    [SerializeField]
    private string id;

    [OdinSerialize]
    private IProductMetadata metadata;

    [Space]
    [SerializeReference]
    private object[] components;

    public Product()
    {
        this.id = string.Empty;
        this.components = new object[0];
    }

    public Product(string id, params object[] components)
    {
        this.id = id;
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
}