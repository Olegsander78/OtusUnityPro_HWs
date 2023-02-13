using System;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public sealed class ProductMetadata : IProductMetadata
{
    public Sprite Icon
    {
        get { return this.icon; }
    }

    public string Title
    {
        get { return this.title; }
    }

    public string Decription
    {
        get { return this.decription; }
    }

    [PreviewField]
    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private string title;

    [TextArea]
    [SerializeField]
    private string decription;
}