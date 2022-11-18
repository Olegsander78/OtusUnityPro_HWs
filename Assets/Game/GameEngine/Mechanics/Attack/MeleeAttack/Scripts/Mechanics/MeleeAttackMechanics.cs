using UnityEngine;
using Elementary;
using System;
using Entities;

public class MeleeAttackMechanics : MonoBehaviour
{
    [SerializeField]
    private EventReceiver_Entity _meleeAttackReciever;

    [SerializeField]
    private TimerBehaviour _countdown;

    [SerializeField]
    private IntBehaviour _damage;

    private void OnEnable()
    {
        _meleeAttackReciever.OnEvent += OnRequestMeleeAttack;
    }
    private void OnDisable()
    {
        _meleeAttackReciever.OnEvent -= OnRequestMeleeAttack;
    }

    private void OnRequestMeleeAttack(UnityEntityBase target)
    {
        if (_countdown.IsPlaying)
            return;

        target.Get<IComponent_TakeDamage>().TakeDamage(_damage.Value);

        _countdown.ResetTime();
        _countdown.Play();
    }
}
