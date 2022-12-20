using Elementary;
using System.Collections;
using UnityEngine;

public class RangeAttackRateState : StateCoroutine
{
    [SerializeField]
    private TimerBehaviour _rateRangeAttack;

    [SerializeField]
    private ProjectileEngine _projectileEngine;


    public override void Exit()
    {
        if (_rateRangeAttack.Duration == 0f)
            base.Exit();
    }
    protected override IEnumerator Do()
    {
        var delay = new WaitForFixedUpdate();
        while (_rateRangeAttack.Duration > 0f)
        {
            yield return delay;            
        }
        Shoot();
    }

    private void Shoot()
    {
        _projectileEngine.ShootProjectile(_projectileEngine.ProjectilePrefab);
    }
}
