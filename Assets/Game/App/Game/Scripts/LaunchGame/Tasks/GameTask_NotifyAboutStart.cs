using System;
using Services;
using UnityEngine;

public sealed class GameTask_NotifyAboutStart : ILoadingTask
{
    private readonly GameManager gameManager;

    private readonly IGameStartListener[] startListeners;

    [ServiceInject]
    public GameTask_NotifyAboutStart(GameManager gameManager, IGameStartListener[] startListeners)
    {
        this.gameManager = gameManager;
        this.startListeners = startListeners;
    }

    public void Do(Action<LoadingResult> callback)
    {
        for (int i = 0, count = this.startListeners.Length; i < count; i++)
        {
            var listener = this.startListeners[i];
            listener.OnStartGame(this.gameManager);
        }

        callback?.Invoke(LoadingResult.Success());

        Debug.Log($"NotifyAboutStartGame Session {LoadingResult.Success()}, {startListeners.Length}");

    }
}