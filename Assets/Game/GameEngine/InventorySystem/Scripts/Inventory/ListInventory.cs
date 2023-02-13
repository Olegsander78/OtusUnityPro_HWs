using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;


public class ListInventory
{
    public event Action<InventoryItem> OnItemAdded;

    public event Action<InventoryItem> OnItemRemoved;

    [ReadOnly]
    [ShowInInspector]
    private readonly List<InventoryItem> items;

    public ListInventory()
    {
        this.items = new List<InventoryItem>();
    }

    public void SetupItems(InventoryItem[] item)
    {
        this.items.Clear();
        this.items.AddRange(item);
    }

    public void AddItem(InventoryItem item)
    {
        this.items.Add(item);
        this.OnItemAdded?.Invoke(item);
    }

    public bool RemoveItem(InventoryItem item)
    {
        if (this.items.Remove(item))
        {
            this.OnItemRemoved?.Invoke(item);
            return true;
        }

        return false;
    }

    public bool IsItemExists(InventoryItem item)
    {
        return this.items.Contains(item);
    }

    public bool IsEmpty()
    {
        return this.items.Count <= 0;
    }

    public InventoryItem[] GetAllItems()
    {
        return this.items.ToArray();
    }

    public List<InventoryItem> GetAllItemsUnsafe()
    {
        return this.items;
    }

    public bool FindItemFirst(InventoryItemFlags flags, out InventoryItem item)
    {
        for (int i = 0, count = this.items.Count; i < count; i++)
        {
            item = this.items[i];
            if (item.FlagsExists(flags))
            {
                return true;
            }
        }

        item = default;
        return false;
    }

    public bool FindItemFirst(string name, out InventoryItem item)
    {
        for (int i = 0, count = this.items.Count; i < count; i++)
        {
            item = this.items[i];
            if (item.Name == name)
            {
                return true;
            }
        }

        item = default;
        return false;
    }

    public bool FindItemLast(string name, out InventoryItem item)
    {
        for (var i = this.items.Count - 1; i >= 0; i--)
        {
            item = this.items[i];
            if (item.Name == name)
            {
                return true;
            }
        }

        item = default;
        return false;
    }

    public bool FindItemFirst(Func<InventoryItem, bool> predicate, out InventoryItem item)
    {
        for (int i = 0, count = this.items.Count; i < count; i++)
        {
            item = this.items[i];
            if (predicate.Invoke(item))
            {
                return true;
            }
        }

        item = default;
        return false;
    }

    public InventoryItem[] FindItems(InventoryItemFlags flags)
    {
        var result = new List<InventoryItem>();
        for (int i = 0, count = this.items.Count; i < count; i++)
        {
            var item = this.items[i];
            if (item.FlagsExists(flags))
            {
                result.Add(item);
            }
        }

        return result.ToArray();
    }

    public InventoryItem[] FindItems(string name)
    {
        var result = new List<InventoryItem>();
        for (int i = 0, count = this.items.Count; i < count; i++)
        {
            var item = this.items[i];
            if (item.Name == name)
            {
                result.Add(item);
            }
        }

        return result.ToArray();
    }

    public int CountItems(InventoryItemFlags flags)
    {
        var result = 0;
        for (int i = 0, count = this.items.Count; i < count; i++)
        {
            var item = this.items[i];
            if (item.FlagsExists(flags))
            {
                result++;
            }
        }

        return result;
    }

    public int CountItems(string itemName)
    {
        var result = 0;
        for (int i = 0, count = this.items.Count; i < count; i++)
        {
            var item = this.items[i];
            if (item.Name == itemName)
            {
                result++;
            }
        }

        return result;
    }
}