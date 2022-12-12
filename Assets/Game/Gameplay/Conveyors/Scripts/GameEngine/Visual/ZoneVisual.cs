using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class ZoneVisual : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> items;

    private int currentAmount;

    public void SetupItems(int currentAmount)
    {
        this.currentAmount = Mathf.Min(currentAmount, this.items.Count);

        for (var i = 0; i < currentAmount; i++)
        {
            var item = this.items[i];
            item.SetActive(true);
        }

        var count = this.items.Count;
        for (var i = currentAmount; i < count; i++)
        {
            var item = this.items[i];
            item.SetActive(false);
        }
    }

    public void IncrementItems(int range)
    {
        var previousAmount = this.currentAmount;
        var newAmount = Mathf.Min(this.currentAmount + range, this.items.Count);
        this.currentAmount = newAmount;

        for (var i = previousAmount; i < newAmount; i++)
        {
            var item = this.items[i];
            item.SetActive(true);
        }
    }

    public void DecrementItems(int range)
    {
        var previousAmount = this.currentAmount;
        var newAmount = Mathf.Max(this.currentAmount - range, 0);
        this.currentAmount = newAmount;

        for (var i = previousAmount - 1; i >= newAmount; i--)
        {
            var item = this.items[i];
            item.SetActive(false);
        }
    }

#if UNITY_EDITOR
    [Button("Setup Items")]
    private void Editor_SetupItems()
    {
        this.items = new List<GameObject>();
        foreach (Transform child in this.transform)
        {
            this.items.Add(child.gameObject);
        }
    }
#endif
}