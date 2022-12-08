using System;
using Elementary;
using UnityEngine;


[AddComponentMenu("Gameplay/Conveyors/Component «Conveyor Load Zone»")]
public sealed class Component_LoadZone : MonoBehaviour, IComponent_LoadZone
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

    public int AvailableAmount
    {
        get { return this.storage.MaxValue - this.storage.Value; }
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

    [Space]
    [SerializeField]
    private LimitedIntBehavior storage;

    public void SetupAmount(int currentAmount)
    {
        this.storage.Value = currentAmount;
    }

    public void PutAmount(int range)
    {
        this.storage.Value += range;
    }
}