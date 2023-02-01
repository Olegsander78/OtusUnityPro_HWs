using GameElements;
using System.Collections.Generic;
using UnityEngine;

public class ChestGetRewardObserver : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    [SerializeField]
    private ChestsManager _chestsManager;


    [SerializeReference]
    private List<IChestGetReward> _chestGetRewardsHandler;

    void IGameInitElement.InitGame(IGameContext context)
    {
        //foreach (var rewardHandler in _chestGetRewardsHandler)
        //{
            
        //}
    }

    void IGameStartElement.StartGame(IGameContext context)
    {
        _chestsManager.OnRewardReceived += OnRewardRecieved;
    }

    void IGameFinishElement.FinishGame(IGameContext context)
    {
        _chestsManager.OnRewardReceived -= OnRewardRecieved;
    }

    public void OnRewardRecieved(Chest chest, ChestRewardConfig chestRewardConfig)
    {
        foreach (var rewardHandler in _chestGetRewardsHandler)
        {
            //if (reward is chestRewardConfig)
            //{
            //    reward.OnRewardRecieved(chest, chestRewardConfig);
            //}
            rewardHandler.OnRewardRecieved(chest, chestRewardConfig);
        }
    }
}
