using GameElements;
using UnityEngine;

public class ChestGetReward_SoftMoney : IChestGetReward
{
    private MoneyStorage _moneyStorage;

    public ChestGetReward_SoftMoney(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
    }
     

    void IChestGetReward.OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        _moneyStorage.EarnMoney(reward.GenerateAmountReward());
        Debug.Log("Money Reward recieved.");
    }
}

 
