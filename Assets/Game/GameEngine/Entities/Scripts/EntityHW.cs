using System;
using UnityEngine;

public class EntityHW : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour[] components;

    public T Get<T>()
    {
        for (int i = 0, count = this.components.Length; i < count; i++)
        {
            var component = this.components[i];
            if (component is T result)
            {
                return result;
            }
        }

        throw new Exception($"Component of type {typeof(T).Name} is not found!");
    }

    public bool TryGet<T>(out T result)
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
