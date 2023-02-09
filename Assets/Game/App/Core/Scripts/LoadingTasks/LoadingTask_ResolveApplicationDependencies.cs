using System;
using System.Threading.Tasks;
using Services;
using UnityEngine;


public sealed class LoadingTask_ResolveApplicationDependencies : ILoadingTask
{
    public async void Do(Action<LoadingResult> callback)
    {
        await ServiceInjector.ResolveDependenciesAsync();
        callback?.Invoke(LoadingResult.Success());

        Debug.Log($"ResolveAppDP Task {LoadingResult.Success()}");

    }
}