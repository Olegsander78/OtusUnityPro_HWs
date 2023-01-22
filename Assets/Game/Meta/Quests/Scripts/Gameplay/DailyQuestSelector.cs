using Random = UnityEngine.Random;
using UnityEngine;


public sealed class DailyQuestSelector: MonoBehaviour 
{
    [SerializeField]
    private DailyQuestsCatalog _catalog;

    public DailyQuestConfig SelectNextDailyQuest(DailyQuestDifficulty difficulty, string excludeDailyQuestId)
    {
        var dailyQuests = _catalog.FindQuests(it => it.Difficulty == difficulty &&
                                                             it.Id != excludeDailyQuestId);
        var randomIndex = Random.Range(0, dailyQuests.Length);
        var config = dailyQuests[randomIndex];
        return config;
    }
}