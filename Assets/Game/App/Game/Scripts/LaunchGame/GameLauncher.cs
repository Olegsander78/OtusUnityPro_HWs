using System;
using System.Threading.Tasks;
using Services;
using UnityEngine;


public sealed class GameLauncher
{
    private const string LAUNCH_PIPELINE = "GameLaunchPipeline";

    public async Task LaunchGame()
    {
        var taskPipeline = Resources.Load<LoadingPipeline>(LAUNCH_PIPELINE);
        var taskList = taskPipeline.GetTaskList();
        for (int i = 0, count = taskList.Length; i < count; i++)
        {
            var taskType = taskList[i];
            await DoTask(taskType);
        }
    }

    private static Task<LoadingResult> DoTask(Type taskType)
    {
        var tcs = new TaskCompletionSource<LoadingResult>();
        var loadingTask = (ILoadingTask)ServiceInjector.Instantiate(taskType);
        loadingTask.Do(result => tcs.SetResult(result));
        return tcs.Task;
    }
}