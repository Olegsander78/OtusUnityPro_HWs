using System;
using UnityEngine;


[AddComponentMenu("GameEngine/Mechanics/Component «Harvest Resource»")]
public sealed class Component_HarvestResource : MonoBehaviour, IComponent_HarvestResource
{
    public event Action<HarvestResourceOperation> OnHarvestStarted
    {
        add { this.harvestEngine.OnStarted += value; }
        remove { this.harvestEngine.OnStarted -= value; }
    }

    public event Action<HarvestResourceOperation> OnHarvestStopped
    {
        add { this.harvestEngine.OnStopped += value; }
        remove { this.harvestEngine.OnStopped -= value; }
    }

    public bool IsHarvesting
    {
        get { return this.harvestEngine.IsHarvesting; }
    }

    [SerializeField]
    private HarvestResourceEngineLS harvestEngine;

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