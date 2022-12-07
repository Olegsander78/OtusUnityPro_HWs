using System;
using Services;


public sealed class GameTask_NotifyAboutSetup : ILoadingTask
{
    public void Do(Action<LoadingResult> callback)
    {
        var gameManager = ServiceLocator.GetService<GameManager>();
        var setupListeners = ServiceLocator.GetServices<IGameSetupListener>();
        foreach (var listener in setupListeners)
        {
            listener.OnSetupGame(gameManager);
        }

        callback?.Invoke(LoadingResult.Success());
    }
}