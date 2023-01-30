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
}