using GameElements;
using UnityEngine;

public class ChestGetReward_SoftMoney : IChestGetRewardObserver,
    IGameInitElement
{
    private MoneyStorage _moneyStorage;

    private ChestsManager _chestsManager;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _chestsManager = context.GetService<ChestsManager>();
        _moneyStorage = context.GetService<MoneyStorage>();
    }

 

    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        _moneyStorage.EarnMoney(reward.GenerateAmountReward());
        Debug.Log("Money Reward recieved.");
    }
}

 
