using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

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

    [Header("Start Game Timer")]
    [SerializeField]
    private float _delay;
    [SerializeField]
    private float _countdown;

    private float _startDelay;

    private void Awake()
    {
      _startDelay = _delay;
    }

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
        StartGameTimer();
        OnGameStarted?.Invoke();
    }

    private void StartGameTimer()
    {
        StartCoroutine(StartGameRoutine());
    }

    private IEnumerator StartGameRoutine()
    {
        while (_delay > 0)
        {
            Debug.Log($"Start in {_delay} second!");
            _delay--;
            yield return new WaitForSeconds(_countdown);
        }

        foreach (var listener in _listeners)
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
        foreach (var listener in _listeners)
        {
            if (listener is IFinishGameListener finishListener)
            {
                finishListener.OnFinishGame();
            }
        }

        Debug.Log("Game Finished!");

        _delay = _startDelay;

        OnGameFinished?.Invoke();
    }
}