using System;

namespace GameElements
{
    public interface IGameContext 
    {
        event Action OnGameInitialized;
        
        event Action OnGameReady;
        
        event Action OnGameStarted;
        
        event Action OnGamePaused;

        event Action OnGameResumed;

        event Action OnGameFinished;

        GameState State { get; }
        
        ///Lifecycle:
        void InitGame();

        void ReadyGame();

        void StartGame();

        void PauseGame();
        
        void ResumeGame();
    
        void FinishGame();
        
        /// Elements:
        void RegisterElement(IGameElement element);

        void UnregisterElement(IGameElement element);

        /// Services:
        void RegisterService(object service);

        void UnregisterService(object service);

        T GetService<T>();

        object[] GetAllServices();

        object GetService(Type type);

        bool TryGetService<T>(out T service);

        bool TryGetService(Type type, out object service);
    }
}