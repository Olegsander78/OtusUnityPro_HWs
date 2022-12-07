using System.Collections.Generic;
using GameElements;
using GameElements.Unity;


public sealed class GameManager
{
    private readonly List<object> registeredServices;

    private readonly List<object> registeredElements;

    private IGameContext gameContext;

    private bool gameSetuped;

    public GameManager()
    {
        this.registeredServices = new List<object>();
        this.registeredElements = new List<object>();
    }

    public void Setup(MonoGameContext gameContext)
    {
        for (int i = 0, count = this.registeredServices.Count; i < count; i++)
        {
            var service = this.registeredServices[i];
            gameContext.RegisterService(service);
        }

        for (int i = 0, count = this.registeredElements.Count; i < count; i++)
        {
            var element = this.registeredElements[i];
            if (element is IGameElement gameElement)
            {
                gameContext.RegisterElement(gameElement);
            }
        }

        gameContext.LoadGame();

        this.gameSetuped = true;
        this.gameContext = gameContext;
    }

    public void InitGame()
    {
        this.gameContext.InitGame();
    }

    public void ReadyGame()
    {
        this.gameContext.ReadyGame();
    }

    public void StartGame()
    {
        this.gameContext.StartGame();
    }

    public T GetService<T>()
    {
        return this.gameContext.GetService<T>();
    }

    public bool TryGetService<T>(out T result)
    {
        return this.gameContext.TryGetService(out result);
    }

    public void RegisterService(object service)
    {
        this.registeredServices.Add(service);
        if (this.gameSetuped)
        {
            this.gameContext.RegisterService(service);
        }
    }

    public void UnregisterService(object service)
    {
        this.registeredServices.Remove(service);
        if (this.gameSetuped)
        {
            this.gameContext.UnregisterService(service);
        }
    }

    public void RegisterElement(IGameElement element)
    {
        this.registeredElements.Add(element);
        if (this.gameSetuped)
        {
            this.gameContext.RegisterElement(element);
        }
    }

    public void UnregisterElement(IGameElement element)
    {
        this.registeredElements.Remove(element);
        if (this.gameSetuped)
        {
            this.gameContext.UnregisterElement(element);
        }
    }
}