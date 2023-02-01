using GameElements;
using UnityEngine;

public class ChestGetReward_HardMoney : IChestGetReward

{
    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        Debug.Log("Crystals Reward recieved.");
    }
}

 
