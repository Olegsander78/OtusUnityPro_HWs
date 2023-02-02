using GameElements;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class ChestGetRewardObserver : MonoBehaviour
{
    [ReadOnly]
    [ShowInInspector]
    private List<IChestGetReward> _rewardListeners =new();

    public void OnRewardRecieved(Chest chest, ChestRewardConfig chestRewardConfig)
    {
        foreach (var rewardHandler in _rewardListeners)
        {
            rewardHandler.OnRewardRecieved(chest, chestRewardConfig);
        }
    }

    public void AddListener(IChestGetReward rewardListener)
    {
        _rewardListeners.Add(rewardListener);
    }

    public void RemoveListener(IChestGetReward rewardListener)
    {
        _rewardListeners.Remove(rewardListener);
    }
}
