using System;
using Services;

namespace Game.App
{
    public sealed class GameTask_StartGame : ILoadingTask
    {
        public void Do(Action<LoadingResult> callback)
        {
            var gameManager = ServiceLocator.GetService<GameManager>();
            gameManager.StartGame();
            callback?.Invoke(LoadingResult.Success());
        }
    }
}