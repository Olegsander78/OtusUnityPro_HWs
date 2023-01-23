using UnityEngine;


[CreateAssetMenu(
    fileName = "ScenarioQuest_StageKillEnemy",
    menuName = ScenarioQuestExtensions.MENU_PATH + "New ScenarioQuest_StageKillEnemy"
)]
public sealed class ScenarioQuestConfig_StageKillEnemy : ScenarioQuestConfig
{
    public EnemyType EnemyTypeOnScenarioQuestStage
    {
        get { return _enemyTypeOnScenarioQuestStage; }
    }
    public int RequiredKills
    {
        get { return _requiredKills; }
    }

    [Header("Scenario Quest Stage")]
    [SerializeField]
    private EnemyType _enemyTypeOnScenarioQuestStage;
    
    [SerializeField]
    private int _requiredKills;

    public override ScenarioQuest InstantiateScenarioQuest()
    {
        return new ScenarioQuest_StageKillEnemy(this);
    }

    public override string Serialize(ScenarioQuest scenarioQuest)
    {
        var myScenarioQuest = (ScenarioQuest_StageKillEnemy)scenarioQuest;
        return myScenarioQuest.CurrentKills.ToString();
    }

    public override void DeserializeTo(string serializedData, ScenarioQuest scenarioQuest)
    {
        int.TryParse(serializedData, out var currentKills);
        var myScenarioQuest = (ScenarioQuest_StageKillEnemy)scenarioQuest;
        myScenarioQuest.Setup(currentKills);
    }
}