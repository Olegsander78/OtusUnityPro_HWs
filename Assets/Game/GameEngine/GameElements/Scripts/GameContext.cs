using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{
    [ReadOnly]
    [ShowInInspector]
    private readonly List<object> listeners = new();

    [ReadOnly]
    [ShowInInspector]
    private readonly List<object> services = new();

    public T GetService<T>()
    {
        foreach (var service in this.services)
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
        this.services.Add(service);
    }

    public void RemoveService(object service)
    {
        this.services.Remove(service);
    }

    public void AddListener(object listener)
    {
        this.listeners.Add(listener);
    }

    public void RemoveListener(object listener)
    {
        this.listeners.Remove(listener);
    }

    [Button]
    public void ConstructGame()
    {
        foreach (var listener in this.listeners)
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
        foreach (var listener in this.listeners)
        {
            if (listener is IStartGameListener startListener)
            {
                startListener.OnStartGame();
            }
        }

        Debug.Log("Game Started!");
    }

    [Button]
    public void FinishGame()
    {
        foreach (var listener in this.listeners)
        {
            if (listener is IFinishGameListener finishListener)
            {
                finishListener.OnFinishGame();
            }
        }

        Debug.Log("Game Finished!");
    }
}
