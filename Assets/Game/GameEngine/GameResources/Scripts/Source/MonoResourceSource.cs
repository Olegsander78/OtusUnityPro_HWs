using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[AddComponentMenu("GameEngine/GameResources/Resource Source")]
public class MonoResourceSource : MonoBehaviour, IResourceSource
{
    public event Action<ResourceType, int> OnValueChanged
    {
        add { this.source.OnValueChanged += value; }
        remove { this.source.OnValueChanged -= value; }
    }

    public event Action OnSetuped
    {
        add { this.source.OnSetuped += value; }
        remove { this.source.OnSetuped -= value; }
    }

    public event Action OnCleared
    {
        add { this.source.OnCleared += value; }
        remove { this.source.OnCleared -= value; }
    }

    public int Count
    {
        get { return this.source.Count; }
    }

    [SerializeField]
    private ResourceSource source = new ResourceSource();

    public int this[ResourceType type]
    {
        get { return this.source[type]; }
        set { this.source[type] = value; }
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
    public void Setup(ResourceData[] resources)
    {
        this.source.Setup(resources);
    }

    [GUIColor(0, 1, 0)]
    [Button]
    public void Set(ResourceType type, int amount)
    {
        this.source[type] = amount;
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
    public void Clear()
    {
        this.source.Clear();
    }
}