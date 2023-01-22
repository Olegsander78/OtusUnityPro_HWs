using GameElements;
using UnityEngine;


public sealed class DailyQuestsInstaller : MonoBehaviour, IGameInitElement
{   
    [SerializeField]
    private DailyQuestConfig _easyDailyQuest;

    [SerializeField]
    private DailyQuestConfig _normalDailyQuest;

    [SerializeField]
    private DailyQuestConfig _hardDailyQuest;

    void IGameInitElement.InitGame(IGameContext context)
    {        
        var missionsManager = context.GetService<DailyQuestManager>();

        if (!missionsManager.IsDailyQuestExists(DailyQuestDifficulty.EASY))
        {
            missionsManager.InstallDailyQuest(_easyDailyQuest);
        }

        if (!missionsManager.IsDailyQuestExists(DailyQuestDifficulty.NORMAL))
        {
            missionsManager.InstallDailyQuest(_normalDailyQuest);
        }

        if (!missionsManager.IsDailyQuestExists(DailyQuestDifficulty.HARD))
        {
            missionsManager.InstallDailyQuest(_hardDailyQuest);
        }
    }
}