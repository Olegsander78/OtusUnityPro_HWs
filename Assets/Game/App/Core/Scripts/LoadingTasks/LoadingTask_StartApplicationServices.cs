using System;
using Services;


public sealed class LoadingTask_StartApplicationServices : ILoadingTask
{
    public void Do(Action<LoadingResult> callback)
    {
        var listeners = ServiceLocator.GetServices<IAppStartListener>();
        foreach (var listener in listeners)
        {
            listener.Start();
        }

        callback?.Invoke(LoadingResult.Success());
    }
}