using System;
using System.Threading.Tasks;
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
    [FormerlySerializedAs("_config")]
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
        var tasks = this.pipeline.GetTasks();
        for (int i = 0, count = tasks.Length; i < count; i++)
        {
            var task = tasks[i];
            var result = await this.DoTask(task);
            if (!result.success)
            {
                this.OnFailed?.Invoke(result.error);
                return;
            }
        }

        this.OnCompleted?.Invoke();
    }

    private Task<LoadingResult> DoTask(ILoadingTask loadingTask)
    {
        var tcs = new TaskCompletionSource<LoadingResult>();
        loadingTask.Do(result => tcs.SetResult(result));
        return tcs.Task;
    }
}