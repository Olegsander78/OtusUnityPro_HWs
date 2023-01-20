#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class MissionManagerDebug : MonoBehaviour
{
    [SerializeField]
    private MissionsManager _manager;

    [PropertySpace(8)]
    [Button]
    private void ReceiveReward(MissionDifficulty difficulty)
    {
        var mission = _manager.GetMission(difficulty);
        if (_manager.CanReceiveReward(mission))
        {
            _manager.ReceiveReward(mission);
            Debug.Log($"RECEIVED Money REWARD {mission.MoneyReward} FROM MISSION {mission.Id}");
            Debug.Log($"RECEIVED Exp REWARD {mission.ExpReward} FROM MISSION {mission.Id}");
        }
    }

    private void Reset()
    {
        _manager = this.GetComponent<MissionsManager>();
    }
}
#endif