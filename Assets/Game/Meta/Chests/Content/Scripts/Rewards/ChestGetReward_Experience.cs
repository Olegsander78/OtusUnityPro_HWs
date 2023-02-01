using Entities;
using GameElements;
using UnityEngine;

public class ChestGetReward_Experience : IChestGetReward,
    IGameInitElement
{
    private IEntity _hero;

    private IComponent_AddExperience _componentAddExp;


    void IGameInitElement.InitGame(IGameContext context)
    {
        _hero = context.GetService<HeroService>().GetHero();

        _componentAddExp = _hero.Get<IComponent_AddExperience>();
    }



    public void OnRewardRecieved(Chest chest, ChestRewardConfig reward)
    {        
        _componentAddExp.AddExperience(reward.GenerateAmountReward());
        Debug.Log("Experience Reward recieved.");
    }
}

 
