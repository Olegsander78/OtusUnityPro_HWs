using GameElements;
using System;
using UnityEngine;


[Serializable]
public class ChestGetReward_Resource : IChestGetReward

{
    public ChestGetReward_Resource()
    {

    }

    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        if(reward is ChestRewardConfig_Resource)
        {
            var resource = (ChestRewardConfig_Resource)reward;
            var amount = resource.GenerateAmountReward();
            var restype = resource.GenerateResourceType();

            Debug.Log($"{restype} = {amount} Resources Reward recieved.");
        }
    }
}

 
