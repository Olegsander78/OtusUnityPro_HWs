using System;
using Services;
using UnityEngine;

public sealed class GameTask_InitGame : ILoadingTask
{
    private readonly GameManager gameManager;

    [ServiceInject]
    public GameTask_InitGame(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Do(Action<LoadingResult> callback)
    {
        this.gameManager.InitGame();
        callback?.Invoke(LoadingResult.Success());

        Debug.Log($"InitGame Session {LoadingResult.Success()}, {gameManager}");

    }
}