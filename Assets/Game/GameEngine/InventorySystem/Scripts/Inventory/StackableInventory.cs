using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
// ReSharper disable UnusedMethodReturnValue.Local


public class StackableInventory
{
    public event Action<InventoryItem> OnItemAdded;

    public event Action<InventoryItem> OnItemRemoved;

    private readonly ListInventory list;

    public StackableInventory(ListInventory list)
    {
        this.list = list;
    }

    public StackableInventory()
    {
        this.list = new ListInventory();
    }

    public InventoryItem[] GetAllItems()
    {
        return this.list.GetAllItems();
    }

    public List<InventoryItem> GetAllItemsUnsafe()
    {
        return this.list.GetAllItemsUnsafe();
    }

    public bool FindItemFirst(string name, out InventoryItem item)
    {
        return this.list.FindItemFirst(name, out item);
    }

    public bool FindItemFirst(InventoryItemFlags tags, out InventoryItem item)
    {
        return this.list.FindItemFirst(tags, out item);
    }

    public bool FindItemLast(string name, out InventoryItem item)
    {
        return list.FindItemLast(name, out item);
    }

    public InventoryItem[] FindItems(string name)
    {
        return this.list.FindItems(name);
    }

    public InventoryItem[] FindItems(InventoryItemFlags tags)
    {
        return this.list.FindItems(tags);
    }

    public bool IsEmpty()
    {
        return this.list.IsEmpty();
    }

    public bool IsItemExists(InventoryItem item)
    {
        return this.list.IsItemExists(item);
    }

    public bool SetupItem(InventoryItem item)
    {
        if (item is null)
        {
            return false;
        }

        if (this.IsItemExists(item))
        {
            return false;
        }

        this.list.AddItem(item);
        return true;
    }

    /// <summary>
    ///     <para>Adds one item in a new slot</para>
    /// </summary>
    public bool AddItemAsInstance(InventoryItem item)
    {
        if (item is null)
        {
            return false;
        }

        if (this.IsItemExists(item))
        {
            return false;
        }

        this.list.AddItem(item);
        this.OnItemAdded?.Invoke(item);
        return true;
    }

    /// <summary>
    ///     <para>Removes one item</para>
    /// </summary>
    /// <param name="item">Target item in inventory.</param>
    public bool RemoveItem(InventoryItem item)
    {
        if (item is null)
        {
            return false;
        }

        if (item.FlagsExists(InventoryItemFlags.STACKABLE))
        {
            return this.RemoveAsStackable(item);
        }

        return this.RemoveAsInstance(item);
    }

    public bool RemoveItem(string itemName)
    {
        if (!this.list.FindItemLast(itemName, out var item))
        {
            return false;
        }

        if (item.FlagsExists(InventoryItemFlags.STACKABLE))
        {
            return this.RemoveAsStackable(item);
        }

        return this.RemoveAsInstance(item);
    }

    /// <summary>
    ///     <para>Removes several items by name</para>
    /// </summary>
    /// <param name="itemName">Target item name</param>
    /// <param name="count">Expected item count to remove</param>
    public void RemoveItems(string itemName, int count)
    {
        while (count > 0)
        {
            if (!this.list.FindItemLast(itemName, out var item))
            {
                return;
            }

            if (item.FlagsExists(InventoryItemFlags.STACKABLE))
            {
                this.DecrementValueInStack(item, ref count);
            }
            else
            {
                this.RemoveAsInstance(item);
            }
        }
    }

    private bool RemoveAsStackable(InventoryItem item)
    {
        if (!this.list.IsItemExists(item))
        {
            return false;
        }

        var component = item.GetComponent<IComponent_Stackable>();
        component.Value--;
        if (component.Value > 0)
        {
            return true;
        }

        if (this.list.RemoveItem(item))
        {
            this.OnItemRemoved?.Invoke(item);
        }

        return true;
    }

    private bool RemoveAsInstance(InventoryItem item)
    {
        if (this.list.RemoveItem(item))
        {
            this.OnItemRemoved?.Invoke(item);
            return true;
        }

        return false;
    }

