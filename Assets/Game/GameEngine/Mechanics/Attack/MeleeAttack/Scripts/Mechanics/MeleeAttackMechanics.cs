using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Elementary;
using System;

public class MeleeAttackMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver _meleeAttackReciever;

    [SerializeField]
    private TimerBehaviour _countdown;

    [SerializeField]
    private IntBehaviour _damage;

    [SerializeField]
    private EnemyDummy _enemy;

    private void OnEnable()
    {
        _meleeAttackReciever.OnEvent += OnRequestMeleeAttack;
    }
    private void OnDisable()
    {
        _meleeAttackReciever.OnEvent -= OnRequestMeleeAttack;
    }

    private void OnRequestMeleeAttack()
    {
        if (_countdown.IsPlaying)
            return;

        _enemy.TakeDamage(_damage.Value);

        _countdown.ResetTime();
        _countdown.Play();
    }
}
