using Elementary;
using System;
using UnityEngine;


public sealed class DoubleWorkOnLoadEngine : MonoBehaviour
{

    [Space]
    [SerializeField]
    private TimerBehaviour _workTimer;

    [SerializeField]
    private LimitedIntBehavior _loadStorageOne;

    [SerializeField]
    private LimitedIntBehavior _loadStorageTwo;

    [SerializeField]
    private LimitedIntBehavior _unloadStorage;

    [SerializeField]
    private ItemFromTwoResourcesInfo _itemFromTwoResourcesInfo;


    private void OnEnable()
    {
        _workTimer.OnFinished += OnWorkFinished;
    }

    private void Update()
    {
        if (CanStartWork())
        {
            StartWork();
        }
    }

    private void OnDisable()
    {
        _workTimer.OnFinished -= OnWorkFinished;
    }

    private bool CanStartWork()
    {
        if (_workTimer.IsPlaying)
        {
            return false;
        }
        if (_loadStorageOne.Value < _itemFromTwoResourcesInfo.AmountResourceOne 
            || _loadStorageTwo.Value < _itemFromTwoResourcesInfo.AmountResourceTwo)
        {
            return false;
        }

        if (_unloadStorage.IsLimit)
        {
            return false;
        }

        return true;
    }

    private void StartWork()
    {
        _loadStorageOne.Value -= _itemFromTwoResourcesInfo.AmountResourceOne;
        _loadStorageTwo.Value -= _itemFromTwoResourcesInfo.AmountResourceTwo;
        _workTimer.ResetTime();
        _workTimer.Play();
    }

    private void OnWorkFinished()
    {
        _unloadStorage.Value++;
    }
}