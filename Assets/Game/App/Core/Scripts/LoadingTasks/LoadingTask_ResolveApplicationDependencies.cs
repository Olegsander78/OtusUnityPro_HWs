using System;
using Services;


public sealed class LoadingTask_ResolveApplicationDependencies : ILoadingTask
{
    public void Do(Action<LoadingResult> callback)
    {
        ServiceInjector.ResolveDependencies();
        callback?.Invoke(LoadingResult.Success());
    }
}