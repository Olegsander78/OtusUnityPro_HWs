using UnityEngine;


[CreateAssetMenu(
    fileName = "ScenarioQuest_StageI",
    menuName = ScenarioQuestExtensions.MENU_PATH + "New ScenarioQuest_StageI"
)]
public sealed class ScenarioQuestConfig_StageI : ScenarioQuestConfig
{
    public EnemyType EnemyTypeOnScenarioQuestStage
    {
        get { return _enemyTypeOnScenarioQuestStage; }
    }
    public int RequiredKills
    {
        get { return _requiredKills; }
    }

    [Header("Scenario Quest Stage I")]
    [SerializeField]
    private EnemyType _enemyTypeOnScenarioQuestStage;
    
    [SerializeField]
    private int _requiredKills;

    public override ScenarioQuest InstantiateScenarioQuest()
    {
        return new ScenarioQuest_StageI(this);
    }

    public override string Serialize(ScenarioQuest scenarioQuest)
    {
        var myScenarioQuest = (ScenarioQuest_StageI)scenarioQuest;
        return myScenarioQuest.CurrentKills.ToString();
    }

    public override void DeserializeTo(string serializedData, ScenarioQuest scenarioQuest)
    {
        int.TryParse(serializedData, out var currentKills);
        var myScenarioQuest = (ScenarioQuest_StageI)scenarioQuest;
        myScenarioQuest.Setup(currentKills);
    }
}

//јвантюрист, приветствую теб€ в нашей забытой богами деревне! я вижу ты в хорошей физической форме, а вот мы нет. — не давнего времени €щеры-воины на севере от деревни зан€ли старое логово и повадились грабить наши караваны. ≈сли ты разгонишь эту шайку жители деревни будут очень благодарны тебе. 
//Adventurer, welcome to our godforsaken village! I see you in good physical shape, but we are not. Not long ago, warrior lizards to the north of the village occupied the old lair and got into the habit of robbing our caravans. If you disperse this gang, the villagers will be very grateful to you.

