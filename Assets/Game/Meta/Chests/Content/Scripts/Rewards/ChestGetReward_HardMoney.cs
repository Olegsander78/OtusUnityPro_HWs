using GameElements;
using System;
using UnityEngine;

[Serializable]
public class ChestGetReward_HardMoney : IChestGetReward, IGameInitElement

{
    //public ChestGetReward_HardMoney()
    //{
            
    //}

    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        if(reward is ChestRewardConfig_HardMoney)
        {
            Debug.Log("Crystals Reward recieved.");
        }        
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        
    }
}

 
