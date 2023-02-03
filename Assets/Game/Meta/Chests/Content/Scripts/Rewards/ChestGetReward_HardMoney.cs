using GameElements;
using Services;
using System;
using UnityEngine;

[Serializable]
public class ChestGetReward_HardMoney : IChestGetReward_Observer, IGameInitElement

{
    [Inject]
    private ChestsManager _chestsManager;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _chestsManager.AddObserver(typeof(ChestGetReward_HardMoney), this);
    }

    public void OnRewardRecieved(ChestRewardConfig chestRewardConfig)
    {
        if (chestRewardConfig is ChestRewardConfig_HardMoney)
        {
            Debug.Log("Crystals Reward recieved.");
        }
    }
}

 
