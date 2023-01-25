using UnityEngine;


public abstract class DailyQuestConfig : ScriptableObject
{
    public string Id
    {
        get { return _id; }
    }

    public DailyQuestDifficulty Difficulty
    {
        get { return _difficulty; }
    }

    public int MoneyReward
    {
        get { return _moneyReward; }
    }

    public int ExpReward
    {
        get { return _expReward; }
    }
    public DailyQuestMetadata Metadata
    {
        get { return _metadata; }
    }

    [SerializeField]
    private string _id;

    [SerializeField]
    private DailyQuestDifficulty _difficulty;

    [SerializeField]
    private int _moneyReward;

    [SerializeField]
    private int _expReward;

    [Space]
    [SerializeField]
    private DailyQuestMetadata _metadata;
   

    public abstract DailyQuest InstantiateMission();

    public abstract string Serialize(DailyQuest mission);

    public abstract void DeserializeTo(string serializedData, DailyQuest mission);
}