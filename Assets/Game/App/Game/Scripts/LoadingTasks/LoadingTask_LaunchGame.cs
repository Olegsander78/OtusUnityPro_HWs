using System;
using Services;

public sealed class LoadingTask_LaunchGame : ILoadingTask
{
    public async void Do(Action<LoadingResult> callback)
    {
        var gameLauncher = ServiceLocator.GetService<GameLauncher>();
        await gameLauncher.LaunchGame();
        callback?.Invoke(LoadingResult.Success());
    }
}