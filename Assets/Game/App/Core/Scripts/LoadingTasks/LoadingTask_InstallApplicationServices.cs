using System;
using Services;
using UnityEngine;


public sealed class LoadingTask_InstallApplicationServices : ILoadingTask
{
    public void Do(Action<LoadingResult> callback)
    {
        var serviceInstaller = GameObject.FindObjectOfType<ServiceInstaller>();
        serviceInstaller.InstallServices();
        callback.Invoke(LoadingResult.Success());
    }
}