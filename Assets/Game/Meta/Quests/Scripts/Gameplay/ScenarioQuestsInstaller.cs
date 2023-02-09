using GameSystem;
using UnityEngine;


public sealed class ScenarioQuestsInstaller : MonoBehaviour,
    IGameConstructElement,
    IGameInitElement
{   
    [SerializeField]
    private ScenarioQuestConfig _startStageScenarioQuest;

    private ScenarioQuestManager _scenarioQuestManager;

    void IGameConstructElement.ConstructGame(IGameContext context)
    {        
        _scenarioQuestManager = context.GetService<ScenarioQuestManager>();
              
    }

    void IGameInitElement.InitGame()
    {
        if (!_scenarioQuestManager.IsScenarioQuestExists(ScenarioQuestStage.STAGE_I))
        {
            _scenarioQuestManager.InstallScenarioQuest(_startStageScenarioQuest);
        }
    }
}