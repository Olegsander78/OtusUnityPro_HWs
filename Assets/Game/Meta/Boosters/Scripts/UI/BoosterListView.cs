using System.Collections.Generic;
using UnityEngine;


public sealed class BoosterListView : MonoBehaviour
{
    [SerializeField]
    private Transform container;

    [SerializeField]
    private BoosterView prefab;

    private readonly Dictionary<Booster, ViewHolder> boosters = new();

    public void AddBooster(Booster booster)
    {
        if (this.boosters.ContainsKey(booster))
        {
            return;
        }

        var view = Instantiate(this.prefab, this.container);
        var adapter = new BoosterViewAdapter(view, booster, coroutineDispatcher: this);
        var viewHolder = new ViewHolder(view, adapter);
        this.boosters.Add(booster, viewHolder);

        adapter.Show();
    }

    public void RemoveBooster(Booster booster)
    {
        if (!this.boosters.ContainsKey(booster))
        {
            return;
        }

        var viewHolder = this.boosters[booster];
        viewHolder.adapter.Hide();
        Destroy(viewHolder.view.gameObject);
        this.boosters.Remove(booster);
    }

    private sealed class ViewHolder
    {
        public readonly BoosterView view;
        public readonly BoosterViewAdapter adapter;

        public ViewHolder(BoosterView view, BoosterViewAdapter adapter)
        {
            this.view = view;
            this.adapter = adapter;
        }
    }
}