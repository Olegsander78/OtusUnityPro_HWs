using UnityEngine;
using Elementary;

public class ProjectileEngine : MonoBehaviour
{
    [SerializeField]
    private FloatBehaviour _speedProjectile;

    [SerializeField]
    private IntBehaviour _damageProjectile;

    [SerializeField]
    private float _lifeTimeProjectile = 3f;

    [SerializeField]
    private GameObject _projectilePrefab;

    public void ShootProjectile()
    {
        GameObject proj = Instantiate(_projectilePrefab, transform.position + Vector3.up, Quaternion.identity);
        proj.GetComponent<Rigidbody>().velocity = transform.forward * _speedProjectile.Value;
        proj.GetComponent<Projectile>().Damage.Assign(_damageProjectile.Value);
        Destroy(proj, _lifeTimeProjectile);
    }
}
