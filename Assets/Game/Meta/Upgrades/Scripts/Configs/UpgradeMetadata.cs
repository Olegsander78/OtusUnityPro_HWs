using System;
using Sirenix.OdinInspector;
using UnityEngine;


[Serializable]
public sealed class UpgradeMetadata
{
    //[TranslationKey]
    //[SerializeField]
    //public string localizedTitle;
    [SerializeField]
    public string Title;

    [PreviewField]
    [SerializeField]
    public Sprite Icon;
}