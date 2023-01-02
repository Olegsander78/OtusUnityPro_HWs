using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    [AddComponentMenu("GameSystem/Game Сontext")]
    [DisallowMultipleComponent]
    public class MonoGameContext : MonoBehaviour, IGameContext
    {
        public event Action OnGameConstructed
        {
            add { this.gameContext.OnGameConstructed += value; }
            remove { this.gameContext.OnGameConstructed -= value; }
        }

        public event Action OnGameInitialized
        {
            add { this.gameContext.OnGameInitialized += value; }
            remove { this.gameContext.OnGameInitialized -= value; }
        }

        public event Action OnGameReady
        {
            add { this.gameContext.OnGameReady += value; }
            remove { this.gameContext.OnGameReady -= value; }
        }

        public event Action OnGameStarted
        {
            add { this.gameContext.OnGameStarted += value; }
            remove { this.gameContext.OnGameStarted -= value; }
        }

        public event Action OnGamePaused
        {
            add { this.gameContext.OnGamePaused += value; }
            remove { this.gameContext.OnGameResumed -= value; }
        }

        public event Action OnGameResumed
        {
            add { this.gameContext.OnGameResumed += value; }
            remove { this.gameContext.OnGameResumed -= value; }
        }

        public event Action OnGameFinished
        {
            add { this.gameContext.OnGameFinished += value; }
            remove { this.gameContext.OnGameFinished -= value; }
        }

        public GameState State
        {
            get { return this.gameContext.State; }
        }

        [SerializeField]
        private bool autoRun = true;

        [SerializeField]
        private bool useInject;

        [Space]
        [SerializeField]
        private List<MonoBehaviour> gameServices = new();

        [Space]
        [SerializeField]
        private List<MonoBehaviour> gameElements = new();

        [Space]
        [SerializeField]
        private List<ConstructTask> constructTasks = new();

        private readonly GameContext gameContext = new();

        private readonly UpdateContext updateContext = new();

        [ContextMenu("Construct Game")]
        public void ConstructGame()
        {
            this.RegisterElements();
            this.RegisterServices();
            this.RegisterConstructTasks();

            if (this.useInject)
            {
                GameInjector.InjectAll(this, this.GetAllElements());
                GameInjector.InjectAll(this, this.GetAllServices());
            }

            this.gameContext.ConstructGame();
        }

        [ContextMenu("Init Game")]
        public void InitGame()
        {
            this.gameContext.InitGame();
        }

        [ContextMenu("Ready Game")]
        public void ReadyGame()
        {
            this.gameContext.ReadyGame();
        }

        [ContextMenu("Start Game")]
        public void StartGame()
        {
            this.enabled = true;
            this.gameContext.StartGame();
        }

        [ContextMenu("Pause Game")]
        public void PauseGame()
        {
            this.enabled = false;
            this.gameContext.PauseGame();
        }

        [ContextMenu("Resume Game")]
        public void ResumeGame()
        {
            this.enabled = true;
            this.gameContext.ResumeGame();
        }

        [ContextMenu("Finish Game")]
        public void FinishGame()
        {
            this.enabled = false;
            this.gameContext.FinishGame();
        }

        public void SetupTasks(IConstructTask[] tasks)
        {
            this.gameContext.SetupTasks(tasks);
        }

        public void RegisterElement(IGameElement element)
        {
            this.gameContext.RegisterElement(element);
        }

        public void UnregisterElement(IGameElement element)
        {
            this.gameContext.UnregisterElement(element);
        }

        public object[] GetAllElements()
        {
            return this.gameContext.GetAllElements();
        }

        public void RegisterService(object service)
        {
            this.gameContext.RegisterService(service);
        }

        public void UnregisterService(object service)
        {
            this.gameContext.UnregisterService(service);
        }

        public T GetService<T>()
        {
            return this.gameContext.GetService<T>();
        }

        public object[] GetServices(Type type)
        {
            return this.gameContext.GetServices(type);
        }

        public object[] GetAllServices()
        {
            return this.gameContext.GetAllServices();
        }

        public object GetService(Type type)
        {
            return this.gameContext.GetService(type);
        }

        public bool TryGetService<T>(out T service)
        {
            return this.gameContext.TryGetService(out service);
        }

        public bool TryGetService(Type type, out object service)
        {
            return this.gameContext.TryGetService(type, out service);
        }

        public T[] GetServices<T>()
        {
            return this.gameContext.GetServices<T>();
        }

        public MonoGameContext()
        {
            this.gameContext.OnRegistered += this.updateContext.AddListener;
            this.gameContext.OnUnregistered += this.updateContext.RemoveListener;
        }

        private void Awake()
        {
            if (this.autoRun)
            {
                this.StartCoroutine(this.AutoRun());
            }
            else
            {
                this.enabled = false;
            }
        }

        private void FixedUpdate()
        {
            this.updateContext.FixedUpdate(Time.fixedDeltaTime);
        }

        private void Update()
        {
            this.updateContext.Update(Time.deltaTime);
        }

        private void LateUpdate()
        {
            this.updateContext.LateUpdate(Time.deltaTime);
        }

        private void OnDestroy()
        {
            this.gameContext.OnRegistered -= this.updateContext.AddListener;
            this.gameContext.OnUnregistered -= this.updateContext.RemoveListener;
        }

        private void RegisterElements()
        {
            for (int i = 0, count = this.gameElements.Count; i < count; i++)
            {
                var monoElement = this.gameElements[i];
                if (monoElement is IGameElement gameElement)
                {
                    this.RegisterElement(gameElement);
                }
            }
        }

        private void RegisterServices()
        {
            for (int i = 0, count = this.gameServices.Count; i < count; i++)
            {
                var monoService = this.gameServices[i];
                if (monoService != null)
                {
                    this.RegisterService(monoService);
                }
            }
        }

        private void RegisterConstructTasks()
        {
            var count = this.constructTasks.Count;
            var tasks = new IConstructTask[count];
            for (var i = 0; i < count; i++)
            {
                tasks[i] = this.constructTasks[i];
            }

            this.gameContext.SetupTasks(tasks);
        }

        private IEnumerator AutoRun()
        {
            yield return new WaitForEndOfFrame();
            this.ConstructGame();
            this.InitGame();
            this.ReadyGame();
            this.StartGame();
        }

#if UNITY_EDITOR
        public void Editor_AddElement(MonoBehaviour gameElement)
        {
            this.gameElements.Add(gameElement);
        }

        public void Editor_AddService(MonoBehaviour gameService)
        {
            this.gameServices.Add(gameService);
        }

        public void Editor_AddConstructTask(ConstructTask task)
        {
            this.constructTasks.Add(task);
        }

        private void OnValidate()
        {
            EditorValidator.ValidateServices(ref this.gameServices);
            EditorValidator.ValidateElements(ref this.gameElements);
        }
#endif
    }
}