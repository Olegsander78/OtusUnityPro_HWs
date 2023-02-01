using GameElements;
using UnityEngine;

public class ChestGetReward_Resource : IChestGetRewardObserver,
    IGameInitElement
{
    private ChestsManager _chestsManager;

    void IGameInitElement.InitGame(IGameContext context)
    {
        _chestsManager = context.GetService<ChestsManager>();
    }



    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {
        var resource = (ChestRewardConfig_Resource)reward;
        var amount = resource.GenerateAmountReward();
        var restype = resource.GenerateResourceType();

        Debug.Log($"{restype} = {amount} Resources Reward recieved.");
    }
}

 
