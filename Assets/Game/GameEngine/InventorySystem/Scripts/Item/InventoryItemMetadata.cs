using System;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public sealed class InventoryItemMetadata
{
    [PreviewField]
    [SerializeField]
    public Sprite icon;

    [SerializeField]
    public string title;

    [TextArea]
    [SerializeField]
    public string decription;
}