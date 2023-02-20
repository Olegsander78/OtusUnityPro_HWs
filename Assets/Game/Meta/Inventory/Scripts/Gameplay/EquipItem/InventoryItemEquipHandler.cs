

public sealed class InventoryItemEquipHandler : IInventoryItemEquipHandler
{
    private readonly HeroService _heroService;


    public InventoryItemEquipHandler(HeroService heroService)
    {
        _heroService = heroService;
    }

    void IInventoryItemEquipHandler.OnEquip(InventoryItem item)
    {
        if (!item.TryGetComponent(out IComponent_GetEqupType equipComponent))
        {
            return;
        }        

        ActivateEffect(item);       
    }

    void IInventoryItemEquipHandler.OnUnequip(InventoryItem item)
    {
        if (!item.TryGetComponent(out IComponent_GetEqupType equipComponent))
        {
            return;
        }

        DeactivateEffect(item);
    }

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
        //var effect = item.GetComponent<IComponent_GetEffect>().Effect;
        //this.heroComponent.RemoveEffect(effect);
    }
}