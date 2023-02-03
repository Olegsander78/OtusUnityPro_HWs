using GameElements;
using Services;
using System;
using UnityEngine;

//[Serializable]
public class ChestGetRewardObserver_HardMoney : ChestRewardObserver
    //IChestGetReward_Observer

{

    protected override void OnRewardRecieved(ChestRewardConfig chestRewardConfig)
    {
        if (chestRewardConfig is ChestRewardConfig_HardMoney)
        {
            Debug.Log("Crystals Reward recieved.");
        }
    }

    //[Inject]
    //private ChestsManager _chestsManager;

    //void IGameInitElement.InitGame(IGameContext context)
    //{
    //    _chestsManager.AddObserver(typeof(ChestGetRewardObserver_HardMoney), this);
    //}


}

 
