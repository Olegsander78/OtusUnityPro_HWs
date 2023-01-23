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

    [Space]
    [TextArea]
    [SerializeField]
    public string storyScenarioQuestStageOnCompleted;

    [Space]
    [PreviewField]
    [SerializeField]
    public Sprite icon;
}


//Stage1
//Авантюрист, приветствую тебя в нашей забытой богами деревне! Я вижу ты в хорошей физической форме, а вот мы не очень. С не давнего времени ящеры-воины на севере от деревни заняли старое логово и повадились грабить наши караваны. Если ты разгонишь эту шайку жители деревни будут очень благодарны тебе. 
//Авантюрист, спасибо тебе что избавил нас от этих надоедливых ящеров. Караваны теперь в безопасности, деревню ждет процветание.
//Adventurer, welcome to our godforsaken village! I see you in good physical shape, but we are not very. Not long ago, warrior lizards to the north of the village occupied the old lair and got into the habit of robbing our caravans. If you disperse this gang, the villagers will be very grateful to you.
//Adventurer, thank you for ridding us of those pesky lizards. The caravans are now safe, the village will prosper.
