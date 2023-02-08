using System;
using Services;
using UnityEngine;

public sealed class LoadingTask_StartRealtimeSession : ILoadingTask
{
    public async void Do(Action<LoadingResult> callback)
    {
        var sessionStarter = ServiceLocator.GetService<RealtimeSessionStarter>();
        await sessionStarter.StartSessionAsync();
        callback?.Invoke(LoadingResult.Success());
        Debug.Log(LoadingResult.Success());
    }

    //public async void Do(Action<LoadingResult> callback)
    //{
    //    var sessionStarter = ServiceLocator.GetService<RealtimeStarter>();
    //    await sessionStarter.StartSessionAsync();
    //    callback?.Invoke(LoadingResult.Success());
    //}
}