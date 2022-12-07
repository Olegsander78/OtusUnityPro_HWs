using System;
using System.Threading.Tasks;
using Services;


public sealed class GameTask_ReadyGame : ILoadingTask
{
    public void Do(Action<LoadingResult> callback)
    {
        ServiceLocator.GetService<GameManager>().ReadyGame();
        callback.Invoke(LoadingResult.Success());
    }
}