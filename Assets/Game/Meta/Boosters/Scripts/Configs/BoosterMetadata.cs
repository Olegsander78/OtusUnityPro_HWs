using System;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public sealed class BoosterMetadata
{
    [PreviewField]
    [SerializeField]
    public Sprite icon;

    [Tooltip("Label of BoosterItem")]
    [SerializeField]
    public string viewLabel;

    [Tooltip("Color of BoosterItem")]
    [SerializeField]
    public Color viewColor;
}