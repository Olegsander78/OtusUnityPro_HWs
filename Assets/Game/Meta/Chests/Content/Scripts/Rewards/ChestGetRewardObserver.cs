using GameElements;
using System.Collections.Generic;
using UnityEngine;

public class ChestGetRewardObserver : MonoBehaviour,    
    IGameStartElement,
    IGameFinishElement
{
    [SerializeField]
    private ChestsManager _chestsManager;


    [SerializeReference]
    private List<IChestGetReward> _chestGetRewardsHandler;

    //void IGameInitElement.InitGame(IGameContext context)
    //{
    //    throw new System.NotImplementedException();
    //}

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
        foreach (var reward in _chestGetRewardsHandler)
        {
            //if (reward is chestRewardConfig)
            //{
            //    reward.OnRewardRecieved(chest, chestRewardConfig);
            //}
            reward.OnRewardRecieved(chest, chestRewardConfig);
        }
    }
}
