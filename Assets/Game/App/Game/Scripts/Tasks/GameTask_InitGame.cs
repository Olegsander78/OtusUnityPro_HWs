using System;
using Services;

namespace Game.App
{
    public sealed class GameTask_InitGame : ILoadingTask
    {
        public void Do(Action<LoadingResult> callback)
        {
            ServiceLocator.GetService<GameManager>().InitGame();
            callback?.Invoke(LoadingResult.Success());
        }
    }
}