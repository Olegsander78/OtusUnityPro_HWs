using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("GameEngine/GameResources/Resource Source «Limited»")]
public sealed class MonoResourceSource_Limited : MonoBehaviour, IResourceSource
{
    public event Action<int> OnLimitChanged;

    public event Action<ResourceType, int> OnValueChanged
    {
        add { this.source.OnValueChanged += value; }
        remove { this.source.OnValueChanged -= value; }
    }

    public event Action OnSetuped;

    public event Action OnCleared
    {
        add { this.source.OnCleared += value; }
        remove { this.source.OnCleared -= value; }
    }

    [PropertySpace]
    [PropertyOrder(-10)]
    [ReadOnly]
    [ShowInInspector]
    public int AvailableCount
    {
        get { return this.limit - this.Count; }
    }

    [PropertyOrder(-9)]
    [ReadOnly]
    [ShowInInspector]
    public int Count
    {
        get { return this.source.Count; }
    }

    public int Limit
    {
        get { return this.limit; }
    }

    [PropertyOrder(-8)]
    [ReadOnly]
    [ShowInInspector]
    public bool IsLimit
    {
        get { return this.Count >= this.limit; }
    }

    [Title("Fields")]
    [SerializeField]
    private int limit;

    [SerializeField]
    private ResourceSource source = new ResourceSource();

    public int this[ResourceType type]
    {
        get { return this.source[type]; }
    }

    public ResourceData[] GetAll()
    {
        return this.source.GetAll();
    }

    public void GetAllNonAlloc(Dictionary<ResourceType, int> result)
    {
        this.source.GetAllNonAlloc(result);
    }

    public void GetAllNonAlloc(List<ResourceData> result)
    {
        this.source.GetAllNonAlloc(result);
    }

    [Title("Methods")]
    [GUIColor(0, 1, 0)]
    [Button]
    public void Setup(ResourceData[] resources, int limit)
    {
        this.source.Setup(resources);
        this.limit = limit;
        this.OnSetuped?.Invoke();
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public void Setup(ResourceData[] resources)
    {
        this.source.Setup(resources);
        this.OnSetuped?.Invoke();
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public bool Exists(ResourceType type, int requiredCount)
    {
        return this.source.Exists(type, requiredCount);
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public void Plus(ResourceType type, int range)
    {
        if (range <= 0)
        {
            return;
        }

        var resourceCount = this.Count;
        if (resourceCount >= this.limit)
        {
            return;
        }

        var newCount = resourceCount + range;
        if (newCount > this.limit)
        {
            range = this.limit - resourceCount;
        }

        this.source.Plus(type, range);
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public void Minus(ResourceType type, int range)
    {
        this.source.Minus(type, range);
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public void SetLimit(int limit)
    {
        this.limit = limit;
        this.OnLimitChanged?.Invoke(limit);
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public void Clear()
    {
        this.source.Clear();
    }
}