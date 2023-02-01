using GameElements;
using UnityEngine;

public class ChestGetRewardObserver : MonoBehaviour,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    [SerializeField]
    private ChestsManager _chestsManager;

    private IChestGetReward _chestGetReward;

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
        throw new System.NotImplementedException();
    }
}
