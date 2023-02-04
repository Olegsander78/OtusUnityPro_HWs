using System;
using UnityEngine;


public class ChestGetRewardObserver_Experience : IChestRewardObserver
{
    private HeroService _heroService;

    public ChestGetRewardObserver_Experience(HeroService heroService)
    {
        _heroService = heroService;
    }
     

    void IChestRewardObserver.OnRewardReceived(ChestRewardConfig reward)
    {
        if (reward is ChestRewardConfig_Experience)
        {
            if (!_heroService.GetHero().TryGet(out IComponent_AddExperience component_AddExperience))
            {
                return;
            }

            component_AddExperience.AddExperience(reward.GenerateAmountReward());
            Debug.Log("Experience Reward recieved.");
        }
    }
}


