using System;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public sealed class ChestMetadata
{
    [PreviewField]
    [SerializeField]
    public Sprite icon;

    [SerializeField]
    public string DisplayName;

    [TextArea]
    [SerializeField]
    public string Description;

    [SerializeField]
    public ChestType ChestType;

    [SerializeField]
    public int PriceOpen;


    //[Tooltip("Label of ChestItem")]
    //[SerializeField]
    //public string viewLabel;

    //[Tooltip("Color of ChesItem")]
    //[SerializeField]
    //public Color viewColor;
}