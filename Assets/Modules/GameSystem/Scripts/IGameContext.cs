using System;
using System.Collections.Generic;

namespace GameSystem
{
    public interface IGameContext
    {
        event Action OnGameConstructed;
        
        event Action OnGameInitialized;

        event Action OnGameReady;
        
        event Action OnGameStarted;
        
        event Action OnGamePaused;

        event Action OnGameResumed;

        event Action OnGameFinished;

        GameState State { get; }

        void ConstructGame();

        void InitGame();

        void ReadyGame();

        void StartGame();

        void PauseGame();
        
        void ResumeGame();
    
        void FinishGame();

        ///Construct tasks:
        void SetupTasks(IConstructTask[] tasks);

        /// Elements:
        void RegisterElement(IGameElement element);

        void UnregisterElement(IGameElement element);

        object[] GetAllElements();

        /// Services:
        void RegisterService(object service);

        void UnregisterService(object service);

        T GetService<T>();

        object GetService(Type type);

        bool TryGetService<T>(out T service);

        bool TryGetService(Type type, out object service);

        T[] GetServices<T>();
        
        object[] GetServices(Type type);

        object[] GetAllServices();
    }
}