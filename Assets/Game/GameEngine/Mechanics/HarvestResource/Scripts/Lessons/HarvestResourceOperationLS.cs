using Entities;

public sealed class HarvestResourceOperationLS
{
    public readonly IEntity TargetResource;

    public bool IsCompleted;

    public HarvestResourceOperationLS(IEntity targetResource)
    {
        TargetResource = targetResource;
    }
}