    public int CountAllItems()
    {
        var result = 0;

        var items = this.list.GetAllItemsUnsafe();
        for (int i = 0, count = items.Count; i < count; i++)
        {
            var item = items[i];
            result += this.CountItem(item);
        }

        return result;
    }

    public Dictionary<string, int> CountAllItemsInDictionary()
    {
        var result = new Dictionary<string, int>();
        var items = this.list.GetAllItemsUnsafe();
        for (int i = 0, count = items.Count; i < count; i++)
        {
            var item = items[i];
            var itemName = item.Name;
            result.TryGetValue(itemName, out var amount);

            if (item.FlagsExists(InventoryItemFlags.STACKABLE))
            {
                amount += item.GetComponent<IComponent_Stackable>().Value;
            }
            else
            {
                amount++;
            }

            result[itemName] = amount;
        }

        return result;
    }

    public int CountItems(string itemName)
    {
        var result = 0;

        var items = this.list.FindItems(itemName);
        for (int i = 0, count = items.Length; i < count; i++)
        {
            var item = items[i];
            result += this.CountItem(item);
        }

        return result;
    }

    private int CountItem(InventoryItem item)
    {
        if (item.FlagsExists(InventoryItemFlags.STACKABLE))
        {
            return item.GetComponent<IComponent_Stackable>().Value;
        }

        return 1;
    }

    /// <summary>
    ///     <para>Spawns items by prototype.</para>
    /// </summary>
    /// <param name="prototype">Origin item (will not be added to inventory)</param>
    /// <param name="count">Required items to add.</param>
    public void AddItemsByPrototype(InventoryItem prototype, int count)
    {
        if (prototype.FlagsExists(InventoryItemFlags.STACKABLE))
        {
            this.SpawnAsStackable(prototype, count);
        }
        else
        {
            this.SpawnAsSingle(prototype, count);
        }
    }

    private void SpawnAsStackable(InventoryItem prototype, int count)
    {
        var itemName = prototype.Name;
        var stackSize = prototype.GetComponent<IComponent_Stackable>().Size;

        while (count > 0)
        {
            if (this.list.FindItemFirst(IsAvailable, out var targetItem))
            {
                this.IncrementValueInStack(targetItem, stackSize, ref count);
            }
            else
            {
                targetItem = prototype.Clone();
                this.IncrementValueInStack(targetItem, stackSize, ref count);
                this.list.AddItem(targetItem);
                this.OnItemAdded?.Invoke(targetItem);
            }
        }

        bool IsAvailable(InventoryItem it)
        {
            return it.Name == itemName && !it.GetComponent<IComponent_Stackable>().IsFull;
        }
    }

    private void SpawnAsSingle(InventoryItem prototype, int count)
    {
        for (var i = 0; i < count; i++)
        {
            this.SpawnAsSingle(prototype);
        }
    }

    private void SpawnAsSingle(InventoryItem prototype)
    {
        var item = prototype.Clone();
        this.list.AddItem(item);
        this.OnItemAdded?.Invoke(item);
    }

    private void IncrementValueInStack(InventoryItem item, int stackSize, ref int remainingCount)
    {
        var stackableComponent = item.GetComponent<IComponent_Stackable>();
        var previousCount = stackableComponent.Value;
        var newCount = previousCount + remainingCount;

        var overflow = newCount - stackSize;
        if (overflow > 0)
        {
            newCount = stackSize;
        }

        stackableComponent.Value = newCount;

        var diff = newCount - previousCount;
        remainingCount -= diff;
    }

    private void DecrementValueInStack(InventoryItem item, ref int remainingCount)
    {
        var stackableComponent = item.GetComponent<IComponent_Stackable>();
        var previousCount = stackableComponent.Value;
        var newCount = previousCount - remainingCount;

        if (newCount <= 0)
        {
            newCount = 0;
            stackableComponent.Value = 0;
            this.RemoveAsInstance(item);
        }
        else
        {
            stackableComponent.Value = newCount;
        }

        var diff = previousCount - newCount;
        remainingCount -= diff;
    }


#if UNITY_EDITOR
    [PropertySpace(8)]
    [PropertyOrder(-10)]
    [ReadOnly]
    [ShowInInspector]
    private List<InventoryItem> _items
    {
        get { return this.list.GetAllItemsUnsafe(); }
    }
#endif
}