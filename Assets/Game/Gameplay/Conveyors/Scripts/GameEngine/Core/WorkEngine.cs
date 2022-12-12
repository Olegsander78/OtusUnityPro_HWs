using Elementary;
using UnityEngine;


public sealed class WorkEngine : MonoBehaviour
{
    [Space]
    [SerializeField]
    private TimerBehaviour _workTimer;

    [SerializeField]
    private LimitedIntBehavior _loadStorage;

    [SerializeField]
    private LimitedIntBehavior _unloadStorage;

    private void OnEnable()
    {
        _workTimer.OnFinished += OnWorkFinished;
    }
    private void OnDisable()
    {
        _workTimer.OnFinished -= OnWorkFinished;
    }


    private void Update()
    {
        if (CanStartWork())
        {
            StartWork();
        }
    }
 
    private bool CanStartWork()
    {
        if (_workTimer.IsPlaying)
        {
            return false;
        }

        if (_loadStorage.Value == 0)
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
        _loadStorage.Value--;
        _workTimer.ResetTime();
        _workTimer.Play();
    }

    private void OnWorkFinished()
    {
        _unloadStorage.Value++;
    }
}