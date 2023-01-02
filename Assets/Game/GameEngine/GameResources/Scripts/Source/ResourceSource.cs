using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public sealed class ResourceSource : IResourceSource
{
    public event Action<ResourceType, int> OnValueChanged;

    public event Action OnSetuped;

    public event Action OnCleared;

    public int Count
    {
        get { return this.GetCount(); }
    }

    [SerializeField]
    private List<ResourceData> resources = new List<ResourceData>();

    private List<ResourceData> buffer = new List<ResourceData>();

    public int this[ResourceType type]
    {
        get { return this.Get(type); }
        set { this.Set(type, value); }
    }

    public void Setup(ResourceData[] resources)
    {
        this.resources.Clear();

        for (int i = 0, count = resources.Length; i < count; i++)
        {
            var resource = resources[i];
            this.resources.Add(resource);
        }

        this.OnSetuped?.Invoke();
    }

    public void GetAllNonAlloc(Dictionary<ResourceType, int> result)
    {
        result.Clear();
        for (int i = 0, count = this.resources.Count; i < count; i++)
        {
            var data = this.resources[i];
            var amount = data.amount;
            if (amount > 0)
            {
                result[data.type] = amount;
            }
        }
    }

    public void GetAllNonAlloc(List<ResourceData> result)
    {
        result.Clear();
        for (int i = 0, count = this.resources.Count; i < count; i++)
        {
            var data = this.resources[i];
            if (data.amount > 0)
            {
                result.Add(data);
            }
        }
    }

    public ResourceData[] GetAll()
    {
        this.buffer.Clear();

        for (int i = 0, count = this.resources.Count; i < count; i++)
        {
            var data = this.resources[i];
            if (data.amount > 0)
            {
                this.buffer.Add(data);
            }
        }

        return this.buffer.ToArray();
    }

    public bool Exists(ResourceType type, int requiredCount)
    {
        var currentAmount = this.Get(type);
        return currentAmount >= requiredCount;
    }

    public void Plus(ResourceType type, int range)
    {
        if (range <= 0)
        {
            return;
        }

        var previousCount = this.Get(type);
        var newCount = previousCount + range;
        this.Set(type, newCount);
    }

    public void Minus(ResourceType type, int range)
    {
        if (range <= 0)
        {
            return;
        }

        var previousCount = this.Get(type);
        var newCount = previousCount - range;
        newCount = Math.Max(newCount, 0);
        this.Set(type, newCount);
    }

    public void Clear()
    {
        this.resources.Clear();
        this.OnCleared?.Invoke();
    }

    private int Get(ResourceType type)
    {
        for (int i = 0, count = this.resources.Count; i < count; i++)
        {
            var resource = this.resources[i];
            if (resource.type == type)
            {
                return resource.amount;
            }
        }

        return 0;
    }

    private void Set(ResourceType type, int newAmount)
    {
        if (newAmount < 0)
        {
            throw new Exception($"Set negative amount {newAmount} of resource type {type}!");
        }

        var result = new ResourceData(type, newAmount);

        for (int i = 0, count = this.resources.Count; i < count; i++)
        {
            var resource = this.resources[i];
            if (resource.type != type)
            {
                continue;
            }

            this.resources[i] = result;
            this.OnValueChanged?.Invoke(type, newAmount);
            return;
        }

        this.resources.Add(result);
        this.OnValueChanged?.Invoke(type, newAmount);
    }

    private int GetCount()
    {
        var result = 0;
        for (int i = 0, count = this.resources.Count; i < count; i++)
        {
            var resource = this.resources[i];
            result += resource.amount;
        }

        return result;
    }
}