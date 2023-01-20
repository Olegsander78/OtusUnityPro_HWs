using GameElements;
using Services;
using Random = UnityEngine.Random;


public sealed class MissionSelector
{
    private MissionCatalog catalog;

    [Inject]
    public void Construct(MissionCatalog catalog)
    {
        this.catalog = catalog;
    }

    public MissionConfig SelectNextMission(MissionDifficulty difficulty, string excludeMissionId)
    {
        var missions = this.catalog.FindMissions(it => it.Difficulty == difficulty &&
                                                             it.Id != excludeMissionId);
        var randomIndex = Random.Range(0, missions.Length);
        var config = missions[randomIndex];
        return config;
    }
}