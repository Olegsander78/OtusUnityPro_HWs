using System;
using UnityEngine;


[AddComponentMenu("Gameplay/Player/Player Resource Storage")]
public sealed class ResourceStorage : MonoBehaviour
{
    public event Action<ResourceType, int> OnResourceChanged
    {
        add { this.source.OnValueChanged += value; }
        remove { this.source.OnValueChanged -= value; }
    }

    public event Action<ResourceType, int> OnResourceAdded;

    public event Action<ResourceType, int> OnResourceExtracted;

    [SerializeField]
    private MonoResourceSource source;

    public void AddResource(ResourceType resourceType, int range)
    {
        this.source.Plus(resourceType, range);
        this.OnResourceAdded?.Invoke(resourceType, range);
    }

    public void ExtractResource(ResourceType type, int range)
    {
        this.source.Minus(type, range);
        this.OnResourceExtracted?.Invoke(type, range);
    }

    public ResourceData[] GetAllResources()
    {
        return this.source.GetAll();
    }

    public void Setup(ResourceData[] resources)
    {
        this.source.Setup(resources);
    }

    public int GetResource(ResourceType type)
    {
        return this.source[type];
    }
}