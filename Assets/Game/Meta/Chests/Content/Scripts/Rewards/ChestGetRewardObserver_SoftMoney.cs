using UnityEngine;

public class ChestGetRewardObserver_SoftMoney : IChestRewardObserver
{
    
    private readonly MoneyStorage _moneyStorage;

    public ChestGetRewardObserver_SoftMoney(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
    }

    void IChestRewardObserver.OnRewardReceived(ChestRewardConfig reward)
    {
        if (reward is ChestRewardConfig_SoftMoney)
        {
            _moneyStorage.EarnMoney(reward.GenerateAmountReward());
            Debug.Log("Money Reward recieved.");
        }
    }
}

 
