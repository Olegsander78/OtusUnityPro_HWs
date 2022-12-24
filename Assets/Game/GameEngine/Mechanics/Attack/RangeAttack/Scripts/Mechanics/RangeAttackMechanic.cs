using UnityEngine;
using Elementary;
using System;
using System.Collections;

public class RangeAttackMechanic : MonoBehaviour
{
    public event Action OnRangeAttackStarted;

    public event Action OnRangeAttackFinished;

    [SerializeField]
    private EventReceiver _rangeAttackReciever; 

    [SerializeField]
    private ProjectileEngine _projectileEngine;

    [Header("Shot Countdown")]
    [SerializeField]
    private TimerBehaviour _shotCountdown;

    [Header("Pre-Shot Countdown")]
    [SerializeField]
    private float _preshotCountdown;

    private void OnEnable()
    {
        _rangeAttackReciever.OnEvent += OnRequestRangeAttack;
        _shotCountdown.OnFinished += OnAttackFinished;
    }

    private void OnDisable()
    {
        _rangeAttackReciever.OnEvent -= OnRequestRangeAttack;
        _shotCountdown.OnFinished -= OnAttackFinished;
    }
    private void OnRequestRangeAttack()
    {
        if (_shotCountdown.IsPlaying)
            return;

        PreShot();

        _shotCountdown.ResetTime();
        _shotCountdown.Play();        
    }

    private void PreShot()
    {
        StartCoroutine(PreShotRoutine());
    }

    private IEnumerator PreShotRoutine()
    {
        OnRangeAttackStarted?.Invoke();

        yield return new WaitForSeconds(_preshotCountdown);

        _projectileEngine.ShootProjectile(_projectileEngine.ProjectilePrefab);        
    }

    private void OnAttackFinished()
    {
        OnRangeAttackFinished?.Invoke();
    }
}
