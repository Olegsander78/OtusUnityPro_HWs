using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(
    fileName = "MissionCatalog",
    menuName = DailyQuestExtensions.MENU_PATH + "New MissionCatalog"
)]
public sealed class DailyQuestsCatalog : ScriptableObject
{
    [SerializeField]
    public DailyQuestConfig[] _quests;

    public DailyQuestConfig[] GetAllQuests()
    {
        return this._quests;
    }

    public DailyQuestConfig FindQuest(string id)
    {
        for (int i = 0, count = this._quests.Length; i < count; i++)
        {
            var mission = this._quests[i];
            if (mission.Id == id)
            {
                return mission;
            }
        }

        throw new Exception($"DailyQuest with id {id} is not found!");
    }

    public DailyQuestConfig[] FindQuests(DailyQuestDifficulty difficulty)
    {
        var missions = new List<DailyQuestConfig>();
        for (int i = 0, count = this._quests.Length; i < count; i++)
        {
            var mission = this._quests[i];
            if (mission.Difficulty == difficulty)
            {
                missions.Add(mission);
            }
        }

        return missions.ToArray();
    }

    public DailyQuestConfig[] FindQuests(Predicate<DailyQuestConfig> predicate)
    {
        return Array.FindAll(this._quests, predicate);
    }
}