using Random = UnityEngine.Random;
using UnityEngine;


public sealed class MissionSelector: MonoBehaviour 
{
    [SerializeField]
    private MissionCatalog _catalog;

    //[Inject]
    //public void Construct(MissionCatalog catalog)
    //{
    //    _catalog = catalog;
    //}

    public MissionConfig SelectNextMission(MissionDifficulty difficulty, string excludeMissionId)
    {
        var missions = _catalog.FindMissions(it => it.Difficulty == difficulty &&
                                                             it.Id != excludeMissionId);
        var randomIndex = Random.Range(0, missions.Length);
        var config = missions[randomIndex];
        return config;
    }
}