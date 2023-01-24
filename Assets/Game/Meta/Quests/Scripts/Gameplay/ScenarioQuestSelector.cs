using Random = UnityEngine.Random;
using UnityEngine;
using System;

public sealed class ScenarioQuestSelector: MonoBehaviour 
{
    [SerializeField]
    private ScenarioQuestsCatalog _catalog;

    public ScenarioQuestConfig SelectNextScenarioQuest(ScenarioQuestStage stage)
    {
        stage++;
        
        var scenarioQuestconfig = _catalog.FindQuest(stage);

        if (scenarioQuestconfig != null)
        {
            return scenarioQuestconfig;
        }

        throw new Exception($"Scenario Quest {stage} is absent!");
    }
}