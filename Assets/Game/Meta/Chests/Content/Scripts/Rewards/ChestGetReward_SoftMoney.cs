using GameElements;
using UnityEngine;

public class ChestGetReward_SoftMoney : IChestGetReward,
    IGameInitElement
{
    private MoneyStorage _moneyStorage;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _moneyStorage = context.GetService<MoneyStorage>();
    }
 

    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        _moneyStorage.EarnMoney(reward.GenerateAmountReward());
        Debug.Log("Money Reward recieved.");
    }
}

 
