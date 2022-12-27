using System.Collections;
using UnityEngine;
using Elementary;

public class LunchProjectileState : StateCoroutine
{
    [SerializeField]
    private ProjectileEngine _projectileEngine;

    [SerializeField]
    private RangeAttackEngine _rangeAttackEngine;

    [Header("Pre-Shot Countdown")]
    [SerializeField]
    private float _preshotCountdown;

    protected override IEnumerator Do()
    {
        yield return new WaitForSeconds(_preshotCountdown);

        _projectileEngine.ShootProjectile();
        _rangeAttackEngine.FinishAttack();
    }
}
