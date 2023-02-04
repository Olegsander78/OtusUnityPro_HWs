using UnityEngine;

public class ChestGetRewardObserver_HardMoney : IChestRewardObserver

{
    public ChestGetRewardObserver_HardMoney() { } 


    void IChestRewardObserver.OnRewardReceived(ChestRewardConfig reward)
    {
        if (reward is ChestRewardConfig_HardMoney)
        {
            Debug.Log("Crystals Reward recieved.");
        }
    }
}

 
