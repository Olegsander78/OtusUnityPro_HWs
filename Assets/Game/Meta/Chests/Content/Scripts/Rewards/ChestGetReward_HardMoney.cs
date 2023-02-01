using GameElements;
using UnityEngine;

public class ChestGetReward_HardMoney : IChestGetReward

{
    public ChestGetReward_HardMoney()
    {
            
    }

    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        Debug.Log("Crystals Reward recieved.");
    }
}

 
