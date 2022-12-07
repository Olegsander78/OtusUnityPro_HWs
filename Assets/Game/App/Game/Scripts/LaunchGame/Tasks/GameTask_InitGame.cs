using System;
using Services;


public sealed class GameTask_InitGame : ILoadingTask
{
    public void Do(Action<LoadingResult> callback)
    {
        ServiceLocator.GetService<GameManager>().InitGame();
        callback?.Invoke(LoadingResult.Success());
    }
}