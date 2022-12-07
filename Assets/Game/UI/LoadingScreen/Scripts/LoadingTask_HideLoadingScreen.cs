using System;
using UnityEngine;


public sealed class LoadingTask_HideLoadingScreen : ILoadingTask
{
    public void Do(Action<LoadingResult> callback)
    {
        var loadingScreen = GameObject.FindObjectOfType<LoadingScreen>();
        loadingScreen.Hide();
        callback.Invoke(LoadingResult.Success());
    }
}