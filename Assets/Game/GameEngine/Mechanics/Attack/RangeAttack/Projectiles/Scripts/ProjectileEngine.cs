using UnityEngine;
using Elementary;

public class ProjectileEngine : MonoBehaviour
{
    public GameObject ProjectilePrefab { get => _projectilePrefab; set => value = _projectilePrefab; }

    [SerializeField]
    private FloatBehaviour _speedProjectile;

    [SerializeField]
    private IntBehaviour _damageProjectile;

    [SerializeField]
    private float _lifeTimeProjectile = 3f;

    [SerializeField]
    private GameObject _projectilePrefab;

    [SerializeField]
    private Transform _startPoint;

    public void ShootProjectile(GameObject projectilePrefab)
    {
        //GameObject proj = Instantiate(projectilePrefab, transform.position + Vector3.up, transform.rotation);
        GameObject proj = Instantiate(projectilePrefab, _startPoint.transform.position,_startPoint.transform.rotation);
        proj.GetComponent<Rigidbody>().velocity = transform.forward * _speedProjectile.Value;
        proj.GetComponent<Projectile>().Damage.Assign(_damageProjectile.Value);
        Destroy(proj, _lifeTimeProjectile);
    }
}
