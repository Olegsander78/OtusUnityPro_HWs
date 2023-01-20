using GameElements;
using Services;
using UnityEngine;


public sealed class MissionFactory
{
    [Inject]
    private IGameContext gameContext;

    public Mission CreateMission(MissionConfig config)
    {
        var mission = config.InstantiateMission();
        if (mission is IGameElement gameMission)
        {
            this.gameContext.RegisterElement(gameMission);
        }

        return mission;
    }

    public void DisposeMission(Mission mission)
    {
        if (mission is IGameElement gameMission)
        {
            this.gameContext.UnregisterElement(gameMission);
        }
    }
}