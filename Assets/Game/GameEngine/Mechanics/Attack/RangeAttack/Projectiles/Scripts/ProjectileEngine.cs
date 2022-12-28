using UnityEngine;
using Elementary;
using Sirenix.OdinInspector;

public class ProjectileEngine : MonoBehaviour
{
    public GameObject ProjectilePrefab { get => _projectilePrefab; set => value = _projectilePrefab; }
    public GameObject CurrentProjectile { get => _currentProjectile; set => value = _currentProjectile; }

    [SerializeField]
    private FloatBehaviour _speedProjectile;

    [SerializeField]
    private IntBehaviour _damageProjectile;

    [SerializeField]
    private float _lifeTimeProjectile = 3f;

    [SerializeField]
    private GameObject _projectilePrefab;
    
    private GameObject _currentProjectile;


    [SerializeField]
    private Transform _startPoint;

    [Button]
    public GameObject CreateProjectile()
    {
        _currentProjectile = Instantiate(_projectilePrefab, _startPoint.transform.position, _startPoint.transform.rotation);
        _currentProjectile.transform.SetParent(_startPoint.transform, true);
        _currentProjectile.SetActive(true);
        Destroy(_currentProjectile, _lifeTimeProjectile);
        return _currentProjectile;
    }

    [Button]
    public void ShootProjectile()
    {
        if(_currentProjectile != null)
        {
            _currentProjectile.transform.parent= null;
            _currentProjectile.GetComponent<Rigidbody>().velocity = transform.forward * _speedProjectile.Value;
            _currentProjectile.GetComponent<Projectile>().Damage.Assign(_damageProjectile.Value);            
            Destroy(_currentProjectile, _lifeTimeProjectile);
        }
    }
}


