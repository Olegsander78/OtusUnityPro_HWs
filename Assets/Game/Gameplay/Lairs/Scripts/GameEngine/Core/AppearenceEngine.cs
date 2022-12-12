using Elementary;
using Entities;
using UnityEngine;

public class AppearenceEngine : MonoBehaviour
{
    [Space]
    [SerializeField]
    private TimerBehaviour _workTimer;

    [SerializeField]
    private EventReceiver_Trigger _triggerZone;

    [SerializeField]
    private LimitedIntBehavior _appearenceZone;

    [SerializeField]
    private UnityEntity _player;

    private void OnEnable()
    {
        _triggerZone.OnTriggerEntered += OnAppearanceStart;
        _triggerZone.OnTriggerStaying += OnAppearanceStart;
        _triggerZone.OnTriggerExited  += OnAppearanceStop;
        _workTimer.OnFinished += OnAppearanceFinishedOnTimer;
    }    

    private void OnDisable()
    {
        _triggerZone.OnTriggerEntered -= OnAppearanceStart;
        _triggerZone.OnTriggerStaying -= OnAppearanceStart;
        _triggerZone.OnTriggerExited -= OnAppearanceStop;
        _workTimer.OnFinished -= OnAppearanceFinishedOnTimer;
    }
    
    private void OnAppearanceStart(Collider obj)
    {
        //if(obj.CompareTag("Player"))
        //{
        //    if (CanStartAppearance())
        //        StartAppearance();
        //}

        if(obj == _player.GetComponent<Collider>())
        {
            if (CanStartAppearance())
                StartAppearance();
        }
    }
    private bool CanStartAppearance()
    {
        if (_workTimer.IsPlaying)
        {
            return false;
        }

        if (_appearenceZone.IsLimit)
        {
            return false;
        }

        return true;
    }
    private void StartAppearance()
    {
        _workTimer.ResetTime();
        _workTimer.Play();
    }
    private void OnAppearanceStop(Collider obj)
    {
        if (obj.CompareTag("Player"))
        {
            StopAppearance();
        }
    }

    private void StopAppearance()
    {
        _workTimer.Stop();
    }

    private void OnAppearanceFinishedOnTimer()
    {
        _appearenceZone.Value++;
    }
}
