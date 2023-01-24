using GameElements;
using UnityEngine;


public sealed class ScenarioQuestsInstaller : MonoBehaviour, IGameInitElement
{   
    [SerializeField]
    private ScenarioQuestConfig _startStageScenarioQuest;

    void IGameInitElement.InitGame(IGameContext context)
    {        
        var scenarioQuestManager = context.GetService<ScenarioQuestManager>();

        if (!scenarioQuestManager. IsScenarioQuestExists(ScenarioQuestStage.STAGE_I))
        {
            scenarioQuestManager.InstallScenarioQuest(_startStageScenarioQuest);
        }        
    }
}