using Elementary;
using System.Collections;
using UnityEngine;

public class RangeAttackState : StateCoroutine
{
    [SerializeField]
    private RangeAttackMechanic _rangeAttackMechanic;

    [SerializeField]
    private ProjectileEngine _projectileEngine;

    protected override IEnumerator Do()
    {
        var delay = new WaitForSeconds(3f);
        while (true)
        {
            yield return delay;
            Shoot();
        }
    }

    private void Shoot()
    {
        _projectileEngine.ShootProjectile(_projectileEngine.ProjectilePrefab);
    }
}
