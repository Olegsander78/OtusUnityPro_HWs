

public sealed class HarvestResourceAction_AddResourcesToStorage : IHarvestResourceAction
{
    private readonly ResourceStorage storage;

    public HarvestResourceAction_AddResourcesToStorage(ResourceStorage storage)
    {
        this.storage = storage;
    }

    public void Do(HarvestResourceOperation operation)
    {
        if (operation.IsCompleted)
        {
            var resource = operation.TargetResource;
            var resourceType = resource.Get<IComponent_GetResourceType>().ResourceType;
            var resourceAmount = resource.Get<IComponent_GetResourceCount>().ResourceCount;
            this.storage.AddResource(resourceType, resourceAmount);
        }
    }
}