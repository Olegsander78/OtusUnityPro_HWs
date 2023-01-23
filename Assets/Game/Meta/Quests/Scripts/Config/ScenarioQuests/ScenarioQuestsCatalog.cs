using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(
    fileName = "ScenarioQuestsCatalog",
    menuName = ScenarioQuestExtensions.MENU_PATH + "New ScenarioQuestsCatalog"
)]
public sealed class ScenarioQuestsCatalog : ScriptableObject
{
    [SerializeField]
    public ScenarioQuestConfig[] _quests;

    public ScenarioQuestConfig[] GetAllQuests()
    {
        return _quests;
    }

    public ScenarioQuestConfig FindQuest(string id)
    {
        for (int i = 0, count = _quests.Length; i < count; i++)
        {
            var quest = _quests[i];
            if (quest.Id == id)
            {
                return quest;
            }
        }

        throw new Exception($"ScenarioQuest with id {id} is not found!");
    }

    public ScenarioQuestConfig[] FindQuests(ScenarioQuestStage stage)
    {
        var quests = new List<ScenarioQuestConfig>();
        for (int i = 0, count = _quests.Length; i < count; i++)
        {
            var quest = _quests[i];
            if (quest.ScenarioQuestStage == stage)
            {
                quests.Add(quest);
            }
        }

        return quests.ToArray();
    }

    public ScenarioQuestConfig[] FindQuests(Predicate<ScenarioQuestConfig> predicate)
    {
        return Array.FindAll(_quests, predicate);
    }
}