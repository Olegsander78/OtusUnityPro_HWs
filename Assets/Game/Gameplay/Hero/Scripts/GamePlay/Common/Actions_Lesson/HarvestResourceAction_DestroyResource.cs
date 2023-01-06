
public sealed class HarvestResourceAction_DestroyResource : IHarvestResourceAction
{
    public void Do(HarvestResourceOperation operation)
    {
        if (operation.IsCompleted)
        {
            var resource = operation.TargetResource;
            resource.Get<IComponent_Collect>().Collect();
        }
    }
}