using System;
using Services;


public sealed class GameTask_StartGame : ILoadingTask
{
    private readonly GameManager gameManager;

    [ServiceInject]
    public GameTask_StartGame(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public void Do(Action<LoadingResult> callback)
    {
        this.gameManager.StartGame();
        callback?.Invoke(LoadingResult.Success());
    }
}