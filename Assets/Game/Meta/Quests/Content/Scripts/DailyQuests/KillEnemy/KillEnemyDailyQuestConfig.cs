using UnityEngine;


[CreateAssetMenu(
    fileName = "KillEnemyDailyQuest",
    menuName = DailyQuestExtensions.MENU_PATH + "New KillEnemyDailyQuest"
)]
public sealed class KillEnemyDailyQuestConfig : DailyQuestConfig
{
    public int RequiredKills
    {
        get { return this.requiredKills; }
    }

    [Header("Quest")]
    [SerializeField]
    private int requiredKills;

    public override DailyQuest InstantiateMission()
    {
        return new KillEnemyDailyQuest(this);
    }

    public override string Serialize(DailyQuest dailyQuest)
    {
        var myDailyQuest = (KillEnemyDailyQuest)dailyQuest;
        return myDailyQuest.CurrentKills.ToString();
    }

    public override void DeserializeTo(string serializedData, DailyQuest dailyQuest)
    {
        int.TryParse(serializedData, out var currentKills);
        var myDailyQuest = (KillEnemyDailyQuest)dailyQuest;
        myDailyQuest.Setup(currentKills);
    }
}