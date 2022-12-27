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
        _projectileEngine.CreateProjectile();

        yield return new WaitForSeconds(_preshotCountdown);

        _projectileEngine.ShootProjectile();

        yield return new WaitForSeconds(_rangeAttackEngine.ShotFullCountdown.Duration - _preshotCountdown);

        _rangeAttackEngine.FinishAttack();
    }
}
