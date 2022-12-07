using System;
using Services;


public sealed class GameTask_NotifyAboutStart : ILoadingTask
{
    public void Do(Action<LoadingResult> callback)
    {
        var gameManager = ServiceLocator.GetService<GameManager>();
        var startListeners = ServiceLocator.GetServices<IGameStartListener>();
        foreach (var listener in startListeners)
        {
            listener.OnStartGame(gameManager);
        }

        callback?.Invoke(LoadingResult.Success());
    }
}