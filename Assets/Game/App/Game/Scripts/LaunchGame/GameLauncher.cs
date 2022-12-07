using System.Threading.Tasks;
using UnityEngine;

public sealed class GameLauncher
{
    private const string LAUNCH_PIPELINE = "GameLaunchPipeline";

    public async Task LaunchGame()
    {
        var taskPipeline = Resources.Load<LoadingPipeline>(LAUNCH_PIPELINE);
        var tasks = taskPipeline.GetTasks();
        for (int i = 0, count = tasks.Length; i < count; i++)
        {
            var task = tasks[i];
            await DoTask(task);
        }
    }

    private static Task<LoadingResult> DoTask(ILoadingTask loadingTask)
    {
        var tcs = new TaskCompletionSource<LoadingResult>();
        loadingTask.Do(result => tcs.SetResult(result));
        return tcs.Task;
    }
}