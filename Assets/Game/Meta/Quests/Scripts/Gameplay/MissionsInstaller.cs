using GameElements;
using UnityEngine;


public sealed class MissionsInstaller : MonoBehaviour, IGameInitElement
{
    //[SerializeField]
    //private MissionsManager _missionsManager;
    
    [SerializeField]
    private MissionConfig _easyMission;

    [SerializeField]
    private MissionConfig _normalMission;

    [SerializeField]
    private MissionConfig _hardMission;

    void IGameInitElement.InitGame(IGameContext context)
    {        
        var missionsManager = context.GetService<MissionsManager>();

        if (!missionsManager.IsMissionExists(MissionDifficulty.EASY))
        {
            missionsManager.InstallMission(_easyMission);
        }

        if (!missionsManager.IsMissionExists(MissionDifficulty.NORMAL))
        {
            missionsManager.InstallMission(_normalMission);
        }

        if (!missionsManager.IsMissionExists(MissionDifficulty.HARD))
        {
            missionsManager.InstallMission(_hardMission);
        }
    }
}