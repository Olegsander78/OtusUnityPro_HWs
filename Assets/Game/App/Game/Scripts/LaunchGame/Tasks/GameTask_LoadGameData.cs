using System;
using Services;
using UnityEngine;

public sealed class GameTask_LoadGameData : ILoadingTask
{
    private readonly GameManager gameManager;

    private readonly IGameLoadDataListener[] loadListeners;

    [ServiceInject]
    public GameTask_LoadGameData(GameManager gameManager, IGameLoadDataListener[] loadListeners)
    {
        this.gameManager = gameManager;
        this.loadListeners = loadListeners;
    }

    public void Do(Action<LoadingResult> callback)
    {
        for (int i = 0, count = this.loadListeners.Length; i < count; i++)
        {
            var listener = this.loadListeners[i];
            listener.OnLoadData(this.gameManager);
        }

        callback?.Invoke(LoadingResult.Success());

        Debug.Log($"LoadGame Session {LoadingResult.Success()}, {loadListeners.Length}");

    }
}