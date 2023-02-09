using GameSystem;
using UnityEngine;


public sealed class DailyQuestsInstaller : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement
{   
    [SerializeField]
    private DailyQuestConfig _easyDailyQuest;

    [SerializeField]
    private DailyQuestConfig _normalDailyQuest;

    [SerializeField]
    private DailyQuestConfig _hardDailyQuest;

    private DailyQuestManager _dailyQuestManager;

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _dailyQuestManager = context.GetService<DailyQuestManager>();
    }

    void IGameInitElement.InitGame()
    {
        if (!_dailyQuestManager.IsDailyQuestExists(DailyQuestDifficulty.EASY))
        {
            _dailyQuestManager.InstallDailyQuest(_easyDailyQuest);
        }

        if (!_dailyQuestManager.IsDailyQuestExists(DailyQuestDifficulty.NORMAL))
        {
            _dailyQuestManager.InstallDailyQuest(_normalDailyQuest);
        }

        if (!_dailyQuestManager.IsDailyQuestExists(DailyQuestDifficulty.HARD))
        {
            _dailyQuestManager.InstallDailyQuest(_hardDailyQuest);
        }
    }
}