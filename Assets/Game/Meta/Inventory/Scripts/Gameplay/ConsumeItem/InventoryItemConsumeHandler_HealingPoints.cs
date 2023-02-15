

public sealed class InventoryItemConsumeHandler_HealingPoints : IInventoryItemConsumeHandler
{
    private readonly HeroService heroService;

    public InventoryItemConsumeHandler_HealingPoints(HeroService heroService)
    {
        this.heroService = heroService;
    }

    void IInventoryItemConsumeHandler.OnConsume(InventoryItem item)
    {
        if (!item.TryGetComponent(out IComponent_GetHealingPoints healingComponent))
        {
            return;
        }

        if (!this.heroService.GetHero().TryGet(out IComponent_AddHitPoints hitPointsComponent))
        {
            return;
        }

        var healingPoints = healingComponent.HealingPoints;
        hitPointsComponent.AddHitPoints(healingPoints);
    }
}