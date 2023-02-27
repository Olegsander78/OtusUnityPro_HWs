using System;
using System.Collections.Generic;
using Game.GameEngine;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class EquipmentItemListPresenter : MonoBehaviour, IGameConstructElement
{

    [SerializeField]
    private List<EquipmentSlot> equipmentSlots;

    private List<KeyValuePair<EquipType, InventoryItem>> equipmentList = new();

    private InventoryService inventoryService;

    private PopupManager popupManager;

    private InventoryItemConsumer consumeManager;

    private InventoryItemEquipper equipperManager;

    [PropertySpace(8)]
    [ReadOnly]
    [ShowInInspector]
    private readonly Dictionary<InventoryItem, ViewHolder> items = new();

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        this.inventoryService = context.GetService<InventoryService>();
        this.popupManager = context.GetService<PopupManager>();
        this.consumeManager = context.GetService<InventoryItemConsumer>();
        this.equipperManager = context.GetService<InventoryItemEquipper>();
    }

    public void Show()
    {
        this.equipmentList.Clear();
        this.equipmentList.AddRange(this.equipperManager.GetEquippedItems());
        Debug.Log($"{equipmentList.Count} - amount items equipped");
        this.equipperManager.OnItemEquipped += SetupEquippedItem;
        this.equipperManager.OnItemUnequipped += RemoveUnquippedItem;

        for (int i = 0, count = this.equipmentList.Count; i < count; i++)
        {
            KeyValuePair<EquipType, InventoryItem> equippedItem = this.equipmentList[i];
            SetupEquippedItem(equippedItem.Key, equippedItem.Value);
        }
    }

    public void Hide()
    {
        this.equipmentList.Clear();

        this.equipperManager.OnItemEquipped -= SetupEquippedItem;
        this.equipperManager.OnItemUnequipped -= RemoveUnquippedItem;

        //var inventoryItems = playerInventory.GetAllItems();
        //for (int i = 0, count = playerEquipment.Length; i < count; i++)
        //{
        //    var equippedItem = playerEquipment[i];
        //    UnquipItem(equippedItem);
        //}
    }

    private void SetupEquippedItem(EquipType type, InventoryItem item)
    {
        if (item == null)
            return;
        
        if (this.items.ContainsKey(item))
        {
            return;
        }


        //var typeItem = item.GetComponent<IComponent_GetEqupType>().Type;

        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            if(type == equipmentSlots[i].TypeEquipment)
            {
                
                var view = Instantiate(equipmentSlots[i].Prefab, this.equipmentSlots[i].Container);
                var presenter = new InventoryItemViewPresenter(view, item);
                presenter.Construct(this.popupManager, this.consumeManager, this.equipperManager);

                var viewHolder = new ViewHolder(view, presenter);
                this.items.Add(item, viewHolder);

                presenter.Start();
            }            
        }
    }


    private void RemoveUnquippedItem(InventoryItem item)
    {
        if (item == null)
            return;

        if (!this.items.ContainsKey(item))
        {
            return;
        }

        var viewHolder = this.items[item];
        viewHolder.presenter.Stop();
        Destroy(viewHolder.view.gameObject);
        this.items.Remove(item);
    }



    private sealed class ViewHolder
    {
        public readonly InventoryItemView view;
        public readonly InventoryItemViewPresenter presenter;

        public ViewHolder(InventoryItemView view, InventoryItemViewPresenter presenter)
        {
            this.view = view;
            this.presenter = presenter;
        }
    }

    [Serializable]
    private sealed class EquipmentSlot
    {
        [SerializeField]
        public EquipType TypeEquipment;

        [SerializeField]
        public InventoryItemView Prefab;

        [SerializeField]
        public Transform Container;

        [SerializeField]
        public InventoryItemViewPresenter Presenter;
        //public EquipmentItemViewPresenter Presenter;
    }
}