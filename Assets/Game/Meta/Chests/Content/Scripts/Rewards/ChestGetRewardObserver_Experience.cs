using Entities;
using GameElements;
using Services;
using System;
using UnityEngine;


[Serializable]
public class ChestGetRewardObserver_Experience : ChestRewardObserver
    //IChestGetReward_Observer
{
    //[Inject]
    private HeroService _heroService;

    
    public override void InitGame(IGameContext context)
    {
        base.InitGame(context);
        _heroService = context.GetService<HeroService>();
    }

    protected override void OnRewardRecieved(ChestRewardConfig chestRewardConfig)
    {
        if (chestRewardConfig is ChestRewardConfig_Experience)
        {
            if (!_heroService.GetHero().TryGet(out IComponent_AddExperience component_AddExperience))
            {
                return;
            }

            component_AddExperience.AddExperience(chestRewardConfig.GenerateAmountReward());
            Debug.Log("Experience Reward recieved.");
        }
    }

    //[Inject]
    //private ChestsManager _chestsManager;

    //public ChestGetReward_Experience(HeroService heroService)
    //{
    //    _heroService = heroService;
    //}



    //void IGameInitElement.InitGame(IGameContext context)
    //{
    //    _heroService = context.GetService<HeroService>();
    //    _chestsManager = context.GetService<ChestsManager>();
    //    _chestsManager.AddObserver(typeof(ChestRewardConfig_Experience), this);
    //}


}


