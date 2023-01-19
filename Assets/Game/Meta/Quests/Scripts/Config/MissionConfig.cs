using UnityEngine;


public abstract class MissionConfig : ScriptableObject
{
    public string Id
    {
        get { return _id; }
    }

    public MissionDifficulty Difficulty
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
    public MissionMetadata Metadata
    {
        get { return _metadata; }
    }

    [SerializeField]
    private string _id;

    [SerializeField]
    private MissionDifficulty _difficulty;

    [SerializeField]
    private int _moneyReward;

    [SerializeField]
    private int _expReward;

    [Space]
    [SerializeField]
    private MissionMetadata _metadata;
   

    public abstract Mission InstantiateMission();

    public abstract string Serialize(Mission mission);

    public abstract void DeserializeTo(string serializedData, Mission mission);
}