using System;
using Services;
using UnityEngine;

public sealed class GameTask_StartGame : ILoadingTask
{
    private readonly GameManager gameManager;

    [ServiceInject]
    public GameTask_StartGame(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Do(Action<LoadingResult> callback)
    {
        this.gameManager.StartGame();
        callback?.Invoke(LoadingResult.Success());
        Debug.Log($"StartGame Session!!! {LoadingResult.Success()}, {gameManager} ");
    }
}