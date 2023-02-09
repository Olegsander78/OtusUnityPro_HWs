using System;
using Services;
using UnityEngine;

public sealed class LoadingTask_StartApplicationServices : ILoadingTask
{
    private readonly IAppStartListener[] services;

    [ServiceInject]
    public LoadingTask_StartApplicationServices(IAppStartListener[] services)
    {
        this.services = services;
    }

    public void Do(Action<LoadingResult> callback)
    {
        for (int i = 0, count = this.services.Length; i < count; i++)
        {
            var service = this.services[i];
            service.Start();
        }

        callback?.Invoke(LoadingResult.Success());

        Debug.Log($"StartApp Task {LoadingResult.Success()}, {services.Length}");

    }
}