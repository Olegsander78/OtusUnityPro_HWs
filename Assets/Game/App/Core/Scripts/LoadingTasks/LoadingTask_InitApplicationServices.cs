using System;
using Services;


public sealed class LoadingTask_InitApplicationServices : ILoadingTask
{
    public void Do(Action<LoadingResult> callback)
    {
        var listeners = ServiceLocator.GetServices<IAppInitListener>();
        foreach (var listener in listeners)
        {
            listener.Init();
        }

        callback?.Invoke(LoadingResult.Success());
    }
}