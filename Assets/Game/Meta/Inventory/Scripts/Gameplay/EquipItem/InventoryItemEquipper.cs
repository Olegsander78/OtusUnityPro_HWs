using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;


public class InventoryItemEquipper 
{
    public event Action<EquipType, InventoryItem> OnItemEquipped;

    public event Action<InventoryItem> OnItemUnequipped;

    private StackableInventory inventory;

    private readonly List<IInventoryItemEquipHandler> _handlers = new();
    public Dictionary<EquipType, InventoryItem> Equipment { get => _equipment; set => _equipment = value; }

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
        InitEquipment();
    }
    public void AddHandler(IInventoryItemEquipHandler handler)
    {
        _handlers.Add(handler);
    }

    public void RemoveHandler(IInventoryItemEquipHandler handler)
    {
        _handlers.Remove(handler);
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public bool CanEquipItem(InventoryItem item)
    {
        Debug.Log($"{InventoryItemFlags.EQUIPPABLE} -Flag, {inventory.IsItemExists(item)} - in inventory?");
        return item.FlagsExists(InventoryItemFlags.EQUIPPABLE) &&
               this.inventory.IsItemExists(item);
    }

    public void EquipItem(InventoryItem item)
    {
        if (item == null)
            return;

        if (!CanEquipItem(item))
        {
            throw new Exception($"Can not equip item {item.Name}!");
        }

        this.inventory.RemoveItem(item);


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

        if (_handlers.Count > 0))
        {
            for (int i = 0, count = _handlers.Count; i < count; i++)
            {
                var handler = _handlers[i];
                handler.OnEquip(item);
            }
        }

        OnItemEquipped?.Invoke(typeItem, item);
    }

    [Button]
    [GUIColor(0, 1, 0)]
    public void UnequipItem(EquipType type)
    {
        if(_equipment.TryGetValue(type , out var equipItem))
        {
            if (_handlers.Count > 0)
            {
                for (int i = 0, count = _handlers.Count; i < count; i++)
                {
                    var handler = _handlers[i];
                    handler.OnUnequip(equipItem);
                }
            }

            inventory.AddItemAsInstance(equipItem);            

            OnItemUnequipped?.Invoke(equipItem);

            _equipment[type] = null;
        }
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
        return _equipment.TryGetValue(type, out var item);
    }

    //public bool IsItemEquipped(EquipType type)
    //{
    //    return _equipment.ContainsKey(type);
    //}

    public InventoryItem GetEquippedItem(EquipType equipType)
    {
        if (_equipment.TryGetValue(equipType, out var item))
        {
            return item;
        }

        throw new Exception($"Item {equipType} is'n equipped!");
    }

    public List<KeyValuePair<EquipType, InventoryItem>> GetEquippedItems()
    {
        var result = new List<KeyValuePair<EquipType, InventoryItem>>();

        foreach (var pair in _equipment)
        {
            if(pair.Value != null)
            {
                result.Add(pair);
            }
        }

        return result;
    }
}