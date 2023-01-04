using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;


public abstract class NumberItemDictionary<K> : MonoBehaviour
{
    [SerializeField]
    private Transform itemsContainer;

    [SerializeField]
    private NumberItem itemPrefab;

    [Space]
    [SerializeField]
    [FormerlySerializedAs("hideItemIfZero")]
    private bool controlItemVisibility = true;

    [ShowIf("controlItemVisibility")]
    [SerializeField]
    private bool changeItemOrder = true;

    private readonly Dictionary<K, NumberItem> items;

    public NumberItemDictionary()
    {
        this.items = new Dictionary<K, NumberItem>();
    }

    public void AddItem(K key, int amount)
    {
        var view = Instantiate(this.itemPrefab, this.itemsContainer);
        view.name = key.ToString();
        view.SetIcon(this.FindIcon(key));
        view.SetupNumber(amount);
        this.items.Add(key, view);
        this.UpdateItemVisibility(view);
    }

    public void RemoveItem(K key)
    {
        var view = this.items[key];
        this.items.Remove(key);
        Destroy(view.gameObject);
    }

    public void ResetAllItems()
    {
        foreach (var view in this.items.Values)
        {
            view.SetupNumber(0);
            this.UpdateItemVisibility(view);
        }
    }

    public void SetupItem(K key, int amount)
    {
        var view = this.items[key];
        view.SetupNumber(amount);
        this.UpdateItemVisibility(view);
    }

    public void UpdateItem(K key, int amount)
    {
        var view = this.items[key];
        view.UpdateNumber(amount);
        this.UpdateItemVisibility(view);
    }

    public void IncrementItem(K key, int range)
    {
        var view = this.items[key];
        view.IncrementNumber(range);
        this.UpdateItemVisibility(view);
    }

    public void DecrementItem(K key, int range)
    {
        var view = this.items[key];
        view.DecrementNumber(range);
        this.UpdateItemVisibility(view);
    }

    public bool IsItemShown(K key)
    {
        var view = this.items[key];
        return view.gameObject.activeInHierarchy;
    }

    public void ShowItem(K key)
    {
        var view = this.items[key];
        view.gameObject.SetActive(true);
    }

    public void HideItem(K key)
    {
        var view = this.items[key];
        view.gameObject.SetActive(false);
    }

    public Vector3 GetIconCenter(K key)
    {
        var view = this.items[key];
        return view.GetIconCenter();
    }

    public int GetCurrentValue(K key)
    {
        var view = this.items[key];
        return view.CurrentValue;
    }

    protected abstract Sprite FindIcon(K key);

    private void UpdateItemVisibility(NumberItem view)
    {
        if (!this.controlItemVisibility)
        {
            return;
        }

        var isVisible = view.CurrentValue > 0;
        var viewObject = view.gameObject;
        if (viewObject.activeSelf == isVisible)
        {
            return;
        }

        viewObject.SetActive(isVisible);

        if (this.changeItemOrder)
        {
            view.transform.SetAsLastSibling();
        }
    }
}