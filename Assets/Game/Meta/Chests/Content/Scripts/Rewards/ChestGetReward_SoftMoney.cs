using GameElements;
using Services;
using System;
using UnityEngine;


[Serializable]
public class ChestGetReward_SoftMoney : IChestGetReward
{
    [Inject]
    private MoneyStorage _moneyStorage;

    //public ChestGetReward_SoftMoney(MoneyStorage moneyStorage)
    //{
    //    _moneyStorage = moneyStorage;
    //}

    [Inject]
    public void Construct(MoneyStorage moneyStorage)
    {
        _moneyStorage = moneyStorage;
    }
    //void IGameInitElement.InitGame(IGameContext context)
    //{
    //    _moneyStorage = context.GetService<MoneyStorage>();
    //}

    void IChestGetReward.OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        if (reward is ChestRewardConfig_SoftMoney)
        {
            _moneyStorage.EarnMoney(reward.GenerateAmountReward());
            Debug.Log("Money Reward recieved.");
        }
    }
}

 
