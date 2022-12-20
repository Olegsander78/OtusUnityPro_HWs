using UnityEngine;
using Elementary;
using System;

public class RangeAttackMechanic : MonoBehaviour
{
    public event Action OnRangeAttackStarted;

    public event Action OnRangeAttackFinished;

    [SerializeField]
    private EventReceiver _rangeAttackReciever; 

    [SerializeField]
    private ProjectileEngine _projectileEngine;

    [SerializeField]
    private TimerBehaviour _attackCountdown;

    private void OnEnable()
    {
        _rangeAttackReciever.OnEvent += OnRequestRangeAttack;
    }

    private void OnDisable()
    {
        _rangeAttackReciever.OnEvent -= OnRequestRangeAttack;
    }
    private void OnRequestRangeAttack()
    {
        if (_attackCountdown.IsPlaying)
            return;

        _projectileEngine.ShootProjectile(_projectileEngine.ProjectilePrefab);
        OnRangeAttackStarted?.Invoke();

        _attackCountdown.ResetTime();
        _attackCountdown.Play();
        OnRangeAttackFinished?.Invoke();
    }
}
