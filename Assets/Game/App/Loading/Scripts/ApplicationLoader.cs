using System;
using System.Threading.Tasks;
using Services;
using UnityEngine;
using UnityEngine.Serialization;


[AddComponentMenu("App/Application Loader")]
public sealed class ApplicationLoader : MonoBehaviour
{
    public event Action OnCompleted;

    public event Action<string> OnFailed;

    [SerializeField]
    private bool loadOnStart = true;

    [Space]
    [SerializeField]
    [FormerlySerializedAs("config")]
    private LoadingPipeline pipeline;

    private int taskPointer;

    private void Start()
    {
        if (this.loadOnStart)
        {
            this.LoadApplication();
        }
    }

    public async void LoadApplication()
    {
        var taskList = this.pipeline.GetTaskList();
        for (int i = 0, count = taskList.Length; i < count; i++)
        {
            var taskType = taskList[i];
            var result = await this.DoTask(taskType);
            if (!result.success)
            {
                this.OnFailed?.Invoke(result.error);
                return;
            }
        }

        this.OnCompleted?.Invoke();
    }

    private Task<LoadingResult> DoTask(Type taskType)
    {
        var tcs = new TaskCompletionSource<LoadingResult>();
        var loadingTask = (ILoadingTask)ServiceInjector.Instantiate(taskType);
        loadingTask.Do(result => tcs.SetResult(result));
        return tcs.Task;
    }
}