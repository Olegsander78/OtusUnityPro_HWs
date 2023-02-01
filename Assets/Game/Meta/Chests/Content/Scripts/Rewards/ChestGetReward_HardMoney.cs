using GameElements;
using UnityEngine;

public class ChestGetReward_HardMoney : IChestGetRewardObserver,
    IGameInitElement
{

    private ChestsManager _chestsManager;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _chestsManager = context.GetService<ChestsManager>();
    }


    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        Debug.Log("Crystals Reward recieved.");
    }
}

 
