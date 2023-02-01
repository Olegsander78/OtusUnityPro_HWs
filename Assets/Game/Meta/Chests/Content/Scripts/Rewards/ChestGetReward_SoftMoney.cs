using GameElements;
using System;
using UnityEngine;


[Serializable]
public class ChestGetReward_SoftMoney : IChestGetReward
{
    private MoneyStorage _moneyStorage;

    public ChestGetReward_SoftMoney(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
    }
     

    void IChestGetReward.OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        if (reward is ChestRewardConfig_SoftMoney)
        {
            _moneyStorage.EarnMoney(reward.GenerateAmountReward());
            Debug.Log("Money Reward recieved.");
        }
    }
}

 
