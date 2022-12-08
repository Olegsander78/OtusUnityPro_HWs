using System;
using Elementary;
using UnityEngine;


[AddComponentMenu("Gameplay/Conveyors/Component «Conveyor Unload Zone»")]
public sealed class Component_UnloadZone : MonoBehaviour, IComponent_UnloadZone
{
    public event Action<int> OnAmountChanged
    {
        add { this.storage.OnValueChanged += value; }
        remove { this.storage.OnValueChanged -= value; }
    }

    public int MaxAmount
    {
        get { return this.storage.MaxValue; }
    }

    public int CurrentAmount
    {
        get { return this.storage.Value; }
    }

    public bool IsFull
    {
        get { return this.storage.IsLimit; }
    }

    public bool IsEmpty
    {
        get { return this.storage.Value <= 0; }
    }

    public ResourceType ResourceType { get; set; }

    public Vector3 ParticlePosition
    {
        get { return this.particlePoint.position; }
    }

    [Space]
    [SerializeField]
    private LimitedIntBehavior storage;

    [SerializeField]
    private Transform particlePoint;

    public void SetupAmount(int currentAmount)
    {
        this.storage.Value = currentAmount;
    }

    public int ExtractAll()
    {
        var resources = this.storage.Value;
        this.storage.Value = 0;
        return resources;
    }
}