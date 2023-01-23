using System;
using Sirenix.OdinInspector;
using UnityEngine;
using LocalizationModule;


[Serializable]
public sealed class ScenarioQuestMetadata
{
    //[TranslationKey]
    [SerializeField]
    public string localizedTitle;

    [TextArea]
    [SerializeField]
    public string storyScenarioQuestStage;

    [PreviewField]
    [SerializeField]
    public Sprite icon;
}