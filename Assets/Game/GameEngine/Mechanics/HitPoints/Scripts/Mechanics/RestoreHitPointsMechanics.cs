using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementary;
using Sirenix.OdinInspector;


public sealed class RestoreHitPointsMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver_Int _takeDamageReceiver;

    [SerializeField]
    private IntBehaviour _curHitPoints;

    [SerializeField]
    private IntBehaviour _maxHitPoints;

    [SerializeField]
    private IntBehaviour _amountRestoreHPPerOne;

    [SerializeField]
    private TimerBehaviour _delay;

    [SerializeField]
    private PeriodBehaviour _restorePeriod;

    private void OnEnable()
    {
        _takeDamageReceiver.OnEvent += OnDamageTaken;
        _delay.OnFinished += OnDelayEnded;
        _restorePeriod.OnPeriodEvent += OnRestoreHitPoints;
    }

    private void OnDisable()
    {
        _takeDamageReceiver.OnEvent -= OnDamageTaken;
        _delay.OnFinished -= OnDelayEnded;
        _restorePeriod.OnPeriodEvent -= OnRestoreHitPoints;
    }

    private void OnDamageTaken(int damage)
    {
        if (_curHitPoints.Value <= 0)
            return;
        
        //—брос задержки:
        _delay.ResetTime();
        if (!_delay.IsPlaying)
        {
            _delay.Play();
        }

        _restorePeriod.Stop();
    }

    private void OnDelayEnded()
    {
        _restorePeriod.Play();
    }

    private void OnRestoreHitPoints()
    {
        _curHitPoints.Value += _amountRestoreHPPerOne.Value;
        if (_curHitPoints.Value >= _maxHitPoints.Value)
        {
            _restorePeriod.Stop();
            _curHitPoints.Value = _maxHitPoints.Value;
        }
    }
}
