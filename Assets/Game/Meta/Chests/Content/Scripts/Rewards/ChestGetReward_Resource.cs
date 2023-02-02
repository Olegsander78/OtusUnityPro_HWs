using GameElements;
using Services;
using System;
using UnityEngine;


[Serializable]
public class ChestGetReward_Resource : IChestGetReward, 
    IGameConstructElement,
    IGameInitElement

{
    [Inject]
    private ResourceStorage _resourceStorage;

    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _resourceStorage = context.GetService<ResourceStorage>();
        Debug.Log("<color=red>ResStorage injected in getreward_res</color>");
    }

    [Inject]
    public void Construct(ResourceStorage resourceStorage)
    {
        _resourceStorage = resourceStorage;
        Debug.Log("<color=red>ResStorage injected in getreward_res</color>");
    }

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
        //_resourceStorage = context.GetService<ResourceStorage>();
    }
}

 
