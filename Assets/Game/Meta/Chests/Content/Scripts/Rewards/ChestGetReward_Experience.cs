using Entities;
using GameElements;
using System;
using UnityEngine;


[Serializable]
public class ChestGetReward_Experience : IChestGetReward
{
    private HeroService _heroService;


    public ChestGetReward_Experience(HeroService heroService)
    {
        _heroService = heroService;
    }

    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
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

 
