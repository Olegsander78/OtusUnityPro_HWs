using GameSystem;
using UnityEngine;


public sealed class DailyQuestFactory: MonoBehaviour, 
    IGameAttachElement
{    
    private IGameContext _gameContext;

    void IGameAttachElement.AttachGame(IGameContext context)
    {
        _gameContext = context;
    }

    public DailyQuest CreateQuest(DailyQuestConfig config)
    {
        var quest = config.InstantiateMission();
        if (quest is IGameElement gameMission)
        {
            _gameContext.RegisterElement(gameMission);
            Debug.Log($"{gameMission} registered.");
        }

        return quest;
    }

    public void DisposeQuest(DailyQuest quest)
    {
        if (quest is IGameElement gameMission)
        {
            _gameContext.UnregisterElement(gameMission);
        }
    }
}