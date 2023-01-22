using GameElements;
using UnityEngine;


public sealed class DailyQuestFactory: MonoBehaviour, IGameAttachElement
{
    //[Inject]
    private IGameContext _gameContext;

    void IGameAttachElement.AttachGame(IGameContext context)
    {
        _gameContext = context;
    }

    public DailyQuest CreateMission(DailyQuestConfig config)
    {
        var mission = config.InstantiateMission();
        if (mission is IGameElement gameMission)
        {
            _gameContext.RegisterElement(gameMission);
            Debug.Log($"{gameMission} registered.");
        }

        return mission;
    }

    public void DisposeMission(DailyQuest mission)
    {
        if (mission is IGameElement gameMission)
        {
            _gameContext.UnregisterElement(gameMission);
        }
    }
}