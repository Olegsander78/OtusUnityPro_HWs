using System;
using System.Collections;
using Asyncoroutine;
using UnityEngine;
using UnityEngine.SceneManagement;


public sealed class GameTask_LoadScene : ILoadingTask
{
    private const string GAME_SCENE = "GameScene";

    public async void Do(Action<LoadingResult> callback)
    {
        await LoadSceneRoutine();
        callback?.Invoke(LoadingResult.Success());

        Debug.Log($"LoadScene Session {LoadingResult.Success()}");

    }

    private static IEnumerator LoadSceneRoutine()
    {
        var operation = SceneManager.LoadSceneAsync(GAME_SCENE, LoadSceneMode.Single);
        while (!operation.isDone)
        {
            yield return null;
        }
    }
}