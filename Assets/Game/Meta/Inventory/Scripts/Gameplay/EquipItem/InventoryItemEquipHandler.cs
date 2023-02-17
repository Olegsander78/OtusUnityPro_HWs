

//public sealed class InventoryItemEquipHandler : IInventoryItemEquipHandler
//{
//    //private readonly HeroService _heroService;

//    private InventoryItemEquipper _equipper;

//    public InventoryItemEquipHandler(InventoryItemEquipper equipper)
//    {
//        //_heroService = heroService;
//        _equipper = equipper;
//    }

//    void IInventoryItemEquipHandler.OnEquip(InventoryItem item)
//    {
//        if (!item.TryGetComponent(out IComponent_GetEqupType equipComponent))
//        {
//            return;
//        }

//        var typeItem = equipComponent.Type;

//        if (_equipper.Equipment[typeItem] == null)
//        {
//            _equipper.Equipment[typeItem] = item;
//        }
//        else
//        {
//            _equipper.UnequipItem(typeItem);
//            _equipper.Equipment[typeItem] = item;
//        }
//    }
//}