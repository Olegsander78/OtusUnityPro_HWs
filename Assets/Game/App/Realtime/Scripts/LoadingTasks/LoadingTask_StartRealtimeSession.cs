using System;
using Services;


public sealed class LoadingTask_StartRealtimeSession : ILoadingTask
{
    public async void Do(Action<LoadingResult> callback)
    {
        var sessionStarter = ServiceLocator.GetService<RealtimeSessionStarter>();
        await sessionStarter.StartSessionAsync();
        callback?.Invoke(LoadingResult.Success());
    }
}