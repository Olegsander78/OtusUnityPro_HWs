using System;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Harvest Resource»")]
public sealed class Component_HarvestResource : MonoBehaviour, IComponent_HarvestResource
{
    public event Action<HarvestResourceOperation> OnHarvestStarted
    {
        add { this.harvestEngine.OnHarvestStarted += value; }
        remove { this.harvestEngine.OnHarvestStarted -= value; }
    }

    public event Action<HarvestResourceOperation> OnHarvestStopped
    {
        add { this.harvestEngine.OnHarvestStopped += value; }
        remove { this.harvestEngine.OnHarvestStopped -= value; }
    }

    public bool IsHarvesting
    {
        get { return this.harvestEngine.IsHarvesting; }
    }

    [SerializeField]
    private HarvestResourceEngine harvestEngine;

    public bool CanStartHarvest(HarvestResourceOperation operation)
    {
        return this.harvestEngine.CanStartHarvest(operation);
    }

    public void StartHarvest(HarvestResourceOperation operation)
    {
        this.harvestEngine.StartHarvest(operation);
    }

    public void StopHarvest()
    {
        this.harvestEngine.StopHarvest();
    }
}