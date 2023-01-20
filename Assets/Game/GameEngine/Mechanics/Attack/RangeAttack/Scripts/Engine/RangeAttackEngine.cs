using UnityEngine;
using Elementary;
using System;

public class RangeAttackEngine : MonoBehaviour
{
    public event Action OnRangeAttackStarted;

    public event Action OnRangeAttackFinished;

    public TimerBehaviour ShotFullCountdown => _shotFullCountdown;

    [Header("Shot Full Countdown")]
    [SerializeField]
    private TimerBehaviour _shotFullCountdown;

    [Header("Pre-Shot Countdown")]
    [SerializeField]
    private float _preshotCountdown;

    private void OnEnable()
    {
        _shotFullCountdown.OnFinished += FinishAttack;
    }

    private void OnDisable()
    {
        _shotFullCountdown.OnFinished -= FinishAttack;
    }
    public void TryShoot()
    {
        if (_shotFullCountdown.IsPlaying)
            return;

        _shotFullCountdown.ResetTime();
        _shotFullCountdown.Play();
        OnRangeAttackStarted?.Invoke();
    }

    public void FinishAttack()
    {
        OnRangeAttackFinished?.Invoke();
    }
}
