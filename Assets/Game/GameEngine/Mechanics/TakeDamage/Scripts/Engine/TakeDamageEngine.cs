using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;


public sealed class TakeDamageEngine : MonoBehaviour
{
    public event Action<TakeDamageEvent> OnDamageTaken;
    public event Action OnDamageTakenFinished;

    [SerializeField]
    private HitPointsEngine hitPointsEngine;

    [SerializeField]
    private DestroyReceiver destroyReceiver;

    [Button]
    [GUIColor(0, 1, 0)]
    public void TakeDamage(TakeDamageEvent damageEvent)
    {
        if (this.hitPointsEngine.CurrentHitPoints <= 0)
        {
            return;
        }

        this.hitPointsEngine.CurrentHitPoints -= damageEvent.damage;
        this.OnDamageTaken?.Invoke(damageEvent);

        StartCoroutine(FinishTakeDamageRoutine());

        if (this.hitPointsEngine.CurrentHitPoints <= 0)
        {
            var destroyEvent = ComposeDestroyEvent(damageEvent);
            this.destroyReceiver?.Invoke(destroyEvent);
        }
    }

    private IEnumerator FinishTakeDamageRoutine()
    {
        yield return new WaitForSeconds(0.22f);
        OnDamageTakenFinished?.Invoke();
    }

    private static DestroyEvent ComposeDestroyEvent(TakeDamageEvent damageEvent)
    {
        var damageReason = damageEvent.reason;
        DestroyReason destroyReason;
        if (damageReason == TakeDamageReason.SELF)
        {
            destroyReason = DestroyReason.SELF;
        }
        else if (damageReason == TakeDamageReason.BULLET)
        {
            destroyReason = DestroyReason.BULLET;
        }
        else if (damageReason == TakeDamageReason.MELEE)
        {
            destroyReason = DestroyReason.ATTACKER;
        }
        else
        {
            destroyReason = DestroyReason.UNDEFINED;
        }

        return new DestroyEvent(destroyReason, damageEvent.source);
    }
}