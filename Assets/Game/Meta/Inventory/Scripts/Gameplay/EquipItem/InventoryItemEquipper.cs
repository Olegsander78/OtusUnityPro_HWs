using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;


public class InventoryItemEquipper 
    //IGameConstructElement,
    //IGameInitElement,
    //IGameStartElement
{
    public event Action<InventoryItem> OnItemEquipped;

    public event Action<InventoryItem> OnItemUnequipped;

    private StackableInventory inventory;

    private HeroService heroService;

    private IComponent_Effector heroComponent;

    //private InventoryService service;

    //private StackableInventory inventory;

    [PropertySpace(8)]
    [ReadOnly]
    [ShowInInspector]
    private Dictionary<EquipType, InventoryItem> _equipment = new();

    public void SetInventory(StackableInventory inventory)
    {
        this.inventory = inventory;
    }

    public InventoryItemEquipper(StackableInventory inventory)
    {
        this.inventory = inventory;
    }

    public InventoryItemEquipper()
    {
    }

    public void Construct(HeroService heroService)
    {
        this.heroService = heroService;
        this.heroComponent = this.heroService.GetHero().Get<IComponent_Effector>();

        InitEquipment();
    }

    //void IGameInitElement.InitGame()
    //{
    //    this.heroComponent = this.heroService.GetHero().Get<IComponent_Effector>();        
    //}


    //void IGameConstructElement.ConstructGame(IGameContext context)
    //{
    //    this.heroService = context.GetService<HeroService>();
    //    this.service = context.GetService<InventoryService>();

    //    inventory = service.GetInventory();
    //    this.heroComponent = this.heroService.GetHero().Get<IComponent_Effector>();
    //}


    //void IGameStartElement.StartGame()
    //{
    //    InstallEquipment();
    //}

    [Button]
    [GUIColor(0, 1, 0)]
    public bool CanEquipItem(InventoryItem item)
    {
        return item.FlagsExists(InventoryItemFlags.EQUIPPABLE) &&
               this.inventory.IsItemExists(item);
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void EquipItem(InventoryItemConfig item)
    {
        if (item.Prototype == null)
            return;

        //if (!CanEquipItem(item))
        //{
        //    throw new Exception($"Can not equip item {item.Prototype.Name}!");
        //}

        var typeItem = item.Prototype.GetComponent<IComponent_GetEqupType>().Type;

        if (_equipment[typeItem] == null)
        {
            _equipment[typeItem] = item.Prototype;            
        }
        else
        {
            UnequipItem(typeItem);
            _equipment[typeItem] = item.Prototype;
        }
            

        if (item.Prototype.FlagsExists(InventoryItemFlags.EFFECTIBLE))
        {
            this.ActivateEffect(item.Prototype);
        }

        inventory.RemoveItem(item.Prototype);
        OnItemEquipped?.Invoke(item.Prototype);
    }

    public void EquipItem(InventoryItem item)
    {
        if (item == null)
            return;

        if (!CanEquipItem(item))
        {
            throw new Exception($"Can not equip item {item.Name}!");
        }

        var typeItem = item.GetComponent<IComponent_GetEqupType>().Type;

        if (_equipment[typeItem] == null)
        {
            _equipment[typeItem] = item;
        }
        else
        {
            UnequipItem(typeItem);
            _equipment[typeItem] = item;
        }


        if (item.FlagsExists(InventoryItemFlags.EFFECTIBLE))
        {
            this.ActivateEffect(item);
        }

        inventory.RemoveItem(item);
        OnItemEquipped?.Invoke(item);
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void UnequipItem(EquipType type)
    {
        if(_equipment.TryGetValue(type , out var equipItem))
        {
            if (equipItem.FlagsExists(InventoryItemFlags.EFFECTIBLE))
            {
                this.DeactivateEffect(equipItem);
            }

            inventory.AddItemAsInstance(equipItem);            

            OnItemUnequipped?.Invoke(equipItem);

            _equipment[type] = null;
        }
    }

    private void ActivateEffect(InventoryItem item)
    {
        var effect = item.GetComponent<IComponent_GetEffect>().Effect;
        this.heroComponent.AddEffect(effect);
    }

    private void DeactivateEffect(InventoryItem item)
    {
        var effect = item.GetComponent<IComponent_GetEffect>().Effect;
        this.heroComponent.RemoveEffect(effect);
    }

    
    public void InitEquipment()
    {
        foreach (EquipType equipType in Enum.GetValues(typeof(EquipType)))
        {
            if (!IsItemEquipped(equipType))
                _equipment[equipType] = null;
        }
    }
    public bool IsItemEquipped(EquipType type)
    {
        return _equipment.ContainsKey(type);
    }

    public InventoryItem GetEquippedItem(EquipType equipType)
    {
        if (_equipment.TryGetValue(equipType, out var item))
        {
            return item;
        }

        throw new Exception($"Item {equipType} is'n equipped!");
    }

    public InventoryItem[] GetEquippedItems()
    {
        return _equipment.Values.ToArray();
    }
}