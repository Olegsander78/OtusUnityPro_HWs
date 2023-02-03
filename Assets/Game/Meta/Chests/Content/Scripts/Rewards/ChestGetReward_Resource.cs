using GameElements;
using Services;
using System;
using UnityEngine;


[Serializable]
public class ChestGetReward_Resource : IChestGetReward_Observer, 
    IGameConstructElement,
    IGameInitElement

{
    [Inject]
    private ResourceStorage _resourceStorage;

    [Inject]
    private ChestsManager _chestsManager;

    [Inject]
    void IGameConstructElement.ConstructGame(IGameContext context)
    {
        _resourceStorage = context.GetService<ResourceStorage>();
        Debug.Log("<color=red>ResStorage injected in getreward_res</color>");
        _chestsManager.AddObserver(typeof(ChestGetReward_Resource), this);
        Debug.Log("<color=red>ChestMan injected in getreward_res</color>");
    }

    [Inject]
    public void Construct(ResourceStorage resourceStorage)
    {
        _resourceStorage = resourceStorage;
        Debug.Log("<color=red>ResStorage injected in getreward_res</color>");
        _chestsManager.AddObserver(typeof(ChestGetReward_Resource), this);
        Debug.Log("<color=red>ChestMan injected in getreward_res</color>");
    }

    void IGameInitElement.InitGame(IGameContext context)
    {
        //_resourceStorage = context.GetService<ResourceStorage>();
    }

    public void OnRewardRecieved(ChestRewardConfig chestRewardConfig)
    {
        if (chestRewardConfig is ChestRewardConfig_Resource)
        {
            var resource = (ChestRewardConfig_Resource)chestRewardConfig;
            var amount = resource.GenerateAmountReward();
            var restype = resource.GenerateResourceType();

            _resourceStorage.AddResource(restype, amount);
            Debug.Log($"{restype} = {amount} Resources Reward recieved.");
        }
    }
}

 
