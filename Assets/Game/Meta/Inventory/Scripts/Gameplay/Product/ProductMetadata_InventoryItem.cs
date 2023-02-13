using System;
using UnityEngine;


[Serializable]
public sealed class ProductMetadata_InventoryItem : IProductMetadata
{
    public Sprite Icon
    {
        get { return this.config.Metadata.icon; }
    }

    public string Title
    {
        get { return this.config.Metadata.title; }
    }

    public string Decription
    {
        get { return this.config.Metadata.decription; }
    }

    [SerializeField]
    private InventoryItemConfig config;
}