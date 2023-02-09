using System;
using Services;


public sealed class LoadingTask_LaunchGame : ILoadingTask
{
    private readonly GameLauncher gameLauncher;

    [ServiceInject]
    public LoadingTask_LaunchGame(GameLauncher gameLauncher)
    {
        this.gameLauncher = gameLauncher;
    }

    public async void Do(Action<LoadingResult> callback)
    {
        await this.gameLauncher.LaunchGame();
        callback?.Invoke(LoadingResult.Success());
    }
}