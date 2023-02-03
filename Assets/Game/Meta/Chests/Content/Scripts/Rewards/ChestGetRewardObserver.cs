using GameElements;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class ChestGetRewardObserver : MonoBehaviour
{
    [ReadOnly]
    [ShowInInspector]
    private List<IChestGetReward_Observer> _rewardListeners =new();

    public void OnRewardRecieved(Chest chest, ChestRewardConfig chestRewardConfig)
    {
        foreach (var rewardHandler in _rewardListeners)
        {
            rewardHandler.OnRewardRecieved(chestRewardConfig);
        }
    }

    public void AddListener(IChestGetReward_Observer rewardListener)
    {
        _rewardListeners.Add(rewardListener);
    }

    public void RemoveListener(IChestGetReward_Observer rewardListener)
    {
        _rewardListeners.Remove(rewardListener);
    }
}
