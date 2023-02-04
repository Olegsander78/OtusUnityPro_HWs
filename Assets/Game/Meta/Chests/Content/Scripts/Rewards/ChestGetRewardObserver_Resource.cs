using UnityEngine;


public class ChestGetRewardObserver_Resource : IChestRewardObserver

{
    private ResourceStorage _resourceStorage;

    public ChestGetRewardObserver_Resource(ResourceStorage resourceStorage)
    {
        _resourceStorage = resourceStorage;
    }
 
    void IChestRewardObserver.OnRewardReceived(ChestRewardConfig reward)
    {
        if (reward is ChestRewardConfig_Resource)
        {
            var resource = (ChestRewardConfig_Resource)reward;
            var amount = resource.GenerateAmountReward();
            var restype = resource.GenerateResourceType();

            _resourceStorage.AddResource(restype, amount);
            Debug.Log($"{restype} = {amount} Resources Reward recieved.");
        }
    }
}

 
