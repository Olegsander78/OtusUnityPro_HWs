

public sealed class InventoryItemConsumeHandler_ActivateBooster : IInventoryItemConsumeHandler
{
    private readonly BoostersManager boostersManager;

    public InventoryItemConsumeHandler_ActivateBooster(BoostersManager boostersManager)
    {
        this.boostersManager = boostersManager;
    }

    void IInventoryItemConsumeHandler.OnConsume(InventoryItem item)
    {
        if (item.TryGetComponent(out IComponent_BoosterInfo boosterComponent))
        {
            BoosterConfig boosterConfig = boosterComponent.BoosterInfo;
            this.boostersManager.LaunchBooster(boosterConfig);
        }
    }
}