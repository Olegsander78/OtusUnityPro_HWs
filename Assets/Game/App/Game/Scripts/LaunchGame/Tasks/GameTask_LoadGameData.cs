using System;
using Services;


public sealed class GameTask_LoadGameData : ILoadingTask
{
    public void Do(Action<LoadingResult> callback)
    {
        var gameManager = ServiceLocator.GetService<GameManager>();
        var setupListeners = ServiceLocator.GetServices<IGameLoadDataListener>();
        foreach (var listener in setupListeners)
        {
            listener.OnLoadData(gameManager);
        }

        callback?.Invoke(LoadingResult.Success());
    }
}