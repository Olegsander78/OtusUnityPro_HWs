using System;
using System.Linq;

#if UNITY_EDITOR
using Debug = UnityEngine.Debug;
#endif

namespace GameElements
{
    public class GameContext : IGameContext
    {
        public event Action OnGameInitialized;

        public event Action OnGameReady;

        public event Action OnGameStarted;

        public event Action OnGamePaused;

        public event Action OnGameResumed;

        public event Action OnGameFinished;

        public GameState State { get; protected set; }

        private readonly ElementContext elementContext;

        private readonly ServiceContext serviceContext;

        public GameContext()
        {
            this.State = GameState.IDLE;
            this.elementContext = new ElementContext(this);
            this.serviceContext = new ServiceContext();
        }

        public void InitGame()
        {
            if (this.State != GameState.IDLE)
            {
                LogWarning($"Can't initialize the game from the {this.State} state, " +
                           $"only from {nameof(GameState.IDLE)}");
                return;
            }

            this.OnInitGame();
            this.State = GameState.INIT;
            this.elementContext.InitGame();
            this.OnGameInitialized?.Invoke();
        }

        protected virtual void OnInitGame()
        {
        }

        public void ReadyGame()
        {
            if (this.State != GameState.INIT)
            {
                LogWarning($"Can't set ready the game from the {this.State} state, " +
                           $"only from {nameof(GameState.INIT)}");
                return;
            }

            this.OnReadyGame();
            this.State = GameState.READY;
            this.elementContext.ReadyGame();
            this.OnGameReady?.Invoke();
        }

        protected virtual void OnReadyGame()
        {
        }

        public void StartGame()
        {
            if (this.State != GameState.READY)
            {
                LogWarning($"Can't start the game from the {this.State} state, " +
                           $"only from {nameof(GameState.READY)}");
                return;
            }

            this.OnStartGame();
            this.State = GameState.PLAY;
            this.elementContext.StartGame();
            this.OnGameStarted?.Invoke();
        }

        protected virtual void OnStartGame()
        {
        }

        public void PauseGame()
        {
            if (this.State != GameState.PLAY)
            {
                LogWarning($"Can't pause the game from the {this.State} state, " +
                           $"only from {nameof(GameState.PLAY)}");
                return;
            }

            this.OnPauseGame();
            this.State = GameState.PAUSE;
            this.elementContext.PauseGame();
            this.OnGamePaused?.Invoke();
        }

        protected virtual void OnPauseGame()
        {
        }

        public void ResumeGame()
        {
            if (this.State != GameState.PAUSE)
            {
                LogWarning($"Can't resume the game from the {this.State} state, " +
                           $"only from {nameof(GameState.PAUSE)}");
                return;
            }

            this.OnResumeGame();
            this.State = GameState.PLAY;
            this.elementContext.ResumeGame();
            this.OnGameResumed?.Invoke();
        }

        protected virtual void OnResumeGame()
        {
        }

        public void FinishGame()
        {
            if (this.State == GameState.PLAY || this.State == GameState.PAUSE)
            {
                this.OnFinishGame();
                this.State = GameState.FINISH;
                this.elementContext.FinishGame();
                this.OnGameFinished?.Invoke();
            }
            else
            {
                LogWarning($"Can't finish the game from the {this.State} state, " +
                           $"only from {nameof(GameState.PLAY)} or {nameof(GameState.PAUSE)}");
            }
        }

        protected virtual void OnFinishGame()
        {
        }

        public void RegisterElement(IGameElement element)
        {
            this.elementContext.AddElement(element);
        }

        public void UnregisterElement(IGameElement element)
        {
            this.elementContext.RemoveElement(element);
        }

        public void RegisterService(object service)
        {
            this.serviceContext.AddService(service);
        }

        public void UnregisterService(object service)
        {
            this.serviceContext.RemoveService(service);
        }

        public T GetService<T>()
        {
            return this.serviceContext.GetService<T>();
        }

        public object[] GetAllServices()
        {
            return this.serviceContext.GetAllServices().ToArray();
        }

        public object GetService(Type type)
        {
            return this.serviceContext.GetService(type);
        }

        public bool TryGetService<T>(out T service)
        {
            return this.serviceContext.TryGetService(out service);
        }

        public bool TryGetService(Type type, out object service)
        {
            return this.serviceContext.TryGetService(type, out service);
        }

        private static void LogWarning(string message)
        {
#if UNITY_EDITOR
            Debug.LogWarning(message);
#endif
        }
    }
}