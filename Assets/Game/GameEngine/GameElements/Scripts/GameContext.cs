using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    public event Action OnGameStarted;    

    public event Action OnGameFinished;

    [ReadOnly]
    [ShowInInspector]
    private readonly List<object> _listeners = new();

    [ReadOnly]
    [ShowInInspector]
    private readonly List<object> _services = new();
    
    public T GetService<T>()
    {
        foreach (var service in _services)
        {
            if (service is T result)
            {
                return result;
            }
        }

        throw new Exception($"Service of type {typeof(T).Name} is not found!");
    }

    public void AddService(object service)
    {
        _services.Add(service);
    }

    public void RemoveService(object service)
    {
        _services.Remove(service);
    }

    public void AddListener(object listener)
    {
        _listeners.Add(listener);
    }

    public void RemoveListener(object listener)
    {
        _listeners.Remove(listener);
    }

    [Button]
    public void ConstructGame()
    {
        foreach (var listener in _listeners)
        {
            if (listener is IConstructListener constructListener)
            {
                constructListener.Construct(context: this);
            }
        }

        Debug.Log("Game Construct!");
    }

    [Button]
    public void StartGame()
    {  
        foreach (var listener in _listeners)
        {
            if (listener is IStartGameListener startListener)
            {
                startListener.OnStartGame();
            }
        }

        Debug.Log("Game Started!");

        OnGameStarted?.Invoke();
    }

    [Button]
    public void FinishGame()
    {
        foreach (var listener in _listeners)
        {
            if (listener is IFinishGameListener finishListener)
            {
                finishListener.OnFinishGame();
            }
        }

        Debug.Log("Game Finished!");        

        OnGameFinished?.Invoke();
    }
}