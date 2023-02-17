using System;
using System.Collections.Generic;
using Game.GameEngine;
using GameSystem;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class EquipmentItemListPresenter : MonoBehaviour, IGameConstructElement
{
    //[SerializeField]
    //private InventoryItemView prefab;

    //[SerializeField]
    //private Transform container;

    [SerializeField]
    private List<EquipmentSlot> equipmentSlots;

    private List<InventoryItem> equipmentList = new();

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
        this.equipperManager.OnItemEquipped += EquipItem;
        this.equipperManager.OnItemUnequipped += UnquipItem;

        //var inventoryItems = playerEquipment.GetAllItems();
        for (int i = 0, count = this.equipmentList.Count; i < count; i++)
        {
            var equippedItem = this.equipmentList[i];
            EquipItem(equippedItem);
        }
    }

    public void Hide()
    {
        this.equipmentList.Clear();

        this.equipperManager.OnItemEquipped -= EquipItem;
        this.equipperManager.OnItemUnequipped -= UnquipItem;

        //var inventoryItems = playerInventory.GetAllItems();
        //for (int i = 0, count = playerEquipment.Length; i < count; i++)
        //{
        //    var equippedItem = playerEquipment[i];
        //    UnquipItem(equippedItem);
        //}
    }

    private void EquipItem(InventoryItem item)
    {
        if (this.items.ContainsKey(item))
        {
            return;
        }

        //this.equipmentList.Add(item);

        var typeItem = item.GetComponent<IComponent_GetEqupType>().Type;

        for (int i = 0; i < equipmentSlots.Count; i++)
        {
            if(typeItem == equipmentSlots[i].TypeEquipment)
            {
                //SpawnAsSingle(item);
                
                var view = Instantiate(equipmentSlots[i].Prefab, this.equipmentSlots[i].Container);
                var presenter = new InventoryItemViewPresenter(view, item);
                presenter.Construct(this.popupManager, this.consumeManager, this.equipperManager);

                var viewHolder = new ViewHolder(view, presenter);
                this.items.Add(item, viewHolder);

                presenter.Start();
            }            
        }
        


        //if (this.items.ContainsKey(item))
        //{
        //    return;
        //}

        //var view = Instantiate(this.prefab, this.container);
        //var presenter = new InventoryItemViewPresenter(view, item);
        //presenter.Construct(this.popupManager, this.consumeManager, this.equipperManager);

        //var viewHolder = new ViewHolder(view, presenter);
        //this.items.Add(item, viewHolder);

        //presenter.Start();
    }

    private void SpawnAsSingle(InventoryItem prototype)
    {
        var item = prototype.Clone();
        this.equipmentList.Add(item);
        //this.OnItemAdded?.Invoke(item);
    }

    private bool RemoveAsInstance(InventoryItem item)
    {
        if (this.equipmentList.Remove(item))
        {
            //this.OnItemRemoved?.Invoke(item);
            return true;
        }

        return false;
    }

    private void UnquipItem(InventoryItem item)
    {
        if (!this.items.ContainsKey(item))
        {
            return;
        }

        //RemoveAsInstance(item);

        var viewHolder = this.items[item];
        viewHolder.presenter.Stop();
        Destroy(viewHolder.view.gameObject);
        this.items.Remove(item);

        //var typeItem = item.GetComponent<IComponent_GetEqupType>().Type;

        //for (int i = 0; i < equipmentSlots.Count; i++)
        //{
        //    if (typeItem == equipmentSlots[i].TypeEquipment)
        //    {
        //        //var view = Instantiate(equipmentSlots[i].Prefab, this.equipmentSlots[i].Container);
        //        //var presenter = new InventoryItemViewPresenter(view, item);
        //        //presenter.Construct(this.popupManager, this.consumeManager, this.equipperManager);

        //        //var viewHolder = new ViewHolder(view, presenter);
        //        //this.items.Add(item, viewHolder);

        //        //presenter.Start();
               
        //    }
        //}

        //var viewHolder = this.items[item];
        //viewHolder.presenter.Stop();
        //Destroy(viewHolder.view.gameObject);
        //this.items.Remove(item);
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
        public EquipmentItemViewPresenter Presenter;
    }
}