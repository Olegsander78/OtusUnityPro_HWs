using System;
using Services;
using UnityEngine;

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

        Debug.Log($"LaunchGame Task {LoadingResult.Success()}, {gameLauncher}");

    }
}