//using GameSystem;
//using Services;
//using UnityEngine;


//public sealed class InventoryItemsEffectController : InventoryObserver,
//    IGameInitElement,
//    IGameStartElement
//{
//    private HeroService heroService;

//    private IComponent_Effector heroComponent;

//    public void Construct(HeroService heroService)
//    {
//        this.heroService = heroService;
//    }

//    void IGameInitElement.InitGame()
//    {
//        this.heroComponent = this.heroService.GetHero().Get<IComponent_Effector>();
//    }

//    void IGameStartElement.StartGame()
//    {
//        //this.ActivateExisitingEffects();
//    }

//    protected override void OnItemAdded(InventoryItem item)
//    {
//        //if (item.FlagsExists(InventoryItemFlags.EFFECTIBLE))
//        //{
//        //    this.ActivateEffect(item);
//        //}
//    }

//    protected override void OnItemRemoved(InventoryItem item)
//    {
//        //if (item.FlagsExists(InventoryItemFlags.EFFECTIBLE))
//        //{
//        //    this.DeactivateEffect(item);
//        //}
//    }

//    private void ActivateEffect(InventoryItem item)
//    {
//        var effect = item.GetComponent<IComponent_GetEffect>().Effect;
//        this.heroComponent.AddEffect(effect);
//    }

//    private void DeactivateEffect(InventoryItem item)
//    {
//        var effect = item.GetComponent<IComponent_GetEffect>().Effect;
//        this.heroComponent.RemoveEffect(effect);
//    }

//    private void ActivateExisitingEffects()
//    {
//        //var items = this.inventory.FindItems(InventoryItemFlags.EFFECTIBLE);
//        //for (int i = 0, count = items.Length; i < count; i++)
//        //{
//        //    var item = items[i];
//        //    this.ActivateEffect(item);
//        //}
//    }
//}