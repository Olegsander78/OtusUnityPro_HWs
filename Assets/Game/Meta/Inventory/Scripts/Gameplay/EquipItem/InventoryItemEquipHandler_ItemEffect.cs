

public sealed class InventoryItemEquipHandler_ItemEffect : IInventoryItemEquipHandler
{
    private readonly HeroService _heroService;


    public InventoryItemEquipHandler_ItemEffect(HeroService heroService)
    {
        _heroService = heroService;
    }

    void IInventoryItemEquipHandler.OnEquip(InventoryItem item) => ActivateEffect(item);

    void IInventoryItemEquipHandler.OnUnequip(InventoryItem item) => DeactivateEffect(item);

    private void ActivateEffect(InventoryItem item)
    {
        if( _heroService.GetHero().TryGet(out IComponent_Effector heroComponent))
        {
            if(item.TryGetComponent(out IComponent_GetEffect component_GetEffect))
            {
                var effect = component_GetEffect.Effect;
                heroComponent.AddEffect(effect);
            }
        }
    }

    private void DeactivateEffect(InventoryItem item)
    {
        if (_heroService.GetHero().TryGet(out IComponent_Effector heroComponent))
        {
            if (item.TryGetComponent(out IComponent_GetEffect component_GetEffect))
            {
                var effect = component_GetEffect.Effect;
                heroComponent.RemoveEffect(effect);
            }
        }
    }
}