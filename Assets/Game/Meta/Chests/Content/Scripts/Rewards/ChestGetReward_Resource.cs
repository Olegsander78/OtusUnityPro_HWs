using GameElements;
using System;
using UnityEngine;


[Serializable]
public class ChestGetReward_Resource : IChestGetReward, IGameInitElement

{
    private ResourceStorage _resourceStorage;

    //public ChestGetReward_Resource()
    //{

    //}

    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        if(reward is ChestRewardConfig_Resource)
        {
            var resource = (ChestRewardConfig_Resource)reward;
            var amount = resource.GenerateAmountReward();
            var restype = resource.GenerateResourceType();

            _resourceStorage.AddResource(restype, amount);
            Debug.Log($"{restype} = {amount} Resources Reward recieved.");
        }
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        _resourceStorage = context.GetService<ResourceStorage>();
    }
}

 
