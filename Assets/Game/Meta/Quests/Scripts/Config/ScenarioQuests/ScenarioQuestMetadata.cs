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
//����������, ����������� ���� � ����� ������� ������ �������! � ���� �� � ������� ���������� �����, � ��� �� �� �����. � �� ������� ������� �����-����� �� ������ �� ������� ������ ������ ������ � ���������� ������� ���� ��������. ���� �� ��������� ��� ����� ������ ������� ����� ����� ���������� ����. 
//����������, ������� ���� ��� ������� ��� �� ���� ����������� ������. �������� ������ � ������������, ������� ���� �����������.
//Adventurer, welcome to our godforsaken village! I see you in good physical shape, but we are not very. Not long ago, warrior lizards to the north of the village occupied the old lair and got into the habit of robbing our caravans. If you disperse this gang, the villagers will be very grateful to you.
//Adventurer, thank you for ridding us of those pesky lizards. The caravans are now safe, the village will prosper.
