using UnityEngine;


[CreateAssetMenu(
    fileName = "EarnMoneyDailyQuest",
    menuName = DailyQuestExtensions.MENU_PATH + "New EarnMoneyDailyQuest"
)]
public sealed class EarnMoneyDailyQuestConfig : DailyQuestConfig
{
    public int RequiredMoney
    {
        get { return this.requiredMoney; }
    }

    [Header("Quest")]
    [SerializeField]
    private int requiredMoney;

    public override DailyQuest InstantiateMission()
    {
        return new EarnMoneyDailyQuest(config: this);
    }

    public override string Serialize(DailyQuest dailyQuest)
    {
        var myDailyQuest = (EarnMoneyDailyQuest)dailyQuest;
        return myDailyQuest.EarnedMoney.ToString();
    }

    public override void DeserializeTo(string serializedData, DailyQuest dailyQuest)
    {
        int.TryParse(serializedData, out var collectedResources);
        var myDailyQuest = (EarnMoneyDailyQuest)dailyQuest;
        myDailyQuest.Setup(collectedResources);
    }
}