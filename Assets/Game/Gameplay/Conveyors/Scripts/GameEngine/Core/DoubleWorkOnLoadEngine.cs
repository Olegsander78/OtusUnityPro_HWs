using Elementary;
using System;
using UnityEngine;


public sealed class DoubleWorkOnLoadEngine : MonoBehaviour
{
        
    [Serializable]
    public struct RecipeTwoPart
    {
        public string Name;
        public string Description;
        public int AmountResourceOne;
        public int AmountResourceTwo;
    }

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
    private RecipeTwoPart _itemOnUnload;

    public RecipeTwoPart ItemOnUnload => _itemOnUnload;

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
        if (_loadStorageOne.Value < _itemOnUnload.AmountResourceOne 
            || _loadStorageTwo.Value < _itemOnUnload.AmountResourceTwo)
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
        _loadStorageOne.Value -= _itemOnUnload.AmountResourceOne;
        _loadStorageTwo.Value -= _itemOnUnload.AmountResourceTwo;
        _workTimer.ResetTime();
        _workTimer.Play();
    }

    private void OnWorkFinished()
    {
        _unloadStorage.Value++;
    }
}