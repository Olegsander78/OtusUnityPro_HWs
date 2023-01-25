using UnityEngine;


public abstract class ScenarioQuestConfig : ScriptableObject
{
    public string Id
    {
        get { return _id; }
    }

    public ScenarioQuestStage ScenarioQuestStage
    {
        get { return _scenarioQuestStage; }
    }

    public int MoneyReward
    {
        get { return _moneyReward; }
    }

    public int ExpReward
    {
        get { return _expReward; }
    }
    public ScenarioQuestMetadata Metadata
    {
        get { return _metadata; }
    }

    [SerializeField]
    private string _id;

    [SerializeField]
    private ScenarioQuestStage _scenarioQuestStage;

    [SerializeField]
    private int _moneyReward;

    [SerializeField]
    private int _expReward;

    [Space]
    [SerializeField]
    private ScenarioQuestMetadata _metadata;
   

    public abstract ScenarioQuest InstantiateScenarioQuest();

    public abstract string Serialize(ScenarioQuest quest);

    public abstract void DeserializeTo(string serializedData, ScenarioQuest quest);
}