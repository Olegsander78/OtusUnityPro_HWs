using GameElements;
using UnityEngine;


public sealed class ScenarioQuestFactory: MonoBehaviour, IGameAttachElement
{    
    private IGameContext _gameContext;

    void IGameAttachElement.AttachGame(IGameContext context)
    {
        _gameContext = context;
    }

    public ScenarioQuest CreateQuest(ScenarioQuestConfig config)
    {
        var quest = config.InstantiateScenarioQuest();
        if (quest is IGameElement gameMission)
        {
            _gameContext.RegisterElement(gameMission);
            Debug.Log($"{gameMission} registered.");
        }

        return quest;
    }

    public void DisposeQuest(ScenarioQuest quest)
    {
        if (quest is IGameElement gameMission)
        {
            _gameContext.UnregisterElement(gameMission);
        }
    }
}