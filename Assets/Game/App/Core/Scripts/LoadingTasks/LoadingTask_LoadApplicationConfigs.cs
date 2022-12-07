using System;
using Services;


public sealed class LoadingTask_LoadApplicationConfigs : ILoadingTask
{
    public void Do(Action<LoadingResult> callback)
    {
        var configLoaders = ServiceLocator.GetServices<IAppConfigsLoader>();
        foreach (var loader in configLoaders)
        {
            loader.LoadConfigs();
        }

        callback?.Invoke(LoadingResult.Success());
    }
}