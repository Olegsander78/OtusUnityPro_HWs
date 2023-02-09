using System;
using Services;
using UnityEngine;

public sealed class LoadingTask_LoadApplicationConfigs : ILoadingTask
{
    private readonly IAppConfigsLoader[] loaders;

    [ServiceInject]
    public LoadingTask_LoadApplicationConfigs(IAppConfigsLoader[] loaders)
    {
        this.loaders = loaders;
    }

    public void Do(Action<LoadingResult> callback)
    {
        for (int i = 0, count = this.loaders.Length; i < count; i++)
        {
            var loader = this.loaders[i];
            loader.LoadConfigs();
        }

        callback?.Invoke(LoadingResult.Success());

        Debug.Log($"LoadAppConf Task {LoadingResult.Success()}, {loaders.Length}");

    }
}