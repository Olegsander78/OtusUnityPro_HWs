using System;
using Sirenix.OdinInspector;
using UnityEngine;
using LocalizationModule;


[Serializable]
public sealed class MissionMetadata
{
    //[TranslationKey]
    [SerializeField]
    public string localizedTitle;

    [PreviewField]
    [SerializeField]
    public Sprite icon;
}