using System;

public interface IComponent_HarvestResource
{
    event Action<HarvestResourceOperation> OnHarvestStarted;

    event Action<HarvestResourceOperation> OnHarvestStopped;

    bool IsHarvesting { get; }

    bool CanStartHarvest(HarvestResourceOperation operation);

    void StartHarvest(HarvestResourceOperation operation);

    void StopHarvest();
}