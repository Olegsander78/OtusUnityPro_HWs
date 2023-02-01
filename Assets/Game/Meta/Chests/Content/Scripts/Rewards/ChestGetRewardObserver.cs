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

    [SerializeField]
    private List<IChestGetReward> _chestGetRewardsHandler = new();

    void IGameInitElement.InitGame(IGameContext context)
    {
        throw new System.NotImplementedException();
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
        foreach (var reward in _chestGetRewardsHandler)
        {
            //if (reward is chestRewardConfig)
            //{
            //    reward.OnRewardRecieved(chest, chestRewardConfig);
            //}
        }
    }
}
