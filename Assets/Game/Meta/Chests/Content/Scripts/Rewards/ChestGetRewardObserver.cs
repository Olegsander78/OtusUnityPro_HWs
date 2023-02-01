using GameElements;
using UnityEngine;

public class ChestGetRewardObserver : MonoBehaviour, IChestGetRewardObserver,
    IGameInitElement,
    IGameStartElement,
    IGameFinishElement
{
    private ChestsManager _chestsManager;

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
