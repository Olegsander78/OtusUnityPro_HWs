#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class DailyQuestManagerDebug : MonoBehaviour
{
    [SerializeField]
    private DailyQuestManager _manager;

    [PropertySpace(8)]
    [Button]
    private void ReceiveReward(DailyQuestDifficulty difficulty)
    {
        var mission = _manager.GetDailyQuest(difficulty);
        if (_manager.CanReceiveReward(mission))
        {
            _manager.ReceiveReward(mission);
            Debug.Log($"RECEIVED Money REWARD {mission.MoneyReward} FROM MISSION {mission.Id}");
            Debug.Log($"RECEIVED Exp REWARD {mission.ExpReward} FROM MISSION {mission.Id}");
        }
    }

    private void Reset()
    {
        _manager = this.GetComponent<DailyQuestManager>();
    }
}
#endif