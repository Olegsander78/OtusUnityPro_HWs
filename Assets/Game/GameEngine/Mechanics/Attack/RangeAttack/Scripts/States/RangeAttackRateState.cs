using Elementary;
using System.Collections;
using UnityEngine;

public class RangeAttackRateState : StateCoroutine
{
    [SerializeField]
    private TimerBehaviour _rateRangeAttack;

    //[SerializeField]
    //private ProjectileEngine _projectileEngine;

    [SerializeField]
    private RangeAttackMechanic _rangeAttackMechanic;
     

    protected override IEnumerator Do()
    {
        _rangeAttackMechanic.OnRequestRangeAttack();        

        //Shoot();

        yield return new WaitForSeconds(_rateRangeAttack.Duration);
    }

    //private void Shoot()
    //{
    //    _projectileEngine.CreateProjectile(_projectileEngine.ProjectilePrefab);
    //    _projectileEngine.ShootProjectile();
    //}
}
