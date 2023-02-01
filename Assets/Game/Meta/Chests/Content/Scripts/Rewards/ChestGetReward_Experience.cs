using Entities;
using GameElements;
using UnityEngine;

public class ChestGetReward_Experience : IChestGetRewardObserver,
    IGameInitElement
{
    private IEntity _hero;

    private IComponent_AddExperience _componentAddExp;

    private ChestsManager _chestsManager;


    void IGameInitElement.InitGame(IGameContext context)
    {
        _chestsManager = context.GetService<ChestsManager>();
        _hero = context.GetService<HeroService>().GetHero();

        _componentAddExp = _hero.Get<IComponent_AddExperience>();
    }



    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {        
        _componentAddExp.AddExperience(reward.GenerateAmountReward());
        Debug.Log("Experience Reward recieved.");
    }
}

 
