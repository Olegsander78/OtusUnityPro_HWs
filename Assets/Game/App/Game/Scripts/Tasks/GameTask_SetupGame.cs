using System;
using System.Threading.Tasks;
using GameElements.Unity;
using Services;
using UnityEngine;

namespace Game.App
{
    public sealed class GameTask_SetupGame : ILoadingTask
    {
        public void Do(Action<LoadingResult> callback)
        {
            var gameSystem = GameObject.FindObjectOfType<MonoGameContext>();
            var gameManager = ServiceLocator.GetService<GameManager>();
            gameManager.Setup(gameSystem);
            callback?.Invoke(LoadingResult.Success());
        }
    }
}